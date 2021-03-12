using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D14 RID: 3348
	[MessageContract(IsWrapped = false)]
	public class GetRoomListsSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030A6 RID: 12454
		[MessageBodyMember(Name = "GetRoomLists", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetRoomListsRequest Body;
	}
}
