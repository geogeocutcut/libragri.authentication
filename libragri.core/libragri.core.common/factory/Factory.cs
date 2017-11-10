using System;
using System.Collections.Generic;

namespace libragri.core.common
{
    public class Factory:IFactory
    {
        Dictionary<Type,object> Configuration = new Dictionary<Type,object>();
        public TIObject Resolve<TIObject>(params object[] obj)
        {
            if(Configuration[typeof(TIObject)] is Type)
            {
                return (TIObject)Activator.CreateInstance((Type)Configuration[typeof(TIObject)],obj);
            }
            else
            {
                return (TIObject)Configuration[typeof(TIObject)];
            }

        }

        public void Register<TIObject>(TIObject classDef)
        {
            Configuration[typeof(TIObject)]=classDef;
        }


        public void Register<TIObject, TObject>()
        {
            Configuration[typeof(TIObject)]=typeof(TObject);
        }

    }
}