using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A8 RID: 168
	[DataContract]
	internal sealed class OlcInboxRule
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0000B6E3 File Offset: 0x000098E3
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0000B6EB File Offset: 0x000098EB
		[DataMember]
		public bool Enabled { get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0000B6F4 File Offset: 0x000098F4
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0000B6FC File Offset: 0x000098FC
		[DataMember]
		public uint ExecutionSequence { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0000B705 File Offset: 0x00009905
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0000B70D File Offset: 0x0000990D
		[DataMember]
		public OlcRuleRestrictionBase Condition { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0000B716 File Offset: 0x00009916
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x0000B71E File Offset: 0x0000991E
		[DataMember]
		public OlcRuleActionBase[] Actions { get; set; }
	}
}
