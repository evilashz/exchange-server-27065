using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200047E RID: 1150
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class CalendarNotificationSettingsService : DataSourceService, ICalendarNotificationSettingsService, IEditObjectService<CalendarNotificationSettings, SetCalendarNotificationSettings>, IGetObjectService<CalendarNotificationSettings>
	{
		// Token: 0x060039BD RID: 14781 RVA: 0x000AF4F0 File Offset: 0x000AD6F0
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TextMessagingAccount?Identity@R:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-CalendarNotification?Identity@R:Self")]
		public PowerShellResults<CalendarNotificationSettings> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<CalendarNotificationSettings> @object = base.GetObject<CalendarNotificationSettings>("Get-CalendarNotification", identity);
			if (!@object.Failed && @object.HasValue)
			{
				PowerShellResults<SmsOptions> powerShellResults = @object.MergeErrors<SmsOptions>(base.GetObject<SmsOptions>("Get-TextMessagingAccount", identity));
				if (powerShellResults.HasValue)
				{
					@object.Value.NotificationEnabled = powerShellResults.Value.NotificationEnabled;
					@object.Value.EasEnabled = powerShellResults.Value.EasEnabled;
				}
			}
			return @object;
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000AF569 File Offset: 0x000AD769
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-CalendarNotification?Identity@R:Self+Set-CalendarNotification?Identity@W:Self")]
		public PowerShellResults<CalendarNotificationSettings> SetObject(Identity identity, SetCalendarNotificationSettings properties)
		{
			identity = Identity.FromExecutingUserId();
			return base.SetObject<CalendarNotificationSettings, SetCalendarNotificationSettings>("Set-CalendarNotification", identity, properties);
		}

		// Token: 0x040026BA RID: 9914
		internal const string GetCmdlet = "Get-CalendarNotification";

		// Token: 0x040026BB RID: 9915
		internal const string SetCmdlet = "Set-CalendarNotification";

		// Token: 0x040026BC RID: 9916
		internal const string ReadScope = "@R:Self";

		// Token: 0x040026BD RID: 9917
		internal const string WriteScope = "@W:Self";

		// Token: 0x040026BE RID: 9918
		private const string GetObjectRole = "Get-CalendarNotification?Identity@R:Self";

		// Token: 0x040026BF RID: 9919
		private const string SetObjectRole = "Get-CalendarNotification?Identity@R:Self+Set-CalendarNotification?Identity@W:Self";
	}
}
