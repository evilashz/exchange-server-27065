using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A98 RID: 2712
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class IdentityRequest : BaseJsonRequest
	{
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06004C9B RID: 19611 RVA: 0x0010665F File Offset: 0x0010485F
		// (set) Token: 0x06004C9C RID: 19612 RVA: 0x00106667 File Offset: 0x00104867
		[DataMember(IsRequired = true)]
		public Identity Identity { get; set; }

		// Token: 0x06004C9D RID: 19613 RVA: 0x00106670 File Offset: 0x00104870
		public override string ToString()
		{
			return string.Format("IdentityRequest: {0}", this.Identity);
		}
	}
}
