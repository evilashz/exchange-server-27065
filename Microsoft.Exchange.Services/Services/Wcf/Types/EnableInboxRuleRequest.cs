using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A9F RID: 2719
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EnableInboxRuleRequest : UpdateInboxRuleRequestBase
	{
		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06004CD3 RID: 19667 RVA: 0x001068B6 File Offset: 0x00104AB6
		// (set) Token: 0x06004CD4 RID: 19668 RVA: 0x001068BE File Offset: 0x00104ABE
		[DataMember(IsRequired = true)]
		public Identity Identity { get; set; }

		// Token: 0x06004CD5 RID: 19669 RVA: 0x001068C7 File Offset: 0x00104AC7
		public override string ToString()
		{
			return string.Format(base.ToString() + ", Identity = {0}", this.Identity);
		}
	}
}
