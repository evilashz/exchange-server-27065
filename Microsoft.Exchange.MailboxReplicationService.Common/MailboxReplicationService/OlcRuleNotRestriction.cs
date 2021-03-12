using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000AD RID: 173
	[DataContract]
	internal sealed class OlcRuleNotRestriction : OlcRuleRestrictionBase
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0000B760 File Offset: 0x00009960
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0000B768 File Offset: 0x00009968
		[DataMember]
		public OlcRuleRestrictionBase Condition { get; set; }
	}
}
