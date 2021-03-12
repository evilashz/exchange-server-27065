using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000003 RID: 3
	internal class StoreDriverDeliveryDiagnostics : IDiagnosable
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000027A8 File Offset: 0x000009A8
		internal static Breadcrumbs<DatabaseHealthBreadcrumb> HealthHistory
		{
			get
			{
				return StoreDriverDeliveryDiagnostics.healthHistory;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000027AF File Offset: 0x000009AF
		protected static TroubleshootingContext TroubleshootingContext
		{
			get
			{
				return StoreDriverDeliveryDiagnostics.transportTroubleshootingContext;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000027B6 File Offset: 0x000009B6
		protected static DeliveriesInProgress HangDetector
		{
			get
			{
				return StoreDriverDeliveryDiagnostics.hangDetector;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000027BD File Offset: 0x000009BD
		protected static int DeliveringThreads
		{
			get
			{
				return StoreDriverDeliveryDiagnostics.deliveringThreads;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000027C6 File Offset: 0x000009C6
		public static void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			StoreDriverDeliveryDiagnostics.eventLogger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000027D6 File Offset: 0x000009D6
		public static void LogEvent(IOrganizationIdForEventLog organizationId, ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			StoreDriverDeliveryDiagnostics.eventLogger.LogEvent(organizationId, tuple, periodicKey, messageArgs);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000027E7 File Offset: 0x000009E7
		public static void Initialize()
		{
			StoreDriverDeliveryDiagnostics.hangDetector = new DeliveriesInProgress(Components.Configuration.LocalServer.MaxConcurrentMailboxDeliveries);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002804 File Offset: 0x00000A04
		public static bool DetectDeliveryHang(out string hangBreadcrumb)
		{
			if (StoreDriverDeliveryDiagnostics.HangDetector == null)
			{
				StoreDriverDeliveryDiagnostics.Diag.TraceDebug(0L, "Store driver component is being initialized and HangDetector hasn't been created yet, we are not hanging.");
				hangBreadcrumb = null;
				return false;
			}
			ulong num;
			MailItemDeliver mailItemDeliver;
			if (StoreDriverDeliveryDiagnostics.HangDetector.DetectHang(Components.Configuration.AppConfig.RemoteDelivery.StoreDriverRecipientDeliveryHangThreshold, out num, out mailItemDeliver))
			{
				hangBreadcrumb = mailItemDeliver.DeliveryBreadcrumb.ToString();
				string[] messageArgs = new string[]
				{
					num.ToString(),
					(mailItemDeliver.Recipient == null) ? "NotSet" : mailItemDeliver.Recipient.Email.ToString(),
					Components.Configuration.AppConfig.RemoteDelivery.StoreDriverRecipientDeliveryHangThreshold.ToString(),
					hangBreadcrumb
				};
				if (mailItemDeliver.MbxTransportMailItem != null)
				{
					PoisonMessage.Context = new MessageContext(mailItemDeliver.MbxTransportMailItem.RecordId, mailItemDeliver.MbxTransportMailItem.InternetMessageId, MessageProcessingSource.StoreDriverLocalDelivery);
				}
				StoreDriverDeliveryDiagnostics.LogEvent(MailboxTransportEventLogConstants.Tuple_DeliveryHang, "DeliveryHang", messageArgs);
				return true;
			}
			hangBreadcrumb = null;
			return false;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000290B File Offset: 0x00000B0B
		public static void UpdateDeliveryThreadCounters()
		{
			DeliveryThrottling.Instance.UpdateMdbThreadCounters();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002917 File Offset: 0x00000B17
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "StoreDriverDelivery";
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002920 File Offset: 0x00000B20
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			bool flag = parameters.Argument.IndexOf("exceptions", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("callstacks", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag4 = flag3 || parameters.Argument.IndexOf("basic", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag5 = parameters.Argument.IndexOf("currentThreads", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag6 = flag4 || parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag7 = !flag6 || parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			if (flag7)
			{
				xelement.Add(new XElement("help", "Supported arguments: config, basic, verbose, exceptions, callstacks, currentThreads, help."));
			}
			if (flag4)
			{
				xelement.Add(new XElement("deliveringThreads", StoreDriverDeliveryDiagnostics.deliveringThreads));
				xelement.Add(DeliveryThrottling.Instance.DeliveryServerDiagnostics);
				xelement.Add(DeliveryThrottling.Instance.DeliveryDatabaseDiagnostics);
				xelement.Add(DeliveryThrottling.Instance.DeliveryRecipientDiagnostics);
			}
			if (flag5 && StoreDriverDeliveryDiagnostics.HangDetector != null)
			{
				xelement.Add(StoreDriverDeliveryDiagnostics.HangDetector.GetDiagnosticInfo());
			}
			if (flag3)
			{
				XElement xelement2 = new XElement("HealthHistory");
				foreach (DatabaseHealthBreadcrumb databaseHealthBreadcrumb in ((IEnumerable<DatabaseHealthBreadcrumb>)StoreDriverDeliveryDiagnostics.healthHistory))
				{
					xelement2.Add(databaseHealthBreadcrumb.GetDiagnosticInfo());
				}
				xelement.Add(xelement2);
				xelement.Add(MailItemDeliver.GetDiagnosticInfo());
				string content;
				using (StringWriter stringWriter = new StringWriter(new StringBuilder(1024)))
				{
					MemoryTraceBuilder memoryTraceBuilder = StoreDriverDeliveryDiagnostics.TroubleshootingContext.MemoryTraceBuilder;
					if (memoryTraceBuilder == null)
					{
						stringWriter.Write("No traces were flushed from any thread yet, or in-memory tracing is disabled.");
					}
					else
					{
						memoryTraceBuilder.Dump(stringWriter, true, true);
					}
					content = stringWriter.ToString();
				}
				xelement.Add(new XElement("tracing", content));
			}
			if (flag)
			{
				StoreDriverDeliveryDiagnostics.DumpExceptionStatistics(xelement);
			}
			if (flag2)
			{
				StoreDriverDeliveryDiagnostics.DumpExceptionCallstacks(xelement);
			}
			return xelement;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002B9C File Offset: 0x00000D9C
		internal static void DumpExceptionStatistics(XElement storeDriverElement)
		{
			XElement xelement = new XElement("ExceptionStatisticRecords");
			lock (StoreDriverDeliveryDiagnostics.deliveryExceptionStatisticRecords)
			{
				XElement xelement2 = new XElement("DeliveryExceptionStatistics");
				foreach (KeyValuePair<string, StoreDriverDeliveryDiagnostics.ExceptionStatisticRecord<StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord>> keyValuePair in StoreDriverDeliveryDiagnostics.deliveryExceptionStatisticRecords)
				{
					XElement xelement3 = new XElement("Exception");
					xelement3.Add(new XAttribute("HitCount", keyValuePair.Value.CountSinceServiceStart));
					xelement3.Add(new XAttribute("Type", keyValuePair.Key));
					foreach (StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord deliveryOccurrenceRecord in keyValuePair.Value.LastOccurrences)
					{
						xelement3.Add(deliveryOccurrenceRecord.GetDiagnosticInfo());
					}
					xelement2.Add(xelement3);
				}
				xelement.Add(xelement2);
			}
			storeDriverElement.Add(xelement);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002CF4 File Offset: 0x00000EF4
		internal static void DumpExceptionCallstacks(XElement storeDriverElement)
		{
			XElement xelement = new XElement("ExceptionCallstackRecords");
			lock (StoreDriverDeliveryDiagnostics.exceptionDeliveryCallstackRecords)
			{
				XElement xelement2 = new XElement("DeliveryCallstackRecords");
				foreach (KeyValuePair<MessageAction, Dictionary<string, StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord>> keyValuePair in StoreDriverDeliveryDiagnostics.exceptionDeliveryCallstackRecords)
				{
					XElement xelement3 = new XElement(keyValuePair.Key.ToString());
					foreach (KeyValuePair<string, StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord> keyValuePair2 in keyValuePair.Value)
					{
						XElement diagnosticInfo = keyValuePair2.Value.GetDiagnosticInfo();
						XElement xelement4 = new XElement("Callstack", keyValuePair2.Key);
						xelement4.Add(new XAttribute("Length", keyValuePair2.Key.Length));
						diagnosticInfo.Add(xelement4);
						xelement3.Add(diagnosticInfo);
					}
					xelement2.Add(xelement3);
				}
				xelement.Add(xelement2);
			}
			storeDriverElement.Add(xelement);
			storeDriverElement.Add(new XElement("ExceptionCallstackTrappedBySubstring", StoreDriverDeliveryDiagnostics.exceptionCallstackTrappedBySubstring));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002EA4 File Offset: 0x000010A4
		internal static void RecordExceptionForDiagnostics(MessageStatus messageStatus, IMessageConverter messageConverter)
		{
			if (messageStatus.Exception != null && (messageStatus.Action == MessageAction.NDR || messageStatus.Action == MessageAction.Retry || messageStatus.Action == MessageAction.RetryQueue || messageStatus.Action == MessageAction.Reroute || messageStatus.Action == MessageAction.RetryMailboxServer || messageStatus.Action == MessageAction.Skip))
			{
				StoreDriverDeliveryDiagnostics.UpdateDeliveryExceptionStatisticRecords(messageStatus, Components.TransportAppConfig.RemoteDelivery.MaxStoreDriverDeliveryExceptionOccurrenceHistoryPerException, Components.Configuration.AppConfig.RemoteDelivery.MaxStoreDriverDeliveryExceptionCallstackHistoryPerBucket, (MailItemDeliver)messageConverter);
				StoreDriverDeliveryDiagnostics.TrapCallstackWithConfiguredSubstring(messageStatus, Components.TransportAppConfig.RemoteDelivery.StoreDriverExceptionCallstackToTrap);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F34 File Offset: 0x00001134
		internal static void UpdateDeliveryExceptionStatisticRecords(MessageStatus messageStatus, int lastOccurrencesPerException, int callstacksPerBucket, MailItemDeliver mailItemDeliver)
		{
			StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord deliveryOccurrenceRecord = new StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord(DateTime.UtcNow, mailItemDeliver.MbxTransportMailItem.DatabaseName, StoreDriverDelivery.MailboxServerFqdn, mailItemDeliver.MbxTransportMailItem.InternetMessageId, mailItemDeliver.Recipient.Email, mailItemDeliver.RecipientStartTime, mailItemDeliver.SessionId, mailItemDeliver.MbxTransportMailItem.MimeSize, mailItemDeliver.MbxTransportMailItem.MailItemRecipientCount, mailItemDeliver.MbxTransportMailItem.MimeSender, mailItemDeliver.MbxTransportMailItem.RoutingTimeStamp, mailItemDeliver.Stage);
			if (lastOccurrencesPerException > 0)
			{
				string key = StoreDriverDeliveryDiagnostics.GenerateExceptionKey(messageStatus);
				lock (StoreDriverDeliveryDiagnostics.deliveryExceptionStatisticRecords)
				{
					StoreDriverDeliveryDiagnostics.ExceptionStatisticRecord<StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord> value;
					if (!StoreDriverDeliveryDiagnostics.deliveryExceptionStatisticRecords.TryGetValue(key, out value))
					{
						value = default(StoreDriverDeliveryDiagnostics.ExceptionStatisticRecord<StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord>);
						value.LastOccurrences = new Queue<StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord>(lastOccurrencesPerException);
					}
					if (value.LastOccurrences.Count == lastOccurrencesPerException)
					{
						value.LastOccurrences.Dequeue();
					}
					value.LastOccurrences.Enqueue(deliveryOccurrenceRecord);
					value.CountSinceServiceStart++;
					StoreDriverDeliveryDiagnostics.deliveryExceptionStatisticRecords[key] = value;
				}
			}
			StoreDriverDeliveryDiagnostics.UpdateDeliveryExceptionCallstackRecords(messageStatus, callstacksPerBucket, deliveryOccurrenceRecord);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003060 File Offset: 0x00001260
		internal static void TrapCallstackWithConfiguredSubstring(MessageStatus messageStatus, string substringOfCallstackToTrap)
		{
			if (!string.IsNullOrEmpty(substringOfCallstackToTrap))
			{
				string text = messageStatus.Exception.ToString();
				if (0 <= text.IndexOf(substringOfCallstackToTrap))
				{
					StoreDriverDeliveryDiagnostics.exceptionCallstackTrappedBySubstring = text;
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003091 File Offset: 0x00001291
		internal static void IncrementDeliveringThreads()
		{
			Interlocked.Increment(ref StoreDriverDeliveryDiagnostics.deliveringThreads);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000309E File Offset: 0x0000129E
		internal static void DecrementDeliveringThreads()
		{
			Interlocked.Decrement(ref StoreDriverDeliveryDiagnostics.deliveringThreads);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030AC File Offset: 0x000012AC
		private static string GenerateExceptionKey(MessageStatus messageStatus)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(messageStatus.Action.ToString());
			stringBuilder.Append(":");
			stringBuilder.Append(messageStatus.Exception.GetType().FullName);
			Exception ex = messageStatus.Exception;
			if (ex is SmtpResponseException)
			{
				stringBuilder.Append(";");
				stringBuilder.Append(messageStatus.Response.StatusCode);
				stringBuilder.Append(";");
				stringBuilder.Append(messageStatus.Response.EnhancedStatusCode);
			}
			while (ex.InnerException != null)
			{
				stringBuilder.Append("~");
				stringBuilder.Append(ex.InnerException.GetType().Name);
				ex = ex.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000318C File Offset: 0x0000138C
		private static void UpdateDeliveryExceptionCallstackRecords(MessageStatus messageStatus, int callstacksPerBucket, StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord occurrenceRecord)
		{
			if (callstacksPerBucket > 0)
			{
				string key = messageStatus.Exception.ToString();
				lock (StoreDriverDeliveryDiagnostics.exceptionDeliveryCallstackRecords)
				{
					Dictionary<string, StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord> dictionary;
					if (!StoreDriverDeliveryDiagnostics.exceptionDeliveryCallstackRecords.TryGetValue(messageStatus.Action, out dictionary))
					{
						dictionary = new Dictionary<string, StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord>(callstacksPerBucket);
						StoreDriverDeliveryDiagnostics.exceptionDeliveryCallstackRecords[messageStatus.Action] = dictionary;
					}
					StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord deliveryOccurrenceRecord;
					if (!dictionary.TryGetValue(key, out deliveryOccurrenceRecord) && dictionary.Count == callstacksPerBucket)
					{
						DateTime t = DateTime.MaxValue;
						string key2 = null;
						foreach (KeyValuePair<string, StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord> keyValuePair in dictionary)
						{
							if (keyValuePair.Value.Timestamp < t)
							{
								t = keyValuePair.Value.Timestamp;
								key2 = keyValuePair.Key;
							}
						}
						dictionary.Remove(key2);
					}
					dictionary[key] = occurrenceRecord;
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032A0 File Offset: 0x000014A0
		internal static void ClearExceptionRecords()
		{
			lock (StoreDriverDeliveryDiagnostics.exceptionDeliveryCallstackRecords)
			{
				StoreDriverDeliveryDiagnostics.exceptionDeliveryCallstackRecords.Clear();
			}
			lock (StoreDriverDeliveryDiagnostics.deliveryExceptionStatisticRecords)
			{
				StoreDriverDeliveryDiagnostics.deliveryExceptionStatisticRecords.Clear();
			}
			StoreDriverDeliveryDiagnostics.exceptionCallstackTrappedBySubstring = null;
		}

		// Token: 0x0400000B RID: 11
		internal const int EstimatedDescriptionStringLength = 400;

		// Token: 0x0400000C RID: 12
		private const string ProcessAccessManagerComponentName = "StoreDriverDelivery";

		// Token: 0x0400000D RID: 13
		protected static readonly Trace Diag = ExTraceGlobals.StoreDriverDeliveryTracer;

		// Token: 0x0400000E RID: 14
		private static DeliveriesInProgress hangDetector;

		// Token: 0x0400000F RID: 15
		private static TroubleshootingContext transportTroubleshootingContext = new TroubleshootingContext("Transport");

		// Token: 0x04000010 RID: 16
		private static ExEventLog eventLogger = new ExEventLog(new Guid("{D81003EF-1A7B-4AF0-BA18-236DB5A83114}"), "MSExchange Store Driver Delivery");

		// Token: 0x04000011 RID: 17
		private static Breadcrumbs<DatabaseHealthBreadcrumb> healthHistory = new Breadcrumbs<DatabaseHealthBreadcrumb>(32);

		// Token: 0x04000012 RID: 18
		private static Dictionary<MessageAction, Dictionary<string, StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord>> exceptionDeliveryCallstackRecords = new Dictionary<MessageAction, Dictionary<string, StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord>>(6);

		// Token: 0x04000013 RID: 19
		private static Dictionary<string, StoreDriverDeliveryDiagnostics.ExceptionStatisticRecord<StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord>> deliveryExceptionStatisticRecords = new Dictionary<string, StoreDriverDeliveryDiagnostics.ExceptionStatisticRecord<StoreDriverDeliveryDiagnostics.DeliveryOccurrenceRecord>>(100);

		// Token: 0x04000014 RID: 20
		private static string exceptionCallstackTrappedBySubstring;

		// Token: 0x04000015 RID: 21
		private static volatile int deliveringThreads;

		// Token: 0x02000004 RID: 4
		private struct ExceptionStatisticRecord<T>
		{
			// Token: 0x04000016 RID: 22
			internal Queue<T> LastOccurrences;

			// Token: 0x04000017 RID: 23
			internal int CountSinceServiceStart;
		}

		// Token: 0x02000005 RID: 5
		private struct DeliveryOccurrenceRecord
		{
			// Token: 0x0600007B RID: 123 RVA: 0x00003388 File Offset: 0x00001588
			internal DeliveryOccurrenceRecord(DateTime timestamp, string mailboxDatabase, string mailboxServer, string messageID, RoutingAddress recipient, ExDateTime recipientStartTime, ulong sessionID, long messageSize, int recipientCount, RoutingAddress sender, DateTime enqueuedTime, MailItemDeliver.DeliveryStage stage)
			{
				this.timestamp = timestamp;
				this.mailboxDatabase = mailboxDatabase;
				this.mailboxServer = mailboxServer;
				this.messageID = messageID;
				this.recipient = recipient;
				this.recipientStartTime = recipientStartTime;
				this.sessionID = sessionID;
				this.messageSize = messageSize;
				this.recipientCount = recipientCount;
				this.sender = sender;
				this.enqueuedTime = enqueuedTime;
				this.stage = stage;
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x0600007C RID: 124 RVA: 0x000033F2 File Offset: 0x000015F2
			internal DateTime Timestamp
			{
				get
				{
					return this.timestamp;
				}
			}

			// Token: 0x0600007D RID: 125 RVA: 0x000033FC File Offset: 0x000015FC
			internal XElement GetDiagnosticInfo()
			{
				return new XElement("Occurrence", new object[]
				{
					new XElement("HitUtc", this.timestamp.ToString(CultureInfo.InvariantCulture)),
					new XElement("MDB", this.mailboxDatabase),
					new XElement("MailboxServer", this.mailboxServer),
					new XElement("MessageID", this.messageID),
					new XElement("Recipient", this.recipient),
					new XElement("RecipientStartTime", this.recipientStartTime.ToString(CultureInfo.InvariantCulture)),
					new XElement("SessionID", this.sessionID),
					new XElement("MessageSize", this.messageSize),
					new XElement("RecipientCount", this.recipientCount),
					new XElement("Sender", this.sender),
					new XElement("EnqueuedTime", this.enqueuedTime.ToString(CultureInfo.InvariantCulture))
				});
			}

			// Token: 0x04000018 RID: 24
			private DateTime timestamp;

			// Token: 0x04000019 RID: 25
			private string mailboxDatabase;

			// Token: 0x0400001A RID: 26
			private string mailboxServer;

			// Token: 0x0400001B RID: 27
			private string messageID;

			// Token: 0x0400001C RID: 28
			private RoutingAddress recipient;

			// Token: 0x0400001D RID: 29
			private ExDateTime recipientStartTime;

			// Token: 0x0400001E RID: 30
			private ulong sessionID;

			// Token: 0x0400001F RID: 31
			private long messageSize;

			// Token: 0x04000020 RID: 32
			private int recipientCount;

			// Token: 0x04000021 RID: 33
			private RoutingAddress sender;

			// Token: 0x04000022 RID: 34
			private DateTime enqueuedTime;

			// Token: 0x04000023 RID: 35
			private MailItemDeliver.DeliveryStage stage;
		}
	}
}
