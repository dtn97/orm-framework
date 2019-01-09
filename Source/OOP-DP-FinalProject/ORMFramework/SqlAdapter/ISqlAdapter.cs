/* License: http://www.apache.org/licenses/LICENSE-2.0 */

using System;
using System.Collections.Generic;

namespace OOPDPFinalProject.ORMFramework
{
    /// <summary>
    /// SQL adapter provides db specific functionality related to db specific SQL syntax
    /// </summary>
    public interface ISqlAdapter
    {
        string QueryString(string selection, string source, string conditions, 
            string order, string grouping, string having);

        string OffSetAndLimit(int offset, int limit);
        string QueryString(string selection, string source, IDictionary<string, object> WhereParams);

        string Table(string tableName);
        string Field(string tableName, string fieldName);
        string Parameter(string parameterId);

        string UpdateString(string tableName, string primaryKey, Dictionary<string,string> fieldValues);
        string InsertString(string tableName, Dictionary<string, string> fieldValues);
        string DeleteString(string tableName, string primaryKey, Dictionary<string, string> fieldValues);
    }
}
