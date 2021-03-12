using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000248 RID: 584
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class MailTips
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00026834 File Offset: 0x00024A34
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x0002683C File Offset: 0x00024A3C
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

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00026845 File Offset: 0x00024A45
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x0002684D File Offset: 0x00024A4D
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

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00026856 File Offset: 0x00024A56
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x0002685E File Offset: 0x00024A5E
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

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00026867 File Offset: 0x00024A67
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x0002686F File Offset: 0x00024A6F
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

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00026878 File Offset: 0x00024A78
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x00026880 File Offset: 0x00024A80
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

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00026889 File Offset: 0x00024A89
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x00026891 File Offset: 0x00024A91
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

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0002689A File Offset: 0x00024A9A
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x000268A2 File Offset: 0x00024AA2
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

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x000268AB File Offset: 0x00024AAB
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x000268B3 File Offset: 0x00024AB3
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

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x000268BC File Offset: 0x00024ABC
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x000268C4 File Offset: 0x00024AC4
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

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x000268CD File Offset: 0x00024ACD
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x000268D5 File Offset: 0x00024AD5
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x000268DE File Offset: 0x00024ADE
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x000268E6 File Offset: 0x00024AE6
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

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000268EF File Offset: 0x00024AEF
		// (set) Token: 0x060015F1 RID: 5617 RVA: 0x000268F7 File Offset: 0x00024AF7
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

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00026900 File Offset: 0x00024B00
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x00026908 File Offset: 0x00024B08
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

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00026911 File Offset: 0x00024B11
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00026919 File Offset: 0x00024B19
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

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00026922 File Offset: 0x00024B22
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x0002692A File Offset: 0x00024B2A
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

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00026933 File Offset: 0x00024B33
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0002693B File Offset: 0x00024B3B
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

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00026944 File Offset: 0x00024B44
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x0002694C File Offset: 0x00024B4C
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

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00026955 File Offset: 0x00024B55
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x0002695D File Offset: 0x00024B5D
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

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00026966 File Offset: 0x00024B66
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x0002696E File Offset: 0x00024B6E
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

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00026977 File Offset: 0x00024B77
		// (set) Token: 0x06001601 RID: 5633 RVA: 0x0002697F File Offset: 0x00024B7F
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

		// Token: 0x04000F04 RID: 3844
		private EmailAddressType recipientAddressField;

		// Token: 0x04000F05 RID: 3845
		private MailTipTypes pendingMailTipsField;

		// Token: 0x04000F06 RID: 3846
		private OutOfOfficeMailTip outOfOfficeField;

		// Token: 0x04000F07 RID: 3847
		private bool mailboxFullField;

		// Token: 0x04000F08 RID: 3848
		private bool mailboxFullFieldSpecified;

		// Token: 0x04000F09 RID: 3849
		private string customMailTipField;

		// Token: 0x04000F0A RID: 3850
		private int totalMemberCountField;

		// Token: 0x04000F0B RID: 3851
		private bool totalMemberCountFieldSpecified;

		// Token: 0x04000F0C RID: 3852
		private int externalMemberCountField;

		// Token: 0x04000F0D RID: 3853
		private bool externalMemberCountFieldSpecified;

		// Token: 0x04000F0E RID: 3854
		private int maxMessageSizeField;

		// Token: 0x04000F0F RID: 3855
		private bool maxMessageSizeFieldSpecified;

		// Token: 0x04000F10 RID: 3856
		private bool deliveryRestrictedField;

		// Token: 0x04000F11 RID: 3857
		private bool deliveryRestrictedFieldSpecified;

		// Token: 0x04000F12 RID: 3858
		private bool isModeratedField;

		// Token: 0x04000F13 RID: 3859
		private bool isModeratedFieldSpecified;

		// Token: 0x04000F14 RID: 3860
		private bool invalidRecipientField;

		// Token: 0x04000F15 RID: 3861
		private bool invalidRecipientFieldSpecified;

		// Token: 0x04000F16 RID: 3862
		private int scopeField;

		// Token: 0x04000F17 RID: 3863
		private bool scopeFieldSpecified;
	}
}
