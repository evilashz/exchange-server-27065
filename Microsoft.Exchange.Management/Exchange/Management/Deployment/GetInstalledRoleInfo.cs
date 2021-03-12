using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001E8 RID: 488
	[Cmdlet("Get", "InstalledRoleInfo")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class GetInstalledRoleInfo : Task
	{
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00049932 File Offset: 0x00047B32
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x00049949 File Offset: 0x00047B49
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

		// Token: 0x060010A4 RID: 4260 RVA: 0x0004995C File Offset: 0x00047B5C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.RoleName
			});
			Version configuredVersion = RolesUtility.GetConfiguredVersion(this.RoleName);
			base.WriteObject(configuredVersion);
			TaskLogger.LogExit();
		}
	}
}
