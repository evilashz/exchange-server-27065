using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200044A RID: 1098
	[XmlType(TypeName = "GetRoomListsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetRoomListsRequest : BaseRequest
	{
		// Token: 0x0600203A RID: 8250 RVA: 0x000A199A File Offset: 0x0009FB9A
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetRoomLists(callContext, this);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x000A19A3 File Offset: 0x0009FBA3
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000A19A6 File Offset: 0x0009FBA6
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
