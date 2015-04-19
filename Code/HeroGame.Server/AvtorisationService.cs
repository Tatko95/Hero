using HeroGame.ILibrary;
using HeroGame.ModelGame;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace HeroGame.Server
{
    public class AvtorisationService : IAvtorisation
    {
        public bool Registration(string name, string pass)
        {
            using (DataGame data = new DataGame())
            {
                return data.Registration(name, pass);
            }
        }

        public User Avtorisation(string name, string pass)
        {
            using (DataGame data = new DataGame())
            {
                User returnUser = data.Login(name);
                if (returnUser == null)
                    return null;
                if (CachData.UsersOnline.ContainsKey(returnUser))
                    return null;
                return returnUser;
            }
        }
    }
}
