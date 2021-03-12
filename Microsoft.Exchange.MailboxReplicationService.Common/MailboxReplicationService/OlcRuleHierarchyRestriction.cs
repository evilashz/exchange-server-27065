using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000AA RID: 170
	[KnownType(typeof(OlcRuleAndRestriction))]
	[KnownType(typeof(OlcRuleOrRestriction))]
	[DataContract]
	internal abstract class OlcRuleHierarchyRestriction : OlcRuleRestrictionBase
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000B737 File Offset: 0x00009937
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0000B73F File Offset: 0x0000993F
		[DataMember]
		public OlcRuleRestrictionBase[] Conditions { get; set; }
	}
}
