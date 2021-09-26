using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;

namespace TTO.UnitTests
{    
    public class PersonRepository
    {
        private readonly IEnumerable<Person> _people;

        public PersonRepository()
        {
            _people = LoadFromCsv();
        }

        public Person Get(int id) => _people.FirstOrDefault(x => x.Id.Equals(id));

        public bool ExistsWithId(int id) => _people.Any(x => x.Id.Equals(id));

        private IEnumerable<Person> LoadFromCsv()
        {
            using var reader = new StreamReader(@"C:\Users\piotr\source\repos\TTO\TTO.UnitTests\Persons.txt");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csv.GetRecords<Person>().ToList();
        }

    }
}
