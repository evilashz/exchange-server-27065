using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000609 RID: 1545
	public abstract class ManageMSExchangeMGMTService : ManageService
	{
		// Token: 0x060036D6 RID: 14038 RVA: 0x000E32F4 File Offset: 0x000E14F4
		protected ManageMSExchangeMGMTService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.MSExchangeMGMTDisplayName;
			base.Description = Strings.MSExchangeMGMTDescription;
			string binPath = ConfigurationContext.Setup.BinPath;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(binPath, "exmgmt.exe");
			base.ServiceInstallContext = installContext;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "exmgmt.exe");
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x060036D7 RID: 14039 RVA: 0x000E338D File Offset: 0x000E158D
		protected override string Name
		{
			get
			{
				return "MSExchangeMGMT";
			}
		}

		// Token: 0x0400256B RID: 9579
		public bool ForceFailure;
	}
}
