using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000017 RID: 23
	public class RestartServiceResponder : ResponderWorkItem
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003966 File Offset: 0x00001B66
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000396E File Offset: 0x00001B6E
		public string WindowsServiceName { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003977 File Offset: 0x00001B77
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000397F File Offset: 0x00001B7F
		public TimeSpan StopTimeout { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003988 File Offset: 0x00001B88
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003990 File Offset: 0x00001B90
		public TimeSpan StartTimeout { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003999 File Offset: 0x00001B99
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000039A1 File Offset: 0x00001BA1
		public TimeSpan StartDelay { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000039AA File Offset: 0x00001BAA
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000039B2 File Offset: 0x00001BB2
		public bool IsMasterAndWorker { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000039BB File Offset: 0x00001BBB
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000039C3 File Offset: 0x00001BC3
		public ProcessKillMode MasterAndWorkerKillMode { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000039CC File Offset: 0x00001BCC
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000039D4 File Offset: 0x00001BD4
		public CommonDumpParameters DumpArgs { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000039DD File Offset: 0x00001BDD
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000039E5 File Offset: 0x00001BE5
		public string AdditionalProcessNameToKill { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000039EE File Offset: 0x00001BEE
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000039F6 File Offset: 0x00001BF6
		public bool RestartEnabled { get; set; }

		// Token: 0x060000A1 RID: 161 RVA: 0x00003A00 File Offset: 0x00001C00
		public static ResponderDefinition CreateDefinition(string responderName, string monitorName, string windowsServiceName, ServiceHealthStatus responderTargetState, int serviceStopTimeoutInSeconds = 15, int serviceStartTimeoutInSeconds = 120, int serviceStartDelayInSeconds = 0, bool isMasterWorker = false, DumpMode dumpOnRestartMode = DumpMode.None, string dumpPath = null, double minimumFreeDiskPercent = 15.0, int maximumDumpDurationInSeconds = 0, string serviceName = "Exchange", string additionalProcessNameToKill = null, bool restartEnabled = true, bool enabled = true, string throttleGroupName = null, bool dumpIgnoreRegistryLimit = false)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = RestartServiceResponder.AssemblyPath;
			responderDefinition.TypeName = RestartServiceResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = monitorName;
			responderDefinition.RecurrenceIntervalSeconds = ((dumpOnRestartMode == DumpMode.FullDump) ? 1800 : 300);
			responderDefinition.TimeoutSeconds = ((dumpOnRestartMode == DumpMode.FullDump) ? 1740 : 240);
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = enabled;
			if (string.IsNullOrEmpty(dumpPath))
			{
				dumpPath = RestartServiceResponder.DefaultValues.DumpPath;
			}
			if (string.IsNullOrEmpty(additionalProcessNameToKill))
			{
				additionalProcessNameToKill = RestartServiceResponder.DefaultValues.AdditionalProcessNameToKill;
			}
			responderDefinition.Attributes["WindowsServiceName"] = windowsServiceName;
			responderDefinition.Attributes["ServiceStopTimeout"] = TimeSpan.FromSeconds((double)serviceStopTimeoutInSeconds).ToString();
			responderDefinition.Attributes["ServiceStartTimeout"] = TimeSpan.FromSeconds((double)serviceStartTimeoutInSeconds).ToString();
			responderDefinition.Attributes["ServiceStartDelay"] = TimeSpan.FromSeconds((double)serviceStartDelayInSeconds).ToString();
			responderDefinition.Attributes["IsMasterAndWorker"] = isMasterWorker.ToString();
			responderDefinition.Attributes["DumpOnRestart"] = dumpOnRestartMode.ToString();
			responderDefinition.Attributes["DumpPath"] = dumpPath;
			responderDefinition.Attributes["DumpIgnoreRegistryLimit"] = dumpIgnoreRegistryLimit.ToString();
			responderDefinition.Attributes["MinimumFreeDiskPercent"] = minimumFreeDiskPercent.ToString();
			responderDefinition.Attributes["MaximumDumpDurationInSeconds"] = maximumDumpDurationInSeconds.ToString();
			responderDefinition.Attributes["AdditionalProcessNameToKill"] = additionalProcessNameToKill;
			responderDefinition.Attributes["RestartEnabled"] = restartEnabled.ToString();
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.RestartService, windowsServiceName, null);
			return responderDefinition;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003BF8 File Offset: 0x00001DF8
		protected void InitializeServiceAttributes(AttributeHelper attributeHelper)
		{
			this.WindowsServiceName = attributeHelper.GetString("WindowsServiceName", true, null);
			this.StopTimeout = attributeHelper.GetTimeSpan("ServiceStopTimeout", false, TimeSpan.FromSeconds(15.0), null, null);
			this.StartTimeout = attributeHelper.GetTimeSpan("ServiceStartTimeout", false, TimeSpan.FromSeconds(120.0), null, null);
			this.StartDelay = attributeHelper.GetTimeSpan("ServiceStartDelay", false, TimeSpan.FromSeconds(0.0), null, null);
			this.IsMasterAndWorker = attributeHelper.GetBool("IsMasterAndWorker", false, false);
			this.MasterAndWorkerKillMode = attributeHelper.GetEnum<ProcessKillMode>("MasterAndWorkerKillMode", false, this.IsMasterAndWorker ? ProcessKillMode.SelfAndChildren : ProcessKillMode.SelfOnly);
			this.AdditionalProcessNameToKill = attributeHelper.GetString("AdditionalProcessNameToKill", false, RestartServiceResponder.DefaultValues.AdditionalProcessNameToKill);
			this.RestartEnabled = attributeHelper.GetBool("RestartEnabled", false, true);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003D0C File Offset: 0x00001F0C
		protected void InitializeDumpAttributes(AttributeHelper attributeHelper)
		{
			this.DumpArgs = new CommonDumpParameters
			{
				Mode = attributeHelper.GetEnum<DumpMode>("DumpOnRestart", false, DumpMode.None),
				Path = attributeHelper.GetString("DumpPath", false, RestartServiceResponder.DefaultValues.DumpPath),
				MinimumFreeSpace = attributeHelper.GetDouble("MinimumFreeDiskPercent", false, 15.0, new double?(0.0), new double?(100.0)),
				MaximumDurationInSeconds = attributeHelper.GetInt("MaximumDumpDurationInSeconds", false, 0, null, null),
				IgnoreRegistryOverride = attributeHelper.GetBool("DumpIgnoreRegistryLimit", false, false)
			};
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003DC0 File Offset: 0x00001FC0
		protected virtual void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.InitializeServiceAttributes(attributeHelper);
			this.InitializeDumpAttributes(attributeHelper);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003DE8 File Offset: 0x00001FE8
		protected string InternalDumpProcess(Process process, string requester, CancellationToken cancellationToken)
		{
			ProcessDumpHelper processDumpHelper = new ProcessDumpHelper(this.DumpArgs, cancellationToken);
			return processDumpHelper.Generate(process, base.Definition.Name);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003E50 File Offset: 0x00002050
		protected void ThrottledDumpProcess(Process process, CancellationToken cancellationToken)
		{
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.WatsonDump, this.WindowsServiceName, this, false, cancellationToken, null);
			recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
			{
				startEntry.CustomArg1 = this.InternalDumpProcess(process, this.Definition.Name, cancellationToken);
			});
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004088 File Offset: 0x00002288
		protected void InternalRestartService(RecoveryActionEntry startEntry, CancellationToken cancellationToken)
		{
			Privilege.RunWithPrivilege("SeDebugPrivilege", true, delegate
			{
				bool flag = false;
				using (ServiceHelper serviceHelper = new ServiceHelper(this.WindowsServiceName, cancellationToken))
				{
					using (Process process = serviceHelper.GetProcess())
					{
						if (process != null)
						{
							int id = process.Id;
							if (this.DumpArgs.IsDumpRequested)
							{
								this.ThrottledDumpProcess(process, cancellationToken);
							}
							if (!this.RestartEnabled)
							{
								return;
							}
							Process[] array = null;
							if (!string.IsNullOrEmpty(this.AdditionalProcessNameToKill))
							{
								array = Process.GetProcessesByName(this.AdditionalProcessNameToKill);
							}
							flag = true;
							if (this.IsMasterAndWorker)
							{
								ProcessHelper.Kill(process, this.MasterAndWorkerKillMode, startEntry.InstanceId);
							}
							else
							{
								ProcessHelper.Kill(process, ProcessKillMode.SelfOnly, startEntry.InstanceId);
							}
							if (array != null)
							{
								foreach (Process process2 in array)
								{
									using (process2)
									{
										try
										{
											ProcessHelper.KillProcess(process2, true, startEntry.InstanceId);
										}
										catch (Win32Exception)
										{
										}
									}
								}
							}
							serviceHelper.WaitUntilProcessGoesAway(id, this.StopTimeout);
						}
					}
					if (flag)
					{
						serviceHelper.Sleep(this.StartDelay);
					}
					serviceHelper.Start();
					serviceHelper.WaitForStatus(ServiceControllerStatus.Running, this.StartTimeout);
				}
			});
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000040E4 File Offset: 0x000022E4
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.RestartService, this.WindowsServiceName, this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
			{
				this.InternalRestartService(startEntry, cancellationToken);
			});
		}

		// Token: 0x04000043 RID: 67
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000044 RID: 68
		private static readonly string TypeName = typeof(RestartServiceResponder).FullName;

		// Token: 0x02000018 RID: 24
		public static class AttributeNames
		{
			// Token: 0x0400004E RID: 78
			public const string WindowsServiceName = "WindowsServiceName";

			// Token: 0x0400004F RID: 79
			public const string ServiceStopTimeout = "ServiceStopTimeout";

			// Token: 0x04000050 RID: 80
			public const string ServiceStartTimeout = "ServiceStartTimeout";

			// Token: 0x04000051 RID: 81
			public const string ServiceStartDelay = "ServiceStartDelay";

			// Token: 0x04000052 RID: 82
			public const string IsMasterAndWorker = "IsMasterAndWorker";

			// Token: 0x04000053 RID: 83
			public const string MasterAndWorkerKillMode = "MasterAndWorkerKillMode";

			// Token: 0x04000054 RID: 84
			public const string DumpOnRestart = "DumpOnRestart";

			// Token: 0x04000055 RID: 85
			public const string DumpPath = "DumpPath";

			// Token: 0x04000056 RID: 86
			public const string DumpIgnoreRegistryLimit = "DumpIgnoreRegistryLimit";

			// Token: 0x04000057 RID: 87
			public const string MinimumFreeDiskPercent = "MinimumFreeDiskPercent";

			// Token: 0x04000058 RID: 88
			public const string MaximumDumpDurationInSeconds = "MaximumDumpDurationInSeconds";

			// Token: 0x04000059 RID: 89
			public const string AdditionalProcessNameToKill = "AdditionalProcessNameToKill";

			// Token: 0x0400005A RID: 90
			public const string RestartEnabled = "RestartEnabled";

			// Token: 0x0400005B RID: 91
			public const string ThrottleGroupName = "throttleGroupName";
		}

		// Token: 0x02000019 RID: 25
		public class DefaultValues
		{
			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000AB RID: 171 RVA: 0x00004160 File Offset: 0x00002360
			public static string DumpPath
			{
				get
				{
					if (string.IsNullOrEmpty(RestartServiceResponder.DefaultValues.dumpPath))
					{
						try
						{
							RestartServiceResponder.DefaultValues.dumpPath = Path.Combine(ExchangeSetupContext.InstallPath, "Dumps");
						}
						catch (SetupVersionInformationCorruptException)
						{
							RestartServiceResponder.DefaultValues.dumpPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
						}
					}
					return RestartServiceResponder.DefaultValues.dumpPath;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000AC RID: 172 RVA: 0x000041B4 File Offset: 0x000023B4
			internal static string AdditionalProcessNameToKill
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x0400005C RID: 92
			public const int ServiceStopTimeoutInSeconds = 15;

			// Token: 0x0400005D RID: 93
			public const int ServiceStartTimeoutInSeconds = 120;

			// Token: 0x0400005E RID: 94
			public const int ServiceStartDelayInSeconds = 0;

			// Token: 0x0400005F RID: 95
			public const DumpMode DumpOnRestart = DumpMode.None;

			// Token: 0x04000060 RID: 96
			public const bool DumpIgnoreRegistryLimit = false;

			// Token: 0x04000061 RID: 97
			public const double MinimumFreeDiskPercent = 15.0;

			// Token: 0x04000062 RID: 98
			public const int MaximumDumpDurationInSeconds = 0;

			// Token: 0x04000063 RID: 99
			public const bool IsMasterWorker = false;

			// Token: 0x04000064 RID: 100
			public const string ServiceName = "Exchange";

			// Token: 0x04000065 RID: 101
			internal const bool RestartEnabled = true;

			// Token: 0x04000066 RID: 102
			internal const bool Enabled = true;

			// Token: 0x04000067 RID: 103
			private static string dumpPath = string.Empty;
		}
	}
}
