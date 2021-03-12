using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200009C RID: 156
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MessageOptions : MessagingBase, IMessageOptions, IMessagingBase<MessageOptionsConfiguration, SetMessageOptionsConfiguration>, IEditObjectService<MessageOptionsConfiguration, SetMessageOptionsConfiguration>, IGetObjectService<MessageOptionsConfiguration>
	{
		// Token: 0x06001BF1 RID: 7153 RVA: 0x00057B94 File Offset: 0x00055D94
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self")]
		public PowerShellResults<MessageOptionsConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<MessageOptionsConfiguration>(identity);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00057B9D File Offset: 0x00055D9D
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self+Set-MailboxMessageConfiguration?Identity@W:Self")]
		public PowerShellResults<MessageOptionsConfiguration> SetObject(Identity identity, SetMessageOptionsConfiguration properties)
		{
			return base.SetObject<MessageOptionsConfiguration, SetMessageOptionsConfiguration>(identity, properties);
		}
	}
}
