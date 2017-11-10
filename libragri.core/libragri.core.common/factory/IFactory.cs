using System;

namespace libragri.core.common
{
    public interface IFactory
    {
        TIObject Resolve<TIObject>(params object[] obj);

        void Register<TIObject,TObject>();
        void Register<TIObject>(TIObject obj);
    }
}