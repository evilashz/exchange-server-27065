using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000F2 RID: 242
	public class ResetIISAppPoolResponder : ResponderWorkItem
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0002CEEF File Offset: 0x0002B0EF
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x0002CEF7 File Offset: 0x0002B0F7
		internal string AppPoolName { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0002CF00 File Offset: 0x0002B100
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x0002CF08 File Offset: 0x0002B108
		internal CommonDumpParameters DumpArgs { get; set; }

		// Token: 0x0600078E RID: 1934 RVA: 0x0002CF14 File Offset: 0x0002B114
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, string appPoolName, ServiceHealthStatus responderTargetState, DumpMode dumpOnRestartMode = DumpMode.None, string dumpPath = null, double minimumFreeDiskPercent = 15.0, int maximumDumpDurationInSeconds = 0, string serviceName = "Exchange", bool enabled = true, string throttleGroupName = null)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = ResetIISAppPoolResponder.AssemblyPath;
			responderDefinition.TypeName = ResetIISAppPoolResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = monitorName;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = enabled;
			if (string.IsNullOrEmpty(dumpPath))
			{
				dumpPath = ResetIISAppPoolResponder.DefaultValues.DumpPath;
			}
			responderDefinition.Attributes["AppPoolName"] = appPoolName;
			responderDefinition.Attributes["DumpOnRestart"] = dumpOnRestartMode.ToString();
			responderDefinition.Attributes["DumpPath"] = dumpPath;
			responderDefinition.Attributes["MinimumFreeDiskPercent"] = minimumFreeDiskPercent.ToString();
			responderDefinition.Attributes["MaximumDumpDurationInSeconds"] = maximumDumpDurationInSeconds.ToString();
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.RecycleApplicationPool, appPoolName, null);
			return responderDefinition;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0002D01C File Offset: 0x0002B21C
		internal virtual void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.AppPoolName = attributeHelper.GetString("AppPoolName", true, null);
			this.InitializeDumpAttributes(attributeHelper);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0002D0F0 File Offset: 0x0002B2F0
		internal void InternalRecyleAppPool(RecoveryActionEntry startEntry, CancellationToken cancellationToken)
		{
			if (this.EnsureIISServicesStarted(cancellationToken))
			{
				throw new InvalidOperationException("IIS service was stopped, service start has been requested. App pool recycle couldn't be executed.");
			}
			using (ApplicationPoolHelper appPoolHelper = new ApplicationPoolHelper(this.AppPoolName))
			{
				List<int> processIds = appPoolHelper.WorkerProcessIds;
				Privilege.RunWithPrivilege("SeDebugPrivilege", true, delegate
				{
					if (this.DumpArgs.IsDumpRequested)
					{
						this.DumpOneWorkerProcess(processIds, cancellationToken);
					}
					if (processIds == null || processIds.Count == 0)
					{
						appPoolHelper.Recycle();
						return;
					}
					ProcessHelper.Kill(processIds, ProcessKillMode.SelfOnly, startEntry.InstanceId);
				});
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0002D21C File Offset: 0x0002B41C
		protected bool EnsureIISServicesStarted(CancellationToken cancellationToken)
		{
			bool serviceStartAttempted = false;
			int num = Interlocked.CompareExchange(ref ResetIISAppPoolResponder.currentlyCheckingServiceStatus, 1, 0);
			if (num != 0)
			{
				return false;
			}
			try
			{
				Privilege.RunWithPrivilege("SeDebugPrivilege", true, delegate
				{
					foreach (string serviceName in ResetIISAppPoolResponder.IisServiceNames)
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
				ResetIISAppPoolResponder.currentlyCheckingServiceStatus = 0;
			}
			return serviceStartAttempted;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0002D28C File Offset: 0x0002B48C
		protected void InitializeDumpAttributes(AttributeHelper attributeHelper)
		{
			this.DumpArgs = new CommonDumpParameters
			{
				Mode = attributeHelper.GetEnum<DumpMode>("DumpOnRestart", false, DumpMode.None),
				Path = attributeHelper.GetString("DumpPath", false, ResetIISAppPoolResponder.DefaultValues.DumpPath),
				MinimumFreeSpace = attributeHelper.GetDouble("MinimumFreeDiskPercent", false, 15.0, new double?(0.0), new double?(100.0)),
				MaximumDurationInSeconds = attributeHelper.GetInt("MaximumDumpDurationInSeconds", false, 0, null, null)
			};
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0002D348 File Offset: 0x0002B548
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.RecycleApplicationPool, this.AppPoolName, this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
			{
				this.InternalRecyleAppPool(startEntry, cancellationToken);
			});
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0002D450 File Offset: 0x0002B650
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

		// Token: 0x04000506 RID: 1286
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000507 RID: 1287
		private static readonly string TypeName = typeof(ResetIISAppPoolResponder).FullName;

		// Token: 0x04000508 RID: 1288
		private static readonly string[] IisServiceNames = new string[]
		{
			"WAS",
			"W3SVC",
			"IISADMIN"
		};

		// Token: 0x04000509 RID: 1289
		private static int currentlyCheckingServiceStatus = 0;

		// Token: 0x020000F3 RID: 243
		internal static class AttributeNames
		{
			// Token: 0x0400050C RID: 1292
			internal const string AppPoolName = "AppPoolName";

			// Token: 0x0400050D RID: 1293
			internal const string DumpOnRestart = "DumpOnRestart";

			// Token: 0x0400050E RID: 1294
			internal const string DumpPath = "DumpPath";

			// Token: 0x0400050F RID: 1295
			internal const string MinimumFreeDiskPercent = "MinimumFreeDiskPercent";

			// Token: 0x04000510 RID: 1296
			internal const string MaximumDumpDurationInSeconds = "MaximumDumpDurationInSeconds";

			// Token: 0x04000511 RID: 1297
			internal const string throttleGroupName = "throttleGroupName";
		}

		// Token: 0x020000F4 RID: 244
		internal class DefaultValues
		{
			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000797 RID: 1943 RVA: 0x0002D514 File Offset: 0x0002B714
			internal static string DumpPath
			{
				get
				{
					if (string.IsNullOrEmpty(ResetIISAppPoolResponder.DefaultValues.dumpPath))
					{
						try
						{
							ResetIISAppPoolResponder.DefaultValues.dumpPath = Path.Combine(ExchangeSetupContext.InstallPath, "Dumps");
						}
						catch (SetupVersionInformationCorruptException)
						{
							ResetIISAppPoolResponder.DefaultValues.dumpPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
						}
					}
					return ResetIISAppPoolResponder.DefaultValues.dumpPath;
				}
			}

			// Token: 0x04000512 RID: 1298
			internal const DumpMode DumpOnRestart = DumpMode.None;

			// Token: 0x04000513 RID: 1299
			internal const double MinimumFreeDiskPercent = 15.0;

			// Token: 0x04000514 RID: 1300
			internal const int MaximumDumpDurationInSeconds = 0;

			// Token: 0x04000515 RID: 1301
			internal const string ServiceName = "Exchange";

			// Token: 0x04000516 RID: 1302
			internal const bool Enabled = true;

			// Token: 0x04000517 RID: 1303
			private static string dumpPath = string.Empty;
		}
	}
}
