using System.Collections.Generic;
using System.Linq;
using Nest;

namespace ElasticSearch.Services.ElasticSearch
{
    public static class ElasticAggregationHelper
    {
        public static AggregationDictionary AggregationBuilder(List<string> propertyFieldNames)
        {
            return StringAggregations(propertyFieldNames);
        }
        
        private static AggregationDictionary StringAggregations(IEnumerable<string> propertyFieldNames)
        {
            var aggregationDict = new AggregationDictionary();
            foreach (var propertyFieldName in propertyFieldNames)
            {
                aggregationDict.Add(propertyFieldName, new TermsAggregation(propertyFieldName)
                {
                    Field = string.Format("properties.{0}.keyword", propertyFieldName)
                });
            }
            return aggregationDict.Any() ? aggregationDict : null;
        }
    }
}