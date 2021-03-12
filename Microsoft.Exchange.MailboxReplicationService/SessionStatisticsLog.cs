using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000080 RID: 128
	internal class SessionStatisticsLog : ObjectLog<SessionStatisticsLogData>
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x0001F5D6 File Offset: 0x0001D7D6
		private SessionStatisticsLog() : base(new SessionStatisticsLog.SessionStatisticsLogSchema(), new SimpleObjectLogConfiguration("SessionStatistic", "SessionStatisticsLogEnabled", "SessionStatisticsLogMaxDirSize", "SessionStatisticsLogMaxFileSize"))
		{
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001F5FC File Offset: 0x0001D7FC
		public static void Write(Guid requestGuid, SessionStatistics sessionStatistics, SessionStatistics archiveSessionStatistics)
		{
			if (sessionStatistics != null || archiveSessionStatistics != null)
			{
				SessionStatisticsLogData objectToLog = new SessionStatisticsLogData(requestGuid, sessionStatistics, archiveSessionStatistics);
				SessionStatisticsLog.instance.LogObject(objectToLog);
			}
		}

		// Token: 0x0400026B RID: 619
		private static SessionStatisticsLog instance = new SessionStatisticsLog();

		// Token: 0x02000081 RID: 129
		private class SessionStatisticsLogSchema : ObjectLogSchema
		{
			// Token: 0x06000586 RID: 1414 RVA: 0x0001F630 File Offset: 0x0001D830
			private static DurationInfo FindMaxDurationInfoFromStats(SessionStatistics stats, bool isArchive)
			{
				string str = isArchive ? "_Archive" : string.Empty;
				DurationInfo durationInfo = new DurationInfo
				{
					Name = string.Empty,
					Duration = TimeSpan.Zero
				};
				if (stats != null)
				{
					if (stats.SourceProviderInfo.Durations.Count > 0)
					{
						DurationInfo durationInfo2 = stats.SourceProviderInfo.Durations[0];
						durationInfo.Name = "SourceProvider_" + durationInfo2.Name + str;
						durationInfo.Duration = durationInfo2.Duration;
					}
					if (stats.DestinationProviderInfo.Durations.Count > 0)
					{
						DurationInfo durationInfo2 = stats.DestinationProviderInfo.Durations[0];
						if (durationInfo2.Duration > durationInfo.Duration)
						{
							durationInfo.Name = "DestinationProvider_" + durationInfo2.Name + str;
							durationInfo.Duration = durationInfo2.Duration;
						}
					}
				}
				return durationInfo;
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x0001F79C File Offset: 0x0001D99C
			private static IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> GetMaximumProviderDurations()
			{
				List<IObjectLogPropertyDefinition<SessionStatisticsLogData>> list = new List<IObjectLogPropertyDefinition<SessionStatisticsLogData>>();
				Func<SessionStatisticsLogData, DurationInfo> findMaxDurationInfo = delegate(SessionStatisticsLogData logData)
				{
					DurationInfo durationInfo = SessionStatisticsLog.SessionStatisticsLogSchema.FindMaxDurationInfoFromStats(logData.SessionStatistics, false);
					DurationInfo durationInfo2 = SessionStatisticsLog.SessionStatisticsLogSchema.FindMaxDurationInfoFromStats(logData.ArchiveSessionStatistics, true);
					if (!(durationInfo.Duration > durationInfo2.Duration))
					{
						return durationInfo2;
					}
					return durationInfo;
				};
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("MaxProviderDurationMethodName", (SessionStatisticsLogData s) => findMaxDurationInfo(s).Name));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("MaxProviderDurationInMilliseconds", (SessionStatisticsLogData s) => (long)findMaxDurationInfo(s).Duration.TotalMilliseconds));
				return list;
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x0001F8B8 File Offset: 0x0001DAB8
			private static IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> GetProviderDurations(bool isArchive = false)
			{
				List<IObjectLogPropertyDefinition<SessionStatisticsLogData>> list = new List<IObjectLogPropertyDefinition<SessionStatisticsLogData>>();
				string str = isArchive ? "_Archive" : string.Empty;
				Func<SessionStatisticsLogData, SessionStatistics> getStatsFunc = delegate(SessionStatisticsLogData logData)
				{
					if (!isArchive)
					{
						return logData.SessionStatistics;
					}
					return logData.ArchiveSessionStatistics;
				};
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("SourceProvider_TotalDurationInMilliseconds" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0L : ((long)getStatsFunc(s).SourceProviderInfo.TotalDuration.TotalMilliseconds)));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("DestinationProvider_TotalDurationInMilliseconds" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0L : ((long)getStatsFunc(s).DestinationProviderInfo.TotalDuration.TotalMilliseconds)));
				foreach (string name in SessionStatisticsLog.SessionStatisticsLogSchema.SourceProviderMethods)
				{
					list.Add(new SessionStatisticsLog.SessionStatisticsLogSchema.ProviderDurationProperty(name, false, isArchive));
				}
				foreach (string name2 in SessionStatisticsLog.SessionStatisticsLogSchema.DestinationProviderMethods)
				{
					list.Add(new SessionStatisticsLog.SessionStatisticsLogSchema.ProviderDurationProperty(name2, true, isArchive));
				}
				return list;
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x0001FC3C File Offset: 0x0001DE3C
			private static IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> GetLatencyInfo(bool isArchive = false)
			{
				List<IObjectLogPropertyDefinition<SessionStatisticsLogData>> list = new List<IObjectLogPropertyDefinition<SessionStatisticsLogData>>();
				string str = isArchive ? "_Archive" : string.Empty;
				Func<SessionStatisticsLogData, SessionStatistics> getStatsFunc = delegate(SessionStatisticsLogData logData)
				{
					if (!isArchive)
					{
						return logData.SessionStatistics;
					}
					return logData.ArchiveSessionStatistics;
				};
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("SourceLatencyInMillisecondsCurrent" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.SourceLatencyInfo.Current;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("SourceLatencyInMillisecondsAverage" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.SourceLatencyInfo.Average;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("SourceLatencyInMillisecondsMin" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null || sessionStatistics.SourceLatencyInfo.Min == int.MaxValue) ? 0 : sessionStatistics.SourceLatencyInfo.Min;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("SourceLatencyInMillisecondsMax" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null || sessionStatistics.SourceLatencyInfo.Max == int.MinValue) ? 0 : sessionStatistics.SourceLatencyInfo.Max;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("SourceLatencyNumberOfSamples" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.SourceLatencyInfo.NumberOfLatencySamplingCalls;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("SourceLatencyTotalNumberOfRemoteCalls" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.SourceLatencyInfo.TotalNumberOfRemoteCalls;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("DestinationLatencyInMillisecondsCurrent" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.DestinationLatencyInfo.Current;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("DestinationLatencyInMillisecondsAverage" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.DestinationLatencyInfo.Average;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("DestinationLatencyInMillisecondsMin" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.DestinationLatencyInfo.Min;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("DestinationLatencyInMillisecondsMax" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.DestinationLatencyInfo.Max;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("DestinationLatencyNumberOfSamples" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.DestinationLatencyInfo.NumberOfLatencySamplingCalls;
				}));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("DestinationLatencyTotalNumberOfRemoteCalls" + str, delegate(SessionStatisticsLogData s)
				{
					SessionStatistics sessionStatistics = getStatsFunc(s);
					return (sessionStatistics == null) ? 0 : sessionStatistics.DestinationLatencyInfo.TotalNumberOfRemoteCalls;
				}));
				return list;
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x000200C8 File Offset: 0x0001E2C8
			private static IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> GetWordBreakingStats(bool isArchive = false)
			{
				List<IObjectLogPropertyDefinition<SessionStatisticsLogData>> list = new List<IObjectLogPropertyDefinition<SessionStatisticsLogData>>();
				string str = isArchive ? "_Archive" : string.Empty;
				Func<SessionStatisticsLogData, SessionStatistics> getStatsFunc = delegate(SessionStatisticsLogData logData)
				{
					if (!isArchive)
					{
						return logData.SessionStatistics;
					}
					return logData.ArchiveSessionStatistics;
				};
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_TotalTimeInMilliseconds" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0L : ((long)getStatsFunc(s).TotalTimeProcessingMessages.TotalMilliseconds)));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_TimeInWordBreakerInMilliseconds" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0L : ((long)getStatsFunc(s).TimeInWordbreaker.TotalMilliseconds)));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_TimeInQueueInMilliseconds" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0L : ((long)getStatsFunc(s).TimeInQueue.TotalMilliseconds)));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_TimeProcessingFailedMessagesInMilliseconds" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0L : ((long)getStatsFunc(s).TimeProcessingFailedMessages.TotalMilliseconds)));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_TimeInTransportRetrieverInMilliseconds" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0L : ((long)getStatsFunc(s).TimeInTransportRetriever.TotalMilliseconds)));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_TimeInDocParserInMilliseconds" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0L : ((long)getStatsFunc(s).TimeInDocParser.TotalMilliseconds)));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_TimeInNLGSubflowInMilliseconds" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0L : ((long)getStatsFunc(s).TimeInNLGSubflow.TotalMilliseconds)));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_TotalMessagesProcessed" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0 : getStatsFunc(s).TotalMessagesProcessed));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_MessageLevelFailures" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0 : getStatsFunc(s).MessageLevelFailures));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_MessagesSuccessfullyAnnotated" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0 : getStatsFunc(s).MessagesSuccessfullyAnnotated));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_AnnotationSkipped" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0 : getStatsFunc(s).AnnotationSkipped));
				list.Add(new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("CI_ConnectionLevelFailures" + str, (SessionStatisticsLogData s) => (getStatsFunc(s) == null) ? 0 : getStatsFunc(s).ConnectionLevelFailures));
				return list;
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x0600058B RID: 1419 RVA: 0x000202A8 File Offset: 0x0001E4A8
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Mailbox Replication Service";
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x0600058C RID: 1420 RVA: 0x000202AF File Offset: 0x0001E4AF
			public override string LogType
			{
				get
				{
					return "SessionStatistics Log";
				}
			}

			// Token: 0x0400026C RID: 620
			private static readonly string[] SourceProviderMethods = new string[]
			{
				"ISourceMailbox.ExportMessages",
				"ISourceMailbox.GetFolder",
				"WrapperBase.Dispose",
				"ISourceFolder.CopyTo",
				"IFolder.EnumerateMessages",
				"IFolder.GetSecurityDescriptor",
				"IFolder.GetFolderRec"
			};

			// Token: 0x0400026D RID: 621
			private static readonly string[] DestinationProviderMethods = new string[]
			{
				"IMapiFxProxy.ProcessRequest",
				"IDestinationFolder.SetSecurityDescriptor",
				"IFxProxyPool.GetFolderProxy",
				"IMailbox.SaveSyncState",
				"IDestinationMailbox.CreateFolder",
				"IFxProxyPool.GetFolderData",
				"IDestinationFolder.SetRules"
			};

			// Token: 0x0400026E RID: 622
			public static readonly ObjectLogSimplePropertyDefinition<SessionStatisticsLogData> RequestGuid = new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("RequestGuid", (SessionStatisticsLogData s) => s.RequestGuid);

			// Token: 0x0400026F RID: 623
			public static readonly IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> MaximumProviderDurations = SessionStatisticsLog.SessionStatisticsLogSchema.GetMaximumProviderDurations();

			// Token: 0x04000270 RID: 624
			public static readonly ObjectLogSimplePropertyDefinition<SessionStatisticsLogData> SessionId = new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("SessionId", delegate(SessionStatisticsLogData s)
			{
				if (s.SessionStatistics != null)
				{
					return s.SessionStatistics.SessionId;
				}
				return null;
			});

			// Token: 0x04000271 RID: 625
			public static readonly IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> WordBreakingStats = SessionStatisticsLog.SessionStatisticsLogSchema.GetWordBreakingStats(false);

			// Token: 0x04000272 RID: 626
			public static readonly IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> ProviderDurations = SessionStatisticsLog.SessionStatisticsLogSchema.GetProviderDurations(false);

			// Token: 0x04000273 RID: 627
			public static readonly IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> LatencyInfo = SessionStatisticsLog.SessionStatisticsLogSchema.GetLatencyInfo(false);

			// Token: 0x04000274 RID: 628
			public static readonly ObjectLogSimplePropertyDefinition<SessionStatisticsLogData> PreFinalSyncDataProcessingDurationInMilliseconds = new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("PreFinalSyncDataProcessingDurationInMilliseconds", (SessionStatisticsLogData s) => (s.SessionStatistics == null) ? 0.0 : s.SessionStatistics.PreFinalSyncDataProcessingDuration.TotalMilliseconds);

			// Token: 0x04000275 RID: 629
			public static readonly ObjectLogSimplePropertyDefinition<SessionStatisticsLogData> SessionId_Archive = new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("SessionId_Archive", delegate(SessionStatisticsLogData s)
			{
				if (s.ArchiveSessionStatistics != null)
				{
					return s.ArchiveSessionStatistics.SessionId;
				}
				return null;
			});

			// Token: 0x04000276 RID: 630
			public static readonly IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> WordBreakingStats_Archive = SessionStatisticsLog.SessionStatisticsLogSchema.GetWordBreakingStats(true);

			// Token: 0x04000277 RID: 631
			public static readonly IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> ProviderDurations_Archive = SessionStatisticsLog.SessionStatisticsLogSchema.GetProviderDurations(true);

			// Token: 0x04000278 RID: 632
			public static readonly IEnumerable<IObjectLogPropertyDefinition<SessionStatisticsLogData>> LatencyInfo_Archive = SessionStatisticsLog.SessionStatisticsLogSchema.GetLatencyInfo(true);

			// Token: 0x04000279 RID: 633
			public static readonly ObjectLogSimplePropertyDefinition<SessionStatisticsLogData> PreFinalSyncDataProcessingDurationInMilliseconds_Archive = new ObjectLogSimplePropertyDefinition<SessionStatisticsLogData>("PreFinalSyncDataProcessingDurationInMilliseconds_Archive", (SessionStatisticsLogData s) => (s.ArchiveSessionStatistics == null) ? 0.0 : s.ArchiveSessionStatistics.PreFinalSyncDataProcessingDuration.TotalMilliseconds);

			// Token: 0x02000082 RID: 130
			private class ProviderDurationProperty : IObjectLogPropertyDefinition<SessionStatisticsLogData>
			{
				// Token: 0x06000595 RID: 1429 RVA: 0x0002052B File Offset: 0x0001E72B
				public ProviderDurationProperty(string name, bool isDestinationSide = false, bool isArchive = false)
				{
					this.name = name;
					this.isArchive = isArchive;
					this.isDestinationSide = isDestinationSide;
				}

				// Token: 0x17000158 RID: 344
				// (get) Token: 0x06000596 RID: 1430 RVA: 0x00020548 File Offset: 0x0001E748
				string IObjectLogPropertyDefinition<SessionStatisticsLogData>.FieldName
				{
					get
					{
						return string.Format("{0}Duration_{1}{2}", this.isDestinationSide ? "Destination" : "Source", this.name, this.isArchive ? "_Archive" : string.Empty);
					}
				}

				// Token: 0x06000597 RID: 1431 RVA: 0x00020598 File Offset: 0x0001E798
				object IObjectLogPropertyDefinition<SessionStatisticsLogData>.GetValue(SessionStatisticsLogData logData)
				{
					List<DurationInfo> durations;
					if (this.isArchive)
					{
						if (logData.ArchiveSessionStatistics == null)
						{
							return 0L;
						}
						if (this.isDestinationSide)
						{
							durations = logData.ArchiveSessionStatistics.DestinationProviderInfo.Durations;
						}
						else
						{
							durations = logData.ArchiveSessionStatistics.SourceProviderInfo.Durations;
						}
					}
					else
					{
						if (logData.SessionStatistics == null)
						{
							return 0L;
						}
						if (this.isDestinationSide)
						{
							durations = logData.SessionStatistics.DestinationProviderInfo.Durations;
						}
						else
						{
							durations = logData.SessionStatistics.SourceProviderInfo.Durations;
						}
					}
					DurationInfo durationInfo = durations.Find((DurationInfo d) => d.Name.Equals(this.name));
					if (durationInfo != null)
					{
						return (long)durationInfo.Duration.TotalMilliseconds;
					}
					return 0L;
				}

				// Token: 0x04000280 RID: 640
				private readonly string name;

				// Token: 0x04000281 RID: 641
				private readonly bool isArchive;

				// Token: 0x04000282 RID: 642
				private readonly bool isDestinationSide;
			}
		}
	}
}
