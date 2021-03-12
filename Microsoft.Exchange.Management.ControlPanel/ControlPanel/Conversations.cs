using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200008F RID: 143
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class Conversations : MessagingBase, IConversations, IMessagingBase<ConversationsConfiguration, SetConversationsConfiguration>, IEditObjectService<ConversationsConfiguration, SetConversationsConfiguration>, IGetObjectService<ConversationsConfiguration>
	{
		// Token: 0x06001BAB RID: 7083 RVA: 0x000576C4 File Offset: 0x000558C4
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self")]
		public PowerShellResults<ConversationsConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<ConversationsConfiguration>(identity);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000576CD File Offset: 0x000558CD
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self+Set-MailboxMessageConfiguration?Identity@W:Self")]
		public PowerShellResults<ConversationsConfiguration> SetObject(Identity identity, SetConversationsConfiguration properties)
		{
			return base.SetObject<ConversationsConfiguration, SetConversationsConfiguration>(identity, properties);
		}
	}
}
