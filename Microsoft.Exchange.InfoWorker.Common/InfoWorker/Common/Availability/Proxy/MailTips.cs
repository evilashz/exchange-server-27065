using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200012F RID: 303
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class MailTips
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00024F1A File Offset: 0x0002311A
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x00024F22 File Offset: 0x00023122
		public EmailAddressType RecipientAddress
		{
			get
			{
				return this.recipientAddressField;
			}
			set
			{
				this.recipientAddressField = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00024F2B File Offset: 0x0002312B
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x00024F33 File Offset: 0x00023133
		public MailTipTypes PendingMailTips
		{
			get
			{
				return this.pendingMailTipsField;
			}
			set
			{
				this.pendingMailTipsField = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00024F3C File Offset: 0x0002313C
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x00024F44 File Offset: 0x00023144
		public OutOfOfficeMailTip OutOfOffice
		{
			get
			{
				return this.outOfOfficeField;
			}
			set
			{
				this.outOfOfficeField = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00024F4D File Offset: 0x0002314D
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x00024F55 File Offset: 0x00023155
		public string OutOfOfficeMessage
		{
			get
			{
				return this.outOfOfficeMessageField;
			}
			set
			{
				this.outOfOfficeMessageField = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00024F5E File Offset: 0x0002315E
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x00024F66 File Offset: 0x00023166
		public bool MailboxFull
		{
			get
			{
				return this.mailboxFullField;
			}
			set
			{
				this.mailboxFullField = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00024F6F File Offset: 0x0002316F
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x00024F77 File Offset: 0x00023177
		[XmlIgnore]
		public bool MailboxFullSpecified
		{
			get
			{
				return this.mailboxFullFieldSpecified;
			}
			set
			{
				this.mailboxFullFieldSpecified = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00024F80 File Offset: 0x00023180
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x00024F88 File Offset: 0x00023188
		public string CustomMailTip
		{
			get
			{
				return this.customMailTipField;
			}
			set
			{
				this.customMailTipField = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00024F91 File Offset: 0x00023191
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x00024F99 File Offset: 0x00023199
		public int TotalMemberCount
		{
			get
			{
				return this.totalMemberCountField;
			}
			set
			{
				this.totalMemberCountField = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00024FA2 File Offset: 0x000231A2
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x00024FAA File Offset: 0x000231AA
		[XmlIgnore]
		public bool TotalMemberCountSpecified
		{
			get
			{
				return this.totalMemberCountFieldSpecified;
			}
			set
			{
				this.totalMemberCountFieldSpecified = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00024FB3 File Offset: 0x000231B3
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x00024FBB File Offset: 0x000231BB
		public int ExternalMemberCount
		{
			get
			{
				return this.externalMemberCountField;
			}
			set
			{
				this.externalMemberCountField = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00024FC4 File Offset: 0x000231C4
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x00024FCC File Offset: 0x000231CC
		[XmlIgnore]
		public bool ExternalMemberCountSpecified
		{
			get
			{
				return this.externalMemberCountFieldSpecified;
			}
			set
			{
				this.externalMemberCountFieldSpecified = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00024FD5 File Offset: 0x000231D5
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00024FDD File Offset: 0x000231DD
		public int MaxMessageSize
		{
			get
			{
				return this.maxMessageSizeField;
			}
			set
			{
				this.maxMessageSizeField = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00024FE6 File Offset: 0x000231E6
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00024FEE File Offset: 0x000231EE
		[XmlIgnore]
		public bool MaxMessageSizeSpecified
		{
			get
			{
				return this.maxMessageSizeFieldSpecified;
			}
			set
			{
				this.maxMessageSizeFieldSpecified = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00024FF7 File Offset: 0x000231F7
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00024FFF File Offset: 0x000231FF
		public bool DeliveryRestricted
		{
			get
			{
				return this.deliveryRestrictedField;
			}
			set
			{
				this.deliveryRestrictedField = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00025008 File Offset: 0x00023208
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00025010 File Offset: 0x00023210
		[XmlIgnore]
		public bool DeliveryRestrictedSpecified
		{
			get
			{
				return this.deliveryRestrictedFieldSpecified;
			}
			set
			{
				this.deliveryRestrictedFieldSpecified = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00025019 File Offset: 0x00023219
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x00025021 File Offset: 0x00023221
		public bool IsModerated
		{
			get
			{
				return this.isModeratedField;
			}
			set
			{
				this.isModeratedField = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0002502A File Offset: 0x0002322A
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x00025032 File Offset: 0x00023232
		[XmlIgnore]
		public bool IsModeratedSpecified
		{
			get
			{
				return this.isModeratedFieldSpecified;
			}
			set
			{
				this.isModeratedFieldSpecified = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0002503B File Offset: 0x0002323B
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00025043 File Offset: 0x00023243
		public bool InvalidRecipient
		{
			get
			{
				return this.invalidRecipientField;
			}
			set
			{
				this.invalidRecipientField = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0002504C File Offset: 0x0002324C
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00025054 File Offset: 0x00023254
		[XmlIgnore]
		public bool InvalidRecipientSpecified
		{
			get
			{
				return this.invalidRecipientFieldSpecified;
			}
			set
			{
				this.invalidRecipientFieldSpecified = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0002505D File Offset: 0x0002325D
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x00025065 File Offset: 0x00023265
		public int Scope
		{
			get
			{
				return this.scopeField;
			}
			set
			{
				this.scopeField = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0002506E File Offset: 0x0002326E
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00025076 File Offset: 0x00023276
		[XmlIgnore]
		public bool ScopeSpecified
		{
			get
			{
				return this.scopeFieldSpecified;
			}
			set
			{
				this.scopeFieldSpecified = value;
			}
		}

		// Token: 0x0400065E RID: 1630
		private EmailAddressType recipientAddressField;

		// Token: 0x0400065F RID: 1631
		private MailTipTypes pendingMailTipsField;

		// Token: 0x04000660 RID: 1632
		private OutOfOfficeMailTip outOfOfficeField;

		// Token: 0x04000661 RID: 1633
		private string outOfOfficeMessageField;

		// Token: 0x04000662 RID: 1634
		private bool mailboxFullField;

		// Token: 0x04000663 RID: 1635
		private bool mailboxFullFieldSpecified;

		// Token: 0x04000664 RID: 1636
		private string customMailTipField;

		// Token: 0x04000665 RID: 1637
		private int totalMemberCountField;

		// Token: 0x04000666 RID: 1638
		private bool totalMemberCountFieldSpecified;

		// Token: 0x04000667 RID: 1639
		private int externalMemberCountField;

		// Token: 0x04000668 RID: 1640
		private bool externalMemberCountFieldSpecified;

		// Token: 0x04000669 RID: 1641
		private int maxMessageSizeField;

		// Token: 0x0400066A RID: 1642
		private bool maxMessageSizeFieldSpecified;

		// Token: 0x0400066B RID: 1643
		private bool deliveryRestrictedField;

		// Token: 0x0400066C RID: 1644
		private bool deliveryRestrictedFieldSpecified;

		// Token: 0x0400066D RID: 1645
		private bool isModeratedField;

		// Token: 0x0400066E RID: 1646
		private bool isModeratedFieldSpecified;

		// Token: 0x0400066F RID: 1647
		private bool invalidRecipientField;

		// Token: 0x04000670 RID: 1648
		private bool invalidRecipientFieldSpecified;

		// Token: 0x04000671 RID: 1649
		private int scopeField;

		// Token: 0x04000672 RID: 1650
		private bool scopeFieldSpecified;
	}
}
