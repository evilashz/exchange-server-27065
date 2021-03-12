using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000B3 RID: 179
	[KnownType(typeof(OlcRuleActionAssignCategory))]
	[KnownType(typeof(OlcRuleActionForward))]
	[KnownType(typeof(OlcRuleActionRemoveCategory))]
	[KnownType(typeof(OlcRuleActionMobileAlert))]
	[KnownType(typeof(OlcRuleCategoryActionBase))]
	[DataContract]
	[KnownType(typeof(OlcRuleActionMarkAsRead))]
	[KnownType(typeof(OlcRuleActionMoveToFolder))]
	internal abstract class OlcRuleActionBase
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0000B84B File Offset: 0x00009A4B
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0000B853 File Offset: 0x00009A53
		[DataMember]
		public int TypeInt { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0000B85C File Offset: 0x00009A5C
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0000B864 File Offset: 0x00009A64
		public OlcActionType Type
		{
			get
			{
				return (OlcActionType)this.TypeInt;
			}
			set
			{
				this.TypeInt = (int)value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0000B86D File Offset: 0x00009A6D
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0000B875 File Offset: 0x00009A75
		[DataMember]
		public uint DeferredThresholdCountOrDays { get; set; }
	}
}
