using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF6 RID: 3062
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRoomsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F4D RID: 12109
		[DataMember(IsRequired = true, Order = 0)]
		public GetRoomsResponse Body;
	}
}
