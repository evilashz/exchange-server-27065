using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A7A RID: 2682
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DisableAppDataRequest : BaseJsonRequest
	{
		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06004BFA RID: 19450 RVA: 0x00105F48 File Offset: 0x00104148
		// (set) Token: 0x06004BFB RID: 19451 RVA: 0x00105F50 File Offset: 0x00104150
		[DataMember(IsRequired = true)]
		public Identity Identity { get; set; }

		// Token: 0x06004BFC RID: 19452 RVA: 0x00105F59 File Offset: 0x00104159
		public override string ToString()
		{
			return string.Format(base.ToString() + ", Identity = {0}", this.Identity);
		}
	}
}
