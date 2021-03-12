using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000455 RID: 1109
	[XmlType("GetUMPinType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUMPinRequest : BaseRequest
	{
		// Token: 0x060020A3 RID: 8355 RVA: 0x000A1D68 File Offset: 0x0009FF68
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUMPin(callContext, this);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000A1D71 File Offset: 0x0009FF71
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000A1D74 File Offset: 0x0009FF74
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
