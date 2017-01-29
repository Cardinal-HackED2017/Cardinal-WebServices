using System;
using cardinal_webservices.DataModels;

namespace cardinal_webservices.Models 
{
    public class MessageModel
    {
        public string content {get; set;}
        public DateTime creationTime {get; set;}
        public UserModel author {get; set;}
        public string messageId {get; set;}
        public string meetingId {get; set;}


        public MessageModel(Message message, User user)
        {
            content = message.content;
            creationTime = message.CreatedTime;
            author = new UserModel(user);
            messageId = message.MessageId;
            meetingId = message.MeetingId;
        }
        
    }
}