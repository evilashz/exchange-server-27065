using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D2B RID: 3371
	public abstract class ManageUMService : ManageService
	{
		// Token: 0x17002817 RID: 10263
		// (get) Token: 0x06008141 RID: 33089
		protected abstract string ServiceExeName { get; }

		// Token: 0x17002818 RID: 10264
		// (get) Token: 0x06008142 RID: 33090
		protected abstract string ServiceShortName { get; }

		// Token: 0x17002819 RID: 10265
		// (get) Token: 0x06008143 RID: 33091
		protected abstract string ServiceDisplayName { get; }

		// Token: 0x1700281A RID: 10266
		// (get) Token: 0x06008144 RID: 33092
		protected abstract string ServiceDescription { get; }

		// Token: 0x1700281B RID: 10267
		// (get) Token: 0x06008145 RID: 33093
		protected abstract ExchangeFirewallRule FirewallRule { get; }

		// Token: 0x1700281C RID: 10268
		// (get) Token: 0x06008146 RID: 33094
		protected abstract string RelativeInstallPath { get; }

		// Token: 0x06008147 RID: 33095 RVA: 0x00210D0C File Offset: 0x0020EF0C
		public ManageUMService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = this.ServiceDisplayName;
			base.Description = this.ServiceDescription;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 10000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 30000U;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath, this.ServiceExeName);
			string[] servicesDependedOn = new string[]
			{
				"KeyIso",
				"MSExchangeADTopology"
			};
			base.ServicesDependedOn = servicesDependedOn;
			base.ServiceInstallContext = installContext;
			base.AddFirewallRule(this.FirewallRule);
		}

		// Token: 0x1700281D RID: 10269
		// (get) Token: 0x06008148 RID: 33096 RVA: 0x00210DEF File Offset: 0x0020EFEF
		// (set) Token: 0x06008149 RID: 33097 RVA: 0x00210DF7 File Offset: 0x0020EFF7
		public bool ForceFailure
		{
			get
			{
				return this.forceFailure;
			}
			set
			{
				this.forceFailure = value;
			}
		}

		// Token: 0x1700281E RID: 10270
		// (get) Token: 0x0600814A RID: 33098 RVA: 0x00210E00 File Offset: 0x0020F000
		protected override string Name
		{
			get
			{
				return this.ServiceShortName;
			}
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x00210E08 File Offset: 0x0020F008
		protected void ReservePorts(ushort startPort, ushort numberOfPorts)
		{
			TcpListener.CreatePersistentTcpPortReservation(startPort, numberOfPorts);
		}

		// Token: 0x04003F22 RID: 16162
		private bool forceFailure;
	}
}
