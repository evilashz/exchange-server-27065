using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D16 RID: 3350
	[MessageContract(IsWrapped = false)]
	public class GetRoomsSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030A8 RID: 12456
		[MessageBodyMember(Name = "GetRooms", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetRoomsRequest Body;
	}
}
