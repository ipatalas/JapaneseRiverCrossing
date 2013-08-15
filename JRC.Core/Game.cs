using System;
using System.Collections.Generic;
using System.Linq;

namespace JRC.Core
{
	using System.IO;
	using Exceptions;

	public class Game : IGameState
	{
		#region [ Fields & Properties ]
		private readonly List<Person> source;
		public IReadOnlyList<Person> Source
		{
			get
			{
				return source;
			}
		}

		private readonly List<Person> destination;
		public IReadOnlyList<Person> Destination
		{
			get
			{
				return destination;
			}
		}

		private readonly List<IRule> Rules;

		public RaftPosition RaftPosition { get; private set; }
		#endregion

		public Game(IEnumerable<IRule> gameRules)
		{
			source = new List<Person>();
			source.AddRange(new[] { 
				new Person(PersonType.Father), 
				new Person(PersonType.Son), 
				new Person(PersonType.Son),
				new Person(PersonType.Mother),
				new Person(PersonType.Daughter),
				new Person(PersonType.Daughter),
				new Person(PersonType.Policeman),
				new Person(PersonType.Thief)
			});

			destination = new List<Person>();
			Rules = gameRules.ToList();
		}

		public void MoveToDestination(params Person[] peopleToMove)
		{
			ThrowRaftNotAvailableIf(RaftPosition != RaftPosition.Source);

			Move(source, destination, peopleToMove);
		}

		public void MoveToSource(params Person[] peopleToMove)
		{
			ThrowRaftNotAvailableIf(RaftPosition != RaftPosition.Destination);

			Move(destination, source, peopleToMove);
		}

		public void Save(Stream stream, DataFormat format)
		{
			format.SaveGame(this, stream);
		}

		private void Move(List<Person> sourceSide, List<Person> destinationSide, params Person[] peopleToMove)
		{
			if (sourceSide.Intersect(peopleToMove).Count() != peopleToMove.Length)
			{
				throw new InvalidOperationException("At least one person is not on the source side");
			}

			var sourceToValidate = sourceSide.Except(peopleToMove).ToList();
			var destinationToValidate = destinationSide.Union(peopleToMove).ToList();

			Rules.ForEach(r =>
			{
				r.Validate(peopleToMove, sourceToValidate);
				r.Validate(peopleToMove, destinationToValidate);
			});

			foreach (Person person in peopleToMove)
			{
				sourceSide.Remove(person);
				destinationSide.Add(person);
			}
			MoveRaftPosition();
		}

		private void ThrowRaftNotAvailableIf(bool condition)
		{
			if (condition)
			{
				throw new RaftNotAvailableException();
			}
		}

		private void MoveRaftPosition()
		{
			RaftPosition = RaftPosition == RaftPosition.Destination ? RaftPosition.Source : RaftPosition.Destination;
		}
	}
}
