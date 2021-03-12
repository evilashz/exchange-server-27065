using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.MailboxTransport.StoreDriverDelivery;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.MailboxTransport.Delivery
{
	// Token: 0x02000042 RID: 66
	internal class DeliveryThrottlingLog : IDeliveryThrottlingLog
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002D7 RID: 727 RVA: 0x0000CE30 File Offset: 0x0000B030
		// (remove) Token: 0x060002D8 RID: 728 RVA: 0x0000CE68 File Offset: 0x0000B068
		public event Action<string> TrackSummary;

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000CE9D File Offset: 0x0000B09D
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		private static string[] InitializeHeaders()
		{
			string[] array = new string[Enum.GetValues(typeof(DeliveryThrottlingLog.Field)).Length];
			array[0] = "dateTime";
			array[1] = "eventID";
			array[2] = "sequenceNumber";
			array[3] = "scope";
			array[4] = "resource";
			array[5] = "threshold";
			array[6] = "impactUnits";
			array[7] = "impact";
			array[8] = "impactRate";
			array[9] = "tenantId";
			array[10] = "recipient";
			array[11] = "mdb";
			array[12] = "mdbHealth";
			array[13] = "customData";
			return array;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000CF45 File Offset: 0x0000B145
		private string GetNextSequenceNumber()
		{
			this.sequenceNumber += 1L;
			return this.sequenceNumber.ToString("X16", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000CF6C File Offset: 0x0000B16C
		private void LogReset()
		{
			if (!this.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(DeliveryThrottlingLog.logSchema);
			logRowFormatter[1] = ThrottlingEvent.Reset;
			this.Append(logRowFormatter);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		private void Append(LogRowFormatter row)
		{
			try
			{
				this.asyncLog.Append(row, 0);
			}
			catch (ObjectDisposedException)
			{
				ExTraceGlobals.StoreDriverDeliveryTracer.TraceDebug(0L, "Appending to Delivery Throttling log failed with ObjectDisposedException");
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		public void Configure(IThrottlingConfig config)
		{
			ArgumentValidator.ThrowIfNull("config", config);
			if (!config.MailboxDeliveryThrottlingEnabled || !config.ThrottlingLogEnabled || config.ThrottlingLogPath == null || string.IsNullOrEmpty(config.ThrottlingLogPath.PathName))
			{
				this.enabled = false;
				return;
			}
			DeliveryThrottlingLog.logSchema = new LogSchema("Microsoft Mailbox Transport Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Mailbox Transport Delivery Throttling Log", DeliveryThrottlingLog.fields);
			this.asyncLog = new AsyncLog("DeliveryThrottling", new LogHeaderFormatter(DeliveryThrottlingLog.logSchema), "DeliveryThrottling");
			this.asyncLog.Configure(config.ThrottlingLogPath.PathName, config.ThrottlingLogMaxAge, (long)(config.ThrottlingLogMaxDirectorySize.IsUnlimited ? 0UL : config.ThrottlingLogMaxDirectorySize.Value.ToBytes()), (long)(config.ThrottlingLogMaxFileSize.IsUnlimited ? 0UL : config.ThrottlingLogMaxFileSize.Value.ToBytes()), config.ThrottlingLogBufferSize, config.ThrottlingLogFlushInterval, config.AsyncLogInterval);
			this.enabled = true;
			this.summaryLogTimer = new GuardedTimer(new TimerCallback(this.SummaryLogWorker), null, config.ThrottlingSummaryLoggingInterval, config.ThrottlingSummaryLoggingInterval);
			this.LogReset();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000D13A File Offset: 0x0000B33A
		public void Close()
		{
			if (this.asyncLog != null)
			{
				this.asyncLog.Close();
				this.asyncLog = null;
			}
			if (this.summaryLogTimer != null)
			{
				this.summaryLogTimer.Dispose(true);
				this.summaryLogTimer = null;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000D171 File Offset: 0x0000B371
		public void SummaryLogWorker(object state)
		{
			if (this.TrackSummary != null)
			{
				this.TrackSummary(this.GetNextSequenceNumber());
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D18C File Offset: 0x0000B38C
		public void LogSummary(string sequenceNumber, ThrottlingScope scope, ThrottlingResource resource, double resourceThreshold, ThrottlingImpactUnits impactUnits, uint impact, double impactRate, Guid externalOrganizationId, string recipient, string mdbName, IList<KeyValuePair<string, double>> mdbHealth, IList<KeyValuePair<string, string>> customData)
		{
			if (!this.enabled)
			{
				return;
			}
			ArgumentValidator.ThrowIfNullOrEmpty("sequenceNumber", sequenceNumber);
			LogRowFormatter logRowFormatter = new LogRowFormatter(DeliveryThrottlingLog.logSchema);
			logRowFormatter[2] = sequenceNumber;
			logRowFormatter[1] = ThrottlingEvent.SummaryThrottle;
			logRowFormatter[3] = scope;
			logRowFormatter[4] = resource;
			if (!resourceThreshold.Equals(double.NaN))
			{
				logRowFormatter[5] = resourceThreshold;
			}
			logRowFormatter[6] = impactUnits;
			logRowFormatter[7] = impact;
			logRowFormatter[8] = impactRate;
			if (externalOrganizationId != Guid.Empty)
			{
				logRowFormatter[9] = externalOrganizationId;
			}
			if (!string.IsNullOrEmpty(recipient))
			{
				logRowFormatter[10] = recipient;
			}
			if (!string.IsNullOrEmpty(mdbName))
			{
				logRowFormatter[11] = mdbName;
			}
			if (mdbHealth != null && mdbHealth.Count > 0)
			{
				logRowFormatter[12] = mdbHealth;
			}
			if (customData != null && customData.Count > 0)
			{
				logRowFormatter[13] = customData;
			}
			this.Append(logRowFormatter);
		}

		// Token: 0x0400011F RID: 287
		private const string ThrottlingLogFilePrefix = "DeliveryThrottling";

		// Token: 0x04000120 RID: 288
		private const string ThrottlingLogComponent = "DeliveryThrottling";

		// Token: 0x04000121 RID: 289
		private const string SoftwareName = "Microsoft Mailbox Transport Server";

		// Token: 0x04000122 RID: 290
		private const string LogType = "Mailbox Transport Delivery Throttling Log";

		// Token: 0x04000123 RID: 291
		private static readonly string[] fields = DeliveryThrottlingLog.InitializeHeaders();

		// Token: 0x04000124 RID: 292
		private static LogSchema logSchema;

		// Token: 0x04000126 RID: 294
		private bool enabled;

		// Token: 0x04000127 RID: 295
		private GuardedTimer summaryLogTimer;

		// Token: 0x04000128 RID: 296
		private AsyncLog asyncLog;

		// Token: 0x04000129 RID: 297
		private long sequenceNumber = DateTime.UtcNow.Ticks;

		// Token: 0x02000043 RID: 67
		private enum Field
		{
			// Token: 0x0400012B RID: 299
			Time,
			// Token: 0x0400012C RID: 300
			EventId,
			// Token: 0x0400012D RID: 301
			SequenceNumber,
			// Token: 0x0400012E RID: 302
			Scope,
			// Token: 0x0400012F RID: 303
			Resource,
			// Token: 0x04000130 RID: 304
			ResourceThreshold,
			// Token: 0x04000131 RID: 305
			ImpactUnits,
			// Token: 0x04000132 RID: 306
			Impact,
			// Token: 0x04000133 RID: 307
			ImpactRate,
			// Token: 0x04000134 RID: 308
			TenantID,
			// Token: 0x04000135 RID: 309
			Recipient,
			// Token: 0x04000136 RID: 310
			MDB,
			// Token: 0x04000137 RID: 311
			MDBHealth,
			// Token: 0x04000138 RID: 312
			CustomData
		}
	}
}
