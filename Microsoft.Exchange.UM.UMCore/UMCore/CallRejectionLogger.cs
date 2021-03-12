using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200007C RID: 124
	internal class CallRejectionLogger : StatisticsLogger
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x00018F0F File Offset: 0x0001710F
		protected CallRejectionLogger()
		{
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00018F22 File Offset: 0x00017122
		public static CallRejectionLogger Instance
		{
			get
			{
				return CallRejectionLogger.instance;
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00018F2C File Offset: 0x0001712C
		public new void Init()
		{
			if (CommonConstants.UseDataCenterLogging)
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					this.Component = currentProcess.MainModule.ModuleName;
				}
				base.Init(AppConfig.Instance.Service.CallRejectionLoggingEnabled, "Logging\\UMCallRejectionLogs");
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00018F90 File Offset: 0x00017190
		protected override StatisticsLogger.StatisticsLogSchema LogSchema
		{
			get
			{
				return this.logSchema;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018F98 File Offset: 0x00017198
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CallRejectionLogger>(this);
		}

		// Token: 0x0400023D RID: 573
		private const string LogPath = "Logging\\UMCallRejectionLogs";

		// Token: 0x0400023E RID: 574
		private readonly StatisticsLogger.StatisticsLogSchema logSchema = new CallRejectionLogger.CallRejectionLogSchema();

		// Token: 0x0400023F RID: 575
		private static CallRejectionLogger instance = new CallRejectionLogger();

		// Token: 0x04000240 RID: 576
		protected string Component;

		// Token: 0x0200007D RID: 125
		private enum Field
		{
			// Token: 0x04000242 RID: 578
			TimeStamp,
			// Token: 0x04000243 RID: 579
			UMServerName,
			// Token: 0x04000244 RID: 580
			Component,
			// Token: 0x04000245 RID: 581
			ErrorCode,
			// Token: 0x04000246 RID: 582
			ErrorType,
			// Token: 0x04000247 RID: 583
			ErrorCategory,
			// Token: 0x04000248 RID: 584
			ErrorDescription,
			// Token: 0x04000249 RID: 585
			ExtraInfo
		}

		// Token: 0x0200007E RID: 126
		public class CallRejectionLogSchema : StatisticsLogger.StatisticsLogSchema
		{
			// Token: 0x060005C0 RID: 1472 RVA: 0x00018FAC File Offset: 0x000171AC
			public CallRejectionLogSchema() : this("CallRejection")
			{
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x00018FB9 File Offset: 0x000171B9
			protected CallRejectionLogSchema(string logType) : base("1.0", logType, CallRejectionLogger.CallRejectionLogSchema.columns)
			{
			}

			// Token: 0x0400024A RID: 586
			private const string CallRejectionLogType = "CallRejection";

			// Token: 0x0400024B RID: 587
			private const string CallRejectionLogVersion = "1.0";

			// Token: 0x0400024C RID: 588
			private static readonly StatisticsLogger.StatisticsLogColumn[] columns = new StatisticsLogger.StatisticsLogColumn[]
			{
				new StatisticsLogger.StatisticsLogColumn(CallRejectionLogger.Field.TimeStamp.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallRejectionLogger.Field.UMServerName.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallRejectionLogger.Field.Component.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallRejectionLogger.Field.ErrorCode.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallRejectionLogger.Field.ErrorType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallRejectionLogger.Field.ErrorCategory.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallRejectionLogger.Field.ErrorDescription.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallRejectionLogger.Field.ExtraInfo.ToString(), false)
			};
		}

		// Token: 0x0200007F RID: 127
		public class CallRejectionLogRow : StatisticsLogger.StatisticsLogRow
		{
			// Token: 0x060005C3 RID: 1475 RVA: 0x00019086 File Offset: 0x00017286
			public CallRejectionLogRow() : base(CallRejectionLogger.Instance.LogSchema)
			{
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00019098 File Offset: 0x00017298
			// (set) Token: 0x060005C5 RID: 1477 RVA: 0x000190A0 File Offset: 0x000172A0
			public string UMServerName { get; set; }

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x060005C6 RID: 1478 RVA: 0x000190A9 File Offset: 0x000172A9
			// (set) Token: 0x060005C7 RID: 1479 RVA: 0x000190B1 File Offset: 0x000172B1
			public DateTime TimeStamp { get; set; }

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000190BA File Offset: 0x000172BA
			// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000190C2 File Offset: 0x000172C2
			public int ErrorCode { get; set; }

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x060005CA RID: 1482 RVA: 0x000190CB File Offset: 0x000172CB
			// (set) Token: 0x060005CB RID: 1483 RVA: 0x000190D3 File Offset: 0x000172D3
			public string ErrorType { get; set; }

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x060005CC RID: 1484 RVA: 0x000190DC File Offset: 0x000172DC
			// (set) Token: 0x060005CD RID: 1485 RVA: 0x000190E4 File Offset: 0x000172E4
			public string ErrorCategory { get; set; }

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x060005CE RID: 1486 RVA: 0x000190ED File Offset: 0x000172ED
			// (set) Token: 0x060005CF RID: 1487 RVA: 0x000190F5 File Offset: 0x000172F5
			public string ErrorDescription { get; set; }

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000190FE File Offset: 0x000172FE
			// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00019106 File Offset: 0x00017306
			public string ExtraInfo { get; set; }

			// Token: 0x060005D2 RID: 1490 RVA: 0x00019110 File Offset: 0x00017310
			public override void PopulateFields()
			{
				base.Fields[0] = this.TimeStamp.ToString("o");
				base.Fields[1] = this.UMServerName;
				base.Fields[2] = CallRejectionLogger.Instance.Component;
				base.Fields[3] = this.ErrorCode.ToString();
				base.Fields[4] = this.ErrorType;
				base.Fields[5] = this.ErrorCategory;
				base.Fields[6] = this.ErrorDescription;
				base.Fields[7] = this.ExtraInfo;
			}
		}
	}
}
