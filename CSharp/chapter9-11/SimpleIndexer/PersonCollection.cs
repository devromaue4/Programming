using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIndexer
{
    class PersonCollection : IEnumerable
    {
        private ArrayList arrPeople = new ArrayList();

        // Специальный для этого класса
        public Person this[int index]
        {
            get { return (Person)arrPeople[index]; }
            set { arrPeople.Insert(index, value); }
        }

        public int Count
        { get { return arrPeople.Count; } }

        // Cast for caller.
        public Person GetPerson(int pos)
        { return (Person)arrPeople[pos]; }

        // Only insert Person types.
        public void AddPerson(Person p)
        { arrPeople.Add(p); }

        public void ClearPeople()
        { arrPeople.Clear(); }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return arrPeople.GetEnumerator();
        }
    }
}
