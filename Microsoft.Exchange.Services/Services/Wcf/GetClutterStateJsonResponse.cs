using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C81 RID: 3201
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetClutterStateJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FD4 RID: 12244
		[DataMember(IsRequired = true, Order = 0)]
		public GetClutterStateResponse Body;
	}
}
