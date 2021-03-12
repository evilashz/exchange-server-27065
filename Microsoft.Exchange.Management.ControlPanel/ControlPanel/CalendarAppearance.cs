using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200006A RID: 106
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class CalendarAppearance : CalendarBase, ICalendarAppearance, ICalendarBase<CalendarAppearanceConfiguration, SetCalendarAppearanceConfiguration>, IEditObjectService<CalendarAppearanceConfiguration, SetCalendarAppearanceConfiguration>, IGetObjectService<CalendarAppearanceConfiguration>
	{
		// Token: 0x06001AA1 RID: 6817 RVA: 0x00054D94 File Offset: 0x00052F94
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxCalendarConfiguration?Identity@R:Self")]
		public PowerShellResults<CalendarAppearanceConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<CalendarAppearanceConfiguration>(identity);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00054D9D File Offset: 0x00052F9D
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxCalendarConfiguration?Identity@R:Self+Set-MailboxCalendarConfiguration?Identity@W:Self")]
		public PowerShellResults<CalendarAppearanceConfiguration> SetObject(Identity identity, SetCalendarAppearanceConfiguration properties)
		{
			return base.SetObject<CalendarAppearanceConfiguration, SetCalendarAppearanceConfiguration>(identity, properties);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00054DA8 File Offset: 0x00052FA8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxCalendarConfiguration?Identity@R:Self+Set-MailboxCalendarConfiguration?Identity@W:Self")]
		public PowerShellResults<CalendarAppearanceConfiguration> UpdateObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			SetCalendarAppearanceConfiguration setCalendarAppearanceConfiguration = new SetCalendarAppearanceConfiguration();
			setCalendarAppearanceConfiguration.WorkingHoursTimeZone = ((RbacPrincipal.Current.UserTimeZone == null) ? ExTimeZone.CurrentTimeZone.Id : RbacPrincipal.Current.UserTimeZone.Id);
			PowerShellResults<CalendarAppearanceConfiguration> powerShellResults;
			lock (RbacPrincipal.Current.OwaOptionsLock)
			{
				powerShellResults = base.SetObject<CalendarAppearanceConfiguration, SetCalendarAppearanceConfiguration>("Set-MailboxCalendarConfiguration", identity, setCalendarAppearanceConfiguration);
			}
			if (powerShellResults != null && powerShellResults.Succeeded)
			{
				Util.NotifyOWAUserSettingsChanged(UserSettings.Calendar);
			}
			return powerShellResults;
		}
	}
}
