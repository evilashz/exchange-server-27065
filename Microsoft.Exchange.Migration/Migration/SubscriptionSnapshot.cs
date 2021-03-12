using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000143 RID: 323
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SubscriptionSnapshot : ISubscriptionStatistics, IStepSnapshot, IMigrationSerializable
	{
		// Token: 0x06001025 RID: 4133 RVA: 0x00044C24 File Offset: 0x00042E24
		public SubscriptionSnapshot(ISubscriptionId id, SnapshotStatus status, bool isInitialSyncComplete, ExDateTime createTime, ExDateTime? lastUpdateTime, ExDateTime? lastSyncTime, LocalizedString? errorMessage, string batchName)
		{
			this.id = id;
			this.Status = status;
			this.IsInitialSyncComplete = isInitialSyncComplete;
			this.CreateTime = createTime;
			this.LastUpdateTime = lastUpdateTime;
			this.LastSyncTime = lastSyncTime;
			this.ErrorMessage = errorMessage;
			this.BatchName = batchName;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00044C74 File Offset: 0x00042E74
		protected SubscriptionSnapshot()
		{
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00044C7C File Offset: 0x00042E7C
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x00044C84 File Offset: 0x00042E84
		public ISnapshotId Id
		{
			get
			{
				return this.id;
			}
			protected set
			{
				this.id = (ISubscriptionId)value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x00044C92 File Offset: 0x00042E92
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x00044C9A File Offset: 0x00042E9A
		public SnapshotStatus Status
		{
			get
			{
				return this.status;
			}
			protected set
			{
				this.status = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x00044CA3 File Offset: 0x00042EA3
		// (set) Token: 0x0600102C RID: 4140 RVA: 0x00044CAB File Offset: 0x00042EAB
		public bool IsInitialSyncComplete
		{
			get
			{
				return this.isInitialSyncComplete;
			}
			protected set
			{
				this.isInitialSyncComplete = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00044CB4 File Offset: 0x00042EB4
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x00044CBC File Offset: 0x00042EBC
		public ExDateTime CreateTime
		{
			get
			{
				return this.createTime;
			}
			protected set
			{
				this.createTime = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x00044CC5 File Offset: 0x00042EC5
		// (set) Token: 0x06001030 RID: 4144 RVA: 0x00044CCD File Offset: 0x00042ECD
		public ExDateTime? LastUpdateTime
		{
			get
			{
				return this.lastUpdateTime;
			}
			protected set
			{
				this.lastUpdateTime = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00044CD6 File Offset: 0x00042ED6
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x00044CDE File Offset: 0x00042EDE
		public ExDateTime? LastSyncTime
		{
			get
			{
				return this.lastSyncTime;
			}
			protected set
			{
				this.lastSyncTime = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00044CE7 File Offset: 0x00042EE7
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x00044CEF File Offset: 0x00042EEF
		public LocalizedString? ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			protected set
			{
				this.errorMessage = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00044CF8 File Offset: 0x00042EF8
		public ExDateTime? InjectionCompletedTime
		{
			get
			{
				return new ExDateTime?(this.CreateTime);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x00044D05 File Offset: 0x00042F05
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x00044D0D File Offset: 0x00042F0D
		public long NumItemsSkipped
		{
			get
			{
				return this.numItemsSkipped;
			}
			protected set
			{
				this.numItemsSkipped = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x00044D16 File Offset: 0x00042F16
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x00044D1E File Offset: 0x00042F1E
		public long NumItemsSynced
		{
			get
			{
				return this.numItemsSynced;
			}
			protected set
			{
				this.numItemsSynced = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x00044D27 File Offset: 0x00042F27
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x00044D2F File Offset: 0x00042F2F
		public long? NumTotalItemsInMailbox
		{
			get
			{
				return this.numTotalItemsInMailbox;
			}
			protected set
			{
				this.numTotalItemsInMailbox = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x00044D38 File Offset: 0x00042F38
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x00044D40 File Offset: 0x00042F40
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return this.totalQueuedDuration;
			}
			set
			{
				this.totalQueuedDuration = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x00044D49 File Offset: 0x00042F49
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x00044D51 File Offset: 0x00042F51
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return this.totalInProgressDuration;
			}
			set
			{
				this.totalInProgressDuration = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00044D5A File Offset: 0x00042F5A
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x00044D62 File Offset: 0x00042F62
		public EnhancedTimeSpan? TotalSyncedDuration
		{
			get
			{
				return this.totalSyncedDuration;
			}
			set
			{
				this.totalSyncedDuration = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x00044D6B File Offset: 0x00042F6B
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x00044D73 File Offset: 0x00042F73
		public EnhancedTimeSpan? TotalStalledDuration
		{
			get
			{
				return this.totalStalledDuration;
			}
			set
			{
				this.totalStalledDuration = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x00044D7C File Offset: 0x00042F7C
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x00044D84 File Offset: 0x00042F84
		public ByteQuantifiedSize? EstimatedTotalTransferSize
		{
			get
			{
				return this.estimatedTotalTransferSize;
			}
			set
			{
				this.estimatedTotalTransferSize = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x00044D8D File Offset: 0x00042F8D
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x00044D95 File Offset: 0x00042F95
		public ulong? EstimatedTotalTransferCount
		{
			get
			{
				return this.estimatedTotalTransferCount;
			}
			set
			{
				this.estimatedTotalTransferCount = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00044D9E File Offset: 0x00042F9E
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x00044DA6 File Offset: 0x00042FA6
		public ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return this.bytesTransferred;
			}
			set
			{
				this.bytesTransferred = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00044DAF File Offset: 0x00042FAF
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x00044DB7 File Offset: 0x00042FB7
		public ByteQuantifiedSize? AverageBytesTransferredPerHour
		{
			get
			{
				return this.averageBytesTransferredPerHour;
			}
			set
			{
				this.averageBytesTransferredPerHour = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00044DC0 File Offset: 0x00042FC0
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x00044DC8 File Offset: 0x00042FC8
		public ByteQuantifiedSize? CurrentBytesTransferredPerMinute
		{
			get
			{
				return this.currentBytesTransferredPerMinute;
			}
			set
			{
				this.currentBytesTransferredPerMinute = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x00044DD1 File Offset: 0x00042FD1
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x00044DD9 File Offset: 0x00042FD9
		public int? PercentageComplete
		{
			get
			{
				return this.percentageComplete;
			}
			set
			{
				this.percentageComplete = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00044DE2 File Offset: 0x00042FE2
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x00044DEA File Offset: 0x00042FEA
		public Report Report
		{
			get
			{
				return this.report;
			}
			set
			{
				this.report = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00044DF3 File Offset: 0x00042FF3
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x00044DFB File Offset: 0x00042FFB
		public string BatchName { get; protected set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00044E04 File Offset: 0x00043004
		public PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return new PropertyDefinition[]
				{
					MigrationBatchMessageSchema.MigrationJobItemItemsSynced,
					MigrationBatchMessageSchema.MigrationJobItemItemsSkipped,
					MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime
				};
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00044E34 File Offset: 0x00043034
		public static SubscriptionSnapshot CreateRemoved()
		{
			return new SubscriptionSnapshot
			{
				Status = SnapshotStatus.Removed
			};
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00044E50 File Offset: 0x00043050
		public static SubscriptionSnapshot CreateFailed(LocalizedString errorMessage)
		{
			return new SubscriptionSnapshot
			{
				Status = SnapshotStatus.Failed,
				ErrorMessage = new LocalizedString?(errorMessage)
			};
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00044E78 File Offset: 0x00043078
		public static SubscriptionSnapshot CreateId(ISubscriptionId id)
		{
			return new SubscriptionSnapshot
			{
				id = id,
				Status = SnapshotStatus.InProgress
			};
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00044E9A File Offset: 0x0004309A
		public void SetStatistics(long numberItemsSynced, long numberItemsSkipped, long? numberItemsTotal)
		{
			this.NumItemsSynced = numberItemsSynced;
			this.NumItemsSkipped = numberItemsSkipped;
			this.NumTotalItemsInMailbox = numberItemsTotal;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00044EB4 File Offset: 0x000430B4
		public bool IsTimedOut(TimeSpan timeout)
		{
			TimeSpan t;
			if (this.LastUpdateTime != null)
			{
				t = ExDateTime.UtcNow - this.LastUpdateTime.Value;
			}
			else
			{
				t = ExDateTime.UtcNow - this.CreateTime;
			}
			return t > timeout;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00044F04 File Offset: 0x00043104
		public bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.NumItemsSynced = (long)message[MigrationBatchMessageSchema.MigrationJobItemItemsSynced];
			this.NumItemsSkipped = (long)message[MigrationBatchMessageSchema.MigrationJobItemItemsSkipped];
			this.LastSyncTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime);
			return true;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00044F44 File Offset: 0x00043144
		public void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			message[MigrationBatchMessageSchema.MigrationJobItemItemsSynced] = this.NumItemsSynced;
			message[MigrationBatchMessageSchema.MigrationJobItemItemsSkipped] = this.NumItemsSkipped;
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime, this.LastSyncTime);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00044F84 File Offset: 0x00043184
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("SubscriptionSnapshot");
			xelement.Add(new object[]
			{
				new XElement("ItemsSynced", this.NumItemsSynced),
				new XElement("ItemsSkipped", this.NumItemsSkipped),
				new XElement("LastSyncTime", this.LastSyncTime)
			});
			return xelement;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00045008 File Offset: 0x00043208
		internal static EnhancedTimeSpan? Subtract(EnhancedTimeSpan? value1, EnhancedTimeSpan? value2)
		{
			if (value1 == null)
			{
				return null;
			}
			if (value2 == null)
			{
				return value1;
			}
			return new EnhancedTimeSpan?(value1.Value - value2.Value);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0004504C File Offset: 0x0004324C
		internal static SubscriptionSnapshot CreateFromMessage(IMigrationStoreObject message)
		{
			SubscriptionSnapshot subscriptionSnapshot = new SubscriptionSnapshot();
			subscriptionSnapshot.ReadFromMessageItem(message);
			return subscriptionSnapshot;
		}

		// Token: 0x040005A5 RID: 1445
		private ISubscriptionId id;

		// Token: 0x040005A6 RID: 1446
		private SnapshotStatus status;

		// Token: 0x040005A7 RID: 1447
		private bool isInitialSyncComplete;

		// Token: 0x040005A8 RID: 1448
		private ExDateTime createTime;

		// Token: 0x040005A9 RID: 1449
		private ExDateTime? lastUpdateTime;

		// Token: 0x040005AA RID: 1450
		private ExDateTime? lastSyncTime;

		// Token: 0x040005AB RID: 1451
		private EnhancedTimeSpan? totalQueuedDuration;

		// Token: 0x040005AC RID: 1452
		private EnhancedTimeSpan? totalInProgressDuration;

		// Token: 0x040005AD RID: 1453
		private EnhancedTimeSpan? totalSyncedDuration;

		// Token: 0x040005AE RID: 1454
		private EnhancedTimeSpan? totalStalledDuration;

		// Token: 0x040005AF RID: 1455
		private LocalizedString? errorMessage;

		// Token: 0x040005B0 RID: 1456
		private long numItemsSkipped;

		// Token: 0x040005B1 RID: 1457
		private long numItemsSynced;

		// Token: 0x040005B2 RID: 1458
		private long? numTotalItemsInMailbox;

		// Token: 0x040005B3 RID: 1459
		private ByteQuantifiedSize? estimatedTotalTransferSize;

		// Token: 0x040005B4 RID: 1460
		private ulong? estimatedTotalTransferCount;

		// Token: 0x040005B5 RID: 1461
		private ByteQuantifiedSize? bytesTransferred;

		// Token: 0x040005B6 RID: 1462
		private ByteQuantifiedSize? currentBytesTransferredPerMinute;

		// Token: 0x040005B7 RID: 1463
		private ByteQuantifiedSize? averageBytesTransferredPerHour;

		// Token: 0x040005B8 RID: 1464
		private int? percentageComplete;

		// Token: 0x040005B9 RID: 1465
		private Report report;
	}
}
