using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B8 RID: 184
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class SchedulingOptions : ResourceBase, ISchedulingOptions, IResourceBase<SchedulingOptionsConfiguration, SetSchedulingOptionsConfiguration>, IEditObjectService<SchedulingOptionsConfiguration, SetSchedulingOptionsConfiguration>, IGetObjectService<SchedulingOptionsConfiguration>
	{
		// Token: 0x06001C8D RID: 7309 RVA: 0x00058BD4 File Offset: 0x00056DD4
		[PrincipalPermission(SecurityAction.Demand, Role = "Resource+Get-CalendarProcessing?Identity@R:Self+Get-MailboxCalendarConfiguration?Identity@R:Self")]
		public PowerShellResults<SchedulingOptionsConfiguration> GetObject(Identity identity)
		{
			PowerShellResults<SchedulingOptionsConfiguration> @object = base.GetObject<SchedulingOptionsConfiguration>(identity);
			if (@object.SucceededWithValue)
			{
				PowerShellResults<MailboxCalendarConfiguration> powerShellResults = @object.MergeErrors<MailboxCalendarConfiguration>(base.GetObject<MailboxCalendarConfiguration>("Get-MailboxCalendarConfiguration", Identity.FromExecutingUserId()));
				if (powerShellResults.SucceededWithValue)
				{
					@object.Value.MailboxCalendarConfiguration = powerShellResults.Value;
				}
			}
			return @object;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00058C24 File Offset: 0x00056E24
		[PrincipalPermission(SecurityAction.Demand, Role = "Resource+Get-CalendarProcessing?Identity@R:Self+Get-MailboxCalendarConfiguration?Identity@R:Self+Set-MailboxCalendarConfiguration?Identity@R:Self")]
		public PowerShellResults<SchedulingOptionsConfiguration> SetObject(Identity identity, SetSchedulingOptionsConfiguration properties)
		{
			properties.FaultIfNull();
			PowerShellResults<SchedulingOptionsConfiguration> powerShellResults = new PowerShellResults<SchedulingOptionsConfiguration>();
			powerShellResults.MergeErrors<CalendarConfiguration>(base.SetObject<CalendarConfiguration, SetMailboxCalendarConfiguration>("Set-MailboxCalendarConfiguration", Identity.FromExecutingUserId(), properties.SetMailboxCalendarConfiguration));
			if (powerShellResults.Failed)
			{
				return powerShellResults;
			}
			powerShellResults.MergeAll(base.SetObject<SchedulingOptionsConfiguration, SetSchedulingOptionsConfiguration>(identity, properties));
			return powerShellResults;
		}

		// Token: 0x04001BA2 RID: 7074
		internal const string GetCalendarCmdlet = "Get-MailboxCalendarConfiguration";

		// Token: 0x04001BA3 RID: 7075
		internal const string SetCalendarCmdlet = "Set-MailboxCalendarConfiguration";

		// Token: 0x04001BA4 RID: 7076
		private const string GetSchedulingOptionsRole = "Resource+Get-CalendarProcessing?Identity@R:Self+Get-MailboxCalendarConfiguration?Identity@R:Self";

		// Token: 0x04001BA5 RID: 7077
		private const string SetSchedulingOptionsRole = "Resource+Get-CalendarProcessing?Identity@R:Self+Get-MailboxCalendarConfiguration?Identity@R:Self+Set-MailboxCalendarConfiguration?Identity@R:Self";
	}
}
