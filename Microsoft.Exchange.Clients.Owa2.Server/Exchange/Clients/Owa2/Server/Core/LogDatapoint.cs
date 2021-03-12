using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000338 RID: 824
	internal class LogDatapoint : ServiceCommand<bool>
	{
		// Token: 0x06001B4A RID: 6986 RVA: 0x00067349 File Offset: 0x00065549
		public LogDatapoint(CallContext callContext, Datapoint[] datapoints, Action<IEnumerable<ILogEvent>> analyticsLogger, Action<IEnumerable<ILogEvent>> diagnosticsLogger, Action<string, Type> registerType, bool isOwa, string owaVersion) : this(callContext, datapoints, analyticsLogger, diagnosticsLogger, new ClientWatsonDatapointHandler(callContext.ProtocolLog, owaVersion), registerType, isOwa)
		{
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00067368 File Offset: 0x00065568
		public LogDatapoint(CallContext callContext, Datapoint[] datapoints, Action<IEnumerable<ILogEvent>> analyticsLogger, Action<IEnumerable<ILogEvent>> diagnosticsLogger, ClientWatsonDatapointHandler clientWatsonHandler, Action<string, Type> registerType, bool isOwa) : base(callContext)
		{
			this.datapoints = datapoints;
			this.analyticsLogger = analyticsLogger;
			this.diagnosticsLogger = diagnosticsLogger;
			this.clientWatsonHandler = clientWatsonHandler;
			this.isOwa = isOwa;
			registerType(LogDatapoint.LogDatapointActionName, typeof(LogDatapointMetadata));
			string value = callContext.HttpContext.Request.Headers["X-OWA-Test-PassThruProxy"];
			if (!string.IsNullOrEmpty(value))
			{
				this.isFromPassThroughProxy = true;
			}
			this.serverVersion = this.GetServerVersion();
			this.clientVersion = (callContext.HttpContext.Request.Headers["X-OWA-ClientBuildVersion"] ?? this.serverVersion);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00067418 File Offset: 0x00065618
		protected override bool InternalExecute()
		{
			if (this.datapoints == null || this.datapoints.Length == 0)
			{
				return true;
			}
			InstrumentationSettings instrumentationSettings = this.GetInstrumentationSettings();
			if (!instrumentationSettings.IsInstrumentationEnabled())
			{
				return true;
			}
			UserContext userContext = this.GetUserContext();
			Stopwatch stopwatch = Stopwatch.StartNew();
			Datapoint chunkHeaderDatapoint = this.GetChunkHeaderDatapoint(this.datapoints[0].Time);
			int num;
			IDictionary<DatapointConsumer, LogDatapoint.ClientLogEventList> dictionary = this.TriageAndConvertDatapoints(userContext, chunkHeaderDatapoint, instrumentationSettings, out num);
			long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
			IList<ClientLogEvent> events = dictionary[DatapointConsumer.Watson].Events;
			if (events.Count > 0 && instrumentationSettings.IsClientWatsonEnabled)
			{
				this.clientWatsonHandler.ReportWatsonEvents(userContext, events, chunkHeaderDatapoint, this.datapoints);
			}
			FaultInjection.GenerateFault((FaultInjection.LIDs)3804638525U);
			stopwatch.Restart();
			this.analyticsLogger(dictionary[DatapointConsumer.Analytics].Events);
			this.diagnosticsLogger(dictionary[DatapointConsumer.Diagnostics].Events);
			long elapsedMilliseconds2 = stopwatch.ElapsedMilliseconds;
			long num2 = 0L;
			long num3 = 0L;
			int num4 = 0;
			if (this.isOwa && dictionary[DatapointConsumer.Inference].Events.Count > 0)
			{
				stopwatch.Restart();
				IActivityLogger activityLogger = this.GetActivityLogger();
				if (activityLogger != null)
				{
					IList<Activity> list = this.CreateInferenceActivities(dictionary[DatapointConsumer.Inference].Events);
					num2 = stopwatch.ElapsedMilliseconds;
					num3 = 0L;
					if (list.Count > 0)
					{
						num4 = list.Count;
						stopwatch.Restart();
						this.WriteInferenceActivities(list, activityLogger);
						num3 = stopwatch.ElapsedMilliseconds;
					}
				}
			}
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.CreateDatapointEventsElapsed, elapsedMilliseconds);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.TotalDatapointSize, num);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.AnalyticsDatapointCount, dictionary[DatapointConsumer.Analytics].Events.Count);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.AnalyticsDatapointSize, dictionary[DatapointConsumer.Analytics].DatapointTotalSize);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.InferenceDatapointCount, dictionary[DatapointConsumer.Inference].Events.Count);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.InferenceDatapointSize, dictionary[DatapointConsumer.Inference].DatapointTotalSize);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.DiagnosticsDatapointCount, dictionary[DatapointConsumer.Diagnostics].Events.Count);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.DiagnosticsDatapointSize, dictionary[DatapointConsumer.Diagnostics].DatapointTotalSize);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.WatsonDatapointCount, dictionary[DatapointConsumer.Watson].Events.Count);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.WatsonDatapointSize, dictionary[DatapointConsumer.Watson].DatapointTotalSize);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.DatapointsToLoggerElapsed, elapsedMilliseconds2);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.InferenceActivitiesToMailboxCount, num4);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.CreateInferenceActivitiesElapsed, num2);
			base.CallContext.ProtocolLog.Set(LogDatapointMetadata.InferenceActivitiesToMailboxElapsed, num3);
			return true;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0006779C File Offset: 0x0006599C
		protected virtual IActivityLogger GetActivityLogger()
		{
			MailboxSession mailboxSession = this.GetMailboxSession() as MailboxSession;
			return ActivityLogger.Create(mailboxSession);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x000677BC File Offset: 0x000659BC
		protected virtual string GetClientAddress()
		{
			string text = HttpContext.Current.Request.Headers["X-Forwarded-For"];
			if (string.IsNullOrEmpty(text))
			{
				return HttpContext.Current.Request.UserHostAddress;
			}
			return text;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000677FC File Offset: 0x000659FC
		protected virtual string GetServerVersion()
		{
			if (this.isOwa)
			{
				return Globals.ApplicationVersion ?? OwaRegistryKeys.InstalledOwaVersion;
			}
			return string.Empty;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0006781A File Offset: 0x00065A1A
		protected virtual InstrumentationSettings GetInstrumentationSettings()
		{
			return InstrumentationSettings.Instance;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00067821 File Offset: 0x00065A21
		protected virtual IMailboxSession GetMailboxSession()
		{
			if (this.mailboxSession == null)
			{
				this.mailboxSession = base.MailboxIdentityMailboxSession;
			}
			return this.mailboxSession;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0006783D File Offset: 0x00065A3D
		protected virtual UserContext GetUserContext()
		{
			if (this.isOwa)
			{
				return UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			}
			return null;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00067865 File Offset: 0x00065A65
		private static bool ShouldProcessForConsumer(Datapoint datapoint, DatapointConsumer consumer, DatapointConsumer enabledConsumers)
		{
			return datapoint.IsForConsumer(consumer) && (consumer & enabledConsumers) != DatapointConsumer.None;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0006787C File Offset: 0x00065A7C
		private IDictionary<DatapointConsumer, LogDatapoint.ClientLogEventList> TriageAndConvertDatapoints(UserContext userContext, Datapoint header, InstrumentationSettings settings, out int totalDatapointSize)
		{
			string userContextId = this.GetUserContextId();
			string clientAddress = this.GetClientAddress();
			string userName = this.isOwa ? base.CallContext.GetEffectiveAccessingSmtpAddress() : "ExternalUser";
			string cookieValueAndSetIfNull = ClientIdCookie.GetCookieValueAndSetIfNull(HttpContext.Current);
			ClientLogEvent header2 = new ClientLogEvent(header, userContextId, clientAddress, userName, this.serverVersion, base.CallContext.IsMowa, cookieValueAndSetIfNull);
			DatapointConsumer enabledConsumers = this.GetEnabledConsumers(settings);
			totalDatapointSize = 0;
			LogDatapoint.ClientLogEventList clientLogEventList = new LogDatapoint.ClientLogEventList(DatapointConsumer.Analytics, enabledConsumers, header2);
			LogDatapoint.ClientLogEventList clientLogEventList2 = new LogDatapoint.ClientLogEventList(DatapointConsumer.Diagnostics, enabledConsumers, header2);
			LogDatapoint.ClientLogEventList clientLogEventList3 = new LogDatapoint.ClientLogEventList(DatapointConsumer.Inference, enabledConsumers, null);
			LogDatapoint.ClientLogEventList clientLogEventList4 = new LogDatapoint.ClientLogEventList(DatapointConsumer.Watson, enabledConsumers, null);
			int num = 0;
			if (userContext != null && this.datapoints[0].Id == "SessionInfo")
			{
				num = 1;
				Datapoint datapoint = this.datapoints[0];
				ClientLogEvent clientLogEvent = new ClientLogEvent(datapoint, userContextId, clientAddress, userName, this.serverVersion, base.CallContext.IsMowa, cookieValueAndSetIfNull);
				userContext.LogEventCommonData.UpdateClientData(clientLogEvent.DatapointProperties);
				this.clientVersion = userContext.LogEventCommonData.ClientBuild;
				clientLogEvent.UpdateTenantInfo(userContext);
				clientLogEvent.UpdateNetid(userContext);
				clientLogEvent.UpdateMailboxGuid(userContext.ExchangePrincipal);
				clientLogEvent.UpdateDatabaseInfo(userContext);
				clientLogEvent.UpdateFlightInfo(userContext.LogEventCommonData);
				clientLogEvent.UpdatePassThroughProxyInfo(this.isFromPassThroughProxy);
				clientLogEvent.UpdateUserAgent(userContext.UserAgent);
				clientLogEventList.CheckAndAdd(clientLogEvent);
				clientLogEventList3.CheckAndAdd(clientLogEvent);
				clientLogEventList2.CheckAndAdd(clientLogEvent);
				clientLogEventList4.CheckAndAdd(clientLogEvent);
			}
			for (int i = num; i < this.datapoints.Length; i++)
			{
				Datapoint datapoint2 = this.datapoints[i];
				totalDatapointSize += datapoint2.Size;
				if ((enabledConsumers & datapoint2.Consumers) != DatapointConsumer.None)
				{
					ClientLogEvent clientLogEvent2 = new ClientLogEvent(datapoint2, userContextId, clientAddress, userName, this.serverVersion, base.CallContext.IsMowa, cookieValueAndSetIfNull);
					if (userContext != null)
					{
						clientLogEvent2.UpdateClientBuildVersion(userContext.LogEventCommonData);
						string id;
						if (clientLogEventList.CheckAndAdd(clientLogEvent2) && (id = datapoint2.Id) != null)
						{
							if (<PrivateImplementationDetails>{4041ACEF-19DC-4DB7-9F0C-17DA902ABE6A}.$$method0x6001a88-1 == null)
							{
								<PrivateImplementationDetails>{4041ACEF-19DC-4DB7-9F0C-17DA902ABE6A}.$$method0x6001a88-1 = new Dictionary<string, int>(11)
								{
									{
										"PerfTraceCTQ",
										0
									},
									{
										"InlineFeedback",
										1
									},
									{
										"PerfNavTime",
										2
									},
									{
										"PerfTrace",
										3
									},
									{
										"Performance",
										4
									},
									{
										"Core.Models.Sync.ModuleStats",
										5
									},
									{
										"Core.Models.Sync.MailSyncReachedLimit",
										6
									},
									{
										"PageDataPayload",
										7
									},
									{
										"Core.Models.Sync.FolderActiveSyncingAgain",
										8
									},
									{
										"Action",
										9
									},
									{
										"ActionRecordDatapoint",
										10
									}
								};
							}
							int num2;
							if (<PrivateImplementationDetails>{4041ACEF-19DC-4DB7-9F0C-17DA902ABE6A}.$$method0x6001a88-1.TryGetValue(id, out num2))
							{
								switch (num2)
								{
								case 0:
									clientLogEvent2.UpdateDeviceInfo(userContext.LogEventCommonData);
									clientLogEvent2.UpdateTenantInfo(userContext);
									clientLogEvent2.UpdateNetid(userContext);
									clientLogEvent2.UpdateMailboxGuid(userContext.ExchangePrincipal);
									clientLogEvent2.UpdateDatabaseInfo(userContext);
									clientLogEvent2.UpdateFlightInfo(userContext.LogEventCommonData);
									clientLogEvent2.UpdateClientInfo(userContext.LogEventCommonData);
									clientLogEvent2.UpdateUserAgent(userContext.UserAgent);
									clientLogEvent2.UpdateTenantGuid(userContext.ExchangePrincipal);
									break;
								case 1:
									clientLogEvent2.UpdateDeviceInfo(userContext.LogEventCommonData);
									clientLogEvent2.UpdateTenantInfo(userContext);
									clientLogEvent2.UpdateNetid(userContext);
									clientLogEvent2.UpdateMailboxGuid(userContext.ExchangePrincipal);
									clientLogEvent2.UpdateDatabaseInfo(userContext);
									clientLogEvent2.UpdateFlightInfo(userContext.LogEventCommonData);
									clientLogEvent2.UpdateClientInfo(userContext.LogEventCommonData);
									clientLogEvent2.UpdateUserAgent(userContext.UserAgent);
									break;
								case 2:
									clientLogEvent2.UpdatePerformanceNavigationDatapoint(userContext);
									break;
								case 3:
									clientLogEvent2.UpdatePerfTraceDatapoint(userContext);
									break;
								case 4:
									clientLogEvent2.UpdateDeviceInfo(userContext.LogEventCommonData);
									clientLogEvent2.UpdateTenantGuid(userContext.ExchangePrincipal);
									clientLogEvent2.UpdateMailboxGuid(userContext.ExchangePrincipal);
									clientLogEvent2.UpdateUserAgent(userContext.UserAgent);
									break;
								case 5:
								case 6:
									clientLogEvent2.UpdateDeviceInfo(userContext.LogEventCommonData);
									clientLogEvent2.UpdateTenantInfo(userContext);
									clientLogEvent2.UpdateNetid(userContext);
									clientLogEvent2.UpdateMailboxGuid(userContext.ExchangePrincipal);
									clientLogEvent2.UpdateDatabaseInfo(userContext);
									clientLogEvent2.UpdateClientLocaleInfo(userContext.LogEventCommonData);
									break;
								case 7:
								case 8:
									clientLogEvent2.UpdateDeviceInfo(userContext.LogEventCommonData);
									clientLogEvent2.UpdateClientLocaleInfo(userContext.LogEventCommonData);
									break;
								case 9:
								case 10:
									clientLogEvent2.UpdateActionRecordDataPoint(userContext);
									break;
								}
							}
						}
					}
					clientLogEvent2.UpdatePassThroughProxyInfo(this.isFromPassThroughProxy);
					clientLogEventList3.CheckAndAdd(clientLogEvent2);
					clientLogEventList2.CheckAndAdd(clientLogEvent2);
					clientLogEventList4.CheckAndAdd(clientLogEvent2);
				}
			}
			if (clientLogEventList.Count == 1)
			{
				clientLogEventList.Clear();
			}
			if (clientLogEventList2.Count == 1)
			{
				clientLogEventList2.Clear();
			}
			return new Dictionary<DatapointConsumer, LogDatapoint.ClientLogEventList>
			{
				{
					DatapointConsumer.Analytics,
					clientLogEventList
				},
				{
					DatapointConsumer.Inference,
					clientLogEventList3
				},
				{
					DatapointConsumer.Diagnostics,
					clientLogEventList2
				},
				{
					DatapointConsumer.Watson,
					clientLogEventList4
				}
			};
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00067D78 File Offset: 0x00065F78
		private Datapoint GetChunkHeaderDatapoint(string time)
		{
			HttpContext httpContext = base.CallContext.HttpContext;
			if (httpContext != null)
			{
				string[] keys = new string[]
				{
					"UA"
				};
				string[] values = new string[]
				{
					httpContext.Request.UserAgent
				};
				return new Datapoint(DatapointConsumer.Analytics, "DatapointChunkHeader", time, keys, values);
			}
			return new Datapoint(DatapointConsumer.Analytics, "DatapointChunkHeader", time, null, null);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00067DDC File Offset: 0x00065FDC
		private DatapointConsumer GetEnabledConsumers(InstrumentationSettings settings)
		{
			DatapointConsumer datapointConsumer = DatapointConsumer.None;
			if (settings.CoreAnalyticsProbability > 0f || settings.AnalyticsProbability > 0f)
			{
				datapointConsumer |= DatapointConsumer.Analytics;
			}
			if (settings.IsInferenceEnabled)
			{
				datapointConsumer |= DatapointConsumer.Inference;
			}
			if (settings.IsClientWatsonEnabled)
			{
				datapointConsumer |= DatapointConsumer.Watson;
			}
			if (settings.DefaultTraceLevel != TraceLevel.Off)
			{
				datapointConsumer |= DatapointConsumer.Diagnostics;
			}
			return datapointConsumer;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00067E9C File Offset: 0x0006609C
		private IList<Activity> CreateInferenceActivities(IList<ClientLogEvent> clientLogEvents)
		{
			UserContext userContext = this.GetUserContext();
			IList<Activity> activities = null;
			this.LogStoreExceptions("CreateInferenceActivities", delegate
			{
				IMailboxSession mailboxSession = this.GetMailboxSession();
				string clientAddress = this.GetClientAddress();
				string userAgent = this.CallContext.UserAgent;
				ActivityConverter activityConverter = new ActivityConverter(userContext, mailboxSession, clientAddress, userAgent, this.clientVersion);
				activities = activityConverter.GetActivities(clientLogEvents);
			});
			return activities ?? new List<Activity>();
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00067F14 File Offset: 0x00066114
		private void WriteInferenceActivities(IList<Activity> activities, IActivityLogger inferenceLogger)
		{
			this.LogStoreExceptions("WriteInferenceActivities", delegate
			{
				inferenceLogger.Log(activities);
			});
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00067F4C File Offset: 0x0006614C
		private string GetUserContextId()
		{
			if (base.CallContext.HttpContext == null)
			{
				return string.Empty;
			}
			UserContextCookie userContextCookie = UserContextCookie.GetUserContextCookie(base.CallContext.HttpContext);
			if (userContextCookie != null)
			{
				return userContextCookie.CookieValue;
			}
			return string.Empty;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00067F8C File Offset: 0x0006618C
		private void LogStoreExceptions(string operation, Action action)
		{
			try
			{
				action();
			}
			catch (StoragePermanentException ex)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(base.CallContext.ProtocolLog, ex, "LogDatapoint_LogStoreExceptions_" + operation);
			}
			catch (StorageTransientException ex2)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(base.CallContext.ProtocolLog, ex2, "LogDatapoint_LogStoreExceptions_" + operation);
			}
		}

		// Token: 0x04000F37 RID: 3895
		private const string PerformanceNavigationTimingDatapointId = "PerfNavTime";

		// Token: 0x04000F38 RID: 3896
		private const string SessionInfoDatapointId = "SessionInfo";

		// Token: 0x04000F39 RID: 3897
		private const string PerfTraceCTQDatapointId = "PerfTraceCTQ";

		// Token: 0x04000F3A RID: 3898
		private const string PerfTraceDatapointId = "PerfTrace";

		// Token: 0x04000F3B RID: 3899
		private const string ActionRecordDatapointId = "Action";

		// Token: 0x04000F3C RID: 3900
		private const string ActionRecordOldDataPointId = "ActionRecordDatapoint";

		// Token: 0x04000F3D RID: 3901
		private const string InlineFeedbackDatapointId = "InlineFeedback";

		// Token: 0x04000F3E RID: 3902
		private const string PerformanceDatapointId = "Performance";

		// Token: 0x04000F3F RID: 3903
		private const string SyncModuleStatsId = "Core.Models.Sync.ModuleStats";

		// Token: 0x04000F40 RID: 3904
		private const string SyncMailSyncReachedLimitId = "Core.Models.Sync.MailSyncReachedLimit";

		// Token: 0x04000F41 RID: 3905
		private const string SyncFolderActiveSyncingAgainId = "Core.Models.Sync.FolderActiveSyncingAgain";

		// Token: 0x04000F42 RID: 3906
		private const string PageDataPayloadId = "PageDataPayload";

		// Token: 0x04000F43 RID: 3907
		private const string ClientPerfCounterId = "PC";

		// Token: 0x04000F44 RID: 3908
		internal const string LogNamespace = "LD";

		// Token: 0x04000F45 RID: 3909
		private static readonly string LogDatapointActionName = typeof(LogDatapoint).Name;

		// Token: 0x04000F46 RID: 3910
		private readonly Datapoint[] datapoints;

		// Token: 0x04000F47 RID: 3911
		private readonly Action<IEnumerable<ILogEvent>> analyticsLogger;

		// Token: 0x04000F48 RID: 3912
		private readonly Action<IEnumerable<ILogEvent>> diagnosticsLogger;

		// Token: 0x04000F49 RID: 3913
		private readonly ClientWatsonDatapointHandler clientWatsonHandler;

		// Token: 0x04000F4A RID: 3914
		private readonly bool isOwa;

		// Token: 0x04000F4B RID: 3915
		private IMailboxSession mailboxSession;

		// Token: 0x04000F4C RID: 3916
		private readonly bool isFromPassThroughProxy;

		// Token: 0x04000F4D RID: 3917
		private string clientVersion;

		// Token: 0x04000F4E RID: 3918
		private readonly string serverVersion;

		// Token: 0x02000339 RID: 825
		private sealed class ClientLogEventList
		{
			// Token: 0x06001B5C RID: 7004 RVA: 0x00068014 File Offset: 0x00066214
			public ClientLogEventList(DatapointConsumer consumerFilter, DatapointConsumer enabledConsumers, ClientLogEvent header = null)
			{
				this.Events = ((header == null) ? new List<ClientLogEvent>() : new List<ClientLogEvent>
				{
					header
				});
				this.DatapointTotalSize = 0;
				this.consumerFilter = consumerFilter;
				this.consumerEnabled = ((consumerFilter & enabledConsumers) != DatapointConsumer.None);
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00068062 File Offset: 0x00066262
			// (set) Token: 0x06001B5E RID: 7006 RVA: 0x0006806A File Offset: 0x0006626A
			public IList<ClientLogEvent> Events { get; private set; }

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x06001B5F RID: 7007 RVA: 0x00068073 File Offset: 0x00066273
			// (set) Token: 0x06001B60 RID: 7008 RVA: 0x0006807B File Offset: 0x0006627B
			public int DatapointTotalSize { get; private set; }

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x06001B61 RID: 7009 RVA: 0x00068084 File Offset: 0x00066284
			public int Count
			{
				get
				{
					return this.Events.Count;
				}
			}

			// Token: 0x06001B62 RID: 7010 RVA: 0x00068091 File Offset: 0x00066291
			public bool CheckAndAdd(ClientLogEvent clientLogEvent)
			{
				if (this.consumerEnabled && clientLogEvent.IsForConsumer(this.consumerFilter))
				{
					this.Events.Add(clientLogEvent);
					this.DatapointTotalSize += clientLogEvent.InnerDatapoint.Size;
					return true;
				}
				return false;
			}

			// Token: 0x06001B63 RID: 7011 RVA: 0x000680D0 File Offset: 0x000662D0
			public void Clear()
			{
				this.Events.Clear();
			}

			// Token: 0x04000F4F RID: 3919
			private readonly DatapointConsumer consumerFilter;

			// Token: 0x04000F50 RID: 3920
			private readonly bool consumerEnabled;
		}
	}
}
