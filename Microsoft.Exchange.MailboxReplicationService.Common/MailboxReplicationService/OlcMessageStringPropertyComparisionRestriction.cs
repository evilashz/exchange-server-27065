using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000AF RID: 175
	[DataContract]
	internal sealed class OlcMessageStringPropertyComparisionRestriction : OlcRuleRestrictionBase
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0000B781 File Offset: 0x00009981
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0000B789 File Offset: 0x00009989
		[DataMember]
		public uint? LCID { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0000B792 File Offset: 0x00009992
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0000B79A File Offset: 0x0000999A
		[DataMember]
		public int PropertyInt { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0000B7A3 File Offset: 0x000099A3
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0000B7AB File Offset: 0x000099AB
		public OlcMessageProperty Property
		{
			get
			{
				return (OlcMessageProperty)this.PropertyInt;
			}
			set
			{
				this.PropertyInt = (int)value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0000B7B4 File Offset: 0x000099B4
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0000B7BC File Offset: 0x000099BC
		[DataMember]
		public int ConditionInt { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000B7C5 File Offset: 0x000099C5
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0000B7CD File Offset: 0x000099CD
		public OlcStringComparison Condition
		{
			get
			{
				return (OlcStringComparison)this.ConditionInt;
			}
			set
			{
				this.ConditionInt = (int)value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0000B7D6 File Offset: 0x000099D6
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0000B7DE File Offset: 0x000099DE
		[DataMember]
		public string Value { get; set; }
	}
}
