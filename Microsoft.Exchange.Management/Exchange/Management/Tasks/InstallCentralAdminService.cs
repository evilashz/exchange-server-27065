using System;
using System.Management.Automation;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000D6 RID: 214
	[LocDescription(Strings.IDs.InstallCentralAdminServiceTask)]
	[Cmdlet("Install", "CentralAdminService")]
	public class InstallCentralAdminService : ManageCentralAdminService
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001B832 File Offset: 0x00019A32
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001B849 File Offset: 0x00019A49
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001B85C File Offset: 0x00019A5C
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0001B873 File Offset: 0x00019A73
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

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001B886 File Offset: 0x00019A86
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001B89D File Offset: 0x00019A9D
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

		// Token: 0x06000677 RID: 1655 RVA: 0x0001B8B0 File Offset: 0x00019AB0
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

		// Token: 0x06000678 RID: 1656 RVA: 0x0001B921 File Offset: 0x00019B21
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
