using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002D8 RID: 728
	internal class PipelineStatisticsLogger : StatisticsLogger
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x0005EAA3 File Offset: 0x0005CCA3
		protected PipelineStatisticsLogger()
		{
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0005EAB6 File Offset: 0x0005CCB6
		public static PipelineStatisticsLogger Instance
		{
			get
			{
				return PipelineStatisticsLogger.instance;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0005EABD File Offset: 0x0005CCBD
		protected override StatisticsLogger.StatisticsLogSchema LogSchema
		{
			get
			{
				return this.logSchema;
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0005EAC5 File Offset: 0x0005CCC5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PipelineStatisticsLogger>(this);
		}

		// Token: 0x04000D1B RID: 3355
		private readonly StatisticsLogger.StatisticsLogSchema logSchema = new PipelineStatisticsLogger.PipelineStatisticsLogSchema();

		// Token: 0x04000D1C RID: 3356
		private static PipelineStatisticsLogger instance = new PipelineStatisticsLogger();

		// Token: 0x020002D9 RID: 729
		private enum Field
		{
			// Token: 0x04000D1E RID: 3358
			SentTime,
			// Token: 0x04000D1F RID: 3359
			WorkId,
			// Token: 0x04000D20 RID: 3360
			MessageType,
			// Token: 0x04000D21 RID: 3361
			TranscriptionLanguage,
			// Token: 0x04000D22 RID: 3362
			TranscriptionResultType,
			// Token: 0x04000D23 RID: 3363
			TranscriptionErrorType,
			// Token: 0x04000D24 RID: 3364
			TranscriptionConfidence,
			// Token: 0x04000D25 RID: 3365
			TranscriptionTotalWords,
			// Token: 0x04000D26 RID: 3366
			TranscriptionCustomWords,
			// Token: 0x04000D27 RID: 3367
			TranscriptionTopNWords,
			// Token: 0x04000D28 RID: 3368
			TranscriptionElapsedTime,
			// Token: 0x04000D29 RID: 3369
			AudioCodec,
			// Token: 0x04000D2A RID: 3370
			AudioCompressionElapsedTime,
			// Token: 0x04000D2B RID: 3371
			CallerName,
			// Token: 0x04000D2C RID: 3372
			CalleeAlias,
			// Token: 0x04000D2D RID: 3373
			OrganizationId
		}

		// Token: 0x020002DA RID: 730
		public class PipelineStatisticsLogSchema : StatisticsLogger.StatisticsLogSchema
		{
			// Token: 0x0600161C RID: 5660 RVA: 0x0005EAD9 File Offset: 0x0005CCD9
			public PipelineStatisticsLogSchema() : this("PipelineStatistics")
			{
			}

			// Token: 0x0600161D RID: 5661 RVA: 0x0005EAE6 File Offset: 0x0005CCE6
			protected PipelineStatisticsLogSchema(string logType) : base("1.0", logType, PipelineStatisticsLogger.PipelineStatisticsLogSchema.columns)
			{
			}

			// Token: 0x04000D2E RID: 3374
			public const string PipelineStatisticsLogType = "PipelineStatistics";

			// Token: 0x04000D2F RID: 3375
			public const string PipelineStatisticsLogVersion = "1.0";

			// Token: 0x04000D30 RID: 3376
			private static readonly StatisticsLogger.StatisticsLogColumn[] columns = new StatisticsLogger.StatisticsLogColumn[]
			{
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.SentTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.WorkId.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.MessageType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.TranscriptionLanguage.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.TranscriptionResultType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.TranscriptionErrorType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.TranscriptionConfidence.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.TranscriptionTotalWords.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.TranscriptionCustomWords.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.TranscriptionTopNWords.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.TranscriptionElapsedTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.AudioCodec.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.AudioCompressionElapsedTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.CallerName.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.CalleeAlias.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(PipelineStatisticsLogger.Field.OrganizationId.ToString(), false)
			};
		}

		// Token: 0x020002DB RID: 731
		public class PipelineStatisticsLogRow : StatisticsLogger.StatisticsLogRow
		{
			// Token: 0x0600161F RID: 5663 RVA: 0x0005EC65 File Offset: 0x0005CE65
			public PipelineStatisticsLogRow() : base(PipelineStatisticsLogger.Instance.LogSchema)
			{
				this.TranscriptionResultType = RecoResultType.Skipped;
				this.TranscriptionErrorType = RecoErrorType.Other;
			}

			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x06001620 RID: 5664 RVA: 0x0005EC86 File Offset: 0x0005CE86
			// (set) Token: 0x06001621 RID: 5665 RVA: 0x0005EC8E File Offset: 0x0005CE8E
			public DateTime SentTime { get; set; }

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x06001622 RID: 5666 RVA: 0x0005EC97 File Offset: 0x0005CE97
			// (set) Token: 0x06001623 RID: 5667 RVA: 0x0005EC9F File Offset: 0x0005CE9F
			public Guid WorkId { get; set; }

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x06001624 RID: 5668 RVA: 0x0005ECA8 File Offset: 0x0005CEA8
			// (set) Token: 0x06001625 RID: 5669 RVA: 0x0005ECB0 File Offset: 0x0005CEB0
			public string MessageType { get; set; }

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x06001626 RID: 5670 RVA: 0x0005ECB9 File Offset: 0x0005CEB9
			// (set) Token: 0x06001627 RID: 5671 RVA: 0x0005ECC1 File Offset: 0x0005CEC1
			public CultureInfo TranscriptionLanguage { get; set; }

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x06001628 RID: 5672 RVA: 0x0005ECCA File Offset: 0x0005CECA
			// (set) Token: 0x06001629 RID: 5673 RVA: 0x0005ECD2 File Offset: 0x0005CED2
			public RecoResultType TranscriptionResultType { get; set; }

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x0600162A RID: 5674 RVA: 0x0005ECDB File Offset: 0x0005CEDB
			// (set) Token: 0x0600162B RID: 5675 RVA: 0x0005ECE3 File Offset: 0x0005CEE3
			public RecoErrorType TranscriptionErrorType { get; set; }

			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x0600162C RID: 5676 RVA: 0x0005ECEC File Offset: 0x0005CEEC
			// (set) Token: 0x0600162D RID: 5677 RVA: 0x0005ECF4 File Offset: 0x0005CEF4
			public float TranscriptionConfidence { get; set; }

			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x0600162E RID: 5678 RVA: 0x0005ECFD File Offset: 0x0005CEFD
			// (set) Token: 0x0600162F RID: 5679 RVA: 0x0005ED05 File Offset: 0x0005CF05
			public int TranscriptionTotalWords { get; set; }

			// Token: 0x17000591 RID: 1425
			// (get) Token: 0x06001630 RID: 5680 RVA: 0x0005ED0E File Offset: 0x0005CF0E
			// (set) Token: 0x06001631 RID: 5681 RVA: 0x0005ED16 File Offset: 0x0005CF16
			public int TranscriptionCustomWords { get; set; }

			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x06001632 RID: 5682 RVA: 0x0005ED1F File Offset: 0x0005CF1F
			// (set) Token: 0x06001633 RID: 5683 RVA: 0x0005ED27 File Offset: 0x0005CF27
			public int TranscriptionTopNWords { get; set; }

			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x06001634 RID: 5684 RVA: 0x0005ED30 File Offset: 0x0005CF30
			// (set) Token: 0x06001635 RID: 5685 RVA: 0x0005ED38 File Offset: 0x0005CF38
			public TimeSpan TranscriptionElapsedTime { get; set; }

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x06001636 RID: 5686 RVA: 0x0005ED41 File Offset: 0x0005CF41
			// (set) Token: 0x06001637 RID: 5687 RVA: 0x0005ED49 File Offset: 0x0005CF49
			public AudioCodecEnum AudioCodec { get; set; }

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x06001638 RID: 5688 RVA: 0x0005ED52 File Offset: 0x0005CF52
			// (set) Token: 0x06001639 RID: 5689 RVA: 0x0005ED5A File Offset: 0x0005CF5A
			public TimeSpan AudioCompressionElapsedTime { get; set; }

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x0600163A RID: 5690 RVA: 0x0005ED63 File Offset: 0x0005CF63
			// (set) Token: 0x0600163B RID: 5691 RVA: 0x0005ED6B File Offset: 0x0005CF6B
			public string CallerName { get; set; }

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x0600163C RID: 5692 RVA: 0x0005ED74 File Offset: 0x0005CF74
			// (set) Token: 0x0600163D RID: 5693 RVA: 0x0005ED7C File Offset: 0x0005CF7C
			public string CalleeAlias { get; set; }

			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x0600163E RID: 5694 RVA: 0x0005ED85 File Offset: 0x0005CF85
			// (set) Token: 0x0600163F RID: 5695 RVA: 0x0005ED8D File Offset: 0x0005CF8D
			public string OrganizationId { get; set; }

			// Token: 0x06001640 RID: 5696 RVA: 0x0005ED98 File Offset: 0x0005CF98
			public override void PopulateFields()
			{
				base.Fields[0] = this.SentTime.ToString(CultureInfo.InvariantCulture);
				base.Fields[1] = this.WorkId.ToString();
				base.Fields[2] = this.MessageType;
				base.Fields[3] = ((this.TranscriptionLanguage != null) ? this.TranscriptionLanguage.ToString() : string.Empty);
				base.Fields[4] = this.TranscriptionResultType.ToString();
				base.Fields[5] = this.TranscriptionErrorType.ToString();
				base.Fields[6] = this.TranscriptionConfidence.ToString();
				base.Fields[7] = this.TranscriptionTotalWords.ToString();
				base.Fields[8] = this.TranscriptionCustomWords.ToString();
				base.Fields[9] = this.TranscriptionTopNWords.ToString();
				base.Fields[10] = this.TranscriptionElapsedTime.ToString();
				base.Fields[11] = this.AudioCodec.ToString();
				base.Fields[12] = this.AudioCompressionElapsedTime.ToString();
				base.Fields[13] = this.CallerName;
				base.Fields[14] = this.CalleeAlias;
				base.Fields[15] = this.OrganizationId;
			}
		}
	}
}
