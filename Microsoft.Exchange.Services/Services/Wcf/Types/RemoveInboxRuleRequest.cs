using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AD0 RID: 2768
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveInboxRuleRequest : UpdateInboxRuleRequestBase
	{
		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06004ED0 RID: 20176 RVA: 0x00108240 File Offset: 0x00106440
		// (set) Token: 0x06004ED1 RID: 20177 RVA: 0x00108248 File Offset: 0x00106448
		[DataMember(IsRequired = true)]
		public Identity Identity { get; set; }

		// Token: 0x06004ED2 RID: 20178 RVA: 0x00108251 File Offset: 0x00106451
		public override string ToString()
		{
			return string.Format(base.ToString() + ", Identity = {0}", this.Identity);
		}
	}
}
