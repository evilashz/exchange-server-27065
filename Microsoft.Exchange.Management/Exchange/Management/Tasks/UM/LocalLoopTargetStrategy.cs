using System;
using System.ServiceProcess;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D6A RID: 3434
	internal abstract class LocalLoopTargetStrategy
	{
		// Token: 0x170028F4 RID: 10484
		// (get) Token: 0x060083D1 RID: 33745
		public abstract string ServiceName { get; }

		// Token: 0x170028F5 RID: 10485
		// (get) Token: 0x060083D2 RID: 33746
		public abstract string CertificateThumbprint { get; }

		// Token: 0x170028F6 RID: 10486
		// (get) Token: 0x060083D3 RID: 33747
		public abstract ServerRole ServerRole { get; }

		// Token: 0x170028F7 RID: 10487
		// (get) Token: 0x060083D4 RID: 33748
		public abstract UMStartupMode StartupMode { get; }

		// Token: 0x170028F8 RID: 10488
		// (get) Token: 0x060083D5 RID: 33749
		public abstract string TargetFqdn { get; }

		// Token: 0x170028F9 RID: 10489
		// (get) Token: 0x060083D6 RID: 33750 RVA: 0x0021B4F1 File Offset: 0x002196F1
		public Server Server
		{
			get
			{
				this.EnsureServerInitialized();
				return this.server;
			}
		}

		// Token: 0x170028FA RID: 10490
		// (get) Token: 0x060083D7 RID: 33751 RVA: 0x0021B4FF File Offset: 0x002196FF
		// (set) Token: 0x060083D8 RID: 33752 RVA: 0x0021B507 File Offset: 0x00219707
		public string LocalFqdn { get; private set; }

		// Token: 0x170028FB RID: 10491
		// (get) Token: 0x060083D9 RID: 33753 RVA: 0x0021B510 File Offset: 0x00219710
		// (set) Token: 0x060083DA RID: 33754 RVA: 0x0021B518 File Offset: 0x00219718
		private protected IConfigurationSession ConfigurationSession { protected get; private set; }

		// Token: 0x060083DB RID: 33755 RVA: 0x0021B521 File Offset: 0x00219721
		protected LocalLoopTargetStrategy(IConfigurationSession configSession)
		{
			this.LocalFqdn = Utils.GetOwnerHostFqdn();
			this.ConfigurationSession = configSession;
		}

		// Token: 0x060083DC RID: 33756 RVA: 0x0021B53B File Offset: 0x0021973B
		public static LocalLoopTargetStrategy Create(IConfigurationSession configSession, bool callRouterSwitchSet)
		{
			if (callRouterSwitchSet)
			{
				return new LocalLoopTargetStrategy.CallRouterStrategy(configSession);
			}
			return new LocalLoopTargetStrategy.BackendStrategy(configSession);
		}

		// Token: 0x060083DD RID: 33757
		public abstract int GetPort(bool isSecured);

		// Token: 0x060083DE RID: 33758
		public abstract TestUMConnectivityHelper.UMConnectivityResults CreateTestResult();

		// Token: 0x060083DF RID: 33759 RVA: 0x0021B550 File Offset: 0x00219750
		public void CheckServiceRunning()
		{
			this.EnsureServerInitialized();
			using (ServiceController serviceController = new ServiceController(this.ServiceName))
			{
				if (serviceController.Status != ServiceControllerStatus.Running)
				{
					throw new ServiceNotStarted(this.ServiceName);
				}
			}
		}

		// Token: 0x060083E0 RID: 33760 RVA: 0x0021B5A0 File Offset: 0x002197A0
		private void EnsureServerInitialized()
		{
			if (this.server == null)
			{
				this.server = LocalServerCache.LocalServer;
				if (this.server == null || (this.server.CurrentServerRole & this.ServerRole) != this.ServerRole)
				{
					throw new LocalServerNotFoundException(this.LocalFqdn);
				}
			}
		}

		// Token: 0x04003FE1 RID: 16353
		private Server server;

		// Token: 0x02000D6B RID: 3435
		private class CallRouterStrategy : LocalLoopTargetStrategy
		{
			// Token: 0x170028FC RID: 10492
			// (get) Token: 0x060083E1 RID: 33761 RVA: 0x0021B5EE File Offset: 0x002197EE
			private SIPFEServerConfiguration Configuration
			{
				get
				{
					if (this.configuration == null)
					{
						this.configuration = SIPFEServerConfiguration.Find(base.Server, base.ConfigurationSession);
						if (this.configuration == null)
						{
							throw new SIPFEServerConfigurationNotFoundException(base.LocalFqdn);
						}
					}
					return this.configuration;
				}
			}

			// Token: 0x170028FD RID: 10493
			// (get) Token: 0x060083E2 RID: 33762 RVA: 0x0021B629 File Offset: 0x00219829
			public override ServerRole ServerRole
			{
				get
				{
					return ServerRole.Cafe;
				}
			}

			// Token: 0x170028FE RID: 10494
			// (get) Token: 0x060083E3 RID: 33763 RVA: 0x0021B62C File Offset: 0x0021982C
			public override UMStartupMode StartupMode
			{
				get
				{
					return this.Configuration.UMStartupMode;
				}
			}

			// Token: 0x170028FF RID: 10495
			// (get) Token: 0x060083E4 RID: 33764 RVA: 0x0021B639 File Offset: 0x00219839
			public override string ServiceName
			{
				get
				{
					return "MSExchangeUMCR";
				}
			}

			// Token: 0x17002900 RID: 10496
			// (get) Token: 0x060083E5 RID: 33765 RVA: 0x0021B640 File Offset: 0x00219840
			public override string CertificateThumbprint
			{
				get
				{
					return this.Configuration.UMCertificateThumbprint;
				}
			}

			// Token: 0x17002901 RID: 10497
			// (get) Token: 0x060083E6 RID: 33766 RVA: 0x0021B64D File Offset: 0x0021984D
			public override string TargetFqdn
			{
				get
				{
					if (this.Configuration.ExternalHostFqdn == null)
					{
						return base.LocalFqdn;
					}
					return this.Configuration.ExternalHostFqdn.ToString();
				}
			}

			// Token: 0x060083E7 RID: 33767 RVA: 0x0021B673 File Offset: 0x00219873
			public CallRouterStrategy(IConfigurationSession configSession) : base(configSession)
			{
			}

			// Token: 0x060083E8 RID: 33768 RVA: 0x0021B67C File Offset: 0x0021987C
			public override int GetPort(bool isSecured)
			{
				if (!isSecured)
				{
					return this.Configuration.SipTcpListeningPort;
				}
				return this.Configuration.SipTlsListeningPort;
			}

			// Token: 0x060083E9 RID: 33769 RVA: 0x0021B698 File Offset: 0x00219898
			public override TestUMConnectivityHelper.UMConnectivityResults CreateTestResult()
			{
				return new TestUMConnectivityHelper.LocalUMConnectivityOptionsResults();
			}

			// Token: 0x04003FE4 RID: 16356
			private SIPFEServerConfiguration configuration;
		}

		// Token: 0x02000D6C RID: 3436
		private class BackendStrategy : LocalLoopTargetStrategy
		{
			// Token: 0x17002902 RID: 10498
			// (get) Token: 0x060083EA RID: 33770 RVA: 0x0021B69F File Offset: 0x0021989F
			private UMServer Configuration
			{
				get
				{
					if (this.configuration == null)
					{
						this.configuration = new UMServer(base.Server);
					}
					return this.configuration;
				}
			}

			// Token: 0x17002903 RID: 10499
			// (get) Token: 0x060083EB RID: 33771 RVA: 0x0021B6C0 File Offset: 0x002198C0
			public override ServerRole ServerRole
			{
				get
				{
					return ServerRole.UnifiedMessaging;
				}
			}

			// Token: 0x17002904 RID: 10500
			// (get) Token: 0x060083EC RID: 33772 RVA: 0x0021B6C4 File Offset: 0x002198C4
			public override UMStartupMode StartupMode
			{
				get
				{
					return this.Configuration.UMStartupMode;
				}
			}

			// Token: 0x17002905 RID: 10501
			// (get) Token: 0x060083ED RID: 33773 RVA: 0x0021B6D1 File Offset: 0x002198D1
			public override string ServiceName
			{
				get
				{
					return "MSExchangeUM";
				}
			}

			// Token: 0x17002906 RID: 10502
			// (get) Token: 0x060083EE RID: 33774 RVA: 0x0021B6D8 File Offset: 0x002198D8
			public override string CertificateThumbprint
			{
				get
				{
					return this.Configuration.UMCertificateThumbprint;
				}
			}

			// Token: 0x17002907 RID: 10503
			// (get) Token: 0x060083EF RID: 33775 RVA: 0x0021B6E5 File Offset: 0x002198E5
			public override string TargetFqdn
			{
				get
				{
					if (this.Configuration.ExternalHostFqdn == null)
					{
						return base.LocalFqdn;
					}
					return this.Configuration.ExternalHostFqdn.ToString();
				}
			}

			// Token: 0x060083F0 RID: 33776 RVA: 0x0021B70B File Offset: 0x0021990B
			public BackendStrategy(IConfigurationSession configSession) : base(configSession)
			{
			}

			// Token: 0x060083F1 RID: 33777 RVA: 0x0021B714 File Offset: 0x00219914
			public override int GetPort(bool isSecured)
			{
				if (!isSecured)
				{
					return this.Configuration.SipTcpListeningPort;
				}
				return this.Configuration.SipTlsListeningPort;
			}

			// Token: 0x060083F2 RID: 33778 RVA: 0x0021B730 File Offset: 0x00219930
			public override TestUMConnectivityHelper.UMConnectivityResults CreateTestResult()
			{
				return new TestUMConnectivityHelper.LocalUMConnectivityResults();
			}

			// Token: 0x04003FE5 RID: 16357
			private UMServer configuration;
		}
	}
}
