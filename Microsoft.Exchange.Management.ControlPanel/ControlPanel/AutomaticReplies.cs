using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000260 RID: 608
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class AutomaticReplies : DataSourceService, IAutomaticReplies, IEditObjectService<AutoReplyConfiguration, SetAutoReplyConfiguration>, IGetObjectService<AutoReplyConfiguration>
	{
		// Token: 0x06002908 RID: 10504 RVA: 0x000810E0 File Offset: 0x0007F2E0
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxAutoReplyConfiguration?Identity@R:Self")]
		public PowerShellResults<AutoReplyConfiguration> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			return base.GetObject<AutoReplyConfiguration>("Get-MailboxAutoReplyConfiguration", identity);
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000810F8 File Offset: 0x0007F2F8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxAutoReplyConfiguration?Identity@R:Self+Set-MailboxAutoReplyConfiguration?Identity@W:Self")]
		public PowerShellResults<AutoReplyConfiguration> SetObject(Identity identity, SetAutoReplyConfiguration properties)
		{
			identity = Identity.FromExecutingUserId();
			return base.SetObject<AutoReplyConfiguration, SetAutoReplyConfiguration>("Set-MailboxAutoReplyConfiguration", identity, properties);
		}

		// Token: 0x040020A9 RID: 8361
		internal const string GetCmdlet = "Get-MailboxAutoReplyConfiguration";

		// Token: 0x040020AA RID: 8362
		internal const string SetCmdlet = "Set-MailboxAutoReplyConfiguration";

		// Token: 0x040020AB RID: 8363
		internal const string ReadScope = "@R:Self";

		// Token: 0x040020AC RID: 8364
		internal const string WriteScope = "@W:Self";

		// Token: 0x040020AD RID: 8365
		private const string GetObjectRole = "Get-MailboxAutoReplyConfiguration?Identity@R:Self";

		// Token: 0x040020AE RID: 8366
		private const string SetObjectRole = "Get-MailboxAutoReplyConfiguration?Identity@R:Self+Set-MailboxAutoReplyConfiguration?Identity@W:Self";
	}
}
