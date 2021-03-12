using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000070 RID: 112
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class CalendarDiagnosticLogService : DataSourceService, ICalendarDiagnosticLogService
	{
		// Token: 0x06001B06 RID: 6918 RVA: 0x000563DC File Offset: 0x000545DC
		[PrincipalPermission(SecurityAction.Demand, Role = "Enterprise+Get-CalendarDiagnosticLog?Identity@R:Self")]
		public PowerShellResults SendLog(CalendarDiagnosticLog properties)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Get-CalendarDiagnosticLog");
			pscommand.AddParameter("Identity", Identity.FromExecutingUserId());
			pscommand.AddParameters(properties);
			return base.Invoke(pscommand);
		}

		// Token: 0x04001B49 RID: 6985
		internal const string Cmdlet = "Get-CalendarDiagnosticLog";

		// Token: 0x04001B4A RID: 6986
		internal const string Scope = "@R:Self";

		// Token: 0x04001B4B RID: 6987
		private const string ObjectRole = "Enterprise+Get-CalendarDiagnosticLog?Identity@R:Self";
	}
}
