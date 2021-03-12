using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000401 RID: 1025
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class DomainContentConfig : ADLegacyVersionableObject
	{
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x000BD368 File Offset: 0x000BB568
		// (set) Token: 0x06002E71 RID: 11889 RVA: 0x000BD37A File Offset: 0x000BB57A
		public SmtpDomainWithSubdomains DomainName
		{
			get
			{
				return (SmtpDomainWithSubdomains)this[EdgeDomainContentConfigSchema.DomainName];
			}
			set
			{
				this[EdgeDomainContentConfigSchema.DomainName] = value;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x000BD388 File Offset: 0x000BB588
		// (set) Token: 0x06002E73 RID: 11891 RVA: 0x000BD39A File Offset: 0x000BB59A
		[Parameter]
		public bool IsInternal
		{
			get
			{
				return (bool)this[DomainContentConfigSchema.InternalDomain];
			}
			set
			{
				this[DomainContentConfigSchema.InternalDomain] = value;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x000BD3AD File Offset: 0x000BB5AD
		// (set) Token: 0x06002E75 RID: 11893 RVA: 0x000BD3BF File Offset: 0x000BB5BF
		public bool TargetDeliveryDomain
		{
			get
			{
				return (bool)this[DomainContentConfigSchema.TargetDeliveryDomain];
			}
			set
			{
				this[DomainContentConfigSchema.TargetDeliveryDomain] = value;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000BD3D2 File Offset: 0x000BB5D2
		// (set) Token: 0x06002E77 RID: 11895 RVA: 0x000BD3E4 File Offset: 0x000BB5E4
		[Parameter(Mandatory = false)]
		public ByteEncoderTypeFor7BitCharsetsEnum ByteEncoderTypeFor7BitCharsets
		{
			get
			{
				return (ByteEncoderTypeFor7BitCharsetsEnum)this[DomainContentConfigSchema.ByteEncoderTypeFor7BitCharsets];
			}
			set
			{
				this[DomainContentConfigSchema.ByteEncoderTypeFor7BitCharsets] = value;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x000BD3F7 File Offset: 0x000BB5F7
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x000BD409 File Offset: 0x000BB609
		[Parameter]
		public string CharacterSet
		{
			get
			{
				return (string)this[DomainContentConfigSchema.CharacterSet];
			}
			set
			{
				this[DomainContentConfigSchema.CharacterSet] = value;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x000BD417 File Offset: 0x000BB617
		// (set) Token: 0x06002E7B RID: 11899 RVA: 0x000BD429 File Offset: 0x000BB629
		[Parameter]
		public string NonMimeCharacterSet
		{
			get
			{
				return (string)this[EdgeDomainContentConfigSchema.NonMimeCharacterSet];
			}
			set
			{
				this[EdgeDomainContentConfigSchema.NonMimeCharacterSet] = value;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x000BD437 File Offset: 0x000BB637
		// (set) Token: 0x06002E7D RID: 11901 RVA: 0x000BD449 File Offset: 0x000BB649
		[Parameter]
		public AllowedOOFType AllowedOOFType
		{
			get
			{
				return (AllowedOOFType)this[DomainContentConfigSchema.AllowedOOFType];
			}
			set
			{
				this[DomainContentConfigSchema.AllowedOOFType] = value;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x000BD45C File Offset: 0x000BB65C
		// (set) Token: 0x06002E7F RID: 11903 RVA: 0x000BD46E File Offset: 0x000BB66E
		[Parameter]
		public bool AutoReplyEnabled
		{
			get
			{
				return (bool)this[DomainContentConfigSchema.AutoReplyEnabled];
			}
			set
			{
				this[DomainContentConfigSchema.AutoReplyEnabled] = value;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000BD481 File Offset: 0x000BB681
		// (set) Token: 0x06002E81 RID: 11905 RVA: 0x000BD493 File Offset: 0x000BB693
		[Parameter]
		public bool AutoForwardEnabled
		{
			get
			{
				return (bool)this[DomainContentConfigSchema.AutoForwardEnabled];
			}
			set
			{
				this[DomainContentConfigSchema.AutoForwardEnabled] = value;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x000BD4A6 File Offset: 0x000BB6A6
		// (set) Token: 0x06002E83 RID: 11907 RVA: 0x000BD4B8 File Offset: 0x000BB6B8
		[Parameter]
		public bool DeliveryReportEnabled
		{
			get
			{
				return (bool)this[DomainContentConfigSchema.DeliveryReportEnabled];
			}
			set
			{
				this[DomainContentConfigSchema.DeliveryReportEnabled] = value;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x000BD4CB File Offset: 0x000BB6CB
		// (set) Token: 0x06002E85 RID: 11909 RVA: 0x000BD4DD File Offset: 0x000BB6DD
		[Parameter]
		public bool NDREnabled
		{
			get
			{
				return (bool)this[DomainContentConfigSchema.NDREnabled];
			}
			set
			{
				this[DomainContentConfigSchema.NDREnabled] = value;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06002E86 RID: 11910 RVA: 0x000BD4F0 File Offset: 0x000BB6F0
		// (set) Token: 0x06002E87 RID: 11911 RVA: 0x000BD502 File Offset: 0x000BB702
		[Parameter]
		public bool MeetingForwardNotificationEnabled
		{
			get
			{
				return (bool)this[DomainContentConfigSchema.MFNEnabled];
			}
			set
			{
				this[DomainContentConfigSchema.MFNEnabled] = value;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000BD515 File Offset: 0x000BB715
		// (set) Token: 0x06002E89 RID: 11913 RVA: 0x000BD527 File Offset: 0x000BB727
		[Parameter]
		public ContentType ContentType
		{
			get
			{
				return (ContentType)this[DomainContentConfigSchema.ContentType];
			}
			set
			{
				this[DomainContentConfigSchema.ContentType] = value;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000BD53A File Offset: 0x000BB73A
		// (set) Token: 0x06002E8B RID: 11915 RVA: 0x000BD54C File Offset: 0x000BB74C
		[Parameter]
		public bool DisplaySenderName
		{
			get
			{
				return (bool)this[DomainContentConfigSchema.DisplaySenderName];
			}
			set
			{
				this[DomainContentConfigSchema.DisplaySenderName] = value;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06002E8C RID: 11916 RVA: 0x000BD55F File Offset: 0x000BB75F
		// (set) Token: 0x06002E8D RID: 11917 RVA: 0x000BD571 File Offset: 0x000BB771
		[Parameter(Mandatory = false)]
		public PreferredInternetCodePageForShiftJisEnum PreferredInternetCodePageForShiftJis
		{
			get
			{
				return (PreferredInternetCodePageForShiftJisEnum)this[DomainContentConfigSchema.PreferredInternetCodePageForShiftJis];
			}
			set
			{
				this[DomainContentConfigSchema.PreferredInternetCodePageForShiftJis] = value;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000BD584 File Offset: 0x000BB784
		// (set) Token: 0x06002E8F RID: 11919 RVA: 0x000BD596 File Offset: 0x000BB796
		[Parameter(Mandatory = false)]
		public int? RequiredCharsetCoverage
		{
			get
			{
				return (int?)this[DomainContentConfigSchema.RequiredCharsetCoverage];
			}
			set
			{
				this[DomainContentConfigSchema.RequiredCharsetCoverage] = value;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x000BD5A9 File Offset: 0x000BB7A9
		// (set) Token: 0x06002E91 RID: 11921 RVA: 0x000BD5BB File Offset: 0x000BB7BB
		[Parameter]
		public bool? TNEFEnabled
		{
			get
			{
				return (bool?)this[DomainContentConfigSchema.TNEFEnabled];
			}
			set
			{
				this[DomainContentConfigSchema.TNEFEnabled] = value;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x000BD5CE File Offset: 0x000BB7CE
		// (set) Token: 0x06002E93 RID: 11923 RVA: 0x000BD5E0 File Offset: 0x000BB7E0
		[Parameter]
		public Unlimited<int> LineWrapSize
		{
			get
			{
				return (Unlimited<int>)this[DomainContentConfigSchema.LineWrapSize];
			}
			set
			{
				this[DomainContentConfigSchema.LineWrapSize] = value;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x000BD5F3 File Offset: 0x000BB7F3
		// (set) Token: 0x06002E95 RID: 11925 RVA: 0x000BD605 File Offset: 0x000BB805
		[Parameter]
		public bool TrustedMailOutboundEnabled
		{
			get
			{
				return (bool)this[EdgeDomainContentConfigSchema.TrustedMailOutboundEnabled];
			}
			set
			{
				this[EdgeDomainContentConfigSchema.TrustedMailOutboundEnabled] = value;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x000BD618 File Offset: 0x000BB818
		// (set) Token: 0x06002E97 RID: 11927 RVA: 0x000BD62A File Offset: 0x000BB82A
		[Parameter]
		public bool TrustedMailInboundEnabled
		{
			get
			{
				return (bool)this[EdgeDomainContentConfigSchema.TrustedMailInboundEnabled];
			}
			set
			{
				this[EdgeDomainContentConfigSchema.TrustedMailInboundEnabled] = value;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x000BD63D File Offset: 0x000BB83D
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x000BD64F File Offset: 0x000BB84F
		[Parameter]
		public bool UseSimpleDisplayName
		{
			get
			{
				return (bool)this[DomainContentConfigSchema.UseSimpleDisplayName];
			}
			set
			{
				this[DomainContentConfigSchema.UseSimpleDisplayName] = value;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x000BD662 File Offset: 0x000BB862
		// (set) Token: 0x06002E9B RID: 11931 RVA: 0x000BD677 File Offset: 0x000BB877
		[Parameter]
		public bool NDRDiagnosticInfoEnabled
		{
			get
			{
				return !(bool)this[DomainContentConfigSchema.NDRDiagnosticInfoDisabled];
			}
			set
			{
				this[DomainContentConfigSchema.NDRDiagnosticInfoDisabled] = !value;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x000BD68D File Offset: 0x000BB88D
		// (set) Token: 0x06002E9D RID: 11933 RVA: 0x000BD69F File Offset: 0x000BB89F
		[Parameter(Mandatory = false)]
		public int MessageCountThreshold
		{
			get
			{
				return (int)this[DomainContentConfigSchema.MessageCountThreshold];
			}
			set
			{
				this[DomainContentConfigSchema.MessageCountThreshold] = value;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x000BD6B2 File Offset: 0x000BB8B2
		internal override ADObjectSchema Schema
		{
			get
			{
				if (TopologyProvider.IsAdamTopology())
				{
					return DomainContentConfig.edgeSchema;
				}
				return DomainContentConfig.schema;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000BD6C6 File Offset: 0x000BB8C6
		internal override ADObjectId ParentPath
		{
			get
			{
				return DomainContentConfig.RootId;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002EA0 RID: 11936 RVA: 0x000BD6CD File Offset: 0x000BB8CD
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchDomainContentConfig";
			}
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000BD6D4 File Offset: 0x000BB8D4
		internal static object AllowedOOFTypeGetter(IPropertyBag propertyBag)
		{
			AcceptMessageType acceptMessageType = (AcceptMessageType)((int)propertyBag[DomainContentConfigSchema.AcceptMessageTypes]);
			acceptMessageType &= (AcceptMessageType.LegacyOOF | AcceptMessageType.BlockOOF | AcceptMessageType.InternalDomain);
			AcceptMessageType acceptMessageType2 = acceptMessageType;
			switch (acceptMessageType2)
			{
			case AcceptMessageType.Default:
				return AllowedOOFType.External;
			case AcceptMessageType.LegacyOOF:
				return AllowedOOFType.ExternalLegacy;
			default:
				if (acceptMessageType2 == AcceptMessageType.BlockOOF)
				{
					return AllowedOOFType.None;
				}
				if (acceptMessageType2 != (AcceptMessageType.LegacyOOF | AcceptMessageType.InternalDomain))
				{
					return AllowedOOFType.External;
				}
				return AllowedOOFType.InternalLegacy;
			}
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000BD738 File Offset: 0x000BB938
		internal static void AllowedOOFTypeSetter(object value, IPropertyBag propertyBag)
		{
			AcceptMessageType acceptMessageType = (AcceptMessageType)((int)propertyBag[DomainContentConfigSchema.AcceptMessageTypes]);
			acceptMessageType &= ~(AcceptMessageType.LegacyOOF | AcceptMessageType.BlockOOF | AcceptMessageType.InternalDomain);
			AllowedOOFType allowedOOFType = (AllowedOOFType)value;
			acceptMessageType |= (AcceptMessageType)allowedOOFType;
			propertyBag[DomainContentConfigSchema.AcceptMessageTypes] = (int)acceptMessageType;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000BD778 File Offset: 0x000BB978
		internal static object NonMimeCharacterSetGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[EdgeDomainContentConfigSchema.ADNonMimeCharacterSet];
			if (!DomainContentConfig.IsValidCharset(text))
			{
				text = DomainContentConfig.MapCharset(text);
			}
			return text.ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000BD7B0 File Offset: 0x000BB9B0
		internal static void NonMimeCharacterSetSetter(object value, IPropertyBag propertyBag)
		{
			DomainContentConfig.ThrowOnInvalidCharset((string)value, EdgeDomainContentConfigSchema.NonMimeCharacterSet);
			propertyBag[EdgeDomainContentConfigSchema.ADNonMimeCharacterSet] = value;
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000BD7D0 File Offset: 0x000BB9D0
		internal static object CharacterSetGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[DomainContentConfigSchema.ADCharacterSet];
			if (!DomainContentConfig.IsValidCharset(text))
			{
				text = DomainContentConfig.MapCharset(text);
			}
			return text.ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000BD808 File Offset: 0x000BBA08
		internal static void CharacterSetSetter(object value, IPropertyBag propertyBag)
		{
			DomainContentConfig.ThrowOnInvalidCharset((string)value, DomainContentConfigSchema.CharacterSet);
			propertyBag[DomainContentConfigSchema.ADCharacterSet] = value;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000BD826 File Offset: 0x000BBA26
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.TargetDeliveryDomain && this.DomainName != null && this.DomainName.IsStar)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.TargetDeliveryDomainCannotBeStar, DomainContentConfigSchema.TargetDeliveryDomain, this));
			}
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000BD864 File Offset: 0x000BBA64
		private static bool IsValidCharset(string inputCharset)
		{
			foreach (DomainContentConfig.CharacterSetInfo characterSetInfo in DomainContentConfig.CharacterSetList)
			{
				if (string.Equals(inputCharset, characterSetInfo.CharsetName, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000BD8AC File Offset: 0x000BBAAC
		private static void ThrowOnInvalidCharset(string inputCharset, PropertyDefinition propertyDefinition)
		{
			if (!DomainContentConfig.IsValidCharset(inputCharset))
			{
				StringBuilder stringBuilder = new StringBuilder(512);
				for (int i = 0; i < DomainContentConfig.CharacterSetList.Length; i++)
				{
					if (!string.IsNullOrEmpty(DomainContentConfig.CharacterSetList[i].CharsetName))
					{
						stringBuilder.Append(DomainContentConfig.CharacterSetList[i].CharsetName);
						stringBuilder.Append(", ");
					}
				}
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.InvalidCharacterSet(inputCharset, stringBuilder.ToString()), propertyDefinition, inputCharset));
			}
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000BD944 File Offset: 0x000BBB44
		private static string MapCharset(string inputCharset)
		{
			foreach (DomainContentConfig.CharsetMapping charsetMapping in DomainContentConfig.charsetMappingList)
			{
				if (string.Equals(inputCharset, charsetMapping.LegacyCharsetName, StringComparison.OrdinalIgnoreCase))
				{
					return charsetMapping.CharsetName;
				}
			}
			return string.Empty;
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000BD993 File Offset: 0x000BBB93
		internal AcceptMessageType GetOOFSettings()
		{
			return (AcceptMessageType)((int)this[DomainContentConfigSchema.AcceptMessageTypes]);
		}

		// Token: 0x04001F69 RID: 8041
		public const string DefaultName = "Default";

		// Token: 0x04001F6A RID: 8042
		public const string Noun = "RemoteDomain";

		// Token: 0x04001F6B RID: 8043
		private const string MostDerivedClass = "msExchDomainContentConfig";

		// Token: 0x04001F6C RID: 8044
		private static readonly ADObjectId RootId = new ADObjectId("CN=Internet Message Formats,CN=Global Settings");

		// Token: 0x04001F6D RID: 8045
		private static readonly DomainContentConfigSchema schema = ObjectSchema.GetInstance<DomainContentConfigSchema>();

		// Token: 0x04001F6E RID: 8046
		private static readonly EdgeDomainContentConfigSchema edgeSchema = ObjectSchema.GetInstance<EdgeDomainContentConfigSchema>();

		// Token: 0x04001F6F RID: 8047
		private static DomainContentConfig.CharsetMapping[] charsetMappingList = new DomainContentConfig.CharsetMapping[]
		{
			new DomainContentConfig.CharsetMapping("ansi", string.Empty),
			new DomainContentConfig.CharsetMapping("iso-2022-jpdbcs", "iso-2022-jp"),
			new DomainContentConfig.CharsetMapping("iso-2022-jpesc", "iso-2022-jp"),
			new DomainContentConfig.CharsetMapping("iso-2022-jpsio", "iso-2022-jp"),
			new DomainContentConfig.CharsetMapping("us-ascii", string.Empty),
			new DomainContentConfig.CharsetMapping("x-euc", "euc-jp")
		};

		// Token: 0x04001F70 RID: 8048
		internal static DomainContentConfig.CharacterSetInfo[] CharacterSetList = new DomainContentConfig.CharacterSetInfo[]
		{
			new DomainContentConfig.CharacterSetInfo(string.Empty, "None", -1),
			new DomainContentConfig.CharacterSetInfo("big5", "Chinese Traditional (Big5)", 950),
			new DomainContentConfig.CharacterSetInfo("din_66003", "German (IA5)", 20106),
			new DomainContentConfig.CharacterSetInfo("euc-jp", "Japanese (EUC)", 51932),
			new DomainContentConfig.CharacterSetInfo("euc-kr", "Korean (EUC)", 51949),
			new DomainContentConfig.CharacterSetInfo("gb18030", "Chinese Simplified (GB18030)", 54936),
			new DomainContentConfig.CharacterSetInfo("gb2312", "Chinese Simplified (GB2312)", 936),
			new DomainContentConfig.CharacterSetInfo("hz-gb-2312", "Chinese Simplified (HZ)", 52936),
			new DomainContentConfig.CharacterSetInfo("iso-2022-jp", "Japanese (JIS)", 50220),
			new DomainContentConfig.CharacterSetInfo("iso-2022-kr", "Korean (ISO)", 50225),
			new DomainContentConfig.CharacterSetInfo("iso-8859-1", "Western European (ISO)", 28591),
			new DomainContentConfig.CharacterSetInfo("iso-8859-13", "Estonian (ISO)", 28603),
			new DomainContentConfig.CharacterSetInfo("iso-8859-15", "Latin 9 (ISO)", 28605),
			new DomainContentConfig.CharacterSetInfo("iso-8859-2", "Central European (ISO)", 28592),
			new DomainContentConfig.CharacterSetInfo("iso-8859-3", "Latin 3 (ISO)", 28593),
			new DomainContentConfig.CharacterSetInfo("iso-8859-4", "Baltic (ISO)", 28594),
			new DomainContentConfig.CharacterSetInfo("iso-8859-5", "Cyrillic (ISO)", 28595),
			new DomainContentConfig.CharacterSetInfo("iso-8859-6", "Arabic (ISO)", 28596),
			new DomainContentConfig.CharacterSetInfo("iso-8859-7", "Greek (ISO)", 28597),
			new DomainContentConfig.CharacterSetInfo("iso-8859-8", "Hebrew (ISO)", 28598),
			new DomainContentConfig.CharacterSetInfo("iso-8859-9", "Turkish (ISO)", 28599),
			new DomainContentConfig.CharacterSetInfo("koi8-r", "Cyrillic (KOI8-R)", 20866),
			new DomainContentConfig.CharacterSetInfo("koi8-u", "Cyrillic (KOI8-U)", 21866),
			new DomainContentConfig.CharacterSetInfo("ks_c_5601-1987", "Korean (Windows)", 949),
			new DomainContentConfig.CharacterSetInfo("ns_4551-1", "Norwegian (IA5)", 20108),
			new DomainContentConfig.CharacterSetInfo("sen_850200_b", "Swedish (IA5)", 20107),
			new DomainContentConfig.CharacterSetInfo("shift_jis", "Japanese (Shift-JIS)", 932),
			new DomainContentConfig.CharacterSetInfo("utf-7", "Unicode (UTF-7)", 65000),
			new DomainContentConfig.CharacterSetInfo("utf-8", "Unicode (UTF-8)", 65001),
			new DomainContentConfig.CharacterSetInfo("windows-1250", "Central European (Windows)", 1250),
			new DomainContentConfig.CharacterSetInfo("windows-1251", "Cyrillic (Windows)", 1251),
			new DomainContentConfig.CharacterSetInfo("windows-1252", "Western European (Windows)", 1252),
			new DomainContentConfig.CharacterSetInfo("windows-1253", "Greek (Windows)", 1253),
			new DomainContentConfig.CharacterSetInfo("windows-1254", "Turkish (Windows)", 1254),
			new DomainContentConfig.CharacterSetInfo("windows-1255", "Hebrew (Windows)", 1255),
			new DomainContentConfig.CharacterSetInfo("windows-1256", "Arabic (Windows)", 1256),
			new DomainContentConfig.CharacterSetInfo("windows-1257", "Baltic (Windows)", 1257),
			new DomainContentConfig.CharacterSetInfo("windows-1258", "Vietnamese (Windows)", 1258),
			new DomainContentConfig.CharacterSetInfo("windows-874", "Thai (Windows)", 874)
		};

		// Token: 0x02000402 RID: 1026
		private struct CharsetMapping
		{
			// Token: 0x06002EAE RID: 11950 RVA: 0x000BDF97 File Offset: 0x000BC197
			public CharsetMapping(string legacyCharsetName, string charsetName)
			{
				this.LegacyCharsetName = legacyCharsetName;
				this.CharsetName = charsetName;
			}

			// Token: 0x04001F71 RID: 8049
			public string LegacyCharsetName;

			// Token: 0x04001F72 RID: 8050
			public string CharsetName;
		}

		// Token: 0x02000403 RID: 1027
		internal struct CharacterSetInfo
		{
			// Token: 0x06002EAF RID: 11951 RVA: 0x000BDFA7 File Offset: 0x000BC1A7
			public CharacterSetInfo(string charsetName, string charsetDesp, int codePage)
			{
				this.CharsetName = charsetName;
				this.CharsetDescription = charsetDesp;
				this.CodePage = codePage;
			}

			// Token: 0x17000D33 RID: 3379
			// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000BDFBE File Offset: 0x000BC1BE
			public string CharacterSetName
			{
				get
				{
					return this.CharsetName;
				}
			}

			// Token: 0x17000D34 RID: 3380
			// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000BDFC6 File Offset: 0x000BC1C6
			public string CharacterSetDescription
			{
				get
				{
					return this.CharsetDescription;
				}
			}

			// Token: 0x04001F73 RID: 8051
			public string CharsetName;

			// Token: 0x04001F74 RID: 8052
			public string CharsetDescription;

			// Token: 0x04001F75 RID: 8053
			public int CodePage;
		}
	}
}
