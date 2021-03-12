using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000098 RID: 152
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MessageFormat : MessagingBase, IMessageFormat, IMessagingBase<MessageFormatConfiguration, SetMessageFormatConfiguration>, IEditObjectService<MessageFormatConfiguration, SetMessageFormatConfiguration>, IGetObjectService<MessageFormatConfiguration>
	{
		// Token: 0x06001BDC RID: 7132 RVA: 0x00057A60 File Offset: 0x00055C60
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self")]
		public PowerShellResults<MessageFormatConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<MessageFormatConfiguration>(identity);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00057A69 File Offset: 0x00055C69
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self+Set-MailboxMessageConfiguration?Identity@W:Self")]
		public PowerShellResults<MessageFormatConfiguration> SetObject(Identity identity, SetMessageFormatConfiguration properties)
		{
			return base.SetObject<MessageFormatConfiguration, SetMessageFormatConfiguration>(identity, properties);
		}
	}
}
