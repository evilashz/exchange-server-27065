using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000B1 RID: 177
	[DataContract]
	internal sealed class OlcRuleCategoryRestriction : OlcRuleRestrictionBase
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0000B7F7 File Offset: 0x000099F7
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0000B7FF File Offset: 0x000099FF
		[DataMember]
		public int CategoryInt { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0000B808 File Offset: 0x00009A08
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0000B810 File Offset: 0x00009A10
		public OlcMessageCategory Category
		{
			get
			{
				return (OlcMessageCategory)this.CategoryInt;
			}
			set
			{
				this.CategoryInt = (int)value;
			}
		}
	}
}
