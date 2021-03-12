using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF3 RID: 3059
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRoomListsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F4A RID: 12106
		[DataMember(IsRequired = true, Order = 0)]
		public GetRoomListsRequest Body;
	}
}
