using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.SQM;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200005C RID: 92
	[Cmdlet("New", "InboxRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class NewInboxRule : NewTenantADTaskBase<InboxRule>
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00019179 File Offset: 0x00017379
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00019181 File Offset: 0x00017381
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001918A File Offset: 0x0001738A
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x00019192 File Offset: 0x00017392
		[Parameter(Mandatory = false)]
		public SwitchParameter AlwaysDeleteOutlookRulesBlob { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001919B File Offset: 0x0001739B
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x000191B2 File Offset: 0x000173B2
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000191C5 File Offset: 0x000173C5
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x000191D2 File Offset: 0x000173D2
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "Identity")]
		public string Name
		{
			get
			{
				return this.DataObject.Name;
			}
			set
			{
				this.DataObject.Name = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000191E0 File Offset: 0x000173E0
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x000191ED File Offset: 0x000173ED
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public int Priority
		{
			get
			{
				return this.DataObject.Priority;
			}
			set
			{
				this.DataObject.Priority = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000191FB File Offset: 0x000173FB
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00019208 File Offset: 0x00017408
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> BodyContainsWords
		{
			get
			{
				return this.DataObject.BodyContainsWords;
			}
			set
			{
				this.DataObject.BodyContainsWords = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00019216 File Offset: 0x00017416
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x00019223 File Offset: 0x00017423
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> ExceptIfBodyContainsWords
		{
			get
			{
				return this.DataObject.ExceptIfBodyContainsWords;
			}
			set
			{
				this.DataObject.ExceptIfBodyContainsWords = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00019231 File Offset: 0x00017431
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0001923E File Offset: 0x0001743E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string FlaggedForAction
		{
			get
			{
				return this.DataObject.FlaggedForAction;
			}
			set
			{
				this.DataObject.FlaggedForAction = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001924C File Offset: 0x0001744C
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x00019259 File Offset: 0x00017459
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string ExceptIfFlaggedForAction
		{
			get
			{
				return this.DataObject.ExceptIfFlaggedForAction;
			}
			set
			{
				this.DataObject.ExceptIfFlaggedForAction = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00019267 File Offset: 0x00017467
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0001927E File Offset: 0x0001747E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RecipientIdParameter[] From
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.From];
			}
			set
			{
				base.Fields[InboxRuleSchema.From] = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00019291 File Offset: 0x00017491
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x000192A8 File Offset: 0x000174A8
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RecipientIdParameter[] ExceptIfFrom
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.ExceptIfFrom];
			}
			set
			{
				base.Fields[InboxRuleSchema.ExceptIfFrom] = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000192BB File Offset: 0x000174BB
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x000192C8 File Offset: 0x000174C8
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> FromAddressContainsWords
		{
			get
			{
				return this.DataObject.FromAddressContainsWords;
			}
			set
			{
				this.DataObject.FromAddressContainsWords = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x000192D6 File Offset: 0x000174D6
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x000192E3 File Offset: 0x000174E3
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> ExceptIfFromAddressContainsWords
		{
			get
			{
				return this.DataObject.ExceptIfFromAddressContainsWords;
			}
			set
			{
				this.DataObject.ExceptIfFromAddressContainsWords = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000192F1 File Offset: 0x000174F1
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x000192FE File Offset: 0x000174FE
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool HasAttachment
		{
			get
			{
				return this.DataObject.HasAttachment;
			}
			set
			{
				this.DataObject.HasAttachment = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001930C File Offset: 0x0001750C
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x00019319 File Offset: 0x00017519
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool ExceptIfHasAttachment
		{
			get
			{
				return this.DataObject.ExceptIfHasAttachment;
			}
			set
			{
				this.DataObject.ExceptIfHasAttachment = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00019327 File Offset: 0x00017527
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0001933E File Offset: 0x0001753E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MessageClassificationIdParameter[] HasClassification
		{
			get
			{
				return (MessageClassificationIdParameter[])base.Fields[InboxRuleSchema.HasClassification];
			}
			set
			{
				base.Fields[InboxRuleSchema.HasClassification] = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00019351 File Offset: 0x00017551
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00019368 File Offset: 0x00017568
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MessageClassificationIdParameter[] ExceptIfHasClassification
		{
			get
			{
				return (MessageClassificationIdParameter[])base.Fields[InboxRuleSchema.ExceptIfHasClassification];
			}
			set
			{
				base.Fields[InboxRuleSchema.ExceptIfHasClassification] = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001937B File Offset: 0x0001757B
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00019388 File Offset: 0x00017588
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> HeaderContainsWords
		{
			get
			{
				return this.DataObject.HeaderContainsWords;
			}
			set
			{
				this.DataObject.HeaderContainsWords = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00019396 File Offset: 0x00017596
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x000193A3 File Offset: 0x000175A3
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> ExceptIfHeaderContainsWords
		{
			get
			{
				return this.DataObject.ExceptIfHeaderContainsWords;
			}
			set
			{
				this.DataObject.ExceptIfHeaderContainsWords = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000193B4 File Offset: 0x000175B4
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x000193D4 File Offset: 0x000175D4
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public InboxRuleMessageType MessageTypeMatches
		{
			get
			{
				return this.DataObject.MessageTypeMatches.Value;
			}
			set
			{
				this.DataObject.MessageTypeMatches = new InboxRuleMessageType?(value);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000193E8 File Offset: 0x000175E8
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x00019408 File Offset: 0x00017608
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public InboxRuleMessageType ExceptIfMessageTypeMatches
		{
			get
			{
				return this.DataObject.ExceptIfMessageTypeMatches.Value;
			}
			set
			{
				this.DataObject.ExceptIfMessageTypeMatches = new InboxRuleMessageType?(value);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001941B File Offset: 0x0001761B
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x00019428 File Offset: 0x00017628
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool MyNameInCcBox
		{
			get
			{
				return this.DataObject.MyNameInCcBox;
			}
			set
			{
				this.DataObject.MyNameInCcBox = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00019436 File Offset: 0x00017636
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x00019443 File Offset: 0x00017643
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool ExceptIfMyNameInCcBox
		{
			get
			{
				return this.DataObject.ExceptIfMyNameInCcBox;
			}
			set
			{
				this.DataObject.ExceptIfMyNameInCcBox = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00019451 File Offset: 0x00017651
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0001945E File Offset: 0x0001765E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool MyNameInToBox
		{
			get
			{
				return this.DataObject.MyNameInToBox;
			}
			set
			{
				this.DataObject.MyNameInToBox = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001946C File Offset: 0x0001766C
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x00019479 File Offset: 0x00017679
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool ExceptIfMyNameInToBox
		{
			get
			{
				return this.DataObject.ExceptIfMyNameInToBox;
			}
			set
			{
				this.DataObject.ExceptIfMyNameInToBox = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00019487 File Offset: 0x00017687
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x00019494 File Offset: 0x00017694
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool MyNameInToOrCcBox
		{
			get
			{
				return this.DataObject.MyNameInToOrCcBox;
			}
			set
			{
				this.DataObject.MyNameInToOrCcBox = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000194A2 File Offset: 0x000176A2
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x000194AF File Offset: 0x000176AF
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool ExceptIfMyNameInToOrCcBox
		{
			get
			{
				return this.DataObject.ExceptIfMyNameInToOrCcBox;
			}
			set
			{
				this.DataObject.ExceptIfMyNameInToOrCcBox = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x000194BD File Offset: 0x000176BD
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x000194CA File Offset: 0x000176CA
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool MyNameNotInToBox
		{
			get
			{
				return this.DataObject.MyNameNotInToBox;
			}
			set
			{
				this.DataObject.MyNameNotInToBox = value;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x000194D8 File Offset: 0x000176D8
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x000194E5 File Offset: 0x000176E5
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool ExceptIfMyNameNotInToBox
		{
			get
			{
				return this.DataObject.ExceptIfMyNameNotInToBox;
			}
			set
			{
				this.DataObject.ExceptIfMyNameNotInToBox = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000194F4 File Offset: 0x000176F4
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x00019514 File Offset: 0x00017714
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ExDateTime ReceivedAfterDate
		{
			get
			{
				return this.DataObject.ReceivedAfterDate.Value;
			}
			set
			{
				this.DataObject.ReceivedAfterDate = new ExDateTime?(value);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00019528 File Offset: 0x00017728
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x00019548 File Offset: 0x00017748
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ExDateTime ExceptIfReceivedAfterDate
		{
			get
			{
				return this.DataObject.ExceptIfReceivedAfterDate.Value;
			}
			set
			{
				this.DataObject.ExceptIfReceivedAfterDate = new ExDateTime?(value);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001955C File Offset: 0x0001775C
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0001957C File Offset: 0x0001777C
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ExDateTime ReceivedBeforeDate
		{
			get
			{
				return this.DataObject.ReceivedBeforeDate.Value;
			}
			set
			{
				this.DataObject.ReceivedBeforeDate = new ExDateTime?(value);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00019590 File Offset: 0x00017790
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x000195B0 File Offset: 0x000177B0
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ExDateTime ExceptIfReceivedBeforeDate
		{
			get
			{
				return this.DataObject.ExceptIfReceivedBeforeDate.Value;
			}
			set
			{
				this.DataObject.ExceptIfReceivedBeforeDate = new ExDateTime?(value);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x000195C3 File Offset: 0x000177C3
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x000195D0 File Offset: 0x000177D0
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> RecipientAddressContainsWords
		{
			get
			{
				return this.DataObject.RecipientAddressContainsWords;
			}
			set
			{
				this.DataObject.RecipientAddressContainsWords = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000195DE File Offset: 0x000177DE
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x000195EB File Offset: 0x000177EB
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return this.DataObject.ExceptIfRecipientAddressContainsWords;
			}
			set
			{
				this.DataObject.ExceptIfRecipientAddressContainsWords = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x000195F9 File Offset: 0x000177F9
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00019606 File Offset: 0x00017806
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool SentOnlyToMe
		{
			get
			{
				return this.DataObject.SentOnlyToMe;
			}
			set
			{
				this.DataObject.SentOnlyToMe = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00019614 File Offset: 0x00017814
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00019621 File Offset: 0x00017821
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool ExceptIfSentOnlyToMe
		{
			get
			{
				return this.DataObject.ExceptIfSentOnlyToMe;
			}
			set
			{
				this.DataObject.ExceptIfSentOnlyToMe = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001962F File Offset: 0x0001782F
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00019646 File Offset: 0x00017846
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RecipientIdParameter[] SentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.SentTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.SentTo] = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00019659 File Offset: 0x00017859
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x00019670 File Offset: 0x00017870
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RecipientIdParameter[] ExceptIfSentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.ExceptIfSentTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.ExceptIfSentTo] = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00019683 File Offset: 0x00017883
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00019690 File Offset: 0x00017890
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> SubjectContainsWords
		{
			get
			{
				return this.DataObject.SubjectContainsWords;
			}
			set
			{
				this.DataObject.SubjectContainsWords = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001969E File Offset: 0x0001789E
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x000196AB File Offset: 0x000178AB
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> ExceptIfSubjectContainsWords
		{
			get
			{
				return this.DataObject.ExceptIfSubjectContainsWords;
			}
			set
			{
				this.DataObject.ExceptIfSubjectContainsWords = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000196B9 File Offset: 0x000178B9
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x000196C6 File Offset: 0x000178C6
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> SubjectOrBodyContainsWords
		{
			get
			{
				return this.DataObject.SubjectOrBodyContainsWords;
			}
			set
			{
				this.DataObject.SubjectOrBodyContainsWords = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x000196D4 File Offset: 0x000178D4
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x000196E1 File Offset: 0x000178E1
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return this.DataObject.ExceptIfSubjectOrBodyContainsWords;
			}
			set
			{
				this.DataObject.ExceptIfSubjectOrBodyContainsWords = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x000196F0 File Offset: 0x000178F0
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00019710 File Offset: 0x00017910
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Microsoft.Exchange.Data.Storage.Importance WithImportance
		{
			get
			{
				return this.DataObject.WithImportance.Value;
			}
			set
			{
				this.DataObject.WithImportance = new Microsoft.Exchange.Data.Storage.Importance?(value);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00019724 File Offset: 0x00017924
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x00019744 File Offset: 0x00017944
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Microsoft.Exchange.Data.Storage.Importance ExceptIfWithImportance
		{
			get
			{
				return this.DataObject.ExceptIfWithImportance.Value;
			}
			set
			{
				this.DataObject.ExceptIfWithImportance = new Microsoft.Exchange.Data.Storage.Importance?(value);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00019757 File Offset: 0x00017957
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x00019764 File Offset: 0x00017964
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ByteQuantifiedSize? WithinSizeRangeMaximum
		{
			get
			{
				return this.DataObject.WithinSizeRangeMaximum;
			}
			set
			{
				this.DataObject.WithinSizeRangeMaximum = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00019772 File Offset: 0x00017972
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0001977F File Offset: 0x0001797F
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ByteQuantifiedSize? ExceptIfWithinSizeRangeMaximum
		{
			get
			{
				return this.DataObject.ExceptIfWithinSizeRangeMaximum;
			}
			set
			{
				this.DataObject.ExceptIfWithinSizeRangeMaximum = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001978D File Offset: 0x0001798D
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0001979A File Offset: 0x0001799A
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ByteQuantifiedSize? WithinSizeRangeMinimum
		{
			get
			{
				return this.DataObject.WithinSizeRangeMinimum;
			}
			set
			{
				this.DataObject.WithinSizeRangeMinimum = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000197A8 File Offset: 0x000179A8
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x000197B5 File Offset: 0x000179B5
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ByteQuantifiedSize? ExceptIfWithinSizeRangeMinimum
		{
			get
			{
				return this.DataObject.ExceptIfWithinSizeRangeMinimum;
			}
			set
			{
				this.DataObject.ExceptIfWithinSizeRangeMinimum = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x000197C4 File Offset: 0x000179C4
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x000197E4 File Offset: 0x000179E4
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Sensitivity WithSensitivity
		{
			get
			{
				return this.DataObject.WithSensitivity.Value;
			}
			set
			{
				this.DataObject.WithSensitivity = new Sensitivity?(value);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000197F8 File Offset: 0x000179F8
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00019818 File Offset: 0x00017A18
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Sensitivity ExceptIfWithSensitivity
		{
			get
			{
				return this.DataObject.ExceptIfWithSensitivity.Value;
			}
			set
			{
				this.DataObject.ExceptIfWithSensitivity = new Sensitivity?(value);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001982B File Offset: 0x00017A2B
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00019842 File Offset: 0x00017A42
		[Parameter(Mandatory = false)]
		public AggregationSubscriptionIdentity[] FromSubscription
		{
			get
			{
				return (AggregationSubscriptionIdentity[])base.Fields[InboxRuleSchema.FromSubscription];
			}
			set
			{
				base.Fields[InboxRuleSchema.FromSubscription] = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00019855 File Offset: 0x00017A55
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001986C File Offset: 0x00017A6C
		[Parameter(Mandatory = false)]
		public AggregationSubscriptionIdentity[] ExceptIfFromSubscription
		{
			get
			{
				return (AggregationSubscriptionIdentity[])base.Fields[InboxRuleSchema.ExceptIfFromSubscription];
			}
			set
			{
				base.Fields[InboxRuleSchema.ExceptIfFromSubscription] = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001987F File Offset: 0x00017A7F
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00019896 File Offset: 0x00017A96
		[ValidateNotNullOrEmpty]
		[Parameter]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x000198A9 File Offset: 0x00017AA9
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x000198B6 File Offset: 0x00017AB6
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> ApplyCategory
		{
			get
			{
				return this.DataObject.ApplyCategory;
			}
			set
			{
				this.DataObject.ApplyCategory = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x000198C4 File Offset: 0x00017AC4
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x000198DB File Offset: 0x00017ADB
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MailboxFolderIdParameter CopyToFolder
		{
			get
			{
				return (MailboxFolderIdParameter)base.Fields[InboxRuleSchema.CopyToFolder];
			}
			set
			{
				base.Fields[InboxRuleSchema.CopyToFolder] = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000198EE File Offset: 0x00017AEE
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x000198FB File Offset: 0x00017AFB
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool DeleteMessage
		{
			get
			{
				return this.DataObject.DeleteMessage;
			}
			set
			{
				this.DataObject.DeleteMessage = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00019909 File Offset: 0x00017B09
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x00019920 File Offset: 0x00017B20
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RecipientIdParameter[] ForwardAsAttachmentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.ForwardAsAttachmentTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.ForwardAsAttachmentTo] = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00019933 File Offset: 0x00017B33
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0001994A File Offset: 0x00017B4A
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RecipientIdParameter[] ForwardTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.ForwardTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.ForwardTo] = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001995D File Offset: 0x00017B5D
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0001996A File Offset: 0x00017B6A
		[Parameter(ParameterSetName = "Identity")]
		public bool MarkAsRead
		{
			get
			{
				return this.DataObject.MarkAsRead;
			}
			set
			{
				this.DataObject.MarkAsRead = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00019978 File Offset: 0x00017B78
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00019998 File Offset: 0x00017B98
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Microsoft.Exchange.Data.Storage.Importance MarkImportance
		{
			get
			{
				return this.DataObject.MarkImportance.Value;
			}
			set
			{
				this.DataObject.MarkImportance = new Microsoft.Exchange.Data.Storage.Importance?(value);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x000199AB File Offset: 0x00017BAB
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x000199C2 File Offset: 0x00017BC2
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MailboxFolderIdParameter MoveToFolder
		{
			get
			{
				return (MailboxFolderIdParameter)base.Fields[InboxRuleSchema.MoveToFolder];
			}
			set
			{
				base.Fields[InboxRuleSchema.MoveToFolder] = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000199D5 File Offset: 0x00017BD5
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x000199EC File Offset: 0x00017BEC
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RecipientIdParameter[] RedirectTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[InboxRuleSchema.RedirectTo];
			}
			set
			{
				base.Fields[InboxRuleSchema.RedirectTo] = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000199FF File Offset: 0x00017BFF
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00019A0C File Offset: 0x00017C0C
		[Parameter(ParameterSetName = "Identity")]
		public MultiValuedProperty<E164Number> SendTextMessageNotificationTo
		{
			get
			{
				return this.DataObject.SendTextMessageNotificationTo;
			}
			set
			{
				this.DataObject.SendTextMessageNotificationTo = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00019A1A File Offset: 0x00017C1A
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00019A31 File Offset: 0x00017C31
		[Parameter(Mandatory = true, ParameterSetName = "FromMessage")]
		public MailboxStoreObjectIdParameter FromMessageId
		{
			get
			{
				return (MailboxStoreObjectIdParameter)base.Fields["FromMessageId"];
			}
			set
			{
				base.Fields["FromMessageId"] = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00019A44 File Offset: 0x00017C44
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00019A5B File Offset: 0x00017C5B
		[Parameter(Mandatory = true, ParameterSetName = "FromMessage")]
		public SwitchParameter ValidateOnly
		{
			get
			{
				return (SwitchParameter)base.Fields["ValidateOnly"];
			}
			set
			{
				base.Fields["ValidateOnly"] = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00019A73 File Offset: 0x00017C73
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00019A80 File Offset: 0x00017C80
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool StopProcessingRules
		{
			get
			{
				return this.DataObject.StopProcessingRules;
			}
			set
			{
				this.DataObject.StopProcessingRules = value;
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00019A98 File Offset: 0x00017C98
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 760, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\InboxRule\\NewInboxRule.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00019B4A File Offset: 0x00017D4A
		protected override void InternalValidate()
		{
			base.InternalValidate();
			InboxRuleDataProvider.ValidateInboxRuleProperties(this.DataObject, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00019B78 File Offset: 0x00017D78
		protected override void InternalProcessRecord()
		{
			if (this.FromMessageId != null)
			{
				if (!base.HasErrors)
				{
					this.WriteResult(this.DataObject);
				}
			}
			else
			{
				InboxRuleDataProvider inboxRuleDataProvider = (InboxRuleDataProvider)base.DataSession;
				if (this.AlwaysDeleteOutlookRulesBlob.IsPresent)
				{
					inboxRuleDataProvider.SetAlwaysDeleteOutlookRulesBlob(new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
				}
				else if (!inboxRuleDataProvider.IsAlwaysDeleteOutlookRulesBlob())
				{
					if (!inboxRuleDataProvider.HandleOutlookBlob(this.Force, () => base.ShouldContinue(Strings.WarningInboxRuleOutlookBlobExists)))
					{
						return;
					}
				}
				ManageInboxRule.ProcessRecord(new Action(base.InternalProcessRecord), new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError), this.Name);
			}
			if (this.DataObject.SendTextMessageNotificationTo.Count > 0)
			{
				SmsSqmDataPointHelper.AddNotificationConfigDataPoint(SmsSqmSession.Instance, this.adUser.Id, this.adUser.LegacyExchangeDN, SMSNotificationType.Email);
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00019C58 File Offset: 0x00017E58
		protected override IConfigDataProvider CreateSession()
		{
			MailboxIdParameter mailboxIdParameter = null;
			if (this.FromMessageId != null && this.FromMessageId.RawOwner != null)
			{
				mailboxIdParameter = this.FromMessageId.RawOwner;
			}
			if (mailboxIdParameter == null)
			{
				ADObjectId executingUserId;
				base.TryGetExecutingUserId(out executingUserId);
				mailboxIdParameter = (this.Mailbox ?? MailboxTaskHelper.ResolveMailboxIdentity(executingUserId, new Task.ErrorLoggerDelegate(base.WriteError)));
			}
			this.adUser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 867, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\InboxRule\\NewInboxRule.cs");
			base.VerifyIsWithinScopes(TaskHelper.UnderscopeSessionToOrganization(tenantOrRootOrgRecipientSession, this.adUser.OrganizationId, true), this.adUser, true, new DataAccessTask<InboxRule>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			XsoMailboxDataProviderBase xsoMailboxDataProviderBase;
			if (this.FromMessageId != null)
			{
				xsoMailboxDataProviderBase = new MailMessageDataProvider(base.SessionSettings, this.adUser, (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken, "New-InboxRule");
				this.FromMessageId.InternalStoreObjectId = new MailboxStoreObjectId((ADObjectId)this.adUser.Identity, this.FromMessageId.RawStoreObjectId);
			}
			else
			{
				xsoMailboxDataProviderBase = new InboxRuleDataProvider(base.SessionSettings, this.adUser, "New-InboxRule");
			}
			this.mailboxOwner = xsoMailboxDataProviderBase.MailboxSession.MailboxOwner.ObjectId.ToString();
			return xsoMailboxDataProviderBase;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00019DDC File Offset: 0x00017FDC
		protected override IConfigurable PrepareDataObject()
		{
			InboxRule inboxRule = (InboxRule)base.PrepareDataObject();
			inboxRule.Provider = (XsoMailboxDataProviderBase)base.DataSession;
			inboxRule.MailboxOwnerId = this.adUser.Id;
			if (this.FromMessageId != null)
			{
				this.PrepareDataObjectFromMessage(inboxRule);
			}
			else
			{
				this.PrepareDataObjectFromParameters(inboxRule);
				inboxRule.ValidateInterdependentParameters(new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			return inboxRule;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00019E44 File Offset: 0x00018044
		private void PrepareDataObjectFromMessage(InboxRule inboxRule)
		{
			inboxRule.Name = Guid.NewGuid().ToString();
			inboxRule.StopProcessingRules = true;
			MailMessage mailMessage = (MailMessage)base.GetDataObject<MailMessage>(this.FromMessageId, base.DataSession, null, null, new LocalizedString?(LocalizedString.Empty));
			if (!string.IsNullOrEmpty(mailMessage.Subject))
			{
				inboxRule.SubjectContainsWords = new MultiValuedProperty<string>(new string[]
				{
					mailMessage.Subject
				});
			}
			if (mailMessage.From != null || mailMessage.Sender != null)
			{
				inboxRule.From = new ADRecipientOrAddress[]
				{
					mailMessage.From ?? mailMessage.Sender
				};
			}
			if (mailMessage.To != null || mailMessage.Cc != null)
			{
				List<ADRecipientOrAddress> list = new List<ADRecipientOrAddress>(((mailMessage.To == null) ? 0 : mailMessage.To.Length) + ((mailMessage.Cc == null) ? 0 : mailMessage.Cc.Length));
				if (mailMessage.To != null)
				{
					list.AddRange(mailMessage.To);
				}
				if (mailMessage.Cc != null)
				{
					list.AddRange(mailMessage.Cc);
				}
				if (list.Count > 0)
				{
					inboxRule.SentTo = list.Distinct<ADRecipientOrAddress>().ToArray<ADRecipientOrAddress>();
				}
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00019F7C File Offset: 0x0001817C
		private void PrepareDataObjectFromParameters(InboxRule inboxRule)
		{
			if (base.Fields.IsModified(InboxRuleSchema.From))
			{
				inboxRule.From = ManageInboxRule.ResolveRecipients(this.From, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ExceptIfFrom))
			{
				inboxRule.ExceptIfFrom = ManageInboxRule.ResolveRecipients(this.ExceptIfFrom, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.HasClassification))
			{
				inboxRule.HasClassification = ManageInboxRule.ResolveMessageClassifications(this.HasClassification, this.ConfigurationSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ExceptIfHasClassification))
			{
				inboxRule.ExceptIfHasClassification = ManageInboxRule.ResolveMessageClassifications(this.ExceptIfHasClassification, this.ConfigurationSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.SentTo))
			{
				inboxRule.SentTo = ManageInboxRule.ResolveRecipients(this.SentTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ExceptIfSentTo))
			{
				inboxRule.ExceptIfSentTo = ManageInboxRule.ResolveRecipients(this.ExceptIfSentTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.CopyToFolder))
			{
				inboxRule.CopyToFolder = ManageInboxRule.ResolveMailboxFolder(this.CopyToFolder, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<MailboxFolder>), base.TenantGlobalCatalogSession, base.SessionSettings, this.adUser, (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ForwardAsAttachmentTo))
			{
				inboxRule.ForwardAsAttachmentTo = ManageInboxRule.ResolveRecipients(this.ForwardAsAttachmentTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ForwardTo))
			{
				inboxRule.ForwardTo = ManageInboxRule.ResolveRecipients(this.ForwardTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.MoveToFolder))
			{
				inboxRule.MoveToFolder = ManageInboxRule.ResolveMailboxFolder(this.MoveToFolder, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<MailboxFolder>), base.TenantGlobalCatalogSession, base.SessionSettings, this.adUser, (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.RedirectTo))
			{
				inboxRule.RedirectTo = ManageInboxRule.ResolveRecipients(this.RedirectTo, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), base.TenantGlobalCatalogSession, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.FromSubscription))
			{
				inboxRule.FromSubscription = ManageInboxRule.ResolveSubscriptions(this.FromSubscription, this.adUser, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(InboxRuleSchema.ExceptIfFromSubscription))
			{
				inboxRule.ExceptIfFromSubscription = ManageInboxRule.ResolveSubscriptions(this.ExceptIfFromSubscription, this.adUser, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (this.FlaggedForAction != null)
			{
				InboxRuleDataProvider.CheckFlaggedAction(this.FlaggedForAction, InboxRuleSchema.FlaggedForAction.Name, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
			if (this.ExceptIfFlaggedForAction != null)
			{
				InboxRuleDataProvider.CheckFlaggedAction(this.ExceptIfFlaggedForAction, InboxRuleSchema.ExceptIfFlaggedForAction.Name, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001A354 File Offset: 0x00018554
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001A367 File Offset: 0x00018567
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewInboxRule(this.Name, this.mailboxOwner);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001A37A File Offset: 0x0001857A
		protected override void InternalStateReset()
		{
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001A38D File Offset: 0x0001858D
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			ManageInboxRule.CleanupInboxRuleDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400017D RID: 381
		private string mailboxOwner;

		// Token: 0x0400017E RID: 382
		private ADUser adUser;
	}
}
