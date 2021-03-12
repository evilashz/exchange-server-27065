using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Services.jsonservice.types.Weather.Weather4
{
	// Token: 0x02000B54 RID: 2900
	[XmlRoot("weatherdata")]
	[Serializable]
	public class WeatherData
	{
		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06005236 RID: 21046 RVA: 0x0010B266 File Offset: 0x00109466
		// (set) Token: 0x06005237 RID: 21047 RVA: 0x0010B26E File Offset: 0x0010946E
		[XmlElement("weather")]
		public WeatherLocation[] Items { get; set; }

		// Token: 0x04002DD2 RID: 11730
		public const string RootElementName = "weatherdata";
	}
}
