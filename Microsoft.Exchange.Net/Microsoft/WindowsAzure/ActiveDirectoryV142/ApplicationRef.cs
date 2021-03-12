using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005EB RID: 1515
	[DataServiceKey("appId")]
	public class ApplicationRef
	{
		// Token: 0x060019AF RID: 6575 RVA: 0x000308B8 File Offset: 0x0002EAB8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ApplicationRef CreateApplicationRef(Guid appContextId, string appId, Collection<AppPermission> appPermissions, Collection<string> identifierUris, Collection<Guid> knownClientApplications, DataServiceStreamLink mainLogo, Collection<string> replyUrls, Collection<RequiredResourceAccess> requiredResourceAccess)
		{
			ApplicationRef applicationRef = new ApplicationRef();
			applicationRef.appContextId = appContextId;
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
			if (knownClientApplications == null)
			{
				throw new ArgumentNullException("knownClientApplications");
			}
			applicationRef.knownClientApplications = knownClientApplications;
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

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00030951 File Offset: 0x0002EB51
		// (set) Token: 0x060019B1 RID: 6577 RVA: 0x00030959 File Offset: 0x0002EB59
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid appContextId
		{
			get
			{
				return this._appContextId;
			}
			set
			{
				this._appContextId = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00030962 File Offset: 0x0002EB62
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x0003096A File Offset: 0x0002EB6A
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

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00030973 File Offset: 0x0002EB73
		// (set) Token: 0x060019B5 RID: 6581 RVA: 0x0003097B File Offset: 0x0002EB7B
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

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x00030984 File Offset: 0x0002EB84
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x0003098C File Offset: 0x0002EB8C
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

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00030995 File Offset: 0x0002EB95
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x0003099D File Offset: 0x0002EB9D
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

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x000309A6 File Offset: 0x0002EBA6
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x000309AE File Offset: 0x0002EBAE
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

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x000309B7 File Offset: 0x0002EBB7
		// (set) Token: 0x060019BD RID: 6589 RVA: 0x000309BF File Offset: 0x0002EBBF
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

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x000309C8 File Offset: 0x0002EBC8
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x000309D0 File Offset: 0x0002EBD0
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

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x000309D9 File Offset: 0x0002EBD9
		// (set) Token: 0x060019C1 RID: 6593 RVA: 0x000309E1 File Offset: 0x0002EBE1
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

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x000309EA File Offset: 0x0002EBEA
		// (set) Token: 0x060019C3 RID: 6595 RVA: 0x000309F2 File Offset: 0x0002EBF2
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

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x000309FB File Offset: 0x0002EBFB
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x00030A03 File Offset: 0x0002EC03
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

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00030A0C File Offset: 0x0002EC0C
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x00030A14 File Offset: 0x0002EC14
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

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00030A1D File Offset: 0x0002EC1D
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x00030A25 File Offset: 0x0002EC25
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

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x00030A2E File Offset: 0x0002EC2E
		// (set) Token: 0x060019CB RID: 6603 RVA: 0x00030A36 File Offset: 0x0002EC36
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

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00030A3F File Offset: 0x0002EC3F
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x00030A47 File Offset: 0x0002EC47
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

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00030A50 File Offset: 0x0002EC50
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x00030A58 File Offset: 0x0002EC58
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

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00030A61 File Offset: 0x0002EC61
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x00030A69 File Offset: 0x0002EC69
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

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x00030A72 File Offset: 0x0002EC72
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x00030A7A File Offset: 0x0002EC7A
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

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x00030A83 File Offset: 0x0002EC83
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x00030A8B File Offset: 0x0002EC8B
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

		// Token: 0x04001BAE RID: 7086
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid _appContextId;

		// Token: 0x04001BAF RID: 7087
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _appId;

		// Token: 0x04001BB0 RID: 7088
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppPermission> _appPermissions = new Collection<AppPermission>();

		// Token: 0x04001BB1 RID: 7089
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _availableToOtherTenants;

		// Token: 0x04001BB2 RID: 7090
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001BB3 RID: 7091
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorUrl;

		// Token: 0x04001BB4 RID: 7092
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _homepage;

		// Token: 0x04001BB5 RID: 7093
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _identifierUris = new Collection<string>();

		// Token: 0x04001BB6 RID: 7094
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<Guid> _knownClientApplications = new Collection<Guid>();

		// Token: 0x04001BB7 RID: 7095
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _mainLogo;

		// Token: 0x04001BB8 RID: 7096
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _logoutUrl;

		// Token: 0x04001BB9 RID: 7097
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _publisherName;

		// Token: 0x04001BBA RID: 7098
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _publicClient;

		// Token: 0x04001BBB RID: 7099
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _replyUrls = new Collection<string>();

		// Token: 0x04001BBC RID: 7100
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<RequiredResourceAccess> _requiredResourceAccess = new Collection<RequiredResourceAccess>();

		// Token: 0x04001BBD RID: 7101
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceApplicationSet;

		// Token: 0x04001BBE RID: 7102
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _samlMetadataUrl;

		// Token: 0x04001BBF RID: 7103
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApi;

		// Token: 0x04001BC0 RID: 7104
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _webApp;
	}
}
