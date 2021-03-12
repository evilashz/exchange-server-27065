using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005DF RID: 1503
	[DataServiceKey("objectId")]
	public class Application : DirectoryObject
	{
		// Token: 0x060018A8 RID: 6312 RVA: 0x0002FB98 File Offset: 0x0002DD98
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Application CreateApplication(string objectId, Collection<AppPermission> appPermissions, Collection<string> identifierUris, Collection<KeyCredential> keyCredentials, Collection<Guid> knownClientApplications, DataServiceStreamLink mainLogo, Collection<PasswordCredential> passwordCredentials, Collection<string> replyUrls, Collection<RequiredResourceAccess> requiredResourceAccess)
		{
			Application application = new Application();
			application.objectId = objectId;
			if (appPermissions == null)
			{
				throw new ArgumentNullException("appPermissions");
			}
			application.appPermissions = appPermissions;
			if (identifierUris == null)
			{
				throw new ArgumentNullException("identifierUris");
			}
			application.identifierUris = identifierUris;
			if (keyCredentials == null)
			{
				throw new ArgumentNullException("keyCredentials");
			}
			application.keyCredentials = keyCredentials;
			if (knownClientApplications == null)
			{
				throw new ArgumentNullException("knownClientApplications");
			}
			application.knownClientApplications = knownClientApplications;
			application.mainLogo = mainLogo;
			if (passwordCredentials == null)
			{
				throw new ArgumentNullException("passwordCredentials");
			}
			application.passwordCredentials = passwordCredentials;
			if (replyUrls == null)
			{
				throw new ArgumentNullException("replyUrls");
			}
			application.replyUrls = replyUrls;
			if (requiredResourceAccess == null)
			{
				throw new ArgumentNullException("requiredResourceAccess");
			}
			application.requiredResourceAccess = requiredResourceAccess;
			return application;
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0002FC56 File Offset: 0x0002DE56
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x0002FC5E File Offset: 0x0002DE5E
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? appId
		{
			get
			{
				return this._appId;
			}
			set
			{
				this._appId = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0002FC67 File Offset: 0x0002DE67
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x0002FC91 File Offset: 0x0002DE91
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public AppMetadata appMetadata
		{
			get
			{
				if (this._appMetadata == null && !this._appMetadataInitialized)
				{
					this._appMetadata = new AppMetadata();
					this._appMetadataInitialized = true;
				}
				return this._appMetadata;
			}
			set
			{
				this._appMetadata = value;
				this._appMetadataInitialized = true;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0002FCA1 File Offset: 0x0002DEA1
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0002FCA9 File Offset: 0x0002DEA9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<AppPermission> appPermissions
		{
			get
			{
				return this._appPermissions;
			}
			set
			{
				this._appPermissions = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0002FCB2 File Offset: 0x0002DEB2
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x0002FCBA File Offset: 0x0002DEBA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? availableToOtherTenants
		{
			get
			{
				return this._availableToOtherTenants;
			}
			set
			{
				this._availableToOtherTenants = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0002FCC3 File Offset: 0x0002DEC3
		// (set) Token: 0x060018B2 RID: 6322 RVA: 0x0002FCCB File Offset: 0x0002DECB
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string displayName
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x0002FCD4 File Offset: 0x0002DED4
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x0002FCDC File Offset: 0x0002DEDC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string errorUrl
		{
			get
			{
				return this._errorUrl;
			}
			set
			{
				this._errorUrl = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x0002FCE5 File Offset: 0x0002DEE5
		// (set) Token: 0x060018B6 RID: 6326 RVA: 0x0002FCED File Offset: 0x0002DEED
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string homepage
		{
			get
			{
				return this._homepage;
			}
			set
			{
				this._homepage = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0002FCF6 File Offset: 0x0002DEF6
		// (set) Token: 0x060018B8 RID: 6328 RVA: 0x0002FCFE File Offset: 0x0002DEFE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> identifierUris
		{
			get
			{
				return this._identifierUris;
			}
			set
			{
				this._identifierUris = value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0002FD07 File Offset: 0x0002DF07
		// (set) Token: 0x060018BA RID: 6330 RVA: 0x0002FD0F File Offset: 0x0002DF0F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<KeyCredential> keyCredentials
		{
			get
			{
				return this._keyCredentials;
			}
			set
			{
				this._keyCredentials = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0002FD18 File Offset: 0x0002DF18
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x0002FD20 File Offset: 0x0002DF20
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<Guid> knownClientApplications
		{
			get
			{
				return this._knownClientApplications;
			}
			set
			{
				this._knownClientApplications = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x0002FD29 File Offset: 0x0002DF29
		// (set) Token: 0x060018BE RID: 6334 RVA: 0x0002FD31 File Offset: 0x0002DF31
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceStreamLink mainLogo
		{
			get
			{
				return this._mainLogo;
			}
			set
			{
				this._mainLogo = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x0002FD3A File Offset: 0x0002DF3A
		// (set) Token: 0x060018C0 RID: 6336 RVA: 0x0002FD42 File Offset: 0x0002DF42
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string logoutUrl
		{
			get
			{
				return this._logoutUrl;
			}
			set
			{
				this._logoutUrl = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0002FD4B File Offset: 0x0002DF4B
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x0002FD53 File Offset: 0x0002DF53
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<PasswordCredential> passwordCredentials
		{
			get
			{
				return this._passwordCredentials;
			}
			set
			{
				this._passwordCredentials = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0002FD5C File Offset: 0x0002DF5C
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x0002FD64 File Offset: 0x0002DF64
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? publicClient
		{
			get
			{
				return this._publicClient;
			}
			set
			{
				this._publicClient = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x0002FD6D File Offset: 0x0002DF6D
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x0002FD75 File Offset: 0x0002DF75
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> replyUrls
		{
			get
			{
				return this._replyUrls;
			}
			set
			{
				this._replyUrls = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0002FD7E File Offset: 0x0002DF7E
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x0002FD86 File Offset: 0x0002DF86
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<RequiredResourceAccess> requiredResourceAccess
		{
			get
			{
				return this._requiredResourceAccess;
			}
			set
			{
				this._requiredResourceAccess = value;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0002FD8F File Offset: 0x0002DF8F
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x0002FD97 File Offset: 0x0002DF97
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string resourceApplicationSet
		{
			get
			{
				return this._resourceApplicationSet;
			}
			set
			{
				this._resourceApplicationSet = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0002FDA0 File Offset: 0x0002DFA0
		// (set) Token: 0x060018CC RID: 6348 RVA: 0x0002FDA8 File Offset: 0x0002DFA8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string samlMetadataUrl
		{
			get
			{
				return this._samlMetadataUrl;
			}
			set
			{
				this._samlMetadataUrl = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0002FDB1 File Offset: 0x0002DFB1
		// (set) Token: 0x060018CE RID: 6350 RVA: 0x0002FDB9 File Offset: 0x0002DFB9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? webApi
		{
			get
			{
				return this._webApi;
			}
			set
			{
				this._webApi = value;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0002FDC2 File Offset: 0x0002DFC2
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x0002FDCA File Offset: 0x0002DFCA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? webApp
		{
			get
			{
				return this._webApp;
			}
			set
			{
				this._webApp = value;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0002FDD3 File Offset: 0x0002DFD3
		// (set) Token: 0x060018D2 RID: 6354 RVA: 0x0002FDDB File Offset: 0x0002DFDB
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<Notification> notifications
		{
			get
			{
				return this._notifications;
			}
			set
			{
				if (value != null)
				{
					this._notifications = value;
				}
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0002FDE7 File Offset: 0x0002DFE7
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x0002FDEF File Offset: 0x0002DFEF
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> defaultPolicy
		{
			get
			{
				return this._defaultPolicy;
			}
			set
			{
				if (value != null)
				{
					this._defaultPolicy = value;
				}
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0002FDFB File Offset: 0x0002DFFB
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x0002FE03 File Offset: 0x0002E003
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ServiceEndpoint> serviceEndpoints
		{
			get
			{
				return this._serviceEndpoints;
			}
			set
			{
				if (value != null)
				{
					this._serviceEndpoints = value;
				}
			}
		}

		// Token: 0x04001B2E RID: 6958
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _appId;

		// Token: 0x04001B2F RID: 6959
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private AppMetadata _appMetadata;

		// Token: 0x04001B30 RID: 6960
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _appMetadataInitialized;

		// Token: 0x04001B31 RID: 6961
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppPermission> _appPermissions = new Collection<AppPermission>();

		// Token: 0x04001B32 RID: 6962
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _availableToOtherTenants;

		// Token: 0x04001B33 RID: 6963
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001B34 RID: 6964
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorUrl;

		// Token: 0x04001B35 RID: 6965
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _homepage;

		// Token: 0x04001B36 RID: 6966
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _identifierUris = new Collection<string>();

		// Token: 0x04001B37 RID: 6967
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<KeyCredential> _keyCredentials = new Collection<KeyCredential>();

		// Token: 0x04001B38 RID: 6968
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<Guid> _knownClientApplications = new Collection<Guid>();

		// Token: 0x04001B39 RID: 6969
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _mainLogo;

		// Token: 0x04001B3A RID: 6970
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _logoutUrl;

		// Token: 0x04001B3B RID: 6971
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<PasswordCredential> _passwordCredentials = new Collection<PasswordCredential>();

		// Token: 0x04001B3C RID: 6972
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _publicClient;

		// Token: 0x04001B3D RID: 6973
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _replyUrls = new Collection<string>();

		// Token: 0x04001B3E RID: 6974
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<RequiredResourceAccess> _requiredResourceAccess = new Collection<RequiredResourceAccess>();

		// Token: 0x04001B3F RID: 6975
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceApplicationSet;

		// Token: 0x04001B40 RID: 6976
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _samlMetadataUrl;

		// Token: 0x04001B41 RID: 6977
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApi;

		// Token: 0x04001B42 RID: 6978
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApp;

		// Token: 0x04001B43 RID: 6979
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<Notification> _notifications = new Collection<Notification>();

		// Token: 0x04001B44 RID: 6980
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _defaultPolicy = new Collection<DirectoryObject>();

		// Token: 0x04001B45 RID: 6981
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceEndpoint> _serviceEndpoints = new Collection<ServiceEndpoint>();
	}
}
