using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AA9 RID: 2729
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetInboxRuleResponse : OptionsResponseBase
	{
		// Token: 0x06004CF6 RID: 19702 RVA: 0x00106A44 File Offset: 0x00104C44
		public GetInboxRuleResponse()
		{
			this.InboxRuleCollection = new InboxRuleCollection();
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x00106A57 File Offset: 0x00104C57
		// (set) Token: 0x06004CF8 RID: 19704 RVA: 0x00106A5F File Offset: 0x00104C5F
		[DataMember(IsRequired = true)]
		public InboxRuleCollection InboxRuleCollection { get; set; }

		// Token: 0x06004CF9 RID: 19705 RVA: 0x00106A68 File Offset: 0x00104C68
		public override string ToString()
		{
			return string.Format("GetInboxRuleResponse: {0}", this.InboxRuleCollection);
		}
	}
}
