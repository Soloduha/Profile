using Profile.BLL.DTO;
using ProfileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfileApp.AdditionalClasses
{
    public static class Mapper
    {
        public static ProfileModel FromUserToProfile(UserDTO user)
        {
            ProfileModel registerUser = new ProfileModel
            {
                Address = user.Address,
                Email = user.Email,
                Login = user.UserName,
                Name = user.Name,
                Photo = user.Photo??String.Empty,
                Surname = user.Surname
            };

            return registerUser;
        }
    }
}