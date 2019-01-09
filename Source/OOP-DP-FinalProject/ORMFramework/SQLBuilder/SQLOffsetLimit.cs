using System;
namespace OOPDPFinalProject.ORMFramework.SQLBuilder
{
    public class SQLOffsetLimit:SQLBuilder
    {
        int _offset = -1;
        int _limit = -1;
        public string getString()
        {
            return _adapter.OffSetAndLimit(_offset, _limit);
        }
        public void SetOffSet(int OffSet)
        {
            _offset = OffSet;
        }
        public void SetLimit(int Limit)
        {
            _limit = Limit;
        }
        public void GetAll()
        {
            _offset = -1;
            _limit =  -1;
        }

        protected override string ComponentsToString()
        {
            throw new NotImplementedException();
        }

        public SQLOffsetLimit()
        {
        }


    }
}
