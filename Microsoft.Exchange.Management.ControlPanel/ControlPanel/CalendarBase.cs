using System;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000069 RID: 105
	public abstract class CalendarBase : DataSourceService
	{
		// Token: 0x06001A9E RID: 6814 RVA: 0x00054D0D File Offset: 0x00052F0D
		public PowerShellResults<O> GetObject<O>(Identity identity) where O : CalendarConfigurationBase
		{
			identity = Identity.FromExecutingUserId();
			return base.GetObject<O>("Get-MailboxCalendarConfiguration", identity);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00054D24 File Offset: 0x00052F24
		public PowerShellResults<O> SetObject<O, U>(Identity identity, U properties) where O : CalendarConfigurationBase where U : SetCalendarConfigurationBase
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<O> powerShellResults;
			lock (RbacPrincipal.Current.OwaOptionsLock)
			{
				powerShellResults = base.SetObject<O, U>("Set-MailboxCalendarConfiguration", identity, properties);
				if (powerShellResults != null && powerShellResults.Succeeded)
				{
					Util.NotifyOWAUserSettingsChanged(UserSettings.Calendar);
				}
			}
			return powerShellResults;
		}

		// Token: 0x04001B1B RID: 6939
		internal const string GetCmdlet = "Get-MailboxCalendarConfiguration";

		// Token: 0x04001B1C RID: 6940
		internal const string SetCmdlet = "Set-MailboxCalendarConfiguration";

		// Token: 0x04001B1D RID: 6941
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001B1E RID: 6942
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001B1F RID: 6943
		internal const string GetObjectRole = "Get-MailboxCalendarConfiguration?Identity@R:Self";

		// Token: 0x04001B20 RID: 6944
		internal const string SetObjectRole = "Get-MailboxCalendarConfiguration?Identity@R:Self+Set-MailboxCalendarConfiguration?Identity@W:Self";
	}
}
