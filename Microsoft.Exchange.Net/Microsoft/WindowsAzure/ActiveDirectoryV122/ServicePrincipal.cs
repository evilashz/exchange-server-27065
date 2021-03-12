using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005CF RID: 1487
	[DataServiceKey("objectId")]
	public class ServicePrincipal : DirectoryObject
	{
		// Token: 0x060017BB RID: 6075 RVA: 0x0002EFA8 File Offset: 0x0002D1A8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ServicePrincipal CreateServicePrincipal(string objectId, Collection<KeyCredential> keyCredentials, Collection<PasswordCredential> passwordCredentials, Collection<string> replyUrls, Collection<string> servicePrincipalNames, Collection<string> tags)
		{
			ServicePrincipal servicePrincipal = new ServicePrincipal();
			servicePrincipal.objectId = objectId;
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

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0002F030 File Offset: 0x0002D230
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x0002F038 File Offset: 0x0002D238
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

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0002F041 File Offset: 0x0002D241
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x0002F049 File Offset: 0x0002D249
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

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0002F052 File Offset: 0x0002D252
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x0002F05A File Offset: 0x0002D25A
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

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0002F063 File Offset: 0x0002D263
		// (set) Token: 0x060017C3 RID: 6083 RVA: 0x0002F08D File Offset: 0x0002D28D
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

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x0002F09D File Offset: 0x0002D29D
		// (set) Token: 0x060017C5 RID: 6085 RVA: 0x0002F0A5 File Offset: 0x0002D2A5
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

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0002F0AE File Offset: 0x0002D2AE
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x0002F0B6 File Offset: 0x0002D2B6
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

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0002F0BF File Offset: 0x0002D2BF
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x0002F0C7 File Offset: 0x0002D2C7
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

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0002F0D0 File Offset: 0x0002D2D0
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x0002F0D8 File Offset: 0x0002D2D8
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

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0002F0E1 File Offset: 0x0002D2E1
		// (set) Token: 0x060017CD RID: 6093 RVA: 0x0002F0E9 File Offset: 0x0002D2E9
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

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0002F0F2 File Offset: 0x0002D2F2
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x0002F0FA File Offset: 0x0002D2FA
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

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0002F103 File Offset: 0x0002D303
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x0002F10B File Offset: 0x0002D30B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string preferredTokenSigningKeyThumbprint
		{
			get
			{
				return this._preferredTokenSigningKeyThumbprint;
			}
			set
			{
				this._preferredTokenSigningKeyThumbprint = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0002F114 File Offset: 0x0002D314
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x0002F11C File Offset: 0x0002D31C
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

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0002F125 File Offset: 0x0002D325
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x0002F12D File Offset: 0x0002D32D
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

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0002F136 File Offset: 0x0002D336
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x0002F13E File Offset: 0x0002D33E
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

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0002F147 File Offset: 0x0002D347
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x0002F14F File Offset: 0x0002D34F
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

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0002F158 File Offset: 0x0002D358
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x0002F160 File Offset: 0x0002D360
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

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0002F169 File Offset: 0x0002D369
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0002F171 File Offset: 0x0002D371
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<Permission> permissions
		{
			get
			{
				return this._permissions;
			}
			set
			{
				if (value != null)
				{
					this._permissions = value;
				}
			}
		}

		// Token: 0x04001AC2 RID: 6850
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001AC3 RID: 6851
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _appId;

		// Token: 0x04001AC4 RID: 6852
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _appOwnerTenantId;

		// Token: 0x04001AC5 RID: 6853
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private ServicePrincipalAuthenticationPolicy _authenticationPolicy;

		// Token: 0x04001AC6 RID: 6854
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _authenticationPolicyInitialized;

		// Token: 0x04001AC7 RID: 6855
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001AC8 RID: 6856
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorUrl;

		// Token: 0x04001AC9 RID: 6857
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _homepage;

		// Token: 0x04001ACA RID: 6858
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<KeyCredential> _keyCredentials = new Collection<KeyCredential>();

		// Token: 0x04001ACB RID: 6859
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _logoutUrl;

		// Token: 0x04001ACC RID: 6860
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<PasswordCredential> _passwordCredentials = new Collection<PasswordCredential>();

		// Token: 0x04001ACD RID: 6861
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _preferredTokenSigningKeyThumbprint;

		// Token: 0x04001ACE RID: 6862
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _publisherName;

		// Token: 0x04001ACF RID: 6863
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _replyUrls = new Collection<string>();

		// Token: 0x04001AD0 RID: 6864
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _samlMetadataUrl;

		// Token: 0x04001AD1 RID: 6865
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _servicePrincipalNames = new Collection<string>();

		// Token: 0x04001AD2 RID: 6866
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _tags = new Collection<string>();

		// Token: 0x04001AD3 RID: 6867
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<Permission> _permissions = new Collection<Permission>();
	}
}
