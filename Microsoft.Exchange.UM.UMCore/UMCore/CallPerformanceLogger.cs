using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000078 RID: 120
	internal class CallPerformanceLogger : StatisticsLogger
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x00018ABF File Offset: 0x00016CBF
		protected CallPerformanceLogger()
		{
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00018AD2 File Offset: 0x00016CD2
		public static CallPerformanceLogger Instance
		{
			get
			{
				return CallPerformanceLogger.instance;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00018AD9 File Offset: 0x00016CD9
		protected override StatisticsLogger.StatisticsLogSchema LogSchema
		{
			get
			{
				return this.logSchema;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00018AE1 File Offset: 0x00016CE1
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CallPerformanceLogger>(this);
		}

		// Token: 0x04000217 RID: 535
		private readonly StatisticsLogger.StatisticsLogSchema logSchema = new CallPerformanceLogger.CallPerformanceLogSchema();

		// Token: 0x04000218 RID: 536
		private static CallPerformanceLogger instance = new CallPerformanceLogger();

		// Token: 0x02000079 RID: 121
		private enum Field
		{
			// Token: 0x0400021A RID: 538
			CallId,
			// Token: 0x0400021B RID: 539
			UMServerName,
			// Token: 0x0400021C RID: 540
			Component,
			// Token: 0x0400021D RID: 541
			CallStartTime,
			// Token: 0x0400021E RID: 542
			Duration,
			// Token: 0x0400021F RID: 543
			AdCount,
			// Token: 0x04000220 RID: 544
			AdLatency,
			// Token: 0x04000221 RID: 545
			MServeCount,
			// Token: 0x04000222 RID: 546
			MServeLatency,
			// Token: 0x04000223 RID: 547
			UserDataRpcCount,
			// Token: 0x04000224 RID: 548
			UserDataRpcLatency,
			// Token: 0x04000225 RID: 549
			UserDataAdCount,
			// Token: 0x04000226 RID: 550
			UserDataAdLatency,
			// Token: 0x04000227 RID: 551
			UserDataDuration,
			// Token: 0x04000228 RID: 552
			UserDataTimedOut,
			// Token: 0x04000229 RID: 553
			UserAgent
		}

		// Token: 0x0200007A RID: 122
		public class CallPerformanceLogSchema : StatisticsLogger.StatisticsLogSchema
		{
			// Token: 0x06000595 RID: 1429 RVA: 0x00018AF5 File Offset: 0x00016CF5
			public CallPerformanceLogSchema() : this("CallPerformance")
			{
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x00018B02 File Offset: 0x00016D02
			protected CallPerformanceLogSchema(string logType) : base("1.1", logType, CallPerformanceLogger.CallPerformanceLogSchema.columns)
			{
			}

			// Token: 0x0400022A RID: 554
			private const string CallPerformanceLogType = "CallPerformance";

			// Token: 0x0400022B RID: 555
			private const string CallPerformanceLogVersion = "1.1";

			// Token: 0x0400022C RID: 556
			private static readonly StatisticsLogger.StatisticsLogColumn[] columns = new StatisticsLogger.StatisticsLogColumn[]
			{
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.CallId.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.UMServerName.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.Component.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.CallStartTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.Duration.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.AdCount.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.AdLatency.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.MServeCount.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.MServeLatency.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.UserDataRpcCount.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.UserDataRpcLatency.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.UserDataAdCount.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.UserDataAdLatency.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.UserDataDuration.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.UserDataTimedOut.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallPerformanceLogger.Field.UserAgent.ToString(), false)
			};
		}

		// Token: 0x0200007B RID: 123
		public class CallPerformanceLogRow : StatisticsLogger.StatisticsLogRow
		{
			// Token: 0x06000598 RID: 1432 RVA: 0x00018C81 File Offset: 0x00016E81
			public CallPerformanceLogRow() : base(CallPerformanceLogger.Instance.LogSchema)
			{
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000599 RID: 1433 RVA: 0x00018C93 File Offset: 0x00016E93
			// (set) Token: 0x0600059A RID: 1434 RVA: 0x00018C9B File Offset: 0x00016E9B
			public string CallId { get; set; }

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x0600059B RID: 1435 RVA: 0x00018CA4 File Offset: 0x00016EA4
			// (set) Token: 0x0600059C RID: 1436 RVA: 0x00018CAC File Offset: 0x00016EAC
			public string UMServerName { get; set; }

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x0600059D RID: 1437 RVA: 0x00018CB5 File Offset: 0x00016EB5
			// (set) Token: 0x0600059E RID: 1438 RVA: 0x00018CBD File Offset: 0x00016EBD
			public string Component { get; set; }

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x0600059F RID: 1439 RVA: 0x00018CC6 File Offset: 0x00016EC6
			// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00018CCE File Offset: 0x00016ECE
			public DateTime CallStartTime { get; set; }

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00018CD7 File Offset: 0x00016ED7
			// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00018CDF File Offset: 0x00016EDF
			public int Duration { get; set; }

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00018CE8 File Offset: 0x00016EE8
			// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00018CF0 File Offset: 0x00016EF0
			public uint AdCount { get; set; }

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00018CF9 File Offset: 0x00016EF9
			// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00018D01 File Offset: 0x00016F01
			public int AdLatency { get; set; }

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00018D0A File Offset: 0x00016F0A
			// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00018D12 File Offset: 0x00016F12
			public uint MServeCount { get; set; }

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00018D1B File Offset: 0x00016F1B
			// (set) Token: 0x060005AA RID: 1450 RVA: 0x00018D23 File Offset: 0x00016F23
			public int MServeLatency { get; set; }

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x060005AB RID: 1451 RVA: 0x00018D2C File Offset: 0x00016F2C
			// (set) Token: 0x060005AC RID: 1452 RVA: 0x00018D34 File Offset: 0x00016F34
			public uint UserDataRpcCount { get; set; }

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x060005AD RID: 1453 RVA: 0x00018D3D File Offset: 0x00016F3D
			// (set) Token: 0x060005AE RID: 1454 RVA: 0x00018D45 File Offset: 0x00016F45
			public int UserDataRpcLatency { get; set; }

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x060005AF RID: 1455 RVA: 0x00018D4E File Offset: 0x00016F4E
			// (set) Token: 0x060005B0 RID: 1456 RVA: 0x00018D56 File Offset: 0x00016F56
			public uint UserDataAdCount { get; set; }

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00018D5F File Offset: 0x00016F5F
			// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00018D67 File Offset: 0x00016F67
			public int UserDataAdLatency { get; set; }

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00018D70 File Offset: 0x00016F70
			// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00018D78 File Offset: 0x00016F78
			public int UserDataDuration { get; set; }

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00018D81 File Offset: 0x00016F81
			// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00018D89 File Offset: 0x00016F89
			public bool UserDataTimedOut { get; set; }

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00018D92 File Offset: 0x00016F92
			// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00018D9A File Offset: 0x00016F9A
			public string UserAgent { get; set; }

			// Token: 0x060005B9 RID: 1465 RVA: 0x00018DA4 File Offset: 0x00016FA4
			public override void PopulateFields()
			{
				base.Fields[0] = this.CallId;
				base.Fields[1] = this.UMServerName;
				base.Fields[2] = this.Component.ToString();
				base.Fields[3] = this.CallStartTime.ToString("o");
				base.Fields[4] = this.Duration.ToString();
				base.Fields[5] = this.AdCount.ToString();
				base.Fields[6] = this.AdLatency.ToString();
				base.Fields[7] = this.MServeCount.ToString();
				base.Fields[8] = this.MServeLatency.ToString();
				base.Fields[9] = this.UserDataRpcCount.ToString();
				base.Fields[10] = this.UserDataRpcLatency.ToString();
				base.Fields[11] = this.UserDataAdCount.ToString();
				base.Fields[12] = this.UserDataAdLatency.ToString();
				base.Fields[13] = this.UserDataDuration.ToString();
				base.Fields[14] = this.UserDataTimedOut.ToString();
				base.Fields[15] = this.UserAgent.ToString();
			}
		}
	}
}
