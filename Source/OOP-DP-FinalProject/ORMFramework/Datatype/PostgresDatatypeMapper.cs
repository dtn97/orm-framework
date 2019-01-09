using System;
namespace OOPDPFinalProject.ORMFramework.Datatype
{
    public class PostgresDatatypeMapper : DataTypeMapper
    {
        public PostgresDatatypeMapper()
        {
            mapper.Add(typeof(string), "text");
            mapper.Add(typeof(int), "integer");
            mapper.Add(typeof(float), "double precision");


            dbMapper.Add("text", "string");
            dbMapper.Add("integer", "int");
            dbMapper.Add("double precision", "float");
            dbMapper.Add("double", "double");
        }
    }
}
