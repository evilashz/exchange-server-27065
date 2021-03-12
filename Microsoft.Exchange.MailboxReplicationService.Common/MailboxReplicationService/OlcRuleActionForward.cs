using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000B6 RID: 182
	[DataContract]
	internal sealed class OlcRuleActionForward : OlcRuleActionBase
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0000B8C9 File Offset: 0x00009AC9
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0000B8D1 File Offset: 0x00009AD1
		[DataMember]
		public string ForwardingEmailAddress { get; set; }
	}
}
