using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.TransportService
{
	// Token: 0x02000002 RID: 2
	internal class TransportService : ProcessManagerService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal TransportService(bool runningAsService) : base("MSExchangeTransport", TransportService.GetWorkerProcessPathName(TransportService.workerProcessName), TransportService.jobObjectName, true, 30, runningAsService, ExTraceGlobals.ServiceTracer, TransportService.serviceLogger)
		{
			base.CanPauseAndContinue = true;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000210C File Offset: 0x0000030C
		public override bool CanHandleConnectionsIfPassive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000210F File Offset: 0x0000030F
		protected override TimeSpan StartTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(15.0);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000211F File Offset: 0x0000031F
		protected override TimeSpan StopTimeout
		{
			get
			{
				return TimeSpan.FromHours(1.0);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002158 File Offset: 0x00000358
		internal static void Main(string[] args)
		{
			List<string> list = new List<string>();
			list.Add("SeAuditPrivilege");
			list.Add("SeChangeNotifyPrivilege");
			list.Add("SeCreateGlobalPrivilege");
			list.Add("SeImpersonatePrivilege");
			bool isSystem;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				isSystem = current.IsSystem;
			}
			if (isSystem)
			{
				list.Add("SeIncreaseQuotaPrivilege");
				list.Add("SeAssignPrimaryTokenPrivilege");
			}
			int num = Privileges.RemoveAllExcept(list.ToArray());
			if (num != 0)
			{
				Environment.Exit(num);
			}
			Globals.InitializeSinglePerfCounterInstance();
			ExWatson.Register();
			bool flag = !Environment.UserInteractive;
			bool flag2 = false;
			bool flag3 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.OrdinalIgnoreCase))
				{
					TransportService.Usage();
					Environment.Exit(0);
				}
				else if (text.StartsWith("-console", StringComparison.OrdinalIgnoreCase))
				{
					flag2 = true;
				}
				else if (text.StartsWith("-wait", StringComparison.OrdinalIgnoreCase))
				{
					flag3 = true;
				}
			}
			if (!flag)
			{
				if (!flag2)
				{
					TransportService.Usage();
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag3)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			SettingOverrideSync.Instance.Start(true);
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Transport.ADExceptionHandling.Enabled)
			{
				TransportService.adConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 324, "Main", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\TransportService\\TransportService.cs");
			}
			else
			{
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					TransportService.adConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 334, "Main", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\TransportService\\TransportService.cs");
				}, 0);
				if (!adoperationResult.Succeeded)
				{
					ExTraceGlobals.ServiceTracer.TraceError<Exception>(0L, "Error when getting AD configuration session: {0}", adoperationResult.Exception);
					TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_MSExchangeTransportInitializationFailure, null, new object[]
					{
						adoperationResult.Exception.ToString()
					});
					Environment.Exit(1);
				}
			}
			TransportService.transportService = new TransportService(flag);
			if (!TransportService.transportService.Initialize())
			{
				ExTraceGlobals.ServiceTracer.TraceError(0L, "Failed to initialize the service. Exiting.");
				ProcessManagerService.StopService();
			}
			if (!flag)
			{
				TransportService.transportService.OnStartInternal(args);
				bool flag4 = false;
				while (!flag4)
				{
					Console.WriteLine("Enter 'q' to shutdown.");
					string text2 = Console.ReadLine();
					if (string.IsNullOrEmpty(text2))
					{
						break;
					}
					switch (text2[0])
					{
					case 'q':
						flag4 = true;
						break;
					case 'r':
						TransportService.transportService.OnCustomCommandInternal(200);
						break;
					case 'u':
						TransportService.transportService.OnCustomCommandInternal(201);
						break;
					}
				}
				Console.WriteLine("Shutting down ...");
				TransportService.transportService.OnStopInternal();
				Console.WriteLine("Done.");
				return;
			}
			ServiceBase.Run(TransportService.transportService);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000244C File Offset: 0x0000064C
		protected override bool Initialize()
		{
			try
			{
				Kerberos.FlushTicketCache();
			}
			catch (Win32Exception ex)
			{
				if (ex.ErrorCode == -2146232828)
				{
					return false;
				}
				string text = string.Format("{0}{1}NativeErrorCode = {2}", ex.ToString(), Environment.NewLine, ex.NativeErrorCode);
				ExTraceGlobals.ServiceTracer.TraceError<string>(0L, "TransportService failed to flush Kerberos ticket cache on Initialize because {0}", text);
				TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_FailedToFlushTicketCacheOnInitialize, null, new object[]
				{
					text
				});
				ExWatson.SendGenericWatsonReport("E12", ExWatson.ApplicationVersion.ToString(), ExWatson.AppName, "15.00.1497.010", Assembly.GetExecutingAssembly().GetName().Name, ex.GetType().Name, ex.StackTrace, ex.GetHashCode().ToString(), ex.TargetSite.Name, text);
				return false;
			}
			return base.Initialize();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000254C File Offset: 0x0000074C
		protected override bool TryReadServerConfig()
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				TransportADNotificationAdapter.Instance.RegisterForMsExchangeTransportServiceDeletedEvents();
			}, 0);
			if (!adoperationResult.Succeeded)
			{
				TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_FailedToRegisterForDeletedObjectsNotification, null, new object[]
				{
					adoperationResult.Exception
				});
				ExTraceGlobals.ServiceTracer.TraceError<Exception>(0L, "Failed to register for Deleted object notifications. Details {0}", adoperationResult.Exception);
				ProcessManagerService.StopService();
			}
			this.readingConfigOnStart = true;
			ADOperationResult adoperationResult2;
			bool flag = this.ReadServerConfig(out adoperationResult2);
			this.readingConfigOnStart = false;
			if (!flag)
			{
				this.GenerateConfigFailureEventLog(adoperationResult2.Exception);
				return false;
			}
			return true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000025EC File Offset: 0x000007EC
		protected override void OnStopInternal()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService OnStopInternal");
			this.UnRegisterConfigurationChangeHandlers();
			base.OnStopInternal();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002644 File Offset: 0x00000844
		protected override void OnStartInternal(string[] args)
		{
			Process[] processesByName = Process.GetProcessesByName("EdgeTransport");
			if (processesByName.Length > 0)
			{
				int pid = Process.GetCurrentProcess().Id;
				Process[] processesByName2 = Process.GetProcessesByName("MSExchangeTransport");
				if (processesByName2.Length == 1)
				{
					foreach (Process process in processesByName)
					{
						TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_KillOrphanedWorker, null, new object[]
						{
							pid,
							process.Id
						});
						ExTraceGlobals.ServiceTracer.TraceDebug<int>(0L, "TransportService: Killing orphaned worker PID:{0}", process.Id);
						try
						{
							process.Kill();
						}
						catch (Win32Exception ex)
						{
							this.LogFailedKill(process.Id, ex.Message);
						}
						catch (NotSupportedException ex2)
						{
							this.LogFailedKill(process.Id, ex2.Message);
						}
						catch (InvalidOperationException ex3)
						{
							this.LogFailedKill(process.Id, ex3.Message);
						}
						if (!process.WaitForExit(10000))
						{
							this.LogFailedKill(process.Id, "Timeout");
							if (Environment.UserInteractive)
							{
								Console.WriteLine("Orphaned worker pid {0} did not exit.", process.Id);
								Environment.Exit(1);
							}
							ProcessManagerService.instance.Stop();
							return;
						}
					}
				}
				else
				{
					IEnumerable<string> source = from i in processesByName2
					where i.Id != pid
					select i.Id.ToString();
					string text = string.Join(",", source.ToArray<string>());
					TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_AnotherServiceRunning, null, new object[]
					{
						pid,
						text
					});
					ExTraceGlobals.ServiceTracer.TraceDebug<string>(0L, "TransportService: another service instance is running ({0}). Exiting", text);
					ProcessManagerService.StopService();
				}
			}
			base.OnStartInternal(args);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002870 File Offset: 0x00000A70
		protected override void OnShutdownInternal()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService OnShutdownInternal");
			this.UnRegisterConfigurationChangeHandlers();
			base.OnShutdownInternal();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000288F File Offset: 0x00000A8F
		protected override void OnConfigUpdate()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService OnConfigUpdate");
			base.OnConfigUpdate();
			base.GetAndSetBindings(false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000028AF File Offset: 0x00000AAF
		protected override void OnMemoryPressure()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService OnMemoryPressure");
			base.OnMemoryPressure();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000028C8 File Offset: 0x00000AC8
		protected override void OnClearConfigCache()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService ClearConfigCache");
			base.OnClearConfigCache();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000028E1 File Offset: 0x00000AE1
		protected override void OnBlockedSubmissionQueue()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService OnMemoryPressure");
			base.OnBlockedSubmissionQueue();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000028FA File Offset: 0x00000AFA
		protected override void OnForcedCrash()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService OnForcedCrash");
			base.OnForcedCrash();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002914 File Offset: 0x00000B14
		protected override void OnClearKerberosTicketCache()
		{
			try
			{
				ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService OnClearKerberosTicketCache");
				Kerberos.FlushTicketCache();
			}
			catch (Win32Exception arg)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Win32Exception>(0L, "TransportService failed to flush Kerberos ticket cache because {0}", arg);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A08 File Offset: 0x00000C08
		protected override bool GetBindings(out IPEndPoint[] bindings)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "Enter GetBindings");
			List<IPEndPoint> bindingList = new List<IPEndPoint>();
			bindings = null;
			bool flag = true;
			ADOperationResult success = ADOperationResult.Success;
			if (this.receiveConnectorNotificationCookie == null)
			{
				flag = this.RegisterConfigurationChangeHandlers(out success);
			}
			if (flag)
			{
				flag = ADNotificationAdapter.TryReadConfigurationPaged<ReceiveConnector>(delegate()
				{
					Server server = TransportService.adConfigurationSession.FindLocalServer();
					return TransportService.adConfigurationSession.FindPaged<ReceiveConnector>(server.Id, QueryScope.SubTree, null, null, ADGenericPagedReader<ReceiveConnector>.DefaultPageSize);
				}, delegate(ReceiveConnector connector)
				{
					if (connector.Enabled)
					{
						foreach (IPBinding ipbinding in connector.Bindings)
						{
							bindingList.Add(new IPEndPoint(ipbinding.Address, ipbinding.Port));
						}
					}
				}, 1, out success);
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceError<bool, ADOperationErrorCode>(0L, "AD result: ok={0}, opResult={1}", flag, success.ErrorCode);
			if (flag)
			{
				bindings = bindingList.ToArray();
				this.lastRetrievedBindings = bindings;
				ExTraceGlobals.ServiceTracer.TraceDebug<int>(0L, "New configuration retrieved: {0} bindings", bindings.Length);
				ExTraceGlobals.ServiceTracer.TraceDebug(0L, "Leave GetBindings");
				return true;
			}
			if (this.lastRetrievedBindings != null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug(0L, "Failed to retrieve configuration data, ignoring the update and continue with the current configuration");
				TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_ReadConfigReceiveConnectorIgnored, null, new object[0]);
				bindings = this.lastRetrievedBindings;
				return true;
			}
			this.GenerateConfigFailureEventLog(success.Exception);
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002B2E File Offset: 0x00000D2E
		protected override void RegisterWorkerEvents(WorkerProcessManager workerProcessManager)
		{
			workerProcessManager.OnWorkerContacted += TransportService.HandleWorkerContacted;
			workerProcessManager.OnWorkerExited += TransportService.HandleWorkerExited;
			workerProcessManager.OnDisconnectPerformanceCounters += TransportService.HandleDisconnectPerformanceCounters;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002B66 File Offset: 0x00000D66
		protected override void UnregisterWorkerEvents(WorkerProcessManager workerProcessManager)
		{
			workerProcessManager.OnWorkerContacted -= TransportService.HandleWorkerContacted;
			workerProcessManager.OnWorkerExited -= TransportService.HandleWorkerExited;
			workerProcessManager.OnDisconnectPerformanceCounters -= TransportService.HandleDisconnectPerformanceCounters;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B9E File Offset: 0x00000D9E
		protected override void OnCommandTimeout()
		{
			if (this.readingConfigOnStart)
			{
				ExTraceGlobals.ServiceTracer.TraceError(0L, "Failed to retrieve configuration data, due to a timeout. Exiting process");
				Environment.Exit(1);
			}
			base.OnCommandTimeout();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002BC8 File Offset: 0x00000DC8
		private static TransportServerAlivePerfCountersInstance GetServerAlivePerfCounterInstance()
		{
			TransportServerAlivePerfCountersInstance result = null;
			try
			{
				result = TransportServerAlivePerfCounters.GetInstance("_total");
			}
			catch (InvalidOperationException ex)
			{
				TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveAvailabilityCounterFailure, null, new object[]
				{
					"_total",
					ex
				});
				ExTraceGlobals.SmtpReceiveTracer.TraceError<string, InvalidOperationException>(0L, "Failed to update ServerAlive perf counter instance '{0}': {1}", "_total", ex);
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002C34 File Offset: 0x00000E34
		private static void HandleWorkerContacted()
		{
			TransportServerAlivePerfCountersInstance serverAlivePerfCounterInstance = TransportService.GetServerAlivePerfCounterInstance();
			if (serverAlivePerfCounterInstance != null)
			{
				serverAlivePerfCounterInstance.ServerAlive.RawValue = 100L;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002C58 File Offset: 0x00000E58
		private static void HandleWorkerExited()
		{
			TransportServerAlivePerfCountersInstance serverAlivePerfCounterInstance = TransportService.GetServerAlivePerfCounterInstance();
			if (serverAlivePerfCounterInstance != null)
			{
				serverAlivePerfCounterInstance.ServerAlive.RawValue = 0L;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002C7C File Offset: 0x00000E7C
		private static string GetWorkerProcessPathName(string imageFileName)
		{
			string fileName = Process.GetCurrentProcess().MainModule.FileName;
			string directoryName = Path.GetDirectoryName(fileName);
			return directoryName + "\\" + imageFileName;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002CAC File Offset: 0x00000EAC
		private static void Usage()
		{
			Console.WriteLine(Strings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002CCC File Offset: 0x00000ECC
		private static void HandleDisconnectPerformanceCounters()
		{
			TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_DisconnectingPerformanceCounters, null, new object[0]);
			foreach (string text in TransportService.TransportPerformanceCounterCategories)
			{
				try
				{
					TransportService.RemoveAllInstancesForProcess(text);
				}
				catch (FileMappingNotFoundException ex)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug<string, string>(0L, "Failed to disconnect perf counters from their old process in category {0}, FileMappingNotFoundException: {1}", text, ex.Message);
				}
				catch (Exception ex2)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug<string, string>(0L, "Failed to disconnect perf counters from their old process in category {0}, Exception: {1}", text, ex2.Message);
					TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_FailedToDisconnectPerformanceCounters, null, new object[]
					{
						text,
						ex2
					});
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002E10 File Offset: 0x00001010
		private static void RemoveAllInstancesForProcess(string categoryName)
		{
			using (PerformanceCounterMemoryMappedFile performanceCounterMemoryMappedFile = new PerformanceCounterMemoryMappedFile(categoryName, true))
			{
				performanceCounterMemoryMappedFile.RemoveCategory(categoryName, delegate(CounterEntry counter, LifetimeEntry lifetime, InstanceEntry currentInstance)
				{
					string processesInfo = ExServiceBase.GetProcessesInfo();
					TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_ProcessHoldingPerformanceCounter, null, new object[]
					{
						lifetime.ProcessId,
						counter.ToString(),
						currentInstance.ToString(),
						categoryName,
						processesInfo
					});
				});
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002F60 File Offset: 0x00001160
		private bool RegisterConfigurationChangeHandlers(out ADOperationResult opResult)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService register configuration change notifications");
			Server server;
			if (ADNotificationAdapter.TryReadConfiguration<Server>(delegate()
			{
				Server result;
				try
				{
					result = TransportService.adConfigurationSession.FindLocalServer();
				}
				catch (LocalServerNotFoundException)
				{
					result = null;
				}
				return result;
			}, out server, out opResult))
			{
				opResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					if (this.receiveConnectorNotificationCookie == null)
					{
						this.receiveConnectorNotificationCookie = TransportADNotificationAdapter.Instance.RegisterForLocalServerReceiveConnectorNotifications(server.Id, new ADNotificationCallback(this.ConnectorsConfigUpdate));
						ExTraceGlobals.ServiceTracer.TraceDebug<ADObjectId>(0L, "TransportService registered for Receive Connector configuration change notifications (server id={0})", server.Id);
					}
					this.serverNotificationCookie = TransportADNotificationAdapter.Instance.RegisterForExchangeServerNotifications(server.Id, new ADNotificationCallback(this.ServerConfigUpdate));
					ExTraceGlobals.ServiceTracer.TraceDebug<ADObjectId>(0L, "TransportService registered for Server configuration change notifications (server id={0})", server.Id);
				});
				return opResult.Succeeded;
			}
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService failed to get local server. Failed to register for configuration change notifications.");
			return false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002FEC File Offset: 0x000011EC
		private void UnRegisterConfigurationChangeHandlers()
		{
			ADNotificationListener.Stop();
			if (this.receiveConnectorNotificationCookie != null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService unregister ReceiveConnector configuration change notifications");
				TransportADNotificationAdapter.Instance.UnregisterChangeNotification(this.receiveConnectorNotificationCookie);
			}
			if (this.serverNotificationCookie != null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService unregister Server configuration change notifications");
				TransportADNotificationAdapter.Instance.UnregisterChangeNotification(this.serverNotificationCookie);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003050 File Offset: 0x00001250
		private void ConnectorsConfigUpdate(ADNotificationEventArgs args)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService ConnectorsConfigUpdate");
			base.GetAndSetBindings(false);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000306C File Offset: 0x0000126C
		private void ServerConfigUpdate(ADNotificationEventArgs args)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "TransportService ServerConfigUpdate");
			ADOperationResult adoperationResult;
			this.ReadServerConfig(out adoperationResult);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003094 File Offset: 0x00001294
		private void LogFailedKill(int pid, string message)
		{
			TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_KillOrphanedWorkerFailed, null, new object[]
			{
				pid,
				message
			});
			ExTraceGlobals.ServiceTracer.TraceDebug<int, string>(0L, "TransportService: Killing orphaned worker PID:{0} '{1}'.", pid, message);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000030DC File Offset: 0x000012DC
		private void GenerateConfigFailureEventLog(Exception configException)
		{
			if (configException is ADTransientException)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug(0L, "Failed to retrieve configuration data, ADTransientException");
				TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_ReadConfigReceiveConnectorUnavail, null, new object[0]);
				return;
			}
			string text = "Failed to retrieve configuration data, permanent exception";
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, text);
			TransportService.logger.LogEvent(TransportEventLogConstants.Tuple_ReadConfigReceiveConnectorFailed, null, new object[0]);
			EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, text, ResultSeverityLevel.Error, false);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000318C File Offset: 0x0000138C
		private bool ReadServerConfig(out ADOperationResult opResult)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug(0L, "Read MaxConnectionRatePerMinute from AD");
			Server server = null;
			if (!ADNotificationAdapter.TryReadConfiguration<Server>(delegate()
			{
				Server result;
				try
				{
					result = TransportService.adConfigurationSession.FindLocalServer();
				}
				catch (LocalServerNotFoundException)
				{
					result = null;
				}
				return result;
			}, out server, out opResult))
			{
				return false;
			}
			base.MaxConnectionRate = server.MaxConnectionRatePerMinute;
			return true;
		}

		// Token: 0x04000001 RID: 1
		internal const string TransportServiceEventSource = "MSExchange TransportService";

		// Token: 0x04000002 RID: 2
		private const string HelpOption = "-?";

		// Token: 0x04000003 RID: 3
		private const string ConsoleOption = "-console";

		// Token: 0x04000004 RID: 4
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000005 RID: 5
		private const int AdRetryCount = 3;

		// Token: 0x04000006 RID: 6
		private const int AdRetryIntervalMsec = 1000;

		// Token: 0x04000007 RID: 7
		private const int WorkerProcessExitTimeoutDefault = 30;

		// Token: 0x04000008 RID: 8
		private const uint ErrorSystemShutdownInProgress = 2148734468U;

		// Token: 0x04000009 RID: 9
		private const long TraceId = 0L;

		// Token: 0x0400000A RID: 10
		private static readonly List<string> TransportPerformanceCounterCategories = new List<string>
		{
			"MSExchange Secure Mail Transport",
			"MSExchange Store Driver",
			"MSExchangeTransport Configuration Cache",
			"MSExchangeTransport DeliveryAgent",
			"MSExchangeTransport DSN",
			"MSExchangeTransport Safety Net",
			"MSExchangeTransport Pickup",
			"MSExchangeTransport Queues",
			"MSExchangeTransport Resolver",
			"MSExchangeTransport Routing",
			"MSExchangeTransport ServerAlive",
			"MSExchangeTransport Shadow Redundancy",
			"MSExchangeTransport Shadow Redundancy Host Info",
			"MSExchangeTransport SMTPAvailability",
			"MSExchangeTransport SMTPReceive",
			"MSExchangeTransport SmtpSend",
			"MSExchangeTransport SMTPProxy",
			"MSExchange Interceptor Agent"
		};

		// Token: 0x0400000B RID: 11
		private static string workerProcessName = "edgetransport.exe";

		// Token: 0x0400000C RID: 12
		private static string jobObjectName = "Microsoft Exchange Transport";

		// Token: 0x0400000D RID: 13
		private static ITopologyConfigurationSession adConfigurationSession;

		// Token: 0x0400000E RID: 14
		private static TransportService transportService;

		// Token: 0x0400000F RID: 15
		private static ExEventLog serviceLogger = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, "MSExchange TransportService");

		// Token: 0x04000010 RID: 16
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, "MSExchangeTransport");

		// Token: 0x04000011 RID: 17
		private ADNotificationRequestCookie receiveConnectorNotificationCookie;

		// Token: 0x04000012 RID: 18
		private ADNotificationRequestCookie serverNotificationCookie;

		// Token: 0x04000013 RID: 19
		private IPEndPoint[] lastRetrievedBindings;

		// Token: 0x04000014 RID: 20
		private bool readingConfigOnStart;
	}
}
