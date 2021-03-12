using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B3 RID: 435
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class RulePredicatesType
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00024A2A File Offset: 0x00022C2A
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x00024A32 File Offset: 0x00022C32
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Categories
		{
			get
			{
				return this.categoriesField;
			}
			set
			{
				this.categoriesField = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00024A3B File Offset: 0x00022C3B
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x00024A43 File Offset: 0x00022C43
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsBodyStrings
		{
			get
			{
				return this.containsBodyStringsField;
			}
			set
			{
				this.containsBodyStringsField = value;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x00024A4C File Offset: 0x00022C4C
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x00024A54 File Offset: 0x00022C54
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsHeaderStrings
		{
			get
			{
				return this.containsHeaderStringsField;
			}
			set
			{
				this.containsHeaderStringsField = value;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x00024A5D File Offset: 0x00022C5D
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x00024A65 File Offset: 0x00022C65
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsRecipientStrings
		{
			get
			{
				return this.containsRecipientStringsField;
			}
			set
			{
				this.containsRecipientStringsField = value;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00024A6E File Offset: 0x00022C6E
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x00024A76 File Offset: 0x00022C76
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsSenderStrings
		{
			get
			{
				return this.containsSenderStringsField;
			}
			set
			{
				this.containsSenderStringsField = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x00024A7F File Offset: 0x00022C7F
		// (set) Token: 0x06001255 RID: 4693 RVA: 0x00024A87 File Offset: 0x00022C87
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsSubjectOrBodyStrings
		{
			get
			{
				return this.containsSubjectOrBodyStringsField;
			}
			set
			{
				this.containsSubjectOrBodyStringsField = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00024A90 File Offset: 0x00022C90
		// (set) Token: 0x06001257 RID: 4695 RVA: 0x00024A98 File Offset: 0x00022C98
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsSubjectStrings
		{
			get
			{
				return this.containsSubjectStringsField;
			}
			set
			{
				this.containsSubjectStringsField = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00024AA1 File Offset: 0x00022CA1
		// (set) Token: 0x06001259 RID: 4697 RVA: 0x00024AA9 File Offset: 0x00022CA9
		public FlaggedForActionType FlaggedForAction
		{
			get
			{
				return this.flaggedForActionField;
			}
			set
			{
				this.flaggedForActionField = value;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00024AB2 File Offset: 0x00022CB2
		// (set) Token: 0x0600125B RID: 4699 RVA: 0x00024ABA File Offset: 0x00022CBA
		[XmlIgnore]
		public bool FlaggedForActionSpecified
		{
			get
			{
				return this.flaggedForActionFieldSpecified;
			}
			set
			{
				this.flaggedForActionFieldSpecified = value;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00024AC3 File Offset: 0x00022CC3
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x00024ACB File Offset: 0x00022CCB
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] FromAddresses
		{
			get
			{
				return this.fromAddressesField;
			}
			set
			{
				this.fromAddressesField = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00024AD4 File Offset: 0x00022CD4
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x00024ADC File Offset: 0x00022CDC
		[XmlArrayItem("String", IsNullable = false)]
		public string[] FromConnectedAccounts
		{
			get
			{
				return this.fromConnectedAccountsField;
			}
			set
			{
				this.fromConnectedAccountsField = value;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00024AE5 File Offset: 0x00022CE5
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x00024AED File Offset: 0x00022CED
		public bool HasAttachments
		{
			get
			{
				return this.hasAttachmentsField;
			}
			set
			{
				this.hasAttachmentsField = value;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00024AF6 File Offset: 0x00022CF6
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x00024AFE File Offset: 0x00022CFE
		[XmlIgnore]
		public bool HasAttachmentsSpecified
		{
			get
			{
				return this.hasAttachmentsFieldSpecified;
			}
			set
			{
				this.hasAttachmentsFieldSpecified = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00024B07 File Offset: 0x00022D07
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x00024B0F File Offset: 0x00022D0F
		public ImportanceChoicesType Importance
		{
			get
			{
				return this.importanceField;
			}
			set
			{
				this.importanceField = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00024B18 File Offset: 0x00022D18
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x00024B20 File Offset: 0x00022D20
		[XmlIgnore]
		public bool ImportanceSpecified
		{
			get
			{
				return this.importanceFieldSpecified;
			}
			set
			{
				this.importanceFieldSpecified = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00024B29 File Offset: 0x00022D29
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x00024B31 File Offset: 0x00022D31
		public bool IsApprovalRequest
		{
			get
			{
				return this.isApprovalRequestField;
			}
			set
			{
				this.isApprovalRequestField = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00024B3A File Offset: 0x00022D3A
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00024B42 File Offset: 0x00022D42
		[XmlIgnore]
		public bool IsApprovalRequestSpecified
		{
			get
			{
				return this.isApprovalRequestFieldSpecified;
			}
			set
			{
				this.isApprovalRequestFieldSpecified = value;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x00024B4B File Offset: 0x00022D4B
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x00024B53 File Offset: 0x00022D53
		public bool IsAutomaticForward
		{
			get
			{
				return this.isAutomaticForwardField;
			}
			set
			{
				this.isAutomaticForwardField = value;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00024B5C File Offset: 0x00022D5C
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x00024B64 File Offset: 0x00022D64
		[XmlIgnore]
		public bool IsAutomaticForwardSpecified
		{
			get
			{
				return this.isAutomaticForwardFieldSpecified;
			}
			set
			{
				this.isAutomaticForwardFieldSpecified = value;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00024B6D File Offset: 0x00022D6D
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x00024B75 File Offset: 0x00022D75
		public bool IsAutomaticReply
		{
			get
			{
				return this.isAutomaticReplyField;
			}
			set
			{
				this.isAutomaticReplyField = value;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00024B7E File Offset: 0x00022D7E
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00024B86 File Offset: 0x00022D86
		[XmlIgnore]
		public bool IsAutomaticReplySpecified
		{
			get
			{
				return this.isAutomaticReplyFieldSpecified;
			}
			set
			{
				this.isAutomaticReplyFieldSpecified = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00024B8F File Offset: 0x00022D8F
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00024B97 File Offset: 0x00022D97
		public bool IsEncrypted
		{
			get
			{
				return this.isEncryptedField;
			}
			set
			{
				this.isEncryptedField = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00024BA0 File Offset: 0x00022DA0
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x00024BA8 File Offset: 0x00022DA8
		[XmlIgnore]
		public bool IsEncryptedSpecified
		{
			get
			{
				return this.isEncryptedFieldSpecified;
			}
			set
			{
				this.isEncryptedFieldSpecified = value;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00024BB1 File Offset: 0x00022DB1
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00024BB9 File Offset: 0x00022DB9
		public bool IsMeetingRequest
		{
			get
			{
				return this.isMeetingRequestField;
			}
			set
			{
				this.isMeetingRequestField = value;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00024BC2 File Offset: 0x00022DC2
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x00024BCA File Offset: 0x00022DCA
		[XmlIgnore]
		public bool IsMeetingRequestSpecified
		{
			get
			{
				return this.isMeetingRequestFieldSpecified;
			}
			set
			{
				this.isMeetingRequestFieldSpecified = value;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00024BD3 File Offset: 0x00022DD3
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x00024BDB File Offset: 0x00022DDB
		public bool IsMeetingResponse
		{
			get
			{
				return this.isMeetingResponseField;
			}
			set
			{
				this.isMeetingResponseField = value;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00024BE4 File Offset: 0x00022DE4
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x00024BEC File Offset: 0x00022DEC
		[XmlIgnore]
		public bool IsMeetingResponseSpecified
		{
			get
			{
				return this.isMeetingResponseFieldSpecified;
			}
			set
			{
				this.isMeetingResponseFieldSpecified = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00024BF5 File Offset: 0x00022DF5
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x00024BFD File Offset: 0x00022DFD
		public bool IsNDR
		{
			get
			{
				return this.isNDRField;
			}
			set
			{
				this.isNDRField = value;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00024C06 File Offset: 0x00022E06
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00024C0E File Offset: 0x00022E0E
		[XmlIgnore]
		public bool IsNDRSpecified
		{
			get
			{
				return this.isNDRFieldSpecified;
			}
			set
			{
				this.isNDRFieldSpecified = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00024C17 File Offset: 0x00022E17
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00024C1F File Offset: 0x00022E1F
		public bool IsPermissionControlled
		{
			get
			{
				return this.isPermissionControlledField;
			}
			set
			{
				this.isPermissionControlledField = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00024C28 File Offset: 0x00022E28
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x00024C30 File Offset: 0x00022E30
		[XmlIgnore]
		public bool IsPermissionControlledSpecified
		{
			get
			{
				return this.isPermissionControlledFieldSpecified;
			}
			set
			{
				this.isPermissionControlledFieldSpecified = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00024C39 File Offset: 0x00022E39
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x00024C41 File Offset: 0x00022E41
		public bool IsReadReceipt
		{
			get
			{
				return this.isReadReceiptField;
			}
			set
			{
				this.isReadReceiptField = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00024C4A File Offset: 0x00022E4A
		// (set) Token: 0x0600128B RID: 4747 RVA: 0x00024C52 File Offset: 0x00022E52
		[XmlIgnore]
		public bool IsReadReceiptSpecified
		{
			get
			{
				return this.isReadReceiptFieldSpecified;
			}
			set
			{
				this.isReadReceiptFieldSpecified = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00024C5B File Offset: 0x00022E5B
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x00024C63 File Offset: 0x00022E63
		public bool IsSigned
		{
			get
			{
				return this.isSignedField;
			}
			set
			{
				this.isSignedField = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x00024C6C File Offset: 0x00022E6C
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x00024C74 File Offset: 0x00022E74
		[XmlIgnore]
		public bool IsSignedSpecified
		{
			get
			{
				return this.isSignedFieldSpecified;
			}
			set
			{
				this.isSignedFieldSpecified = value;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00024C7D File Offset: 0x00022E7D
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x00024C85 File Offset: 0x00022E85
		public bool IsVoicemail
		{
			get
			{
				return this.isVoicemailField;
			}
			set
			{
				this.isVoicemailField = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00024C8E File Offset: 0x00022E8E
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x00024C96 File Offset: 0x00022E96
		[XmlIgnore]
		public bool IsVoicemailSpecified
		{
			get
			{
				return this.isVoicemailFieldSpecified;
			}
			set
			{
				this.isVoicemailFieldSpecified = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00024C9F File Offset: 0x00022E9F
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00024CA7 File Offset: 0x00022EA7
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ItemClasses
		{
			get
			{
				return this.itemClassesField;
			}
			set
			{
				this.itemClassesField = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00024CB0 File Offset: 0x00022EB0
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x00024CB8 File Offset: 0x00022EB8
		[XmlArrayItem("String", IsNullable = false)]
		public string[] MessageClassifications
		{
			get
			{
				return this.messageClassificationsField;
			}
			set
			{
				this.messageClassificationsField = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00024CC1 File Offset: 0x00022EC1
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x00024CC9 File Offset: 0x00022EC9
		public bool NotSentToMe
		{
			get
			{
				return this.notSentToMeField;
			}
			set
			{
				this.notSentToMeField = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00024CD2 File Offset: 0x00022ED2
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x00024CDA File Offset: 0x00022EDA
		[XmlIgnore]
		public bool NotSentToMeSpecified
		{
			get
			{
				return this.notSentToMeFieldSpecified;
			}
			set
			{
				this.notSentToMeFieldSpecified = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00024CE3 File Offset: 0x00022EE3
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x00024CEB File Offset: 0x00022EEB
		public bool SentCcMe
		{
			get
			{
				return this.sentCcMeField;
			}
			set
			{
				this.sentCcMeField = value;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00024CF4 File Offset: 0x00022EF4
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x00024CFC File Offset: 0x00022EFC
		[XmlIgnore]
		public bool SentCcMeSpecified
		{
			get
			{
				return this.sentCcMeFieldSpecified;
			}
			set
			{
				this.sentCcMeFieldSpecified = value;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00024D05 File Offset: 0x00022F05
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x00024D0D File Offset: 0x00022F0D
		public bool SentOnlyToMe
		{
			get
			{
				return this.sentOnlyToMeField;
			}
			set
			{
				this.sentOnlyToMeField = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x00024D16 File Offset: 0x00022F16
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x00024D1E File Offset: 0x00022F1E
		[XmlIgnore]
		public bool SentOnlyToMeSpecified
		{
			get
			{
				return this.sentOnlyToMeFieldSpecified;
			}
			set
			{
				this.sentOnlyToMeFieldSpecified = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00024D27 File Offset: 0x00022F27
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x00024D2F File Offset: 0x00022F2F
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] SentToAddresses
		{
			get
			{
				return this.sentToAddressesField;
			}
			set
			{
				this.sentToAddressesField = value;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00024D38 File Offset: 0x00022F38
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x00024D40 File Offset: 0x00022F40
		public bool SentToMe
		{
			get
			{
				return this.sentToMeField;
			}
			set
			{
				this.sentToMeField = value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00024D49 File Offset: 0x00022F49
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x00024D51 File Offset: 0x00022F51
		[XmlIgnore]
		public bool SentToMeSpecified
		{
			get
			{
				return this.sentToMeFieldSpecified;
			}
			set
			{
				this.sentToMeFieldSpecified = value;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00024D5A File Offset: 0x00022F5A
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x00024D62 File Offset: 0x00022F62
		public bool SentToOrCcMe
		{
			get
			{
				return this.sentToOrCcMeField;
			}
			set
			{
				this.sentToOrCcMeField = value;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x00024D6B File Offset: 0x00022F6B
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x00024D73 File Offset: 0x00022F73
		[XmlIgnore]
		public bool SentToOrCcMeSpecified
		{
			get
			{
				return this.sentToOrCcMeFieldSpecified;
			}
			set
			{
				this.sentToOrCcMeFieldSpecified = value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00024D7C File Offset: 0x00022F7C
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x00024D84 File Offset: 0x00022F84
		public SensitivityChoicesType Sensitivity
		{
			get
			{
				return this.sensitivityField;
			}
			set
			{
				this.sensitivityField = value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00024D8D File Offset: 0x00022F8D
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x00024D95 File Offset: 0x00022F95
		[XmlIgnore]
		public bool SensitivitySpecified
		{
			get
			{
				return this.sensitivityFieldSpecified;
			}
			set
			{
				this.sensitivityFieldSpecified = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00024D9E File Offset: 0x00022F9E
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x00024DA6 File Offset: 0x00022FA6
		public RulePredicateDateRangeType WithinDateRange
		{
			get
			{
				return this.withinDateRangeField;
			}
			set
			{
				this.withinDateRangeField = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00024DAF File Offset: 0x00022FAF
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00024DB7 File Offset: 0x00022FB7
		public RulePredicateSizeRangeType WithinSizeRange
		{
			get
			{
				return this.withinSizeRangeField;
			}
			set
			{
				this.withinSizeRangeField = value;
			}
		}

		// Token: 0x04000CDD RID: 3293
		private string[] categoriesField;

		// Token: 0x04000CDE RID: 3294
		private string[] containsBodyStringsField;

		// Token: 0x04000CDF RID: 3295
		private string[] containsHeaderStringsField;

		// Token: 0x04000CE0 RID: 3296
		private string[] containsRecipientStringsField;

		// Token: 0x04000CE1 RID: 3297
		private string[] containsSenderStringsField;

		// Token: 0x04000CE2 RID: 3298
		private string[] containsSubjectOrBodyStringsField;

		// Token: 0x04000CE3 RID: 3299
		private string[] containsSubjectStringsField;

		// Token: 0x04000CE4 RID: 3300
		private FlaggedForActionType flaggedForActionField;

		// Token: 0x04000CE5 RID: 3301
		private bool flaggedForActionFieldSpecified;

		// Token: 0x04000CE6 RID: 3302
		private EmailAddressType[] fromAddressesField;

		// Token: 0x04000CE7 RID: 3303
		private string[] fromConnectedAccountsField;

		// Token: 0x04000CE8 RID: 3304
		private bool hasAttachmentsField;

		// Token: 0x04000CE9 RID: 3305
		private bool hasAttachmentsFieldSpecified;

		// Token: 0x04000CEA RID: 3306
		private ImportanceChoicesType importanceField;

		// Token: 0x04000CEB RID: 3307
		private bool importanceFieldSpecified;

		// Token: 0x04000CEC RID: 3308
		private bool isApprovalRequestField;

		// Token: 0x04000CED RID: 3309
		private bool isApprovalRequestFieldSpecified;

		// Token: 0x04000CEE RID: 3310
		private bool isAutomaticForwardField;

		// Token: 0x04000CEF RID: 3311
		private bool isAutomaticForwardFieldSpecified;

		// Token: 0x04000CF0 RID: 3312
		private bool isAutomaticReplyField;

		// Token: 0x04000CF1 RID: 3313
		private bool isAutomaticReplyFieldSpecified;

		// Token: 0x04000CF2 RID: 3314
		private bool isEncryptedField;

		// Token: 0x04000CF3 RID: 3315
		private bool isEncryptedFieldSpecified;

		// Token: 0x04000CF4 RID: 3316
		private bool isMeetingRequestField;

		// Token: 0x04000CF5 RID: 3317
		private bool isMeetingRequestFieldSpecified;

		// Token: 0x04000CF6 RID: 3318
		private bool isMeetingResponseField;

		// Token: 0x04000CF7 RID: 3319
		private bool isMeetingResponseFieldSpecified;

		// Token: 0x04000CF8 RID: 3320
		private bool isNDRField;

		// Token: 0x04000CF9 RID: 3321
		private bool isNDRFieldSpecified;

		// Token: 0x04000CFA RID: 3322
		private bool isPermissionControlledField;

		// Token: 0x04000CFB RID: 3323
		private bool isPermissionControlledFieldSpecified;

		// Token: 0x04000CFC RID: 3324
		private bool isReadReceiptField;

		// Token: 0x04000CFD RID: 3325
		private bool isReadReceiptFieldSpecified;

		// Token: 0x04000CFE RID: 3326
		private bool isSignedField;

		// Token: 0x04000CFF RID: 3327
		private bool isSignedFieldSpecified;

		// Token: 0x04000D00 RID: 3328
		private bool isVoicemailField;

		// Token: 0x04000D01 RID: 3329
		private bool isVoicemailFieldSpecified;

		// Token: 0x04000D02 RID: 3330
		private string[] itemClassesField;

		// Token: 0x04000D03 RID: 3331
		private string[] messageClassificationsField;

		// Token: 0x04000D04 RID: 3332
		private bool notSentToMeField;

		// Token: 0x04000D05 RID: 3333
		private bool notSentToMeFieldSpecified;

		// Token: 0x04000D06 RID: 3334
		private bool sentCcMeField;

		// Token: 0x04000D07 RID: 3335
		private bool sentCcMeFieldSpecified;

		// Token: 0x04000D08 RID: 3336
		private bool sentOnlyToMeField;

		// Token: 0x04000D09 RID: 3337
		private bool sentOnlyToMeFieldSpecified;

		// Token: 0x04000D0A RID: 3338
		private EmailAddressType[] sentToAddressesField;

		// Token: 0x04000D0B RID: 3339
		private bool sentToMeField;

		// Token: 0x04000D0C RID: 3340
		private bool sentToMeFieldSpecified;

		// Token: 0x04000D0D RID: 3341
		private bool sentToOrCcMeField;

		// Token: 0x04000D0E RID: 3342
		private bool sentToOrCcMeFieldSpecified;

		// Token: 0x04000D0F RID: 3343
		private SensitivityChoicesType sensitivityField;

		// Token: 0x04000D10 RID: 3344
		private bool sensitivityFieldSpecified;

		// Token: 0x04000D11 RID: 3345
		private RulePredicateDateRangeType withinDateRangeField;

		// Token: 0x04000D12 RID: 3346
		private RulePredicateSizeRangeType withinSizeRangeField;
	}
}
