using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B61 RID: 2913
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class WeatherLocationId
	{
		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06005292 RID: 21138 RVA: 0x0010B545 File Offset: 0x00109745
		// (set) Token: 0x06005293 RID: 21139 RVA: 0x0010B54D File Offset: 0x0010974D
		[DataMember]
		public double Latitude { get; set; }

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06005294 RID: 21140 RVA: 0x0010B556 File Offset: 0x00109756
		// (set) Token: 0x06005295 RID: 21141 RVA: 0x0010B55E File Offset: 0x0010975E
		[DataMember]
		public double Longitude { get; set; }

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x0010B567 File Offset: 0x00109767
		// (set) Token: 0x06005297 RID: 21143 RVA: 0x0010B56F File Offset: 0x0010976F
		[DataMember]
		public string Name { get; set; }

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06005298 RID: 21144 RVA: 0x0010B578 File Offset: 0x00109778
		// (set) Token: 0x06005299 RID: 21145 RVA: 0x0010B580 File Offset: 0x00109780
		[DataMember]
		public string EntityId { get; set; }
	}
}
