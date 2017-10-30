using System;
using System.Collections.Generic;
using NLipsum.Core;
using UtilizePimModels;

namespace UtilizePimDataMocker.Service
{
    
    public static class MockProductBuilder 
    {
        private static readonly List<string> Brands = new List<string>()
        {
            "adidas", "asics", "puma", "nike", "billabong", "converse", "dunlop", "fila", "lacoste", "hummel", "reebok"
        };
        
        private static readonly List<string> Colours = new List<string>()
        {
            "blue", "red", "yellow", "green", "purple", "white", "black", "grey"
        };
        
        private static readonly List<string> Shapes = new List<string>()
        {
            "circle", "triangle", "square", "pentagon", "hexagon", "heptagon", "octagon", "star" 
        };
        
        private static readonly List<string> Flavours = new List<string>()
        {
            "sweet","sour","salt","bitter"
        };
        
        private static readonly List<string> Materials = new List<string>()
        {
            "metal", "wood", "silver", "gold", "copper", "glass"
        };
        
        public static Product GenerateProduct()
        {
            return new Product()
            {
                Id = Guid.NewGuid().ToString(),
                ShortDescription = GenerateShortDescription(),
                LongDescription = GenerateLongDescription(),
                Properties = GenerateProperties()
                  
            };
        }

        private static Dictionary<string, string> GenerateLongDescription()
        {
            return new Dictionary<string, string>
            {
                {"en", GetParagraphGenerator(Lipsums.TheRaven).GenerateLipsum(1)},
                {"de", GetParagraphGenerator(Lipsums.InDerFremde).GenerateLipsum(1)},
                {"es", GetParagraphGenerator(Lipsums.TierrayLuna).GenerateLipsum(1)}
            };

        }

        private static Dictionary<string, string> GenerateShortDescription()
        {
            return new Dictionary<string, string>
            {
                {"en", new LipsumGenerator (Lipsums.TheRaven, false).GenerateSentences(1, Sentence.Medium)[0]},
                {"de", new LipsumGenerator (Lipsums.InDerFremde, false).GenerateSentences(1, Sentence.Medium)[0]},
                {"es", new LipsumGenerator (Lipsums.TierrayLuna, false).GenerateSentences(1, Sentence.Medium)[0]}
            };
        }

        private static Dictionary<string, object> GenerateProperties()
        {
            
            return new Dictionary<string, object>()
            {
                {"brand", GetRandomFromList(Brands)},
                {"colour", GetRandomFromList(Colours)},
                {"shape", GetRandomFromList(Shapes)},
                {"flavour", GetRandomFromList(Flavours)},
                {"material", GetRandomFromList(Materials)},
                {"length", GetRandomDoubleBetween(0.5, 7.5)},
                {"width", GetRandomDoubleBetween(0.02, 2.0)},
                {"weight", GetRandomDoubleBetween(5.0, 10.0)},
                {"volume", GetRandomDoubleBetween(0.6, 0.8)},
                {"powerCost", GetRandomDoubleBetween(0.05, 0.07)}
            };
        }

        private static LipsumGenerator GetParagraphGenerator(string lipsum)
        {
            var generator = new LipsumGenerator (lipsum, false);

            var options = Paragraph.Long;
            options.SentenceOptions = Sentence.Medium;
            options.SentenceOptions.FormatString = FormatStrings.Sentence.Exclamation;

            return generator;
        }

        private static string GetRandomFromList(IReadOnlyList<string> strings)
        {
            var random = new Random();
            return strings[random.Next(strings.Count)];
        }

        private static double GetRandomDoubleBetween(double minimum, double maximum)
        {
            var random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        
        
    }
}