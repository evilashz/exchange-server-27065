using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001C3 RID: 451
	internal class MRSSettingsLog : MRSScheduledLog<MRSSettingsData>, ILogProcessable
	{
		// Token: 0x060010FA RID: 4346 RVA: 0x000277A9 File Offset: 0x000259A9
		public MRSSettingsLog() : base(new MRSSettingsLog.MRSSettingsLogSchema(), new SimpleObjectLogConfiguration("MRSSetting", "MRSSettingsLogEnabled", "MRSSettingsLogMaxDirSize", "MRSSettingsLogMaxFileSize"))
		{
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x000277CF File Offset: 0x000259CF
		protected override bool IsLogEnabled
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<bool>("MRSSettingsLogEnabled");
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x000277DB File Offset: 0x000259DB
		protected override TimeSpan ScheduledLoggingPeriod
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("MRSSettingsLoggingPeriod");
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000277E8 File Offset: 0x000259E8
		public void ProcessLogs()
		{
			if (!this.LogIsNeeded())
			{
				return;
			}
			MRSSettingsLogCollection config = ConfigBase<MRSConfigSchema>.GetConfig<MRSSettingsLogCollection>("MRSSettingsLogList");
			if (config == null)
			{
				MRSSettingsLog.PublishPeriodicExceptionNotification(string.Format("The value for {0} setting is corrupt. Check and correct setting value", "MRSSettingsLogList"));
				return;
			}
			foreach (MRSSettingsLogCollection.MRSSettingsLogElement mrssettingsLogElement in config.SettingsLogCollection)
			{
				try
				{
					MRSSettingsData loggingStatsData;
					if (StringComparer.OrdinalIgnoreCase.Equals(mrssettingsLogElement.SettingName, "IsJobPickupEnabled"))
					{
						using (IEnumerator enumerator2 = Enum.GetValues(typeof(RequestWorkloadType)).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								RequestWorkloadType requestWorkloadType = (RequestWorkloadType)obj;
								if (requestWorkloadType != RequestWorkloadType.None)
								{
									SettingsContextBase settingsContextBase = new GenericSettingsContext("RequestWorkloadType", requestWorkloadType.ToString(), null);
									using (settingsContextBase.Activate())
									{
										bool config2 = ConfigBase<MRSConfigSchema>.GetConfig<bool>(mrssettingsLogElement.SettingName);
										loggingStatsData = new MRSSettingsData
										{
											Context = string.Format("{0}={1}", "RequestWorkloadType", requestWorkloadType.ToString()),
											SettingName = "IsJobPickupEnabled",
											SettingValue = Convert.ToInt32(config2).ToString()
										};
									}
									this.Write(loggingStatsData);
								}
							}
							continue;
						}
					}
					if (StringComparer.OrdinalIgnoreCase.Equals(mrssettingsLogElement.SettingName, "IgnoreHealthMonitor"))
					{
						List<ResourceKey> list = new List<ResourceKey>
						{
							ADResourceKey.Key,
							ProcessorResourceKey.Local
						};
						using (List<ResourceKey>.Enumerator enumerator3 = list.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								ResourceKey resourceKey = enumerator3.Current;
								SettingsContextBase settingsContextBase2 = new GenericSettingsContext("WlmHealthMonitor", resourceKey.ToString(), null);
								using (settingsContextBase2.Activate())
								{
									bool config3 = ConfigBase<MRSConfigSchema>.GetConfig<bool>(mrssettingsLogElement.SettingName);
									loggingStatsData = new MRSSettingsData
									{
										Context = string.Format("{0}={1}", "WlmHealthMonitor", resourceKey.ToString()),
										SettingName = "IgnoreHealthMonitor",
										SettingValue = Convert.ToInt32(config3).ToString()
									};
								}
								this.Write(loggingStatsData);
							}
							continue;
						}
					}
					ConfigurationProperty configurationProperty;
					if (!ConfigBase<MRSConfigSchema>.Schema.TryGetConfigurationProperty(mrssettingsLogElement.SettingName, out configurationProperty))
					{
						throw new MRSSettingsLog.BadConfigSettingException(string.Format("Can not find corresponding name of MRS config setting specified by string {0}. Check if the setting name and correct it if needed", mrssettingsLogElement.SettingName));
					}
					string settingValue = string.Empty;
					if (configurationProperty.Type == typeof(bool))
					{
						settingValue = ConfigBase<MRSConfigSchema>.GetConfig<bool>(configurationProperty.Name).ToString();
					}
					else if (configurationProperty.Type == typeof(int))
					{
						settingValue = ConfigBase<MRSConfigSchema>.GetConfig<int>(configurationProperty.Name).ToString();
					}
					else if (configurationProperty.Type == typeof(long))
					{
						settingValue = ConfigBase<MRSConfigSchema>.GetConfig<long>(configurationProperty.Name).ToString();
					}
					else if (configurationProperty.Type == typeof(string))
					{
						settingValue = ConfigBase<MRSConfigSchema>.GetConfig<string>(configurationProperty.Name);
					}
					else
					{
						if (!(configurationProperty.Type == typeof(TimeSpan)))
						{
							throw new MRSSettingsLog.BadConfigSettingException(string.Format("Type {0} of a provided setting {1} is not supported by logging functionality. Check and correct list of the settings to be logged", configurationProperty.Type, mrssettingsLogElement.SettingName));
						}
						settingValue = ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>(configurationProperty.Name).ToString();
					}
					loggingStatsData = new MRSSettingsData
					{
						Context = "Server",
						SettingName = configurationProperty.Name,
						SettingValue = settingValue
					};
					this.Write(loggingStatsData);
				}
				catch (MRSSettingsLog.BadConfigSettingException ex)
				{
					MRSSettingsLog.PublishPeriodicExceptionNotification(ex.Message);
				}
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00027C70 File Offset: 0x00025E70
		private static void PublishPeriodicExceptionNotification(string message)
		{
			EventNotificationItem.PublishPeriodic(ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration.Name, "MRSConfigSettingsCorrupted", message, "MRSSettingsLogExceptionNotification", MRSSettingsLog.PeriodicExceptionNotificationPeriod, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00027C9D File Offset: 0x00025E9D
		public void Write(MRSSettingsData loggingStatsData)
		{
			base.LogObject(loggingStatsData);
		}

		// Token: 0x04000988 RID: 2440
		private const string ContextPairOutputFormat = "{0}={1}";

		// Token: 0x04000989 RID: 2441
		private const string ServerContextName = "Server";

		// Token: 0x0400098A RID: 2442
		private const string PeriodicExceptionNotificationKey = "MRSSettingsLogExceptionNotification";

		// Token: 0x0400098B RID: 2443
		private static readonly TimeSpan PeriodicExceptionNotificationPeriod = TimeSpan.FromHours(1.0);

		// Token: 0x020001C4 RID: 452
		internal class MRSSettingsLogSchema : ObjectLogSchema
		{
			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x06001101 RID: 4353 RVA: 0x00027CBB File Offset: 0x00025EBB
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Mailbox Replication Service";
				}
			}

			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x06001102 RID: 4354 RVA: 0x00027CC2 File Offset: 0x00025EC2
			public override string LogType
			{
				get
				{
					return "MRSSettings Log";
				}
			}

			// Token: 0x0400098C RID: 2444
			public static readonly ObjectLogSimplePropertyDefinition<MRSSettingsData> OwnerResourceName = new ObjectLogSimplePropertyDefinition<MRSSettingsData>("Context", (MRSSettingsData d) => d.Context);

			// Token: 0x0400098D RID: 2445
			public static readonly ObjectLogSimplePropertyDefinition<MRSSettingsData> OwnerResourceGuid = new ObjectLogSimplePropertyDefinition<MRSSettingsData>("SettingName", (MRSSettingsData d) => d.SettingName);

			// Token: 0x0400098E RID: 2446
			public static readonly ObjectLogSimplePropertyDefinition<MRSSettingsData> OwnerResourceType = new ObjectLogSimplePropertyDefinition<MRSSettingsData>("SettingValue", (MRSSettingsData d) => d.SettingValue);
		}

		// Token: 0x020001C5 RID: 453
		private class BadConfigSettingException : Exception
		{
			// Token: 0x06001108 RID: 4360 RVA: 0x00027D7D File Offset: 0x00025F7D
			private BadConfigSettingException()
			{
			}

			// Token: 0x06001109 RID: 4361 RVA: 0x00027D85 File Offset: 0x00025F85
			public BadConfigSettingException(string exceptionMessage) : base(exceptionMessage)
			{
			}

			// Token: 0x0600110A RID: 4362 RVA: 0x00027D8E File Offset: 0x00025F8E
			public BadConfigSettingException(string exceptionMessage, Exception innerException) : base(exceptionMessage, innerException)
			{
			}
		}
	}
}
