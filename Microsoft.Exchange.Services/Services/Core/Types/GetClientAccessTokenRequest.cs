using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200042A RID: 1066
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetClientAccessTokenType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetClientAccessTokenRequest : BaseRequest
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x000A0AA5 File Offset: 0x0009ECA5
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x000A0AAD File Offset: 0x0009ECAD
		[XmlArrayItem(ElementName = "TokenRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(ClientAccessTokenRequestType))]
		[DataMember]
		public ClientAccessTokenRequestType[] TokenRequests { get; set; }

		// Token: 0x06001F3F RID: 7999 RVA: 0x000A0AB6 File Offset: 0x0009ECB6
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetClientAccessToken(callContext, this);
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000A0ABF File Offset: 0x0009ECBF
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000A0AC2 File Offset: 0x0009ECC2
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
