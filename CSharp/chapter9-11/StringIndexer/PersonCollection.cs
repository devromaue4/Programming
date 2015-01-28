using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringIndexer
{
    class PersonCollection : IEnumerable
    {
        //private ArrayList arrPeople = new ArrayList();

        private Dictionary<string, Person> listPeople = new Dictionary<string, Person>();

        // Специальный для этого класса
        public Person this[string name]
        {
            get { return (Person)listPeople[name]; }
            set { listPeople[name] = value; }
        }

        public int Count
        { get { return listPeople.Count; } }

        //// Cast for caller.
        //public Person GetPerson(int pos)
        //{ return (Person)arrPeople[pos]; }

        //// Only insert Person types.
        //public void AddPerson(Person p)
        //{ arrPeople.Add(p); }

        public void ClearPeople()
        { listPeople.Clear(); }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return listPeople.GetEnumerator();
        }
    }
}
