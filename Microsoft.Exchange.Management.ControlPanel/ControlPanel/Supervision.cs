using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000490 RID: 1168
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class Supervision : DataSourceService, ISupervision, IEditObjectService<SupervisionStatus, SetSupervisionStatus>, IGetObjectService<SupervisionStatus>
	{
		// Token: 0x06003A4B RID: 14923 RVA: 0x000B0720 File Offset: 0x000AE920
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-SupervisionPolicy?Identity@R:Self")]
		public PowerShellResults<SupervisionStatus> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			if (RbacPrincipal.Current.IsInRole("Get-SupervisionPolicy?DisplayDetails"))
			{
				PSCommand pscommand = new PSCommand();
				pscommand.AddCommand("Get-SupervisionPolicy");
				pscommand.AddParameter("DisplayDetails");
				return base.GetObject<SupervisionStatus>(pscommand, identity);
			}
			return base.GetObject<SupervisionStatus>("Get-SupervisionPolicy", identity);
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000B0778 File Offset: 0x000AE978
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-SupervisionPolicy?Identity@R:Self+MultiTenant+Set-SupervisionPolicy?Identity@W:Organization")]
		public PowerShellResults<SupervisionStatus> SetObject(Identity identity, SetSupervisionStatus properties)
		{
			identity = Identity.FromExecutingUserId();
			properties.FaultIfNull();
			PowerShellResults<SupervisionStatus> powerShellResults = new PowerShellResults<SupervisionStatus>();
			powerShellResults.MergeErrors<SupervisionStatus>(base.SetObject<SupervisionStatus, SetSupervisionStatus>("Set-SupervisionPolicy", identity, properties));
			if (powerShellResults.Failed)
			{
				return powerShellResults;
			}
			powerShellResults.MergeAll(base.SetObject<SupervisionStatus, SetClosedCampusOutboundPolicyConfiguration>("Set-SupervisionPolicy", identity, properties.MyClosedCampusOutboundPolicyConfiguration));
			return powerShellResults;
		}

		// Token: 0x040026E8 RID: 9960
		private const string DisplayDetails = "DisplayDetails";

		// Token: 0x040026E9 RID: 9961
		internal const string GetCmdlet = "Get-SupervisionPolicy";

		// Token: 0x040026EA RID: 9962
		internal const string GetSupervisionPolicyDetailsRole = "Get-SupervisionPolicy?DisplayDetails";

		// Token: 0x040026EB RID: 9963
		internal const string SetCmdlet = "Set-SupervisionPolicy";

		// Token: 0x040026EC RID: 9964
		internal const string ReadScope = "@R:Self";

		// Token: 0x040026ED RID: 9965
		internal const string WriteScope = "@W:Organization";

		// Token: 0x040026EE RID: 9966
		private const string GetObjectRole = "MultiTenant+Get-SupervisionPolicy?Identity@R:Self";

		// Token: 0x040026EF RID: 9967
		private const string SetObjectRole = "MultiTenant+Get-SupervisionPolicy?Identity@R:Self+MultiTenant+Set-SupervisionPolicy?Identity@W:Organization";
	}
}
