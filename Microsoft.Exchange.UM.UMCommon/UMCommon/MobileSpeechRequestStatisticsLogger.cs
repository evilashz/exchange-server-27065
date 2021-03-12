using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000161 RID: 353
	internal class MobileSpeechRequestStatisticsLogger : StatisticsLogger
	{
		// Token: 0x06000B31 RID: 2865 RVA: 0x00029D77 File Offset: 0x00027F77
		protected MobileSpeechRequestStatisticsLogger()
		{
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00029D8A File Offset: 0x00027F8A
		public static MobileSpeechRequestStatisticsLogger Instance
		{
			get
			{
				return MobileSpeechRequestStatisticsLogger.instance;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00029D91 File Offset: 0x00027F91
		protected override StatisticsLogger.StatisticsLogSchema LogSchema
		{
			get
			{
				return this.logSchema;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00029D99 File Offset: 0x00027F99
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MobileSpeechRequestStatisticsLogger>(this);
		}

		// Token: 0x040005F5 RID: 1525
		private readonly StatisticsLogger.StatisticsLogSchema logSchema = new MobileSpeechRequestStatisticsLogger.MobileSpeechRequestStatisticsLogSchema();

		// Token: 0x040005F6 RID: 1526
		private static MobileSpeechRequestStatisticsLogger instance = new MobileSpeechRequestStatisticsLogger();

		// Token: 0x02000162 RID: 354
		private enum Field
		{
			// Token: 0x040005F8 RID: 1528
			RequestId,
			// Token: 0x040005F9 RID: 1529
			StartTime,
			// Token: 0x040005FA RID: 1530
			RequestType,
			// Token: 0x040005FB RID: 1531
			RequestStepId,
			// Token: 0x040005FC RID: 1532
			RequestLanguage,
			// Token: 0x040005FD RID: 1533
			AudioLength,
			// Token: 0x040005FE RID: 1534
			UserObjectGuid,
			// Token: 0x040005FF RID: 1535
			RecognitionErrorCode,
			// Token: 0x04000600 RID: 1536
			RecognitionErrorMessage,
			// Token: 0x04000601 RID: 1537
			RecognitionTotalResults,
			// Token: 0x04000602 RID: 1538
			RequestTotalElapsedTime,
			// Token: 0x04000603 RID: 1539
			TimeZone,
			// Token: 0x04000604 RID: 1540
			TenantGuid,
			// Token: 0x04000605 RID: 1541
			Tag,
			// Token: 0x04000606 RID: 1542
			LogOrigin
		}

		// Token: 0x02000163 RID: 355
		public class MobileSpeechRequestStatisticsLogSchema : StatisticsLogger.StatisticsLogSchema
		{
			// Token: 0x06000B36 RID: 2870 RVA: 0x00029DAD File Offset: 0x00027FAD
			public MobileSpeechRequestStatisticsLogSchema() : this("MobileSpeechRequestStatistics")
			{
			}

			// Token: 0x06000B37 RID: 2871 RVA: 0x00029DBA File Offset: 0x00027FBA
			protected MobileSpeechRequestStatisticsLogSchema(string logType) : base("1.3", logType, MobileSpeechRequestStatisticsLogger.MobileSpeechRequestStatisticsLogSchema.columns)
			{
			}

			// Token: 0x04000607 RID: 1543
			public const string MobileSpeechRequestStatisticsLogType = "MobileSpeechRequestStatistics";

			// Token: 0x04000608 RID: 1544
			public const string MobileSpeechRequestStatisticsLogVersion = "1.3";

			// Token: 0x04000609 RID: 1545
			private static readonly StatisticsLogger.StatisticsLogColumn[] columns = new StatisticsLogger.StatisticsLogColumn[]
			{
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.RequestId.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.StartTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.RequestType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.RequestStepId.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.RequestLanguage.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.AudioLength.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.UserObjectGuid.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.RecognitionErrorMessage.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.RecognitionErrorCode.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.RecognitionTotalResults.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.RequestTotalElapsedTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.TimeZone.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.TenantGuid.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.Tag.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MobileSpeechRequestStatisticsLogger.Field.LogOrigin.ToString(), false)
			};
		}

		// Token: 0x02000164 RID: 356
		public class MobileSpeechRequestStatisticsLogRow : StatisticsLogger.StatisticsLogRow
		{
			// Token: 0x06000B39 RID: 2873 RVA: 0x00029F23 File Offset: 0x00028123
			public MobileSpeechRequestStatisticsLogRow() : base(MobileSpeechRequestStatisticsLogger.Instance.LogSchema)
			{
			}

			// Token: 0x170002AE RID: 686
			// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00029F35 File Offset: 0x00028135
			// (set) Token: 0x06000B3B RID: 2875 RVA: 0x00029F3D File Offset: 0x0002813D
			public Guid RequestId { get; set; }

			// Token: 0x170002AF RID: 687
			// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00029F46 File Offset: 0x00028146
			// (set) Token: 0x06000B3D RID: 2877 RVA: 0x00029F4E File Offset: 0x0002814E
			public ExDateTime StartTime { get; set; }

			// Token: 0x170002B0 RID: 688
			// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00029F57 File Offset: 0x00028157
			// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00029F5F File Offset: 0x0002815F
			public MobileSpeechRecoRequestType? RequestType { get; set; }

			// Token: 0x170002B1 RID: 689
			// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00029F68 File Offset: 0x00028168
			// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00029F70 File Offset: 0x00028170
			public MobileSpeechRecoRequestStepLogId? RequestStepId { get; set; }

			// Token: 0x170002B2 RID: 690
			// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00029F79 File Offset: 0x00028179
			// (set) Token: 0x06000B43 RID: 2883 RVA: 0x00029F81 File Offset: 0x00028181
			public string RequestLanguage { get; set; }

			// Token: 0x170002B3 RID: 691
			// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00029F8A File Offset: 0x0002818A
			// (set) Token: 0x06000B45 RID: 2885 RVA: 0x00029F92 File Offset: 0x00028192
			public string RecognitionErrorMessage { get; set; }

			// Token: 0x170002B4 RID: 692
			// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00029F9B File Offset: 0x0002819B
			// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00029FA3 File Offset: 0x000281A3
			public int RecognitionErrorCode { get; set; }

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00029FAC File Offset: 0x000281AC
			// (set) Token: 0x06000B49 RID: 2889 RVA: 0x00029FB4 File Offset: 0x000281B4
			public int RecognitionTotalResults { get; set; }

			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00029FBD File Offset: 0x000281BD
			// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00029FC5 File Offset: 0x000281C5
			public TimeSpan RequestTotalElapsedTime { get; set; }

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00029FCE File Offset: 0x000281CE
			// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00029FD6 File Offset: 0x000281D6
			public string TimeZone { get; set; }

			// Token: 0x170002B8 RID: 696
			// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00029FDF File Offset: 0x000281DF
			// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00029FE7 File Offset: 0x000281E7
			public int AudioLength { get; set; }

			// Token: 0x170002B9 RID: 697
			// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00029FF0 File Offset: 0x000281F0
			// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00029FF8 File Offset: 0x000281F8
			public Guid? UserObjectGuid { get; set; }

			// Token: 0x170002BA RID: 698
			// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002A001 File Offset: 0x00028201
			// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0002A009 File Offset: 0x00028209
			public Guid? TenantGuid { get; set; }

			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002A012 File Offset: 0x00028212
			// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0002A01A File Offset: 0x0002821A
			public string Tag { get; set; }

			// Token: 0x170002BC RID: 700
			// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002A023 File Offset: 0x00028223
			// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0002A02B File Offset: 0x0002822B
			public MobileSpeechRecoLogStatisticOrigin? LogOrigin { get; set; }

			// Token: 0x06000B58 RID: 2904 RVA: 0x0002A034 File Offset: 0x00028234
			public override void PopulateFields()
			{
				base.Fields[0] = this.RequestId.ToString();
				base.Fields[1] = this.StartTime.ToString("o");
				base.Fields[2] = ((this.RequestType == null) ? string.Empty : this.RequestType.ToString());
				base.Fields[3] = this.RequestStepId.ToString();
				base.Fields[4] = ((this.RequestLanguage == null) ? string.Empty : this.RequestLanguage);
				base.Fields[5] = this.AudioLength.ToString();
				base.Fields[6] = ((this.UserObjectGuid == null) ? string.Empty : this.UserObjectGuid.ToString());
				base.Fields[8] = this.RecognitionErrorMessage;
				base.Fields[7] = this.RecognitionErrorCode.ToString();
				base.Fields[9] = this.RecognitionTotalResults.ToString();
				base.Fields[10] = this.RequestTotalElapsedTime.ToString();
				base.Fields[11] = this.TimeZone;
				base.Fields[12] = ((this.TenantGuid == null) ? string.Empty : this.TenantGuid.ToString());
				base.Fields[13] = this.Tag;
				base.Fields[14] = ((this.LogOrigin == null) ? string.Empty : this.LogOrigin.ToString());
			}
		}
	}
}
