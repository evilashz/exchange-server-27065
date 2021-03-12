using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C5 RID: 1477
	[DataServiceKey("objectId")]
	public class Application : DirectoryObject
	{
		// Token: 0x060016FA RID: 5882 RVA: 0x0002E65C File Offset: 0x0002C85C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Application CreateApplication(string objectId, Collection<string> identifierUris, Collection<KeyCredential> keyCredentials, DataServiceStreamLink mainLogo, Collection<PasswordCredential> passwordCredentials, Collection<string> replyUrls)
		{
			Application application = new Application();
			application.objectId = objectId;
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
			return application;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0002E6D6 File Offset: 0x0002C8D6
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x0002E6DE File Offset: 0x0002C8DE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? allowActAsForAllClients
		{
			get
			{
				return this._allowActAsForAllClients;
			}
			set
			{
				this._allowActAsForAllClients = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x0002E6E7 File Offset: 0x0002C8E7
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x0002E6EF File Offset: 0x0002C8EF
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

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0002E6F8 File Offset: 0x0002C8F8
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x0002E700 File Offset: 0x0002C900
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

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0002E709 File Offset: 0x0002C909
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x0002E711 File Offset: 0x0002C911
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

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0002E71A File Offset: 0x0002C91A
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x0002E722 File Offset: 0x0002C922
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

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0002E72B File Offset: 0x0002C92B
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x0002E733 File Offset: 0x0002C933
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string groupMembershipClaims
		{
			get
			{
				return this._groupMembershipClaims;
			}
			set
			{
				this._groupMembershipClaims = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0002E73C File Offset: 0x0002C93C
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0002E744 File Offset: 0x0002C944
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

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0002E74D File Offset: 0x0002C94D
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x0002E755 File Offset: 0x0002C955
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

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0002E75E File Offset: 0x0002C95E
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x0002E766 File Offset: 0x0002C966
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

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0002E76F File Offset: 0x0002C96F
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x0002E777 File Offset: 0x0002C977
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

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0002E780 File Offset: 0x0002C980
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x0002E788 File Offset: 0x0002C988
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

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0002E791 File Offset: 0x0002C991
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x0002E799 File Offset: 0x0002C999
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

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0002E7A2 File Offset: 0x0002C9A2
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x0002E7AA File Offset: 0x0002C9AA
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

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0002E7B3 File Offset: 0x0002C9B3
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x0002E7BB File Offset: 0x0002C9BB
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

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0002E7C4 File Offset: 0x0002C9C4
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x0002E7CC File Offset: 0x0002C9CC
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

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0002E7D5 File Offset: 0x0002C9D5
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x0002E7DD File Offset: 0x0002C9DD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<AppLocalizedBranding> appLocalizedBranding
		{
			get
			{
				return this._appLocalizedBranding;
			}
			set
			{
				if (value != null)
				{
					this._appLocalizedBranding = value;
				}
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0002E7E9 File Offset: 0x0002C9E9
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x0002E7F1 File Offset: 0x0002C9F1
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<AppNonLocalizedBranding> appNonLocalizedBranding
		{
			get
			{
				return this._appNonLocalizedBranding;
			}
			set
			{
				if (value != null)
				{
					this._appNonLocalizedBranding = value;
				}
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0002E7FD File Offset: 0x0002C9FD
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x0002E805 File Offset: 0x0002CA05
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ExtensionProperty> extensionProperties
		{
			get
			{
				return this._extensionProperties;
			}
			set
			{
				if (value != null)
				{
					this._extensionProperties = value;
				}
			}
		}

		// Token: 0x04001A6B RID: 6763
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _allowActAsForAllClients;

		// Token: 0x04001A6C RID: 6764
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _appId;

		// Token: 0x04001A6D RID: 6765
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _availableToOtherTenants;

		// Token: 0x04001A6E RID: 6766
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001A6F RID: 6767
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorUrl;

		// Token: 0x04001A70 RID: 6768
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _groupMembershipClaims;

		// Token: 0x04001A71 RID: 6769
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _homepage;

		// Token: 0x04001A72 RID: 6770
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _identifierUris = new Collection<string>();

		// Token: 0x04001A73 RID: 6771
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<KeyCredential> _keyCredentials = new Collection<KeyCredential>();

		// Token: 0x04001A74 RID: 6772
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _mainLogo;

		// Token: 0x04001A75 RID: 6773
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _logoutUrl;

		// Token: 0x04001A76 RID: 6774
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<PasswordCredential> _passwordCredentials = new Collection<PasswordCredential>();

		// Token: 0x04001A77 RID: 6775
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _publicClient;

		// Token: 0x04001A78 RID: 6776
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _replyUrls = new Collection<string>();

		// Token: 0x04001A79 RID: 6777
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _samlMetadataUrl;

		// Token: 0x04001A7A RID: 6778
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppLocalizedBranding> _appLocalizedBranding = new Collection<AppLocalizedBranding>();

		// Token: 0x04001A7B RID: 6779
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AppNonLocalizedBranding> _appNonLocalizedBranding = new Collection<AppNonLocalizedBranding>();

		// Token: 0x04001A7C RID: 6780
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ExtensionProperty> _extensionProperties = new Collection<ExtensionProperty>();
	}
}
