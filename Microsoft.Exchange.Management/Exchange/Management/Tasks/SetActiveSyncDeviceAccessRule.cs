using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000081 RID: 129
	[Cmdlet("Set", "ActiveSyncDeviceAccessRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetActiveSyncDeviceAccessRule : SetSystemConfigurationObjectTask<ActiveSyncDeviceAccessRuleIdParameter, ActiveSyncDeviceAccessRule>
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x00010778 File Offset: 0x0000E978
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (Datacenter.IsMultiTenancyEnabled() && this.DataObject.OrganizationId == OrganizationId.ForestWideOrgId && this.DataObject.AccessLevel != DeviceAccessLevel.Block)
			{
				base.WriteError(new ArgumentException(Strings.ErrorOnlyForestWideBlockIsAllowed), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000107CE File Offset: 0x0000E9CE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetActiveSyncDeviceAccessRule(this.Identity.ToString());
			}
		}
	}
}
