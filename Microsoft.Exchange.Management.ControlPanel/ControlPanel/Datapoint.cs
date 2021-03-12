using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200027A RID: 634
	[DataContract]
	public class Datapoint
	{
		// Token: 0x060029C1 RID: 10689 RVA: 0x00083524 File Offset: 0x00081724
		public Datapoint(string name, string source, string requestId, string timestamp)
		{
			this.Name = name;
			this.Src = source;
			this.ReqId = requestId;
			this.Time = timestamp;
		}

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x00083549 File Offset: 0x00081749
		// (set) Token: 0x060029C3 RID: 10691 RVA: 0x00083551 File Offset: 0x00081751
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x0008355A File Offset: 0x0008175A
		// (set) Token: 0x060029C5 RID: 10693 RVA: 0x00083562 File Offset: 0x00081762
		[DataMember]
		public string Src { get; set; }

		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x0008356B File Offset: 0x0008176B
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x00083573 File Offset: 0x00081773
		[DataMember]
		public string ReqId { get; set; }

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x0008357C File Offset: 0x0008177C
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x00083584 File Offset: 0x00081784
		[DataMember]
		public string Time { get; set; }
	}
}
