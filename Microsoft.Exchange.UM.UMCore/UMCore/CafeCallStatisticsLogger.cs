using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000248 RID: 584
	internal class CafeCallStatisticsLogger : StatisticsLogger
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x0004CAF0 File Offset: 0x0004ACF0
		protected CafeCallStatisticsLogger()
		{
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0004CB03 File Offset: 0x0004AD03
		public static CafeCallStatisticsLogger Instance
		{
			get
			{
				return CafeCallStatisticsLogger.instance;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0004CB0A File Offset: 0x0004AD0A
		protected override StatisticsLogger.StatisticsLogSchema LogSchema
		{
			get
			{
				return this.logSchema;
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0004CB12 File Offset: 0x0004AD12
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CafeCallStatisticsLogger>(this);
		}

		// Token: 0x04000BB8 RID: 3000
		private readonly StatisticsLogger.StatisticsLogSchema logSchema = new CafeCallStatisticsLogger.CafeCallStatisticsLogSchema();

		// Token: 0x04000BB9 RID: 3001
		private static CafeCallStatisticsLogger instance = new CafeCallStatisticsLogger();

		// Token: 0x02000249 RID: 585
		private enum Field
		{
			// Token: 0x04000BBB RID: 3003
			CallStartTime,
			// Token: 0x04000BBC RID: 3004
			CallLatency,
			// Token: 0x04000BBD RID: 3005
			CallType,
			// Token: 0x04000BBE RID: 3006
			CallId,
			// Token: 0x04000BBF RID: 3007
			CafeServerName,
			// Token: 0x04000BC0 RID: 3008
			DialPlanGuid,
			// Token: 0x04000BC1 RID: 3009
			DialPlanType,
			// Token: 0x04000BC2 RID: 3010
			CalledPhoneNumber,
			// Token: 0x04000BC3 RID: 3011
			CallerPhoneNumber,
			// Token: 0x04000BC4 RID: 3012
			OfferResult,
			// Token: 0x04000BC5 RID: 3013
			OrganizationId
		}

		// Token: 0x0200024A RID: 586
		public class CafeCallStatisticsLogSchema : StatisticsLogger.StatisticsLogSchema
		{
			// Token: 0x06001125 RID: 4389 RVA: 0x0004CB26 File Offset: 0x0004AD26
			public CafeCallStatisticsLogSchema() : this("UmCallRouter")
			{
			}

			// Token: 0x06001126 RID: 4390 RVA: 0x0004CB33 File Offset: 0x0004AD33
			protected CafeCallStatisticsLogSchema(string logType) : base("1.0", logType, CafeCallStatisticsLogger.CafeCallStatisticsLogSchema.columns)
			{
			}

			// Token: 0x04000BC6 RID: 3014
			private const string CallStatisticsLogType = "UmCallRouter";

			// Token: 0x04000BC7 RID: 3015
			private const string CallStatisticsLogVersion = "1.0";

			// Token: 0x04000BC8 RID: 3016
			private static readonly StatisticsLogger.StatisticsLogColumn[] columns = new StatisticsLogger.StatisticsLogColumn[]
			{
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.CallStartTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.CallLatency.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.CallType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.CallId.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.CafeServerName.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.DialPlanGuid.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.DialPlanType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.CalledPhoneNumber.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.CallerPhoneNumber.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.OfferResult.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CafeCallStatisticsLogger.Field.OrganizationId.ToString(), false)
			};
		}

		// Token: 0x0200024B RID: 587
		public class CafeCallStatisticsLogRow : StatisticsLogger.StatisticsLogRow
		{
			// Token: 0x06001128 RID: 4392 RVA: 0x0004CC43 File Offset: 0x0004AE43
			public CafeCallStatisticsLogRow() : base(CafeCallStatisticsLogger.Instance.LogSchema)
			{
			}

			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x06001129 RID: 4393 RVA: 0x0004CC55 File Offset: 0x0004AE55
			// (set) Token: 0x0600112A RID: 4394 RVA: 0x0004CC5D File Offset: 0x0004AE5D
			public DateTime CallStartTime { get; set; }

			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x0600112B RID: 4395 RVA: 0x0004CC66 File Offset: 0x0004AE66
			// (set) Token: 0x0600112C RID: 4396 RVA: 0x0004CC6E File Offset: 0x0004AE6E
			public TimeSpan CallLatency { get; set; }

			// Token: 0x17000412 RID: 1042
			// (get) Token: 0x0600112D RID: 4397 RVA: 0x0004CC77 File Offset: 0x0004AE77
			// (set) Token: 0x0600112E RID: 4398 RVA: 0x0004CC7F File Offset: 0x0004AE7F
			public string CallType { get; set; }

			// Token: 0x17000413 RID: 1043
			// (get) Token: 0x0600112F RID: 4399 RVA: 0x0004CC88 File Offset: 0x0004AE88
			// (set) Token: 0x06001130 RID: 4400 RVA: 0x0004CC90 File Offset: 0x0004AE90
			public string CallIdentity { get; set; }

			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x06001131 RID: 4401 RVA: 0x0004CC99 File Offset: 0x0004AE99
			// (set) Token: 0x06001132 RID: 4402 RVA: 0x0004CCA1 File Offset: 0x0004AEA1
			public string CafeServerName { get; set; }

			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x06001133 RID: 4403 RVA: 0x0004CCAA File Offset: 0x0004AEAA
			// (set) Token: 0x06001134 RID: 4404 RVA: 0x0004CCB2 File Offset: 0x0004AEB2
			public Guid DialPlanGuid { get; set; }

			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x06001135 RID: 4405 RVA: 0x0004CCBB File Offset: 0x0004AEBB
			// (set) Token: 0x06001136 RID: 4406 RVA: 0x0004CCC3 File Offset: 0x0004AEC3
			public string DialPlanType { get; set; }

			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x06001137 RID: 4407 RVA: 0x0004CCCC File Offset: 0x0004AECC
			// (set) Token: 0x06001138 RID: 4408 RVA: 0x0004CCD4 File Offset: 0x0004AED4
			public string CalledPhoneNumber { get; set; }

			// Token: 0x17000418 RID: 1048
			// (get) Token: 0x06001139 RID: 4409 RVA: 0x0004CCDD File Offset: 0x0004AEDD
			// (set) Token: 0x0600113A RID: 4410 RVA: 0x0004CCE5 File Offset: 0x0004AEE5
			public string CallerPhoneNumber { get; set; }

			// Token: 0x17000419 RID: 1049
			// (get) Token: 0x0600113B RID: 4411 RVA: 0x0004CCEE File Offset: 0x0004AEEE
			// (set) Token: 0x0600113C RID: 4412 RVA: 0x0004CCF6 File Offset: 0x0004AEF6
			public string OfferResult { get; set; }

			// Token: 0x1700041A RID: 1050
			// (get) Token: 0x0600113D RID: 4413 RVA: 0x0004CCFF File Offset: 0x0004AEFF
			// (set) Token: 0x0600113E RID: 4414 RVA: 0x0004CD07 File Offset: 0x0004AF07
			public string OrganizationId { get; set; }

			// Token: 0x0600113F RID: 4415 RVA: 0x0004CD10 File Offset: 0x0004AF10
			public override void PopulateFields()
			{
				base.Fields[0] = this.CallStartTime.ToString(CultureInfo.InvariantCulture);
				base.Fields[1] = this.CallLatency.TotalSeconds.ToString(CultureInfo.InvariantCulture);
				base.Fields[2] = this.CallType;
				base.Fields[3] = this.CallIdentity;
				base.Fields[4] = this.CafeServerName;
				base.Fields[5] = this.DialPlanGuid.ToString();
				base.Fields[6] = this.DialPlanType;
				base.Fields[7] = this.CalledPhoneNumber;
				base.Fields[8] = this.CallerPhoneNumber;
				base.Fields[9] = this.OfferResult;
				base.Fields[10] = this.OrganizationId;
			}
		}
	}
}
