using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000366 RID: 870
	internal sealed class ADOwaVirtualDirectorySchema : ExchangeWebAppVirtualDirectorySchema
	{
		// Token: 0x06002833 RID: 10291 RVA: 0x000A893E File Offset: 0x000A6B3E
		internal static SetterDelegate FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(int mask, PropertyDefinition propertyDefinition, string description)
		{
			return ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersion(mask, propertyDefinition, description, OwaVersions.Exchange2007, false);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000A894A File Offset: 0x000A6B4A
		internal static SetterDelegate FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(int mask, PropertyDefinition propertyDefinition, string description)
		{
			return ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersion(mask, propertyDefinition, description, OwaVersions.Exchange2010, false);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000A8956 File Offset: 0x000A6B56
		internal static SetterDelegate FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(int mask, PropertyDefinition propertyDefinition, string description, bool invert)
		{
			return ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersion(mask, propertyDefinition, description, OwaVersions.Exchange2010, invert);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000A8A24 File Offset: 0x000A6C24
		private static SetterDelegate FlagSetterDelegateWithValidationOnOwaVersion(int mask, PropertyDefinition propertyDefinition, string description, OwaVersions owaVersion, bool invert)
		{
			return delegate(object value, IPropertyBag bag)
			{
				if ((OwaVersions)bag[ADOwaVirtualDirectorySchema.OwaVersion] < owaVersion)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory(description), propertyDefinition, value), null);
				}
				if (value != null)
				{
					bool flag = invert ? (!(bool)value) : ((bool)value);
					int num = (int)bag[propertyDefinition];
					bag[propertyDefinition] = (flag ? (num | mask) : (num & ~mask));
					return;
				}
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnE12VirtualDirectoryToNull(description), propertyDefinition, value), null);
			};
		}

		// Token: 0x04001866 RID: 6246
		private const int MaxNotificationInterval = 86400;

		// Token: 0x04001867 RID: 6247
		private const int MaxUserContextTimeout = 604800;

		// Token: 0x04001868 RID: 6248
		private const string FileExtensionOrSplatRegularExpression = "(^\\.([^.]+)|\\*)$";

		// Token: 0x04001869 RID: 6249
		private const string FileExtensionRegularExpression = "^\\.([^.]+)$";

		// Token: 0x0400186A RID: 6250
		internal static readonly string[] Exchange2007RTMWebReadyDocumentViewingSupportedFileTypes = new string[]
		{
			".doc",
			".dot",
			".rtf",
			".xls",
			".ppt",
			".pps",
			".pdf"
		};

		// Token: 0x0400186B RID: 6251
		internal static readonly string[] Exchange2007RTMWebReadyDocumentViewingSupportedMimeTypes = new string[]
		{
			"application/msword",
			"application/vnd.ms-excel",
			"application/x-msexcel",
			"application/vnd.ms-powerpoint",
			"application/x-mspowerpoint",
			"application/pdf"
		};

		// Token: 0x0400186C RID: 6252
		public static ADPropertyDefinition OwaVersion = new ADPropertyDefinition("OwaVersion", ExchangeObjectVersion.Exchange2007, typeof(OwaVersions), "msExchOwaVersion", ADPropertyDefinitionFlags.PersistDefaultValue, OwaVersions.Exchange2013, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400186D RID: 6253
		public static readonly ADPropertyDefinition FileAccessControlOnPublicComputers = new ADPropertyDefinition("FileAccessControlOnPublicComputers", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOWAFileAccessControlOnPublicComputers", ADPropertyDefinitionFlags.PersistDefaultValue, (int)OwaMailboxPolicySchema.FileAccessControlOnPublicComputers.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400186E RID: 6254
		public static readonly ADPropertyDefinition FileAccessControlOnPrivateComputers = new ADPropertyDefinition("FileAccessControlOnPrivateComputers", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOWAFileAccessControlOnPrivateComputers", ADPropertyDefinitionFlags.PersistDefaultValue, (int)OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400186F RID: 6255
		public static readonly ADPropertyDefinition RemoteDocumentsActionForUnknownServers = new ADPropertyDefinition("RemoteDocumentsActionForUnknownServers", ExchangeObjectVersion.Exchange2007, typeof(RemoteDocumentsActions), "msExchOWARemoteDocumentsActionForUnknownServers", ADPropertyDefinitionFlags.None, RemoteDocumentsActions.Block, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001870 RID: 6256
		public static readonly ADPropertyDefinition ActionForUnknownFileAndMIMETypes = new ADPropertyDefinition("ActionForUnknownFileAndMIMETypes", ExchangeObjectVersion.Exchange2007, typeof(AttachmentBlockingActions), "msExchOWAActionForUnknownFileAndMIMETypes", ADPropertyDefinitionFlags.None, (AttachmentBlockingActions)OwaMailboxPolicySchema.ActionForUnknownFileAndMIMETypes.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001871 RID: 6257
		internal static readonly ADPropertyDefinition ADWebReadyFileTypes = SharedPropertyDefinitions.ADWebReadyFileTypes;

		// Token: 0x04001872 RID: 6258
		public static readonly ADPropertyDefinition WebReadyFileTypes = new ADPropertyDefinition("WebReadyFileTypes", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADWebReadyFileTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.WebReadyFileTypesGetter), new SetterDelegate(ADOwaVirtualDirectory.WebReadyFileTypesSetter), null, null);

		// Token: 0x04001873 RID: 6259
		internal static readonly ADPropertyDefinition ADWebReadyMimeTypes = SharedPropertyDefinitions.ADWebReadyMimeTypes;

		// Token: 0x04001874 RID: 6260
		public static readonly ADPropertyDefinition WebReadyMimeTypes = new ADPropertyDefinition("WebReadyMimeTypes", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADWebReadyMimeTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.WebReadyMimeTypesGetter), new SetterDelegate(ADOwaVirtualDirectory.WebReadyMimeTypesSetter), null, null);

		// Token: 0x04001875 RID: 6261
		internal static readonly ADPropertyDefinition ADWebReadyDocumentViewingFlags = new ADPropertyDefinition("ADWebReadyDocumentViewingFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOWATranscodingFlags", ADPropertyDefinitionFlags.PersistDefaultValue, (int)OwaMailboxPolicySchema.ADWebReadyDocumentViewingFlags.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001876 RID: 6262
		public static readonly ADPropertyDefinition WebReadyDocumentViewingForAllSupportedTypes = new ADPropertyDefinition("WebReadyDocumentViewingForAllSupportedTypes", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADWebReadyDocumentViewingFlags
		}, null, ADObject.FlagGetterDelegate(1, ADOwaVirtualDirectorySchema.ADWebReadyDocumentViewingFlags), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(1, ADOwaVirtualDirectorySchema.ADWebReadyDocumentViewingFlags, "WebReadyDocumentViewingForAllSupportedTypes"), null, null);

		// Token: 0x04001877 RID: 6263
		internal static readonly ADPropertyDefinition ADAllowedFileTypes = SharedPropertyDefinitions.ADAllowedFileTypes;

		// Token: 0x04001878 RID: 6264
		public static readonly ADPropertyDefinition AllowedFileTypes = new ADPropertyDefinition("AllowedFileTypes", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RegexConstraint("^\\.([^.]+)$", DataStrings.FileExtensionPatternDescription)
		}, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADAllowedFileTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.AllowedFileTypesGetter), new SetterDelegate(ADOwaVirtualDirectory.AllowedFileTypesSetter), null, null);

		// Token: 0x04001879 RID: 6265
		internal static readonly ADPropertyDefinition ADAllowedMimeTypes = SharedPropertyDefinitions.ADAllowedMimeTypes;

		// Token: 0x0400187A RID: 6266
		public static readonly ADPropertyDefinition AllowedMimeTypes = new ADPropertyDefinition("AllowedMimeTypes", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADAllowedMimeTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.AllowedMimeTypesGetter), new SetterDelegate(ADOwaVirtualDirectory.AllowedMimeTypesSetter), null, null);

		// Token: 0x0400187B RID: 6267
		internal static readonly ADPropertyDefinition ADForceSaveFileTypes = SharedPropertyDefinitions.ADForceSaveFileTypes;

		// Token: 0x0400187C RID: 6268
		public static readonly ADPropertyDefinition ForceSaveFileTypes = new ADPropertyDefinition("ForceSaveFileTypes", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RegexConstraint("^\\.([^.]+)$", DataStrings.FileExtensionPatternDescription)
		}, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADForceSaveFileTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.ForceSaveFileTypesGetter), new SetterDelegate(ADOwaVirtualDirectory.ForceSaveFileTypesSetter), null, null);

		// Token: 0x0400187D RID: 6269
		internal static readonly ADPropertyDefinition ADForceSaveMimeTypes = SharedPropertyDefinitions.ADForceSaveMimeTypes;

		// Token: 0x0400187E RID: 6270
		public static readonly ADPropertyDefinition ForceSaveMimeTypes = new ADPropertyDefinition("ForceSaveMimeTypes", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADForceSaveMimeTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.ForceSaveMimeTypesGetter), new SetterDelegate(ADOwaVirtualDirectory.ForceSaveMimeTypesSetter), null, null);

		// Token: 0x0400187F RID: 6271
		internal static readonly ADPropertyDefinition ADBlockedFileTypes = SharedPropertyDefinitions.ADBlockedFileTypes;

		// Token: 0x04001880 RID: 6272
		public static readonly ADPropertyDefinition BlockedFileTypes = new ADPropertyDefinition("BlockedFileTypes", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RegexConstraint("^\\.([^.]+)$", DataStrings.FileExtensionPatternDescription)
		}, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADBlockedFileTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.BlockedFileTypesGetter), new SetterDelegate(ADOwaVirtualDirectory.BlockedFileTypesSetter), null, null);

		// Token: 0x04001881 RID: 6273
		internal static readonly ADPropertyDefinition ADBlockedMimeTypes = SharedPropertyDefinitions.ADBlockedMimeTypes;

		// Token: 0x04001882 RID: 6274
		public static readonly ADPropertyDefinition BlockedMimeTypes = new ADPropertyDefinition("BlockedMimeTypes", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADBlockedMimeTypes,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.BlockedMimeTypesGetter), new SetterDelegate(ADOwaVirtualDirectory.BlockedMimeTypesSetter), null, null);

		// Token: 0x04001883 RID: 6275
		internal static readonly ADPropertyDefinition ADRemoteDocumentsAllowedServers = new ADPropertyDefinition("ADRemoteDocumentsAllowedServers", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWARemoteDocumentsAllowedServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001884 RID: 6276
		public static readonly ADPropertyDefinition RemoteDocumentsAllowedServers = new ADPropertyDefinition("RemoteDocumentsAllowedServers", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 256)
		}, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADRemoteDocumentsAllowedServers,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.RemoteDocumentsAllowedServersGetter), new SetterDelegate(ADOwaVirtualDirectory.RemoteDocumentsAllowedServersSetter), null, null);

		// Token: 0x04001885 RID: 6277
		internal static readonly ADPropertyDefinition ADRemoteDocumentsBlockedServers = new ADPropertyDefinition("ADRemoteDocumentsBlockedServers", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWARemoteDocumentsBlockedServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001886 RID: 6278
		public static readonly ADPropertyDefinition RemoteDocumentsBlockedServers = new ADPropertyDefinition("RemoteDocumentsBlockedServers", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 256)
		}, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADRemoteDocumentsBlockedServers,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.RemoteDocumentsBlockedServersGetter), new SetterDelegate(ADOwaVirtualDirectory.RemoteDocumentsBlockedServersSetter), null, null);

		// Token: 0x04001887 RID: 6279
		internal static readonly ADPropertyDefinition ADRemoteDocumentsInternalDomainSuffixList = new ADPropertyDefinition("ADRemoteDocumentsInternalDomainSuffixList", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWARemoteDocumentsInternalDomainSuffixList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001888 RID: 6280
		public static readonly ADPropertyDefinition RemoteDocumentsInternalDomainSuffixList = new ADPropertyDefinition("RemoteDocumentsInternalDomainSuffixList", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidDomainConstraint()
		}, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADOwaVirtualDirectory.RemoteDocumentsInternalDomainSuffixListGetter), new SetterDelegate(ADOwaVirtualDirectory.RemoteDocumentsInternalDomainSuffixListSetter), null, null);

		// Token: 0x04001889 RID: 6281
		public static readonly ADPropertyDefinition LogonFormat = new ADPropertyDefinition("LogonFormat", ExchangeObjectVersion.Exchange2007, typeof(LogonFormats), "msExchOWALogonFormat", ADPropertyDefinitionFlags.None, LogonFormats.FullDomain, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400188A RID: 6282
		public static readonly ADPropertyDefinition ClientAuthCleanupLevel = new ADPropertyDefinition("ClientAuthCleanupLevel", ExchangeObjectVersion.Exchange2007, typeof(ClientAuthCleanupLevels), "msExchOWAClientAuthCleanupLevel", ADPropertyDefinitionFlags.None, ClientAuthCleanupLevels.High, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400188B RID: 6283
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x0400188C RID: 6284
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<ADOwaVirtualDirectoryConfigXML>(ADOwaVirtualDirectorySchema.ConfigurationXMLRaw);

		// Token: 0x0400188D RID: 6285
		public static readonly ADPropertyDefinition IsPublic = XMLSerializableBase.ConfigXmlProperty<ADOwaVirtualDirectoryConfigXML, bool>("IsPublic", ExchangeObjectVersion.Exchange2010, ADOwaVirtualDirectorySchema.ConfigurationXML, false, (ADOwaVirtualDirectoryConfigXML configXml) => configXml.IsPublic, delegate(ADOwaVirtualDirectoryConfigXML configXml, bool value)
		{
			configXml.IsPublic = value;
		}, null, null);

		// Token: 0x0400188E RID: 6286
		public static readonly ADPropertyDefinition FilterWebBeaconsAndHtmlForms = new ADPropertyDefinition("FilterWebBeaconsAndHtmlForms", ExchangeObjectVersion.Exchange2007, typeof(WebBeaconFilterLevels), "msExchOWAFilterWebBeacons", ADPropertyDefinitionFlags.None, WebBeaconFilterLevels.UserFilterChoice, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400188F RID: 6287
		public static readonly ADPropertyDefinition NotificationInterval = new ADPropertyDefinition("NotificationInterval", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchOWANotificationInterval", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(30, 86400)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001890 RID: 6288
		public static readonly ADPropertyDefinition DefaultTheme = new ADPropertyDefinition("DefaultTheme", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchOWADefaultTheme", ADPropertyDefinitionFlags.None, (string)OwaMailboxPolicySchema.DefaultTheme.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001891 RID: 6289
		public static readonly ADPropertyDefinition UserContextTimeout = new ADPropertyDefinition("UserContextTimeout", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchOWAUserContextTimeout", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 604800)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001892 RID: 6290
		public static readonly ADPropertyDefinition ExchwebProxyDestination = new ADPropertyDefinition("ExchwebProxyDestination", ExchangeObjectVersion.Exchange2007, typeof(ExchwebProxyDestinations), "msExchOwaExchwebProxyDestination", ADPropertyDefinitionFlags.None, ExchwebProxyDestinations.NotSpecified, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001893 RID: 6291
		public static readonly ADPropertyDefinition VirtualDirectoryType = new ADPropertyDefinition("VirtualDirectoryType", ExchangeObjectVersion.Exchange2007, typeof(VirtualDirectoryTypes), "msExchOwaVirtualDirectoryType", ADPropertyDefinitionFlags.None, VirtualDirectoryTypes.NotSpecified, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001894 RID: 6292
		public static readonly ADPropertyDefinition FolderPathname = new ADPropertyDefinition("FolderPathname", ExchangeObjectVersion.Exchange2007, typeof(string), "folderPathname", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001895 RID: 6293
		internal static readonly ADPropertyDefinition Url = new ADPropertyDefinition("Url", ExchangeObjectVersion.Exchange2007, typeof(string), "url", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001896 RID: 6294
		public static readonly ADPropertyDefinition InstantMessagingType = new ADPropertyDefinition("InstantMessagingType", ExchangeObjectVersion.Exchange2010, typeof(InstantMessagingTypeOptions), "msExchOWAIMProviderType", ADPropertyDefinitionFlags.None, (InstantMessagingTypeOptions)OwaMailboxPolicySchema.InstantMessagingType.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001897 RID: 6295
		public static readonly ADPropertyDefinition InstantMessagingCertificateThumbprint = new ADPropertyDefinition("InstantMessagingCertificateThumbprint", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchOWAIMCertificateThumbprint", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001898 RID: 6296
		public static readonly ADPropertyDefinition InstantMessagingServerName = new ADPropertyDefinition("InstantMessagingServerName", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchOWAIMServerName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001899 RID: 6297
		internal static readonly ADPropertyDefinition SetPhotoURL = new ADPropertyDefinition("SetPhotoURL", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchAggregationSubscriptionCredential", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400189A RID: 6298
		public static readonly ADPropertyDefinition RedirectToOptimalOWAServer = new ADPropertyDefinition("RedirectToOptimalOWAServer", ExchangeObjectVersion.Exchange2007, typeof(RedirectToOptimalOWAServerOptions), "msExchOWARedirectToOptimalOWAServer", ADPropertyDefinitionFlags.None, RedirectToOptimalOWAServerOptions.Enabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400189B RID: 6299
		public static readonly ADPropertyDefinition DefaultClientLanguage = new ADPropertyDefinition("DefaultClientLanguage", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchOWADefaultClientLanguage", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new OWASupportedLanguageConstraint()
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400189C RID: 6300
		public static readonly ADPropertyDefinition LogonAndErrorLanguage = new ADPropertyDefinition("LogonAndErrorLanguage", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchOWALogonAndErrorLanguage", ADPropertyDefinitionFlags.PersistDefaultValue, (int)OwaMailboxPolicySchema.LogonAndErrorLanguage.DefaultValue, new PropertyDefinitionConstraint[]
		{
			new OWASupportedLanguageConstraint()
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400189D RID: 6301
		public static readonly ADPropertyDefinition UseGB18030 = new ADPropertyDefinition("UseGB18030", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchOWAUseGB18030", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400189E RID: 6302
		public static readonly ADPropertyDefinition UseISO885915 = new ADPropertyDefinition("UseISO885915", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchOWAUseISO885915", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400189F RID: 6303
		public static readonly ADPropertyDefinition OutboundCharset = new ADPropertyDefinition("OutboundCharset", ExchangeObjectVersion.Exchange2007, typeof(OutboundCharsetOptions), "msExchOWAOutboundCharset", ADPropertyDefinitionFlags.None, (OutboundCharsetOptions)OwaMailboxPolicySchema.OutboundCharset.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040018A0 RID: 6304
		public static readonly ADPropertyDefinition ADMailboxFolderSet = new ADPropertyDefinition("ADMailboxFolderSet", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMailboxFolderSet", ADPropertyDefinitionFlags.PersistDefaultValue, (int)OwaMailboxPolicySchema.ADMailboxFolderSet.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040018A1 RID: 6305
		public static readonly ADPropertyDefinition ADMailboxFolderSet2 = new ADPropertyDefinition("ADMailboxFolderSet2", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchMailboxFolderSet2", ADPropertyDefinitionFlags.PersistDefaultValue, (int)OwaMailboxPolicySchema.ADMailboxFolderSet2.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040018A2 RID: 6306
		public static readonly ADPropertyDefinition GlobalAddressListEnabled = new ADPropertyDefinition("GlobalAddressListEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(1, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(1, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "GlobalAddressListEnabled"), null, null);

		// Token: 0x040018A3 RID: 6307
		public static readonly ADPropertyDefinition OrganizationEnabled = new ADPropertyDefinition("OrganizationEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(128, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(128, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "OrganizationEnabled"), null, null);

		// Token: 0x040018A4 RID: 6308
		public static readonly ADPropertyDefinition ExplicitLogonEnabled = new ADPropertyDefinition("ExplicitLogonEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(8388608, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(8388608, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "ExplicitLogonEnabled"), null, null);

		// Token: 0x040018A5 RID: 6309
		public static readonly ADPropertyDefinition OWALightEnabled = new ADPropertyDefinition("OWALightEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(536870912, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(536870912, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "OWALightEnabled"), null, null);

		// Token: 0x040018A6 RID: 6310
		public static readonly ADPropertyDefinition DelegateAccessEnabled = new ADPropertyDefinition("DelegateAccessEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(1073741824, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(1073741824, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "DelegateAccessEnabled"), null, null);

		// Token: 0x040018A7 RID: 6311
		public static readonly ADPropertyDefinition IRMEnabled = new ADPropertyDefinition("IRMEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(int.MinValue, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(int.MinValue, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "IRMEnabled"), null, null);

		// Token: 0x040018A8 RID: 6312
		public static readonly ADPropertyDefinition CalendarEnabled = new ADPropertyDefinition("CalendarEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(2, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(2, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "CalendarEnabled"), null, null);

		// Token: 0x040018A9 RID: 6313
		public static readonly ADPropertyDefinition ContactsEnabled = new ADPropertyDefinition("ContactsEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(4, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(4, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "ContactsEnabled"), null, null);

		// Token: 0x040018AA RID: 6314
		public static readonly ADPropertyDefinition TasksEnabled = new ADPropertyDefinition("TasksEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(8, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(8, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "TasksEnabled"), null, null);

		// Token: 0x040018AB RID: 6315
		public static readonly ADPropertyDefinition JournalEnabled = new ADPropertyDefinition("JournalEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(16, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(16, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "JournalEnabled"), null, null);

		// Token: 0x040018AC RID: 6316
		public static readonly ADPropertyDefinition NotesEnabled = new ADPropertyDefinition("NotesEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(32, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(32, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "NotesEnabled"), null, null);

		// Token: 0x040018AD RID: 6317
		public static readonly ADPropertyDefinition RemindersAndNotificationsEnabled = new ADPropertyDefinition("RemindersAndNotificationsEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(256, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(256, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "RemindersAndNotificationsEnabled"), null, null);

		// Token: 0x040018AE RID: 6318
		public static readonly ADPropertyDefinition PremiumClientEnabled = new ADPropertyDefinition("PremiumClientEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(512, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(512, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "PremiumClientEnabled"), null, null);

		// Token: 0x040018AF RID: 6319
		public static readonly ADPropertyDefinition SpellCheckerEnabled = new ADPropertyDefinition("SpellCheckerEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(1024, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(1024, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "SpellCheckerEnabled"), null, null);

		// Token: 0x040018B0 RID: 6320
		public static readonly ADPropertyDefinition SearchFoldersEnabled = new ADPropertyDefinition("SearchFoldersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(4096, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(4096, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "SearchFoldersEnabled"), null, null);

		// Token: 0x040018B1 RID: 6321
		public static readonly ADPropertyDefinition SignaturesEnabled = new ADPropertyDefinition("SignaturesEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(8192, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(8192, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "SignaturesEnabled"), null, null);

		// Token: 0x040018B2 RID: 6322
		public static readonly ADPropertyDefinition ThemeSelectionEnabled = new ADPropertyDefinition("ThemeSelectionEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(32768, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(32768, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "ThemeSelectionEnabled"), null, null);

		// Token: 0x040018B3 RID: 6323
		public static readonly ADPropertyDefinition JunkEmailEnabled = new ADPropertyDefinition("JunkEmailEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(65536, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(65536, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "JunkEmailEnabled"), null, null);

		// Token: 0x040018B4 RID: 6324
		public static readonly ADPropertyDefinition UMIntegrationEnabled = new ADPropertyDefinition("UMIntegrationEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(131072, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(131072, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "UMIntegrationEnabled"), null, null);

		// Token: 0x040018B5 RID: 6325
		public static readonly ADPropertyDefinition WSSAccessOnPublicComputersEnabled = new ADPropertyDefinition("WSSAccessOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(262144, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(262144, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "WSSAccessOnPublicComputersEnabled"), null, null);

		// Token: 0x040018B6 RID: 6326
		public static readonly ADPropertyDefinition WSSAccessOnPrivateComputersEnabled = new ADPropertyDefinition("WSSAccessOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(524288, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(524288, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "WSSAccessOnPrivateComputersEnabled"), null, null);

		// Token: 0x040018B7 RID: 6327
		public static readonly ADPropertyDefinition ChangePasswordEnabled = new ADPropertyDefinition("ChangePasswordEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(67108864, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(67108864, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "ChangePasswordEnabled"), null, null);

		// Token: 0x040018B8 RID: 6328
		public static readonly ADPropertyDefinition UNCAccessOnPublicComputersEnabled = new ADPropertyDefinition("UNCAccessOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(1048576, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(1048576, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "UNCAccessOnPublicComputersEnabled"), null, null);

		// Token: 0x040018B9 RID: 6329
		public static readonly ADPropertyDefinition UNCAccessOnPrivateComputersEnabled = new ADPropertyDefinition("UNCAccessOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(2097152, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(2097152, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "UNCAccessOnPrivateComputersEnabled"), null, null);

		// Token: 0x040018BA RID: 6330
		public static readonly ADPropertyDefinition ActiveSyncIntegrationEnabled = new ADPropertyDefinition("ActiveSyncIntegrationEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(4194304, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(4194304, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "ActiveSyncIntegrationEnabled"), null, null);

		// Token: 0x040018BB RID: 6331
		public static readonly ADPropertyDefinition AllAddressListsEnabled = new ADPropertyDefinition("AllAddressListsEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(16777216, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(16777216, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "AllAddressListsEnabled"), null, null);

		// Token: 0x040018BC RID: 6332
		public static readonly ADPropertyDefinition RulesEnabled = new ADPropertyDefinition("RulesEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(16384, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(16384, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "RulesEnabled"), null, null);

		// Token: 0x040018BD RID: 6333
		public static readonly ADPropertyDefinition PublicFoldersEnabled = new ADPropertyDefinition("PublicFoldersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(64, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(64, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "PublicFoldersEnabled"), null, null);

		// Token: 0x040018BE RID: 6334
		public static readonly ADPropertyDefinition SMimeEnabled = new ADPropertyDefinition("SMimeEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(2048, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(2048, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "SMimeEnabled"), null, null);

		// Token: 0x040018BF RID: 6335
		public static readonly ADPropertyDefinition RecoverDeletedItemsEnabled = new ADPropertyDefinition("RecoverDeletedItemsEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(33554432, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(33554432, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "RecoverDeletedItemsEnabled"), null, null);

		// Token: 0x040018C0 RID: 6336
		public static readonly ADPropertyDefinition InstantMessagingEnabled = new ADPropertyDefinition("InstantMessagingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(134217728, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(134217728, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "InstantMessagingEnabled"), null, null);

		// Token: 0x040018C1 RID: 6337
		public static readonly ADPropertyDefinition TextMessagingEnabled = new ADPropertyDefinition("TextMessagingEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(268435456, ADOwaVirtualDirectorySchema.ADMailboxFolderSet), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(268435456, ADOwaVirtualDirectorySchema.ADMailboxFolderSet, "TextMessagingEnabled"), null, null);

		// Token: 0x040018C2 RID: 6338
		public static readonly ADPropertyDefinition ForceSaveAttachmentFilteringEnabled = new ADPropertyDefinition("ForceSaveAttachmentFilteringEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, (bool)OwaMailboxPolicySchema.ForceSaveAttachmentFilteringEnabled.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet2,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(1, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(1, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "ForceSaveAttachmentFilteringEnabled"), null, null);

		// Token: 0x040018C3 RID: 6339
		public static readonly ADPropertyDefinition SilverlightEnabled = new ADPropertyDefinition("SilverlightEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, (bool)OwaMailboxPolicySchema.SilverlightEnabled.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet2,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(2, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(2, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "SilverlightEnabled"), null, null);

		// Token: 0x040018C4 RID: 6340
		public static readonly ADPropertyDefinition PlacesEnabled = new ADPropertyDefinition("PlacesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, (bool)OwaMailboxPolicySchema.PlacesEnabled.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(64, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(64, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "PlacesEnabled"), null, null);

		// Token: 0x040018C5 RID: 6341
		public static readonly ADPropertyDefinition WeatherEnabled = new ADPropertyDefinition("WeatherEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, (bool)OwaMailboxPolicySchema.WeatherEnabled.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(67108864, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(67108864, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "WeatherEnabled"), null, null);

		// Token: 0x040018C6 RID: 6342
		public static readonly ADPropertyDefinition AllowCopyContactsToDeviceAddressBook = new ADPropertyDefinition("AllowCopyContactsToDeviceAddressBook", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(4194304, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(4194304, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "AllowCopyContactsToDeviceAddressBook"), null, null);

		// Token: 0x040018C7 RID: 6343
		public static readonly ADPropertyDefinition LogonPagePublicPrivateSelectionEnabled = new ADPropertyDefinition("LogonPagePublicPrivateSelectionEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet2,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(4096, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(4096, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "LogonPagePublicPrivateSelectionEnabled"), null, null);

		// Token: 0x040018C8 RID: 6344
		public static readonly ADPropertyDefinition LogonPageLightSelectionEnabled = new ADPropertyDefinition("LogonPageLightSelectionEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet2,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(2048, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(2048, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "LogonPageLightSelectionEnabled"), null, null);

		// Token: 0x040018C9 RID: 6345
		public static readonly ADPropertyDefinition AllowOfflineOn = OwaMailboxPolicySchema.AllowOfflineOnProperty("AllowOfflineOn", AllowOfflineOnEnum.AllComputers, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2);

		// Token: 0x040018CA RID: 6346
		public static readonly ADPropertyDefinition AnonymousFeaturesEnabled = new ADPropertyDefinition("AnonymousFeaturesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet2,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(4, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(4, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "AnonymousFeaturesEnabled"), null, null);

		// Token: 0x040018CB RID: 6347
		public static readonly ADPropertyDefinition IntegratedFeaturesEnabled = new ADPropertyDefinition("IntegratedFeaturesEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet2,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(16384, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(16384, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "IntegratedFeaturesEnabled"), null, null);

		// Token: 0x040018CC RID: 6348
		public static readonly ADPropertyDefinition DisplayPhotosEnabled = new ADPropertyDefinition("DisplayPhotosEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, OwaMailboxPolicySchema.DefaultSenderPhotoEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet2,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(512, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(512, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "DisplayPhotosEnabled"), null, null);

		// Token: 0x040018CD RID: 6349
		public static readonly ADPropertyDefinition SetPhotoEnabled = new ADPropertyDefinition("SetPhotoEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, OwaMailboxPolicySchema.DefaultSenderPhotoEnabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.ADMailboxFolderSet2,
			ADOwaVirtualDirectorySchema.OwaVersion
		}, null, ADObject.FlagGetterDelegate(1024, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(1024, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "SetPhotoEnabled"), null, null);

		// Token: 0x040018CE RID: 6350
		public static readonly ADPropertyDefinition PredictedActionsEnabled = new ADPropertyDefinition("PredictedActionsEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, (bool)OwaMailboxPolicySchema.PredictedActionsEnabled.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(8192, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(8192, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "PredictedActionsEnabled"), null, null);

		// Token: 0x040018CF RID: 6351
		public static readonly ADPropertyDefinition UserDiagnosticEnabled = new ADPropertyDefinition("UserDiagnosticEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(32768, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(32768, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "UserDiagnosticEnabled"), null, null);

		// Token: 0x040018D0 RID: 6352
		public static readonly ADPropertyDefinition ReportJunkEmailEnabled = new ADPropertyDefinition("ReportJunkEmailEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OwaMailboxPolicySchema.ADMailboxFolderSet2,
			ADObjectSchema.ExchangeVersion
		}, null, ADObject.FlagGetterDelegate(8388608, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(8388608, ADOwaVirtualDirectorySchema.ADMailboxFolderSet2, "ReportJunkEmailEnabled"), null, null);

		// Token: 0x040018D1 RID: 6353
		public static readonly ADPropertyDefinition WebPartsFrameOptionsType = ADObject.BitfieldProperty("WebPartsFrameOptionsType", 20, 2, OwaMailboxPolicySchema.ADMailboxFolderSet2);

		// Token: 0x040018D2 RID: 6354
		public static readonly ADPropertyDefinition DirectFileAccessOnPublicComputersEnabled = new ADPropertyDefinition("DirectFileAccessOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.FlagGetterDelegate(1, ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(1, ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers, "DirectFileAccessOnPublicComputersEnabled"), null, null);

		// Token: 0x040018D3 RID: 6355
		public static readonly ADPropertyDefinition DirectFileAccessOnPrivateComputersEnabled = new ADPropertyDefinition("DirectFileAccessOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.FlagGetterDelegate(1, ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(1, ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers, "DirectFileAccessOnPrivateComputersEnabled"), null, null);

		// Token: 0x040018D4 RID: 6356
		public static readonly ADPropertyDefinition WebReadyDocumentViewingOnPublicComputersEnabled = new ADPropertyDefinition("WebReadyDocumentViewingOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.FlagGetterDelegate(2, ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(2, ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers, "WebReadyDocumentViewingOnPublicComputersEnabled"), null, null);

		// Token: 0x040018D5 RID: 6357
		public static readonly ADPropertyDefinition WebReadyDocumentViewingOnPrivateComputersEnabled = new ADPropertyDefinition("WebReadyDocumentViewingOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.FlagGetterDelegate(2, ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(2, ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers, "WebReadyDocumentViewingOnPrivateComputersEnabled"), null, null);

		// Token: 0x040018D6 RID: 6358
		public static readonly ADPropertyDefinition ForceWebReadyDocumentViewingFirstOnPublicComputers = new ADPropertyDefinition("ForceWebReadyDocumentViewingFirstOnPublicComputers", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.FlagGetterDelegate(4, ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(4, ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers, "ForceWebReadyDocumentViewingFirstOnPublicComputers"), null, null);

		// Token: 0x040018D7 RID: 6359
		public static readonly ADPropertyDefinition ForceWebReadyDocumentViewingFirstOnPrivateComputers = new ADPropertyDefinition("ForceWebReadyDocumentViewingFirstOnPrivateComputers", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.FlagGetterDelegate(4, ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange12OrLater(4, ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers, "ForceWebReadyDocumentViewingFirstOnPrivateComputers"), null, null);

		// Token: 0x040018D8 RID: 6360
		public static readonly ADPropertyDefinition WacViewingOnPublicComputersEnabled = new ADPropertyDefinition("WacViewingOnPublicComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.InvertFlagGetterDelegate(ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers, 8), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(8, ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers, "WacViewingOnPublicComputersEnabled", true), null, null);

		// Token: 0x040018D9 RID: 6361
		public static readonly ADPropertyDefinition WacViewingOnPrivateComputersEnabled = new ADPropertyDefinition("WacViewingOnPrivateComputersEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.InvertFlagGetterDelegate(ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers, 8), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(8, ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers, "WacViewingOnPrivateComputersEnabled", true), null, null);

		// Token: 0x040018DA RID: 6362
		public static readonly ADPropertyDefinition ForceWacViewingFirstOnPublicComputers = new ADPropertyDefinition("ForceWacViewingFirstOnPublicComputers", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers
		}, null, ADObject.FlagGetterDelegate(16, ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(16, ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers, "ForceWacViewingFirstOnPublicComputers"), null, null);

		// Token: 0x040018DB RID: 6363
		public static readonly ADPropertyDefinition ForceWacViewingFirstOnPrivateComputers = new ADPropertyDefinition("ForceWacViewingFirstOnPrivateComputers", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers
		}, null, ADObject.FlagGetterDelegate(16, ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers), ADOwaVirtualDirectorySchema.FlagSetterDelegateWithValidationOnOwaVersionIsExchange14OrLater(16, ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers, "ForceWacViewingFirstOnPrivateComputers"), null, null);

		// Token: 0x040018DC RID: 6364
		public static readonly ADPropertyDefinition Exchange2003Url = new ADPropertyDefinition("Exchange2003Url", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExch2003Url", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040018DD RID: 6365
		public static readonly ADPropertyDefinition FailbackUrl = new ADPropertyDefinition("FailbackUrl", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchOWAFailbackURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x02000367 RID: 871
		private struct Defaults
		{
			// Token: 0x040018E0 RID: 6368
			internal const RemoteDocumentsActions RemoteDocumentsActionForUnknownServers = RemoteDocumentsActions.Block;

			// Token: 0x040018E1 RID: 6369
			internal const LogonFormats LogonFormat = LogonFormats.FullDomain;

			// Token: 0x040018E2 RID: 6370
			internal const ClientAuthCleanupLevels ClientAuthCleanupLevel = ClientAuthCleanupLevels.High;

			// Token: 0x040018E3 RID: 6371
			internal const WebBeaconFilterLevels FilterWebBeaconsAndHtmlForms = WebBeaconFilterLevels.UserFilterChoice;

			// Token: 0x040018E4 RID: 6372
			internal const ExchwebProxyDestinations ExchwebProxyDestination = ExchwebProxyDestinations.NotSpecified;

			// Token: 0x040018E5 RID: 6373
			internal const VirtualDirectoryTypes VirtualDirectoryType = VirtualDirectoryTypes.NotSpecified;

			// Token: 0x040018E6 RID: 6374
			internal const OwaVersions OwaVersion = OwaVersions.Exchange2013;

			// Token: 0x040018E7 RID: 6375
			internal const RedirectToOptimalOWAServerOptions RedirectToOptimalOWAServer = RedirectToOptimalOWAServerOptions.Enabled;
		}
	}
}
