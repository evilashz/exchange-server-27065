using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A7C RID: 2684
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EnableAppDataRequest : BaseJsonRequest
	{
		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06004BFF RID: 19455 RVA: 0x00105F86 File Offset: 0x00104186
		// (set) Token: 0x06004C00 RID: 19456 RVA: 0x00105F8E File Offset: 0x0010418E
		[DataMember(IsRequired = true)]
		public Identity Identity { get; set; }

		// Token: 0x06004C01 RID: 19457 RVA: 0x00105F97 File Offset: 0x00104197
		public override string ToString()
		{
			return string.Format(base.ToString() + ", Identity = {0}", this.Identity);
		}
	}
}
