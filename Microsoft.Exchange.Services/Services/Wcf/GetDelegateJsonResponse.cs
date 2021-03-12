using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BCC RID: 3020
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetDelegateJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F23 RID: 12067
		[DataMember(IsRequired = true, Order = 0)]
		public GetDelegateResponseMessage Body;
	}
}
