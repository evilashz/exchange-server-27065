using System;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200008E RID: 142
	public abstract class MessagingBase : DataSourceService
	{
		// Token: 0x06001BA8 RID: 7080 RVA: 0x0005763C File Offset: 0x0005583C
		public PowerShellResults<O> GetObject<O>(Identity identity) where O : MessagingConfigurationBase
		{
			identity = Identity.FromExecutingUserId();
			return base.GetObject<O>("Get-MailboxMessageConfiguration", identity);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00057654 File Offset: 0x00055854
		public PowerShellResults<O> SetObject<O, U>(Identity identity, U properties) where O : MessagingConfigurationBase where U : SetMessagingConfigurationBase
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<O> powerShellResults;
			lock (RbacPrincipal.Current.OwaOptionsLock)
			{
				powerShellResults = base.SetObject<O, U>("Set-MailboxMessageConfiguration", identity, properties);
				if (powerShellResults != null && powerShellResults.Succeeded)
				{
					Util.NotifyOWAUserSettingsChanged(UserSettings.Mail);
				}
			}
			return powerShellResults;
		}

		// Token: 0x04001B6A RID: 7018
		internal const string GetCmdlet = "Get-MailboxMessageConfiguration";

		// Token: 0x04001B6B RID: 7019
		internal const string SetCmdlet = "Set-MailboxMessageConfiguration";

		// Token: 0x04001B6C RID: 7020
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001B6D RID: 7021
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001B6E RID: 7022
		internal const string GetObjectRole = "Get-MailboxMessageConfiguration?Identity@R:Self";

		// Token: 0x04001B6F RID: 7023
		internal const string SetObjectRole = "Get-MailboxMessageConfiguration?Identity@R:Self+Set-MailboxMessageConfiguration?Identity@W:Self";
	}
}
