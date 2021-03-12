using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000C7 RID: 199
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class CDRDataType
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0002010E File Offset: 0x0001E30E
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00020116 File Offset: 0x0001E316
		public DateTime CallStartTime
		{
			get
			{
				return this.callStartTimeField;
			}
			set
			{
				this.callStartTimeField = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0002011F File Offset: 0x0001E31F
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00020127 File Offset: 0x0001E327
		public string CallType
		{
			get
			{
				return this.callTypeField;
			}
			set
			{
				this.callTypeField = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00020130 File Offset: 0x0001E330
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00020138 File Offset: 0x0001E338
		public string CallIdentity
		{
			get
			{
				return this.callIdentityField;
			}
			set
			{
				this.callIdentityField = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00020141 File Offset: 0x0001E341
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00020149 File Offset: 0x0001E349
		public string ParentCallIdentity
		{
			get
			{
				return this.parentCallIdentityField;
			}
			set
			{
				this.parentCallIdentityField = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00020152 File Offset: 0x0001E352
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0002015A File Offset: 0x0001E35A
		public string UMServerName
		{
			get
			{
				return this.uMServerNameField;
			}
			set
			{
				this.uMServerNameField = value;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00020163 File Offset: 0x0001E363
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0002016B File Offset: 0x0001E36B
		public string DialPlanGuid
		{
			get
			{
				return this.dialPlanGuidField;
			}
			set
			{
				this.dialPlanGuidField = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00020174 File Offset: 0x0001E374
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0002017C File Offset: 0x0001E37C
		public string DialPlanName
		{
			get
			{
				return this.dialPlanNameField;
			}
			set
			{
				this.dialPlanNameField = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00020185 File Offset: 0x0001E385
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0002018D File Offset: 0x0001E38D
		public int CallDuration
		{
			get
			{
				return this.callDurationField;
			}
			set
			{
				this.callDurationField = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00020196 File Offset: 0x0001E396
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x0002019E File Offset: 0x0001E39E
		public string IPGatewayAddress
		{
			get
			{
				return this.iPGatewayAddressField;
			}
			set
			{
				this.iPGatewayAddressField = value;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x000201A7 File Offset: 0x0001E3A7
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x000201AF File Offset: 0x0001E3AF
		public string IPGatewayName
		{
			get
			{
				return this.iPGatewayNameField;
			}
			set
			{
				this.iPGatewayNameField = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x000201B8 File Offset: 0x0001E3B8
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x000201C0 File Offset: 0x0001E3C0
		public string GatewayGuid
		{
			get
			{
				return this.gatewayGuidField;
			}
			set
			{
				this.gatewayGuidField = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x000201C9 File Offset: 0x0001E3C9
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x000201D1 File Offset: 0x0001E3D1
		public string CalledPhoneNumber
		{
			get
			{
				return this.calledPhoneNumberField;
			}
			set
			{
				this.calledPhoneNumberField = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x000201DA File Offset: 0x0001E3DA
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x000201E2 File Offset: 0x0001E3E2
		public string CallerPhoneNumber
		{
			get
			{
				return this.callerPhoneNumberField;
			}
			set
			{
				this.callerPhoneNumberField = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x000201EB File Offset: 0x0001E3EB
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x000201F3 File Offset: 0x0001E3F3
		public string OfferResult
		{
			get
			{
				return this.offerResultField;
			}
			set
			{
				this.offerResultField = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000201FC File Offset: 0x0001E3FC
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00020204 File Offset: 0x0001E404
		public string DropCallReason
		{
			get
			{
				return this.dropCallReasonField;
			}
			set
			{
				this.dropCallReasonField = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0002020D File Offset: 0x0001E40D
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00020215 File Offset: 0x0001E415
		public string ReasonForCall
		{
			get
			{
				return this.reasonForCallField;
			}
			set
			{
				this.reasonForCallField = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002021E File Offset: 0x0001E41E
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00020226 File Offset: 0x0001E426
		public string TransferredNumber
		{
			get
			{
				return this.transferredNumberField;
			}
			set
			{
				this.transferredNumberField = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002022F File Offset: 0x0001E42F
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00020237 File Offset: 0x0001E437
		public string DialedString
		{
			get
			{
				return this.dialedStringField;
			}
			set
			{
				this.dialedStringField = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00020240 File Offset: 0x0001E440
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00020248 File Offset: 0x0001E448
		public string CallerMailboxAlias
		{
			get
			{
				return this.callerMailboxAliasField;
			}
			set
			{
				this.callerMailboxAliasField = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00020251 File Offset: 0x0001E451
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00020259 File Offset: 0x0001E459
		public string CalleeMailboxAlias
		{
			get
			{
				return this.calleeMailboxAliasField;
			}
			set
			{
				this.calleeMailboxAliasField = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00020262 File Offset: 0x0001E462
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x0002026A File Offset: 0x0001E46A
		public string CallerLegacyExchangeDN
		{
			get
			{
				return this.callerLegacyExchangeDNField;
			}
			set
			{
				this.callerLegacyExchangeDNField = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00020273 File Offset: 0x0001E473
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x0002027B File Offset: 0x0001E47B
		public string CalleeLegacyExchangeDN
		{
			get
			{
				return this.calleeLegacyExchangeDNField;
			}
			set
			{
				this.calleeLegacyExchangeDNField = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00020284 File Offset: 0x0001E484
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0002028C File Offset: 0x0001E48C
		public string AutoAttendantName
		{
			get
			{
				return this.autoAttendantNameField;
			}
			set
			{
				this.autoAttendantNameField = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00020295 File Offset: 0x0001E495
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0002029D File Offset: 0x0001E49D
		public AudioQualityType AudioQualityMetrics
		{
			get
			{
				return this.audioQualityMetricsField;
			}
			set
			{
				this.audioQualityMetricsField = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x000202A6 File Offset: 0x0001E4A6
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x000202AE File Offset: 0x0001E4AE
		public DateTime CreationTime
		{
			get
			{
				return this.creationTimeField;
			}
			set
			{
				this.creationTimeField = value;
			}
		}

		// Token: 0x0400058C RID: 1420
		private DateTime callStartTimeField;

		// Token: 0x0400058D RID: 1421
		private string callTypeField;

		// Token: 0x0400058E RID: 1422
		private string callIdentityField;

		// Token: 0x0400058F RID: 1423
		private string parentCallIdentityField;

		// Token: 0x04000590 RID: 1424
		private string uMServerNameField;

		// Token: 0x04000591 RID: 1425
		private string dialPlanGuidField;

		// Token: 0x04000592 RID: 1426
		private string dialPlanNameField;

		// Token: 0x04000593 RID: 1427
		private int callDurationField;

		// Token: 0x04000594 RID: 1428
		private string iPGatewayAddressField;

		// Token: 0x04000595 RID: 1429
		private string iPGatewayNameField;

		// Token: 0x04000596 RID: 1430
		private string gatewayGuidField;

		// Token: 0x04000597 RID: 1431
		private string calledPhoneNumberField;

		// Token: 0x04000598 RID: 1432
		private string callerPhoneNumberField;

		// Token: 0x04000599 RID: 1433
		private string offerResultField;

		// Token: 0x0400059A RID: 1434
		private string dropCallReasonField;

		// Token: 0x0400059B RID: 1435
		private string reasonForCallField;

		// Token: 0x0400059C RID: 1436
		private string transferredNumberField;

		// Token: 0x0400059D RID: 1437
		private string dialedStringField;

		// Token: 0x0400059E RID: 1438
		private string callerMailboxAliasField;

		// Token: 0x0400059F RID: 1439
		private string calleeMailboxAliasField;

		// Token: 0x040005A0 RID: 1440
		private string callerLegacyExchangeDNField;

		// Token: 0x040005A1 RID: 1441
		private string calleeLegacyExchangeDNField;

		// Token: 0x040005A2 RID: 1442
		private string autoAttendantNameField;

		// Token: 0x040005A3 RID: 1443
		private AudioQualityType audioQualityMetricsField;

		// Token: 0x040005A4 RID: 1444
		private DateTime creationTimeField;
	}
}
