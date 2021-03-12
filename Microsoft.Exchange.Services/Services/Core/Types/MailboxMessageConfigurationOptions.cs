using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000ABE RID: 2750
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MailboxMessageConfigurationOptions : OptionsPropertyChangeTracker
	{
		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06004E0E RID: 19982 RVA: 0x00107849 File Offset: 0x00105A49
		// (set) Token: 0x06004E0F RID: 19983 RVA: 0x00107851 File Offset: 0x00105A51
		[DataMember]
		public AfterMoveOrDeleteBehavior AfterMoveOrDeleteBehavior
		{
			get
			{
				return this.afterMoveOrDeleteBehavior;
			}
			set
			{
				this.afterMoveOrDeleteBehavior = value;
				base.TrackPropertyChanged("AfterMoveOrDeleteBehavior");
			}
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06004E10 RID: 19984 RVA: 0x00107865 File Offset: 0x00105A65
		// (set) Token: 0x06004E11 RID: 19985 RVA: 0x0010786D File Offset: 0x00105A6D
		[DataMember]
		public bool AlwaysShowBcc
		{
			get
			{
				return this.alwaysShowBcc;
			}
			set
			{
				this.alwaysShowBcc = value;
				base.TrackPropertyChanged("AlwaysShowBcc");
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x00107881 File Offset: 0x00105A81
		// (set) Token: 0x06004E13 RID: 19987 RVA: 0x00107889 File Offset: 0x00105A89
		[DataMember]
		public bool AlwaysShowFrom
		{
			get
			{
				return this.alwaysShowFrom;
			}
			set
			{
				this.alwaysShowFrom = value;
				base.TrackPropertyChanged("AlwaysShowFrom");
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06004E14 RID: 19988 RVA: 0x0010789D File Offset: 0x00105A9D
		// (set) Token: 0x06004E15 RID: 19989 RVA: 0x001078A5 File Offset: 0x00105AA5
		[DataMember]
		public bool AutoAddSignature
		{
			get
			{
				return this.autoAddSignature;
			}
			set
			{
				this.autoAddSignature = value;
				base.TrackPropertyChanged("AutoAddSignature");
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06004E16 RID: 19990 RVA: 0x001078B9 File Offset: 0x00105AB9
		// (set) Token: 0x06004E17 RID: 19991 RVA: 0x001078C1 File Offset: 0x00105AC1
		[DataMember]
		public bool AutoAddSignatureOnMobile
		{
			get
			{
				return this.autoAddSignatureOnMobile;
			}
			set
			{
				this.autoAddSignatureOnMobile = value;
				base.TrackPropertyChanged("AutoAddSignatureOnMobile");
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06004E18 RID: 19992 RVA: 0x001078D5 File Offset: 0x00105AD5
		// (set) Token: 0x06004E19 RID: 19993 RVA: 0x001078DD File Offset: 0x00105ADD
		[DataMember]
		public bool CheckForForgottenAttachments
		{
			get
			{
				return this.checkForForgottenAttachments;
			}
			set
			{
				this.checkForForgottenAttachments = value;
				base.TrackPropertyChanged("CheckForForgottenAttachments");
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x001078F1 File Offset: 0x00105AF1
		// (set) Token: 0x06004E1B RID: 19995 RVA: 0x001078F9 File Offset: 0x00105AF9
		[DataMember]
		public ConversationSortOrder ConversationSortOrder
		{
			get
			{
				return this.conversationSortOrder;
			}
			set
			{
				this.conversationSortOrder = value;
				base.TrackPropertyChanged("ConversationSortOrder");
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06004E1C RID: 19996 RVA: 0x0010790D File Offset: 0x00105B0D
		// (set) Token: 0x06004E1D RID: 19997 RVA: 0x00107915 File Offset: 0x00105B15
		[DataMember]
		public string DefaultFontColor
		{
			get
			{
				return this.defaultFontColor;
			}
			set
			{
				this.defaultFontColor = value;
				base.TrackPropertyChanged("DefaultFontColor");
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06004E1E RID: 19998 RVA: 0x00107929 File Offset: 0x00105B29
		// (set) Token: 0x06004E1F RID: 19999 RVA: 0x00107931 File Offset: 0x00105B31
		[DataMember]
		public FontFlags DefaultFontFlags
		{
			get
			{
				return this.defaultFontFlags;
			}
			set
			{
				this.defaultFontFlags = value;
				base.TrackPropertyChanged("DefaultFontFlags");
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06004E20 RID: 20000 RVA: 0x00107945 File Offset: 0x00105B45
		// (set) Token: 0x06004E21 RID: 20001 RVA: 0x0010794D File Offset: 0x00105B4D
		[DataMember]
		public string DefaultFontName
		{
			get
			{
				return this.defaultFontName;
			}
			set
			{
				this.defaultFontName = value;
				base.TrackPropertyChanged("DefaultFontName");
			}
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06004E22 RID: 20002 RVA: 0x00107961 File Offset: 0x00105B61
		// (set) Token: 0x06004E23 RID: 20003 RVA: 0x00107969 File Offset: 0x00105B69
		[DataMember]
		public int DefaultFontSize
		{
			get
			{
				return this.defaultFontSize;
			}
			set
			{
				this.defaultFontSize = value;
				base.TrackPropertyChanged("DefaultFontSize");
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06004E24 RID: 20004 RVA: 0x0010797D File Offset: 0x00105B7D
		// (set) Token: 0x06004E25 RID: 20005 RVA: 0x00107985 File Offset: 0x00105B85
		[DataMember]
		public MailFormat DefaultFormat
		{
			get
			{
				return this.defaultFormat;
			}
			set
			{
				this.defaultFormat = value;
				base.TrackPropertyChanged("DefaultFormat");
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06004E26 RID: 20006 RVA: 0x00107999 File Offset: 0x00105B99
		// (set) Token: 0x06004E27 RID: 20007 RVA: 0x001079A1 File Offset: 0x00105BA1
		[DataMember]
		public EmailComposeMode EmailComposeMode
		{
			get
			{
				return this.emailComposeMode;
			}
			set
			{
				this.emailComposeMode = value;
				base.TrackPropertyChanged("EmailComposeMode");
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06004E28 RID: 20008 RVA: 0x001079B5 File Offset: 0x00105BB5
		// (set) Token: 0x06004E29 RID: 20009 RVA: 0x001079BD File Offset: 0x00105BBD
		[DataMember]
		public bool EmptyDeletedItemsOnLogoff
		{
			get
			{
				return this.emptyDeletedItemsOnLogoff;
			}
			set
			{
				this.emptyDeletedItemsOnLogoff = value;
				base.TrackPropertyChanged("EmptyDeletedItemsOnLogoff");
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06004E2A RID: 20010 RVA: 0x001079D1 File Offset: 0x00105BD1
		// (set) Token: 0x06004E2B RID: 20011 RVA: 0x001079D9 File Offset: 0x00105BD9
		[DataMember]
		public bool HideDeletedItems
		{
			get
			{
				return this.hideDeletedItems;
			}
			set
			{
				this.hideDeletedItems = value;
				base.TrackPropertyChanged("HideDeletedItems");
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06004E2C RID: 20012 RVA: 0x001079ED File Offset: 0x00105BED
		// (set) Token: 0x06004E2D RID: 20013 RVA: 0x001079F5 File Offset: 0x00105BF5
		[DataMember]
		public NewItemNotification NewItemNotification
		{
			get
			{
				return this.newItemNotification;
			}
			set
			{
				this.newItemNotification = value;
				base.TrackPropertyChanged("NewItemNotification");
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06004E2E RID: 20014 RVA: 0x00107A09 File Offset: 0x00105C09
		// (set) Token: 0x06004E2F RID: 20015 RVA: 0x00107A11 File Offset: 0x00105C11
		[DataMember]
		public PreviewMarkAsReadBehavior PreviewMarkAsReadBehavior
		{
			get
			{
				return this.previewMarkAsReadBehavior;
			}
			set
			{
				this.previewMarkAsReadBehavior = value;
				base.TrackPropertyChanged("PreviewMarkAsReadBehavior");
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x00107A25 File Offset: 0x00105C25
		// (set) Token: 0x06004E31 RID: 20017 RVA: 0x00107A2D File Offset: 0x00105C2D
		[DataMember]
		public int PreviewMarkAsReadDelaytime
		{
			get
			{
				return this.previewMarkAsReadDelayTime;
			}
			set
			{
				this.previewMarkAsReadDelayTime = value;
				base.TrackPropertyChanged("PreviewMarkAsReadDelaytime");
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x00107A41 File Offset: 0x00105C41
		// (set) Token: 0x06004E33 RID: 20019 RVA: 0x00107A49 File Offset: 0x00105C49
		[DataMember]
		public ReadReceiptResponse ReadReceiptResponse
		{
			get
			{
				return this.readReceiptResponse;
			}
			set
			{
				this.readReceiptResponse = value;
				base.TrackPropertyChanged("ReadReceiptResponse");
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06004E34 RID: 20020 RVA: 0x00107A5D File Offset: 0x00105C5D
		// (set) Token: 0x06004E35 RID: 20021 RVA: 0x00107A65 File Offset: 0x00105C65
		[DataMember]
		public string SendAddressDefault
		{
			get
			{
				return this.sendAddressDefault;
			}
			set
			{
				this.sendAddressDefault = value;
				base.TrackPropertyChanged("SendAddressDefault");
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06004E36 RID: 20022 RVA: 0x00107A79 File Offset: 0x00105C79
		// (set) Token: 0x06004E37 RID: 20023 RVA: 0x00107A81 File Offset: 0x00105C81
		[DataMember]
		public bool ShowConversationAsTree
		{
			get
			{
				return this.showConversationAsTree;
			}
			set
			{
				this.showConversationAsTree = value;
				base.TrackPropertyChanged("ShowConversationAsTree");
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06004E38 RID: 20024 RVA: 0x00107A95 File Offset: 0x00105C95
		// (set) Token: 0x06004E39 RID: 20025 RVA: 0x00107A9D File Offset: 0x00105C9D
		[DataMember]
		public string SignatureHtml
		{
			get
			{
				return this.signatureHtml;
			}
			set
			{
				this.signatureHtml = value;
				base.TrackPropertyChanged("SignatureHtml");
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06004E3A RID: 20026 RVA: 0x00107AB1 File Offset: 0x00105CB1
		// (set) Token: 0x06004E3B RID: 20027 RVA: 0x00107AB9 File Offset: 0x00105CB9
		[DataMember]
		public string SignatureText
		{
			get
			{
				return this.signatureText;
			}
			set
			{
				this.signatureText = value;
				base.TrackPropertyChanged("SignatureText");
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06004E3C RID: 20028 RVA: 0x00107ACD File Offset: 0x00105CCD
		// (set) Token: 0x06004E3D RID: 20029 RVA: 0x00107AD5 File Offset: 0x00105CD5
		[DataMember]
		public string SignatureTextOnMobile
		{
			get
			{
				return this.signatureTextOnMobile;
			}
			set
			{
				this.signatureTextOnMobile = value;
				base.TrackPropertyChanged("SignatureTextOnMobile");
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x00107AE9 File Offset: 0x00105CE9
		// (set) Token: 0x06004E3F RID: 20031 RVA: 0x00107AF1 File Offset: 0x00105CF1
		[DataMember]
		public bool UseDefaultSignatureOnMobile
		{
			get
			{
				return this.useDefaultSignatureOnMobile;
			}
			set
			{
				this.useDefaultSignatureOnMobile = value;
				base.TrackPropertyChanged("UseDefaultSignatureOnMobile");
			}
		}

		// Token: 0x04002BE4 RID: 11236
		private AfterMoveOrDeleteBehavior afterMoveOrDeleteBehavior;

		// Token: 0x04002BE5 RID: 11237
		private bool alwaysShowBcc;

		// Token: 0x04002BE6 RID: 11238
		private bool alwaysShowFrom;

		// Token: 0x04002BE7 RID: 11239
		private bool autoAddSignature;

		// Token: 0x04002BE8 RID: 11240
		private bool autoAddSignatureOnMobile;

		// Token: 0x04002BE9 RID: 11241
		private bool checkForForgottenAttachments;

		// Token: 0x04002BEA RID: 11242
		private ConversationSortOrder conversationSortOrder;

		// Token: 0x04002BEB RID: 11243
		private string defaultFontColor;

		// Token: 0x04002BEC RID: 11244
		private FontFlags defaultFontFlags;

		// Token: 0x04002BED RID: 11245
		private string defaultFontName;

		// Token: 0x04002BEE RID: 11246
		private int defaultFontSize;

		// Token: 0x04002BEF RID: 11247
		private MailFormat defaultFormat;

		// Token: 0x04002BF0 RID: 11248
		private EmailComposeMode emailComposeMode;

		// Token: 0x04002BF1 RID: 11249
		private bool emptyDeletedItemsOnLogoff;

		// Token: 0x04002BF2 RID: 11250
		private bool hideDeletedItems;

		// Token: 0x04002BF3 RID: 11251
		private NewItemNotification newItemNotification;

		// Token: 0x04002BF4 RID: 11252
		private PreviewMarkAsReadBehavior previewMarkAsReadBehavior;

		// Token: 0x04002BF5 RID: 11253
		private int previewMarkAsReadDelayTime;

		// Token: 0x04002BF6 RID: 11254
		private ReadReceiptResponse readReceiptResponse;

		// Token: 0x04002BF7 RID: 11255
		private bool showConversationAsTree;

		// Token: 0x04002BF8 RID: 11256
		private string sendAddressDefault;

		// Token: 0x04002BF9 RID: 11257
		private string signatureHtml;

		// Token: 0x04002BFA RID: 11258
		private string signatureText;

		// Token: 0x04002BFB RID: 11259
		private string signatureTextOnMobile;

		// Token: 0x04002BFC RID: 11260
		private bool useDefaultSignatureOnMobile;
	}
}
