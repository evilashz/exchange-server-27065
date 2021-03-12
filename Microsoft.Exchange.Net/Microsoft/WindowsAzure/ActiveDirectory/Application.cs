using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x0200059A RID: 1434
	[DataServiceKey("objectId")]
	public class Application : DirectoryObject
	{
		// Token: 0x060013C6 RID: 5062 RVA: 0x0002BE44 File Offset: 0x0002A044
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Application CreateApplication(string objectId, Collection<AppPermission> appPermissions, Collection<string> identifierUris, Collection<KeyCredential> keyCredentials, DataServiceStreamLink mainLogo, Collection<PasswordCredential> passwordCredentials, Collection<string> replyUrls, Collection<RequiredResourceAccess> requiredResourceAccess)
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

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0002BEEB File Offset: 0x0002A0EB
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x0002BEF3 File Offset: 0x0002A0F3
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

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0002BEFC File Offset: 0x0002A0FC
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0002BF26 File Offset: 0x0002A126
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

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0002BF36 File Offset: 0x0002A136
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x0002BF3E File Offset: 0x0002A13E
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

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0002BF47 File Offset: 0x0002A147
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x0002BF4F File Offset: 0x0002A14F
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

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0002BF58 File Offset: 0x0002A158
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x0002BF60 File Offset: 0x0002A160
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0002BF69 File Offset: 0x0002A169
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x0002BF71 File Offset: 0x0002A171
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

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0002BF7A File Offset: 0x0002A17A
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x0002BF82 File Offset: 0x0002A182
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

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0002BF8B File Offset: 0x0002A18B
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0002BF93 File Offset: 0x0002A193
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

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0002BF9C File Offset: 0x0002A19C
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
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

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0002BFAD File Offset: 0x0002A1AD
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x0002BFB5 File Offset: 0x0002A1B5
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

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0002BFBE File Offset: 0x0002A1BE
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x0002BFC6 File Offset: 0x0002A1C6
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

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0002BFCF File Offset: 0x0002A1CF
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x0002BFD7 File Offset: 0x0002A1D7
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

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0002BFE0 File Offset: 0x0002A1E0
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0002BFE8 File Offset: 0x0002A1E8
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

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0002BFF1 File Offset: 0x0002A1F1
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x0002BFF9 File Offset: 0x0002A1F9
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

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0002C002 File Offset: 0x0002A202
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x0002C00A File Offset: 0x0002A20A
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

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0002C013 File Offset: 0x0002A213
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0002C01B File Offset: 0x0002A21B
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

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0002C024 File Offset: 0x0002A224
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x0002C02C File Offset: 0x0002A22C
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

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0002C035 File Offset: 0x0002A235
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x0002C03D File Offset: 0x0002A23D
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

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0002C046 File Offset: 0x0002A246
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x0002C04E File Offset: 0x0002A24E
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

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0002C057 File Offset: 0x0002A257
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x0002C05F File Offset: 0x0002A25F
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0002C06B File Offset: 0x0002A26B
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x0002C073 File Offset: 0x0002A273
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

		// Token: 0x040018EE RID: 6382
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _appId;

		// Token: 0x040018EF RID: 6383
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private AppMetadata _appMetadata;

		// Token: 0x040018F0 RID: 6384
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _appMetadataInitialized;

		// Token: 0x040018F1 RID: 6385
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppPermission> _appPermissions = new Collection<AppPermission>();

		// Token: 0x040018F2 RID: 6386
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _availableToOtherTenants;

		// Token: 0x040018F3 RID: 6387
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x040018F4 RID: 6388
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorUrl;

		// Token: 0x040018F5 RID: 6389
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _homepage;

		// Token: 0x040018F6 RID: 6390
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _identifierUris = new Collection<string>();

		// Token: 0x040018F7 RID: 6391
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<KeyCredential> _keyCredentials = new Collection<KeyCredential>();

		// Token: 0x040018F8 RID: 6392
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _mainLogo;

		// Token: 0x040018F9 RID: 6393
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _logoutUrl;

		// Token: 0x040018FA RID: 6394
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<PasswordCredential> _passwordCredentials = new Collection<PasswordCredential>();

		// Token: 0x040018FB RID: 6395
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _publicClient;

		// Token: 0x040018FC RID: 6396
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _replyUrls = new Collection<string>();

		// Token: 0x040018FD RID: 6397
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<RequiredResourceAccess> _requiredResourceAccess = new Collection<RequiredResourceAccess>();

		// Token: 0x040018FE RID: 6398
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceApplicationSet;

		// Token: 0x040018FF RID: 6399
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _samlMetadataUrl;

		// Token: 0x04001900 RID: 6400
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApi;

		// Token: 0x04001901 RID: 6401
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApp;

		// Token: 0x04001902 RID: 6402
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<Notification> _notifications = new Collection<Notification>();

		// Token: 0x04001903 RID: 6403
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceEndpoint> _serviceEndpoints = new Collection<ServiceEndpoint>();
	}
}
