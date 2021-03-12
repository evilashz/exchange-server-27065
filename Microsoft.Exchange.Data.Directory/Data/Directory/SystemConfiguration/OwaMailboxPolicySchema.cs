using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000536 RID: 1334
	internal sealed class OwaMailboxPolicySchema : MailboxPolicySchema
	{
		// Token: 0x06003B50 RID: 15184 RVA: 0x000E25F8 File Offset: 0x000E07F8
		internal static ADPropertyDefinition AllowOfflineOnProperty(string name, AllowOfflineOnEnum defaultValue, ADPropertyDefinition fieldProperty)
		{
			GetterDelegate getterDelegate = delegate(IPropertyBag bag)
			{
				object obj = null;
				if (!(bag as ADPropertyBag).TryGetField(fieldProperty, ref obj))
				{
					return defaultValue;
				}
				if (obj == null)
				{
					return defaultValue;
				}
				int num = (int)obj & 384;
				int num2 = num;
				AllowOfflineOnEnum allowOfflineOnEnum;
				if (num2 != 128)
				{
					if (num2 != 256)
					{
						if (num2 != 384)
						{
						}
						allowOfflineOnEnum = AllowOfflineOnEnum.AllComputers;
					}
					else
					{
						allowOfflineOnEnum = AllowOfflineOnEnum.NoComputers;
					}
				}
				else
				{
					allowOfflineOnEnum = AllowOfflineOnEnum.PrivateComputersOnly;
				}
				return allowOfflineOnEnum;
			};
			SetterDelegate setterDelegate = OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(fieldProperty, name, delegate(object value, IPropertyBag bag)
			{
				int num = (int)value;
				int num2 = (int)bag[fieldProperty];
				num2 &= -385;
				switch (num)
				{
				case 1:
					num2 |= 128;
					break;
				case 2:
					num2 |= 256;
					break;
				case 3:
					num2 |= 384;
					break;
				default:
					throw new DataValidationException(new PropertyValidationError(DataStrings.BadEnumValue(typeof(AllowOfflineOnEnum)), fieldProperty, value));
				}
				bag[fieldProperty] = num2;
			});
			return new ADPropertyDefinition(name, fieldProperty.VersionAdded, typeof(AllowOfflineOnEnum), null, ADPropertyDefinitionFlags.Calculated, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
			{
				fieldProperty
			}, null, getterDelegate, setterDelegate, null, null);
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000E2687 File Offset: 0x000E0887
		private static SetterDelegate FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(int mask, PropertyDefinition propertyDefinition, string description)
		{
			return OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(mask, propertyDefinition, description, false);
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000E26FC File Offset: 0x000E08FC
		private static SetterDelegate FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(int mask, PropertyDefinition propertyDefinition, string description, bool invert)
		{
			return OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(propertyDefinition, description, delegate(object value, IPropertyBag bag)
			{
				bool flag = invert ? (!(bool)value) : ((bool)value);
				int num = (int)bag[propertyDefinition];
				bag[propertyDefinition] = (flag ? (num | mask) : (num & ~mask));
			});
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x000E27BC File Offset: 0x000E09BC
		private static SetterDelegate FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(PropertyDefinition propertyDefinition, string description, SetterDelegate callback)
		{
			return delegate(object value, IPropertyBag bag)
			{
				if (!((ExchangeObjectVersion)bag[ADObjectSchema.ExchangeVersion]).Equals(ExchangeObjectVersion.Exchange2010))
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyMailboxPolicy(description), propertyDefinition, value), null);
				}
				if (value != null)
				{
					callback(value, bag);
					return;
				}
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnE14MailboxPolicyToNull(description), propertyDefinition, value), null);
			};
		}

		// Token: 0x04002822 RID: 10274
		private const string FileExtensionRegularExpression = "^\\.([^.]+)$";

		// Token: 0x04002823 RID: 10275
		internal static readonly string[] DefaultWebReadyFileTypes = new string[]
		{
			".doc",
			".dot",
			".rtf",
			".xls",
			".ppt",
			".pps",
			".pdf",
			".docx",
			".xlsx",
			".pptx"
		};

		// Token: 0x04002824 RID: 10276
		internal static readonly string[] DefaultWebReadyMimeTypes = new string[]
		{
			"application/msword",
			"application/vnd.ms-excel",
			"application/x-msexcel",
			"application/vnd.ms-powerpoint",
			"application/x-mspowerpoint",
			"application/pdf",
			"application/vnd.openxmlformats-officedocument.wordprocessingml.document",
			"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
			"application/vnd.openxmlformats-officedocument.presentationml.presentation"
		};

		// Token: 0x04002825 RID: 10277
		internal static readonly string[] DefaultAllowedFileTypes = new string[]
		{
			".jpg",
			".gif",
			".bmp",
			".png",
			".tif",
			".tiff",
			".pdf",
			".txt",
			".rtf",
			".wav",
			".mp3",
			".avi",
			".wmv",
			".wma",
			".doc",
			".xls",
			".ppt",
			".vsd",
			".vss",
			".vst",
			".vdx",
			".vsx",
			".vtx",
			".vsdx",
			".vssx",
			".vstx",
			".vsdm",
			".vssm",
			".vstm",
			".one",
			".pub",
			".rpmsg",
			".docx",
			".docm",
			".xlsx",
			".xlsm",
			".xlsb",
			".pptx",
			".pptm",
			".ppsx",
			".ppsm",
			".zip"
		};

		// Token: 0x04002826 RID: 10278
		internal static readonly string[] DefaultAllowedMimeTypes = new string[]
		{
			"image/bmp",
			"image/gif",
			"image/jpeg",
			"image/png"
		};

		// Token: 0x04002827 RID: 10279
		internal static readonly string[] DefaultForceSaveFileTypes = new string[]
		{
			".dir",
			".dcr",
			".spl",
			".swf",
			".htm",
			".html"
		};

		// Token: 0x04002828 RID: 10280
		internal static readonly string[] DefaultForceSaveMimeTypes = new string[]
		{
			"Application/octet-stream",
			"Application/x-shockwave-flash",
			"Application/futuresplash",
			"Application/x-director",
			"text/html"
		};

		// Token: 0x04002829 RID: 10281
		internal static readonly string[] DefaultBlockedFileTypes = new string[]
		{
			".ade",
			".adp",
			".app",
			".asp",
			".aspx",
			".asx",
			".bas",
			".bat",
			".cer",
			".chm",
			".cmd",
			".cnt",
			".com",
			".cpl",
			".crt",
			".csh",
			".der",
			".exe",
			".fxp",
			".gadget",
			".hlp",
			".hpj",
			".hta",
			".htc",
			".inf",
			".ins",
			".isp",
			".its",
			".js",
			".jse",
			".ksh",
			".lnk",
			".mad",
			".maf",
			".mag",
			".mam",
			".maq",
			".mar",
			".mas",
			".mat",
			".mau",
			".mav",
			".maw",
			".mda",
			".mdb",
			".mde",
			".mdt",
			".mdw",
			".mdz",
			".mht",
			".mhtml",
			".msc",
			".msh",
			".msh1",
			".msh1xml",
			".msh2",
			".msh2xml",
			".mshxml",
			".msi",
			".msp",
			".mst",
			".ops",
			".osd",
			".pcd",
			".pif",
			".plg",
			".prf",
			".prg",
			".ps1",
			".ps1xml",
			".ps2",
			".ps2xml",
			".psc1",
			".psc2",
			".pst",
			".reg",
			".scf",
			".scr",
			".sct",
			".shb",
			".shs",
			".tmp",
			".url",
			".vb",
			".vbe",
			".vbp",
			".vbs",
			".vsmacros",
			".vsw",
			".ws",
			".wsc",
			".wsf",
			".wsh",
			".xml"
		};

		// Token: 0x0400282A RID: 10282
		internal static readonly string[] DefaultBlockedMimeTypes = new string[]
		{
			"application/hta",
			"x-internet-signup",
			"application/javascript",
			"application/x-javascript",
			"text/javascript",
			"application/msaccess",
			"application/prg",
			"application/xml",
			"text/scriplet",
			"text/xml"
		};

		// Token: 0x0400282B RID: 10283
		internal static readonly bool DefaultForceSaveAttachmentFilteringEnabled = false;

		// Token: 0x0400282C RID: 10284
		internal static readonly bool DefaultSilverlightEnabled = true;

		// Token: 0x0400282D RID: 10285
		internal static readonly bool DefaultPlacesEnabled = Datacenter.IsMicrosoftHostedOnly(true);

		// Token: 0x0400282E RID: 10286
		internal static readonly bool DefaultWeatherEnabled = Datacenter.IsMicrosoftHostedOnly(true);

		// Token: 0x0400282F RID: 10287
		internal static readonly bool DefaultSenderPhotoEnabled = true;

		// Token: 0x04002830 RID: 10288
		internal static readonly bool DefaultPredictedActionsEnabled = false;

		// Token: 0x04002831 RID: 10289
		public static readonly ADPropertyDefinition FileAccessControlOnPublicComputers = new ADPropertyDefinition("FileAccessControlOnPublicComputers", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOWAFileAccessControlOnPublicComputers", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002832 RID: 10290
		public static readonly ADPropertyDefinition FileAccessControlOnPrivateComputers = new ADPropertyDefinition("FileAccessControlOnPrivateComputers", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOWAFileAccessControlOnPrivateComputers", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002833 RID: 10291
		public static readonly ADPropertyDefinition ActionForUnknownFileAndMIMETypes = new ADPropertyDefinition("ActionForUnknownFileAndMIMETypes", ExchangeObjectVersion.Exchange2010, typeof(AttachmentBlockingActions), "msExchOWAActionForUnknownFileAndMIMETypes", ADPropertyDefinitionFlags.None, AttachmentBlockingActions.ForceSave, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002834 RID: 10292
		internal static readonly ADPropertyDefinition ADWebReadyFileTypes = SharedPropertyDefinitions.ADWebReadyFileTypes;

		// Token: 0x04002835 RID: 10293
		public static readonly ADPropertyDefinition WebReadyFileTypes = new ADPropertyDefinition("WebReadyFileTypes", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new InStringArrayConstraint(OwaMailboxPolicySchema.DefaultWebReadyFileTypes, true)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADWebReadyFileTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(OwaMailboxPolicy.WebReadyFileTypesGetter), new SetterDelegate(OwaMailboxPolicy.WebReadyFileTypesSetter), null, null);

		// Token: 0x04002836 RID: 10294
		internal static readonly ADPropertyDefinition ADWebReadyMimeTypes = SharedPropertyDefinitions.ADWebReadyMimeTypes;

		// Token: 0x04002837 RID: 10295
		public static readonly ADPropertyDefinition WebReadyMimeTypes = new ADPropertyDefinition("WebReadyMimeTypes", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new InStringArrayConstraint(OwaMailboxPolicySchema.DefaultWebReadyMimeTypes, true)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADWebReadyMimeTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(OwaMailboxPolicy.WebReadyMimeTypesGetter), new SetterDelegate(OwaMailboxPolicy.WebReadyMimeTypesSetter), null, null);

		// Token: 0x04002838 RID: 10296
		internal static readonly ADPropertyDefinition ADWebReadyDocumentViewingFlags = new ADPropertyDefinition("ADWebReadyDocumentViewingFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOWATranscodingFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002839 RID: 10297
		public static readonly ADPropertyDefinition WebReadyDocumentViewingForAllSupportedTypes = new ADPropertyDefinition("WebReadyDocumentViewingForAllSupportedTypes", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADWebReadyDocumentViewingFlags
		}, null, ADObject.FlagGetterDelegate(1, OwaMailboxPolicySchema.ADWebReadyDocumentViewingFlags), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(1, OwaMailboxPolicySchema.ADWebReadyDocumentViewingFlags, "WebReadyDocumentViewingForAllSupportedTypes"), null, null);

		// Token: 0x0400283A RID: 10298
		internal static readonly ADPropertyDefinition ADAllowedFileTypes = SharedPropertyDefinitions.ADAllowedFileTypes;

		// Token: 0x0400283B RID: 10299
		public static readonly ADPropertyDefinition AllowedFileTypes = new ADPropertyDefinition("AllowedFileTypes", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RegexConstraint("^\\.([^.]+)$", DataStrings.FileExtensionPatternDescription)
		}, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADAllowedFileTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(OwaMailboxPolicy.AllowedFileTypesGetter), new SetterDelegate(OwaMailboxPolicy.AllowedFileTypesSetter), null, null);

		// Token: 0x0400283C RID: 10300
		internal static readonly ADPropertyDefinition ADAllowedMimeTypes = SharedPropertyDefinitions.ADAllowedMimeTypes;

		// Token: 0x0400283D RID: 10301
		public static readonly ADPropertyDefinition AllowedMimeTypes = new ADPropertyDefinition("AllowedMimeTypes", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADAllowedMimeTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(OwaMailboxPolicy.AllowedMimeTypesGetter), new SetterDelegate(OwaMailboxPolicy.AllowedMimeTypesSetter), null, null);

		// Token: 0x0400283E RID: 10302
		internal static readonly ADPropertyDefinition ADForceSaveFileTypes = SharedPropertyDefinitions.ADForceSaveFileTypes;

		// Token: 0x0400283F RID: 10303
		public static readonly ADPropertyDefinition ForceSaveFileTypes = new ADPropertyDefinition("ForceSaveFileTypes", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RegexConstraint("^\\.([^.]+)$", DataStrings.FileExtensionPatternDescription)
		}, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADForceSaveFileTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(OwaMailboxPolicy.ForceSaveFileTypesGetter), new SetterDelegate(OwaMailboxPolicy.ForceSaveFileTypesSetter), null, null);

		// Token: 0x04002840 RID: 10304
		internal static readonly ADPropertyDefinition ADForceSaveMimeTypes = SharedPropertyDefinitions.ADForceSaveMimeTypes;

		// Token: 0x04002841 RID: 10305
		public static readonly ADPropertyDefinition ForceSaveMimeTypes = new ADPropertyDefinition("ForceSaveMimeTypes", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADForceSaveMimeTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(OwaMailboxPolicy.ForceSaveMimeTypesGetter), new SetterDelegate(OwaMailboxPolicy.ForceSaveMimeTypesSetter), null, null);

		// Token: 0x04002842 RID: 10306
		internal static readonly ADPropertyDefinition ADBlockedFileTypes = SharedPropertyDefinitions.ADBlockedFileTypes;

		// Token: 0x04002843 RID: 10307
		public static readonly ADPropertyDefinition BlockedFileTypes = new ADPropertyDefinition("BlockedFileTypes", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RegexConstraint("^\\.([^.]+)$", DataStrings.FileExtensionPatternDescription)
		}, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADBlockedFileTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(OwaMailboxPolicy.BlockedFileTypesGetter), new SetterDelegate(OwaMailboxPolicy.BlockedFileTypesSetter), null, null);

		// Token: 0x04002844 RID: 10308
		internal static readonly ADPropertyDefinition ADBlockedMimeTypes = SharedPropertyDefinitions.ADBlockedMimeTypes;

		// Token: 0x04002845 RID: 10309
		public static readonly ADPropertyDefinition BlockedMimeTypes = new ADPropertyDefinition("BlockedMimeTypes", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADBlockedMimeTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(OwaMailboxPolicy.BlockedMimeTypesGetter), new SetterDelegate(OwaMailboxPolicy.BlockedMimeTypesSetter), null, null);

		// Token: 0x04002846 RID: 10310
		public static readonly ADPropertyDefinition PhoneticSupportEnabled = new ADPropertyDefinition("PhoneticSupportEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchPhoneticSupport", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002847 RID: 10311
		public static readonly ADPropertyDefinition DefaultTheme = new ADPropertyDefinition("DefaultTheme", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchOWADefaultTheme", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002848 RID: 10312
		internal static readonly ADPropertyDefinition SetPhotoURL = new ADPropertyDefinition("SetPhotoURL", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchAggregationSubscriptionCredential", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002849 RID: 10313
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxPolicySchema.MailboxPolicyFlags
		}, new CustomFilterBuilderDelegate(OwaMailboxPolicy.IsDefaultFilterBuilder), ADObject.FlagGetterDelegate(MailboxPolicySchema.MailboxPolicyFlags, 1), ADObject.FlagSetterDelegate(MailboxPolicySchema.MailboxPolicyFlags, 1), null, null);

		// Token: 0x0400284A RID: 10314
		public static readonly ADPropertyDefinition DefaultClientLanguage = new ADPropertyDefinition("DefaultClientLanguage", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOWADefaultClientLanguage", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new OWASupportedLanguageConstraint()
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400284B RID: 10315
		public static readonly ADPropertyDefinition LogonAndErrorLanguage = new ADPropertyDefinition("LogonAndErrorLanguage", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOWALogonAndErrorLanguage", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new OWASupportedLanguageConstraint()
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400284C RID: 10316
		public static readonly ADPropertyDefinition UseGB18030 = new ADPropertyDefinition("UseGB18030", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOWAUseGB18030", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400284D RID: 10317
		public static readonly ADPropertyDefinition UseISO885915 = new ADPropertyDefinition("UseISO885915", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOWAUseISO885915", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400284E RID: 10318
		public static readonly ADPropertyDefinition OutboundCharset = new ADPropertyDefinition("OutboundCharset", ExchangeObjectVersion.Exchange2010, typeof(OutboundCharsetOptions), "msExchOWAOutboundCharset", ADPropertyDefinitionFlags.None, OutboundCharsetOptions.AutoDetect, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400284F RID: 10319
		public static readonly ADPropertyDefinition InstantMessagingType = new ADPropertyDefinition("InstantMessagingType", ExchangeObjectVersion.Exchange2010, typeof(InstantMessagingTypeOptions), "msExchOWAIMProviderType", ADPropertyDefinitionFlags.None, InstantMessagingTypeOptions.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002850 RID: 10320
		public static readonly ADPropertyDefinition ADMailboxFolderSet = new ADPropertyDefinition("ADMailboxFolderSet", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMailboxFolderSet", ADPropertyDefinitionFlags.PersistDefaultValue, -1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002851 RID: 10321
		public static readonly ADPropertyDefinition ADMailboxFolderSet2 = new ADPropertyDefinition("ADMailboxFolderSet2", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchMailboxFolderSet2", ADPropertyDefinitionFlags.PersistDefaultValue, -63578, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002852 RID: 10322
		public static readonly ADPropertyDefinition GlobalAddressListEnabled = new ADPropertyDefinition("GlobalAddressListEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(1, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(1, OwaMailboxPolicySchema.ADMailboxFolderSet, "GlobalAddressListEnabled"), null, null);

		// Token: 0x04002853 RID: 10323
		public static readonly ADPropertyDefinition OrganizationEnabled = new ADPropertyDefinition("OrganizationEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(128, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(128, OwaMailboxPolicySchema.ADMailboxFolderSet, "OrganizationEnabled"), null, null);

		// Token: 0x04002854 RID: 10324
		public static readonly ADPropertyDefinition ExplicitLogonEnabled = new ADPropertyDefinition("ExplicitLogonEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(8388608, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(8388608, OwaMailboxPolicySchema.ADMailboxFolderSet, "ExplicitLogonEnabled"), null, null);

		// Token: 0x04002855 RID: 10325
		public static readonly ADPropertyDefinition OWALightEnabled = new ADPropertyDefinition("OWALightEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(536870912, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(536870912, OwaMailboxPolicySchema.ADMailboxFolderSet, "OWALightEnabled"), null, null);

		// Token: 0x04002856 RID: 10326
		public static readonly ADPropertyDefinition DelegateAccessEnabled = new ADPropertyDefinition("DelegateAccessEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(1073741824, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(1073741824, OwaMailboxPolicySchema.ADMailboxFolderSet, "DelegateAccessEnabled"), null, null);

		// Token: 0x04002857 RID: 10327
		public static readonly ADPropertyDefinition IRMEnabled = new ADPropertyDefinition("IRMEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(int.MinValue, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(int.MinValue, OwaMailboxPolicySchema.ADMailboxFolderSet, "IRMEnabled"), null, null);

		// Token: 0x04002858 RID: 10328
		public static readonly ADPropertyDefinition CalendarEnabled = new ADPropertyDefinition("CalendarEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(2, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(2, OwaMailboxPolicySchema.ADMailboxFolderSet, "CalendarEnabled"), null, null);

		// Token: 0x04002859 RID: 10329
		public static readonly ADPropertyDefinition ContactsEnabled = new ADPropertyDefinition("ContactsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(4, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(4, OwaMailboxPolicySchema.ADMailboxFolderSet, "ContactsEnabled"), null, null);

		// Token: 0x0400285A RID: 10330
		public static readonly ADPropertyDefinition TasksEnabled = new ADPropertyDefinition("TasksEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(8, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(8, OwaMailboxPolicySchema.ADMailboxFolderSet, "TasksEnabled"), null, null);

		// Token: 0x0400285B RID: 10331
		public static readonly ADPropertyDefinition JournalEnabled = new ADPropertyDefinition("JournalEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(16, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(16, OwaMailboxPolicySchema.ADMailboxFolderSet, "JournalEnabled"), null, null);

		// Token: 0x0400285C RID: 10332
		public static readonly ADPropertyDefinition NotesEnabled = new ADPropertyDefinition("NotesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(32, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(32, OwaMailboxPolicySchema.ADMailboxFolderSet, "NotesEnabled"), null, null);

		// Token: 0x0400285D RID: 10333
		public static readonly ADPropertyDefinition RemindersAndNotificationsEnabled = new ADPropertyDefinition("RemindersAndNotificationsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(256, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(256, OwaMailboxPolicySchema.ADMailboxFolderSet, "RemindersAndNotificationsEnabled"), null, null);

		// Token: 0x0400285E RID: 10334
		public static readonly ADPropertyDefinition PremiumClientEnabled = new ADPropertyDefinition("PremiumClientEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(512, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(512, OwaMailboxPolicySchema.ADMailboxFolderSet, "PremiumClientEnabled"), null, null);

		// Token: 0x0400285F RID: 10335
		public static readonly ADPropertyDefinition SpellCheckerEnabled = new ADPropertyDefinition("SpellCheckerEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(1024, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(1024, OwaMailboxPolicySchema.ADMailboxFolderSet, "SpellCheckerEnabled"), null, null);

		// Token: 0x04002860 RID: 10336
		public static readonly ADPropertyDefinition SearchFoldersEnabled = new ADPropertyDefinition("SearchFoldersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(4096, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(4096, OwaMailboxPolicySchema.ADMailboxFolderSet, "SearchFoldersEnabled"), null, null);

		// Token: 0x04002861 RID: 10337
		public static readonly ADPropertyDefinition SignaturesEnabled = new ADPropertyDefinition("SignaturesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(8192, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(8192, OwaMailboxPolicySchema.ADMailboxFolderSet, "SignaturesEnabled"), null, null);

		// Token: 0x04002862 RID: 10338
		public static readonly ADPropertyDefinition ThemeSelectionEnabled = new ADPropertyDefinition("ThemeSelectionEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(32768, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(32768, OwaMailboxPolicySchema.ADMailboxFolderSet, "ThemeSelectionEnabled"), null, null);

		// Token: 0x04002863 RID: 10339
		public static readonly ADPropertyDefinition JunkEmailEnabled = new ADPropertyDefinition("JunkEmailEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(65536, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(65536, OwaMailboxPolicySchema.ADMailboxFolderSet, "JunkEmailEnabled"), null, null);

		// Token: 0x04002864 RID: 10340
		public static readonly ADPropertyDefinition UMIntegrationEnabled = new ADPropertyDefinition("UMIntegrationEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(131072, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(131072, OwaMailboxPolicySchema.ADMailboxFolderSet, "UMIntegrationEnabled"), null, null);

		// Token: 0x04002865 RID: 10341
		public static readonly ADPropertyDefinition WSSAccessOnPublicComputersEnabled = new ADPropertyDefinition("WSSAccessOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(262144, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(262144, OwaMailboxPolicySchema.ADMailboxFolderSet, "WSSAccessOnPublicComputersEnabled"), null, null);

		// Token: 0x04002866 RID: 10342
		public static readonly ADPropertyDefinition WSSAccessOnPrivateComputersEnabled = new ADPropertyDefinition("WSSAccessOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(524288, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(524288, OwaMailboxPolicySchema.ADMailboxFolderSet, "WSSAccessOnPrivateComputersEnabled"), null, null);

		// Token: 0x04002867 RID: 10343
		public static readonly ADPropertyDefinition ChangePasswordEnabled = new ADPropertyDefinition("ChangePasswordEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(67108864, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(67108864, OwaMailboxPolicySchema.ADMailboxFolderSet, "ChangePasswordEnabled"), null, null);

		// Token: 0x04002868 RID: 10344
		public static readonly ADPropertyDefinition UNCAccessOnPublicComputersEnabled = new ADPropertyDefinition("UNCAccessOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(1048576, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(1048576, OwaMailboxPolicySchema.ADMailboxFolderSet, "UNCAccessOnPublicComputersEnabled"), null, null);

		// Token: 0x04002869 RID: 10345
		public static readonly ADPropertyDefinition UNCAccessOnPrivateComputersEnabled = new ADPropertyDefinition("UNCAccessOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(2097152, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(2097152, OwaMailboxPolicySchema.ADMailboxFolderSet, "UNCAccessOnPrivateComputersEnabled"), null, null);

		// Token: 0x0400286A RID: 10346
		public static readonly ADPropertyDefinition ActiveSyncIntegrationEnabled = new ADPropertyDefinition("ActiveSyncIntegrationEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(4194304, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(4194304, OwaMailboxPolicySchema.ADMailboxFolderSet, "ActiveSyncIntegrationEnabled"), null, null);

		// Token: 0x0400286B RID: 10347
		public static readonly ADPropertyDefinition AllAddressListsEnabled = new ADPropertyDefinition("AllAddressListsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(16777216, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(16777216, OwaMailboxPolicySchema.ADMailboxFolderSet, "AllAddressListsEnabled"), null, null);

		// Token: 0x0400286C RID: 10348
		public static readonly ADPropertyDefinition RulesEnabled = new ADPropertyDefinition("RulesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(16384, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(16384, OwaMailboxPolicySchema.ADMailboxFolderSet, "RulesEnabled"), null, null);

		// Token: 0x0400286D RID: 10349
		public static readonly ADPropertyDefinition PublicFoldersEnabled = new ADPropertyDefinition("PublicFoldersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(64, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(64, OwaMailboxPolicySchema.ADMailboxFolderSet, "PublicFoldersEnabled"), null, null);

		// Token: 0x0400286E RID: 10350
		public static readonly ADPropertyDefinition SMimeEnabled = new ADPropertyDefinition("SMimeEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(2048, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(2048, OwaMailboxPolicySchema.ADMailboxFolderSet, "SMimeEnabled"), null, null);

		// Token: 0x0400286F RID: 10351
		public static readonly ADPropertyDefinition RecoverDeletedItemsEnabled = new ADPropertyDefinition("RecoverDeletedItemsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(33554432, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(33554432, OwaMailboxPolicySchema.ADMailboxFolderSet, "RecoverDeletedItemsEnabled"), null, null);

		// Token: 0x04002870 RID: 10352
		public static readonly ADPropertyDefinition InstantMessagingEnabled = new ADPropertyDefinition("InstantMessagingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(134217728, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(134217728, OwaMailboxPolicySchema.ADMailboxFolderSet, "InstantMessagingEnabled"), null, null);

		// Token: 0x04002871 RID: 10353
		public static readonly ADPropertyDefinition TextMessagingEnabled = new ADPropertyDefinition("TextMessagingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(268435456, OwaMailboxPolicySchema.ADMailboxFolderSet), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(268435456, OwaMailboxPolicySchema.ADMailboxFolderSet, "TextMessagingEnabled"), null, null);

		// Token: 0x04002872 RID: 10354
		public static readonly ADPropertyDefinition DisplayPhotosEnabled = new ADPropertyDefinition("DisplayPhotosEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, OwaMailboxPolicySchema.DefaultSenderPhotoEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(512, OwaMailboxPolicySchema.ADMailboxFolderSet2), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(512, OwaMailboxPolicySchema.ADMailboxFolderSet2, "DisplayPhotosEnabled"), null, null);

		// Token: 0x04002873 RID: 10355
		public static readonly ADPropertyDefinition SetPhotoEnabled = new ADPropertyDefinition("SetPhotoEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, OwaMailboxPolicySchema.DefaultSenderPhotoEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(1024, OwaMailboxPolicySchema.ADMailboxFolderSet2), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(1024, OwaMailboxPolicySchema.ADMailboxFolderSet2, "SetPhotoEnabled"), null, null);

		// Token: 0x04002874 RID: 10356
		public static readonly ADPropertyDefinition AllowOfflineOn = OwaMailboxPolicySchema.AllowOfflineOnProperty("AllowOfflineOn", AllowOfflineOnEnum.AllComputers, OwaMailboxPolicySchema.ADMailboxFolderSet2);

		// Token: 0x04002875 RID: 10357
		public static readonly ADPropertyDefinition DirectFileAccessOnPublicComputersEnabled = new ADPropertyDefinition("DirectFileAccessOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.FlagGetterDelegate(1, OwaMailboxPolicySchema.FileAccessControlOnPublicComputers), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(1, OwaMailboxPolicySchema.FileAccessControlOnPublicComputers, "DirectFileAccessOnPublicComputersEnabled"), null, null);

		// Token: 0x04002876 RID: 10358
		public static readonly ADPropertyDefinition DirectFileAccessOnPrivateComputersEnabled = new ADPropertyDefinition("DirectFileAccessOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.FlagGetterDelegate(1, OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(1, OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers, "DirectFileAccessOnPrivateComputersEnabled"), null, null);

		// Token: 0x04002877 RID: 10359
		public static readonly ADPropertyDefinition WebReadyDocumentViewingOnPublicComputersEnabled = new ADPropertyDefinition("WebReadyDocumentViewingOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.FlagGetterDelegate(2, OwaMailboxPolicySchema.FileAccessControlOnPublicComputers), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(2, OwaMailboxPolicySchema.FileAccessControlOnPublicComputers, "WebReadyDocumentViewingOnPublicComputersEnabled"), null, null);

		// Token: 0x04002878 RID: 10360
		public static readonly ADPropertyDefinition WebReadyDocumentViewingOnPrivateComputersEnabled = new ADPropertyDefinition("WebReadyDocumentViewingOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.FlagGetterDelegate(2, OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(2, OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers, "WebReadyDocumentViewingOnPrivateComputersEnabled"), null, null);

		// Token: 0x04002879 RID: 10361
		public static readonly ADPropertyDefinition ForceWebReadyDocumentViewingFirstOnPublicComputers = new ADPropertyDefinition("ForceWebReadyDocumentViewingFirstOnPublicComputers", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.FlagGetterDelegate(4, OwaMailboxPolicySchema.FileAccessControlOnPublicComputers), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(4, OwaMailboxPolicySchema.FileAccessControlOnPublicComputers, "ForceWebReadyDocumentViewingFirstOnPublicComputers"), null, null);

		// Token: 0x0400287A RID: 10362
		public static readonly ADPropertyDefinition ForceWebReadyDocumentViewingFirstOnPrivateComputers = new ADPropertyDefinition("ForceWebReadyDocumentViewingFirstOnPrivateComputers", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.FlagGetterDelegate(4, OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(4, OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers, "ForceWebReadyDocumentViewingFirstOnPrivateComputers"), null, null);

		// Token: 0x0400287B RID: 10363
		public static readonly ADPropertyDefinition WacViewingOnPublicComputersEnabled = new ADPropertyDefinition("WacViewingOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.InvertFlagGetterDelegate(OwaMailboxPolicySchema.FileAccessControlOnPublicComputers, 8), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(8, OwaMailboxPolicySchema.FileAccessControlOnPublicComputers, "WacViewingOnPublicComputersEnabled", true), null, null);

		// Token: 0x0400287C RID: 10364
		public static readonly ADPropertyDefinition WacViewingOnPrivateComputersEnabled = new ADPropertyDefinition("WacViewingOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.InvertFlagGetterDelegate(OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers, 8), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(8, OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers, "WacViewingOnPrivateComputersEnabled", true), null, null);

		// Token: 0x0400287D RID: 10365
		public static readonly ADPropertyDefinition ForceWacViewingFirstOnPublicComputers = new ADPropertyDefinition("ForceWacViewingFirstOnPublicComputers", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.FlagGetterDelegate(16, OwaMailboxPolicySchema.FileAccessControlOnPublicComputers), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(16, OwaMailboxPolicySchema.FileAccessControlOnPublicComputers, "ForceWacViewingFirstOnPublicComputers"), null, null);

		// Token: 0x0400287E RID: 10366
		public static readonly ADPropertyDefinition ForceWacViewingFirstOnPrivateComputers = new ADPropertyDefinition("ForceWacViewingFirstOnPrivateComputers", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.FlagGetterDelegate(16, OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(16, OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers, "ForceWacViewingFirstOnPrivateComputers"), null, null);

		// Token: 0x0400287F RID: 10367
		public static readonly ADPropertyDefinition ForceSaveAttachmentFilteringEnabled = new ADPropertyDefinition("ForceSaveAttachmentFilteringEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, OwaMailboxPolicySchema.DefaultForceSaveAttachmentFilteringEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(1, OwaMailboxPolicySchema.ADMailboxFolderSet2), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(1, OwaMailboxPolicySchema.ADMailboxFolderSet2, "ForceSaveAttachmentFilteringEnabled"), null, null);

		// Token: 0x04002880 RID: 10368
		public static readonly ADPropertyDefinition SilverlightEnabled = new ADPropertyDefinition("SilverlightEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, OwaMailboxPolicySchema.DefaultSilverlightEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(2, OwaMailboxPolicySchema.ADMailboxFolderSet2), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(2, OwaMailboxPolicySchema.ADMailboxFolderSet2, "SilverlightEnabled"), null, null);

		// Token: 0x04002881 RID: 10369
		public static readonly ADPropertyDefinition PlacesEnabled = new ADPropertyDefinition("PlacesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, OwaMailboxPolicySchema.DefaultPlacesEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(64, OwaMailboxPolicySchema.ADMailboxFolderSet2), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(64, OwaMailboxPolicySchema.ADMailboxFolderSet2, "PlacesEnabled"), null, null);

		// Token: 0x04002882 RID: 10370
		public static readonly ADPropertyDefinition WeatherEnabled = new ADPropertyDefinition("WeatherEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, OwaMailboxPolicySchema.DefaultWeatherEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(67108864, OwaMailboxPolicySchema.ADMailboxFolderSet2), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(67108864, OwaMailboxPolicySchema.ADMailboxFolderSet2, "WeatherEnabled"), null, null);

		// Token: 0x04002883 RID: 10371
		public static readonly ADPropertyDefinition AllowCopyContactsToDeviceAddressBook = new ADPropertyDefinition("AllowCopyContactsToDeviceAddressBook", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(4194304, OwaMailboxPolicySchema.ADMailboxFolderSet2), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(4194304, OwaMailboxPolicySchema.ADMailboxFolderSet2, "AllowCopyContactsToDeviceAddressBook"), null, null);

		// Token: 0x04002884 RID: 10372
		public static readonly ADPropertyDefinition PredictedActionsEnabled = new ADPropertyDefinition("PredictedActionsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, OwaMailboxPolicySchema.DefaultPredictedActionsEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(8192, OwaMailboxPolicySchema.ADMailboxFolderSet2), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(8192, OwaMailboxPolicySchema.ADMailboxFolderSet2, "PredictedActionsEnabled"), null, null);

		// Token: 0x04002885 RID: 10373
		public static readonly ADPropertyDefinition UserDiagnosticEnabled = new ADPropertyDefinition("UserDiagnosticEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(32768, OwaMailboxPolicySchema.ADMailboxFolderSet2), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(32768, OwaMailboxPolicySchema.ADMailboxFolderSet2, "UserDiagnosticEnabled"), null, null);

		// Token: 0x04002886 RID: 10374
		public static readonly ADPropertyDefinition FacebookEnabled = new ADPropertyDefinition("FacebookEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(OwaMailboxPolicySchema.ADMailboxFolderSet2, 65536), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(65536, OwaMailboxPolicySchema.ADMailboxFolderSet2, "FacebookEnabled"), null, null);

		// Token: 0x04002887 RID: 10375
		public static readonly ADPropertyDefinition LinkedInEnabled = new ADPropertyDefinition("LinkedInEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(OwaMailboxPolicySchema.ADMailboxFolderSet2, 131072), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(131072, OwaMailboxPolicySchema.ADMailboxFolderSet2, "LinkedInEnabled"), null, null);

		// Token: 0x04002888 RID: 10376
		public static readonly ADPropertyDefinition WacExternalServicesEnabled = new ADPropertyDefinition("WacExternalServicesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(OwaMailboxPolicySchema.ADMailboxFolderSet2, 262144), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(262144, OwaMailboxPolicySchema.ADMailboxFolderSet2, "WacExternalServicesEnabled"), null, null);

		// Token: 0x04002889 RID: 10377
		public static readonly ADPropertyDefinition WacOMEXEnabled = new ADPropertyDefinition("WacOMEXEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(OwaMailboxPolicySchema.ADMailboxFolderSet2, 524288), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(524288, OwaMailboxPolicySchema.ADMailboxFolderSet2, "WacOMEXEnabledMask"), null, null);

		// Token: 0x0400288A RID: 10378
		public static readonly ADPropertyDefinition ReportJunkEmailEnabled = new ADPropertyDefinition("ReportJunkEmailEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(OwaMailboxPolicySchema.ADMailboxFolderSet2, 8388608), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(8388608, OwaMailboxPolicySchema.ADMailboxFolderSet2, "ReportJunkEmailEnabledMask"), null, null);

		// Token: 0x0400288B RID: 10379
		public static readonly ADPropertyDefinition GroupCreationEnabled = new ADPropertyDefinition("GroupCreationEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(OwaMailboxPolicySchema.ADMailboxFolderSet2, 16777216), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(16777216, OwaMailboxPolicySchema.ADMailboxFolderSet2, "GroupCreationEnabledMask"), null, null);

		// Token: 0x0400288C RID: 10380
		public static readonly ADPropertyDefinition SkipCreateUnifiedGroupCustomSharepointClassification = new ADPropertyDefinition("SkipCreateUnifiedGroupCustomSharepointClassification", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(OwaMailboxPolicySchema.ADMailboxFolderSet2, 33554432), OwaMailboxPolicySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14(33554432, OwaMailboxPolicySchema.ADMailboxFolderSet2, "SkipCreateUnifiedGroupCustomSharepointClassificationMask"), null, null);

		// Token: 0x0400288D RID: 10381
		public static readonly ADPropertyDefinition WebPartsFrameOptionsType = ADObject.BitfieldProperty("WebPartsFrameOptionsType", 20, 2, OwaMailboxPolicySchema.ADMailboxFolderSet2);
	}
}
