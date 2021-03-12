using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B0A RID: 2826
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RegionData : OptionsPropertyChangeTracker
	{
		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06005026 RID: 20518 RVA: 0x001093A9 File Offset: 0x001075A9
		// (set) Token: 0x06005027 RID: 20519 RVA: 0x001093B1 File Offset: 0x001075B1
		[DataMember]
		public string RegionId { get; set; }

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06005028 RID: 20520 RVA: 0x001093BA File Offset: 0x001075BA
		// (set) Token: 0x06005029 RID: 20521 RVA: 0x001093C2 File Offset: 0x001075C2
		[DataMember]
		public string RegionName { get; set; }

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x001093CB File Offset: 0x001075CB
		// (set) Token: 0x0600502B RID: 20523 RVA: 0x001093D3 File Offset: 0x001075D3
		[DataMember]
		public string CountryCode { get; set; }

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x001093DC File Offset: 0x001075DC
		// (set) Token: 0x0600502D RID: 20525 RVA: 0x001093E4 File Offset: 0x001075E4
		[DataMember]
		public string[] CarrierIds { get; set; }
	}
}
