using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200042D RID: 1069
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetClutterStateRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetClutterStateRequest : BaseRequest
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x000A0B57 File Offset: 0x0009ED57
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetClutterState(callContext, this);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x000A0B60 File Offset: 0x0009ED60
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000A0B63 File Offset: 0x0009ED63
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
