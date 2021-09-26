using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TTO.UnitTests
{
    public class PersonRepositoryTest: IClassFixture<PersonRepository>
    {
        private readonly PersonRepository _repository;

        public PersonRepositoryTest(PersonRepository repository)
        {
            _repository = repository;
        }

        [Fact]
        public void PersonWithId1ShouldBeJanKowalski()
        {
            var personId = 1;

            var jan = _repository.Get(personId);

            jan.Id.Should().Be(personId);
            jan.FirstName.Should().Be("Jan");
            jan.LastName.Should().Be("Kowalski");
        }

        [Fact]
        public void PersonWithId3ShouldNotExits()
        {
            var personId = 3;

            var person = _repository.Get(personId);

            person.Should().BeNull();
        }

    }
}
