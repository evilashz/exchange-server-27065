using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF4 RID: 3060
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRoomListsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F4B RID: 12107
		[DataMember(IsRequired = true, Order = 0)]
		public GetRoomListsResponse Body;
	}
}
