using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C20 RID: 3104
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPasswordExpirationDateJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F77 RID: 12151
		[DataMember(IsRequired = true, Order = 0)]
		public GetPasswordExpirationDateResponse Body;
	}
}
