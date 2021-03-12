using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000480 RID: 1152
	[XmlType("SetTeamMailboxRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetTeamMailboxRequest : BaseRequest
	{
		// Token: 0x06002230 RID: 8752 RVA: 0x000A2D15 File Offset: 0x000A0F15
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SetTeamMailbox(callContext, this);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000A2D1E File Offset: 0x000A0F1E
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000A2D28 File Offset: 0x000A0F28
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x040014DA RID: 5338
		public EmailAddressWrapper EmailAddress;

		// Token: 0x040014DB RID: 5339
		public string SharePointSiteUrl;

		// Token: 0x040014DC RID: 5340
		public TeamMailboxLifecycleState State;
	}
}
