using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C1 RID: 705
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class MailMessages : DataSourceService, IMailMessages, INewObjectService<MailMessageRow, NewMailMessage>
	{
		// Token: 0x06002C17 RID: 11287 RVA: 0x00088D23 File Offset: 0x00086F23
		[PrincipalPermission(SecurityAction.Demand, Role = "Mailbox+New-MailMessage?Subject&Body&BodyFormat@W:Self")]
		public PowerShellResults<MailMessageRow> NewObject(NewMailMessage properties)
		{
			return base.NewObject<MailMessageRow, NewMailMessage>("New-MailMessage", properties);
		}

		// Token: 0x040021DB RID: 8667
		internal const string NewCmdlet = "New-MailMessage";

		// Token: 0x040021DC RID: 8668
		internal const string WriteScope = "@W:Self";

		// Token: 0x040021DD RID: 8669
		private const string NewObjectRole = "Mailbox+New-MailMessage?Subject&Body&BodyFormat@W:Self";
	}
}
