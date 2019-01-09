using System;
namespace Generator.DataTypeMapper
{
    public class MySQLDataTypeMapper : DataTypeMapper
    {
        public MySQLDataTypeMapper()
        {
            mapper.Add(typeof(string), "text");
            mapper.Add(typeof(int), "integer");
            mapper.Add(typeof(float), "float");


            dbMapper.Add("text", "string");
            dbMapper.Add("integer", "int");
            dbMapper.Add("float", "float");
            dbMapper.Add("int(11)", "int");
        }
    }
}
