using System;
namespace OOPDPFinalProject.ORMFramework
{
    public abstract class DomainObject
    {
        enum LoadStatus { GHOST, LOADED, LOADING };
        private LoadStatus status;
        protected object key;
        public object getKey()
        {
            return key;
        }

        protected DomainObject()
        {
            status = LoadStatus.GHOST;
        }

        public Boolean IsGhost()
        {
            return (status == DomainObject.LoadStatus.GHOST);
        }


        public Boolean IsLoaded()
        {
            return (status == DomainObject.LoadStatus.LOADED);
        }


        private void MarkLoading()
        {
            status = LoadStatus.LOADING;
        }

        private void MarkLoaded()
        {
            status = LoadStatus.LOADED;
        }

        protected void Load<T>() where T : class, new()
        {
            if (IsGhost())
            {
                MarkLoading();
                ORMFramework.Session.getCurSession().Table<T>().DoLoadLine(this);
                MarkLoaded();
            }
        }

        protected void SetNew<T>() where T : class, new()
        {
            ORMFramework.Session.getCurSession().Table<T>().unitOfWork.RegisterNew(this);
        }

        protected void SetRemoved<T>() where T : class, new()
        {
            ORMFramework.Session.getCurSession().Table<T>().unitOfWork.RegisterRemoved(this);
        }

        protected bool IsExisted<T>() where T : class, new()
        {
            return ORMFramework.Session.getCurSession().Table<T>().unitOfWork.IsExisted(this.getKey());
        }

        protected void CheckDirty<T>() where T : class, new()
        {
            Load<T>();
            if (IsLoaded())
                ORMFramework.Session.getCurSession().Table<T>().unitOfWork.RegisterDirty(this);
        }

    }
}
