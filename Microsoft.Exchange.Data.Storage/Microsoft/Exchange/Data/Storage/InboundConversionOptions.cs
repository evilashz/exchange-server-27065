using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005C0 RID: 1472
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InboundConversionOptions : ICloneable
	{
		// Token: 0x06003C65 RID: 15461 RVA: 0x000F81E0 File Offset: 0x000F63E0
		private InboundConversionOptions()
		{
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x000F8238 File Offset: 0x000F6438
		public InboundConversionOptions(string imceaDomain)
		{
			this.imceaResolvableDomain = InboundConversionOptions.CheckImceaDomain(imceaDomain);
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x000F829A File Offset: 0x000F649A
		public InboundConversionOptions(IRecipientSession scopedRecipientSession) : this(scopedRecipientSession, null)
		{
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x000F82A4 File Offset: 0x000F64A4
		public InboundConversionOptions(IRecipientSession scopedRecipientSession, string imceaDomain)
		{
			if (string.IsNullOrEmpty(imceaDomain))
			{
				this.ignoreImceaDomain = true;
			}
			else
			{
				this.imceaResolvableDomain = InboundConversionOptions.CheckImceaDomain(imceaDomain);
			}
			this.UserADSession = scopedRecipientSession;
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x000F831E File Offset: 0x000F651E
		public InboundConversionOptions(IADRecipientCache scopedRecipientCache) : this(scopedRecipientCache, null)
		{
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x000F8328 File Offset: 0x000F6528
		public InboundConversionOptions(IADRecipientCache scopedRecipientCache, string imceaDomain)
		{
			if (string.IsNullOrEmpty(imceaDomain))
			{
				this.ignoreImceaDomain = true;
			}
			else
			{
				this.imceaResolvableDomain = InboundConversionOptions.CheckImceaDomain(imceaDomain);
			}
			this.RecipientCache = scopedRecipientCache;
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x000F83A4 File Offset: 0x000F65A4
		public InboundConversionOptions(InboundConversionOptions source)
		{
			this.defaultCharset = source.defaultCharset;
			this.trustAsciiCharsets = source.trustAsciiCharsets;
			this.isSenderTrusted = source.isSenderTrusted;
			this.imceaResolvableDomain = source.imceaResolvableDomain;
			this.preserveReportBody = source.preserveReportBody;
			this.clearCategories = source.clearCategories;
			this.userADSession = source.userADSession;
			this.recipientCache = source.recipientCache;
			this.limits = (ConversionLimits)source.limits.Clone();
			this.dsnWriter = source.dsnWriter;
			this.clientSubmittedSecurely = source.clientSubmittedSecurely;
			this.serverSubmittedSecurely = source.serverSubmittedSecurely;
			this.logDirectoryPath = source.logDirectoryPath;
			this.treatInlineDispositionAsAttachment = source.treatInlineDispositionAsAttachment;
			this.headerPromotionMode = source.headerPromotionMode;
			this.convertReportToMessage = source.convertReportToMessage;
			this.detectionOptions = new CharsetDetectionOptions(source.detectionOptions);
			this.applyTrustToAttachedMessages = source.applyTrustToAttachedMessages;
			this.resolveRecipientsInAttachedMessages = source.resolveRecipientsInAttachedMessages;
			this.applyHeaderFirewall = source.applyHeaderFirewall;
			this.ignoreImceaDomain = source.ignoreImceaDomain;
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x000F8508 File Offset: 0x000F6708
		internal static InboundConversionOptions CreateWithNoDomain()
		{
			return new InboundConversionOptions
			{
				ignoreImceaDomain = true
			};
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x000F8524 File Offset: 0x000F6724
		internal static InboundConversionOptions FromOutboundOptions(OutboundConversionOptions outboundOptions)
		{
			InboundConversionOptions inboundConversionOptions = new InboundConversionOptions(outboundOptions.ImceaEncapsulationDomain);
			if (outboundOptions.DetectionOptions.PreferredCharset != null)
			{
				inboundConversionOptions.defaultCharset = outboundOptions.DetectionOptions.PreferredCharset;
			}
			inboundConversionOptions.isSenderTrusted = outboundOptions.IsSenderTrusted;
			inboundConversionOptions.userADSession = outboundOptions.UserADSession;
			inboundConversionOptions.recipientCache = outboundOptions.RecipientCache;
			inboundConversionOptions.clearCategories = outboundOptions.ClearCategories;
			inboundConversionOptions.preserveReportBody = outboundOptions.PreserveReportBody;
			inboundConversionOptions.logDirectoryPath = outboundOptions.LogDirectoryPath;
			inboundConversionOptions.detectionOptions = outboundOptions.DetectionOptions;
			inboundConversionOptions.resolveRecipientsInAttachedMessages = outboundOptions.ResolveRecipientsInAttachedMessages;
			if (outboundOptions.Limits != null)
			{
				inboundConversionOptions.limits = outboundOptions.Limits;
			}
			return inboundConversionOptions;
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x000F85D0 File Offset: 0x000F67D0
		internal static string CheckImceaDomain(string imceaDomain)
		{
			if (string.IsNullOrEmpty(imceaDomain))
			{
				return null;
			}
			if (!SmtpAddress.IsValidDomain(imceaDomain))
			{
				throw new ArgumentException("imceaDomain must be a valid domain name. Domain value: " + imceaDomain);
			}
			return imceaDomain;
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x000F85F8 File Offset: 0x000F67F8
		public override string ToString()
		{
			return string.Format("InboundConversionOptions:\r\n- defaultCharset: {0}\r\n- trustAsciiCharsets: {1}\r\n- isSenderTrusted: {2}\r\n- imceaResolveableDomain: {3}\r\n- preserveReportBody: {4}\r\n- clearCategories: {5}\r\n- userADSession: {6}\r\n- recipientCache: {7}\r\n- clientSubmittedSecurely: {8}\r\n- serverSubmittedSecurely: {9}\r\n- charsetDetectionOptions: {10}\r\n- convertReportToMessage:  {11}\r\n- headerPromotionMode: {12}\r\n- treatInlineDispositionAsAttachment:  {13}\r\n- applyTrustToAttachedMessages: {14}\r\n- resolveRecipientsInAttachedMessages: {15}\r\n- applyHeaderFirewall: {16}\r\n- ignoreImceaDomain: {17}\r\n{18}\r\n", new object[]
			{
				this.defaultCharset,
				this.trustAsciiCharsets,
				this.isSenderTrusted,
				this.imceaResolvableDomain,
				this.preserveReportBody,
				this.clearCategories,
				this.userADSession,
				this.recipientCache,
				this.clientSubmittedSecurely,
				this.serverSubmittedSecurely,
				this.detectionOptions.ToString(),
				this.convertReportToMessage,
				this.headerPromotionMode,
				this.treatInlineDispositionAsAttachment,
				this.applyTrustToAttachedMessages,
				this.resolveRecipientsInAttachedMessages,
				this.applyHeaderFirewall,
				this.ignoreImceaDomain,
				this.limits.ToString()
			});
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x000F8718 File Offset: 0x000F6918
		internal bool IgnoreImceaDomain
		{
			get
			{
				return this.ignoreImceaDomain;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x000F8720 File Offset: 0x000F6920
		// (set) Token: 0x06003C72 RID: 15474 RVA: 0x000F8728 File Offset: 0x000F6928
		public Charset DefaultCharset
		{
			get
			{
				return this.defaultCharset;
			}
			set
			{
				this.defaultCharset = (value ?? Culture.Default.MimeCharset);
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x000F873F File Offset: 0x000F693F
		// (set) Token: 0x06003C74 RID: 15476 RVA: 0x000F8747 File Offset: 0x000F6947
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

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x000F875E File Offset: 0x000F695E
		// (set) Token: 0x06003C76 RID: 15478 RVA: 0x000F8766 File Offset: 0x000F6966
		public bool TrustAsciiCharsets
		{
			get
			{
				return this.trustAsciiCharsets;
			}
			set
			{
				this.trustAsciiCharsets = value;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06003C77 RID: 15479 RVA: 0x000F876F File Offset: 0x000F696F
		// (set) Token: 0x06003C78 RID: 15480 RVA: 0x000F8777 File Offset: 0x000F6977
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

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x000F8780 File Offset: 0x000F6980
		// (set) Token: 0x06003C7A RID: 15482 RVA: 0x000F8788 File Offset: 0x000F6988
		public string ImceaEncapsulationDomain
		{
			get
			{
				return this.imceaResolvableDomain;
			}
			set
			{
				this.imceaResolvableDomain = InboundConversionOptions.CheckImceaDomain(value);
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06003C7B RID: 15483 RVA: 0x000F8796 File Offset: 0x000F6996
		// (set) Token: 0x06003C7C RID: 15484 RVA: 0x000F879E File Offset: 0x000F699E
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

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x000F87A7 File Offset: 0x000F69A7
		// (set) Token: 0x06003C7E RID: 15486 RVA: 0x000F87AF File Offset: 0x000F69AF
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

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x000F87B8 File Offset: 0x000F69B8
		// (set) Token: 0x06003C80 RID: 15488 RVA: 0x000F87C0 File Offset: 0x000F69C0
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

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06003C81 RID: 15489 RVA: 0x000F87C9 File Offset: 0x000F69C9
		// (set) Token: 0x06003C82 RID: 15490 RVA: 0x000F87D1 File Offset: 0x000F69D1
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

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003C83 RID: 15491 RVA: 0x000F87DA File Offset: 0x000F69DA
		// (set) Token: 0x06003C84 RID: 15492 RVA: 0x000F87E2 File Offset: 0x000F69E2
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

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003C85 RID: 15493 RVA: 0x000F87EB File Offset: 0x000F69EB
		// (set) Token: 0x06003C86 RID: 15494 RVA: 0x000F87F3 File Offset: 0x000F69F3
		public HeaderPromotionMode HeaderPromotion
		{
			get
			{
				return this.headerPromotionMode;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<HeaderPromotionMode>(value);
				this.headerPromotionMode = value;
			}
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x000F8804 File Offset: 0x000F6A04
		internal PropertyBagSaveFlags GetSaveFlags(bool isTopLevelMessage)
		{
			PropertyBagSaveFlags propertyBagSaveFlags = PropertyBagSaveFlags.Default;
			HeaderPromotionMode headerPromotionMode = isTopLevelMessage ? this.HeaderPromotion : HeaderPromotionMode.NoCreate;
			if (headerPromotionMode == HeaderPromotionMode.MayCreate)
			{
				propertyBagSaveFlags |= PropertyBagSaveFlags.IgnoreUnresolvedHeaders;
			}
			else if (headerPromotionMode == HeaderPromotionMode.NoCreate)
			{
				propertyBagSaveFlags |= (PropertyBagSaveFlags.IgnoreUnresolvedHeaders | PropertyBagSaveFlags.DisableNewXHeaderMapping);
			}
			return propertyBagSaveFlags;
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x000F8833 File Offset: 0x000F6A33
		// (set) Token: 0x06003C89 RID: 15497 RVA: 0x000F883B File Offset: 0x000F6A3B
		public bool TreatInlineDispositionAsAttachment
		{
			get
			{
				return this.treatInlineDispositionAsAttachment;
			}
			set
			{
				this.treatInlineDispositionAsAttachment = value;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06003C8A RID: 15498 RVA: 0x000F8844 File Offset: 0x000F6A44
		// (set) Token: 0x06003C8B RID: 15499 RVA: 0x000F884C File Offset: 0x000F6A4C
		public bool ApplyTrustToAttachedMessages
		{
			get
			{
				return this.applyTrustToAttachedMessages;
			}
			set
			{
				this.applyTrustToAttachedMessages = value;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x000F8855 File Offset: 0x000F6A55
		// (set) Token: 0x06003C8D RID: 15501 RVA: 0x000F885D File Offset: 0x000F6A5D
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

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06003C8E RID: 15502 RVA: 0x000F8866 File Offset: 0x000F6A66
		// (set) Token: 0x06003C8F RID: 15503 RVA: 0x000F886E File Offset: 0x000F6A6E
		public bool ConvertReportToMessage
		{
			get
			{
				return this.convertReportToMessage;
			}
			set
			{
				this.convertReportToMessage = value;
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06003C90 RID: 15504 RVA: 0x000F8877 File Offset: 0x000F6A77
		// (set) Token: 0x06003C91 RID: 15505 RVA: 0x000F887F File Offset: 0x000F6A7F
		public ConversionLimits Limits
		{
			get
			{
				return this.limits;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.limits = value;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x000F8891 File Offset: 0x000F6A91
		// (set) Token: 0x06003C93 RID: 15507 RVA: 0x000F8899 File Offset: 0x000F6A99
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

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06003C94 RID: 15508 RVA: 0x000F88A2 File Offset: 0x000F6AA2
		// (set) Token: 0x06003C95 RID: 15509 RVA: 0x000F88AA File Offset: 0x000F6AAA
		public bool ClientSubmittedSecurely
		{
			get
			{
				return this.clientSubmittedSecurely;
			}
			set
			{
				this.clientSubmittedSecurely = value;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06003C96 RID: 15510 RVA: 0x000F88B3 File Offset: 0x000F6AB3
		// (set) Token: 0x06003C97 RID: 15511 RVA: 0x000F88BB File Offset: 0x000F6ABB
		public bool ServerSubmittedSecurely
		{
			get
			{
				return this.serverSubmittedSecurely;
			}
			set
			{
				this.serverSubmittedSecurely = value;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003C98 RID: 15512 RVA: 0x000F88C4 File Offset: 0x000F6AC4
		// (set) Token: 0x06003C99 RID: 15513 RVA: 0x000F88CC File Offset: 0x000F6ACC
		public bool ApplyHeaderFirewall
		{
			get
			{
				return this.applyHeaderFirewall;
			}
			set
			{
				this.applyHeaderFirewall = value;
			}
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000F88D5 File Offset: 0x000F6AD5
		public object Clone()
		{
			return new InboundConversionOptions(this);
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000F88E0 File Offset: 0x000F6AE0
		public void LoadPerOrganizationCharsetDetectionOptions(OrganizationId organizationId)
		{
			OrganizationContentConversionProperties organizationContentConversionProperties;
			if (InboundConversionOptions.directoryAccessor.TryGetOrganizationContentConversionProperties(organizationId, out organizationContentConversionProperties))
			{
				this.detectionOptions.PreferredInternetCodePageForShiftJis = organizationContentConversionProperties.PreferredInternetCodePageForShiftJis;
				this.detectionOptions.RequiredCoverage = organizationContentConversionProperties.RequiredCharsetCoverage;
			}
		}

		// Token: 0x04002018 RID: 8216
		public const int MaxParticipantDisplayNameLength = 512;

		// Token: 0x04002019 RID: 8217
		internal static readonly string NoScopedTenantInfoNotice = "No IRecipientSession or IADRecipientCache has been supplied into the [Inbound/Outbound]ConversionOptions object you used. This is now required to properly scope recipient lookups.";

		// Token: 0x0400201A RID: 8218
		private string logDirectoryPath;

		// Token: 0x0400201B RID: 8219
		private Charset defaultCharset = Culture.Default.MimeCharset;

		// Token: 0x0400201C RID: 8220
		private bool trustAsciiCharsets = true;

		// Token: 0x0400201D RID: 8221
		private bool isSenderTrusted;

		// Token: 0x0400201E RID: 8222
		private string imceaResolvableDomain;

		// Token: 0x0400201F RID: 8223
		private bool preserveReportBody;

		// Token: 0x04002020 RID: 8224
		private bool clearCategories = true;

		// Token: 0x04002021 RID: 8225
		private IRecipientSession userADSession;

		// Token: 0x04002022 RID: 8226
		private IADRecipientCache recipientCache;

		// Token: 0x04002023 RID: 8227
		private ConversionLimits limits = new ConversionLimits(true);

		// Token: 0x04002024 RID: 8228
		private bool clientSubmittedSecurely;

		// Token: 0x04002025 RID: 8229
		private bool serverSubmittedSecurely;

		// Token: 0x04002026 RID: 8230
		private DsnHumanReadableWriter dsnWriter;

		// Token: 0x04002027 RID: 8231
		private HeaderPromotionMode headerPromotionMode;

		// Token: 0x04002028 RID: 8232
		private CharsetDetectionOptions detectionOptions = new CharsetDetectionOptions();

		// Token: 0x04002029 RID: 8233
		private bool convertReportToMessage;

		// Token: 0x0400202A RID: 8234
		private bool treatInlineDispositionAsAttachment;

		// Token: 0x0400202B RID: 8235
		private bool applyTrustToAttachedMessages = true;

		// Token: 0x0400202C RID: 8236
		private bool resolveRecipientsInAttachedMessages = true;

		// Token: 0x0400202D RID: 8237
		private bool applyHeaderFirewall;

		// Token: 0x0400202E RID: 8238
		private bool ignoreImceaDomain;

		// Token: 0x0400202F RID: 8239
		private static readonly IDirectoryAccessor directoryAccessor = new DirectoryAccessor();
	}
}
