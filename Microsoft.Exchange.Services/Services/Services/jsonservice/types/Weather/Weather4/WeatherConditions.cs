using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Services.jsonservice.types.Weather.Weather4
{
	// Token: 0x02000B52 RID: 2898
	[Serializable]
	public class WeatherConditions : IWeatherConditions
	{
		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06005222 RID: 21026 RVA: 0x0010B1BD File Offset: 0x001093BD
		// (set) Token: 0x06005223 RID: 21027 RVA: 0x0010B1C5 File Offset: 0x001093C5
		[XmlAttribute("temperature")]
		public int Temperature { get; set; }

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06005224 RID: 21028 RVA: 0x0010B1CE File Offset: 0x001093CE
		// (set) Token: 0x06005225 RID: 21029 RVA: 0x0010B1D6 File Offset: 0x001093D6
		[XmlAttribute("skycode")]
		public int SkyCode { get; set; }

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06005226 RID: 21030 RVA: 0x0010B1DF File Offset: 0x001093DF
		// (set) Token: 0x06005227 RID: 21031 RVA: 0x0010B1E7 File Offset: 0x001093E7
		[XmlAttribute("skytext")]
		public string SkyText { get; set; }
	}
}
