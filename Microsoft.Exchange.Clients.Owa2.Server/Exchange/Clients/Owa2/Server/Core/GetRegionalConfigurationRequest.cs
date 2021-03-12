using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003F7 RID: 1015
	[DataContract(Name = "GetRegionalConfigurationRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRegionalConfigurationRequest : BaseRequest
	{
		// Token: 0x060020FB RID: 8443 RVA: 0x000793F8 File Offset: 0x000775F8
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00079402 File Offset: 0x00077602
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
