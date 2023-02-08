using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RainbowMage.OverlayPlugin.EventSources
{
    public class PostNamazuEventSource : EventSourceBase
    {
        private static readonly HttpClient client = new HttpClient();

        public BuiltinEventConfig Config { get; set; }

        public PostNamazuEventSource(TinyIoCContainer container) : base(container)
        {
            RegisterEventHandler("PostNamazu", DoAction);
        }

        private JToken DoAction(JObject jo)
        {
            string command = jo["c"]?.Value<string>() ?? "null";
            string payload = jo["p"]?.Value<string>() ?? "";
            client.PostAsync("http://127.0.0.1:1002/" + command, new StringContent(payload));
            return null;
        }

        public override void Start()
        {
        }

        public override void LoadConfig(IPluginConfig cfg)
        {
            Config = container.Resolve<BuiltinEventConfig>();
        }

        public override void SaveConfig(IPluginConfig config)
        {
        }

        protected override void Update()
        {
        }
    }
}
