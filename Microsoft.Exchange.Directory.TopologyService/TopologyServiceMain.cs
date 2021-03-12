using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Policy;
using System.Reflection;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Cache;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.EventLog;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000021 RID: 33
	internal class TopologyServiceMain : ExServiceBase
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000995C File Offset: 0x00007B5C
		internal TopologyServiceMain()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "TopologyServiceMain.TopologyServiceMain() - initialize");
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
			this.queue = new TopologyDiscoveryWorkProvider();
			this.forestMMMonitor = new ForestMMMonitor(this.queue);
			this.siteMonitor = new SiteMonitor(this.queue);
			ConfigurationData.LogServiceStartingEvent("TopologyServiceMain..const()");
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009A6C File Offset: 0x00007C6C
		public static void Main(string[] args)
		{
			ExWatson.Register();
			ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.Main() - ExWatson Register completed.");
			TopologyServiceMain.InitializePerfCounters();
			ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.Main() - PerCounters Initialized.");
			TopologyServiceMain.InitializeADDriver();
			ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.Main() - InitializeADDriver called.");
			TaskScheduler.UnobservedTaskException += delegate(object sender, UnobservedTaskExceptionEventArgs eventArgs)
			{
				eventArgs.SetObserved();
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)eventArgs.GetHashCode(), "Unobserved Exception {0}", eventArgs.Exception.Flatten().ToString());
				eventArgs.Exception.Handle(delegate(Exception ex)
				{
					ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)eventArgs.GetHashCode(), "Unobserved Exception {0}", ex);
					return true;
				});
			};
			TopologyServiceMain.runningAsService = !Environment.UserInteractive;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-console"))
				{
					flag = true;
				}
				else if (text.StartsWith("-wait"))
				{
					flag2 = true;
				}
			}
			if (flag && flag3)
			{
				throw new Exception("Startup crash to test ExWatson stuff");
			}
			TopologyServiceMain.instance = new TopologyServiceMain();
			ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.Main() - instance created.");
			if (TopologyServiceMain.runningAsService)
			{
				ServiceBase.Run(TopologyServiceMain.instance);
				return;
			}
			if (flag)
			{
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
				ExServiceBase.RunAsConsole(TopologyServiceMain.instance);
				return;
			}
			Console.WriteLine("Use the '-console' argument to run from the command line");
			Environment.Exit(0);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009B98 File Offset: 0x00007D98
		protected override void OnStartInternal(string[] args)
		{
			try
			{
				this.CheckCriticalDependencies(args);
				TopologyServiceMain.InitializeServiceHost(ref this.host, typeof(TopologyService), true);
				ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.OnStartInternal() - Service host created and configured.");
				if (Globals.IsDatacenter)
				{
					TopologyServiceMain.InitializeServiceHost(ref this.cacheServiceHost, typeof(DirectoryCacheService), false);
					ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.OnStartInternal() - Cache Service host created and configured.");
				}
				TopologyDiscoveryWorker.Instance.Start(new Action(this.AbortServiceGracefully));
				TopologyDiscoveryWorker.Instance.TryRegisterWorkQueue(base.GetType().FullName, this.queue);
				ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.OnStartInternal() - Topology Discovery worker started.");
				TopologyDiscoveryManager.Instance.Start((args != null && args.Length > 2) ? args[2] : null);
				ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.OnStartInternal() - TopologyDiscoveryManager Initialized.");
				this.siteMonitor.Start();
				ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.OnStartInternal() - SiteMonitor Initialized.");
				if (Globals.IsDatacenter)
				{
					this.forestMMMonitor.Start();
					ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.OnStartInternal() - MM Monitor Initialized.");
				}
				if (TopologyServiceMain.runningAsService)
				{
					TimeSpan timeSpan = this.host.OpenTimeout + TimeSpan.FromSeconds(10.0);
					base.RequestAdditionalTime((int)timeSpan.TotalMilliseconds);
					ConfigurationData.LogServiceStartingEvent(string.Format("TopologyServiceMain.OnStartInternal() - additional time requested. Total time (msec): {0}", timeSpan.TotalMilliseconds.ToString()));
				}
				this.host.Open(TimeSpan.FromSeconds(120.0));
				ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.OnStartInternal() - host opened.");
				if (Globals.IsDatacenter)
				{
					this.cacheServiceHost.Open(TimeSpan.FromSeconds(60.0));
					ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.OnStartInternal() - cache host opened.");
				}
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ADTopologyServiceStartSuccess, null, new object[0]);
			}
			catch (AddressAlreadyInUseException ex)
			{
				this.AbortStartup(ex);
			}
			catch (ArgumentException ex2)
			{
				this.AbortStartup(ex2);
			}
			catch (InvalidOperationException ex3)
			{
				this.AbortStartup(ex3);
			}
			catch (System.TimeoutException ex4)
			{
				this.AbortStartup(ex4);
			}
			catch (CannotGetSiteInfoException ex5)
			{
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_ServerNotInSiteFatal, "ServerNotInSiteFatal", new object[0]);
				this.AbortStartup(ex5);
			}
			catch (System.ServiceProcess.TimeoutException ex6)
			{
				this.AbortStartup(ex6);
			}
			catch (CommunicationException ex7)
			{
				this.AbortStartup(ex7);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009E48 File Offset: 0x00008048
		protected override void OnStopInternal()
		{
			try
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "TopologyServiceMain.OnStopInternal - Stopping TopologyService");
				if (TopologyServiceMain.runningAsService)
				{
					base.RequestAdditionalTime((int)(((this.host != null) ? this.host.CloseTimeout : TimeSpan.Zero) + TimeSpan.FromMinutes(2.0)).TotalMilliseconds);
				}
				this.ServiceCleanUp(true);
			}
			catch (System.TimeoutException arg)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "TopologyServiceMain.OnStopInternal() - TopologyService stopped with exception: {0}", arg);
			}
			catch (System.ServiceProcess.TimeoutException arg2)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "TopologyServiceMain.OnStopInternal() - TopologyService stopped with exception: {0}", arg2);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009F08 File Offset: 0x00008108
		private static void InitializePerfCounters()
		{
			if (Globals.InstanceType == InstanceType.NotInitialized)
			{
				Globals.InitializeSinglePerfCounterInstance();
			}
			foreach (ExPerformanceCounter exPerformanceCounter in TopologyServicePerfCounters.AllCounters)
			{
				exPerformanceCounter.RawValue = 0L;
			}
			TopologyServicePerfCounters.PID.RawValue = (long)Globals.ProcessId;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009F54 File Offset: 0x00008154
		private static void InitializeADDriver()
		{
			TopologyServiceServerSettings serverSettings = TopologyServiceServerSettings.CreateTopologyServiceServerSettings();
			ADSessionSettings.SetProcessADContext(new ADDriverContext(serverSettings, ContextMode.TopologyService));
			TopologyProvider.SetProcessTopologyMode(TopologyMode.Ldap);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009F7C File Offset: 0x0000817C
		private static void HandleUnknownMessageReceived(object sender, EventArgs e)
		{
			ExTraceGlobals.WCFServiceEndpointTracer.TraceError(0L, "Unknown Message Received {0}", new object[]
			{
				sender ?? string.Empty
			});
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009FB0 File Offset: 0x000081B0
		private static void HandleServiceHostFault(object sender, EventArgs e)
		{
			ServiceHost serviceHost = null;
			Trace trace = null;
			try
			{
				bool configSecurity = true;
				Type typeFromHandle;
				if (sender is TopologyService)
				{
					serviceHost = TopologyServiceMain.instance.host;
					typeFromHandle = typeof(TopologyService);
					trace = ExTraceGlobals.WCFServiceEndpointTracer;
					ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_WCFFaultedState, null, new object[0]);
				}
				else
				{
					serviceHost = TopologyServiceMain.instance.cacheServiceHost;
					typeFromHandle = typeof(DirectoryCacheService);
					configSecurity = false;
					trace = ExTraceGlobals.WCFServiceEndpointTracer;
				}
				trace.TraceError((long)TopologyServiceMain.instance.GetHashCode(), "Service Endpoint Faulted. Details {0}", new object[]
				{
					sender ?? string.Empty
				});
				serviceHost.Faulted -= TopologyServiceMain.HandleServiceHostFault;
				serviceHost.UnknownMessageReceived -= new EventHandler<UnknownMessageReceivedEventArgs>(TopologyServiceMain.HandleUnknownMessageReceived);
				WcfUtils.DisposeWcfClientGracefully(serviceHost, false);
				serviceHost = null;
				TopologyServiceMain.InitializeServiceHost(ref serviceHost, typeFromHandle, configSecurity);
				serviceHost.Open(TimeSpan.FromSeconds(120.0));
			}
			catch (Exception ex)
			{
				trace.TraceError<string>((long)TopologyServiceMain.instance.GetHashCode(), "Error trying to create WCF Endpoint. Details {0}", ex.ToString());
				TopologyServiceMain.instance.AbortServiceGracefully();
			}
			finally
			{
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000A0F8 File Offset: 0x000082F8
		private static void InitializeServiceHost(ref ServiceHost hostToInitialize, Type wcfType, bool configSecurity)
		{
			ArgumentValidator.ThrowIfNull("wcfType", wcfType);
			hostToInitialize = new ServiceHost(wcfType, new Uri[0]);
			hostToInitialize.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(TopologyServiceMain.HandleUnknownMessageReceived);
			hostToInitialize.Faulted += TopologyServiceMain.HandleServiceHostFault;
			if (configSecurity)
			{
				ServiceAuthorizationBehavior serviceAuthorizationBehavior = hostToInitialize.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
				serviceAuthorizationBehavior.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
				serviceAuthorizationBehavior.ExternalAuthorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy>
				{
					new AuthorizationPolicy()
				});
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000A180 File Offset: 0x00008380
		private void AbortStartup(Exception ex)
		{
			ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_ServiceFailedToStart, null, new object[]
			{
				ex
			});
			base.GracefullyAbortStartup();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000A1AA File Offset: 0x000083AA
		private void AbortServiceGracefully()
		{
			this.ServiceCleanUp(false);
			Environment.Exit(1);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000A1BC File Offset: 0x000083BC
		private void ServiceCleanUp(bool throwOnUnExpectedError)
		{
			try
			{
				if (this.host != null)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "TopologyServiceMain.ServiceCleanUp - Stopping Wcf Host");
					WcfUtils.DisposeWcfClientGracefully(this.host, false);
					this.host = null;
				}
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "TopologyServiceMain.ServiceCleanUp - Stopping Site Monitor");
				this.siteMonitor.Stop();
				if (Globals.IsDatacenter)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "TopologyServiceMain.ServiceCleanUp - Stopping MM Monitor");
					this.forestMMMonitor.Stop();
				}
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "TopologyServiceMain.ServiceCleanUp - Stopping Discovery Worker");
				TopologyDiscoveryWorker.Instance.Stop();
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ADTopologyServiceStopSuccess, null, new object[0]);
			}
			catch (System.TimeoutException arg)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "TopologyServiceMain.ServiceCleanUp() - TopologyService stopped with exception: {0}", arg);
			}
			catch (System.ServiceProcess.TimeoutException arg2)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "TopologyServiceMain.ServiceCleanUp() - TopologyService stopped with exception: {0}", arg2);
			}
			catch (CommunicationException arg3)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "TopologyServiceMain.ServiceCleanUp() - TopologyService stopped with exception: {0}", arg3);
			}
			catch (AggregateException arg4)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "TopologyServiceMain.ServiceCleanUp() - TopologyService stopped with exception: {0}", arg4);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "TopologyServiceMain.ServiceCleanUp() - TopologyService stopped with exception: {0}", ex);
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_ServiceStoppedWithException, null, new object[]
				{
					ex
				});
				if (throwOnUnExpectedError)
				{
					throw;
				}
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000A364 File Offset: 0x00008564
		private void CheckCriticalDependencies(string[] args)
		{
			NativeHelpers.GetSiteName(true);
			ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.CheckCriticalDependencies() - SiteName found.");
			if (TopologyServiceMain.runningAsService && args != null && args.Length > 0)
			{
				ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.CheckCriticalDependencies() - Checkin tokens.");
				this.CheckForMachineTokensMemberOfExchangeServers(args[1]);
			}
			int num = Privileges.RemoveAllExcept(TopologyServiceMain.ProcessTokenPrivileges);
			if (num != 0)
			{
				throw new TopologyServicePermanentException(Strings.ErrorAdjustingPrivileges(NativeMethods.HRESULT_FROM_WIN32((uint)num).ToString("X")));
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000A3D4 File Offset: 0x000085D4
		private void CheckForMachineTokensMemberOfExchangeServers(string sddl)
		{
			if (string.IsNullOrEmpty(sddl))
			{
				return;
			}
			try
			{
				this.exchangeServersSid = new SecurityIdentifier(sddl);
			}
			catch (ArgumentException arg)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string, ArgumentException>((long)this.GetHashCode(), "Error trying to create SID from passed arguments {0}. Error Details {1}", sddl, arg);
			}
			ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.CheckForMachineTokensMemberOfExchangeServers() - Token Membership LocalSystem");
			base.RequestAdditionalTime(630000);
			Exception ex;
			if (!this.TryCheckLocalSystemTokenIsMemberOfExchangeServers(out ex))
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Machine token is not member of exchange servers group {0}", (ex != null) ? ex.ToString() : "<NULL>");
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ADTOPO_RPC_FLUSH_LOCALSYSTEM_TICKET_FAILED, "StartADTopo_LocalSystem", new object[]
				{
					"LocalSystem",
					(ex != null) ? ex.Message : string.Empty,
					this.exchangeServersSid.ToString()
				});
				ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.CheckForMachineTokensMemberOfExchangeServers() - Error Token Membership LocalSystem");
				if (ex == null)
				{
					ex = new TopologyServicePermanentException(Strings.ErrorFlushingKerberosTicketForAccount("LocalSystem"));
				}
				throw ex;
			}
			ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.CheckForMachineTokensMemberOfExchangeServers() - Token Membership NetworkService");
			base.RequestAdditionalTime(630000);
			if (!this.TryCheckNetworkServiceTokenMemberOfExchangeServers(out ex))
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Network Service token is not member of exchange servers group {0}", (ex != null) ? ex.ToString() : "<NULL>");
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ADTOPO_RPC_FLUSH_NETWORKSERVICE_TICKET_FAILED, "StartADTopo_NetworkService", new object[]
				{
					"NetworkService",
					(ex != null) ? ex.Message : string.Empty,
					this.exchangeServersSid.ToString()
				});
				ConfigurationData.LogServiceStartingEvent("TopologyServiceMain.CheckForMachineTokensMemberOfExchangeServers() - Error Token Membership NetworkService");
				if (ex == null)
				{
					ex = new TopologyServicePermanentException(Strings.ErrorFlushingKerberosTicketForAccount("NetworkService"));
				}
				throw ex;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A574 File Offset: 0x00008774
		private bool TryCheckNetworkServiceTokenMemberOfExchangeServers(out Exception exception)
		{
			NetworkServiceImpersonator.Initialize();
			if (NetworkServiceImpersonator.Exception != null)
			{
				exception = NetworkServiceImpersonator.Exception;
				return false;
			}
			bool result;
			using (NetworkServiceImpersonator.Impersonate())
			{
				result = this.TryCheckLogonTokenIsMemberOfExchangeServers(out exception);
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A5C4 File Offset: 0x000087C4
		private bool TryCheckLocalSystemTokenIsMemberOfExchangeServers(out Exception exception)
		{
			return this.TryCheckLogonTokenIsMemberOfExchangeServers(out exception);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000A5D0 File Offset: 0x000087D0
		private bool TryCheckLogonTokenIsMemberOfExchangeServers(out Exception exception)
		{
			int num = 0;
			for (;;)
			{
				this.TryFlushKerberosTicket(out exception);
				if (null == this.exchangeServersSid)
				{
					break;
				}
				bool flag = false;
				using (AuthenticationContext authenticationContext = new AuthenticationContext())
				{
					SecurityStatus securityStatus = authenticationContext.LogonAsMachineAccount();
					if (securityStatus != SecurityStatus.OK)
					{
						exception = new Win32Exception((int)securityStatus);
						return false;
					}
					WindowsPrincipal windowsPrincipal = new WindowsPrincipal(authenticationContext.Identity);
					flag = windowsPrincipal.IsInRole(this.exchangeServersSid);
				}
				if (flag)
				{
					goto Block_3;
				}
				num++;
				ExTraceGlobals.ServiceTracer.TraceDebug<int, int, int>((long)this.GetHashCode(), "Token of machine does not contain Exchange Servers Group in attempt {0}/{1}. Sleeping {2} seconds", num, 60, 10);
				if (num < 60)
				{
					Thread.Sleep(10000);
				}
				if (num >= 60)
				{
					return false;
				}
			}
			exception = null;
			return true;
			Block_3:
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Machine is member of Exchange servers group");
			exception = null;
			return true;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000A6A8 File Offset: 0x000088A8
		private bool TryFlushKerberosTicket(out Exception exception)
		{
			exception = null;
			try
			{
				Kerberos.FlushTicketCache();
			}
			catch (Win32Exception ex)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Win32Exception>((long)this.GetHashCode(), "Error trying to flush kerberos ticket. Error Details {0}", ex);
				exception = ex;
				ConfigurationData.LogServiceStartingEvent("Error flushing kerberos token: " + exception.ToString());
			}
			return null == exception;
		}

		// Token: 0x04000083 RID: 131
		private const int MaxRetriesForTokenMembershipCheck = 60;

		// Token: 0x04000084 RID: 132
		private const int SleepSeconds = 10;

		// Token: 0x04000085 RID: 133
		private const string ConsoleOption = "-console";

		// Token: 0x04000086 RID: 134
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000087 RID: 135
		private static readonly string[] ProcessTokenPrivileges = new string[]
		{
			"SeAuditPrivilege",
			"SeChangeNotifyPrivilege",
			"SeCreateGlobalPrivilege"
		};

		// Token: 0x04000088 RID: 136
		private static TopologyServiceMain instance;

		// Token: 0x04000089 RID: 137
		private static bool runningAsService;

		// Token: 0x0400008A RID: 138
		private readonly TopologyDiscoveryWorkProvider queue;

		// Token: 0x0400008B RID: 139
		private readonly ForestMMMonitor forestMMMonitor;

		// Token: 0x0400008C RID: 140
		private readonly SiteMonitor siteMonitor;

		// Token: 0x0400008D RID: 141
		private ServiceHost host;

		// Token: 0x0400008E RID: 142
		private ServiceHost cacheServiceHost;

		// Token: 0x0400008F RID: 143
		private SecurityIdentifier exchangeServersSid;
	}
}
