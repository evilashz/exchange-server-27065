using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005C5 RID: 1477
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OutboundConversionOptions : ICloneable
	{
		// Token: 0x06003C9D RID: 15517 RVA: 0x000F8934 File Offset: 0x000F6B34
		public OutboundConversionOptions(string imceaDomain)
		{
			this.imceaEncapsulationDomain = InboundConversionOptions.CheckImceaDomain(imceaDomain);
			this.encodeAttachmentsAsBinhex = false;
			this.suppressDisplayName = false;
			this.internetMessageFormat = InternetMessageFormat.Mime;
			this.internetTextFormat = InternetTextFormat.HtmlAndTextAlternative;
			this.preserveReportBody = true;
			this.byteEncoderTypeFor7BitCharsets = ByteEncoderTypeFor7BitCharsets.UseQP;
			this.clearCategories = true;
			this.owaServer = null;
			this.userADSession = null;
			this.recipientCache = null;
			this.useRFC2231Encoding = false;
			this.allowUTF8Headers = false;
			this.limits = new ConversionLimits(false);
			this.dsnWriter = null;
			this.logDirectoryPath = null;
			this.isSenderTrusted = true;
			this.useSimpleDisplayName = false;
			this.allowPartialStnefContent = false;
			this.generateMimeSkeleton = false;
			this.demoteBcc = false;
			this.resolveRecipientsInAttachedMessages = true;
			this.quoteDisplayNameBeforeRfc2047Encoding = false;
			this.allowDlpHeadersToPenetrateFirewall = false;
			this.EnableCalendarHeaderGeneration = true;
			this.EwsOutboundMimeConversion = false;
			this.blockPlainTextConversion = true;
			this.useSkeleton = true;
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000F8A20 File Offset: 0x000F6C20
		public OutboundConversionOptions(IRecipientSession scopedRecipientSession, string imceaDomain) : this(imceaDomain)
		{
			this.UserADSession = scopedRecipientSession;
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x000F8A30 File Offset: 0x000F6C30
		public OutboundConversionOptions(IADRecipientCache scopedRecipientCache, string imceaDomain) : this(imceaDomain)
		{
			this.RecipientCache = scopedRecipientCache;
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x000F8A40 File Offset: 0x000F6C40
		public OutboundConversionOptions(OutboundConversionOptions source)
		{
			this.encodeAttachmentsAsBinhex = source.encodeAttachmentsAsBinhex;
			this.suppressDisplayName = source.suppressDisplayName;
			this.internetMessageFormat = source.internetMessageFormat;
			this.internetTextFormat = source.internetTextFormat;
			this.imceaEncapsulationDomain = source.imceaEncapsulationDomain;
			this.preserveReportBody = source.preserveReportBody;
			this.byteEncoderTypeFor7BitCharsets = source.byteEncoderTypeFor7BitCharsets;
			this.clearCategories = source.clearCategories;
			this.owaServer = source.owaServer;
			this.userADSession = source.userADSession;
			this.recipientCache = source.recipientCache;
			this.useRFC2231Encoding = source.useRFC2231Encoding;
			this.allowUTF8Headers = source.allowUTF8Headers;
			this.limits = (ConversionLimits)source.limits.Clone();
			this.dsnWriter = source.dsnWriter;
			this.logDirectoryPath = source.LogDirectoryPath;
			this.isSenderTrusted = source.IsSenderTrusted;
			this.useSimpleDisplayName = source.useSimpleDisplayName;
			this.allowPartialStnefContent = source.allowPartialStnefContent;
			this.generateMimeSkeleton = source.generateMimeSkeleton;
			this.demoteBcc = source.demoteBcc;
			this.detectionOptions = source.detectionOptions;
			this.filterAttachment = source.filterAttachment;
			this.filterBody = source.filterBody;
			this.resolveRecipientsInAttachedMessages = source.resolveRecipientsInAttachedMessages;
			this.quoteDisplayNameBeforeRfc2047Encoding = source.quoteDisplayNameBeforeRfc2047Encoding;
			this.allowDlpHeadersToPenetrateFirewall = source.allowDlpHeadersToPenetrateFirewall;
			this.EnableCalendarHeaderGeneration = source.EnableCalendarHeaderGeneration;
			this.EwsOutboundMimeConversion = source.EwsOutboundMimeConversion;
			this.blockPlainTextConversion = source.BlockPlainTextConversion;
			this.useSkeleton = source.UseSkeleton;
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x000F8BDC File Offset: 0x000F6DDC
		// (set) Token: 0x06003CA2 RID: 15522 RVA: 0x000F8BE4 File Offset: 0x000F6DE4
		public CharsetDetectionOptions DetectionOptions
		{
			get
			{
				return this.detectionOptions;
			}
			set
			{
				if (value != null)
				{
					this.detectionOptions = new CharsetDetectionOptions(value);
					return;
				}
				throw new ArgumentNullException();
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x000F8BFB File Offset: 0x000F6DFB
		// (set) Token: 0x06003CA4 RID: 15524 RVA: 0x000F8C03 File Offset: 0x000F6E03
		public bool DemoteBcc
		{
			get
			{
				return this.demoteBcc;
			}
			set
			{
				this.demoteBcc = value;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x000F8C0C File Offset: 0x000F6E0C
		// (set) Token: 0x06003CA6 RID: 15526 RVA: 0x000F8C14 File Offset: 0x000F6E14
		public bool GenerateMimeSkeleton
		{
			get
			{
				return this.generateMimeSkeleton;
			}
			set
			{
				this.generateMimeSkeleton = value;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x000F8C1D File Offset: 0x000F6E1D
		// (set) Token: 0x06003CA8 RID: 15528 RVA: 0x000F8C25 File Offset: 0x000F6E25
		public bool EncodeAttachmentsAsBinhex
		{
			get
			{
				return this.encodeAttachmentsAsBinhex;
			}
			set
			{
				this.encodeAttachmentsAsBinhex = value;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x000F8C2E File Offset: 0x000F6E2E
		// (set) Token: 0x06003CAA RID: 15530 RVA: 0x000F8C36 File Offset: 0x000F6E36
		public bool SuppressDisplayName
		{
			get
			{
				return this.suppressDisplayName;
			}
			set
			{
				this.suppressDisplayName = value;
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x000F8C3F File Offset: 0x000F6E3F
		// (set) Token: 0x06003CAC RID: 15532 RVA: 0x000F8C47 File Offset: 0x000F6E47
		public DsnMdnOptions DsnMdnOptions
		{
			get
			{
				return this.dsnMdnOptions;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<DsnMdnOptions>(value, "value");
				this.dsnMdnOptions = value;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000F8C5B File Offset: 0x000F6E5B
		// (set) Token: 0x06003CAE RID: 15534 RVA: 0x000F8C63 File Offset: 0x000F6E63
		public InternetMessageFormat InternetMessageFormat
		{
			get
			{
				return this.internetMessageFormat;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<InternetMessageFormat>(value, "value");
				this.internetMessageFormat = value;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06003CAF RID: 15535 RVA: 0x000F8C77 File Offset: 0x000F6E77
		// (set) Token: 0x06003CB0 RID: 15536 RVA: 0x000F8C7F File Offset: 0x000F6E7F
		public InternetTextFormat InternetTextFormat
		{
			get
			{
				return this.internetTextFormat;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<InternetTextFormat>(value, "value");
				this.internetTextFormat = value;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06003CB1 RID: 15537 RVA: 0x000F8C93 File Offset: 0x000F6E93
		// (set) Token: 0x06003CB2 RID: 15538 RVA: 0x000F8C9B File Offset: 0x000F6E9B
		public bool PreserveReportBody
		{
			get
			{
				return this.preserveReportBody;
			}
			set
			{
				this.preserveReportBody = value;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x000F8CA4 File Offset: 0x000F6EA4
		// (set) Token: 0x06003CB4 RID: 15540 RVA: 0x000F8CAC File Offset: 0x000F6EAC
		public ConversionLimits Limits
		{
			get
			{
				return this.limits;
			}
			set
			{
				this.limits = value;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000F8CB5 File Offset: 0x000F6EB5
		// (set) Token: 0x06003CB6 RID: 15542 RVA: 0x000F8CBD File Offset: 0x000F6EBD
		public string ImceaEncapsulationDomain
		{
			get
			{
				return this.imceaEncapsulationDomain;
			}
			set
			{
				this.imceaEncapsulationDomain = InboundConversionOptions.CheckImceaDomain(value);
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x000F8CCB File Offset: 0x000F6ECB
		// (set) Token: 0x06003CB8 RID: 15544 RVA: 0x000F8CD3 File Offset: 0x000F6ED3
		public ByteEncoderTypeFor7BitCharsets ByteEncoderTypeFor7BitCharsets
		{
			get
			{
				return this.byteEncoderTypeFor7BitCharsets;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<ByteEncoderTypeFor7BitCharsets>(value, "ByteEncoderTypeFor7BitCharsets");
				this.byteEncoderTypeFor7BitCharsets = value;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000F8CE7 File Offset: 0x000F6EE7
		// (set) Token: 0x06003CBA RID: 15546 RVA: 0x000F8CEF File Offset: 0x000F6EEF
		public bool ClearCategories
		{
			get
			{
				return this.clearCategories;
			}
			set
			{
				this.clearCategories = value;
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x000F8CF8 File Offset: 0x000F6EF8
		// (set) Token: 0x06003CBC RID: 15548 RVA: 0x000F8D00 File Offset: 0x000F6F00
		public string LogDirectoryPath
		{
			get
			{
				return this.logDirectoryPath;
			}
			set
			{
				this.logDirectoryPath = value;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x000F8D09 File Offset: 0x000F6F09
		// (set) Token: 0x06003CBE RID: 15550 RVA: 0x000F8D11 File Offset: 0x000F6F11
		public bool IsSenderTrusted
		{
			get
			{
				return this.isSenderTrusted;
			}
			set
			{
				this.isSenderTrusted = value;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000F8D1A File Offset: 0x000F6F1A
		// (set) Token: 0x06003CC0 RID: 15552 RVA: 0x000F8D24 File Offset: 0x000F6F24
		public string OwaServer
		{
			get
			{
				return this.owaServer;
			}
			set
			{
				Uri uri;
				if (!Uri.TryCreate(value, UriKind.Absolute, out uri))
				{
					throw new ArgumentException("value");
				}
				this.owaServer = uri.ToString();
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000F8D53 File Offset: 0x000F6F53
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x000F8D5B File Offset: 0x000F6F5B
		public IRecipientSession UserADSession
		{
			get
			{
				return this.userADSession;
			}
			set
			{
				this.userADSession = value;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000F8D64 File Offset: 0x000F6F64
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x000F8D6C File Offset: 0x000F6F6C
		public IADRecipientCache RecipientCache
		{
			get
			{
				return this.recipientCache;
			}
			set
			{
				this.recipientCache = value;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000F8D75 File Offset: 0x000F6F75
		// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x000F8D7D File Offset: 0x000F6F7D
		public bool UseRFC2231Encoding
		{
			get
			{
				return this.useRFC2231Encoding;
			}
			set
			{
				this.useRFC2231Encoding = value;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x000F8D86 File Offset: 0x000F6F86
		// (set) Token: 0x06003CC8 RID: 15560 RVA: 0x000F8D8E File Offset: 0x000F6F8E
		public bool AllowUTF8Headers
		{
			get
			{
				return this.allowUTF8Headers;
			}
			set
			{
				this.allowUTF8Headers = value;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000F8D97 File Offset: 0x000F6F97
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x000F8D9F File Offset: 0x000F6F9F
		public DsnHumanReadableWriter DsnHumanReadableWriter
		{
			get
			{
				return this.dsnWriter;
			}
			set
			{
				this.dsnWriter = value;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000F8DA8 File Offset: 0x000F6FA8
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x000F8DB0 File Offset: 0x000F6FB0
		public bool UseSimpleDisplayName
		{
			get
			{
				return this.useSimpleDisplayName;
			}
			set
			{
				this.useSimpleDisplayName = value;
			}
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x000F8DB9 File Offset: 0x000F6FB9
		// (set) Token: 0x06003CCE RID: 15566 RVA: 0x000F8DC1 File Offset: 0x000F6FC1
		public bool AllowPartialStnefConversion
		{
			get
			{
				return this.allowPartialStnefContent;
			}
			set
			{
				this.allowPartialStnefContent = value;
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x000F8DCA File Offset: 0x000F6FCA
		// (set) Token: 0x06003CD0 RID: 15568 RVA: 0x000F8DD2 File Offset: 0x000F6FD2
		public bool QuoteDisplayNameBeforeRfc2047Encoding
		{
			get
			{
				return this.quoteDisplayNameBeforeRfc2047Encoding;
			}
			set
			{
				this.quoteDisplayNameBeforeRfc2047Encoding = value;
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x000F8DDB File Offset: 0x000F6FDB
		// (set) Token: 0x06003CD2 RID: 15570 RVA: 0x000F8DE3 File Offset: 0x000F6FE3
		public bool ResolveRecipientsInAttachedMessages
		{
			get
			{
				return this.resolveRecipientsInAttachedMessages;
			}
			set
			{
				this.resolveRecipientsInAttachedMessages = value;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x000F8DEC File Offset: 0x000F6FEC
		// (set) Token: 0x06003CD4 RID: 15572 RVA: 0x000F8DF4 File Offset: 0x000F6FF4
		public OutboundConversionOptions.FilterAttachment FilterAttachmentHandler
		{
			get
			{
				return this.filterAttachment;
			}
			set
			{
				this.filterAttachment = value;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06003CD5 RID: 15573 RVA: 0x000F8DFD File Offset: 0x000F6FFD
		// (set) Token: 0x06003CD6 RID: 15574 RVA: 0x000F8E05 File Offset: 0x000F7005
		public OutboundConversionOptions.FilterBody FilterBodyHandler
		{
			get
			{
				return this.filterBody;
			}
			set
			{
				this.filterBody = value;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000F8E0E File Offset: 0x000F700E
		// (set) Token: 0x06003CD8 RID: 15576 RVA: 0x000F8E16 File Offset: 0x000F7016
		public bool AllowDlpHeadersToPenetrateFirewall
		{
			get
			{
				return this.allowDlpHeadersToPenetrateFirewall;
			}
			set
			{
				this.allowDlpHeadersToPenetrateFirewall = value;
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x000F8E1F File Offset: 0x000F701F
		// (set) Token: 0x06003CDA RID: 15578 RVA: 0x000F8E27 File Offset: 0x000F7027
		public bool BlockPlainTextConversion
		{
			get
			{
				return this.blockPlainTextConversion;
			}
			set
			{
				this.blockPlainTextConversion = value;
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x000F8E30 File Offset: 0x000F7030
		// (set) Token: 0x06003CDC RID: 15580 RVA: 0x000F8E38 File Offset: 0x000F7038
		public bool UseSkeleton
		{
			get
			{
				return this.useSkeleton;
			}
			set
			{
				this.useSkeleton = value;
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x000F8E41 File Offset: 0x000F7041
		// (set) Token: 0x06003CDE RID: 15582 RVA: 0x000F8E49 File Offset: 0x000F7049
		public bool EnableCalendarHeaderGeneration { get; set; }

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06003CDF RID: 15583 RVA: 0x000F8E52 File Offset: 0x000F7052
		// (set) Token: 0x06003CE0 RID: 15584 RVA: 0x000F8E5A File Offset: 0x000F705A
		public bool EwsOutboundMimeConversion { get; set; }

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000F8E63 File Offset: 0x000F7063
		public object Clone()
		{
			return new OutboundConversionOptions(this);
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000F8E6C File Offset: 0x000F706C
		public override string ToString()
		{
			return string.Format("OutboundConversionOptions:\r\n- detectionOptions: {0}\r\n- encodeAttachmentsAsBinhex: {1}\r\n- suppressDisplayName: {2}\r\n- internetMessageFormat: {3}\r\n- internetTextFormat: {4}\r\n- imceaEncapsulationDomain: {5}\r\n- preserveReportBody: {6}\r\n- byteEncoderTypeFor7BitCharsets: {7}\r\n- clearCategories: {8}\r\n- owaServer: {9}\r\n- logDirectoryPath: {10}\r\n- isSenderTrusted: {11}\r\n- dsnWriter: {12}\r\n- userADSession: {13}\r\n- useRFC2231Encoding: {14}\r\n- recipientCache: {15}\r\n- demoteBcc: {16}\r\n- useSimpleDisplayName: {17}\r\n- partialStnefConversion: {18}\r\n- resolveRecipientsInAttachedMessages: {19}\r\n- quoteDisplayNameBeforeRfc2047Encoding: {20}\r\n- allowDlpHeadersToPenetrateFirewall: {21}\r\n- enableCalendarHeaderGeneration: {22}\r\n- ewsOutboundMimeConversion: {23}\r\n{24}\r\n", new object[]
			{
				this.detectionOptions.ToString(),
				this.encodeAttachmentsAsBinhex,
				this.suppressDisplayName,
				this.internetMessageFormat,
				this.internetTextFormat,
				this.imceaEncapsulationDomain,
				this.preserveReportBody,
				this.byteEncoderTypeFor7BitCharsets,
				this.clearCategories,
				this.owaServer,
				this.logDirectoryPath,
				this.isSenderTrusted,
				this.dsnWriter,
				this.userADSession,
				this.useRFC2231Encoding,
				this.recipientCache,
				this.demoteBcc,
				this.useSimpleDisplayName,
				this.allowPartialStnefContent,
				this.resolveRecipientsInAttachedMessages,
				this.quoteDisplayNameBeforeRfc2047Encoding,
				this.allowDlpHeadersToPenetrateFirewall,
				this.EnableCalendarHeaderGeneration,
				this.EwsOutboundMimeConversion,
				this.limits.ToString()
			});
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000F8FDC File Offset: 0x000F71DC
		public void LoadPerOrganizationCharsetDetectionOptions(OrganizationId organizationId)
		{
			OrganizationContentConversionProperties organizationContentConversionProperties;
			if (OutboundConversionOptions.directoryAccessor.TryGetOrganizationContentConversionProperties(organizationId, out organizationContentConversionProperties))
			{
				this.detectionOptions.PreferredInternetCodePageForShiftJis = organizationContentConversionProperties.PreferredInternetCodePageForShiftJis;
				this.detectionOptions.RequiredCoverage = organizationContentConversionProperties.RequiredCharsetCoverage;
				if (Enum.IsDefined(typeof(ByteEncoderTypeFor7BitCharsets), organizationContentConversionProperties.ByteEncoderTypeFor7BitCharsets))
				{
					this.byteEncoderTypeFor7BitCharsets = (ByteEncoderTypeFor7BitCharsets)organizationContentConversionProperties.ByteEncoderTypeFor7BitCharsets;
				}
			}
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x000F9044 File Offset: 0x000F7244
		internal IADRecipientCache InternalGetRecipientCache(int count)
		{
			IADRecipientCache iadrecipientCache = this.RecipientCache;
			if (iadrecipientCache == null)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = this.UserADSession;
				if (tenantOrRootOrgRecipientSession == null)
				{
					try
					{
						tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2002, "InternalGetRecipientCache", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ContentConversion\\ConversionOptions.cs");
					}
					catch (ExAssertException)
					{
						throw new ArgumentException(InboundConversionOptions.NoScopedTenantInfoNotice);
					}
				}
				iadrecipientCache = new ADRecipientCache<ADRawEntry>(tenantOrRootOrgRecipientSession, Util.CollectionToArray<ADPropertyDefinition>(ParticipantSchema.SupportedADProperties), count);
			}
			return iadrecipientCache;
		}

		// Token: 0x04002049 RID: 8265
		private OutboundConversionOptions.FilterAttachment filterAttachment;

		// Token: 0x0400204A RID: 8266
		private OutboundConversionOptions.FilterBody filterBody;

		// Token: 0x0400204B RID: 8267
		private bool encodeAttachmentsAsBinhex;

		// Token: 0x0400204C RID: 8268
		private bool suppressDisplayName;

		// Token: 0x0400204D RID: 8269
		private DsnMdnOptions dsnMdnOptions;

		// Token: 0x0400204E RID: 8270
		private InternetMessageFormat internetMessageFormat;

		// Token: 0x0400204F RID: 8271
		private InternetTextFormat internetTextFormat;

		// Token: 0x04002050 RID: 8272
		private string imceaEncapsulationDomain;

		// Token: 0x04002051 RID: 8273
		private bool preserveReportBody;

		// Token: 0x04002052 RID: 8274
		private ByteEncoderTypeFor7BitCharsets byteEncoderTypeFor7BitCharsets;

		// Token: 0x04002053 RID: 8275
		private bool clearCategories;

		// Token: 0x04002054 RID: 8276
		private string owaServer;

		// Token: 0x04002055 RID: 8277
		private IRecipientSession userADSession;

		// Token: 0x04002056 RID: 8278
		private IADRecipientCache recipientCache;

		// Token: 0x04002057 RID: 8279
		private string logDirectoryPath;

		// Token: 0x04002058 RID: 8280
		private bool useRFC2231Encoding;

		// Token: 0x04002059 RID: 8281
		private bool allowUTF8Headers;

		// Token: 0x0400205A RID: 8282
		private bool isSenderTrusted;

		// Token: 0x0400205B RID: 8283
		private bool useSimpleDisplayName;

		// Token: 0x0400205C RID: 8284
		private bool allowPartialStnefContent;

		// Token: 0x0400205D RID: 8285
		private bool quoteDisplayNameBeforeRfc2047Encoding;

		// Token: 0x0400205E RID: 8286
		private bool generateMimeSkeleton;

		// Token: 0x0400205F RID: 8287
		private bool demoteBcc;

		// Token: 0x04002060 RID: 8288
		private CharsetDetectionOptions detectionOptions = new CharsetDetectionOptions();

		// Token: 0x04002061 RID: 8289
		private ConversionLimits limits;

		// Token: 0x04002062 RID: 8290
		private DsnHumanReadableWriter dsnWriter;

		// Token: 0x04002063 RID: 8291
		private bool resolveRecipientsInAttachedMessages;

		// Token: 0x04002064 RID: 8292
		private bool allowDlpHeadersToPenetrateFirewall;

		// Token: 0x04002065 RID: 8293
		private bool blockPlainTextConversion;

		// Token: 0x04002066 RID: 8294
		private bool useSkeleton;

		// Token: 0x04002067 RID: 8295
		private static readonly IDirectoryAccessor directoryAccessor = new DirectoryAccessor();

		// Token: 0x020005C6 RID: 1478
		// (Invoke) Token: 0x06003CE7 RID: 15591
		public delegate bool FilterAttachment(Item item, Attachment attachment);

		// Token: 0x020005C7 RID: 1479
		// (Invoke) Token: 0x06003CEB RID: 15595
		public delegate bool FilterBody(Item item);
	}
}
