using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A89 RID: 2697
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveAppDataRequest : BaseJsonRequest
	{
		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06004C5A RID: 19546 RVA: 0x00106368 File Offset: 0x00104568
		// (set) Token: 0x06004C5B RID: 19547 RVA: 0x00106370 File Offset: 0x00104570
		[DataMember(IsRequired = true)]
		public Identity Identity { get; set; }

		// Token: 0x06004C5C RID: 19548 RVA: 0x00106379 File Offset: 0x00104579
		public override string ToString()
		{
			return string.Format(base.ToString() + ", Identity = {0}", this.Identity);
		}
	}
}
