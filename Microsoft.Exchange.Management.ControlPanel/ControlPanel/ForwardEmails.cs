using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A8 RID: 680
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class ForwardEmails : RecipientDataSourceService, IForwardEmails, IEditObjectService<ForwardEmailMailbox, SetForwardEmailMailbox>, IGetObjectService<ForwardEmailMailbox>
	{
		// Token: 0x06002BA5 RID: 11173 RVA: 0x000881F5 File Offset: 0x000863F5
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Mailbox?Identity@R:Self")]
		public PowerShellResults<ForwardEmailMailbox> GetObject(Identity identity)
		{
			identity = (identity ?? Identity.FromExecutingUserId());
			return base.GetObject<ForwardEmailMailbox>("Get-Mailbox", identity);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0008820F File Offset: 0x0008640F
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Mailbox?Identity@R:Self+Set-Mailbox?Identity@W:Self")]
		public PowerShellResults<ForwardEmailMailbox> SetObject(Identity identity, SetForwardEmailMailbox properties)
		{
			identity = (identity ?? Identity.FromExecutingUserId());
			return base.SetObject<ForwardEmailMailbox, SetForwardEmailMailbox>("Set-Mailbox", identity, properties);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0008822C File Offset: 0x0008642C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Mailbox?Identity@R:Self+Set-Mailbox?Identity@W:Self")]
		public PowerShellResults<ForwardEmailMailbox> StopForward(Identity[] identities, BaseWebServiceParameters parameters)
		{
			SetForwardEmailMailbox setForwardEmailMailbox = new SetForwardEmailMailbox();
			setForwardEmailMailbox.ForwardingSmtpAddress = null;
			return this.SetObject(identities[0], setForwardEmailMailbox);
		}

		// Token: 0x040021B3 RID: 8627
		private const string GetObjectRole = "Get-Mailbox?Identity@R:Self";

		// Token: 0x040021B4 RID: 8628
		private const string SetObjectRole = "Get-Mailbox?Identity@R:Self+Set-Mailbox?Identity@W:Self";

		// Token: 0x040021B5 RID: 8629
		private const string StopForwardRole = "Get-Mailbox?Identity@R:Self+Set-Mailbox?Identity@W:Self";
	}
}
