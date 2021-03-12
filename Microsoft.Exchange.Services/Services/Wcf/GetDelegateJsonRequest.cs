using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BCB RID: 3019
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetDelegateJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F22 RID: 12066
		[DataMember(IsRequired = true, Order = 0)]
		public GetDelegateRequest Body;
	}
}
