﻿using libragri.authentication.model;
using libragri.authentication.repository.interfaces;
using libragri.core.cqrs;
using libragri.core.repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace libragri.authentification.cqrs.command.handler
{
    public class LoginCommandHandler : CommandHandler<UserModel, LoginCommand>
    {

        public IUserRespository repository;

        public LoginCommandHandler(IUserRespository repo)
        {
            repository = repo;
        }
        public override UserModel handle(LoginCommand commandtodo)
        {
            UserModel user= repository.GetByLogin(commandtodo.Login);
            if(user.PwdSHA1==commandtodo.PwdSHA1)
            {
                return user;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

    }
}
