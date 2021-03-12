using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B60 RID: 2912
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class WeatherLocation
	{
		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x06005289 RID: 21129 RVA: 0x0010B4F9 File Offset: 0x001096F9
		// (set) Token: 0x0600528A RID: 21130 RVA: 0x0010B501 File Offset: 0x00109701
		[DataMember]
		public WeatherLocationId Id { get; set; }

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x0600528B RID: 21131 RVA: 0x0010B50A File Offset: 0x0010970A
		// (set) Token: 0x0600528C RID: 21132 RVA: 0x0010B512 File Offset: 0x00109712
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x0600528D RID: 21133 RVA: 0x0010B51B File Offset: 0x0010971B
		// (set) Token: 0x0600528E RID: 21134 RVA: 0x0010B523 File Offset: 0x00109723
		[DataMember]
		public WeatherCurrentConditions Conditions { get; set; }

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x0600528F RID: 21135 RVA: 0x0010B52C File Offset: 0x0010972C
		// (set) Token: 0x06005290 RID: 21136 RVA: 0x0010B534 File Offset: 0x00109734
		[DataMember]
		public WeatherMultidayForecast MultidayForecast { get; set; }
	}
}
