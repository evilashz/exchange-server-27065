using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000475 RID: 1141
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class JunkEmailConfigurations : DataSourceService, IJunkEmailConfigurations, IEditObjectService<JunkEmailConfiguration, SetJunkEmailConfiguration>, IGetObjectService<JunkEmailConfiguration>
	{
		// Token: 0x06003973 RID: 14707 RVA: 0x000AE8D5 File Offset: 0x000ACAD5
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxJunkEmailConfiguration?Identity@R:Self")]
		public PowerShellResults<JunkEmailConfiguration> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			return base.GetObject<JunkEmailConfiguration>("Get-MailboxJunkEmailConfiguration", identity);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000AE8EA File Offset: 0x000ACAEA
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxJunkEmailConfiguration?Identity@R:Self+Set-MailboxJunkEmailConfiguration?Identity@W:Self")]
		public PowerShellResults<JunkEmailConfiguration> SetObject(Identity identity, SetJunkEmailConfiguration properties)
		{
			identity = Identity.FromExecutingUserId();
			return base.SetObject<JunkEmailConfiguration, SetJunkEmailConfiguration>("Set-MailboxJunkEmailConfiguration", identity, properties);
		}

		// Token: 0x040026A2 RID: 9890
		internal const string GetCmdlet = "Get-MailboxJunkEmailConfiguration";

		// Token: 0x040026A3 RID: 9891
		internal const string SetCmdlet = "Set-MailboxJunkEmailConfiguration";

		// Token: 0x040026A4 RID: 9892
		internal const string ReadScope = "@R:Self";

		// Token: 0x040026A5 RID: 9893
		internal const string WriteScope = "@W:Self";

		// Token: 0x040026A6 RID: 9894
		private const string GetObjectRole = "Get-MailboxJunkEmailConfiguration?Identity@R:Self";

		// Token: 0x040026A7 RID: 9895
		private const string SetObjectRole = "Get-MailboxJunkEmailConfiguration?Identity@R:Self+Set-MailboxJunkEmailConfiguration?Identity@W:Self";
	}
}
