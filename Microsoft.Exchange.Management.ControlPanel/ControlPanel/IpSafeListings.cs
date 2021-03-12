using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000409 RID: 1033
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class IpSafeListings : DataSourceService, IIpSafeListings, IEditObjectService<IpSafeListing, SetIpSafeListing>, IGetObjectService<IpSafeListing>
	{
		// Token: 0x060034E0 RID: 13536 RVA: 0x000A4F54 File Offset: 0x000A3154
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-PerimeterConfig?Identity@R:Organization")]
		public PowerShellResults<IpSafeListing> GetObject(Identity identity)
		{
			return base.GetObject<IpSafeListing>("Get-PerimeterConfig");
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000A4F64 File Offset: 0x000A3164
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-PerimeterConfig?Identity@R:Organization+Set-PerimeterConfig?Identity@W:Organization")]
		public PowerShellResults<IpSafeListing> SetObject(Identity identity, SetIpSafeListing properties)
		{
			identity = new Identity(RbacPrincipal.Current.RbacConfiguration.OrganizationId.ConfigurationUnit, RbacPrincipal.Current.RbacConfiguration.OrganizationId.ConfigurationUnit.Name);
			return base.SetObject<IpSafeListing, SetIpSafeListing>("Set-PerimeterConfig", identity, properties);
		}

		// Token: 0x04002544 RID: 9540
		private const string Noun = "PerimeterConfig";

		// Token: 0x04002545 RID: 9541
		internal const string GetCmdlet = "Get-PerimeterConfig";

		// Token: 0x04002546 RID: 9542
		internal const string SetCmdlet = "Set-PerimeterConfig";

		// Token: 0x04002547 RID: 9543
		internal const string ReadScope = "@R:Organization";

		// Token: 0x04002548 RID: 9544
		internal const string WriteScope = "@W:Organization";

		// Token: 0x04002549 RID: 9545
		internal const string GetObjectRole = "Get-PerimeterConfig?Identity@R:Organization";

		// Token: 0x0400254A RID: 9546
		private const string SetObjectRole = "Get-PerimeterConfig?Identity@R:Organization+Set-PerimeterConfig?Identity@W:Organization";
	}
}
