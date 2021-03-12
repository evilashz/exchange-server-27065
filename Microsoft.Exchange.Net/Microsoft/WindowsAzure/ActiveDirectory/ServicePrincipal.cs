using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005B0 RID: 1456
	[DataServiceKey("objectId")]
	public class ServicePrincipal : DirectoryObject
	{
		// Token: 0x06001587 RID: 5511 RVA: 0x0002D44C File Offset: 0x0002B64C
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

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0002D4F4 File Offset: 0x0002B6F4
		// (set) Token: 0x06001589 RID: 5513 RVA: 0x0002D4FC File Offset: 0x0002B6FC
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

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x0002D505 File Offset: 0x0002B705
		// (set) Token: 0x0600158B RID: 5515 RVA: 0x0002D50D File Offset: 0x0002B70D
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

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0002D516 File Offset: 0x0002B716
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x0002D540 File Offset: 0x0002B740
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

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0002D550 File Offset: 0x0002B750
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x0002D558 File Offset: 0x0002B758
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

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0002D561 File Offset: 0x0002B761
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x0002D569 File Offset: 0x0002B769
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

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0002D572 File Offset: 0x0002B772
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x0002D59C File Offset: 0x0002B79C
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

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x0002D5AC File Offset: 0x0002B7AC
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x0002D5B4 File Offset: 0x0002B7B4
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

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0002D5BD File Offset: 0x0002B7BD
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x0002D5C5 File Offset: 0x0002B7C5
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

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0002D5CE File Offset: 0x0002B7CE
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x0002D5D6 File Offset: 0x0002B7D6
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

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0002D5DF File Offset: 0x0002B7DF
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x0002D5E7 File Offset: 0x0002B7E7
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

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x0002D5F8 File Offset: 0x0002B7F8
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

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0002D601 File Offset: 0x0002B801
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x0002D609 File Offset: 0x0002B809
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

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0002D612 File Offset: 0x0002B812
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x0002D61A File Offset: 0x0002B81A
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

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x0002D623 File Offset: 0x0002B823
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x0002D62B File Offset: 0x0002B82B
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

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x0002D634 File Offset: 0x0002B834
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x0002D63C File Offset: 0x0002B83C
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

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0002D645 File Offset: 0x0002B845
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x0002D64D File Offset: 0x0002B84D
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

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0002D656 File Offset: 0x0002B856
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x0002D65E File Offset: 0x0002B85E
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

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x0002D667 File Offset: 0x0002B867
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x0002D66F File Offset: 0x0002B86F
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

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x0002D678 File Offset: 0x0002B878
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x0002D680 File Offset: 0x0002B880
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

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0002D689 File Offset: 0x0002B889
		// (set) Token: 0x060015AF RID: 5551 RVA: 0x0002D691 File Offset: 0x0002B891
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

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0002D69A File Offset: 0x0002B89A
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x0002D6A2 File Offset: 0x0002B8A2
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

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0002D6AB File Offset: 0x0002B8AB
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0002D6B3 File Offset: 0x0002B8B3
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

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0002D6BF File Offset: 0x0002B8BF
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x0002D6C7 File Offset: 0x0002B8C7
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

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0002D6D3 File Offset: 0x0002B8D3
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x0002D6DB File Offset: 0x0002B8DB
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

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0002D6E7 File Offset: 0x0002B8E7
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x0002D6EF File Offset: 0x0002B8EF
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

		// Token: 0x040019BE RID: 6590
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x040019BF RID: 6591
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _appId;

		// Token: 0x040019C0 RID: 6592
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private AppMetadata _appMetadata;

		// Token: 0x040019C1 RID: 6593
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _appMetadataInitialized;

		// Token: 0x040019C2 RID: 6594
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _appOwnerTenantId;

		// Token: 0x040019C3 RID: 6595
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppPermission> _appPermissions = new Collection<AppPermission>();

		// Token: 0x040019C4 RID: 6596
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private ServicePrincipalAuthenticationPolicy _authenticationPolicy;

		// Token: 0x040019C5 RID: 6597
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _authenticationPolicyInitialized;

		// Token: 0x040019C6 RID: 6598
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x040019C7 RID: 6599
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorUrl;

		// Token: 0x040019C8 RID: 6600
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _explicitAccessGrantRequired;

		// Token: 0x040019C9 RID: 6601
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _homepage;

		// Token: 0x040019CA RID: 6602
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<KeyCredential> _keyCredentials = new Collection<KeyCredential>();

		// Token: 0x040019CB RID: 6603
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _logoutUrl;

		// Token: 0x040019CC RID: 6604
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<PasswordCredential> _passwordCredentials = new Collection<PasswordCredential>();

		// Token: 0x040019CD RID: 6605
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _publisherName;

		// Token: 0x040019CE RID: 6606
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _replyUrls = new Collection<string>();

		// Token: 0x040019CF RID: 6607
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceApplicationSet;

		// Token: 0x040019D0 RID: 6608
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _samlMetadataUrl;

		// Token: 0x040019D1 RID: 6609
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _servicePrincipalNames = new Collection<string>();

		// Token: 0x040019D2 RID: 6610
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _tags = new Collection<string>();

		// Token: 0x040019D3 RID: 6611
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApi;

		// Token: 0x040019D4 RID: 6612
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApp;

		// Token: 0x040019D5 RID: 6613
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ImpersonationAccessGrant> _impersonationAccessGrants = new Collection<ImpersonationAccessGrant>();

		// Token: 0x040019D6 RID: 6614
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectAccessGrant> _directAccessGrants = new Collection<DirectAccessGrant>();

		// Token: 0x040019D7 RID: 6615
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectAccessGrant> _directAccessGrantedTo = new Collection<DirectAccessGrant>();

		// Token: 0x040019D8 RID: 6616
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceEndpoint> _serviceEndpoints = new Collection<ServiceEndpoint>();
	}
}
