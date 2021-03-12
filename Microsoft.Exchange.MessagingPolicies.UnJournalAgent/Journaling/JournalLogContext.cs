using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000005 RID: 5
	internal class JournalLogContext : IDisposable
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000293C File Offset: 0x00000B3C
		public JournalLogContext(string source, string eventTopic, string messageId, MailItem mailItem)
		{
			this.source = source;
			this.eventTopic = eventTopic;
			this.Status = "Processing";
			this.messageId = (string.IsNullOrEmpty(messageId) ? string.Empty : messageId);
			this.internalMesageId = 0L;
			this.externalOrganizationId = Guid.Empty;
			this.logData = new List<KeyValuePair<string, object>>();
			try
			{
				if (mailItem != null)
				{
					TransportMailItem transportMailItem = ((ITransportMailItemWrapperFacade)mailItem).TransportMailItem as TransportMailItem;
					if (transportMailItem != null && !transportMailItem.IsRowDeleted)
					{
						this.internalMesageId = mailItem.InternalMessageId;
						this.externalOrganizationId = transportMailItem.ExternalOrganizationId;
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.JournalingTracer.TraceError((long)this.GetHashCode(), "JournalLogContext failed with exception: " + ex);
				JournalLogContext.JournalLog.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_JournalingLogException, null, new object[]
				{
					"JournalLogContext",
					ex.ToString()
				});
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002A34 File Offset: 0x00000C34
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002A3C File Offset: 0x00000C3C
		private string Status { get; set; }

		// Token: 0x06000023 RID: 35 RVA: 0x00002A48 File Offset: 0x00000C48
		public void AddParameter(string key, params object[] objects)
		{
			if (!string.IsNullOrEmpty(key))
			{
				string text = string.Empty;
				try
				{
					if (objects != null && objects.Any<object>())
					{
						List<object> list = new List<object>();
						foreach (object obj in objects)
						{
							if (obj != null)
							{
								IEnumerable<object> enumerable = obj as IEnumerable<object>;
								if (!(obj is string) && enumerable != null)
								{
									list.AddRange(enumerable);
								}
								else
								{
									list.Add(obj);
								}
							}
						}
						text = string.Join<object>("|", list);
					}
					this.logData.Add(new KeyValuePair<string, object>(key, (text.Length > 256) ? text.Substring(0, 256) : text));
				}
				catch (Exception ex)
				{
					ExTraceGlobals.JournalingTracer.TraceError((long)this.GetHashCode(), "JournalLog AddParameter failed with exception: " + ex);
					JournalLogContext.JournalLog.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_JournalingLogException, null, new object[]
					{
						"AddParameter",
						ex.ToString()
					});
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B54 File Offset: 0x00000D54
		public void LogAsSkipped(string key, params object[] objects)
		{
			this.Status = "Skipped";
			this.AddParameter(key, objects);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B69 File Offset: 0x00000D69
		public void LogAsRetriableError(params object[] objects)
		{
			this.Status = "RetriableError";
			this.AddParameter("RetriableError", objects);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B82 File Offset: 0x00000D82
		public void LogAsFatalError(params object[] objects)
		{
			this.Status = "FatalError";
			this.AddParameter("FatalError", objects);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B9C File Offset: 0x00000D9C
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				if (string.IsNullOrEmpty(this.Status) || (!this.Status.Equals("Skipped", StringComparison.CurrentCultureIgnoreCase) && !this.Status.Equals("FatalError", StringComparison.CurrentCultureIgnoreCase) && !this.Status.Equals("RetriableError", StringComparison.CurrentCultureIgnoreCase)))
				{
					this.Status = "Processed";
				}
				JournalLogContext.JournalLog.Instance.LogEvent(this.source, this.externalOrganizationId, this.eventTopic, this.Status, this.internalMesageId, this.messageId, this.logData);
			}
		}

		// Token: 0x04000032 RID: 50
		private const int MaxCustomStringLength = 256;

		// Token: 0x04000033 RID: 51
		private readonly string source;

		// Token: 0x04000034 RID: 52
		private readonly string eventTopic;

		// Token: 0x04000035 RID: 53
		private readonly string messageId;

		// Token: 0x04000036 RID: 54
		private readonly long internalMesageId;

		// Token: 0x04000037 RID: 55
		private readonly Guid externalOrganizationId;

		// Token: 0x04000038 RID: 56
		private List<KeyValuePair<string, object>> logData;

		// Token: 0x04000039 RID: 57
		private bool isDisposed;

		// Token: 0x02000006 RID: 6
		internal class Source
		{
			// Token: 0x0400003B RID: 59
			public const string JournalAgent = "JA";

			// Token: 0x0400003C RID: 60
			public const string JournalFilterAgent = "JF";

			// Token: 0x0400003D RID: 61
			public const string UnwrapJournalAgent = "UJA";
		}

		// Token: 0x02000007 RID: 7
		internal class StatusCode
		{
			// Token: 0x0400003E RID: 62
			public const string Processing = "Processing";

			// Token: 0x0400003F RID: 63
			public const string Processed = "Processed";

			// Token: 0x04000040 RID: 64
			public const string Skipped = "Skipped";

			// Token: 0x04000041 RID: 65
			public const string RetriableError = "RetriableError";

			// Token: 0x04000042 RID: 66
			public const string FatalError = "FatalError";
		}

		// Token: 0x02000008 RID: 8
		internal class Tag
		{
			// Token: 0x04000043 RID: 67
			internal const string Configure = "Cfg";

			// Token: 0x04000044 RID: 68
			internal const string RuleId = "RId";

			// Token: 0x04000045 RID: 69
			internal const string JournalReportItem = "JRItem";

			// Token: 0x04000046 RID: 70
			internal const string JournalRecipients = "JRRec";

			// Token: 0x04000047 RID: 71
			internal const string MessageCheck = "MC";

			// Token: 0x04000048 RID: 72
			internal const string ProcessingStatus = "PSt";

			// Token: 0x04000049 RID: 73
			internal const string GccRule = "GR";

			// Token: 0x0400004A RID: 74
			internal const string AlreadyProcessed = "APed";

			// Token: 0x0400004B RID: 75
			internal const string UnwrapJournalTargetRecipients = "UnRec";
		}

		// Token: 0x02000009 RID: 9
		private class JournalLog
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002C58 File Offset: 0x00000E58
			public static JournalLogContext.JournalLog Instance
			{
				get
				{
					if (JournalLogContext.JournalLog.instance == null)
					{
						lock (JournalLogContext.JournalLog.initLock)
						{
							if (JournalLogContext.JournalLog.instance == null)
							{
								JournalLogContext.JournalLog journalLog = new JournalLogContext.JournalLog();
								journalLog.Configure();
								JournalLogContext.JournalLog.instance = journalLog;
							}
						}
					}
					return JournalLogContext.JournalLog.instance;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600002C RID: 44 RVA: 0x00002CB8 File Offset: 0x00000EB8
			public static ExEventLog Logger
			{
				get
				{
					return JournalLogContext.JournalLog.logger;
				}
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002CC0 File Offset: 0x00000EC0
			public void LogEvent(string source, Guid externalOrganizationId, string eventTopic, string status, long internalMessageId, string messageId, IEnumerable<KeyValuePair<string, object>> customData)
			{
				if (!this.initialized)
				{
					return;
				}
				try
				{
					LogRowFormatter logRowFormatter = new LogRowFormatter(JournalLogContext.JournalLog.journalLogSchema);
					logRowFormatter[1] = source;
					logRowFormatter[2] = ((externalOrganizationId == Guid.Empty) ? null : externalOrganizationId);
					logRowFormatter[5] = eventTopic;
					logRowFormatter[6] = status;
					logRowFormatter[3] = internalMessageId;
					logRowFormatter[4] = messageId;
					logRowFormatter[7] = customData;
					this.log.Append(logRowFormatter, 0);
				}
				catch (Exception ex)
				{
					ExTraceGlobals.JournalingTracer.TraceError((long)this.GetHashCode(), "JournalLog LogEvent with exception: " + ex);
					JournalLogContext.JournalLog.logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_JournalingLogException, null, new object[]
					{
						"LogEvent",
						ex.ToString()
					});
				}
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00002DA0 File Offset: 0x00000FA0
			private void Configure()
			{
				Server transportServer = Components.Configuration.LocalServer.TransportServer;
				if (transportServer.JournalLogEnabled)
				{
					if (transportServer.JournalLogPath != null && !string.IsNullOrEmpty(transportServer.JournalLogPath.PathName))
					{
						this.log = new AsyncLog("JournalLog", new LogHeaderFormatter(JournalLogContext.JournalLog.journalLogSchema), "JournalLog");
						this.log.Configure(transportServer.JournalLogPath.PathName, transportServer.JournalLogMaxAge, (long)(transportServer.JournalLogMaxDirectorySize.IsUnlimited ? 0UL : transportServer.JournalLogMaxDirectorySize.Value.ToBytes()), (long)(transportServer.JournalLogMaxFileSize.IsUnlimited ? 0UL : transportServer.JournalLogMaxDirectorySize.Value.ToBytes()), 65536, TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(15.0));
						this.initialized = true;
						return;
					}
					ExTraceGlobals.JournalingTracer.TraceError((long)this.GetHashCode(), "JournalLog configure failed with empty logpath");
					JournalLogContext.JournalLog.logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_JournalingLogConfigureError, null, new object[0]);
				}
			}

			// Token: 0x0400004C RID: 76
			internal const string LogName = "JournalLog";

			// Token: 0x0400004D RID: 77
			internal const string LogType = "Journal Log";

			// Token: 0x0400004E RID: 78
			internal const string LogComponent = "JournalLog";

			// Token: 0x0400004F RID: 79
			private static LogSchema journalLogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Journal Log", Enum.GetNames(typeof(JournalLogContext.JournalLog.JournalLogFields)));

			// Token: 0x04000050 RID: 80
			private static ExEventLog logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");

			// Token: 0x04000051 RID: 81
			private static object initLock = new object();

			// Token: 0x04000052 RID: 82
			private static JournalLogContext.JournalLog instance;

			// Token: 0x04000053 RID: 83
			private bool initialized;

			// Token: 0x04000054 RID: 84
			private AsyncLog log;

			// Token: 0x0200000A RID: 10
			private enum JournalLogFields
			{
				// Token: 0x04000056 RID: 86
				TimeStamp,
				// Token: 0x04000057 RID: 87
				Source,
				// Token: 0x04000058 RID: 88
				ExternalOrganizationId,
				// Token: 0x04000059 RID: 89
				InternalMessageId,
				// Token: 0x0400005A RID: 90
				MessageId,
				// Token: 0x0400005B RID: 91
				Event,
				// Token: 0x0400005C RID: 92
				Status,
				// Token: 0x0400005D RID: 93
				CustomData
			}
		}
	}
}
