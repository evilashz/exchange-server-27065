using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C80 RID: 3200
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetClutterStateJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FD3 RID: 12243
		[DataMember(IsRequired = true, Order = 0)]
		public GetClutterStateRequest Body;
	}
}
