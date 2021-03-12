using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D17 RID: 3351
	[MessageContract(IsWrapped = false)]
	public class GetRoomsSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030A9 RID: 12457
		[MessageBodyMember(Name = "GetRoomsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetRoomsResponse Body;
	}
}
