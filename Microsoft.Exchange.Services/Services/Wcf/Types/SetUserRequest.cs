using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A93 RID: 2707
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetUserRequest : BaseJsonRequest
	{
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x00106524 File Offset: 0x00104724
		// (set) Token: 0x06004C81 RID: 19585 RVA: 0x0010652C File Offset: 0x0010472C
		[DataMember(IsRequired = true)]
		public SetUserData User { get; set; }

		// Token: 0x06004C82 RID: 19586 RVA: 0x00106535 File Offset: 0x00104735
		public override string ToString()
		{
			return string.Format("SetUserRequest: {0}", this.User);
		}
	}
}
