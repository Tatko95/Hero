using HeroGame.ModelGame;
using System;
using System.ServiceModel;

namespace HeroGame.ILibrary
{
    [ServiceContract]
    public interface IAvtorisation
    {
        [OperationContract]
        bool Registration(string name, string pass);
        [OperationContract]
        User Avtorisation(string name, string pass);
    }
}
