using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	internal sealed class MailboxReplicationServiceImpl : ExServiceBase
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
		public MailboxReplicationServiceImpl()
		{
			base.ServiceName = "MSExchangeMailboxReplication";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002160 File Offset: 0x00000360
		public static void Main(string[] args)
		{
			ExWatson.Register();
			Globals.InitializeMultiPerfCounterInstance("MSExchMbxRepl");
			MailboxReplicationServiceImpl.runningAsService = !Environment.UserInteractive;
			bool flag = false;
			bool flag2 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.Ordinal))
				{
					MailboxReplicationServiceImpl.Usage();
					Environment.Exit(0);
				}
				else if (text.StartsWith("-console"))
				{
					flag = true;
				}
				else if (text.StartsWith("-wait"))
				{
					flag2 = true;
				}
			}
			if (!MailboxReplicationServiceImpl.runningAsService)
			{
				if (!flag)
				{
					MailboxReplicationServiceImpl.Usage();
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			MailboxReplicationServiceImpl.instance = new MailboxReplicationServiceImpl();
			if (!MailboxReplicationServiceImpl.runningAsService)
			{
				ExServiceBase.RunAsConsole(MailboxReplicationServiceImpl.instance);
				return;
			}
			ServiceBase.Run(MailboxReplicationServiceImpl.instance);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002248 File Offset: 0x00000448
		protected override void OnStartInternal(string[] args)
		{
			try
			{
				ConfigBase<MRSConfigSchema>.InitializeConfigProvider(new Func<IConfigSchema, IConfigProvider>(ConfigProvider.CreateProvider));
				if (!ConfigBase<MRSConfigSchema>.GetConfig<bool>("IsEnabled"))
				{
					MRSService.LogEvent(MRSEventLogConstants.Tuple_ServiceIsDisabled, new object[0]);
					this.AbortStartup(null);
				}
				else
				{
					MrsTracer.Service.Debug("Starting Mailbox Replication Service", new object[0]);
					MRSService.Instance = new MRSService();
					this.host = new ServiceHost(typeof(MailboxReplicationService), new Uri[0]);
					this.proxyHost = new ServiceHost(typeof(MailboxReplicationProxyService), new Uri[0]);
					if (MailboxReplicationServiceImpl.runningAsService)
					{
						base.RequestAdditionalTime((int)(this.host.OpenTimeout + TimeSpan.FromSeconds(10.0)).TotalMilliseconds);
					}
					this.host.Open(TimeSpan.FromSeconds(120.0));
					this.proxyHost.Open(TimeSpan.FromSeconds(120.0));
					MRSService.Instance.StartService();
					MrsTracer.Service.Debug("Mailbox Replication Service started successfully", new object[0]);
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						MRSService.LogEvent(MRSEventLogConstants.Tuple_ServiceStarted, new object[]
						{
							VersionInformation.MRS.ProductMajor,
							VersionInformation.MRS.ProductMinor,
							VersionInformation.MRS.BuildMajor,
							VersionInformation.MRS.BuildMinor,
							currentProcess.Id
						});
					}
				}
			}
			catch (AddressAlreadyInUseException ex)
			{
				this.AbortStartup(ex);
			}
			catch (ArgumentException ex2)
			{
				this.AbortStartup(ex2);
			}
			catch (ConfigurationErrorsException ex3)
			{
				this.AbortStartup(ex3);
			}
			catch (ConfigurationSettingsException ex4)
			{
				this.AbortStartup(ex4);
			}
			catch (InvalidOperationException ex5)
			{
				this.AbortStartup(ex5);
			}
			catch (System.TimeoutException ex6)
			{
				this.AbortStartup(ex6);
			}
			catch (System.ServiceProcess.TimeoutException ex7)
			{
				this.AbortStartup(ex7);
			}
			catch (CommunicationException ex8)
			{
				this.AbortStartup(ex8);
			}
			catch (RpcException ex9)
			{
				this.AbortStartup(ex9);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002548 File Offset: 0x00000748
		protected override void OnStopInternal()
		{
			try
			{
				MrsTracer.Service.Debug("Stopping Mailbox Replication Service", new object[0]);
				if (MailboxReplicationServiceImpl.runningAsService)
				{
					base.RequestAdditionalTime((int)(((this.host != null) ? this.host.CloseTimeout : TimeSpan.Zero) + TimeSpan.FromMinutes(2.0)).TotalMilliseconds);
				}
				if (this.host != null)
				{
					this.host.Abort();
					this.host.Close();
					this.host = null;
				}
				if (this.proxyHost != null)
				{
					this.proxyHost.Abort();
					this.proxyHost.Close();
					this.proxyHost = null;
				}
				if (MRSService.Instance != null)
				{
					MRSService.Instance.StopService();
				}
			}
			catch (System.TimeoutException)
			{
			}
			catch (System.ServiceProcess.TimeoutException)
			{
			}
			catch (CommunicationException)
			{
			}
			finally
			{
				if (MRSService.Instance != null)
				{
					MRSService.Instance.Dispose();
					MRSService.Instance = null;
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002664 File Offset: 0x00000864
		private static void Usage()
		{
			Console.WriteLine(MrsStrings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002684 File Offset: 0x00000884
		private void AbortStartup(Exception ex)
		{
			MRSService.LogEvent(MRSEventLogConstants.Tuple_ServiceFailedToStart, new object[]
			{
				(ex != null) ? CommonUtils.FullExceptionMessage(ex, true) : string.Empty
			});
			base.GracefullyAbortStartup();
		}

		// Token: 0x04000001 RID: 1
		private const string HelpOption = "-?";

		// Token: 0x04000002 RID: 2
		private const string ConsoleOption = "-console";

		// Token: 0x04000003 RID: 3
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000004 RID: 4
		private static MailboxReplicationServiceImpl instance;

		// Token: 0x04000005 RID: 5
		private static bool runningAsService;

		// Token: 0x04000006 RID: 6
		private ServiceHost host;

		// Token: 0x04000007 RID: 7
		private ServiceHost proxyHost;
	}
}
