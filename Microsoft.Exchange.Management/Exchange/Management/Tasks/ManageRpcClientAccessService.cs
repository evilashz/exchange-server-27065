using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000747 RID: 1863
	public abstract class ManageRpcClientAccessService : ManageService
	{
		// Token: 0x060041FC RID: 16892 RVA: 0x0010D514 File Offset: 0x0010B714
		protected ManageRpcClientAccessService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.RpcClientAccessServiceDisplayName;
			base.Description = Strings.RpcClientAccessServiceDescription;
			string path = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath);
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(path, "Microsoft.Exchange.RpcClientAccess.Service.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.FailureActionsFlag = true;
			base.ServiceInstallContext = installContext;
			base.CategoryCount = 1;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.RpcClientAccess.Service.EventLog.dll");
			base.ServiceInstaller.AfterInstall += this.AfterInstallEventHandler;
			base.AddFirewallRule(new MSExchangeRpcRPCRule());
			base.AddFirewallRule(new MSExchangeRpcRPCEPMapRule());
			base.AddFirewallRule(new MSExchangeRpcNumberedRule());
			base.AddFirewallRule(new MSExchangeABRPCEPMapFirewallRule());
			base.AddFirewallRule(new MSExchangeABNumberedFirewallRule());
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x0010D651 File Offset: 0x0010B851
		private void AfterInstallEventHandler(object sender, InstallEventArgs e)
		{
			base.LockdownServiceAccess();
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x0010D659 File Offset: 0x0010B859
		protected override string Name
		{
			get
			{
				return "MSExchangeRPC";
			}
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x0010D660 File Offset: 0x0010B860
		protected string RelativeInstallPath
		{
			get
			{
				return "bin";
			}
		}

		// Token: 0x04002992 RID: 10642
		public bool ForceFailure;
	}
}
