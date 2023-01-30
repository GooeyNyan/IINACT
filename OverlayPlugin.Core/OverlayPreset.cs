using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace RainbowMage.OverlayPlugin {
    internal class OverlayPreset : IOverlayPreset {
        public string Name { get; set; }
        public string Url { get; set; }
        [JsonProperty("http_proxy")]
        public string HttpUrl { get; set; }
        public bool Modern { get; set; }

        public override string ToString() {
            return Name;
        }
        public string Type { get; set; }
        [JsonIgnore]
        public int[] Size { get; set; }
        public bool Locked { get; set; }
        public List<string> Supports { get; set; }
    }
}
