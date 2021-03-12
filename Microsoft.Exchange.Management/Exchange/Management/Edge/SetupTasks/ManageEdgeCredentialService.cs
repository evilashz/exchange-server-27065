using System;
using System.Configuration.Install;
using System.IO;
using System.Management.Automation;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002FC RID: 764
	public abstract class ManageEdgeCredentialService : ManageService
	{
		// Token: 0x06001A31 RID: 6705 RVA: 0x00074700 File Offset: 0x00072900
		protected ManageEdgeCredentialService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.EdgeCredentialServiceDisplayName;
			base.Description = Strings.EdgeCredentialServiceDescription;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.EdgeCredentialSvc.exe");
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x000747BF File Offset: 0x000729BF
		// (set) Token: 0x06001A33 RID: 6707 RVA: 0x000747C7 File Offset: 0x000729C7
		[Parameter]
		public new string[] ServicesDependedOn
		{
			get
			{
				return base.ServicesDependedOn;
			}
			set
			{
				base.ServicesDependedOn = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x000747D0 File Offset: 0x000729D0
		protected override string Name
		{
			get
			{
				return "MSExchangeEdgeCredential";
			}
		}
	}
}
