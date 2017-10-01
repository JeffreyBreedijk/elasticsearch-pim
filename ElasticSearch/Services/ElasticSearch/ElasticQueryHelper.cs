using System;
using System.Collections.Generic;
using System.Linq;
using ElasticSearch.Models;
using Nest;

namespace ElasticSearch.Services.ElasticSearch
{
    public static class ElasticQueryHelper
    {
       
        public static QueryContainer QueryBuilder(string queryString, string lang, Dictionary<string, string> properties, 
            string category)
        {
            var qc = new QueryContainer();
            qc =  qc &= TextQuery(queryString, lang);
            if (properties != null && properties.Count > 0)
            {
                 qc = qc &= StringPropertiesQuery(properties);
            }
            if (category != null)
            {
                qc &= CategoryQuery(category);
            }
            return qc;
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
        
        private static BoolQuery StringPropertiesQuery(Dictionary<string, string> properties)
        {
            return new BoolQuery()
            {
                Must = properties.Select(p => FieldMatch("properties." + p.Key, p.Value)).ToArray()
            };
        }
        
        private static BoolQuery CategoryQuery(string categoryId)
        {
            return new BoolQuery()
            {
                Must = new List<QueryContainer>()
                {
                    FieldMatch("categories", categoryId)
                }
            };
        }

        private static QueryContainer FieldMatch(string field, string value)
        {
            return Query<Product>
                .Match(m => 
                    m.Field(field)
                        .Query(value));
        }
    }
}