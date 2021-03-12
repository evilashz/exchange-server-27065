using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000FC RID: 252
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationProcessorResponse
	{
		// Token: 0x06000D57 RID: 3415 RVA: 0x00036E73 File Offset: 0x00035073
		public MigrationProcessorResponse() : this(MigrationProcessorResult.Deleted, null)
		{
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00036E7D File Offset: 0x0003507D
		protected MigrationProcessorResponse(MigrationProcessorResult result, LocalizedException error = null)
		{
			this.Result = result;
			this.DebugInfo = null;
			this.Error = error;
			this.ClearPoison = false;
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00036EA1 File Offset: 0x000350A1
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x00036EA9 File Offset: 0x000350A9
		public MigrationProcessorResult Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00036EB2 File Offset: 0x000350B2
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x00036EDC File Offset: 0x000350DC
		public TimeSpan? DelayTime
		{
			get
			{
				if (this.delayTime == null && this.Result == MigrationProcessorResult.Waiting)
				{
					return new TimeSpan?(this.DefaultDelay);
				}
				return this.delayTime;
			}
			set
			{
				if (value != null)
				{
					this.delayTime = new TimeSpan?(MigrationUtil.MinTimeSpan(this.MaxDelay, value.Value));
					return;
				}
				this.delayTime = null;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00036F11 File Offset: 0x00035111
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00036F19 File Offset: 0x00035119
		public TimeSpan? ProcessingDuration { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00036F22 File Offset: 0x00035122
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x00036F2A File Offset: 0x0003512A
		public string DebugInfo { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00036F33 File Offset: 0x00035133
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x00036F3B File Offset: 0x0003513B
		public LocalizedException Error { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00036F44 File Offset: 0x00035144
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x00036F4C File Offset: 0x0003514C
		public ExDateTime? EarliestDelayedChildTime { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00036F55 File Offset: 0x00035155
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x00036F5D File Offset: 0x0003515D
		public bool ClearPoison { get; set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00036F66 File Offset: 0x00035166
		protected virtual TimeSpan DefaultDelay
		{
			get
			{
				return ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationProcessorAverageWaitingJobDelay");
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00036F72 File Offset: 0x00035172
		protected virtual TimeSpan MaxDelay
		{
			get
			{
				return ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationProcessorMaxWaitingJobDelay");
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00036F80 File Offset: 0x00035180
		public static MigrationProcessorResponse Create(MigrationProcessorResult result, TimeSpan? delayTime = null, LocalizedException error = null)
		{
			MigrationProcessorResponse migrationProcessorResponse = new MigrationProcessorResponse(result, error);
			if (delayTime != null)
			{
				migrationProcessorResponse.DelayTime = delayTime;
			}
			return migrationProcessorResponse;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00036FA6 File Offset: 0x000351A6
		public static MigrationProcessorResponse CreateWaitingMax()
		{
			return MigrationProcessorResponse.Create(MigrationProcessorResult.Waiting, new TimeSpan?(ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationProcessorMaxWaitingJobDelay")), null);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00036FC0 File Offset: 0x000351C0
		public static T MergeResponses<T>(T current, T incoming) where T : MigrationProcessorResponse, new()
		{
			MigrationProcessorResult migrationProcessorResult = MigrationProcessorResult.Working;
			if (current.Result == incoming.Result)
			{
				migrationProcessorResult = current.Result;
			}
			else
			{
				foreach (MigrationProcessorResult migrationProcessorResult2 in MigrationProcessorResponse.JobResultPrecedence)
				{
					if (current.Result == migrationProcessorResult2 || incoming.Result == migrationProcessorResult2)
					{
						migrationProcessorResult = migrationProcessorResult2;
						break;
					}
				}
			}
			T t = Activator.CreateInstance<T>();
			t.Result = migrationProcessorResult;
			t.Merge(current, incoming);
			return t;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0003706C File Offset: 0x0003526C
		public override string ToString()
		{
			if (this.Result == MigrationProcessorResult.Waiting)
			{
				return string.Format("{0} ({1})", this.Result, this.DelayTime);
			}
			return string.Format("{0}", this.Result);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000370B8 File Offset: 0x000352B8
		public virtual void Aggregate(MigrationProcessorResponse childResponse)
		{
			MigrationProcessorResult migrationProcessorResult = this.Result;
			foreach (MigrationProcessorResult migrationProcessorResult2 in MigrationProcessorResponse.JobResultPrecedence)
			{
				if (migrationProcessorResult == migrationProcessorResult2 || childResponse.Result == migrationProcessorResult2)
				{
					migrationProcessorResult = migrationProcessorResult2;
					break;
				}
			}
			if (childResponse.DelayTime != null)
			{
				ExDateTime exDateTime = ExDateTime.UtcNow + childResponse.DelayTime.Value;
				ExDateTime exDateTime2 = this.EarliestDelayedChildTime ?? ExDateTime.MaxValue;
				this.EarliestDelayedChildTime = new ExDateTime?((exDateTime < exDateTime2) ? exDateTime : exDateTime2);
			}
			this.result = migrationProcessorResult;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00037168 File Offset: 0x00035368
		protected virtual void Merge(MigrationProcessorResponse left, MigrationProcessorResponse right)
		{
			if (this.Result == MigrationProcessorResult.Waiting)
			{
				if (left.Result == MigrationProcessorResult.Waiting && right.Result == MigrationProcessorResult.Waiting)
				{
					this.DelayTime = new TimeSpan?(MigrationUtil.MinTimeSpan(left.DelayTime.Value, right.DelayTime.Value));
					return;
				}
				if (left.Result == MigrationProcessorResult.Waiting)
				{
					this.DelayTime = left.DelayTime;
					return;
				}
				this.DelayTime = right.DelayTime;
			}
		}

		// Token: 0x040004D4 RID: 1236
		private static readonly MigrationProcessorResult[] JobResultPrecedence = new MigrationProcessorResult[]
		{
			MigrationProcessorResult.Working,
			MigrationProcessorResult.Waiting,
			MigrationProcessorResult.Completed,
			MigrationProcessorResult.Failed,
			MigrationProcessorResult.Suspended,
			MigrationProcessorResult.Deleted
		};

		// Token: 0x040004D5 RID: 1237
		private MigrationProcessorResult result;

		// Token: 0x040004D6 RID: 1238
		private TimeSpan? delayTime;
	}
}
