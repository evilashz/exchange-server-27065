using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001E9 RID: 489
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Remove", "InstalledRoleInfo")]
	public sealed class RemoveInstalledRoleInfo : Task
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x0004999F File Offset: 0x00047B9F
		// (set) Token: 0x060010A7 RID: 4263 RVA: 0x000499B6 File Offset: 0x00047BB6
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

		// Token: 0x060010A8 RID: 4264 RVA: 0x000499CC File Offset: 0x00047BCC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.RoleName
			});
			RolesUtility.DeleteConfiguredVersion(this.RoleName);
			TaskLogger.LogExit();
		}
	}
}
