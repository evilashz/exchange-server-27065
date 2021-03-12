using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005FB RID: 1531
	[DataServiceKey("objectId")]
	public class ServicePrincipal : DirectoryObject
	{
		// Token: 0x06001ACC RID: 6860 RVA: 0x000316C4 File Offset: 0x0002F8C4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ServicePrincipal CreateServicePrincipal(string objectId, Collection<AppPermission> appPermissions, bool explicitAccessGrantRequired, Collection<KeyCredential> keyCredentials, Collection<PasswordCredential> passwordCredentials, Collection<string> replyUrls, Collection<string> servicePrincipalNames, Collection<string> tags)
		{
			ServicePrincipal servicePrincipal = new ServicePrincipal();
			servicePrincipal.objectId = objectId;
			if (appPermissions == null)
			{
				throw new ArgumentNullException("appPermissions");
			}
			servicePrincipal.appPermissions = appPermissions;
			servicePrincipal.explicitAccessGrantRequired = explicitAccessGrantRequired;
			if (keyCredentials == null)
			{
				throw new ArgumentNullException("keyCredentials");
			}
			servicePrincipal.keyCredentials = keyCredentials;
			if (passwordCredentials == null)
			{
				throw new ArgumentNullException("passwordCredentials");
			}
			servicePrincipal.passwordCredentials = passwordCredentials;
			if (replyUrls == null)
			{
				throw new ArgumentNullException("replyUrls");
			}
			servicePrincipal.replyUrls = replyUrls;
			if (servicePrincipalNames == null)
			{
				throw new ArgumentNullException("servicePrincipalNames");
			}
			servicePrincipal.servicePrincipalNames = servicePrincipalNames;
			if (tags == null)
			{
				throw new ArgumentNullException("tags");
			}
			servicePrincipal.tags = tags;
			return servicePrincipal;
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0003176C File Offset: 0x0002F96C
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x00031774 File Offset: 0x0002F974
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? accountEnabled
		{
			get
			{
				return this._accountEnabled;
			}
			set
			{
				this._accountEnabled = value;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0003177D File Offset: 0x0002F97D
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x00031785 File Offset: 0x0002F985
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string appDisplayName
		{
			get
			{
				return this._appDisplayName;
			}
			set
			{
				this._appDisplayName = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0003178E File Offset: 0x0002F98E
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x00031796 File Offset: 0x0002F996
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

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0003179F File Offset: 0x0002F99F
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x000317C9 File Offset: 0x0002F9C9
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

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x000317D9 File Offset: 0x0002F9D9
		// (set) Token: 0x06001AD6 RID: 6870 RVA: 0x000317E1 File Offset: 0x0002F9E1
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? appOwnerTenantId
		{
			get
			{
				return this._appOwnerTenantId;
			}
			set
			{
				this._appOwnerTenantId = value;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x000317EA File Offset: 0x0002F9EA
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x000317F2 File Offset: 0x0002F9F2
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

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x000317FB File Offset: 0x0002F9FB
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x00031825 File Offset: 0x0002FA25
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public ServicePrincipalAuthenticationPolicy authenticationPolicy
		{
			get
			{
				if (this._authenticationPolicy == null && !this._authenticationPolicyInitialized)
				{
					this._authenticationPolicy = new ServicePrincipalAuthenticationPolicy();
					this._authenticationPolicyInitialized = true;
				}
				return this._authenticationPolicy;
			}
			set
			{
				this._authenticationPolicy = value;
				this._authenticationPolicyInitialized = true;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x00031835 File Offset: 0x0002FA35
		// (set) Token: 0x06001ADC RID: 6876 RVA: 0x0003183D File Offset: 0x0002FA3D
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

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x00031846 File Offset: 0x0002FA46
		// (set) Token: 0x06001ADE RID: 6878 RVA: 0x0003184E File Offset: 0x0002FA4E
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

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x00031857 File Offset: 0x0002FA57
		// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x0003185F File Offset: 0x0002FA5F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool explicitAccessGrantRequired
		{
			get
			{
				return this._explicitAccessGrantRequired;
			}
			set
			{
				this._explicitAccessGrantRequired = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x00031868 File Offset: 0x0002FA68
		// (set) Token: 0x06001AE2 RID: 6882 RVA: 0x00031870 File Offset: 0x0002FA70
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

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00031879 File Offset: 0x0002FA79
		// (set) Token: 0x06001AE4 RID: 6884 RVA: 0x00031881 File Offset: 0x0002FA81
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

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0003188A File Offset: 0x0002FA8A
		// (set) Token: 0x06001AE6 RID: 6886 RVA: 0x00031892 File Offset: 0x0002FA92
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

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0003189B File Offset: 0x0002FA9B
		// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x000318A3 File Offset: 0x0002FAA3
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? microsoftFirstParty
		{
			get
			{
				return this._microsoftFirstParty;
			}
			set
			{
				this._microsoftFirstParty = value;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x000318AC File Offset: 0x0002FAAC
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x000318B4 File Offset: 0x0002FAB4
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

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x000318BD File Offset: 0x0002FABD
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x000318C5 File Offset: 0x0002FAC5
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string publisherName
		{
			get
			{
				return this._publisherName;
			}
			set
			{
				this._publisherName = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x000318CE File Offset: 0x0002FACE
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x000318D6 File Offset: 0x0002FAD6
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

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000318DF File Offset: 0x0002FADF
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x000318E7 File Offset: 0x0002FAE7
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

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000318F0 File Offset: 0x0002FAF0
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x000318F8 File Offset: 0x0002FAF8
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

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00031901 File Offset: 0x0002FB01
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x00031909 File Offset: 0x0002FB09
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> servicePrincipalNames
		{
			get
			{
				return this._servicePrincipalNames;
			}
			set
			{
				this._servicePrincipalNames = value;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00031912 File Offset: 0x0002FB12
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x0003191A File Offset: 0x0002FB1A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> tags
		{
			get
			{
				return this._tags;
			}
			set
			{
				this._tags = value;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00031923 File Offset: 0x0002FB23
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x0003192B File Offset: 0x0002FB2B
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

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00031934 File Offset: 0x0002FB34
		// (set) Token: 0x06001AFA RID: 6906 RVA: 0x0003193C File Offset: 0x0002FB3C
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

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x00031945 File Offset: 0x0002FB45
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0003194D File Offset: 0x0002FB4D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ImpersonationAccessGrant> impersonationAccessGrants
		{
			get
			{
				return this._impersonationAccessGrants;
			}
			set
			{
				if (value != null)
				{
					this._impersonationAccessGrants = value;
				}
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x00031959 File Offset: 0x0002FB59
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x00031961 File Offset: 0x0002FB61
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectAccessGrant> directAccessGrants
		{
			get
			{
				return this._directAccessGrants;
			}
			set
			{
				if (value != null)
				{
					this._directAccessGrants = value;
				}
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0003196D File Offset: 0x0002FB6D
		// (set) Token: 0x06001B00 RID: 6912 RVA: 0x00031975 File Offset: 0x0002FB75
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectAccessGrant> directAccessGrantedTo
		{
			get
			{
				return this._directAccessGrantedTo;
			}
			set
			{
				if (value != null)
				{
					this._directAccessGrantedTo = value;
				}
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x00031981 File Offset: 0x0002FB81
		// (set) Token: 0x06001B02 RID: 6914 RVA: 0x00031989 File Offset: 0x0002FB89
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

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x00031995 File Offset: 0x0002FB95
		// (set) Token: 0x06001B04 RID: 6916 RVA: 0x0003199D File Offset: 0x0002FB9D
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

		// Token: 0x04001C2E RID: 7214
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001C2F RID: 7215
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _appDisplayName;

		// Token: 0x04001C30 RID: 7216
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _appId;

		// Token: 0x04001C31 RID: 7217
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private AppMetadata _appMetadata;

		// Token: 0x04001C32 RID: 7218
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _appMetadataInitialized;

		// Token: 0x04001C33 RID: 7219
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _appOwnerTenantId;

		// Token: 0x04001C34 RID: 7220
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppPermission> _appPermissions = new Collection<AppPermission>();

		// Token: 0x04001C35 RID: 7221
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private ServicePrincipalAuthenticationPolicy _authenticationPolicy;

		// Token: 0x04001C36 RID: 7222
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _authenticationPolicyInitialized;

		// Token: 0x04001C37 RID: 7223
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001C38 RID: 7224
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorUrl;

		// Token: 0x04001C39 RID: 7225
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _explicitAccessGrantRequired;

		// Token: 0x04001C3A RID: 7226
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _homepage;

		// Token: 0x04001C3B RID: 7227
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<KeyCredential> _keyCredentials = new Collection<KeyCredential>();

		// Token: 0x04001C3C RID: 7228
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _logoutUrl;

		// Token: 0x04001C3D RID: 7229
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _microsoftFirstParty;

		// Token: 0x04001C3E RID: 7230
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<PasswordCredential> _passwordCredentials = new Collection<PasswordCredential>();

		// Token: 0x04001C3F RID: 7231
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _publisherName;

		// Token: 0x04001C40 RID: 7232
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _replyUrls = new Collection<string>();

		// Token: 0x04001C41 RID: 7233
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceApplicationSet;

		// Token: 0x04001C42 RID: 7234
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _samlMetadataUrl;

		// Token: 0x04001C43 RID: 7235
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _servicePrincipalNames = new Collection<string>();

		// Token: 0x04001C44 RID: 7236
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _tags = new Collection<string>();

		// Token: 0x04001C45 RID: 7237
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApi;

		// Token: 0x04001C46 RID: 7238
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApp;

		// Token: 0x04001C47 RID: 7239
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ImpersonationAccessGrant> _impersonationAccessGrants = new Collection<ImpersonationAccessGrant>();

		// Token: 0x04001C48 RID: 7240
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectAccessGrant> _directAccessGrants = new Collection<DirectAccessGrant>();

		// Token: 0x04001C49 RID: 7241
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectAccessGrant> _directAccessGrantedTo = new Collection<DirectAccessGrant>();

		// Token: 0x04001C4A RID: 7242
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _defaultPolicy = new Collection<DirectoryObject>();

		// Token: 0x04001C4B RID: 7243
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceEndpoint> _serviceEndpoints = new Collection<ServiceEndpoint>();
	}
}
