using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x0200083C RID: 2108
	[DataContract]
	[Serializable]
	public class AggregatedQueueNormalDetails
	{
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x0006539E File Offset: 0x0006359E
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x000653A6 File Offset: 0x000635A6
		[DataMember(IsRequired = true)]
		public string QueueIdentity { get; set; }

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000653AF File Offset: 0x000635AF
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x000653B7 File Offset: 0x000635B7
		[DataMember(IsRequired = true)]
		public string ServerIdentity { get; set; }

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x000653C0 File Offset: 0x000635C0
		// (set) Token: 0x06002CEE RID: 11502 RVA: 0x000653C8 File Offset: 0x000635C8
		[DataMember(IsRequired = true)]
		public int MessageCount { get; set; }
	}
}
