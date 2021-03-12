using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200045D RID: 1117
	[XmlType("InitUMMailboxType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class InitUMMailboxRequest : BaseRequest
	{
		// Token: 0x060020EA RID: 8426 RVA: 0x000A209A File Offset: 0x000A029A
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new InitUMMailbox(callContext, this);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000A20A3 File Offset: 0x000A02A3
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000A20A6 File Offset: 0x000A02A6
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
