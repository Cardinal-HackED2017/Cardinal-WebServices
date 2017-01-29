using System;
using cardinal_webservices.DataModels;

namespace cardinal_webservices.Models 
{
    public class UserModel 
    {
        public string userName {get; set;}

        public string email {get; set;}

        public string id {get; set;}

        public UserModel(User user)
        {
            userName = user.DisplayName;
            id = user.Id;
            email = user.Email;
        }
    }
}