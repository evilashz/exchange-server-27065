using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B62 RID: 2914
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class WeatherMultidayForecast
	{
		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x0010B591 File Offset: 0x00109791
		// (set) Token: 0x0600529C RID: 21148 RVA: 0x0010B599 File Offset: 0x00109799
		[DataMember]
		public WeatherProviderAttribution Attribution { get; set; }

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x0600529D RID: 21149 RVA: 0x0010B5A2 File Offset: 0x001097A2
		// (set) Token: 0x0600529E RID: 21150 RVA: 0x0010B5AA File Offset: 0x001097AA
		[DataMember]
		public WeatherDailyConditions[] DailyForecasts { get; set; }
	}
}
