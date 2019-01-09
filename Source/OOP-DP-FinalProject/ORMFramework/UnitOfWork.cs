using System;
using System.Collections.Generic;

namespace OOPDPFinalProject.ORMFramework
{
    public class UnitOfWork
    {
        private Dictionary<object, object> newObjects = new Dictionary<object, object>();
        private Dictionary<object, object> dirtyObjects = new Dictionary<object, object>();
        private Dictionary<object, object> removedObjects = new Dictionary<object, object>();
        private Dictionary<object, object> identityMap = new Dictionary<object, object>();

        public bool IsExisted(object key)
        {
            if (key == null)
                return false;
            return identityMap.ContainsKey(key);
        }

        public object GetObjectByKey(object key)
        {
            if (!identityMap.ContainsKey(key))
            {
                return null;
            }
            return identityMap[key];
        }

        public bool RegisterFromDatabase(ORMFramework.DomainObject obj)
        {
            if (obj == null)
            {
                return false;
            }
            identityMap.Add(obj.getKey(), obj);
            return true;
        }
        public bool RegisterNew(ORMFramework.DomainObject obj)
        {
            if (obj == null) return false;
            object key = obj.getKey();
            if (identityMap.ContainsKey(key))
                return false; 
            newObjects.Add(key, obj);
            return true;
        }
        public bool RegisterRemoved(ORMFramework.DomainObject obj)
        {
            if (obj == null || obj.IsGhost())
            {
                return false;
            }
            object key = obj.getKey();
            //1. Neu la obj moi, thi loai bo khoi danh sach them moi, ko can quan tam
            if (newObjects.ContainsKey(key))
            {
                newObjects.Remove(key);
                return true;
            }
            //2. Neu da co trong dsach loai bo roi thi ko can them nua
            if (removedObjects.ContainsKey(key)) return false;
            //3. Neu can loai bo, kiem tra co thuoc danh sach dirty hay ko, neu co thi loai luon
            if (dirtyObjects.ContainsKey(key)) dirtyObjects.Remove(key);
            //4. Loai bo khoi danh sach cac thuoc tinh hien tai
            if (identityMap.ContainsKey(key)) identityMap.Remove(key);
            //5. Them vao danh sach loai bo
            removedObjects.Add(key, obj);
            return true;
        }
        public bool RegisterDirty(ORMFramework.DomainObject obj)
        {
            if (obj == null) return false;
            object key = obj.getKey();
            // Khong phai la object moi duoc tao, chua duoc them lan nao, chua bi loai bo
            if (removedObjects.ContainsKey(key) || dirtyObjects.ContainsKey(key) || newObjects.ContainsKey(key))
            {
                return false;
            }
            dirtyObjects.Add(key, obj);
            return true;
        }

        public List<object> getDirtyList()
        {
            List<object> res = new List<object>();
            foreach (object obj in dirtyObjects.Values)
            {
                res.Add(obj);
            }
            return res;
        }

        public List<object> getNewList()
        {
            List<object> res = new List<object>();
            foreach (object obj in newObjects.Values)
            {
                res.Add(obj);
            }
            return res;
        }

        public List<object> getRemovedList()
        {
            List<object> res = new List<object>();
            foreach (object obj in removedObjects.Values)
            {
                res.Add(obj);
            }
            return res;
        }


        public void Print()
        {
            Console.WriteLine("Existing obj on memory: ");
            foreach (object str in identityMap.Keys)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("New obj waiting for insert: ");
            foreach (object str in newObjects.Keys)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("Edited obj waiting for update: ");
            foreach (object str in dirtyObjects.Keys)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("Removed obj waiting for delete: ");
            foreach (object str in removedObjects.Keys)
            {
                Console.WriteLine(str);
            }
        }

        public void ClearAll()
        {
            removedObjects.Clear();
            dirtyObjects.Clear();
            newObjects.Clear();
        }
        public UnitOfWork()
        {
        }
    }
}
