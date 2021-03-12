using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public sealed class MigrationUserStatistics : MigrationUser
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00036585 File Offset: 0x00034785
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x0003658D File Offset: 0x0003478D
		public new MigrationUserId Identity
		{
			get
			{
				return base.Identity;
			}
			internal set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00036596 File Offset: 0x00034796
		// (set) Token: 0x06000D0A RID: 3338 RVA: 0x0003659E File Offset: 0x0003479E
		public new MigrationBatchId BatchId
		{
			get
			{
				return base.BatchId;
			}
			internal set
			{
				base.BatchId = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x000365A7 File Offset: 0x000347A7
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x000365AF File Offset: 0x000347AF
		public new SmtpAddress EmailAddress
		{
			get
			{
				return base.EmailAddress;
			}
			internal set
			{
				base.EmailAddress = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x000365B8 File Offset: 0x000347B8
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x000365C0 File Offset: 0x000347C0
		public new MigrationUserRecipientType RecipientType
		{
			get
			{
				return base.RecipientType;
			}
			internal set
			{
				base.RecipientType = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000365C9 File Offset: 0x000347C9
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x000365D1 File Offset: 0x000347D1
		public new long SkippedItemCount
		{
			get
			{
				return base.SkippedItemCount;
			}
			internal set
			{
				base.SkippedItemCount = value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x000365DA File Offset: 0x000347DA
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x000365EC File Offset: 0x000347EC
		public long? TotalItemsInSourceMailboxCount
		{
			get
			{
				return (long?)this[MigrationUserStatisticsSchema.TotalItemsInSourceMailboxCount];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.TotalItemsInSourceMailboxCount] = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x000365FF File Offset: 0x000347FF
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00036607 File Offset: 0x00034807
		public new long SyncedItemCount
		{
			get
			{
				return base.SyncedItemCount;
			}
			internal set
			{
				base.SyncedItemCount = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00036610 File Offset: 0x00034810
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x00036618 File Offset: 0x00034818
		public new Guid MailboxGuid
		{
			get
			{
				return base.MailboxGuid;
			}
			internal set
			{
				base.MailboxGuid = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00036621 File Offset: 0x00034821
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x00036629 File Offset: 0x00034829
		public new string MailboxLegacyDN
		{
			get
			{
				return base.MailboxLegacyDN;
			}
			internal set
			{
				base.MailboxLegacyDN = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00036632 File Offset: 0x00034832
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x0003663A File Offset: 0x0003483A
		public new Guid RequestGuid
		{
			get
			{
				return base.RequestGuid;
			}
			internal set
			{
				base.RequestGuid = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00036643 File Offset: 0x00034843
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x0003664B File Offset: 0x0003484B
		public new DateTime? LastSuccessfulSyncTime
		{
			get
			{
				return base.LastSuccessfulSyncTime;
			}
			internal set
			{
				base.LastSuccessfulSyncTime = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00036654 File Offset: 0x00034854
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x0003665C File Offset: 0x0003485C
		public new MigrationUserStatus Status
		{
			get
			{
				return base.Status;
			}
			internal set
			{
				base.Status = value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00036665 File Offset: 0x00034865
		public new MigrationUserStatusSummary StatusSummary
		{
			get
			{
				return base.StatusSummary;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0003666D File Offset: 0x0003486D
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00036675 File Offset: 0x00034875
		public new DateTime? LastSubscriptionCheckTime
		{
			get
			{
				return base.LastSubscriptionCheckTime;
			}
			internal set
			{
				base.LastSubscriptionCheckTime = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0003667E File Offset: 0x0003487E
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00036690 File Offset: 0x00034890
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return (EnhancedTimeSpan?)this[MigrationUserStatisticsSchema.TotalQueuedDuration];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.TotalQueuedDuration] = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x000366A3 File Offset: 0x000348A3
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x000366B5 File Offset: 0x000348B5
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return (EnhancedTimeSpan?)this[MigrationUserStatisticsSchema.TotalInProgressDuration];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.TotalInProgressDuration] = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000366C8 File Offset: 0x000348C8
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x000366DA File Offset: 0x000348DA
		public EnhancedTimeSpan? TotalSyncedDuration
		{
			get
			{
				return (EnhancedTimeSpan?)this[MigrationUserStatisticsSchema.TotalSyncedDuration];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.TotalSyncedDuration] = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x000366ED File Offset: 0x000348ED
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x000366FF File Offset: 0x000348FF
		public EnhancedTimeSpan? TotalStalledDuration
		{
			get
			{
				return (EnhancedTimeSpan?)this[MigrationUserStatisticsSchema.TotalStalledDuration];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.TotalStalledDuration] = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00036712 File Offset: 0x00034912
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00036724 File Offset: 0x00034924
		public FailureRec Error
		{
			get
			{
				return (FailureRec)this[MigrationUserStatisticsSchema.Error];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.Error] = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00036732 File Offset: 0x00034932
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00036744 File Offset: 0x00034944
		public MigrationType MigrationType
		{
			get
			{
				return (MigrationType)this[MigrationUserStatisticsSchema.MigrationType];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.MigrationType] = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00036757 File Offset: 0x00034957
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x00036769 File Offset: 0x00034969
		public ByteQuantifiedSize? EstimatedTotalTransferSize
		{
			get
			{
				return (ByteQuantifiedSize?)this[MigrationUserStatisticsSchema.EstimatedTotalTransferSize];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.EstimatedTotalTransferSize] = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0003677C File Offset: 0x0003497C
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0003678E File Offset: 0x0003498E
		public ulong? EstimatedTotalTransferCount
		{
			get
			{
				return (ulong?)this[MigrationUserStatisticsSchema.EstimatedTotalTransferCount];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.EstimatedTotalTransferCount] = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x000367A1 File Offset: 0x000349A1
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x000367B3 File Offset: 0x000349B3
		public ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return (ByteQuantifiedSize?)this[MigrationUserStatisticsSchema.BytesTransferred];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.BytesTransferred] = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x000367C6 File Offset: 0x000349C6
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x000367D8 File Offset: 0x000349D8
		public ByteQuantifiedSize? AverageBytesTransferredPerHour
		{
			get
			{
				return (ByteQuantifiedSize?)this[MigrationUserStatisticsSchema.AverageBytesTransferredPerHour];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.AverageBytesTransferredPerHour] = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000367EB File Offset: 0x000349EB
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x000367FD File Offset: 0x000349FD
		public ByteQuantifiedSize? CurrentBytesTransferredPerMinute
		{
			get
			{
				return (ByteQuantifiedSize?)this[MigrationUserStatisticsSchema.CurrentBytesTransferredPerMinute];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.CurrentBytesTransferredPerMinute] = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00036810 File Offset: 0x00034A10
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00036822 File Offset: 0x00034A22
		public int? PercentageComplete
		{
			get
			{
				return (int?)this[MigrationUserStatisticsSchema.PercentageComplete];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.PercentageComplete] = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00036835 File Offset: 0x00034A35
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x00036847 File Offset: 0x00034A47
		public MultiValuedProperty<MigrationUserSkippedItem> SkippedItems
		{
			get
			{
				return (MultiValuedProperty<MigrationUserSkippedItem>)this[MigrationUserStatisticsSchema.SkippedItems];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.SkippedItems] = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00036855 File Offset: 0x00034A55
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00036867 File Offset: 0x00034A67
		public string DiagnosticInfo
		{
			get
			{
				return (string)this[MigrationUserStatisticsSchema.DiagnosticInfo];
			}
			internal set
			{
				this[MigrationUserStatisticsSchema.DiagnosticInfo] = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00036875 File Offset: 0x00034A75
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x0003687D File Offset: 0x00034A7D
		public Report Report { get; internal set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00036886 File Offset: 0x00034A86
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MigrationUserStatistics.schema;
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00036890 File Offset: 0x00034A90
		public override bool Equals(object obj)
		{
			return obj is MigrationUserStatistics && base.Equals(obj);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000368B0 File Offset: 0x00034AB0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040004D0 RID: 1232
		private static MigrationUserStatisticsSchema schema = ObjectSchema.GetInstance<MigrationUserStatisticsSchema>();
	}
}
