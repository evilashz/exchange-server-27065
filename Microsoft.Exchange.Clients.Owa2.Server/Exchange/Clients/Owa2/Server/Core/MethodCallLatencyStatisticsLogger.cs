using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000388 RID: 904
	internal class MethodCallLatencyStatisticsLogger : StatisticsLogger
	{
		// Token: 0x06001CEF RID: 7407 RVA: 0x00073F52 File Offset: 0x00072152
		protected MethodCallLatencyStatisticsLogger()
		{
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00073F65 File Offset: 0x00072165
		public static MethodCallLatencyStatisticsLogger Instance
		{
			get
			{
				return MethodCallLatencyStatisticsLogger.instance;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x00073F6C File Offset: 0x0007216C
		protected override StatisticsLogger.StatisticsLogSchema LogSchema
		{
			get
			{
				return this.logSchema;
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00073F74 File Offset: 0x00072174
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MethodCallLatencyStatisticsLogger>(this);
		}

		// Token: 0x04001041 RID: 4161
		private readonly StatisticsLogger.StatisticsLogSchema logSchema = new MethodCallLatencyStatisticsLogger.MethodCallLatencyStatisticsLogSchema();

		// Token: 0x04001042 RID: 4162
		private static MethodCallLatencyStatisticsLogger instance = new MethodCallLatencyStatisticsLogger();

		// Token: 0x02000389 RID: 905
		private enum Field
		{
			// Token: 0x04001044 RID: 4164
			RequestId,
			// Token: 0x04001045 RID: 4165
			StartTime,
			// Token: 0x04001046 RID: 4166
			Latency,
			// Token: 0x04001047 RID: 4167
			MethodName,
			// Token: 0x04001048 RID: 4168
			UserObjectGuid,
			// Token: 0x04001049 RID: 4169
			TenantGuid,
			// Token: 0x0400104A RID: 4170
			Tag
		}

		// Token: 0x0200038A RID: 906
		public class MethodCallLatencyStatisticsLogSchema : StatisticsLogger.StatisticsLogSchema
		{
			// Token: 0x06001CF4 RID: 7412 RVA: 0x00073F88 File Offset: 0x00072188
			public MethodCallLatencyStatisticsLogSchema() : this("MethodCallLatencyStatistics")
			{
			}

			// Token: 0x06001CF5 RID: 7413 RVA: 0x00073F95 File Offset: 0x00072195
			protected MethodCallLatencyStatisticsLogSchema(string logType) : base("1.0", logType, MethodCallLatencyStatisticsLogger.MethodCallLatencyStatisticsLogSchema.columns)
			{
			}

			// Token: 0x0400104B RID: 4171
			public const string MethodCallLatencyStatisticsLogType = "MethodCallLatencyStatistics";

			// Token: 0x0400104C RID: 4172
			public const string MethodCallLatencyStatisticsLogVersion = "1.0";

			// Token: 0x0400104D RID: 4173
			private static readonly StatisticsLogger.StatisticsLogColumn[] columns = new StatisticsLogger.StatisticsLogColumn[]
			{
				new StatisticsLogger.StatisticsLogColumn(MethodCallLatencyStatisticsLogger.Field.RequestId.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MethodCallLatencyStatisticsLogger.Field.StartTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MethodCallLatencyStatisticsLogger.Field.Latency.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MethodCallLatencyStatisticsLogger.Field.MethodName.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MethodCallLatencyStatisticsLogger.Field.UserObjectGuid.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MethodCallLatencyStatisticsLogger.Field.TenantGuid.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(MethodCallLatencyStatisticsLogger.Field.Tag.ToString(), false)
			};
		}

		// Token: 0x0200038B RID: 907
		public class MethodCallLatencyStatisticsLogRow : StatisticsLogger.StatisticsLogRow
		{
			// Token: 0x06001CF7 RID: 7415 RVA: 0x0007404E File Offset: 0x0007224E
			public MethodCallLatencyStatisticsLogRow() : base(MethodCallLatencyStatisticsLogger.Instance.LogSchema)
			{
			}

			// Token: 0x17000672 RID: 1650
			// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00074060 File Offset: 0x00072260
			// (set) Token: 0x06001CF9 RID: 7417 RVA: 0x00074068 File Offset: 0x00072268
			public Guid RequestId { get; set; }

			// Token: 0x17000673 RID: 1651
			// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00074071 File Offset: 0x00072271
			// (set) Token: 0x06001CFB RID: 7419 RVA: 0x00074079 File Offset: 0x00072279
			public ExDateTime StartTime { get; set; }

			// Token: 0x17000674 RID: 1652
			// (get) Token: 0x06001CFC RID: 7420 RVA: 0x00074082 File Offset: 0x00072282
			// (set) Token: 0x06001CFD RID: 7421 RVA: 0x0007408A File Offset: 0x0007228A
			public TimeSpan Latency { get; set; }

			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x06001CFE RID: 7422 RVA: 0x00074093 File Offset: 0x00072293
			// (set) Token: 0x06001CFF RID: 7423 RVA: 0x0007409B File Offset: 0x0007229B
			public string MethodName { get; set; }

			// Token: 0x17000676 RID: 1654
			// (get) Token: 0x06001D00 RID: 7424 RVA: 0x000740A4 File Offset: 0x000722A4
			// (set) Token: 0x06001D01 RID: 7425 RVA: 0x000740AC File Offset: 0x000722AC
			public Guid? UserObjectGuid { get; set; }

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x06001D02 RID: 7426 RVA: 0x000740B5 File Offset: 0x000722B5
			// (set) Token: 0x06001D03 RID: 7427 RVA: 0x000740BD File Offset: 0x000722BD
			public Guid? TenantGuid { get; set; }

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x06001D04 RID: 7428 RVA: 0x000740C6 File Offset: 0x000722C6
			// (set) Token: 0x06001D05 RID: 7429 RVA: 0x000740CE File Offset: 0x000722CE
			public string Tag { get; set; }

			// Token: 0x06001D06 RID: 7430 RVA: 0x000740D8 File Offset: 0x000722D8
			public override void PopulateFields()
			{
				base.Fields[0] = this.RequestId.ToString();
				base.Fields[1] = this.StartTime.ToString("o");
				base.Fields[2] = this.Latency.TotalSeconds.ToString(CultureInfo.InvariantCulture);
				base.Fields[3] = this.MethodName;
				base.Fields[4] = ((this.UserObjectGuid == null) ? string.Empty : this.UserObjectGuid.ToString());
				base.Fields[5] = ((this.TenantGuid == null) ? string.Empty : this.TenantGuid.ToString());
				base.Fields[6] = this.Tag;
			}
		}
	}
}
