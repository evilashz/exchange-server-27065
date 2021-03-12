using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000305 RID: 773
	public abstract class ManageFmsService : ManageService
	{
		// Token: 0x06001A4B RID: 6731 RVA: 0x00074BF4 File Offset: 0x00072DF4
		protected ManageFmsService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.FmsServiceDisplayName;
			base.Description = Strings.FmsServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.FipsBinPath, "FMS.exe");
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = null;
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00074C7D File Offset: 0x00072E7D
		protected override string Name
		{
			get
			{
				return "FMS";
			}
		}

		// Token: 0x04000B75 RID: 2933
		private const string ServiceShortName = "FMS";

		// Token: 0x04000B76 RID: 2934
		private const string ServiceBinaryName = "FMS.exe";

		// Token: 0x04000B77 RID: 2935
		private const string EventLogBinaryName = "Microsoft.Exchange.Common.ProcessManagerMsg.dll";
	}
}
