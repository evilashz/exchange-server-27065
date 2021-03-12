using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200007A RID: 122
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class CalendarWorkflow : DataSourceService, ICalendarWorkflow, IEditObjectService<CalendarWorkflowConfiguration, SetCalendarWorkflowConfiguration>, IGetObjectService<CalendarWorkflowConfiguration>
	{
		// Token: 0x06001B3D RID: 6973 RVA: 0x00056A3D File Offset: 0x00054C3D
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-CalendarProcessing?Identity@R:Self")]
		public PowerShellResults<CalendarWorkflowConfiguration> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			return base.GetObject<CalendarWorkflowConfiguration>("Get-CalendarProcessing", identity);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00056A54 File Offset: 0x00054C54
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-CalendarProcessing?Identity@R:Self+Set-CalendarProcessing?Identity@W:Self")]
		public PowerShellResults<CalendarWorkflowConfiguration> SetObject(Identity identity, SetCalendarWorkflowConfiguration properties)
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<CalendarWorkflowConfiguration> powerShellResults;
			lock (RbacPrincipal.Current.OwaOptionsLock)
			{
				powerShellResults = base.SetObject<CalendarWorkflowConfiguration, SetCalendarWorkflowConfiguration>("Set-CalendarProcessing", identity, properties);
			}
			if (powerShellResults != null && powerShellResults.Succeeded)
			{
				Util.NotifyOWAUserSettingsChanged(UserSettings.Calendar);
			}
			return powerShellResults;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00056ABC File Offset: 0x00054CBC
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-CalendarProcessing?Identity@R:Self+Set-CalendarProcessing?Identity@W:Self")]
		public PowerShellResults<CalendarWorkflowConfiguration> UpdateObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			SetCalendarWorkflowConfiguration setCalendarWorkflowConfiguration = new SetCalendarWorkflowConfiguration();
			setCalendarWorkflowConfiguration.AutomateProcessing = CalendarProcessingFlags.AutoUpdate.ToString();
			PowerShellResults<CalendarWorkflowConfiguration> powerShellResults;
			lock (RbacPrincipal.Current.OwaOptionsLock)
			{
				powerShellResults = base.SetObject<CalendarWorkflowConfiguration, SetCalendarWorkflowConfiguration>("Set-CalendarProcessing", identity, setCalendarWorkflowConfiguration);
			}
			if (powerShellResults != null && powerShellResults.Succeeded)
			{
				Util.NotifyOWAUserSettingsChanged(UserSettings.Calendar);
			}
			return powerShellResults;
		}

		// Token: 0x04001B4F RID: 6991
		internal const string GetCmdlet = "Get-CalendarProcessing";

		// Token: 0x04001B50 RID: 6992
		internal const string SetCmdlet = "Set-CalendarProcessing";

		// Token: 0x04001B51 RID: 6993
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001B52 RID: 6994
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001B53 RID: 6995
		private const string GetObjectRole = "Get-CalendarProcessing?Identity@R:Self";

		// Token: 0x04001B54 RID: 6996
		private const string SetObjectRole = "Get-CalendarProcessing?Identity@R:Self+Set-CalendarProcessing?Identity@W:Self";
	}
}
