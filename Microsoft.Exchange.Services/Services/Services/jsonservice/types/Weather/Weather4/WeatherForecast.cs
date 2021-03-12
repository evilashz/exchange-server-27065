using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Services.jsonservice.types.Weather.Weather4
{
	// Token: 0x02000B53 RID: 2899
	[Serializable]
	public class WeatherForecast : IWeatherForecast
	{
		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06005229 RID: 21033 RVA: 0x0010B1F8 File Offset: 0x001093F8
		// (set) Token: 0x0600522A RID: 21034 RVA: 0x0010B200 File Offset: 0x00109400
		[XmlAttribute("skytextday")]
		public string SkyTextDay { get; set; }

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x0600522B RID: 21035 RVA: 0x0010B209 File Offset: 0x00109409
		// (set) Token: 0x0600522C RID: 21036 RVA: 0x0010B211 File Offset: 0x00109411
		[XmlAttribute("precip")]
		public int PrecipitationCertainty { get; set; }

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x0600522D RID: 21037 RVA: 0x0010B21A File Offset: 0x0010941A
		// (set) Token: 0x0600522E RID: 21038 RVA: 0x0010B222 File Offset: 0x00109422
		[XmlAttribute("high")]
		public int High { get; set; }

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x0600522F RID: 21039 RVA: 0x0010B22B File Offset: 0x0010942B
		// (set) Token: 0x06005230 RID: 21040 RVA: 0x0010B233 File Offset: 0x00109433
		[XmlAttribute("low")]
		public int Low { get; set; }

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06005231 RID: 21041 RVA: 0x0010B23C File Offset: 0x0010943C
		// (set) Token: 0x06005232 RID: 21042 RVA: 0x0010B244 File Offset: 0x00109444
		[XmlAttribute("skycodeday")]
		public int SkyCodeDay { get; set; }

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06005233 RID: 21043 RVA: 0x0010B24D File Offset: 0x0010944D
		// (set) Token: 0x06005234 RID: 21044 RVA: 0x0010B255 File Offset: 0x00109455
		[XmlAttribute("skycodenight")]
		public int SkyCodeNight { get; set; }
	}
}
