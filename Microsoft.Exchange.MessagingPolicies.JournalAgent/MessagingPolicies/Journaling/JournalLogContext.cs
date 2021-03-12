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
	// Token: 0x02000004 RID: 4
	internal class JournalLogContext : IDisposable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002734 File Offset: 0x00000934
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

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000282C File Offset: 0x00000A2C
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002834 File Offset: 0x00000A34
		private string Status { get; set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002840 File Offset: 0x00000A40
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

		// Token: 0x0600000A RID: 10 RVA: 0x0000294C File Offset: 0x00000B4C
		public void LogAsSkipped(string key, params object[] objects)
		{
			this.Status = "Skipped";
			this.AddParameter(key, objects);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002961 File Offset: 0x00000B61
		public void LogAsRetriableError(params object[] objects)
		{
			this.Status = "RetriableError";
			this.AddParameter("RetriableError", objects);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000297A File Offset: 0x00000B7A
		public void LogAsFatalError(params object[] objects)
		{
			this.Status = "FatalError";
			this.AddParameter("FatalError", objects);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002994 File Offset: 0x00000B94
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

		// Token: 0x0400001A RID: 26
		private const int MaxCustomStringLength = 256;

		// Token: 0x0400001B RID: 27
		private readonly string source;

		// Token: 0x0400001C RID: 28
		private readonly string eventTopic;

		// Token: 0x0400001D RID: 29
		private readonly string messageId;

		// Token: 0x0400001E RID: 30
		private readonly long internalMesageId;

		// Token: 0x0400001F RID: 31
		private readonly Guid externalOrganizationId;

		// Token: 0x04000020 RID: 32
		private List<KeyValuePair<string, object>> logData;

		// Token: 0x04000021 RID: 33
		private bool isDisposed;

		// Token: 0x02000005 RID: 5
		internal class Source
		{
			// Token: 0x04000023 RID: 35
			public const string JournalAgent = "JA";

			// Token: 0x04000024 RID: 36
			public const string JournalFilterAgent = "JF";

			// Token: 0x04000025 RID: 37
			public const string UnwrapJournalAgent = "UJA";
		}

		// Token: 0x02000006 RID: 6
		internal class StatusCode
		{
			// Token: 0x04000026 RID: 38
			public const string Processing = "Processing";

			// Token: 0x04000027 RID: 39
			public const string Processed = "Processed";

			// Token: 0x04000028 RID: 40
			public const string Skipped = "Skipped";

			// Token: 0x04000029 RID: 41
			public const string RetriableError = "RetriableError";

			// Token: 0x0400002A RID: 42
			public const string FatalError = "FatalError";
		}

		// Token: 0x02000007 RID: 7
		internal class Tag
		{
			// Token: 0x0400002B RID: 43
			internal const string Configure = "Cfg";

			// Token: 0x0400002C RID: 44
			internal const string RuleId = "RId";

			// Token: 0x0400002D RID: 45
			internal const string JournalReportItem = "JRItem";

			// Token: 0x0400002E RID: 46
			internal const string JournalRecipients = "JRRec";

			// Token: 0x0400002F RID: 47
			internal const string MessageCheck = "MC";

			// Token: 0x04000030 RID: 48
			internal const string ProcessingStatus = "PSt";

			// Token: 0x04000031 RID: 49
			internal const string GccRule = "GR";

			// Token: 0x04000032 RID: 50
			internal const string AlreadyProcessed = "APed";

			// Token: 0x04000033 RID: 51
			internal const string UnwrapJournalTargetRecipients = "UnRec";
		}

		// Token: 0x02000008 RID: 8
		private class JournalLog
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000011 RID: 17 RVA: 0x00002A50 File Offset: 0x00000C50
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

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000012 RID: 18 RVA: 0x00002AB0 File Offset: 0x00000CB0
			public static ExEventLog Logger
			{
				get
				{
					return JournalLogContext.JournalLog.logger;
				}
			}

			// Token: 0x06000013 RID: 19 RVA: 0x00002AB8 File Offset: 0x00000CB8
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

			// Token: 0x06000014 RID: 20 RVA: 0x00002B98 File Offset: 0x00000D98
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

			// Token: 0x04000034 RID: 52
			internal const string LogName = "JournalLog";

			// Token: 0x04000035 RID: 53
			internal const string LogType = "Journal Log";

			// Token: 0x04000036 RID: 54
			internal const string LogComponent = "JournalLog";

			// Token: 0x04000037 RID: 55
			private static LogSchema journalLogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Journal Log", Enum.GetNames(typeof(JournalLogContext.JournalLog.JournalLogFields)));

			// Token: 0x04000038 RID: 56
			private static ExEventLog logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");

			// Token: 0x04000039 RID: 57
			private static object initLock = new object();

			// Token: 0x0400003A RID: 58
			private static JournalLogContext.JournalLog instance;

			// Token: 0x0400003B RID: 59
			private bool initialized;

			// Token: 0x0400003C RID: 60
			private AsyncLog log;

			// Token: 0x02000009 RID: 9
			private enum JournalLogFields
			{
				// Token: 0x0400003E RID: 62
				TimeStamp,
				// Token: 0x0400003F RID: 63
				Source,
				// Token: 0x04000040 RID: 64
				ExternalOrganizationId,
				// Token: 0x04000041 RID: 65
				InternalMessageId,
				// Token: 0x04000042 RID: 66
				MessageId,
				// Token: 0x04000043 RID: 67
				Event,
				// Token: 0x04000044 RID: 68
				Status,
				// Token: 0x04000045 RID: 69
				CustomData
			}
		}
	}
}
