using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000BC RID: 188
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class SchedulingPermissions : ResourceBase, ISchedulingPermissions, IResourceBase<SchedulingPermissionsConfiguration, SetSchedulingPermissionsConfiguration>, IEditObjectService<SchedulingPermissionsConfiguration, SetSchedulingPermissionsConfiguration>, IGetObjectService<SchedulingPermissionsConfiguration>
	{
		// Token: 0x06001CAA RID: 7338 RVA: 0x00058E81 File Offset: 0x00057081
		[PrincipalPermission(SecurityAction.Demand, Role = "Resource+Get-CalendarProcessing?Identity@R:Self")]
		public PowerShellResults<SchedulingPermissionsConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<SchedulingPermissionsConfiguration>(identity);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00058E8A File Offset: 0x0005708A
		[PrincipalPermission(SecurityAction.Demand, Role = "Resource+Get-CalendarProcessing?Identity@R:Self+Set-CalendarProcessing?Identity@W:Self")]
		public PowerShellResults<SchedulingPermissionsConfiguration> SetObject(Identity identity, SetSchedulingPermissionsConfiguration properties)
		{
			return base.SetObject<SchedulingPermissionsConfiguration, SetSchedulingPermissionsConfiguration>(identity, properties);
		}
	}
}
