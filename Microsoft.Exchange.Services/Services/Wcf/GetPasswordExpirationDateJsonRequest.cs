using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C1F RID: 3103
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPasswordExpirationDateJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F76 RID: 12150
		[DataMember(IsRequired = true, Order = 0)]
		public GetPasswordExpirationDateRequest Body;
	}
}
