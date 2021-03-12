using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000B8 RID: 184
	[KnownType(typeof(OlcRuleActionRemoveCategory))]
	[DataContract]
	[KnownType(typeof(OlcRuleActionAssignCategory))]
	internal abstract class OlcRuleCategoryActionBase : OlcRuleActionBase
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0000B8EA File Offset: 0x00009AEA
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0000B8F2 File Offset: 0x00009AF2
		[DataMember]
		public ushort CategoryId { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0000B8FB File Offset: 0x00009AFB
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0000B903 File Offset: 0x00009B03
		public OlcMessageCategory SystemCategory
		{
			get
			{
				return (OlcMessageCategory)this.CategoryId;
			}
			set
			{
				this.CategoryId = (ushort)value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0000B90D File Offset: 0x00009B0D
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0000B915 File Offset: 0x00009B15
		[DataMember]
		public bool IsUserCategory { get; set; }
	}
}
