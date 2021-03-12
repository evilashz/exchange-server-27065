using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B5A RID: 2906
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetWeatherForecastResponse : BaseJsonResponse
	{
		// Token: 0x06005268 RID: 21096 RVA: 0x0010B404 File Offset: 0x00109604
		internal GetWeatherForecastResponse()
		{
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06005269 RID: 21097 RVA: 0x0010B40C File Offset: 0x0010960C
		// (set) Token: 0x0600526A RID: 21098 RVA: 0x0010B414 File Offset: 0x00109614
		[DataMember(IsRequired = true)]
		public WeatherLocation[] WeatherLocations { get; set; }

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x0600526B RID: 21099 RVA: 0x0010B41D File Offset: 0x0010961D
		// (set) Token: 0x0600526C RID: 21100 RVA: 0x0010B425 File Offset: 0x00109625
		[DataMember(IsRequired = false)]
		public string ErrorMessage { get; set; }

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x0600526D RID: 21101 RVA: 0x0010B42E File Offset: 0x0010962E
		// (set) Token: 0x0600526E RID: 21102 RVA: 0x0010B436 File Offset: 0x00109636
		[DataMember(IsRequired = true)]
		public int PollingWindowInMinutes { get; set; }

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x0600526F RID: 21103 RVA: 0x0010B43F File Offset: 0x0010963F
		// (set) Token: 0x06005270 RID: 21104 RVA: 0x0010B447 File Offset: 0x00109647
		[DataMember(IsRequired = true)]
		public TemperatureUnit TemperatureUnit { get; set; }
	}
}
