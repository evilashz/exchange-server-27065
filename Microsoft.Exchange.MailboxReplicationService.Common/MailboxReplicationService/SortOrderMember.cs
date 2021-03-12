using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000080 RID: 128
	[DataContract]
	internal sealed class SortOrderMember
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000A679 File Offset: 0x00008879
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000A681 File Offset: 0x00008881
		[DataMember(IsRequired = true)]
		public int PropTag { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000A68A File Offset: 0x0000888A
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000A692 File Offset: 0x00008892
		[DataMember(EmitDefaultValue = false)]
		public int Flags { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000A69B File Offset: 0x0000889B
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000A6A3 File Offset: 0x000088A3
		[DataMember(EmitDefaultValue = false)]
		public bool IsCategory { get; set; }

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000A6AC File Offset: 0x000088AC
		public override string ToString()
		{
			return string.Format("[{0}{1}:{2}]", this.IsCategory ? "Cat:" : string.Empty, TraceUtils.DumpPropTag((PropTag)this.PropTag), ((SortFlags)this.Flags).ToString());
		}
	}
}
