﻿using System;
using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Outputs
{
    public class SlackOutput : Output
    {
        private SlackClient _slackClient;
        public Property<string> WebHookUrl { get; set; }
        public Property<string> Username { get; set; }
        public Property<string> Message { get; set; }
        public Property<string> Emoji { get; set; }

        public override void Do(ExpandoObject msg)
        {
            if (_slackClient == null)
                _slackClient = new SlackClient(WebHookUrl);
            
            var payload = new Payload()
            {
                Text = Message.Get(msg),
                Username = Username.Get(msg),
                Emoji = Emoji.Get(msg)
            };
            _slackClient.PostMessage(payload);
            Logger.Debug(JsonConvert.SerializeObject(payload));
        }
    }
}