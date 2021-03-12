using System;
using System.Globalization;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using System.Web;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000AB RID: 171
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class RegionalSettings : DataSourceService, IRegionalSettings, IEditObjectService<RegionalSettingsConfiguration, SetRegionalSettingsConfiguration>, IGetObjectService<RegionalSettingsConfiguration>
	{
		// Token: 0x06001C3B RID: 7227 RVA: 0x00058444 File Offset: 0x00056644
		internal PowerShellResults<RegionalSettingsConfiguration> GetSettings(Identity identity, bool verifyFolderNameLanguage)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Get-MailboxRegionalConfiguration");
			if (verifyFolderNameLanguage && RbacPrincipal.Current.IsInRole("Get-MailboxRegionalConfiguration?Identity&VerifyDefaultFolderNameLanguage@R:Self"))
			{
				pscommand.AddParameter("VerifyDefaultFolderNameLanguage");
			}
			return base.GetObject<RegionalSettingsConfiguration>(pscommand, identity);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0005848C File Offset: 0x0005668C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxRegionalConfiguration?Identity@R:Self+Get-MailboxCalendarConfiguration?Identity@R:Self")]
		public PowerShellResults<RegionalSettingsConfiguration> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<RegionalSettingsConfiguration> settings = this.GetSettings(identity, true);
			if (settings.SucceededWithValue)
			{
				if (settings.Value.UserCulture == null)
				{
					settings.Value.MailboxRegionalConfiguration.Language = Culture.GetDefaultCulture(HttpContext.Current);
				}
				PowerShellResults<MailboxCalendarConfiguration> powerShellResults = settings.MergeErrors<MailboxCalendarConfiguration>(base.GetObject<MailboxCalendarConfiguration>("Get-MailboxCalendarConfiguration", identity));
				if (powerShellResults.SucceededWithValue)
				{
					settings.Value.MailboxCalendarConfiguration = powerShellResults.Value;
				}
			}
			return settings;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00058508 File Offset: 0x00056708
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxRegionalConfiguration?Identity@R:Self+Get-MailboxCalendarConfiguration?Identity@R:Self+Set-MailboxRegionalConfiguration?Identity@W:Self")]
		public PowerShellResults<RegionalSettingsConfiguration> SetObject(Identity identity, SetRegionalSettingsConfiguration properties)
		{
			properties.FaultIfNull();
			identity = Identity.FromExecutingUserId();
			PowerShellResults<RegionalSettingsConfiguration> powerShellResults = null;
			bool flag = CultureInfo.CurrentCulture.LCID != properties.Language;
			properties.ReturnObjectType = ReturnObjectTypes.Full;
			lock (RbacPrincipal.Current.OwaOptionsLock)
			{
				powerShellResults = base.SetObject<RegionalSettingsConfiguration, SetRegionalSettingsConfiguration>("Set-MailboxRegionalConfiguration", identity, properties);
			}
			if (powerShellResults != null && powerShellResults.SucceededWithValue)
			{
				LocalSession.Current.AddRegionalSettingsToCache(powerShellResults.Value);
				if (HttpContext.Current.IsExplicitSignOn())
				{
					LocalSession.Current.UpdateUserTimeZone(powerShellResults.Value.TimeZone);
				}
				else
				{
					LocalSession.Current.UpdateRegionalSettings(powerShellResults.Value);
					if (flag)
					{
						LocalSession.Current.RbacConfiguration.ExecutingUserLanguagesChanged = true;
						HttpContext.Current.Response.Cookies.Add(new HttpCookie("mkt", powerShellResults.Value.UserCulture.Name)
						{
							HttpOnly = false
						});
					}
				}
				UserSettings userSettings = flag ? UserSettings.RegionAndLanguage : UserSettings.RegionWithoutLanguage;
				Util.NotifyOWAUserSettingsChanged(userSettings);
			}
			return powerShellResults;
		}

		// Token: 0x04001B89 RID: 7049
		internal const string GetCmdlet = "Get-MailboxRegionalConfiguration";

		// Token: 0x04001B8A RID: 7050
		internal const string SetCmdlet = "Set-MailboxRegionalConfiguration";

		// Token: 0x04001B8B RID: 7051
		internal const string GetCalendarCmdlet = "Get-MailboxCalendarConfiguration";

		// Token: 0x04001B8C RID: 7052
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001B8D RID: 7053
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001B8E RID: 7054
		internal const string GetSettingsRole = "Get-MailboxRegionalConfiguration?Identity@R:Self";

		// Token: 0x04001B8F RID: 7055
		private const string GetSettingsAndVerifyFolderLanguageRole = "Get-MailboxRegionalConfiguration?Identity&VerifyDefaultFolderNameLanguage@R:Self";

		// Token: 0x04001B90 RID: 7056
		private const string VerifyFolderLanguageParam = "VerifyDefaultFolderNameLanguage";

		// Token: 0x04001B91 RID: 7057
		private const string GetObjectRole = "Get-MailboxRegionalConfiguration?Identity@R:Self+Get-MailboxCalendarConfiguration?Identity@R:Self";

		// Token: 0x04001B92 RID: 7058
		private const string SetObjectRole = "Get-MailboxRegionalConfiguration?Identity@R:Self+Get-MailboxCalendarConfiguration?Identity@R:Self+Set-MailboxRegionalConfiguration?Identity@W:Self";
	}
}
