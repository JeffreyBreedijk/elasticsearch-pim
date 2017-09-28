using System.Collections.Generic;
using System.Linq;
using ElasticSearch.Models;
using Nest;

namespace ElasticSearch.Services.ElasticSearch
{
    public static class ElasticQueryHelper
    {
       
        public static QueryContainer QueryBuilder(string queryString, string lang, Dictionary<string, string> properties)
        {
            if (properties != null && properties.Count > 0)
            {
                return TextQuery(queryString, lang) && PropertiesQuery(properties);
            }
            return TextQuery(queryString, lang);
        }

        private static MultiMatchQuery TextQuery(string queryString, string lang)
        {
            return new MultiMatchQuery()
            {
                Fields = new List<string>()
                {
                    "id", 
                    string.Format("shortDescription.{0}", lang), 
                    string.Format("longDescription.{0}", lang)
                }.ToArray(),
                Query = queryString,
                Operator = Operator.Or,
                Fuzziness = Fuzziness.Auto
            };
        }
        
        private static BoolQuery PropertiesQuery(Dictionary<string, string> properties)
        {
            return new BoolQuery()
            {
                Must = properties.Select(p => ProduceQueryContainer("properties.", p.Key, p.Value)).ToArray()
            };
        }

        private static QueryContainer ProduceQueryContainer(string prefix, string key, string value)
        {
            return Query<Product>
                .Match(m => 
                    m.Field(prefix + key)
                        .Query(value));
        }
    }
}