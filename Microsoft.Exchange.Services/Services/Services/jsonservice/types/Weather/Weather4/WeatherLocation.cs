using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Services.jsonservice.types.Weather.Weather4
{
	// Token: 0x02000B55 RID: 2901
	[Serializable]
	public class WeatherLocation : IWeatherLocation
	{
		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06005239 RID: 21049 RVA: 0x0010B27F File Offset: 0x0010947F
		// (set) Token: 0x0600523A RID: 21050 RVA: 0x0010B287 File Offset: 0x00109487
		[XmlAttribute("errormessage")]
		public string ErrorMessage { get; set; }

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x0010B290 File Offset: 0x00109490
		// (set) Token: 0x0600523C RID: 21052 RVA: 0x0010B298 File Offset: 0x00109498
		[XmlElement("current")]
		public WeatherConditions Conditions { get; set; }

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x0600523D RID: 21053 RVA: 0x0010B2A1 File Offset: 0x001094A1
		// (set) Token: 0x0600523E RID: 21054 RVA: 0x0010B2A9 File Offset: 0x001094A9
		[XmlAttribute("name")]
		public string Name { get; set; }

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x0600523F RID: 21055 RVA: 0x0010B2B2 File Offset: 0x001094B2
		// (set) Token: 0x06005240 RID: 21056 RVA: 0x0010B2BA File Offset: 0x001094BA
		[XmlAttribute("fullName")]
		public string FullName { get; set; }

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06005241 RID: 21057 RVA: 0x0010B2C3 File Offset: 0x001094C3
		// (set) Token: 0x06005242 RID: 21058 RVA: 0x0010B2CB File Offset: 0x001094CB
		[XmlAttribute("url")]
		public string Url { get; set; }

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06005243 RID: 21059 RVA: 0x0010B2D4 File Offset: 0x001094D4
		// (set) Token: 0x06005244 RID: 21060 RVA: 0x0010B2DC File Offset: 0x001094DC
		[XmlAttribute("degreetype")]
		public string DegreeUnit { get; set; }

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06005245 RID: 21061 RVA: 0x0010B2E5 File Offset: 0x001094E5
		// (set) Token: 0x06005246 RID: 21062 RVA: 0x0010B2ED File Offset: 0x001094ED
		[XmlAttribute("lat")]
		public double Latitude { get; set; }

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06005247 RID: 21063 RVA: 0x0010B2F6 File Offset: 0x001094F6
		// (set) Token: 0x06005248 RID: 21064 RVA: 0x0010B2FE File Offset: 0x001094FE
		[XmlAttribute("long")]
		public double Longitude { get; set; }

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06005249 RID: 21065 RVA: 0x0010B307 File Offset: 0x00109507
		// (set) Token: 0x0600524A RID: 21066 RVA: 0x0010B30F File Offset: 0x0010950F
		[XmlAttribute("entityId")]
		public long EntityId { get; set; }

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x0600524B RID: 21067 RVA: 0x0010B318 File Offset: 0x00109518
		// (set) Token: 0x0600524C RID: 21068 RVA: 0x0010B320 File Offset: 0x00109520
		[XmlElement("forecast")]
		public WeatherForecast[] Forecast { get; set; }

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x0600524D RID: 21069 RVA: 0x0010B329 File Offset: 0x00109529
		// (set) Token: 0x0600524E RID: 21070 RVA: 0x0010B331 File Offset: 0x00109531
		[XmlAttribute("attribution")]
		public string Attribution { get; set; }

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x0600524F RID: 21071 RVA: 0x0010B33A File Offset: 0x0010953A
		// (set) Token: 0x06005250 RID: 21072 RVA: 0x0010B342 File Offset: 0x00109542
		[XmlAttribute("attribution2")]
		public string ShortAttribution { get; set; }

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06005251 RID: 21073 RVA: 0x0010B34B File Offset: 0x0010954B
		// (set) Token: 0x06005252 RID: 21074 RVA: 0x0010B353 File Offset: 0x00109553
		[XmlAttribute("provider")]
		public string Provider { get; set; }
	}
}
