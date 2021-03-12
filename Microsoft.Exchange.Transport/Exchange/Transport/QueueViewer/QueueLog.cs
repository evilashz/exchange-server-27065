using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Transport.QueueViewer
{
	// Token: 0x0200007D RID: 125
	internal class QueueLog
	{
		// Token: 0x06000394 RID: 916 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		public static void Start()
		{
			if (!Components.TransportAppConfig.QueueConfiguration.QueueLoggingEnabled)
			{
				return;
			}
			QueueLog.queueLogSchema = new LogSchema("Microsoft Exchange Server", "15.00.1497.010", "Transport Queue Log", QueueLog.Fields);
			Server transportServer = Components.Configuration.LocalServer.TransportServer;
			string text = (transportServer.QueueLogPath == null) ? null : transportServer.QueueLogPath.PathName;
			if (!string.IsNullOrEmpty(text))
			{
				QueueLog.log = new Log(QueueLogSchema.LogPrefix, new LogHeaderFormatter(QueueLog.queueLogSchema), "QueueLogs");
				QueueLog.log.Configure(text, transportServer.QueueLogMaxAge, (long)(transportServer.QueueLogMaxDirectorySize.IsUnlimited ? 9223372036854775807UL : transportServer.QueueLogMaxDirectorySize.Value.ToBytes()), (long)(transportServer.QueueLogMaxFileSize.IsUnlimited ? 9223372036854775807UL : transportServer.QueueLogMaxFileSize.Value.ToBytes()), 1048576, TimeSpan.FromSeconds(2.0));
				QueueLog.enabled = true;
				QueueLog.LogServiceStart();
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00010000 File Offset: 0x0000E200
		public static void LogServiceStart()
		{
			if (!QueueLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(QueueLog.queueLogSchema);
			logRowFormatter[20] = "Started MsExchangeTransport service.";
			logRowFormatter[2] = QueueLogEventId.START;
			QueueLog.log.Append(logRowFormatter, 0);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010048 File Offset: 0x0000E248
		public static void Log(QueueAggregationInfo queueObject)
		{
			if (!QueueLog.enabled)
			{
				return;
			}
			int num = 0;
			foreach (LocalQueueInfo localQueueInfo in queueObject.QueueInfo)
			{
				QueueLog.LogQueueInfo(localQueueInfo, queueObject.Time, ++num);
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(QueueLog.queueLogSchema);
			logRowFormatter[0] = DateTime.UtcNow;
			logRowFormatter[2] = QueueLogEventId.SUMMARY;
			logRowFormatter[20] = string.Format("TotalMessageCount = {0}; PoisonMessageCount = {1}", queueObject.TotalMessageCount, queueObject.PoisonMessageCount);
			QueueLog.Append(logRowFormatter);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00010108 File Offset: 0x0000E308
		public static void Stop()
		{
			if (QueueLog.log != null)
			{
				QueueLog.enabled = false;
				QueueLog.log.Close();
				QueueLog.log = null;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00010128 File Offset: 0x0000E328
		private static string[] InitializeFields()
		{
			string[] array = new string[Enum.GetValues(typeof(QueueLog.QueueLogField)).Length];
			array[0] = "Timestamp";
			array[1] = "SequenceNumber";
			array[2] = "EventId";
			array[3] = "QueueIdentity";
			array[4] = "Status";
			array[5] = "DeliveryType";
			array[6] = "NextHopDomain";
			array[8] = "MessageCount";
			array[7] = "NextHopKey";
			array[10] = "LockedMessageCount";
			array[9] = "DeferredMessageCount";
			array[11] = "IncomingRate";
			array[14] = "NextHopCategory";
			array[12] = "OutgoingRate";
			array[15] = "RiskLevel";
			array[16] = "OutboundIPPool";
			array[19] = "LastError";
			array[13] = "Velocity";
			array[17] = "NextHopConnector";
			array[18] = "TlsDomain";
			array[20] = "Data";
			array[21] = "CustomData";
			return array;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00010210 File Offset: 0x0000E410
		private static void Append(LogRowFormatter row)
		{
			try
			{
				QueueLog.log.Append(row, -1);
			}
			catch (ObjectDisposedException)
			{
				ExTraceGlobals.MessageTrackingTracer.TraceDebug(0L, "Message tracking append failed with ObjectDisposedException");
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00010250 File Offset: 0x0000E450
		private static void LogQueueInfo(LocalQueueInfo localQueueInfo, DateTime timeToStamp, int sequenceNumber)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(QueueLog.queueLogSchema);
			logRowFormatter[0] = timeToStamp;
			logRowFormatter[1] = sequenceNumber;
			logRowFormatter[2] = QueueLogEventId.QUEUE;
			logRowFormatter[3] = localQueueInfo.QueueIdentity;
			logRowFormatter[4] = localQueueInfo.Status;
			logRowFormatter[5] = localQueueInfo.DeliveryType;
			logRowFormatter[6] = localQueueInfo.NextHopDomain;
			logRowFormatter[8] = localQueueInfo.MessageCount;
			logRowFormatter[7] = localQueueInfo.NextHopKey;
			logRowFormatter[9] = localQueueInfo.DeferredMessageCount;
			logRowFormatter[10] = localQueueInfo.LockedMessageCount;
			logRowFormatter[11] = localQueueInfo.IncomingRate;
			logRowFormatter[12] = localQueueInfo.OutgoingRate;
			logRowFormatter[13] = localQueueInfo.Velocity;
			logRowFormatter[14] = localQueueInfo.NextHopCategory;
			logRowFormatter[15] = localQueueInfo.RiskLevel;
			logRowFormatter[16] = localQueueInfo.OutboundIPPool;
			logRowFormatter[17] = localQueueInfo.NextHopConnector;
			logRowFormatter[18] = localQueueInfo.TlsDomain;
			string text = localQueueInfo.LastError;
			if (!string.IsNullOrEmpty(text) && text.Length > 100)
			{
				text = localQueueInfo.LastError.Substring(0, 100);
			}
			logRowFormatter[19] = text;
			QueueLog.Append(logRowFormatter);
		}

		// Token: 0x04000203 RID: 515
		private const string LogComponentName = "QueueLogs";

		// Token: 0x04000204 RID: 516
		private static readonly string[] Fields = QueueLog.InitializeFields();

		// Token: 0x04000205 RID: 517
		private static LogSchema queueLogSchema;

		// Token: 0x04000206 RID: 518
		private static Log log;

		// Token: 0x04000207 RID: 519
		private static bool enabled;

		// Token: 0x0200007E RID: 126
		internal enum QueueLogField
		{
			// Token: 0x04000209 RID: 521
			Time,
			// Token: 0x0400020A RID: 522
			SequenceNumber,
			// Token: 0x0400020B RID: 523
			EventId,
			// Token: 0x0400020C RID: 524
			QueueIdentity,
			// Token: 0x0400020D RID: 525
			Status,
			// Token: 0x0400020E RID: 526
			DeliveryType,
			// Token: 0x0400020F RID: 527
			NextHopDomain,
			// Token: 0x04000210 RID: 528
			NextHopKey,
			// Token: 0x04000211 RID: 529
			MessageCount,
			// Token: 0x04000212 RID: 530
			DeferredMessageCount,
			// Token: 0x04000213 RID: 531
			LockedMessageCount,
			// Token: 0x04000214 RID: 532
			IncomingRate,
			// Token: 0x04000215 RID: 533
			OutgoingRate,
			// Token: 0x04000216 RID: 534
			Velocity,
			// Token: 0x04000217 RID: 535
			NextHopCategory,
			// Token: 0x04000218 RID: 536
			RiskLevel,
			// Token: 0x04000219 RID: 537
			OutboundIPPool,
			// Token: 0x0400021A RID: 538
			NextHopConnector,
			// Token: 0x0400021B RID: 539
			TlsDomain,
			// Token: 0x0400021C RID: 540
			LastError,
			// Token: 0x0400021D RID: 541
			Data,
			// Token: 0x0400021E RID: 542
			CustomData
		}
	}
}
