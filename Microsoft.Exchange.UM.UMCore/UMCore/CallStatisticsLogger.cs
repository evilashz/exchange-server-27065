using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000074 RID: 116
	internal class CallStatisticsLogger : StatisticsLogger
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x0001833E File Offset: 0x0001653E
		protected CallStatisticsLogger()
		{
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00018351 File Offset: 0x00016551
		public static CallStatisticsLogger Instance
		{
			get
			{
				return CallStatisticsLogger.instance;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00018358 File Offset: 0x00016558
		protected override StatisticsLogger.StatisticsLogSchema LogSchema
		{
			get
			{
				return this.logSchema;
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00018360 File Offset: 0x00016560
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CallStatisticsLogger>(this);
		}

		// Token: 0x040001D3 RID: 467
		private readonly StatisticsLogger.StatisticsLogSchema logSchema = new CallStatisticsLogger.CallStatisticsLogSchema();

		// Token: 0x040001D4 RID: 468
		private static CallStatisticsLogger instance = new CallStatisticsLogger();

		// Token: 0x02000075 RID: 117
		private enum Field
		{
			// Token: 0x040001D6 RID: 470
			CallStartTime,
			// Token: 0x040001D7 RID: 471
			CallType,
			// Token: 0x040001D8 RID: 472
			CallId,
			// Token: 0x040001D9 RID: 473
			ParentCallIdentity,
			// Token: 0x040001DA RID: 474
			UMServerName,
			// Token: 0x040001DB RID: 475
			DialPlanGuid,
			// Token: 0x040001DC RID: 476
			DialPlanName,
			// Token: 0x040001DD RID: 477
			CallDuration,
			// Token: 0x040001DE RID: 478
			IPGatewayAddress,
			// Token: 0x040001DF RID: 479
			GatewayGuid,
			// Token: 0x040001E0 RID: 480
			CalledPhoneNumber,
			// Token: 0x040001E1 RID: 481
			CallerPhoneNumber,
			// Token: 0x040001E2 RID: 482
			OfferResult,
			// Token: 0x040001E3 RID: 483
			DropCallReason,
			// Token: 0x040001E4 RID: 484
			ReasonForCall,
			// Token: 0x040001E5 RID: 485
			TransferredNumber,
			// Token: 0x040001E6 RID: 486
			DialedString,
			// Token: 0x040001E7 RID: 487
			CallerMailboxAlias,
			// Token: 0x040001E8 RID: 488
			CalleeMailboxAlias,
			// Token: 0x040001E9 RID: 489
			AutoAttendantName,
			// Token: 0x040001EA RID: 490
			OrganizationId,
			// Token: 0x040001EB RID: 491
			AudioCodec,
			// Token: 0x040001EC RID: 492
			AudioQualityBurstDensity,
			// Token: 0x040001ED RID: 493
			AudioQualityBurstDuration,
			// Token: 0x040001EE RID: 494
			AudioQualityJitter,
			// Token: 0x040001EF RID: 495
			AudioQualityNMOS,
			// Token: 0x040001F0 RID: 496
			AudioQualityNMOSDegradation,
			// Token: 0x040001F1 RID: 497
			AudioQualityNMOSDegradationJitter,
			// Token: 0x040001F2 RID: 498
			AudioQualityNMOSDegradationPacketLoss,
			// Token: 0x040001F3 RID: 499
			AudioQualityPacketLoss,
			// Token: 0x040001F4 RID: 500
			AudioQualityRoundTrip
		}

		// Token: 0x02000076 RID: 118
		public class CallStatisticsLogSchema : StatisticsLogger.StatisticsLogSchema
		{
			// Token: 0x0600054D RID: 1357 RVA: 0x00018374 File Offset: 0x00016574
			public CallStatisticsLogSchema() : this("CallStatistics")
			{
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x00018381 File Offset: 0x00016581
			protected CallStatisticsLogSchema(string logType) : base("1.2", logType, CallStatisticsLogger.CallStatisticsLogSchema.columns)
			{
			}

			// Token: 0x040001F5 RID: 501
			private const string CallStatisticsLogType = "CallStatistics";

			// Token: 0x040001F6 RID: 502
			private const string CallStatisticsLogVersion = "1.2";

			// Token: 0x040001F7 RID: 503
			private static readonly StatisticsLogger.StatisticsLogColumn[] columns = new StatisticsLogger.StatisticsLogColumn[]
			{
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.CallStartTime.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.CallType.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.CallId.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.ParentCallIdentity.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.UMServerName.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.DialPlanGuid.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.DialPlanName.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.CallDuration.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.IPGatewayAddress.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.GatewayGuid.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.CalledPhoneNumber.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.CallerPhoneNumber.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.OfferResult.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.DropCallReason.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.ReasonForCall.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.TransferredNumber.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.DialedString.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.CallerMailboxAlias.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.CalleeMailboxAlias.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AutoAttendantName.ToString(), true),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.OrganizationId.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioCodec.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioQualityBurstDensity.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioQualityBurstDuration.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioQualityJitter.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioQualityNMOS.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioQualityNMOSDegradation.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioQualityNMOSDegradationJitter.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioQualityNMOSDegradationPacketLoss.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioQualityPacketLoss.ToString(), false),
				new StatisticsLogger.StatisticsLogColumn(CallStatisticsLogger.Field.AudioQualityRoundTrip.ToString(), false)
			};
		}

		// Token: 0x02000077 RID: 119
		public class CallStatisticsLogRow : StatisticsLogger.StatisticsLogRow
		{
			// Token: 0x06000550 RID: 1360 RVA: 0x00018647 File Offset: 0x00016847
			public CallStatisticsLogRow() : base(CallStatisticsLogger.Instance.LogSchema)
			{
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06000551 RID: 1361 RVA: 0x00018659 File Offset: 0x00016859
			// (set) Token: 0x06000552 RID: 1362 RVA: 0x00018661 File Offset: 0x00016861
			public DateTime CallStartTime { get; set; }

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001866A File Offset: 0x0001686A
			// (set) Token: 0x06000554 RID: 1364 RVA: 0x00018672 File Offset: 0x00016872
			public string CallType { get; set; }

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001867B File Offset: 0x0001687B
			// (set) Token: 0x06000556 RID: 1366 RVA: 0x00018683 File Offset: 0x00016883
			public string CallIdentity { get; set; }

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001868C File Offset: 0x0001688C
			// (set) Token: 0x06000558 RID: 1368 RVA: 0x00018694 File Offset: 0x00016894
			public string ParentCallIdentity { get; set; }

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001869D File Offset: 0x0001689D
			// (set) Token: 0x0600055A RID: 1370 RVA: 0x000186A5 File Offset: 0x000168A5
			public string UMServerName { get; set; }

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x0600055B RID: 1371 RVA: 0x000186AE File Offset: 0x000168AE
			// (set) Token: 0x0600055C RID: 1372 RVA: 0x000186B6 File Offset: 0x000168B6
			public Guid DialPlanGuid { get; set; }

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x0600055D RID: 1373 RVA: 0x000186BF File Offset: 0x000168BF
			// (set) Token: 0x0600055E RID: 1374 RVA: 0x000186C7 File Offset: 0x000168C7
			public string DialPlanName { get; set; }

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x0600055F RID: 1375 RVA: 0x000186D0 File Offset: 0x000168D0
			// (set) Token: 0x06000560 RID: 1376 RVA: 0x000186D8 File Offset: 0x000168D8
			public int CallDuration { get; set; }

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000561 RID: 1377 RVA: 0x000186E1 File Offset: 0x000168E1
			// (set) Token: 0x06000562 RID: 1378 RVA: 0x000186E9 File Offset: 0x000168E9
			public string IPGatewayAddress { get; set; }

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000563 RID: 1379 RVA: 0x000186F2 File Offset: 0x000168F2
			// (set) Token: 0x06000564 RID: 1380 RVA: 0x000186FA File Offset: 0x000168FA
			public Guid GatewayGuid { get; set; }

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x06000565 RID: 1381 RVA: 0x00018703 File Offset: 0x00016903
			// (set) Token: 0x06000566 RID: 1382 RVA: 0x0001870B File Offset: 0x0001690B
			public string CalledPhoneNumber { get; set; }

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000567 RID: 1383 RVA: 0x00018714 File Offset: 0x00016914
			// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001871C File Offset: 0x0001691C
			public string CallerPhoneNumber { get; set; }

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000569 RID: 1385 RVA: 0x00018725 File Offset: 0x00016925
			// (set) Token: 0x0600056A RID: 1386 RVA: 0x0001872D File Offset: 0x0001692D
			public string OfferResult { get; set; }

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x0600056B RID: 1387 RVA: 0x00018736 File Offset: 0x00016936
			// (set) Token: 0x0600056C RID: 1388 RVA: 0x0001873E File Offset: 0x0001693E
			public string DropCallReason { get; set; }

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x0600056D RID: 1389 RVA: 0x00018747 File Offset: 0x00016947
			// (set) Token: 0x0600056E RID: 1390 RVA: 0x0001874F File Offset: 0x0001694F
			public string ReasonForCall { get; set; }

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x0600056F RID: 1391 RVA: 0x00018758 File Offset: 0x00016958
			// (set) Token: 0x06000570 RID: 1392 RVA: 0x00018760 File Offset: 0x00016960
			public string TransferredNumber { get; set; }

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000571 RID: 1393 RVA: 0x00018769 File Offset: 0x00016969
			// (set) Token: 0x06000572 RID: 1394 RVA: 0x00018771 File Offset: 0x00016971
			public string DialedString { get; set; }

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001877A File Offset: 0x0001697A
			// (set) Token: 0x06000574 RID: 1396 RVA: 0x00018782 File Offset: 0x00016982
			public string CallerMailboxAlias { get; set; }

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001878B File Offset: 0x0001698B
			// (set) Token: 0x06000576 RID: 1398 RVA: 0x00018793 File Offset: 0x00016993
			public string CalleeMailboxAlias { get; set; }

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001879C File Offset: 0x0001699C
			// (set) Token: 0x06000578 RID: 1400 RVA: 0x000187A4 File Offset: 0x000169A4
			public string AutoAttendantName { get; set; }

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000579 RID: 1401 RVA: 0x000187AD File Offset: 0x000169AD
			// (set) Token: 0x0600057A RID: 1402 RVA: 0x000187B5 File Offset: 0x000169B5
			public string OrganizationId { get; set; }

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x0600057B RID: 1403 RVA: 0x000187BE File Offset: 0x000169BE
			// (set) Token: 0x0600057C RID: 1404 RVA: 0x000187C6 File Offset: 0x000169C6
			public string AudioCodec { get; set; }

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x0600057D RID: 1405 RVA: 0x000187CF File Offset: 0x000169CF
			// (set) Token: 0x0600057E RID: 1406 RVA: 0x000187D7 File Offset: 0x000169D7
			public float AudioQualityBurstDensity { get; set; }

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x0600057F RID: 1407 RVA: 0x000187E0 File Offset: 0x000169E0
			// (set) Token: 0x06000580 RID: 1408 RVA: 0x000187E8 File Offset: 0x000169E8
			public float AudioQualityBurstDuration { get; set; }

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000581 RID: 1409 RVA: 0x000187F1 File Offset: 0x000169F1
			// (set) Token: 0x06000582 RID: 1410 RVA: 0x000187F9 File Offset: 0x000169F9
			public float AudioQualityJitter { get; set; }

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000583 RID: 1411 RVA: 0x00018802 File Offset: 0x00016A02
			// (set) Token: 0x06000584 RID: 1412 RVA: 0x0001880A File Offset: 0x00016A0A
			public float AudioQualityNMOS { get; set; }

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000585 RID: 1413 RVA: 0x00018813 File Offset: 0x00016A13
			// (set) Token: 0x06000586 RID: 1414 RVA: 0x0001881B File Offset: 0x00016A1B
			public float AudioQualityNMOSDegradation { get; set; }

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000587 RID: 1415 RVA: 0x00018824 File Offset: 0x00016A24
			// (set) Token: 0x06000588 RID: 1416 RVA: 0x0001882C File Offset: 0x00016A2C
			public float AudioQualityNMOSDegradationJitter { get; set; }

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000589 RID: 1417 RVA: 0x00018835 File Offset: 0x00016A35
			// (set) Token: 0x0600058A RID: 1418 RVA: 0x0001883D File Offset: 0x00016A3D
			public float AudioQualityNMOSDegradationPacketLoss { get; set; }

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600058B RID: 1419 RVA: 0x00018846 File Offset: 0x00016A46
			// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001884E File Offset: 0x00016A4E
			public float AudioQualityPacketLoss { get; set; }

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x00018857 File Offset: 0x00016A57
			// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001885F File Offset: 0x00016A5F
			public float AudioQualityRoundTrip { get; set; }

			// Token: 0x0600058F RID: 1423 RVA: 0x00018868 File Offset: 0x00016A68
			public override void PopulateFields()
			{
				base.Fields[0] = this.CallStartTime.ToString(CultureInfo.InvariantCulture);
				base.Fields[1] = this.CallType;
				base.Fields[2] = this.CallIdentity;
				base.Fields[3] = this.ParentCallIdentity;
				base.Fields[4] = this.UMServerName;
				base.Fields[5] = this.DialPlanGuid.ToString();
				base.Fields[6] = this.DialPlanName;
				base.Fields[7] = this.CallDuration.ToString();
				base.Fields[8] = this.IPGatewayAddress;
				base.Fields[9] = this.GatewayGuid.ToString();
				base.Fields[10] = this.CalledPhoneNumber;
				base.Fields[11] = this.CallerPhoneNumber;
				base.Fields[12] = this.OfferResult;
				base.Fields[13] = this.DropCallReason;
				base.Fields[14] = this.ReasonForCall;
				base.Fields[15] = this.TransferredNumber;
				base.Fields[16] = this.DialedString;
				base.Fields[17] = this.CallerMailboxAlias;
				base.Fields[18] = this.CalleeMailboxAlias;
				base.Fields[19] = this.AutoAttendantName;
				base.Fields[20] = this.OrganizationId;
				base.Fields[21] = this.AudioCodec;
				base.Fields[22] = this.AudioQualityBurstDensity.ToString();
				base.Fields[23] = this.AudioQualityBurstDuration.ToString();
				base.Fields[24] = this.AudioQualityJitter.ToString();
				base.Fields[25] = this.AudioQualityNMOS.ToString();
				base.Fields[26] = this.AudioQualityNMOSDegradation.ToString();
				base.Fields[27] = this.AudioQualityNMOSDegradationJitter.ToString();
				base.Fields[28] = this.AudioQualityNMOSDegradationPacketLoss.ToString();
				base.Fields[29] = this.AudioQualityPacketLoss.ToString();
				base.Fields[30] = this.AudioQualityRoundTrip.ToString();
			}
		}
	}
}
