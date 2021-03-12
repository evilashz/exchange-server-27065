using System;
using System.Management.Automation;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000DA RID: 218
	[LocDescription(Strings.IDs.InstallRecoveryActionArbiterServiceTask)]
	[Cmdlet("Install", "RecoveryActionArbiterService")]
	public class InstallRecoveryActionArbiterService : ManageRecoveryActionArbiterService
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001BC22 File Offset: 0x00019E22
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001BC39 File Offset: 0x00019E39
		[Parameter(Mandatory = false)]
		public string UserName
		{
			get
			{
				return (string)base.Fields["UserName"];
			}
			set
			{
				base.Fields["UserName"] = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001BC4C File Offset: 0x00019E4C
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0001BC63 File Offset: 0x00019E63
		[Parameter(Mandatory = false)]
		public string Domain
		{
			get
			{
				return (string)base.Fields["Domain"];
			}
			set
			{
				base.Fields["Domain"] = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001BC76 File Offset: 0x00019E76
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0001BC8D File Offset: 0x00019E8D
		[Parameter(Mandatory = false)]
		public string Password
		{
			get
			{
				return (string)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (!string.IsNullOrEmpty(this.UserName))
			{
				base.Account = ServiceAccount.User;
				base.ServiceInstallContext.Parameters["Username"] = string.Format("{0}\\{1}", this.Domain, this.UserName);
				base.ServiceInstallContext.Parameters["Password"] = this.Password;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001BD11 File Offset: 0x00019F11
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
