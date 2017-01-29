using System;
using cardinal_webservices.DataModels;

namespace cardinal_webservices.Models 
{
    public class MessageModel
    {
        public string content {get; set;}
        public DateTime creationTime {get; set;}
        public string userName {get; set;}
        public string messageId {get; set;}
        public string meetingId {get; set;}


        public MessageModel(Message message, User user)
        {
            content = message.content;
            creationTime = message.CreatedTime;
            userName = user.DisplayName;
            messageId = message.MessageId;
            meetingId = message.MeetingId;
        }
        
    }
}