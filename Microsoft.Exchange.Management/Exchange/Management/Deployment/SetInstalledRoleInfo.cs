using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001E7 RID: 487
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Set", "InstalledRoleInfo")]
	public sealed class SetInstalledRoleInfo : Task
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00049867 File Offset: 0x00047A67
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x0004987E File Offset: 0x00047A7E
		[Parameter(Mandatory = true)]
		public string RoleName
		{
			get
			{
				return (string)base.Fields["RoleName"];
			}
			set
			{
				base.Fields["RoleName"] = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00049891 File Offset: 0x00047A91
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x000498A8 File Offset: 0x00047AA8
		[Parameter]
		public Version ConfiguredVersion
		{
			get
			{
				return (Version)base.Fields["ConfiguredVersion"];
			}
			set
			{
				base.Fields["ConfiguredVersion"] = value;
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000498BB File Offset: 0x00047ABB
		protected override void InternalValidate()
		{
			if (null == this.ConfiguredVersion)
			{
				this.ConfiguredVersion = ConfigurationContext.Setup.InstalledVersion;
			}
			base.InternalValidate();
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000498DC File Offset: 0x00047ADC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.RoleName,
				this.ConfiguredVersion
			});
			RolesUtility.SetConfiguredVersion(this.RoleName, this.ConfiguredVersion);
			base.WriteObject(this.ConfiguredVersion);
			TaskLogger.LogExit();
		}
	}
}
