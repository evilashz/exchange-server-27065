using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200040D RID: 1037
	[XmlType("CreateUnifiedMailboxRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateUnifiedMailboxRequest : BaseAggregatedAccountRequest
	{
		// Token: 0x06001D9E RID: 7582 RVA: 0x0009F51C File Offset: 0x0009D71C
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateUnifiedMailbox(callContext, this);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0009F525 File Offset: 0x0009D725
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0009F528 File Offset: 0x0009D728
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
