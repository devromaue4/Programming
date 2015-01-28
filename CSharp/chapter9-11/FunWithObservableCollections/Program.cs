using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithObservableCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            // Сделать коллекцию наблюдаемой и добавить в нее несколько объектов Person
            ObservableCollection<Person> people = new ObservableCollection<Person>()
            {
                new Person{ FirstName="Peter", LastName = "Murphy", Age = 52 },
                new Person{ FirstName="Kevin", LastName = "Key", Age = 48 }
            };

            // Привязаться к событию 
            people.CollectionChanged += people_CollectionChanged;

            people.Add(new Person { FirstName = "Fred", LastName = "Smit", Age = 32 });
            
            people.Remove(people[0]);

            Console.ReadLine();
        }

        static void people_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Выяснить действие, которое привело к генерации события
            Console.WriteLine("Action for this event: {0}", e.Action);

            // Было что-то удалено
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine("Here are the OLD items:");
                foreach(Person item in e.OldItems)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.WriteLine();
            }

            // Было что-то добавлено
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine("Here are the NEW items:");
                foreach (Person item in e.NewItems)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.WriteLine();
            }
        }
    }
}
