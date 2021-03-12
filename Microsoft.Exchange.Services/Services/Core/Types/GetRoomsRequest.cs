using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200044B RID: 1099
	[XmlType(TypeName = "GetRoomsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetRoomsRequest : BaseRequest
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x000A19B1 File Offset: 0x0009FBB1
		// (set) Token: 0x0600203F RID: 8255 RVA: 0x000A19B9 File Offset: 0x0009FBB9
		[XmlElement("RoomList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public EmailAddressWrapper RoomList { get; set; }

		// Token: 0x06002040 RID: 8256 RVA: 0x000A19C2 File Offset: 0x0009FBC2
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetRooms(callContext, this);
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000A19CB File Offset: 0x0009FBCB
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000A19CE File Offset: 0x0009FBCE
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
