using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200040D RID: 1037
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class JournalReportNdrToService : DataSourceService, IJournalReportNdrTo, IEditObjectService<JournalReportNdrTo, SetJournalReportNdrTo>, IGetObjectService<JournalReportNdrTo>
	{
		// Token: 0x060034ED RID: 13549 RVA: 0x000A50C7 File Offset: 0x000A32C7
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TransportConfig@C:OrganizationConfig")]
		public PowerShellResults<JournalReportNdrTo> GetObject(Identity identity)
		{
			return base.GetObject<JournalReportNdrTo>("Get-TransportConfig");
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000A50D4 File Offset: 0x000A32D4
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TransportConfig@C:OrganizationConfig+Set-TransportConfig@C:OrganizationConfig")]
		public PowerShellResults<JournalReportNdrTo> SetObject(Identity identity, SetJournalReportNdrTo properties)
		{
			PowerShellResults<JournalReportNdrTo> powerShellResults = new PowerShellResults<JournalReportNdrTo>();
			properties.IgnoreNullOrEmpty = false;
			if (properties.Any<KeyValuePair<string, object>>())
			{
				PSCommand psCommand = new PSCommand().AddCommand("Set-TransportConfig");
				psCommand.AddParameters(properties);
				PowerShellResults<JournalReportNdrTo> results = base.Invoke<JournalReportNdrTo>(psCommand);
				powerShellResults.MergeAll(results);
			}
			if (powerShellResults.Succeeded)
			{
				powerShellResults.MergeAll(this.GetObject(identity));
			}
			return powerShellResults;
		}

		// Token: 0x0400254C RID: 9548
		internal const string GetCmdlet = "Get-TransportConfig";

		// Token: 0x0400254D RID: 9549
		internal const string SetCmdlet = "Set-TransportConfig";

		// Token: 0x0400254E RID: 9550
		internal const string JournalingReportNdrToParameterName = "JournalingReportNdrTo";

		// Token: 0x0400254F RID: 9551
		internal const string ReadScope = "@C:OrganizationConfig";

		// Token: 0x04002550 RID: 9552
		internal const string WriteScope = "@C:OrganizationConfig";

		// Token: 0x04002551 RID: 9553
		private const string GetObjectRole = "Get-TransportConfig@C:OrganizationConfig";

		// Token: 0x04002552 RID: 9554
		private const string SetObjectRole = "Get-TransportConfig@C:OrganizationConfig+Set-TransportConfig@C:OrganizationConfig";
	}
}
