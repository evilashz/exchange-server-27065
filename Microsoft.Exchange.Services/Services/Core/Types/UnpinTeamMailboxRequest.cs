using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000491 RID: 1169
	[XmlType("UnpinTeamMailboxRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UnpinTeamMailboxRequest : BaseRequest
	{
		// Token: 0x060022E3 RID: 8931 RVA: 0x000A360D File Offset: 0x000A180D
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UnpinTeamMailbox(callContext, this);
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000A3616 File Offset: 0x000A1816
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000A3620 File Offset: 0x000A1820
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x0400151B RID: 5403
		public EmailAddressWrapper EmailAddress;
	}
}
