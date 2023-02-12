using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace RainbowMage.OverlayPlugin.EventSources
{
    public class XivSlothComboEventSource : EventSourceBase
    {
        private static readonly HttpClient client = new HttpClient();

        public BuiltinEventConfig Config { get; set; }

        public XivSlothComboEventSource(TinyIoCContainer container) : base(container)
        {
            RegisterEventHandler("XIVSlothCombo", DoAction);
        }

        private JToken DoAction(JObject jo)
        {
            string payload = jo["p"]?.Value<string>() ?? "";
            client.PostAsync("http://127.0.0.1:47775/", new StringContent(payload));
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
