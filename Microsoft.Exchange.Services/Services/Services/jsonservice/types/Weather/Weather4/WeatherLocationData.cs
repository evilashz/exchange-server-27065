using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Services.jsonservice.types.Weather.Weather4
{
	// Token: 0x02000B56 RID: 2902
	[XmlRoot("searchLocations")]
	[Serializable]
	public class WeatherLocationData
	{
		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06005254 RID: 21076 RVA: 0x0010B364 File Offset: 0x00109564
		// (set) Token: 0x06005255 RID: 21077 RVA: 0x0010B36C File Offset: 0x0010956C
		[XmlElement("location")]
		public WeatherLocation[] Items { get; set; }
	}
}
