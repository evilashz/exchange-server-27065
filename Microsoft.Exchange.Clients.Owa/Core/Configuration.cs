using System;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000F5 RID: 245
	public class Configuration : ConfigurationBase
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x0003BA7C File Offset: 0x00039C7C
		internal Configuration(IConfigurationSession session, string virtualDirectory, string webSiteName, ADObjectId vDirADObjectId, bool isPhoneticSupportEnabled)
		{
			base.PhoneticSupportEnabled = isPhoneticSupportEnabled;
			if (Globals.IsPreCheckinApp)
			{
				this.ExpirationTime = DateTime.UtcNow + Configuration.expirationPeriod;
				this.LoadPreCheckInVdirConfiguration();
				return;
			}
			ADOwaVirtualDirectory adowaVirtualDirectory = session.Read<ADOwaVirtualDirectory>(vDirADObjectId);
			if (adowaVirtualDirectory == null)
			{
				string message = string.Format(LocalizedStrings.GetNonEncoded(-1166886287), virtualDirectory, webSiteName);
				throw new OwaInvalidConfigurationException(message);
			}
			this.formsAuthenticationEnabled = (adowaVirtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.Fba) ? 1 : 0);
			AttachmentPolicy.Level treatUnknownTypeAs = ConfigurationBase.AttachmentActionToLevel(adowaVirtualDirectory.ActionForUnknownFileAndMIMETypes);
			AttachmentPolicy attachmentPolicy = new AttachmentPolicy(adowaVirtualDirectory.BlockedFileTypes.ToArray(), adowaVirtualDirectory.BlockedMimeTypes.ToArray(), adowaVirtualDirectory.ForceSaveFileTypes.ToArray(), adowaVirtualDirectory.ForceSaveMimeTypes.ToArray(), adowaVirtualDirectory.AllowedFileTypes.ToArray(), adowaVirtualDirectory.AllowedMimeTypes.ToArray(), treatUnknownTypeAs, adowaVirtualDirectory.DirectFileAccessOnPublicComputersEnabled.Value, adowaVirtualDirectory.DirectFileAccessOnPrivateComputersEnabled.Value, adowaVirtualDirectory.ForceWebReadyDocumentViewingFirstOnPublicComputers.Value, adowaVirtualDirectory.ForceWebReadyDocumentViewingFirstOnPrivateComputers.Value, adowaVirtualDirectory.WebReadyDocumentViewingOnPublicComputersEnabled.Value, adowaVirtualDirectory.WebReadyDocumentViewingOnPrivateComputersEnabled.Value, adowaVirtualDirectory.WebReadyFileTypes.ToArray(), adowaVirtualDirectory.WebReadyMimeTypes.ToArray(), adowaVirtualDirectory.WebReadyDocumentViewingSupportedFileTypes.ToArray(), adowaVirtualDirectory.WebReadyDocumentViewingSupportedMimeTypes.ToArray(), adowaVirtualDirectory.WebReadyDocumentViewingForAllSupportedTypes.Value);
			base.AttachmentPolicy = attachmentPolicy;
			base.DefaultClientLanguage = adowaVirtualDirectory.DefaultClientLanguage.Value;
			this.filterWebBeaconsAndHtmlForms = adowaVirtualDirectory.FilterWebBeaconsAndHtmlForms.Value;
			base.LogonAndErrorLanguage = adowaVirtualDirectory.LogonAndErrorLanguage;
			this.logonFormat = adowaVirtualDirectory.LogonFormat;
			this.defaultDomain = adowaVirtualDirectory.DefaultDomain;
			this.notificationInterval = (adowaVirtualDirectory.NotificationInterval ?? 120);
			this.sessionTimeout = (adowaVirtualDirectory.UserContextTimeout ?? 60);
			this.redirectToOptimalOWAServer = (adowaVirtualDirectory.RedirectToOptimalOWAServer == true);
			base.DefaultTheme = adowaVirtualDirectory.DefaultTheme;
			base.SetPhotoURL = adowaVirtualDirectory.SetPhotoURL;
			this.clientAuthCleanupLevel = adowaVirtualDirectory.ClientAuthCleanupLevel;
			this.imCertificateThumbprint = adowaVirtualDirectory.InstantMessagingCertificateThumbprint;
			this.imServerName = adowaVirtualDirectory.InstantMessagingServerName;
			this.isSMimeEnabledOnCurrentServerr = (adowaVirtualDirectory.SMimeEnabled ?? false);
			this.documentAccessAllowedServers = adowaVirtualDirectory.RemoteDocumentsAllowedServers.ToArray();
			this.documentAccessBlockedServers = adowaVirtualDirectory.RemoteDocumentsBlockedServers.ToArray();
			this.documentAccessInternalDomainSuffixList = adowaVirtualDirectory.RemoteDocumentsInternalDomainSuffixList.ToArray();
			RemoteDocumentsActions? remoteDocumentsActions = adowaVirtualDirectory.RemoteDocumentsActionForUnknownServers;
			if (remoteDocumentsActions != null)
			{
				if (remoteDocumentsActions == RemoteDocumentsActions.Allow)
				{
					this.remoteDocumentsActionForUnknownServers = RemoteDocumentsActions.Allow;
				}
				else
				{
					this.remoteDocumentsActionForUnknownServers = RemoteDocumentsActions.Block;
				}
			}
			base.InternalAuthenticationMethod = ConfigurationBase.GetAuthenticationMethod(adowaVirtualDirectory[ADVirtualDirectorySchema.InternalAuthenticationMethodFlags]);
			base.ExternalAuthenticationMethod = ConfigurationBase.GetAuthenticationMethod(adowaVirtualDirectory[ADVirtualDirectorySchema.ExternalAuthenticationMethodFlags]);
			base.Exchange2003Url = adowaVirtualDirectory.Exchange2003Url;
			base.LegacyRedirectType = LegacyRedirectTypeOptions.Silent;
			int segmentationBits = (int)adowaVirtualDirectory[ADOwaVirtualDirectorySchema.ADMailboxFolderSet];
			int segmentationBits2 = (int)adowaVirtualDirectory[ADOwaVirtualDirectorySchema.ADMailboxFolderSet2];
			base.SegmentationFlags = Utilities.SetSegmentationFlags(segmentationBits, segmentationBits2);
			if (adowaVirtualDirectory.OutboundCharset != null)
			{
				base.OutboundCharset = adowaVirtualDirectory.OutboundCharset.Value;
			}
			if (adowaVirtualDirectory.UseGB18030 != null && adowaVirtualDirectory.UseGB18030.Value)
			{
				base.UseGB18030 = true;
			}
			else
			{
				base.UseGB18030 = false;
			}
			if (adowaVirtualDirectory.UseISO885915 != null && adowaVirtualDirectory.UseISO885915.Value)
			{
				base.UseISO885915 = true;
			}
			else
			{
				base.UseISO885915 = false;
			}
			base.InstantMessagingType = ((adowaVirtualDirectory.InstantMessagingType != null) ? adowaVirtualDirectory.InstantMessagingType.Value : InstantMessagingTypeOptions.None);
			this.defaultAcceptedDomain = session.GetDefaultAcceptedDomain();
			this.publicFoldersEnabledOnThisVdir = (adowaVirtualDirectory.PublicFoldersEnabled ?? false);
			this.ExpirationTime = DateTime.UtcNow + Configuration.expirationPeriod;
			OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ConfigurationSettingsUpdated, string.Empty, new object[]
			{
				virtualDirectory,
				webSiteName
			});
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0003BF06 File Offset: 0x0003A106
		protected Configuration()
		{
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0003BF24 File Offset: 0x0003A124
		private string GetAccessProxyAddress()
		{
			string result = string.Empty;
			CachedOrganizationConfiguration instance = CachedOrganizationConfiguration.GetInstance(OrganizationId.ForestWideOrgId, CachedOrganizationConfiguration.ConfigurationTypes.All);
			Organization configuration = instance.OrganizationConfiguration.Configuration;
			ProtocolConnectionSettings sipaccessService = configuration.SIPAccessService;
			if (sipaccessService != null)
			{
				result = sipaccessService.Hostname.ToString();
			}
			return result;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0003BF6A File Offset: 0x0003A16A
		internal WebBeaconFilterLevels FilterWebBeaconsAndHtmlForms
		{
			get
			{
				return this.filterWebBeaconsAndHtmlForms;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0003BF72 File Offset: 0x0003A172
		public LogonFormats LogonFormat
		{
			get
			{
				return this.logonFormat;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0003BF7A File Offset: 0x0003A17A
		public string DefaultDomain
		{
			get
			{
				return this.defaultDomain;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0003BF82 File Offset: 0x0003A182
		public int NotificationInterval
		{
			get
			{
				return this.notificationInterval;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0003BF8A File Offset: 0x0003A18A
		public int SessionTimeout
		{
			get
			{
				return this.sessionTimeout;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0003BF92 File Offset: 0x0003A192
		public bool FormsAuthenticationEnabled
		{
			get
			{
				return this.formsAuthenticationEnabled != 0;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0003BFA0 File Offset: 0x0003A1A0
		public bool RedirectToOptimalOWAServer
		{
			get
			{
				return this.redirectToOptimalOWAServer;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0003BFA8 File Offset: 0x0003A1A8
		public ClientAuthCleanupLevels ClientAuthCleanupLevel
		{
			get
			{
				return this.clientAuthCleanupLevel;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0003BFB0 File Offset: 0x0003A1B0
		public string[] BlockedDocumentStoreList
		{
			get
			{
				return this.documentAccessBlockedServers;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0003BFB8 File Offset: 0x0003A1B8
		public string[] AllowedDocumentStoreList
		{
			get
			{
				return this.documentAccessAllowedServers;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0003BFC0 File Offset: 0x0003A1C0
		internal RemoteDocumentsActions RemoteDocumentsActionForUnknownServers
		{
			get
			{
				return this.remoteDocumentsActionForUnknownServers;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0003BFC8 File Offset: 0x0003A1C8
		public string[] InternalFQDNSuffixList
		{
			get
			{
				return this.documentAccessInternalDomainSuffixList;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0003BFD0 File Offset: 0x0003A1D0
		internal virtual AcceptedDomain DefaultAcceptedDomain
		{
			get
			{
				if (this.defaultAcceptedDomain == null)
				{
					throw new OwaInvalidConfigurationException("No default accepted domain found.");
				}
				return this.defaultAcceptedDomain;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0003BFEB File Offset: 0x0003A1EB
		internal bool IsPublicFoldersEnabledOnThisVdir
		{
			get
			{
				return this.publicFoldersEnabledOnThisVdir;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x0003BFF3 File Offset: 0x0003A1F3
		internal bool IsSMimeEnabledOnCurrentServerr
		{
			get
			{
				return this.isSMimeEnabledOnCurrentServerr;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0003BFFB File Offset: 0x0003A1FB
		internal string InstantMessagingCertificateThumbprint
		{
			get
			{
				return this.imCertificateThumbprint;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0003C004 File Offset: 0x0003A204
		internal string InstantMessagingServerName
		{
			get
			{
				if (string.IsNullOrEmpty(this.imServerName) && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.UseAccessProxyForInstantMessagingServerName.Enabled)
				{
					this.imServerName = this.GetAccessProxyAddress();
				}
				return this.imServerName;
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0003C050 File Offset: 0x0003A250
		private void LoadPreCheckInVdirConfiguration()
		{
			AttachmentPolicy attachmentPolicy = new AttachmentPolicy(new StringArrayAppSettingsEntry("BlockFileTypes", new string[]
			{
				".vsmacros",
				".msh2xml",
				".msh1xml",
				".ps2xml",
				".ps1xml",
				".mshxml",
				".mhtml",
				".psc2",
				".psc1",
				".msh2",
				".msh1",
				".aspx",
				".xml",
				".wsh",
				".wsf",
				".wsc",
				".vsw",
				".vst",
				".vss",
				".vbs",
				".vbe",
				".url",
				".tmp",
				".shs",
				".shb",
				".sct",
				".scr",
				".scf",
				".reg",
				".pst",
				".ps2",
				".ps1",
				".prg",
				".prf",
				".plg",
				".pif",
				".pcd",
				".ops",
				".mst",
				".msp",
				".msi",
				".msh",
				".msc",
				".mht",
				".mdz",
				".mdw",
				".mdt",
				".mde",
				".mdb",
				".mda",
				".maw",
				".mav",
				".mau",
				".mat",
				".mas",
				".mar",
				".maq",
				".mam",
				".mag",
				".maf",
				".mad",
				".lnk",
				".ksh",
				".jse",
				".its",
				".isp",
				".ins",
				".inf",
				".htc",
				".hta",
				".hlp",
				".fxp",
				".exe",
				".der",
				".csh",
				".crt",
				".cpl",
				".com",
				".cmd",
				".chm",
				".cer",
				".bat",
				".bas",
				".asx",
				".asp",
				".app",
				".adp",
				".ade",
				".ws",
				".vb",
				".js"
			}, null).Value, new StringArrayAppSettingsEntry("BlockMimeTypes", new string[]
			{
				"application/x-javascript",
				"application/javascript",
				"application/msaccess",
				"x-internet-signup",
				"text/javascript",
				"application/xml",
				"application/prg",
				"application/hta",
				"text/scriplet",
				"text/xml"
			}, null).Value, new StringArrayAppSettingsEntry("ForceSaveFileTypes", new string[]
			{
				".vsmacros",
				".msh2xml",
				".msh1xml",
				".ps2xml",
				".ps1xml",
				".mshxml",
				".mhtml",
				".psc2",
				".psc1",
				".msh2",
				".msh1",
				".aspx",
				".xml",
				".wsh",
				".wsf",
				".wsc",
				".vsw",
				".vst",
				".vss",
				".vbs",
				".vbe",
				".url",
				".tmp",
				".shs",
				".shb",
				".sct",
				".scr",
				".scf",
				".reg",
				".pst",
				".ps2",
				".ps1",
				".prg",
				".prf",
				".plg",
				".pif",
				".pcd",
				".ops",
				".mst",
				".msp",
				".msi",
				".msh",
				".msc",
				".mht",
				".mdz",
				".mdw",
				".mdt",
				".mde",
				".mdb",
				".mda",
				".maw",
				".mav",
				".mau",
				".mat",
				".mas",
				".mar",
				".maq",
				".mam",
				".mag",
				".maf",
				".mad",
				".lnk",
				".ksh",
				".jse",
				".its",
				".isp",
				".ins",
				".inf",
				".htc",
				".hta",
				".hlp",
				".fxp",
				".exe",
				".der",
				".csh",
				".crt",
				".cpl",
				".com",
				".cmd",
				".chm",
				".cer",
				".bat",
				".bas",
				".asx",
				".asp",
				".app",
				".adp",
				".ade",
				".ws",
				".vb",
				".js"
			}, null).Value, new StringArrayAppSettingsEntry("ForceSaveMimeTypes", new string[]
			{
				"Application/x-shockwave-flash",
				"Application/octet-stream",
				"Application/futuresplash",
				"Application/x-director"
			}, null).Value, new StringArrayAppSettingsEntry("AllowFileTypes", new string[]
			{
				".rpmsg",
				".xlsx",
				".xlsm",
				".xlsb",
				".pptx",
				".pptm",
				".ppsx",
				".ppsm",
				".docx",
				".docm",
				".zip",
				".xls",
				".wmv",
				".wma",
				".wav",
				".vsd",
				".txt",
				".tif",
				".rtf",
				".pub",
				".ppt",
				".png",
				".pdf",
				".one",
				".mp3",
				".jpg",
				".gif",
				".doc",
				".bmp",
				".avi"
			}, null).Value, new StringArrayAppSettingsEntry("AllowMimeTypes", new string[0], null).Value, (AttachmentPolicy.Level)Enum.Parse(typeof(AttachmentPolicy.Level), new StringAppSettingsEntry("ActionForUnknownFileAndMIMETypes", "ForceSave", null).Value), new BoolAppSettingsEntry("DirectFileAccessOnPublicComputersEnabled", true, null).Value, new BoolAppSettingsEntry("DirectFileAccessOnPrivateComputersEnabled", true, null).Value, new BoolAppSettingsEntry("ForceWebReadyDocumentViewingFirstOnPublicComputers", false, null).Value, new BoolAppSettingsEntry("ForceWebReadyDocumentViewingFirstOnPrivateComputers", false, null).Value, new BoolAppSettingsEntry("WebReadyDocumentViewingOnPublicComputersEnabled", true, null).Value, new BoolAppSettingsEntry("WebReadyDocumentViewingOnPrivateComputersEnabled", true, null).Value, new StringArrayAppSettingsEntry("WebReadyFileTypes", new string[]
			{
				".xlsx",
				".xlsm",
				".xlsb",
				".pptx",
				".pptm",
				".ppsx",
				".ppsm",
				".docx",
				".docm",
				".xls",
				".rtf",
				".pdf"
			}, null).Value, new StringArrayAppSettingsEntry("WebReadyMimeTypes", new string[0], null).Value, new StringArrayAppSettingsEntry("WebReadyDocumentViewingSupportedFileTypes", new string[]
			{
				".xlsx",
				".xlsm",
				".xlsb",
				".pptx",
				".pptm",
				".ppsx",
				".ppsm",
				".docx",
				".docm",
				".xls",
				".rtf",
				".pdf"
			}, null).Value, new StringArrayAppSettingsEntry("WebReadyDocumentViewingSupportedMimeTypes", new string[0], null).Value, new BoolAppSettingsEntry("WebReadyDocumentViewingForAllSupportedTypes", false, null).Value);
			base.AttachmentPolicy = attachmentPolicy;
			string value = new StringAppSettingsEntry("FilterWebBeaconsAndHtmlForms", "UserFilterChoice", null).Value;
			this.filterWebBeaconsAndHtmlForms = (WebBeaconFilterLevels)Enum.Parse(typeof(WebBeaconFilterLevels), value);
			base.DefaultTheme = new StringAppSettingsEntry("DefaultTheme", string.Empty, null).Value;
			base.SegmentationFlags = (ulong)Enum.Parse(typeof(Feature), new StringAppSettingsEntry("SegmentationFlags", "All", null).Value);
			base.OutboundCharset = (OutboundCharsetOptions)Enum.Parse(typeof(OutboundCharsetOptions), new StringAppSettingsEntry("OutboundCharset", "AutoDetect", null).Value);
			base.UseGB18030 = new BoolAppSettingsEntry("UseGB18030", false, null).Value;
			base.UseISO885915 = new BoolAppSettingsEntry("UseISO885915", false, null).Value;
			base.InstantMessagingType = (InstantMessagingTypeOptions)Enum.Parse(typeof(InstantMessagingTypeOptions), new StringAppSettingsEntry("InstantMessagingType", "Ocs", null).Value);
			this.imServerName = new StringAppSettingsEntry("InstantMessagingServerName", string.Empty, null).Value;
			this.formsAuthenticationEnabled = (new BoolAppSettingsEntry("FormsAuthenticationEnabled", true, null).Value ? 1 : 0);
			this.publicFoldersEnabledOnThisVdir = new BoolAppSettingsEntry("PublicFoldersEnabled", false, null).Value;
			this.notificationInterval = new IntAppSettingsEntry("NotificationInterval", 120, null).Value;
			this.sessionTimeout = new IntAppSettingsEntry("UserContextTimeout", 60, null).Value;
		}

		// Token: 0x040005C8 RID: 1480
		private const int DefaultNotificationInterval = 120;

		// Token: 0x040005C9 RID: 1481
		private const int DefaultSessionTimeOut = 60;

		// Token: 0x040005CA RID: 1482
		private static TimeSpan expirationPeriod = new TimeSpan(0, 3, 0, 0);

		// Token: 0x040005CB RID: 1483
		protected LogonFormats logonFormat;

		// Token: 0x040005CC RID: 1484
		protected string defaultDomain;

		// Token: 0x040005CD RID: 1485
		protected int notificationInterval = 120;

		// Token: 0x040005CE RID: 1486
		protected int sessionTimeout;

		// Token: 0x040005CF RID: 1487
		protected int formsAuthenticationEnabled;

		// Token: 0x040005D0 RID: 1488
		protected WebBeaconFilterLevels filterWebBeaconsAndHtmlForms;

		// Token: 0x040005D1 RID: 1489
		protected string[] documentAccessAllowedServers;

		// Token: 0x040005D2 RID: 1490
		protected string[] documentAccessBlockedServers;

		// Token: 0x040005D3 RID: 1491
		protected string[] documentAccessInternalDomainSuffixList;

		// Token: 0x040005D4 RID: 1492
		protected RemoteDocumentsActions remoteDocumentsActionForUnknownServers = RemoteDocumentsActions.Block;

		// Token: 0x040005D5 RID: 1493
		protected bool redirectToOptimalOWAServer = true;

		// Token: 0x040005D6 RID: 1494
		protected ClientAuthCleanupLevels clientAuthCleanupLevel;

		// Token: 0x040005D7 RID: 1495
		protected bool isSMimeEnabledOnCurrentServerr;

		// Token: 0x040005D8 RID: 1496
		protected bool publicFoldersEnabledOnThisVdir;

		// Token: 0x040005D9 RID: 1497
		protected string imCertificateThumbprint;

		// Token: 0x040005DA RID: 1498
		protected string imServerName;

		// Token: 0x040005DB RID: 1499
		protected AcceptedDomain defaultAcceptedDomain;

		// Token: 0x040005DC RID: 1500
		public readonly DateTime ExpirationTime;
	}
}
