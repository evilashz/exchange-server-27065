using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020001FF RID: 511
	public class MapiMTCheckMemoryRestartResponder : RestartServiceResponder
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0005F844 File Offset: 0x0005DA44
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x0005F84C File Offset: 0x0005DA4C
		internal string AppPoolName { get; set; }

		// Token: 0x06000E48 RID: 3656 RVA: 0x0005F858 File Offset: 0x0005DA58
		internal static ResponderDefinition CreateDefinition(string name, string alertMask, ServiceHealthStatus targetHealthState, int serviceStopTimeoutInSeconds = 15, int serviceStartTimeoutInSeconds = 120, int serviceStartDelayInSeconds = 0, bool isMasterWorker = false, DumpMode dumpOnRestartMode = DumpMode.None, string dumpPath = null, double minimumFreeDiskPercent = 15.0, int maximumDumpDurationInSeconds = 0, string serviceName = "Exchange", string additionalProcessNameToKill = null, bool restartEnabled = true, bool enabled = true, string throttleGroupName = "Dag")
		{
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition(name, alertMask, "MSExchangeRPC", targetHealthState, serviceStopTimeoutInSeconds, serviceStartTimeoutInSeconds, serviceStartDelayInSeconds, isMasterWorker, dumpOnRestartMode, dumpPath, minimumFreeDiskPercent, maximumDumpDurationInSeconds, serviceName, additionalProcessNameToKill, restartEnabled, enabled, throttleGroupName, false);
			responderDefinition.AssemblyPath = MapiMTCheckMemoryRestartResponder.assemblyPath;
			responderDefinition.TypeName = MapiMTCheckMemoryRestartResponder.responderTypeName;
			responderDefinition.Attributes["AppPoolName"] = "MSExchangeRpcProxyAppPool";
			RestartResponderChecker[] array = new RestartResponderChecker[]
			{
				new MemoryRestartResponderChecker(null),
				new PerformanceCounterRestartResponderCheckers(null)
			};
			foreach (RestartResponderChecker restartResponderChecker in array)
			{
				responderDefinition.Attributes[restartResponderChecker.KeyOfEnabled] = restartResponderChecker.EnabledByDefault.ToString();
				responderDefinition.Attributes[restartResponderChecker.KeyOfSetting] = restartResponderChecker.DefaultSetting;
			}
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.RpcClientAccessRestart, "MSExchangeRPC", null);
			return responderDefinition;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0005F938 File Offset: 0x0005DB38
		protected override void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.AppPoolName = attributeHelper.GetString("AppPoolName", true, null);
			base.InitializeServiceAttributes(attributeHelper);
			base.InitializeDumpAttributes(attributeHelper);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0005F990 File Offset: 0x0005DB90
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.RpcClientAccessRestart, base.WindowsServiceName, this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate()
			{
				this.InternalRestartAppPoolAndService(cancellationToken);
			});
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0005F9E0 File Offset: 0x0005DBE0
		private bool IsRunningOnLowResource()
		{
			if (this.restartResponderCheckers == null)
			{
				this.restartResponderCheckers = new RestartResponderChecker[]
				{
					new MemoryRestartResponderChecker(base.Definition),
					new PerformanceCounterRestartResponderCheckers(base.Definition)
				};
			}
			foreach (RestartResponderChecker restartResponderChecker in this.restartResponderCheckers)
			{
				if (!restartResponderChecker.IsRestartAllowed)
				{
					ResponderResult result = base.Result;
					result.StateAttribute1 += restartResponderChecker.SkipReasonOrException;
					return true;
				}
				if (restartResponderChecker.SkipReasonOrException != null)
				{
					ResponderResult result2 = base.Result;
					result2.StateAttribute1 += restartResponderChecker.SkipReasonOrException;
				}
			}
			return false;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0005FE0C File Offset: 0x0005E00C
		internal void InternalRestartAppPoolAndService(CancellationToken cancellationToken)
		{
			MapiMTCheckMemoryRestartResponder.<>c__DisplayClass4 CS$<>8__locals1 = new MapiMTCheckMemoryRestartResponder.<>c__DisplayClass4();
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.entryId = Guid.NewGuid().ToString();
			if (this.EnsureIISServicesStarted(CS$<>8__locals1.cancellationToken))
			{
				throw new InvalidOperationException("IIS service was stopped, service start has been requested. App pool recycle couldn't be executed.");
			}
			using (ApplicationPoolHelper appPoolHelper = new ApplicationPoolHelper(this.AppPoolName))
			{
				Privilege.RunWithPrivilege("SeDebugPrivilege", true, delegate
				{
					if (CS$<>8__locals1.<>4__this.IsRunningOnLowResource())
					{
						return;
					}
					bool flag = false;
					using (ServiceHelper serviceHelper = new ServiceHelper(CS$<>8__locals1.<>4__this.WindowsServiceName, CS$<>8__locals1.cancellationToken))
					{
						List<int> workerProcessIds = appPoolHelper.WorkerProcessIds;
						if (CS$<>8__locals1.<>4__this.DumpArgs.IsDumpRequested)
						{
							CS$<>8__locals1.<>4__this.DumpOneWorkerProcess(workerProcessIds, CS$<>8__locals1.cancellationToken);
						}
						if (workerProcessIds == null || workerProcessIds.Count == 0)
						{
							appPoolHelper.Initialize();
							if (appPoolHelper.ApplicationPool.State == 1)
							{
								appPoolHelper.ApplicationPool.AutoStart = false;
								if (appPoolHelper.ApplicationPool.Stop() != 3)
								{
									throw new InvalidOperationException(string.Format("Failed to stop application pool (poolName={0}, state={1})", CS$<>8__locals1.<>4__this.AppPoolName, appPoolHelper.ApplicationPool.State));
								}
							}
						}
						else
						{
							ProcessHelper.Kill(workerProcessIds, ProcessKillMode.SelfOnly, CS$<>8__locals1.entryId);
						}
						using (Process process = serviceHelper.GetProcess())
						{
							if (process != null)
							{
								int id = process.Id;
								if (CS$<>8__locals1.<>4__this.DumpArgs.IsDumpRequested)
								{
									CS$<>8__locals1.<>4__this.ThrottledDumpProcess(process, CS$<>8__locals1.cancellationToken);
								}
								if (!CS$<>8__locals1.<>4__this.RestartEnabled)
								{
									return;
								}
								Process[] array = null;
								if (!string.IsNullOrEmpty(CS$<>8__locals1.<>4__this.AdditionalProcessNameToKill))
								{
									array = Process.GetProcessesByName(CS$<>8__locals1.<>4__this.AdditionalProcessNameToKill);
								}
								flag = true;
								if (CS$<>8__locals1.<>4__this.IsMasterAndWorker)
								{
									ProcessHelper.Kill(process, CS$<>8__locals1.<>4__this.MasterAndWorkerKillMode, CS$<>8__locals1.entryId);
								}
								else
								{
									ProcessHelper.Kill(process, ProcessKillMode.SelfOnly, CS$<>8__locals1.entryId);
								}
								if (array != null)
								{
									foreach (Process process2 in array)
									{
										using (process2)
										{
											try
											{
												ProcessHelper.KillProcess(process2, true, CS$<>8__locals1.entryId);
											}
											catch (Win32Exception)
											{
											}
										}
									}
								}
								serviceHelper.WaitUntilProcessGoesAway(id, CS$<>8__locals1.<>4__this.StopTimeout);
							}
						}
						if (flag)
						{
							serviceHelper.Sleep(CS$<>8__locals1.<>4__this.StartDelay);
						}
						serviceHelper.Start();
						serviceHelper.WaitForStatus(ServiceControllerStatus.Running, CS$<>8__locals1.<>4__this.StartTimeout);
					}
					if (appPoolHelper.ApplicationPool.State == 3)
					{
						appPoolHelper.ApplicationPool.AutoStart = true;
						ObjectState objectState = appPoolHelper.ApplicationPool.Start();
						if (objectState != null && objectState != 1)
						{
							throw new InvalidOperationException(string.Format("Failed to start application pool (poolName={0}, state={1})", CS$<>8__locals1.<>4__this.AppPoolName, objectState));
						}
					}
				});
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0005FF34 File Offset: 0x0005E134
		protected bool EnsureIISServicesStarted(CancellationToken cancellationToken)
		{
			bool serviceStartAttempted = false;
			int num = Interlocked.CompareExchange(ref MapiMTCheckMemoryRestartResponder.currentlyCheckingServiceStatus, 1, 0);
			if (num != 0)
			{
				return false;
			}
			try
			{
				Privilege.RunWithPrivilege("SeDebugPrivilege", true, delegate
				{
					foreach (string serviceName in MapiMTCheckMemoryRestartResponder.IisServiceNames)
					{
						using (ServiceHelper serviceHelper = new ServiceHelper(serviceName, cancellationToken))
						{
							serviceStartAttempted = (serviceHelper.Start() || serviceStartAttempted);
						}
					}
				});
			}
			finally
			{
				MapiMTCheckMemoryRestartResponder.currentlyCheckingServiceStatus = 0;
			}
			return serviceStartAttempted;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0006005C File Offset: 0x0005E25C
		private void DumpOneWorkerProcess(List<int> pids, CancellationToken cancellationToken)
		{
			if (pids.Count <= 0)
			{
				return;
			}
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.WatsonDump, this.AppPoolName, this, false, cancellationToken, null);
			recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
			{
				foreach (int processId in pids)
				{
					using (Process processByIdBestEffort = ProcessHelper.GetProcessByIdBestEffort(processId))
					{
						ProcessDumpHelper processDumpHelper = new ProcessDumpHelper(this.DumpArgs, cancellationToken);
						try
						{
							startEntry.Context = processDumpHelper.Generate(processByIdBestEffort, this.Definition.Name);
							break;
						}
						catch (Exception)
						{
						}
					}
				}
			});
		}

		// Token: 0x04000A9F RID: 2719
		private const string RpcClientAccessServiceName = "MSExchangeRPC";

		// Token: 0x04000AA0 RID: 2720
		private static readonly string assemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000AA1 RID: 2721
		private static readonly string responderTypeName = typeof(MapiMTCheckMemoryRestartResponder).FullName;

		// Token: 0x04000AA2 RID: 2722
		private static readonly string[] IisServiceNames = new string[]
		{
			"WAS",
			"W3SVC",
			"IISADMIN"
		};

		// Token: 0x04000AA3 RID: 2723
		private static int currentlyCheckingServiceStatus = 0;

		// Token: 0x04000AA4 RID: 2724
		private RestartResponderChecker[] restartResponderCheckers;

		// Token: 0x02000200 RID: 512
		public new static class AttributeNames
		{
			// Token: 0x04000AA6 RID: 2726
			internal const string AdditionalProcessNameToKill = "AdditionalProcessNameToKill";

			// Token: 0x04000AA7 RID: 2727
			internal const string AppPoolName = "AppPoolName";

			// Token: 0x04000AA8 RID: 2728
			internal const string DumpOnRestart = "DumpOnRestart";

			// Token: 0x04000AA9 RID: 2729
			internal const string DumpPath = "DumpPath";

			// Token: 0x04000AAA RID: 2730
			internal const string IsMasterAndWorker = "IsMasterAndWorker";

			// Token: 0x04000AAB RID: 2731
			internal const string MasterAndWorkerKillMode = "MasterAndWorkerKillMode";

			// Token: 0x04000AAC RID: 2732
			internal const string MaximumDumpDurationInSeconds = "MaximumDumpDurationInSeconds";

			// Token: 0x04000AAD RID: 2733
			internal const string MinimumFreeDiskPercent = "MinimumFreeDiskPercent";

			// Token: 0x04000AAE RID: 2734
			internal const string RestartEnabled = "RestartEnabled";

			// Token: 0x04000AAF RID: 2735
			internal const string ServiceStartDelay = "ServiceStartDelay";

			// Token: 0x04000AB0 RID: 2736
			internal const string ServiceStartTimeout = "ServiceStartTimeout";

			// Token: 0x04000AB1 RID: 2737
			internal const string ServiceStopTimeout = "ServiceStopTimeout";

			// Token: 0x04000AB2 RID: 2738
			internal const string throttleGroupName = "throttleGroupName";

			// Token: 0x04000AB3 RID: 2739
			internal const string WindowsServiceName = "WindowsServiceName";
		}

		// Token: 0x02000201 RID: 513
		internal new class DefaultValues
		{
			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00060120 File Offset: 0x0005E320
			internal static string DumpPath
			{
				get
				{
					if (string.IsNullOrEmpty(MapiMTCheckMemoryRestartResponder.DefaultValues.dumpPath))
					{
						try
						{
							MapiMTCheckMemoryRestartResponder.DefaultValues.dumpPath = Path.Combine(ExchangeSetupContext.InstallPath, "Dumps");
						}
						catch (SetupVersionInformationCorruptException)
						{
							MapiMTCheckMemoryRestartResponder.DefaultValues.dumpPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
						}
					}
					return MapiMTCheckMemoryRestartResponder.DefaultValues.dumpPath;
				}
			}

			// Token: 0x04000AB4 RID: 2740
			internal const int RecurrenceIntervalSeconds = 300;

			// Token: 0x04000AB5 RID: 2741
			internal const int WaitIntervalSeconds = 30;

			// Token: 0x04000AB6 RID: 2742
			internal const int TimeoutSeconds = 150;

			// Token: 0x04000AB7 RID: 2743
			internal const int MaxRetryAttempts = 3;

			// Token: 0x04000AB8 RID: 2744
			internal const DumpMode DumpOnRestart = DumpMode.None;

			// Token: 0x04000AB9 RID: 2745
			internal const double MinimumFreeDiskPercent = 15.0;

			// Token: 0x04000ABA RID: 2746
			internal const int MaximumDumpDurationInSeconds = 0;

			// Token: 0x04000ABB RID: 2747
			internal const bool RestartEnabled = true;

			// Token: 0x04000ABC RID: 2748
			internal const string ServiceName = "Exchange";

			// Token: 0x04000ABD RID: 2749
			internal const bool Enabled = true;

			// Token: 0x04000ABE RID: 2750
			internal const bool IsMasterWorker = false;

			// Token: 0x04000ABF RID: 2751
			internal const int ServiceStopTimeoutInSeconds = 15;

			// Token: 0x04000AC0 RID: 2752
			internal const int ServiceStartTimeoutInSeconds = 120;

			// Token: 0x04000AC1 RID: 2753
			internal const int ServiceStartDelayInSeconds = 0;

			// Token: 0x04000AC2 RID: 2754
			private static string dumpPath = string.Empty;
		}
	}
}
