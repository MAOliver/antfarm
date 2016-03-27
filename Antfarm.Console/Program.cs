using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace antfarm.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var namesJson = File.ReadAllText(@".\NamesDictionary.json");
            var namesIndex = JsonConvert.DeserializeObject<NamesIndex>(namesJson);
            var results = namesIndex.CreateRandomNames(1000);
            Console.WriteLine(string.Join(",", results));
            Console.WriteLine("Hit enter to end...");

            Console.ReadLine();
        }
    }

    public class PersonalityTraits
    {
        public int Openness { get; set; }
        public int Concientiousness { get; set; }
        public int Extraversion { get; set; }
        public int Agreeableness { get; set; }
        public int Neuroticism { get; set; }
    }

    public class PhysicalTraits
    {
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Build { get; set; }
        public int Complexion { get; set; }
        public int Sex { get; set; }
        public int Age { get; set; }
    }

    public class PhysicalStats
    {
        public PhysicalTraits PhysicalTraits { get; set; }
    }

    public class MentalStats
    {
        public PersonalityTraits PersonalityTraits { get; set; }
        public Interests Interests { get; set; }
        public string Name { get; set; }
    }

    public class Interests
    {
        public List<Topic> Topics { get; set; }
    }

    public enum Topic
    {
        Art, Thought, Vocabulary, Work, People
    }

    public class Being
    {
        public PhysicalStats PhysicalStats { get; set; }
        public MentalStats MentalStats { get; set; }
    }

    public class NamesIndex
    {
        public string[] FirstNames { get; set; }

        public Surname[] Surnames { get; set; }
    }

    public static class NamesIndexExtensions
    {
        public static string CreateRandomName(this NamesIndex index, int? seed = null)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var rng = seed != null ? new Random(seed.Value) : new Random(); 
            var fnIndex = rng.Next(0, index.FirstNames.Length - 1);
            var surnameIndex = rng.Next(0, index.Surnames.Length - 1);
            return index.FirstNames[fnIndex].Substring(1) + " " + textInfo.ToTitleCase(index.Surnames[surnameIndex].Name);
        }

        public static IEnumerable<string> CreateRandomNames(this NamesIndex index, int numberToCreate, int? seed = null)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var rng = seed != null ? new Random(seed.Value) : new Random();
            for (var i = 0; i < numberToCreate; i++)
            {
                var fnIndex = rng.Next(0, index.FirstNames.Length - 1);
                var surnameIndex = rng.Next(0, index.Surnames.Length - 1);
                yield return index.FirstNames[fnIndex].Substring(1) + " " + textInfo.ToTitleCase(index.Surnames[surnameIndex].Name.ToLowerInvariant());
            }            
        }
    }


    public class Surname
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Numberofoccurrences { get; set; }
        public decimal Occurrences100000people { get; set; }
        public decimal Cumulativeproportion100000 { get; set; }
        public decimal NonHispanicWhiteOnly { get; set; }
        public decimal NonHispanicBlackOnly { get; set; }
        public decimal NonHispanicAsianPacificIslanderOnly { get; set; }
        public decimal NonHispanicAmericanIndianAlaskanNativeOnly { get; set; }
        public decimal NonHispanicof2orMoreRaces { get; set; }
        public decimal HispanicOrigin { get; set; }
    }

}
