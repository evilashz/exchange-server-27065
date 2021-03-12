using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000245 RID: 581
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Set", "AdminConfiguredVersion")]
	public sealed class SetAdminConfiguredVersion : Task
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x0005B750 File Offset: 0x00059950
		public SetAdminConfiguredVersion()
		{
			base.Fields["InstallationMode"] = InstallationModes.Unknown;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0005B76E File Offset: 0x0005996E
		// (set) Token: 0x060015C0 RID: 5568 RVA: 0x0005B785 File Offset: 0x00059985
		[Parameter(Mandatory = true)]
		public InstallationModes Mode
		{
			get
			{
				return (InstallationModes)base.Fields["InstallationMode"];
			}
			set
			{
				base.Fields["InstallationMode"] = value;
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0005B79D File Offset: 0x0005999D
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0005B7B0 File Offset: 0x000599B0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			Version unpackedVersion = RolesUtility.GetUnpackedVersion("AdminToolsRole");
			if (unpackedVersion != null && unpackedVersion < AdminToolsRole.FirstConfiguredVersion && RolesUtility.GetConfiguredVersion("AdminToolsRole") == null)
			{
				TaskLogger.Log(Strings.UpdatingAdminToolsConfiguredVersion(unpackedVersion.ToString()));
				RolesUtility.SetConfiguredVersion("AdminToolsRole", unpackedVersion);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400097B RID: 2427
		private const string TargetRoleName = "AdminToolsRole";
	}
}
