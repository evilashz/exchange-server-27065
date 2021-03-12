using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000433 RID: 1075
	[XmlType("GetEncryptionConfigurationRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetEncryptionConfigurationRequest : BaseRequest
	{
		// Token: 0x06001F8E RID: 8078 RVA: 0x000A0E31 File Offset: 0x0009F031
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetEncryptionConfiguration(callContext, this);
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x000A0E3A File Offset: 0x0009F03A
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000A0E3D File Offset: 0x0009F03D
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
