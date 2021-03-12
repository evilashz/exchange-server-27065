using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009FB RID: 2555
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class RefreshGALContactsFolderRequest : BaseRequest
	{
		// Token: 0x0600483D RID: 18493 RVA: 0x0010153F File Offset: 0x000FF73F
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x00101549 File Offset: 0x000FF749
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
