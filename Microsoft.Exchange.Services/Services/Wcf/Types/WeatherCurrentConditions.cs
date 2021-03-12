using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B5E RID: 2910
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class WeatherCurrentConditions
	{
		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06005275 RID: 21109 RVA: 0x0010B450 File Offset: 0x00109650
		// (set) Token: 0x06005276 RID: 21110 RVA: 0x0010B458 File Offset: 0x00109658
		[DataMember]
		public WeatherProviderAttribution Attribution { get; set; }

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06005277 RID: 21111 RVA: 0x0010B461 File Offset: 0x00109661
		// (set) Token: 0x06005278 RID: 21112 RVA: 0x0010B469 File Offset: 0x00109669
		[DataMember]
		public int Temperature { get; set; }

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06005279 RID: 21113 RVA: 0x0010B472 File Offset: 0x00109672
		// (set) Token: 0x0600527A RID: 21114 RVA: 0x0010B47A File Offset: 0x0010967A
		[DataMember]
		public int SkyCode { get; set; }

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x0600527B RID: 21115 RVA: 0x0010B483 File Offset: 0x00109683
		// (set) Token: 0x0600527C RID: 21116 RVA: 0x0010B48B File Offset: 0x0010968B
		[DataMember]
		public string SkyText { get; set; }
	}
}
