using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008D9 RID: 2265
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public class PropertiesType
	{
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x00072F62 File Offset: 0x00071162
		// (set) Token: 0x060030A7 RID: 12455 RVA: 0x00072F6A File Offset: 0x0007116A
		public AccountStatusType AccountStatus
		{
			get
			{
				return this.accountStatusField;
			}
			set
			{
				this.accountStatusField = value;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x00072F73 File Offset: 0x00071173
		// (set) Token: 0x060030A9 RID: 12457 RVA: 0x00072F7B File Offset: 0x0007117B
		public ParentalControlStatusType ParentalControlStatus
		{
			get
			{
				return this.parentalControlStatusField;
			}
			set
			{
				this.parentalControlStatusField = value;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x00072F84 File Offset: 0x00071184
		// (set) Token: 0x060030AB RID: 12459 RVA: 0x00072F8C File Offset: 0x0007118C
		public long MailBoxSize
		{
			get
			{
				return this.mailBoxSizeField;
			}
			set
			{
				this.mailBoxSizeField = value;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x00072F95 File Offset: 0x00071195
		// (set) Token: 0x060030AD RID: 12461 RVA: 0x00072F9D File Offset: 0x0007119D
		public long MaxMailBoxSize
		{
			get
			{
				return this.maxMailBoxSizeField;
			}
			set
			{
				this.maxMailBoxSizeField = value;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x00072FA6 File Offset: 0x000711A6
		// (set) Token: 0x060030AF RID: 12463 RVA: 0x00072FAE File Offset: 0x000711AE
		public int MaxAttachments
		{
			get
			{
				return this.maxAttachmentsField;
			}
			set
			{
				this.maxAttachmentsField = value;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x00072FB7 File Offset: 0x000711B7
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x00072FBF File Offset: 0x000711BF
		public long MaxMessageSize
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

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x00072FC8 File Offset: 0x000711C8
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x00072FD0 File Offset: 0x000711D0
		public long MaxUnencodedMessageSize
		{
			get
			{
				return this.maxUnencodedMessageSizeField;
			}
			set
			{
				this.maxUnencodedMessageSizeField = value;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x00072FD9 File Offset: 0x000711D9
		// (set) Token: 0x060030B5 RID: 12469 RVA: 0x00072FE1 File Offset: 0x000711E1
		public int MaxFilters
		{
			get
			{
				return this.maxFiltersField;
			}
			set
			{
				this.maxFiltersField = value;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x00072FEA File Offset: 0x000711EA
		// (set) Token: 0x060030B7 RID: 12471 RVA: 0x00072FF2 File Offset: 0x000711F2
		public int MaxFilterClauseValueLength
		{
			get
			{
				return this.maxFilterClauseValueLengthField;
			}
			set
			{
				this.maxFilterClauseValueLengthField = value;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x00072FFB File Offset: 0x000711FB
		// (set) Token: 0x060030B9 RID: 12473 RVA: 0x00073003 File Offset: 0x00071203
		public int MaxRecipients
		{
			get
			{
				return this.maxRecipientsField;
			}
			set
			{
				this.maxRecipientsField = value;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x0007300C File Offset: 0x0007120C
		// (set) Token: 0x060030BB RID: 12475 RVA: 0x00073014 File Offset: 0x00071214
		public int MaxUserSignatureLength
		{
			get
			{
				return this.maxUserSignatureLengthField;
			}
			set
			{
				this.maxUserSignatureLengthField = value;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x0007301D File Offset: 0x0007121D
		// (set) Token: 0x060030BD RID: 12477 RVA: 0x00073025 File Offset: 0x00071225
		public int BlockListAddressMax
		{
			get
			{
				return this.blockListAddressMaxField;
			}
			set
			{
				this.blockListAddressMaxField = value;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x0007302E File Offset: 0x0007122E
		// (set) Token: 0x060030BF RID: 12479 RVA: 0x00073036 File Offset: 0x00071236
		public int BlockListDomainMax
		{
			get
			{
				return this.blockListDomainMaxField;
			}
			set
			{
				this.blockListDomainMaxField = value;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x0007303F File Offset: 0x0007123F
		// (set) Token: 0x060030C1 RID: 12481 RVA: 0x00073047 File Offset: 0x00071247
		public int WhiteListAddressMax
		{
			get
			{
				return this.whiteListAddressMaxField;
			}
			set
			{
				this.whiteListAddressMaxField = value;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x00073050 File Offset: 0x00071250
		// (set) Token: 0x060030C3 RID: 12483 RVA: 0x00073058 File Offset: 0x00071258
		public int WhiteListDomainMax
		{
			get
			{
				return this.whiteListDomainMaxField;
			}
			set
			{
				this.whiteListDomainMaxField = value;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x00073061 File Offset: 0x00071261
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x00073069 File Offset: 0x00071269
		public int WhiteToListMax
		{
			get
			{
				return this.whiteToListMaxField;
			}
			set
			{
				this.whiteToListMaxField = value;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x00073072 File Offset: 0x00071272
		// (set) Token: 0x060030C7 RID: 12487 RVA: 0x0007307A File Offset: 0x0007127A
		public int AlternateFromListMax
		{
			get
			{
				return this.alternateFromListMaxField;
			}
			set
			{
				this.alternateFromListMaxField = value;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x00073083 File Offset: 0x00071283
		// (set) Token: 0x060030C9 RID: 12489 RVA: 0x0007308B File Offset: 0x0007128B
		public int MaxDailySendMessages
		{
			get
			{
				return this.maxDailySendMessagesField;
			}
			set
			{
				this.maxDailySendMessagesField = value;
			}
		}

		// Token: 0x04002A05 RID: 10757
		private AccountStatusType accountStatusField;

		// Token: 0x04002A06 RID: 10758
		private ParentalControlStatusType parentalControlStatusField;

		// Token: 0x04002A07 RID: 10759
		private long mailBoxSizeField;

		// Token: 0x04002A08 RID: 10760
		private long maxMailBoxSizeField;

		// Token: 0x04002A09 RID: 10761
		private int maxAttachmentsField;

		// Token: 0x04002A0A RID: 10762
		private long maxMessageSizeField;

		// Token: 0x04002A0B RID: 10763
		private long maxUnencodedMessageSizeField;

		// Token: 0x04002A0C RID: 10764
		private int maxFiltersField;

		// Token: 0x04002A0D RID: 10765
		private int maxFilterClauseValueLengthField;

		// Token: 0x04002A0E RID: 10766
		private int maxRecipientsField;

		// Token: 0x04002A0F RID: 10767
		private int maxUserSignatureLengthField;

		// Token: 0x04002A10 RID: 10768
		private int blockListAddressMaxField;

		// Token: 0x04002A11 RID: 10769
		private int blockListDomainMaxField;

		// Token: 0x04002A12 RID: 10770
		private int whiteListAddressMaxField;

		// Token: 0x04002A13 RID: 10771
		private int whiteListDomainMaxField;

		// Token: 0x04002A14 RID: 10772
		private int whiteToListMaxField;

		// Token: 0x04002A15 RID: 10773
		private int alternateFromListMaxField;

		// Token: 0x04002A16 RID: 10774
		private int maxDailySendMessagesField;
	}
}
