using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200038D RID: 909
	internal class SpeechProcessorStatisticsLogger : StatisticsLogger
	{
		// Token: 0x06001D07 RID: 7431 RVA: 0x000741C5 File Offset: 0x000723C5
		protected SpeechProcessorStatisticsLogger()
		{
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000741D8 File Offset: 0x000723D8
		public static SpeechProcessorStatisticsLogger Instance
		{
			get
			{
				return SpeechProcessorStatisticsLogger.instance;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x000741DF File Offset: 0x000723DF
		protected override StatisticsLogger.StatisticsLogSchema LogSchema
		{
			get
			{
				return this.logSchema;
			}
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000741E7 File Offset: 0x000723E7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SpeechProcessorStatisticsLogger>(this);
		}

		// Token: 0x0400105B RID: 4187
		private readonly StatisticsLogger.StatisticsLogSchema logSchema = new SpeechProcessorStatisticsLogger.SpeechProcessorStatisticsLogSchema();

		// Token: 0x0400105C RID: 4188
		private static SpeechProcessorStatisticsLogger instance = new SpeechProcessorStatisticsLogger();

		// Token: 0x0200038E RID: 910
		private enum Field
		{
			// Token: 0x0400105E RID: 4190
			RequestId,
			// Token: 0x0400105F RID: 4191
			StartTime,
			// Token: 0x04001060 RID: 4192
			ProcessType,
			// Token: 0x04001061 RID: 4193
			Culture,
			// Token: 0x04001062 RID: 4194
			AudioLength,
			// Token: 0x04001063 RID: 4195
			UserObjectGuid,
			// Token: 0x04001064 RID: 4196
			TimeZone,
			// Token: 0x04001065 RID: 4197
			TenantGuid,
			// Token: 0x04001066 RID: 4198
			Tag,
			// Token: 0x04001067 RID: 4199
			MobileSpeechRecoResultType,
			// Token: 0x04001068 RID: 4200
			ResultText
		}

		// Token: 0x0200038F RID: 911
		public class SpeechProcessorStatisticsLogSchema : StatisticsLogger.StatisticsLogSchema
		{
			// Token: 0x06001D0C RID: 7436 RVA: 0x000741FB File Offset: 0x000723FB
			public SpeechProcessorStatisticsLogSchema() : this("SpeechProcessorStatistics")
			{
			}

			// Token: 0x06001D0D RID: 7437 RVA: 0x00074208 File Offset: 0x00072408
			protected SpeechProcessorStatisticsLogSchema(string logType) : base("1.0", logType, SpeechProcessorStatisticsLogger.SpeechProcessorStatisticsLogSchema.columns)
			{
			}

			// Token: 0x04001069 RID: 4201
			public const string SpeechProcessorStatisticsLogType = "SpeechProcessorStatistics";

			// Token: 0x0400106A RID: 4202
			public const string SpeechProcessorStatisticsLogVersion = "1.0";

			// Token: 0x0400106B RID: 4203
			private static readonly StatisticsLogger.StatisticsLogColumn[] columns = new StatisticsLogger.StatisticsLogColumn[]
			{
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.RequestId.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.StartTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.ProcessType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.Culture.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.AudioLength.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.UserObjectGuid.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.TimeZone.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.TenantGuid.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.Tag.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.MobileSpeechRecoResultType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(SpeechProcessorStatisticsLogger.Field.ResultText.ToString(), false)
			};
		}

		// Token: 0x02000390 RID: 912
		public class SpeechProcessorStatisticsLogRow : StatisticsLogger.StatisticsLogRow
		{
			// Token: 0x06001D0F RID: 7439 RVA: 0x00074317 File Offset: 0x00072517
			public SpeechProcessorStatisticsLogRow() : base(SpeechProcessorStatisticsLogger.Instance.LogSchema)
			{
			}

			// Token: 0x1700067B RID: 1659
			// (get) Token: 0x06001D10 RID: 7440 RVA: 0x00074329 File Offset: 0x00072529
			// (set) Token: 0x06001D11 RID: 7441 RVA: 0x00074331 File Offset: 0x00072531
			public Guid RequestId { get; set; }

			// Token: 0x1700067C RID: 1660
			// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0007433A File Offset: 0x0007253A
			// (set) Token: 0x06001D13 RID: 7443 RVA: 0x00074342 File Offset: 0x00072542
			public ExDateTime StartTime { get; set; }

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0007434B File Offset: 0x0007254B
			// (set) Token: 0x06001D15 RID: 7445 RVA: 0x00074353 File Offset: 0x00072553
			public SpeechLoggerProcessType? ProcessType { get; set; }

			// Token: 0x1700067E RID: 1662
			// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0007435C File Offset: 0x0007255C
			// (set) Token: 0x06001D17 RID: 7447 RVA: 0x00074364 File Offset: 0x00072564
			public CultureInfo Culture { get; set; }

			// Token: 0x1700067F RID: 1663
			// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0007436D File Offset: 0x0007256D
			// (set) Token: 0x06001D19 RID: 7449 RVA: 0x00074375 File Offset: 0x00072575
			public MobileSpeechRecoResultType? MobileSpeechRecoResultType { get; set; }

			// Token: 0x17000680 RID: 1664
			// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0007437E File Offset: 0x0007257E
			// (set) Token: 0x06001D1B RID: 7451 RVA: 0x00074386 File Offset: 0x00072586
			public string TimeZone { get; set; }

			// Token: 0x17000681 RID: 1665
			// (get) Token: 0x06001D1C RID: 7452 RVA: 0x0007438F File Offset: 0x0007258F
			// (set) Token: 0x06001D1D RID: 7453 RVA: 0x00074397 File Offset: 0x00072597
			public int AudioLength { get; set; }

			// Token: 0x17000682 RID: 1666
			// (get) Token: 0x06001D1E RID: 7454 RVA: 0x000743A0 File Offset: 0x000725A0
			// (set) Token: 0x06001D1F RID: 7455 RVA: 0x000743A8 File Offset: 0x000725A8
			public Guid? UserObjectGuid { get; set; }

			// Token: 0x17000683 RID: 1667
			// (get) Token: 0x06001D20 RID: 7456 RVA: 0x000743B1 File Offset: 0x000725B1
			// (set) Token: 0x06001D21 RID: 7457 RVA: 0x000743B9 File Offset: 0x000725B9
			public Guid? TenantGuid { get; set; }

			// Token: 0x17000684 RID: 1668
			// (get) Token: 0x06001D22 RID: 7458 RVA: 0x000743C2 File Offset: 0x000725C2
			// (set) Token: 0x06001D23 RID: 7459 RVA: 0x000743CA File Offset: 0x000725CA
			public string Tag { get; set; }

			// Token: 0x17000685 RID: 1669
			// (get) Token: 0x06001D24 RID: 7460 RVA: 0x000743D3 File Offset: 0x000725D3
			// (set) Token: 0x06001D25 RID: 7461 RVA: 0x000743DB File Offset: 0x000725DB
			public string ResultText { get; set; }

			// Token: 0x06001D26 RID: 7462 RVA: 0x000743E4 File Offset: 0x000725E4
			public override void PopulateFields()
			{
				base.Fields[0] = this.RequestId.ToString();
				base.Fields[1] = this.StartTime.ToString("o");
				base.Fields[2] = ((this.ProcessType == null) ? string.Empty : this.ProcessType.ToString());
				base.Fields[3] = this.Culture.ToString();
				base.Fields[4] = this.AudioLength.ToString();
				base.Fields[5] = ((this.UserObjectGuid == null) ? string.Empty : this.UserObjectGuid.ToString());
				base.Fields[6] = this.TimeZone;
				base.Fields[7] = ((this.TenantGuid == null) ? string.Empty : this.TenantGuid.ToString());
				base.Fields[8] = this.Tag;
				base.Fields[9] = ((this.MobileSpeechRecoResultType == null) ? string.Empty : this.MobileSpeechRecoResultType.ToString());
				base.Fields[10] = this.ResultText;
			}
		}
	}
}
