using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            var knoten = new List<Knoten> {
                new Knoten("A", null, null),
                new Knoten("B", null, null),
                new Knoten("C", null, null),
                new Knoten("D", null, null),
                new Knoten("E", null, null)
            };


            var kanten = new List<Kanten> {
                new Kanten("A", "B", 5),
                new Kanten("B", "D", 1),
                new Kanten("E", "D", 1),
                new Kanten("A", "E", 3),
                new Kanten("A", "C", 1),
                new Kanten("C", "E", 1)
            };


            var strecken = Dijkstra(knoten, kanten, "A", "B").OrderBy(x => x.Distanz);

            foreach (var item in strecken)
            {
                Console.WriteLine(item.Name);
            }

            Console.ReadKey();
        }

        public static List<Knoten> Dijkstra(List<Knoten> knoten, List<Kanten> kanten, string start, string ziel)
        {
            var strecken = new List<Knoten>();
            var queue = new List<Knoten>();

            for (int i = 0; i < knoten.Count; i++)
            {
                var item = knoten[i].Name;
                if (item == start)
                {
                    knoten[i].Distanz = 0;
                }

                queue.Add(knoten[i]);
            }

            while (queue.Any())
            {
                var u = queue.OrderBy(x => x.Distanz).FirstOrDefault(x => x.Distanz != null);
                queue.Remove(u);

                foreach (var queueElement in queue)
                {
                    var kante = kanten.FirstOrDefault(x => (x.Start == u.Name || x.Start == queueElement.Name) && (x.End == queueElement.Name || x.End == u.Name));
                    if (kante == null)
                    {
                        continue;
                    }

                    if (u.Distanz + kante.Strecke < queueElement.Distanz || queueElement.Distanz == null)
                    {
                        queueElement.Distanz = u.Distanz + kante.Strecke;
                        queueElement.Vorgaenger = u.Name;
                    }
                }

                strecken.Add(u);
            }

            var streckenResult = new List<Knoten>();
            var end = strecken.FirstOrDefault(x => x.Name == ziel);
            var previous = end.Vorgaenger;
            streckenResult.Add(end);

            while (previous != null)
            {
                var current = strecken.FirstOrDefault(x => x.Name == previous);

                if (current == null)
                {
                    break;
                }

                streckenResult.Add(current);

                previous = current.Vorgaenger;
            }
            
            return streckenResult;
        }
    }

    class Knoten
    {
        public string Name { get; set; }

        public int? Distanz { get; set; } = null;

        public string Vorgaenger { get; set; } = null;

        public Knoten(string name, int? distanz, string vorgaenger)
        {
            Name = name;
            Distanz = distanz;
            Vorgaenger = vorgaenger;
        }
    }

    class Kanten
    {
        public string Start { get; set; }

        public string End { get; set; }

        public int Strecke { get; set; }

        public Kanten(string start, string end, int strecke)
        {
            Start = start;
            End = end;
            Strecke = strecke;
        }
    }
}
