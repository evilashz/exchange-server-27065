using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A2 RID: 1442
	[DataServiceKey("appId")]
	public class ApplicationRef
	{
		// Token: 0x06001485 RID: 5253 RVA: 0x0002C7F0 File Offset: 0x0002A9F0
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ApplicationRef CreateApplicationRef(string appId, Collection<AppPermission> appPermissions, Collection<string> identifierUris, DataServiceStreamLink mainLogo, Collection<string> replyUrls, Collection<RequiredResourceAccess> requiredResourceAccess)
		{
			ApplicationRef applicationRef = new ApplicationRef();
			applicationRef.appId = appId;
			if (appPermissions == null)
			{
				throw new ArgumentNullException("appPermissions");
			}
			applicationRef.appPermissions = appPermissions;
			if (identifierUris == null)
			{
				throw new ArgumentNullException("identifierUris");
			}
			applicationRef.identifierUris = identifierUris;
			applicationRef.mainLogo = mainLogo;
			if (replyUrls == null)
			{
				throw new ArgumentNullException("replyUrls");
			}
			applicationRef.replyUrls = replyUrls;
			if (requiredResourceAccess == null)
			{
				throw new ArgumentNullException("requiredResourceAccess");
			}
			applicationRef.requiredResourceAccess = requiredResourceAccess;
			return applicationRef;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0002C86A File Offset: 0x0002AA6A
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x0002C872 File Offset: 0x0002AA72
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string appId
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

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0002C87B File Offset: 0x0002AA7B
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x0002C883 File Offset: 0x0002AA83
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

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0002C88C File Offset: 0x0002AA8C
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x0002C894 File Offset: 0x0002AA94
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

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0002C89D File Offset: 0x0002AA9D
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x0002C8A5 File Offset: 0x0002AAA5
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

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0002C8AE File Offset: 0x0002AAAE
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x0002C8B6 File Offset: 0x0002AAB6
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

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0002C8BF File Offset: 0x0002AABF
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x0002C8C7 File Offset: 0x0002AAC7
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

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0002C8D0 File Offset: 0x0002AAD0
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x0002C8D8 File Offset: 0x0002AAD8
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

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0002C8E1 File Offset: 0x0002AAE1
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x0002C8E9 File Offset: 0x0002AAE9
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

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0002C8F2 File Offset: 0x0002AAF2
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x0002C8FA File Offset: 0x0002AAFA
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

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0002C903 File Offset: 0x0002AB03
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x0002C90B File Offset: 0x0002AB0B
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

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0002C914 File Offset: 0x0002AB14
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x0002C91C File Offset: 0x0002AB1C
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

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0002C925 File Offset: 0x0002AB25
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x0002C92D File Offset: 0x0002AB2D
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

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0002C936 File Offset: 0x0002AB36
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x0002C93E File Offset: 0x0002AB3E
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0002C947 File Offset: 0x0002AB47
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x0002C94F File Offset: 0x0002AB4F
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0002C958 File Offset: 0x0002AB58
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x0002C960 File Offset: 0x0002AB60
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

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0002C969 File Offset: 0x0002AB69
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x0002C971 File Offset: 0x0002AB71
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

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0002C97A File Offset: 0x0002AB7A
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x0002C982 File Offset: 0x0002AB82
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

		// Token: 0x0400194B RID: 6475
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _appId;

		// Token: 0x0400194C RID: 6476
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppPermission> _appPermissions = new Collection<AppPermission>();

		// Token: 0x0400194D RID: 6477
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _availableToOtherTenants;

		// Token: 0x0400194E RID: 6478
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x0400194F RID: 6479
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorUrl;

		// Token: 0x04001950 RID: 6480
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _homepage;

		// Token: 0x04001951 RID: 6481
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _identifierUris = new Collection<string>();

		// Token: 0x04001952 RID: 6482
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _mainLogo;

		// Token: 0x04001953 RID: 6483
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _logoutUrl;

		// Token: 0x04001954 RID: 6484
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _publisherName;

		// Token: 0x04001955 RID: 6485
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _publicClient;

		// Token: 0x04001956 RID: 6486
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _replyUrls = new Collection<string>();

		// Token: 0x04001957 RID: 6487
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<RequiredResourceAccess> _requiredResourceAccess = new Collection<RequiredResourceAccess>();

		// Token: 0x04001958 RID: 6488
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceApplicationSet;

		// Token: 0x04001959 RID: 6489
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _samlMetadataUrl;

		// Token: 0x0400195A RID: 6490
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApi;

		// Token: 0x0400195B RID: 6491
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApp;
	}
}
