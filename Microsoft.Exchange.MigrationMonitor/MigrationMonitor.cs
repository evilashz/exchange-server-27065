using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.ServiceHost;
using Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationServiceMonitor;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200001F RID: 31
	internal class MigrationMonitor : Servicelet
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00006223 File Offset: 0x00004423
		public MigrationMonitor()
		{
			this.InitInstallAndLogPath();
			MigrationMonitor.SqlHelper = new MigMonSqlHelper();
			this.InitKnownStringIdMap();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00006241 File Offset: 0x00004441
		internal static string ComputerName
		{
			get
			{
				return CommonUtils.LocalComputerName;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006248 File Offset: 0x00004448
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000624F File Offset: 0x0000444F
		internal static Dictionary<KnownStringType, Dictionary<string, int?>> KnownStringIdMap { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00006257 File Offset: 0x00004457
		internal static MRSRequestCsvSchema MRSRequestCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.mrsRequestCsvSchemaInstance;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000625E File Offset: 0x0000445E
		internal static MRSFailureCsvSchema MRSFailureCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.mrsFailureCsvSchemaInstance;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00006265 File Offset: 0x00004465
		internal static MRSBadItemCsvSchema MRSBadItemCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.mrsBadItemCsvSchemaInstance;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000626C File Offset: 0x0000446C
		internal static MrsSessionStatisticsCsvSchema MrsSessionStatisticsCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.mrsSessionStatisticsCsvSchemaInstance;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00006273 File Offset: 0x00004473
		internal static MigrationServiceJobItemCsvSchema MigrationServiceJobItemCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.migrationServiceJobItemCsvSchemaInstance;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000627A File Offset: 0x0000447A
		internal static MigrationServiceJobCsvSchema MigrationServiceJobCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.migrationServiceJobCsvSchemaInstance;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00006281 File Offset: 0x00004481
		internal static MigrationServiceEndpointCsvSchema MigrationServiceEndpointCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.migrationServiceEndpointCsvSchemaInstance;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006288 File Offset: 0x00004488
		internal static DatabaseInfoCsvSchema DatabaseInfoCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.databaseInfoCsvSchemaInstance;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000628F File Offset: 0x0000448F
		internal static MailboxStatsCsvSchema MailboxStatsCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.mailboxStatsCsvSchemaInstance;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006296 File Offset: 0x00004496
		internal static MRSWorkloadAvailabilityCsvSchema MRSWorkloadAvailabilityCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.mrsWorkloadAvailabilityCsvSchemaInstance;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000629D File Offset: 0x0000449D
		internal static QueueMRSWorkStatsCsvSchema QueueMRSWorkStatsCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.queueMRSWorkStatsCsvSchemaInstance;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000062A4 File Offset: 0x000044A4
		internal static WLMResourceStatsCsvSchema WLMResourceStatsCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.wlmResoureStatsCsvSchemaInstance;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000062AB File Offset: 0x000044AB
		internal static JobPickupResultsCsvSchema JobPickupResultsCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.jobPickupResultsCsvSchemaInstance;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000062B2 File Offset: 0x000044B2
		internal static DrumTestingResultCsvSchema DrumTestingResultCsvSchemaInstance
		{
			get
			{
				return MigrationMonitor.drumTestingResultCsvSchemaInstance;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000062B9 File Offset: 0x000044B9
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000062C0 File Offset: 0x000044C0
		internal static AnchorContext MigrationMonitorContext { get; private set; } = new AnchorContext("MigrationMonitor", OrganizationCapability.TenantUpgrade, MigrationMonitor.CreateConfigSchema());

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000062C8 File Offset: 0x000044C8
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000062CF File Offset: 0x000044CF
		internal static string ExchangeInstallPath { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000062D7 File Offset: 0x000044D7
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000062DE File Offset: 0x000044DE
		internal static string MigrationMonitorLogPath { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000062E6 File Offset: 0x000044E6
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x000062ED File Offset: 0x000044ED
		internal static MigMonSqlHelper SqlHelper { get; private set; }

		// Token: 0x060000F2 RID: 242 RVA: 0x000062F8 File Offset: 0x000044F8
		public override void Work()
		{
			using (new ExchangeDiagnostics(MigrationMonitor.MigrationMonitorContext.Config))
			{
				TimeSpan config;
				do
				{
					if (this.ShouldRun())
					{
						config = MigrationMonitor.MigrationMonitorContext.Config.GetConfig<TimeSpan>("ActiveRunDelay");
						this.HandleKnownMigrationMonitorWorkExceptions(new Action(this.ProcessMRSLogs), "Processing MRS logs", new Action(MigrationMonitor.SqlHelper.ClearConnectionPool), "Clearing SQL connection pool");
						this.HandleKnownMigrationMonitorWorkExceptions(new Action(this.ProcessDCInfoLogs), "Processing DC information logs", new Action(MigrationMonitor.SqlHelper.ClearConnectionPool), "Clearing SQL connection pool");
					}
					else
					{
						config = MigrationMonitor.MigrationMonitorContext.Config.GetConfig<TimeSpan>("IdleRunDelay");
						MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "This server will not run, sleeping for {0}", new object[]
						{
							config
						});
					}
				}
				while (!base.StopEvent.WaitOne(config));
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000063F4 File Offset: 0x000045F4
		internal static AnchorConfig CreateConfigSchema()
		{
			return new MigrationMonitor.MigrationMonitorConfig();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000063FB File Offset: 0x000045FB
		internal void SwitchConnectionStrings(MigMonSqlHelper.MigMonDatabaseSelection db = MigMonSqlHelper.MigMonDatabaseSelection.PrimaryMRSDatabase)
		{
			this.ResetCachedIds();
			MigrationMonitor.SqlHelper.GetConnectionStringFromConfig(db);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006410 File Offset: 0x00004610
		internal void ProcessMRSLogs()
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Starting to process MRS related logs.", new object[0]);
			List<BaseLogProcessor> list = new List<BaseLogProcessor>();
			MigrationMonitor.AddLogProcessorIfEnabled<MRSRequestLogProcessor>(list, "IsMRSLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<MRSFailureLogProcessor>(list, "IsMRSLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<MRSBadItemLogProcessor>(list, "IsMRSLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<MrsSessionStatisticsLogProcessor>(list, "IsMrsSessionStatisticsLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<MigrationServiceEndpointLogProcessor>(list, "IsMigServiceStatsLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<MigrationServiceJobLogProcessor>(list, "IsMigServiceStatsLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<MigrationServiceJobItemLogProcessor>(list, "IsMigServiceStatsLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<QueueMRSWorkStatsLogProcessor>(list, "IsQueueMRSWorkStatsLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<JobPickupResultsLogProcessor>(list, "IsJobPickupResultsLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<DrumTestingResultLogProcessor>(list, "IsDrumTestingResultLogProcessorEnabled");
			this.SwitchConnectionStrings(MigMonSqlHelper.MigMonDatabaseSelection.PrimaryMRSDatabase);
			this.RegisterServer();
			foreach (BaseLogProcessor baseLogProcessor in list)
			{
				baseLogProcessor.ProcessLogs();
			}
			this.PublishServerHealthStatus();
			this.UpdateHeartBeat();
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Finished processing MRS related logs.", new object[0]);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006520 File Offset: 0x00004720
		internal void ProcessDCInfoLogs()
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Starting to process DC info related logs.", new object[0]);
			List<BaseLogProcessor> list = new List<BaseLogProcessor>();
			MigrationMonitor.AddLogProcessorIfEnabled<DatabaseInfoLogProcessor>(list, "IsDatabaseInfoLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<MailboxStatsLogProcessor>(list, "IsMailboxStatsLogProcessorEnabled");
			MigrationMonitor.AddLogProcessorIfEnabled<MRSWorkloadAvailabilityLogProcessor>(list, "IsMRSWorkloadAvailabilityLogProcessorEnabled");
			this.SwitchConnectionStrings(MigMonSqlHelper.MigMonDatabaseSelection.DCInfoDatabase);
			this.RegisterServer();
			foreach (BaseLogProcessor baseLogProcessor in list)
			{
				baseLogProcessor.ProcessLogs();
			}
			this.UpdateHeartBeat();
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Finished processing DC info related logs.", new object[0]);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000065E0 File Offset: 0x000047E0
		private static void AddLogProcessorIfEnabled<T>(List<BaseLogProcessor> logProcessorList, string configKeyName) where T : BaseLogProcessor, new()
		{
			if (!MigrationMonitor.MigrationMonitorContext.Config.GetConfig<bool>(configKeyName))
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "{0} is disabled by config.", new object[]
				{
					typeof(T).Name
				});
				return;
			}
			logProcessorList.Add(Activator.CreateInstance<T>());
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006640 File Offset: 0x00004840
		private void HandleKnownMigrationMonitorWorkExceptions(Action action, string actionHint, Action postAction, string postActionHint)
		{
			try
			{
				action();
			}
			catch (SqlServerTimeoutException)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, "SQL server timed out. {0} :: skipping cycle.", new object[]
				{
					actionHint
				});
			}
			catch (LogFileReadException)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, "Error reading log file. {0} :: skipping cycle.", new object[]
				{
					actionHint
				});
			}
			catch (SqlServerUnreachableException exception)
			{
				this.ResetCachedIds();
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, exception, "{0} :: Unable to reach SQL Server. Skipping cycle.", new object[]
				{
					actionHint
				});
			}
			catch (LocalizedException exception2)
			{
				ExWatson.SendReport(exception2, ReportOptions.None, null);
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, exception2, "Error encountered during processing. {0} :: skipping cycle.", new object[]
				{
					actionHint
				});
			}
			finally
			{
				try
				{
					postAction();
				}
				catch (Exception exception3)
				{
					ExWatson.SendReport(exception3, ReportOptions.None, null);
					MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, exception3, "Error encountered during {0} post action execution", new object[]
					{
						postActionHint
					});
				}
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000678C File Offset: 0x0000498C
		private void PublishServerHealthStatus()
		{
			if (DateTime.UtcNow <= this.nextServerHealthRefreshTime)
			{
				return;
			}
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Trying to update server health info.", new object[0]);
			try
			{
				MigMonHealthMonitor.PublishServerHealthStatus();
			}
			catch (HealthStatusPublishFailureException exception)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, exception, "Failed to publish server health info. Will try again next cycle.", new object[0]);
				return;
			}
			int num = int.MaxValue;
			try
			{
				num = Convert.ToInt32(MigrationMonitor.MigrationMonitorContext.Config.GetConfig<TimeSpan>("HealthStatusPublishInterval").TotalMinutes);
			}
			catch (OverflowException ex)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "avg interval too large, defaulting to intmax", new object[]
				{
					ex
				});
			}
			int num2 = num / 4;
			Random random = new Random();
			int num3 = random.Next(num - num2, num + num2);
			this.nextServerHealthRefreshTime = DateTime.UtcNow.AddMinutes((double)num3);
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Successfully published server health info. Next update at {0}", new object[]
			{
				this.nextServerHealthRefreshTime.ToString()
			});
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000068C4 File Offset: 0x00004AC4
		private void InitInstallAndLogPath()
		{
			MigrationMonitor.ExchangeInstallPath = CommonUtils.GetExchangeInstallPath();
			MigrationMonitor.MigrationMonitorLogPath = Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MigrationMonitorLogs");
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000068E4 File Offset: 0x00004AE4
		private void UpdateHeartBeat()
		{
			MigrationMonitor.SqlHelper.SetHeartBeatTS();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000068F0 File Offset: 0x00004AF0
		private void RegisterServer()
		{
			string text = NativeHelpers.GetForestName();
			int num = text.IndexOf('.');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Registering Current Server: {0}   Forest: {1}     Site: {2}", new object[]
			{
				MigrationMonitor.ComputerName,
				text,
				NativeHelpers.GetSiteName(false)
			});
			MigrationMonitor.SqlHelper.RegisterLoggingServer(MigrationMonitor.ComputerName, text, NativeHelpers.GetSiteName(false), SysInfoHelper.GetCPUCores(true), SysInfoHelper.GetDiskSize(true));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006970 File Offset: 0x00004B70
		private bool ShouldRun()
		{
			if (!MigrationMonitor.MigrationMonitorContext.Config.GetConfig<bool>("IsEnabled"))
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "MigrationMonitor is not enabled on this server", new object[0]);
				return false;
			}
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Verbose, "Migration Monitor is running on System.Environment.MachineName: {0}", new object[]
			{
				Environment.MachineName
			});
			return true;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000069D8 File Offset: 0x00004BD8
		private void InitKnownStringIdMap()
		{
			MigrationMonitor.KnownStringIdMap = new Dictionary<KnownStringType, Dictionary<string, int?>>();
			foreach (object obj in Enum.GetValues(typeof(KnownStringType)))
			{
				KnownStringType key = (KnownStringType)obj;
				MigrationMonitor.KnownStringIdMap.Add(key, new Dictionary<string, int?>());
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006A50 File Offset: 0x00004C50
		private void ResetCachedIds()
		{
			foreach (KnownStringType key in MigrationMonitor.KnownStringIdMap.Keys.ToArray<KnownStringType>())
			{
				MigrationMonitor.KnownStringIdMap[key] = new Dictionary<string, int?>();
			}
		}

		// Token: 0x040000AB RID: 171
		private const string KeyNameAvgHealthStatusPublishInterval = "HealthStatusPublishInterval";

		// Token: 0x040000AC RID: 172
		private const string AppName = "MigrationMonitor";

		// Token: 0x040000AD RID: 173
		private static MRSRequestCsvSchema mrsRequestCsvSchemaInstance = new MRSRequestCsvSchema();

		// Token: 0x040000AE RID: 174
		private static MRSFailureCsvSchema mrsFailureCsvSchemaInstance = new MRSFailureCsvSchema();

		// Token: 0x040000AF RID: 175
		private static MRSBadItemCsvSchema mrsBadItemCsvSchemaInstance = new MRSBadItemCsvSchema();

		// Token: 0x040000B0 RID: 176
		private static MrsSessionStatisticsCsvSchema mrsSessionStatisticsCsvSchemaInstance = new MrsSessionStatisticsCsvSchema();

		// Token: 0x040000B1 RID: 177
		private static MigrationServiceJobItemCsvSchema migrationServiceJobItemCsvSchemaInstance = new MigrationServiceJobItemCsvSchema();

		// Token: 0x040000B2 RID: 178
		private static MigrationServiceJobCsvSchema migrationServiceJobCsvSchemaInstance = new MigrationServiceJobCsvSchema();

		// Token: 0x040000B3 RID: 179
		private static MigrationServiceEndpointCsvSchema migrationServiceEndpointCsvSchemaInstance = new MigrationServiceEndpointCsvSchema();

		// Token: 0x040000B4 RID: 180
		private static DatabaseInfoCsvSchema databaseInfoCsvSchemaInstance = new DatabaseInfoCsvSchema();

		// Token: 0x040000B5 RID: 181
		private static MailboxStatsCsvSchema mailboxStatsCsvSchemaInstance = new MailboxStatsCsvSchema();

		// Token: 0x040000B6 RID: 182
		private static MRSWorkloadAvailabilityCsvSchema mrsWorkloadAvailabilityCsvSchemaInstance = new MRSWorkloadAvailabilityCsvSchema();

		// Token: 0x040000B7 RID: 183
		private static QueueMRSWorkStatsCsvSchema queueMRSWorkStatsCsvSchemaInstance = new QueueMRSWorkStatsCsvSchema();

		// Token: 0x040000B8 RID: 184
		private static WLMResourceStatsCsvSchema wlmResoureStatsCsvSchemaInstance = new WLMResourceStatsCsvSchema();

		// Token: 0x040000B9 RID: 185
		private static JobPickupResultsCsvSchema jobPickupResultsCsvSchemaInstance = new JobPickupResultsCsvSchema();

		// Token: 0x040000BA RID: 186
		private static DrumTestingResultCsvSchema drumTestingResultCsvSchemaInstance = new DrumTestingResultCsvSchema();

		// Token: 0x040000BB RID: 187
		private DateTime nextServerHealthRefreshTime;

		// Token: 0x02000020 RID: 32
		protected class MigrationMonitorConfig : AnchorConfig
		{
			// Token: 0x06000100 RID: 256 RVA: 0x00006A90 File Offset: 0x00004C90
			internal MigrationMonitorConfig() : base("MigrationMonitor")
			{
				bool value = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Mrs.MigrationMonitor.Enabled;
				string value2 = "Server=tcp:l0tqt6mh64.database.windows.net,1433;Database=exo-mig-mon;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
				string value3 = "Server=tcp:caf5176sig.database.windows.net,1433;Database=exo-mig-mon-dcinfo;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
				if (CommonUtils.LocalComputerName.ToLower().Contains("eurprd"))
				{
					value2 = "Server=tcp:apu9k5pu1h.database.windows.net,1433;Database=exo-mig-mon-eur;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
				}
				if (CommonUtils.LocalComputerName.ToLower().Contains("apcprd"))
				{
					value2 = "Server=tcp:fbwrmjzyoo.database.windows.net,1433;Database=exo-mig-mon-apc;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
				}
				if (CommonUtils.LocalComputerName.ToLower().Contains("chnpr"))
				{
					value2 = "Server=tcp:odq4wwqizo.database.chinacloudapi.cn,1433;Database=exo-mig-mon-cn;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
					value3 = "Server=tcp:odq4wwqizo.database.chinacloudapi.cn,1433;Database=exo-mig-mon-dcinfo-cn;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
				}
				if (ExEnvironment.IsTestDomain)
				{
					value = false;
					value2 = "Server=tcp:wawqf20dco.database.windows.net,1433;Database=exo-mig-mon-test;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
					value3 = "Server=tcp:wawqf20dco.database.windows.net,1433;Database=exo-mig-mon-dcinfo-test;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
				}
				base.UpdateConfig<string>("ConnectionStringPrimary", value2);
				base.UpdateConfig<string>("ConnectionStringDCInfo", value3);
				base.UpdateConfig<bool>("IsEnabled", value);
				base.UpdateConfig<TimeSpan>("IdleRunDelay", TimeSpan.FromMinutes(15.0));
				base.UpdateConfig<TimeSpan>("ActiveRunDelay", TimeSpan.FromMinutes(15.0));
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00006B98 File Offset: 0x00004D98
			// (set) Token: 0x06000102 RID: 258 RVA: 0x00006BA5 File Offset: 0x00004DA5
			[ConfigurationProperty("HealthStatusPublishInterval", DefaultValue = "06:00:00")]
			public TimeSpan KeyNameAvgHealthStatusPublishInterval
			{
				get
				{
					return this.InternalGetConfig<TimeSpan>("KeyNameAvgHealthStatusPublishInterval");
				}
				set
				{
					this.InternalSetConfig<TimeSpan>(value, "KeyNameAvgHealthStatusPublishInterval");
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000103 RID: 259 RVA: 0x00006BB3 File Offset: 0x00004DB3
			// (set) Token: 0x06000104 RID: 260 RVA: 0x00006BC0 File Offset: 0x00004DC0
			[ConfigurationProperty("SqlMaxRetryAttempts", DefaultValue = "5")]
			[IntegerValidator(MinValue = 0, MaxValue = 10000, ExcludeRange = false)]
			public int KeyNameSqlMaxRetryAttempts
			{
				get
				{
					return this.InternalGetConfig<int>("KeyNameSqlMaxRetryAttempts");
				}
				set
				{
					this.InternalSetConfig<int>(value, "KeyNameSqlMaxRetryAttempts");
				}
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000105 RID: 261 RVA: 0x00006BCE File Offset: 0x00004DCE
			// (set) Token: 0x06000106 RID: 262 RVA: 0x00006BDB File Offset: 0x00004DDB
			[ConfigurationProperty("SqlSleepBetweenRetryDuration", DefaultValue = "00:00:10")]
			[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "99999.00:00:00", ExcludeRange = false)]
			public TimeSpan KeyNameSqlSleepBetweenRetryDuration
			{
				get
				{
					return this.InternalGetConfig<TimeSpan>("KeyNameSqlSleepBetweenRetryDuration");
				}
				set
				{
					this.InternalSetConfig<TimeSpan>(value, "KeyNameSqlSleepBetweenRetryDuration");
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000107 RID: 263 RVA: 0x00006BE9 File Offset: 0x00004DE9
			// (set) Token: 0x06000108 RID: 264 RVA: 0x00006BF6 File Offset: 0x00004DF6
			[IntegerValidator(MinValue = 0, MaxValue = 600, ExcludeRange = false)]
			[ConfigurationProperty("BulkInsertSqlCommandTimeout", DefaultValue = "30")]
			public int KeyNameBulkInsertSqlCommandTimeout
			{
				get
				{
					return this.InternalGetConfig<int>("KeyNameBulkInsertSqlCommandTimeout");
				}
				set
				{
					this.InternalSetConfig<int>(value, "KeyNameBulkInsertSqlCommandTimeout");
				}
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000109 RID: 265 RVA: 0x00006C04 File Offset: 0x00004E04
			// (set) Token: 0x0600010A RID: 266 RVA: 0x00006C11 File Offset: 0x00004E11
			[ConfigurationProperty("TransientErrorAlertTreshold", DefaultValue = "03:00:00")]
			[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "99999.00:00:00", ExcludeRange = false)]
			public TimeSpan KeyNameTransientErrorAlertTreshold
			{
				get
				{
					return this.InternalGetConfig<TimeSpan>("KeyNameTransientErrorAlertTreshold");
				}
				set
				{
					this.InternalSetConfig<TimeSpan>(value, "KeyNameTransientErrorAlertTreshold");
				}
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x0600010B RID: 267 RVA: 0x00006C1F File Offset: 0x00004E1F
			// (set) Token: 0x0600010C RID: 268 RVA: 0x00006C2C File Offset: 0x00004E2C
			[ConfigurationProperty("ConnectionStringPrimary", DefaultValue = "Server=tcp:l0tqt6mh64.database.windows.net,1433;Database=exo-mig-mon;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;")]
			public string KeyNameConnectionStringPrimary
			{
				get
				{
					return this.InternalGetConfig<string>("KeyNameConnectionStringPrimary");
				}
				set
				{
					this.InternalSetConfig<string>(value, "KeyNameConnectionStringPrimary");
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600010D RID: 269 RVA: 0x00006C3A File Offset: 0x00004E3A
			// (set) Token: 0x0600010E RID: 270 RVA: 0x00006C47 File Offset: 0x00004E47
			[ConfigurationProperty("ConnectionStringDCInfo", DefaultValue = "Server=tcp:caf5176sig.database.windows.net,1433;Database=exo-mig-mon-dcinfo;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;")]
			public string KeyNameConnectionStringDCInfo
			{
				get
				{
					return this.InternalGetConfig<string>("KeyNameConnectionStringDCInfo");
				}
				set
				{
					this.InternalSetConfig<string>(value, "KeyNameConnectionStringDCInfo");
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600010F RID: 271 RVA: 0x00006C55 File Offset: 0x00004E55
			// (set) Token: 0x06000110 RID: 272 RVA: 0x00006C62 File Offset: 0x00004E62
			[ConfigurationProperty("MbxDBStatsFolder", DefaultValue = "Logging\\CompleteMailboxStats")]
			public string KeyNameMbxDBStatsFolder
			{
				get
				{
					return this.InternalGetConfig<string>("KeyNameMbxDBStatsFolder");
				}
				set
				{
					this.InternalSetConfig<string>(value, "KeyNameMbxDBStatsFolder");
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000111 RID: 273 RVA: 0x00006C70 File Offset: 0x00004E70
			// (set) Token: 0x06000112 RID: 274 RVA: 0x00006C7D File Offset: 0x00004E7D
			[ConfigurationProperty("DBStatsFileName", DefaultValue = "*DBStats*.log")]
			public string KeyNameDBStatsFileName
			{
				get
				{
					return this.InternalGetConfig<string>("KeyNameDBStatsFileName");
				}
				set
				{
					this.InternalSetConfig<string>(value, "KeyNameDBStatsFileName");
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000113 RID: 275 RVA: 0x00006C8B File Offset: 0x00004E8B
			// (set) Token: 0x06000114 RID: 276 RVA: 0x00006C98 File Offset: 0x00004E98
			[ConfigurationProperty("MbxStatsFileName", DefaultValue = "*MbxStats*.log")]
			public string KeyNameMbxStatsFileName
			{
				get
				{
					return this.InternalGetConfig<string>("KeyNameMbxStatsFileName");
				}
				set
				{
					this.InternalSetConfig<string>(value, "KeyNameMbxStatsFileName");
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000115 RID: 277 RVA: 0x00006CA6 File Offset: 0x00004EA6
			// (set) Token: 0x06000116 RID: 278 RVA: 0x00006CB3 File Offset: 0x00004EB3
			[ConfigurationProperty("IsBaseLogProcessorEnabled", DefaultValue = false)]
			public bool KeyNameIsLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsLogProcessorEnabled");
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000117 RID: 279 RVA: 0x00006CC1 File Offset: 0x00004EC1
			// (set) Token: 0x06000118 RID: 280 RVA: 0x00006CCE File Offset: 0x00004ECE
			[ConfigurationProperty("IsMRSLogProcessorEnabled", DefaultValue = true)]
			public bool KeyNameIsMRSLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsMRSLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsMRSLogProcessorEnabled");
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000119 RID: 281 RVA: 0x00006CDC File Offset: 0x00004EDC
			// (set) Token: 0x0600011A RID: 282 RVA: 0x00006CE9 File Offset: 0x00004EE9
			[ConfigurationProperty("IsQueueMRSWorkStatsLogProcessorEnabled", DefaultValue = true)]
			public bool KeyNameIsQueueStatsLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsQueueStatsLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsQueueStatsLogProcessorEnabled");
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x0600011B RID: 283 RVA: 0x00006CF7 File Offset: 0x00004EF7
			// (set) Token: 0x0600011C RID: 284 RVA: 0x00006D04 File Offset: 0x00004F04
			[ConfigurationProperty("IsJobPickupResultsLogProcessorEnabled", DefaultValue = true)]
			public bool KeyNameIsJobPickupResultsLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsJobPickupResultsLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsJobPickupResultsLogProcessorEnabled");
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x0600011D RID: 285 RVA: 0x00006D12 File Offset: 0x00004F12
			// (set) Token: 0x0600011E RID: 286 RVA: 0x00006D1F File Offset: 0x00004F1F
			[ConfigurationProperty("IsWLMResourceStatsLogProcessorEnabled", DefaultValue = true)]
			public bool KeyNameIsWLMResourceStatsMRSLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsWLMResourceStatsMRSLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsWLMResourceStatsMRSLogProcessorEnabled");
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x0600011F RID: 287 RVA: 0x00006D2D File Offset: 0x00004F2D
			// (set) Token: 0x06000120 RID: 288 RVA: 0x00006D3A File Offset: 0x00004F3A
			[ConfigurationProperty("IsMigServiceStatsLogProcessorEnabled", DefaultValue = true)]
			public bool KeyNameIsMigServiceLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsMigServiceLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsMigServiceLogProcessorEnabled");
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000121 RID: 289 RVA: 0x00006D48 File Offset: 0x00004F48
			// (set) Token: 0x06000122 RID: 290 RVA: 0x00006D55 File Offset: 0x00004F55
			[ConfigurationProperty("IsDatabaseInfoLogProcessorEnabled", DefaultValue = true)]
			public bool KeyNameIsDatabaseInfoLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsDatabaseInfoLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsDatabaseInfoLogProcessorEnabled");
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000123 RID: 291 RVA: 0x00006D63 File Offset: 0x00004F63
			// (set) Token: 0x06000124 RID: 292 RVA: 0x00006D70 File Offset: 0x00004F70
			[ConfigurationProperty("IsMailboxStatsLogProcessorEnabled", DefaultValue = true)]
			public bool KeyNameIsMailboxStatsLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsMailboxStatsLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsMailboxStatsLogProcessorEnabled");
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000125 RID: 293 RVA: 0x00006D7E File Offset: 0x00004F7E
			// (set) Token: 0x06000126 RID: 294 RVA: 0x00006D8B File Offset: 0x00004F8B
			[ConfigurationProperty("IsMRSWorkloadAvailabilityLogProcessorEnabled", DefaultValue = true)]
			public bool KeyNameIsMRSWorkloadAvailabilityLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsMRSWorkloadAvailabilityLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsMRSWorkloadAvailabilityLogProcessorEnabled");
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000127 RID: 295 RVA: 0x00006D99 File Offset: 0x00004F99
			// (set) Token: 0x06000128 RID: 296 RVA: 0x00006DA6 File Offset: 0x00004FA6
			[ConfigurationProperty("IsMrsSessionStatisticsLogProcessorEnabled", DefaultValue = true)]
			public bool KeyNameIsMrsSessionStatisticsLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsMrsSessionStatisticsLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsMrsSessionStatisticsLogProcessorEnabled");
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000129 RID: 297 RVA: 0x00006DB4 File Offset: 0x00004FB4
			// (set) Token: 0x0600012A RID: 298 RVA: 0x00006DC1 File Offset: 0x00004FC1
			[ConfigurationProperty("IsDrumTestingResultLogProcessorEnabled", DefaultValue = false)]
			public bool KeyNameIsDrumTestingResultLogProcessorEnabled
			{
				get
				{
					return this.InternalGetConfig<bool>("KeyNameIsDrumTestingResultLogProcessorEnabled");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "KeyNameIsDrumTestingResultLogProcessorEnabled");
				}
			}

			// Token: 0x040000C1 RID: 193
			private const string ConnectionStringProdMRSNAM = "Server=tcp:l0tqt6mh64.database.windows.net,1433;Database=exo-mig-mon;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

			// Token: 0x040000C2 RID: 194
			private const string ConnectionStringProdMRSEUR = "Server=tcp:apu9k5pu1h.database.windows.net,1433;Database=exo-mig-mon-eur;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

			// Token: 0x040000C3 RID: 195
			private const string ConnectionStringProdMRSAPC = "Server=tcp:fbwrmjzyoo.database.windows.net,1433;Database=exo-mig-mon-apc;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

			// Token: 0x040000C4 RID: 196
			private const string ConnectionStringProdMRSGALLATIN = "Server=tcp:odq4wwqizo.database.chinacloudapi.cn,1433;Database=exo-mig-mon-cn;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

			// Token: 0x040000C5 RID: 197
			private const string ConnectionStringProdDCInfo = "Server=tcp:caf5176sig.database.windows.net,1433;Database=exo-mig-mon-dcinfo;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

			// Token: 0x040000C6 RID: 198
			private const string ConnectionStringProdDCInfoGALLATIN = "Server=tcp:odq4wwqizo.database.chinacloudapi.cn,1433;Database=exo-mig-mon-dcinfo-cn;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

			// Token: 0x040000C7 RID: 199
			private const string ConnectionStringTestMRS = "Server=tcp:wawqf20dco.database.windows.net,1433;Database=exo-mig-mon-test;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

			// Token: 0x040000C8 RID: 200
			private const string ConnectionStringTestDCInfo = "Server=tcp:wawqf20dco.database.windows.net,1433;Database=exo-mig-mon-dcinfo-test;User ID=migmonsvclet;Password=UC2yX5d4sSMIMvpB4Vffww==;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
		}
	}
}
