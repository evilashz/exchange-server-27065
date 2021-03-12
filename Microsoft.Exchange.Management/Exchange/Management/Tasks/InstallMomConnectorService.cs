using System;
using System.Management.Automation;
using System.Security;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000D8 RID: 216
	[Cmdlet("Install", "MomConnectorService")]
	[LocDescription(Strings.IDs.InstallMomConnectorServiceTask)]
	public class InstallMomConnectorService : ManageMomConnectorService
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001BA1E File Offset: 0x00019C1E
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0001BA35 File Offset: 0x00019C35
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

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001BA48 File Offset: 0x00019C48
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0001BA5F File Offset: 0x00019C5F
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001BA72 File Offset: 0x00019C72
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x0001BA89 File Offset: 0x00019C89
		[Parameter(Mandatory = false)]
		public SecureString Password
		{
			get
			{
				return (SecureString)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001BA9C File Offset: 0x00019C9C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!string.IsNullOrEmpty(this.UserName))
			{
				base.Account = ServiceAccount.User;
				base.ServiceInstallContext.Parameters["Username"] = string.Format("{0}\\{1}", this.Domain, this.UserName);
				if (this.Password != null)
				{
					base.ServiceInstallContext.Parameters["Password"] = this.Password.ConvertToUnsecureString();
				}
			}
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
