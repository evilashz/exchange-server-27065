using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000003 RID: 3
	internal sealed class DiagnosticsService : ExServiceBase
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000024B4 File Offset: 0x000006B4
		static DiagnosticsService()
		{
			object obj = null;
			if (CommonUtils.TryGetRegistryValue(DiagnosticsService.DiagnosticsRegistryKey, "LogFolderPath", null, out obj))
			{
				DiagnosticsService.serviceLogFolderPath = obj.ToString();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000024EC File Offset: 0x000006EC
		public DiagnosticsService()
		{
			base.ServiceName = "MSExchangeDiagnostics";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
			base.CanShutdown = false;
			this.retentionAgents = new List<RetentionAgent>();
			this.executablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			if (!(TriggerHandler.Instance is EventLogger))
			{
				string configString = Configuration.GetConfigString("EventLogger", "Microsoft.Exchange.Diagnostics.Service.Common.EventLogger, Microsoft.Exchange.Diagnostics.Service.Common");
				TriggerHandler.Instance = this.CreateObject<EventLogger>(configString);
			}
			PerfLogCounterTrigger.IsDatacenter = DiagnosticsConfiguration.GetIsDatacenterMode();
			this.uploadSql = true;
			if (DiagnosticsService.serviceLogFolderPath == null)
			{
				this.uploadSql = false;
				string text = Path.Combine(ExchangeSetupContext.LoggingPath, "Diagnostics");
				Logger.LogWarningMessage("Could not retrieve the LogFolderPath from the registry, defaulting to {0}.", new object[]
				{
					text
				});
				DiagnosticsService.serviceLogFolderPath = text;
			}
			object obj;
			if (CommonUtils.TryGetRegistryValue(DiagnosticsService.DiagnosticsRegistryKey, "UploadSql", null, out obj))
			{
				this.uploadSql = Convert.ToBoolean(obj.ToString());
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000025DE File Offset: 0x000007DE
		public static string ServiceLogFolderPath
		{
			get
			{
				return DiagnosticsService.serviceLogFolderPath;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000025E5 File Offset: 0x000007E5
		private static bool IsRunningAsService
		{
			get
			{
				return !Environment.UserInteractive;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000025F0 File Offset: 0x000007F0
		public static void Main(string[] args)
		{
			ExWatson.Register();
			using (DiagnosticsService diagnosticsService = new DiagnosticsService())
			{
				if (DiagnosticsService.IsRunningAsService)
				{
					ServiceBase.Run(diagnosticsService);
				}
				else
				{
					ExServiceBase.RunAsConsole(diagnosticsService);
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000263C File Offset: 0x0000083C
		internal static bool DriveLocked(string diagnosticsRootDrive)
		{
			string arg = diagnosticsRootDrive.TrimEnd(new char[]
			{
				'\\'
			});
			bool result = false;
			string queryString = string.Format("SELECT ProtectionStatus FROM Win32_EncryptableVolume WHERE DriveLetter = '{0}'", arg);
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2\\Security\\MicrosoftVolumeEncryption", queryString))
				{
					string text = "Query returned no value";
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								using (ManagementBaseObject managementBaseObject = enumerator.Current)
								{
									text = managementBaseObject["ProtectionStatus"].ToString();
									if ("2".Equals(text, StringComparison.Ordinal))
									{
										result = true;
									}
								}
							}
						}
					}
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_BitlockerState, new object[]
					{
						diagnosticsRootDrive,
						text
					});
				}
			}
			catch (Exception arg2)
			{
				string text2 = string.Format("{0}{1}{0}{2}", Environment.NewLine, "Exception", arg2);
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_BitlockerStateDetectionError, new object[]
				{
					diagnosticsRootDrive,
					text2
				});
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000027A0 File Offset: 0x000009A0
		protected override void OnStartInternal(string[] args)
		{
			this.diagnosticsRootDirectory = DiagnosticsService.ServiceLogFolderPath;
			bool configBool = Configuration.GetConfigBool("DriveLockCheckEnabled", true);
			if (configBool)
			{
				TimeSpan configTimeSpan = Configuration.GetConfigTimeSpan("DriveLockCheckInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(10.0));
				TimeSpan configTimeSpan2 = Configuration.GetConfigTimeSpan("DriveLockMaxDuration", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(240.0));
				DateTime t = DateTime.UtcNow + configTimeSpan2;
				string pathRoot = Path.GetPathRoot(this.diagnosticsRootDirectory);
				while (DiagnosticsService.DriveLocked(pathRoot) && DateTime.UtcNow < t)
				{
					Thread.Sleep(configTimeSpan);
				}
			}
			string text = Path.Combine(this.diagnosticsRootDirectory, "ServiceLogs");
			try
			{
				Directory.CreateDirectory(text);
				Log.Instance = new ExFileLog(text);
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ServiceStarting, new object[0]);
				Logger.LogInformationMessage("Service starting.", new object[0]);
			}
			catch (Exception ex)
			{
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_DirectoryCreationException, new object[]
				{
					text,
					ex
				});
				base.GracefullyAbortStartup();
			}
			LoadEdsPerformanceCounters.RegisterCounters(this.executablePath);
			try
			{
				string configString = Configuration.GetConfigString("ConfigurationOverrides", "Microsoft.Exchange.Diagnostics.Service.Common.ConfigurationOverrides, Microsoft.Exchange.Diagnostics.Service.Common");
				this.configurationOverrides = this.CreateObject<ConfigurationOverrides>(configString);
				this.configurationOverrides.Refresh();
				this.configuration = new DiagnosticsConfiguration();
				this.SynchronizeAndLogConfiguration();
			}
			catch (Exception ex2)
			{
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ConfigurationExceptionOnStartup, new object[]
				{
					ex2.ToString()
				});
				Logger.LogErrorMessage("Unable to load and synchronize configuration, aborting service start. Exception: {0}", new object[]
				{
					ex2
				});
				base.GracefullyAbortStartup();
			}
			string text2 = Path.Combine(this.diagnosticsRootDirectory, "Watermarks");
			try
			{
				Directory.CreateDirectory(text2);
			}
			catch (Exception ex3)
			{
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_DirectoryCreationException, new object[]
				{
					text2,
					ex3
				});
				Logger.LogErrorMessage("Unable to create watermarks directory '{0}'. Exception: {1}", new object[]
				{
					text2,
					ex3
				});
				base.GracefullyAbortStartup();
			}
			string text3 = Path.Combine(this.diagnosticsRootDirectory, "AnalyzerLogs");
			ServiceConfiguration serviceConfiguration = this.configuration.ServiceConfiguration;
			bool isDatacenterMode = DiagnosticsConfiguration.GetIsDatacenterMode();
			foreach (object obj in serviceConfiguration.Directories)
			{
				ServiceConfiguration.DirectoryConfigurationElement configuredDirectory = (ServiceConfiguration.DirectoryConfigurationElement)obj;
				this.AddRetentionDirectory(configuredDirectory, isDatacenterMode);
			}
			string text4 = Environment.MachineName + "_ServiceOutputStream";
			this.outputStreams = new Dictionary<string, OutputStream>();
			this.outputStreams.Add("null", new NullOutputStream(null));
			OutputStream outputStream = new FileChunkOutputStream(text4, text3);
			this.outputStreams.Add("default", outputStream);
			int configInt = Configuration.GetConfigInt("SqlOutputStreamMaxBufferSize", 1, 1000, 1000);
			TimeSpan configTimeSpan3 = Configuration.GetConfigTimeSpan("SqlOutputStreamFlushInterval", TimeSpan.FromSeconds(15.0), TimeSpan.FromMinutes(5.0), TimeSpan.FromMinutes(5.0));
			int configInt2 = Configuration.GetConfigInt("SqlOutputStreamPerformanceDataUploadTimeoutSeconds", 30, 3600, 60);
			TimeSpan configTimeSpan4 = Configuration.GetConfigTimeSpan("SqlOutputStreamLogEventAfterUploadRetryFor", TimeSpan.FromMinutes(1.0), TimeSpan.FromHours(5.0), TimeSpan.FromHours(1.0));
			SqlOutputStream.Configuration configuration = new SqlOutputStream.Configuration(configInt, configTimeSpan3, configTimeSpan4, configInt2);
			OutputStream value = new SqlOutputStream(outputStream, this.uploadSql, Environment.MachineName + "_ServiceUploaderStream", Path.Combine(text3, text4), configuration);
			this.outputStreams.Add("sql", value);
			JobSection jobSection = this.configuration.JobSection;
			this.jobManager = new JobManager(jobSection.Jobs, this.outputStreams, text2);
			this.jobManager.CreateAndStartJobsAsync();
			Logger.LogInformationMessage("Service started.", new object[0]);
			Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ServiceStarted, new object[0]);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002BF8 File Offset: 0x00000DF8
		protected override void OnStopInternal()
		{
			Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ServiceStopping, new object[0]);
			Logger.LogInformationMessage("Service shutting down.", new object[0]);
			if (this.configurationOverrides != null)
			{
				this.configurationOverrides.Dispose();
				this.configurationOverrides = null;
			}
			if (this.jobManager != null)
			{
				this.jobManager.StopJobs();
			}
			if (this.outputStreams != null)
			{
				foreach (OutputStream outputStream in this.outputStreams.Values)
				{
					outputStream.Dispose();
				}
				this.outputStreams.Clear();
			}
			if (this.retentionAgents != null)
			{
				foreach (RetentionAgent retentionAgent in this.retentionAgents)
				{
					retentionAgent.Dispose();
				}
			}
			this.retentionAgents.Clear();
			if (this.jobManager != null)
			{
				this.jobManager.Dispose();
				this.jobManager = null;
			}
			Logger.LogInformationMessage("Service shutdown.", new object[0]);
			Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ServiceStopped, new object[0]);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002D40 File Offset: 0x00000F40
		private T CreateObject<T>(string objectType) where T : new()
		{
			try
			{
				Logger.LogInformationMessage("Creating object of type '{0}'", new object[]
				{
					objectType
				});
				Type type = Type.GetType(objectType, true, true);
				ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
				return (T)((object)constructor.Invoke(null));
			}
			catch (Exception ex)
			{
				Logger.LogErrorMessage("Unable to create object of type '{0}'. Exception: {1}", new object[]
				{
					objectType,
					ex
				});
			}
			if (default(T) != null)
			{
				return default(T);
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002DE0 File Offset: 0x00000FE0
		private void AddRetentionDirectory(ServiceConfiguration.DirectoryConfigurationElement configuredDirectory, bool datacenter)
		{
			string text = Path.Combine(this.diagnosticsRootDirectory, configuredDirectory.Name);
			try
			{
				int maxSize = configuredDirectory.MaxSize;
				int maxSizeDatacenter = configuredDirectory.MaxSizeDatacenter;
				int num = (datacenter && maxSizeDatacenter != 0) ? maxSizeDatacenter : maxSize;
				TimeSpan maxAge = configuredDirectory.MaxAge;
				TimeSpan checkInterval = configuredDirectory.CheckInterval;
				bool logDataLoss = configuredDirectory.LogDataLoss;
				string agent = configuredDirectory.Agent;
				Type[] types = new Type[]
				{
					typeof(string),
					typeof(TimeSpan),
					typeof(int),
					typeof(TimeSpan),
					typeof(bool)
				};
				object[] parameters = new object[]
				{
					text,
					maxAge,
					num,
					checkInterval,
					logDataLoss
				};
				Type type = string.IsNullOrEmpty(agent) ? typeof(RetentionAgent) : Type.GetType(agent, true, true);
				ConstructorInfo constructor = type.GetConstructor(types);
				RetentionAgent item = (RetentionAgent)constructor.Invoke(parameters);
				this.retentionAgents.Add(item);
			}
			catch (Exception ex)
			{
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_DirectoryCreationException, new object[]
				{
					text,
					ex
				});
				Logger.LogErrorMessage("Unable to create the log directory '{0}' or start the retention agent. Exception: {1}", new object[]
				{
					text,
					ex
				});
				base.GracefullyAbortStartup();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002F78 File Offset: 0x00001178
		private void SynchronizeAndLogConfiguration()
		{
			Logger.LogInformationMessage("The active service configurations are:", new object[0]);
			Logger.LogInformationMessage("Product Version: {0}", new object[]
			{
				"v15"
			});
			Dictionary<string, string> configStrings = Configuration.GetConfigStrings(string.Empty);
			foreach (KeyValuePair<string, string> keyValuePair in configStrings)
			{
				Logger.LogInformationMessage("'{0}' = '{1}'", new object[]
				{
					keyValuePair.Key,
					keyValuePair.Value
				});
			}
		}

		// Token: 0x04000008 RID: 8
		internal const string DiagnosticsLogFolderRegistryValue = "LogFolderPath";

		// Token: 0x04000009 RID: 9
		private const string DiagnosticsServiceName = "MSExchangeDiagnostics";

		// Token: 0x0400000A RID: 10
		internal static readonly string DiagnosticsRegistryKey = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\ExchangeServer\\v15\\Diagnostics";

		// Token: 0x0400000B RID: 11
		private static string serviceLogFolderPath;

		// Token: 0x0400000C RID: 12
		private readonly bool uploadSql;

		// Token: 0x0400000D RID: 13
		private readonly string executablePath;

		// Token: 0x0400000E RID: 14
		private readonly List<RetentionAgent> retentionAgents;

		// Token: 0x0400000F RID: 15
		private DiagnosticsConfiguration configuration;

		// Token: 0x04000010 RID: 16
		private ConfigurationOverrides configurationOverrides;

		// Token: 0x04000011 RID: 17
		private string diagnosticsRootDirectory;

		// Token: 0x04000012 RID: 18
		private Dictionary<string, OutputStream> outputStreams;

		// Token: 0x04000013 RID: 19
		private JobManager jobManager;
	}
}
