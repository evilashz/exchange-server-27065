using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B5F RID: 2911
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class WeatherDailyConditions
	{
		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x0010B49C File Offset: 0x0010969C
		// (set) Token: 0x0600527F RID: 21119 RVA: 0x0010B4A4 File Offset: 0x001096A4
		[DataMember]
		public int High { get; set; }

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06005280 RID: 21120 RVA: 0x0010B4AD File Offset: 0x001096AD
		// (set) Token: 0x06005281 RID: 21121 RVA: 0x0010B4B5 File Offset: 0x001096B5
		[DataMember]
		public int Low { get; set; }

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06005282 RID: 21122 RVA: 0x0010B4BE File Offset: 0x001096BE
		// (set) Token: 0x06005283 RID: 21123 RVA: 0x0010B4C6 File Offset: 0x001096C6
		[DataMember]
		public int SkyCode { get; set; }

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06005284 RID: 21124 RVA: 0x0010B4CF File Offset: 0x001096CF
		// (set) Token: 0x06005285 RID: 21125 RVA: 0x0010B4D7 File Offset: 0x001096D7
		[DataMember]
		public string SkyText { get; set; }

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06005286 RID: 21126 RVA: 0x0010B4E0 File Offset: 0x001096E0
		// (set) Token: 0x06005287 RID: 21127 RVA: 0x0010B4E8 File Offset: 0x001096E8
		[DataMember]
		public string PrecipitationChance { get; set; }
	}
}
