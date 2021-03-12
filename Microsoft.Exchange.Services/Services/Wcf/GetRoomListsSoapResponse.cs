using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D15 RID: 3349
	[MessageContract(IsWrapped = false)]
	public class GetRoomListsSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030A7 RID: 12455
		[MessageBodyMember(Name = "GetRoomListsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetRoomListsResponse Body;
	}
}
