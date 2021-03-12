using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200042E RID: 1070
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetComplianceConfigurationRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetComplianceConfigurationRequest : BaseRequest
	{
		// Token: 0x06001F59 RID: 8025 RVA: 0x000A0B75 File Offset: 0x0009ED75
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetComplianceConfiguration(callContext, this);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000A0B7E File Offset: 0x0009ED7E
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x000A0B81 File Offset: 0x0009ED81
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
