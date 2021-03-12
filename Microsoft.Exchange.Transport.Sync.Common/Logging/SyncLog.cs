using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x02000084 RID: 132
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncLog : DisposeTrackableBase
	{
		// Token: 0x06000387 RID: 903 RVA: 0x00014DCC File Offset: 0x00012FCC
		public SyncLog(SyncLogConfiguration syncLogConfiguration)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogConfiguration", syncLogConfiguration);
			this.schema = syncLogConfiguration.CreateLogSchema(SyncLog.Fields);
			this.syncLogImplementation = new DiagnosticsLogSyncLogImplementation(this.schema, syncLogConfiguration);
			this.ConfigureLog(syncLogConfiguration.Enabled, syncLogConfiguration.LogFilePath, syncLogConfiguration.AgeQuotaInHours, syncLogConfiguration.DirectorySizeQuota, syncLogConfiguration.PerFileSizeQuota, syncLogConfiguration.SyncLoggingLevel);
			this.blackBox = new SyncLogBlackBox();
			ExTraceGlobals.SyncLogTracer.TraceDebug((long)this.GetHashCode(), "sync Log Created and Configured");
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00014E70 File Offset: 0x00013070
		private SyncLog()
		{
			SyncLogConfiguration syncLogConfiguration = new SyncLogConfiguration(SyncLoggingLevel.Debugging);
			this.schema = syncLogConfiguration.CreateLogSchema(SyncLog.Fields);
			this.syncLogImplementation = new InMemorySyncLogImplementation();
			this.blackBox = new SyncLogBlackBox();
			this.loggingLevel = SyncLoggingLevel.Debugging;
			ExTraceGlobals.SyncLogTracer.TraceDebug((long)this.GetHashCode(), "in memory sync Log Created");
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00014EE4 File Offset: 0x000130E4
		internal static SyncLog InMemorySyncLog
		{
			get
			{
				return SyncLog.inMemorySyncLog;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00014EEB File Offset: 0x000130EB
		internal WatsonReporter WatsonReporter
		{
			get
			{
				base.CheckDisposed();
				return this.watsonReporter;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00014EF9 File Offset: 0x000130F9
		internal SyncLoggingLevel LoggingLevel
		{
			get
			{
				base.CheckDisposed();
				return this.loggingLevel;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00014F08 File Offset: 0x00013108
		public void ConfigureLog(bool enabled, string path, long ageQuota, long directorySizeQuota, long perFileSizeQuota, SyncLoggingLevel loggingLevel)
		{
			base.CheckDisposed();
			lock (this.syncRoot)
			{
				this.syncLogImplementation.Configure(enabled, path, ageQuota, directorySizeQuota, perFileSizeQuota);
				this.loggingLevel = loggingLevel;
			}
			ExTraceGlobals.SyncLogTracer.TraceDebug((long)this.GetHashCode(), "sync Logs Configured");
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00014F78 File Offset: 0x00013178
		public SyncLogSession OpenSession()
		{
			base.CheckDisposed();
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[3] = string.Empty;
			logRowFormatter[5] = string.Empty;
			logRowFormatter[6] = string.Empty;
			return new SyncLogSession(this, logRowFormatter);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00014FC4 File Offset: 0x000131C4
		public GlobalSyncLogSession OpenGlobalSession()
		{
			base.CheckDisposed();
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[3] = string.Empty;
			logRowFormatter[5] = string.Empty;
			logRowFormatter[6] = string.Empty;
			return new GlobalSyncLogSession(this, logRowFormatter);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00015010 File Offset: 0x00013210
		public SyncLogSession OpenSession(AggregationSubscription subscription)
		{
			base.CheckDisposed();
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[3] = ((subscription.UserExchangeMailboxSmtpAddress != null) ? subscription.UserExchangeMailboxSmtpAddress : subscription.UserLegacyDN);
			logRowFormatter[4] = subscription.SubscriptionGuid.ToString();
			logRowFormatter[5] = subscription.SubscriptionType.ToString();
			logRowFormatter[6] = string.Empty;
			return new SyncLogSession(this, logRowFormatter);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00015094 File Offset: 0x00013294
		public SyncLogSession OpenSession(Guid mailboxGuid, Guid subscriptionId)
		{
			base.CheckDisposed();
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[3] = mailboxGuid.ToString();
			logRowFormatter[4] = subscriptionId.ToString();
			logRowFormatter[5] = string.Empty;
			logRowFormatter[6] = string.Empty;
			return new SyncLogSession(this, logRowFormatter);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000150FC File Offset: 0x000132FC
		public SyncLogSession OpenSession(Guid userMailboxGuid, AggregationSubscriptionType subscriptionType, Guid subscriptionId)
		{
			base.CheckDisposed();
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[3] = userMailboxGuid.ToString();
			logRowFormatter[4] = subscriptionId.ToString();
			logRowFormatter[5] = subscriptionType.ToString();
			logRowFormatter[6] = string.Empty;
			return new SyncLogSession(this, logRowFormatter);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00015168 File Offset: 0x00013368
		public void Close()
		{
			base.CheckDisposed();
			lock (this.syncRoot)
			{
				if (this.syncLogImplementation != null)
				{
					this.syncLogImplementation.Close();
					this.syncLogImplementation = null;
				}
			}
			ExTraceGlobals.SyncLogTracer.TraceDebug((long)this.GetHashCode(), "sync Logs Closed");
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000151D8 File Offset: 0x000133D8
		public string GenerateBlackBoxLog()
		{
			if (!Monitor.TryEnter(this.syncRoot, 30000))
			{
				return "Unable to take this.syncRoot lock in GenerateBlackBoxLog.";
			}
			string result;
			try
			{
				if (base.IsDisposed)
				{
				}
				string text = this.blackBox.ToString();
				result = text;
			}
			finally
			{
				Monitor.Exit(this.syncRoot);
			}
			return result;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00015238 File Offset: 0x00013438
		public void Append(LogRowFormatter row)
		{
			base.CheckDisposed();
			lock (this.syncRoot)
			{
				if (this.syncLogImplementation != null && this.syncLogImplementation.IsEnabled())
				{
					this.blackBox.Append(row);
					this.syncLogImplementation.Append(row, 0);
				}
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000152A8 File Offset: 0x000134A8
		internal static SyncLog CreateInMemorySyncLog()
		{
			return new SyncLog();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000152AF File Offset: 0x000134AF
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000152BA File Offset: 0x000134BA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncLog>(this);
		}

		// Token: 0x040001BA RID: 442
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"thread-id",
			"level",
			"user",
			"subscription-guid",
			"subscription-type",
			"logentry-id",
			"context",
			"property-bag"
		};

		// Token: 0x040001BB RID: 443
		private static readonly int processId = Process.GetCurrentProcess().Id;

		// Token: 0x040001BC RID: 444
		private static readonly SyncLog inMemorySyncLog = new SyncLog();

		// Token: 0x040001BD RID: 445
		private readonly WatsonReporter watsonReporter = new WatsonReporter();

		// Token: 0x040001BE RID: 446
		private SyncLogBlackBox blackBox;

		// Token: 0x040001BF RID: 447
		private LogSchema schema;

		// Token: 0x040001C0 RID: 448
		private SyncLoggingLevel loggingLevel;

		// Token: 0x040001C1 RID: 449
		private ISyncLogImplementation syncLogImplementation;

		// Token: 0x040001C2 RID: 450
		private object syncRoot = new object();

		// Token: 0x02000085 RID: 133
		internal enum Field
		{
			// Token: 0x040001C4 RID: 452
			Time,
			// Token: 0x040001C5 RID: 453
			ThreadId,
			// Token: 0x040001C6 RID: 454
			Level,
			// Token: 0x040001C7 RID: 455
			User,
			// Token: 0x040001C8 RID: 456
			SubscriptionGuid,
			// Token: 0x040001C9 RID: 457
			SubscriptionType,
			// Token: 0x040001CA RID: 458
			LogEntryId,
			// Token: 0x040001CB RID: 459
			Context,
			// Token: 0x040001CC RID: 460
			PropertyBag
		}
	}
}
