using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A9D RID: 2717
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DisableInboxRuleRequest : UpdateInboxRuleRequestBase
	{
		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06004CCE RID: 19662 RVA: 0x00106878 File Offset: 0x00104A78
		// (set) Token: 0x06004CCF RID: 19663 RVA: 0x00106880 File Offset: 0x00104A80
		[DataMember(IsRequired = true)]
		public Identity Identity { get; set; }

		// Token: 0x06004CD0 RID: 19664 RVA: 0x00106889 File Offset: 0x00104A89
		public override string ToString()
		{
			return string.Format(base.ToString() + ", Identity = {0}", this.Identity);
		}
	}
}
