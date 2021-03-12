using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005FF RID: 1535
	[DataServiceKey("objectId")]
	public class TenantDetail : DirectoryObject
	{
		// Token: 0x06001B22 RID: 6946 RVA: 0x00031B40 File Offset: 0x0002FD40
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static TenantDetail CreateTenantDetail(string objectId, Collection<AssignedPlan> assignedPlans, Collection<string> companyTags, Collection<string> marketingNotificationEmails, Collection<ProvisionedPlan> provisionedPlans, Collection<ProvisioningError> provisioningErrors, Collection<string> technicalNotificationMails, Collection<VerifiedDomain> verifiedDomains)
		{
			TenantDetail tenantDetail = new TenantDetail();
			tenantDetail.objectId = objectId;
			if (assignedPlans == null)
			{
				throw new ArgumentNullException("assignedPlans");
			}
			tenantDetail.assignedPlans = assignedPlans;
			if (companyTags == null)
			{
				throw new ArgumentNullException("companyTags");
			}
			tenantDetail.companyTags = companyTags;
			if (marketingNotificationEmails == null)
			{
				throw new ArgumentNullException("marketingNotificationEmails");
			}
			tenantDetail.marketingNotificationEmails = marketingNotificationEmails;
			if (provisionedPlans == null)
			{
				throw new ArgumentNullException("provisionedPlans");
			}
			tenantDetail.provisionedPlans = provisionedPlans;
			if (provisioningErrors == null)
			{
				throw new ArgumentNullException("provisioningErrors");
			}
			tenantDetail.provisioningErrors = provisioningErrors;
			if (technicalNotificationMails == null)
			{
				throw new ArgumentNullException("technicalNotificationMails");
			}
			tenantDetail.technicalNotificationMails = technicalNotificationMails;
			if (verifiedDomains == null)
			{
				throw new ArgumentNullException("verifiedDomains");
			}
			tenantDetail.verifiedDomains = verifiedDomains;
			return tenantDetail;
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x00031BF6 File Offset: 0x0002FDF6
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x00031BFE File Offset: 0x0002FDFE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<AssignedPlan> assignedPlans
		{
			get
			{
				return this._assignedPlans;
			}
			set
			{
				this._assignedPlans = value;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00031C07 File Offset: 0x0002FE07
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x00031C0F File Offset: 0x0002FE0F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string city
		{
			get
			{
				return this._city;
			}
			set
			{
				this._city = value;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00031C18 File Offset: 0x0002FE18
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x00031C20 File Offset: 0x0002FE20
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? companyLastDirSyncTime
		{
			get
			{
				return this._companyLastDirSyncTime;
			}
			set
			{
				this._companyLastDirSyncTime = value;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x00031C29 File Offset: 0x0002FE29
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x00031C31 File Offset: 0x0002FE31
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> companyTags
		{
			get
			{
				return this._companyTags;
			}
			set
			{
				this._companyTags = value;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x00031C3A File Offset: 0x0002FE3A
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x00031C42 File Offset: 0x0002FE42
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string country
		{
			get
			{
				return this._country;
			}
			set
			{
				this._country = value;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x00031C4B File Offset: 0x0002FE4B
		// (set) Token: 0x06001B2E RID: 6958 RVA: 0x00031C53 File Offset: 0x0002FE53
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string countryLetterCode
		{
			get
			{
				return this._countryLetterCode;
			}
			set
			{
				this._countryLetterCode = value;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x00031C5C File Offset: 0x0002FE5C
		// (set) Token: 0x06001B30 RID: 6960 RVA: 0x00031C64 File Offset: 0x0002FE64
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? dirSyncEnabled
		{
			get
			{
				return this._dirSyncEnabled;
			}
			set
			{
				this._dirSyncEnabled = value;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00031C6D File Offset: 0x0002FE6D
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x00031C75 File Offset: 0x0002FE75
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

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x00031C7E File Offset: 0x0002FE7E
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x00031C86 File Offset: 0x0002FE86
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> marketingNotificationEmails
		{
			get
			{
				return this._marketingNotificationEmails;
			}
			set
			{
				this._marketingNotificationEmails = value;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x00031C8F File Offset: 0x0002FE8F
		// (set) Token: 0x06001B36 RID: 6966 RVA: 0x00031C97 File Offset: 0x0002FE97
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string postalCode
		{
			get
			{
				return this._postalCode;
			}
			set
			{
				this._postalCode = value;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x00031CA0 File Offset: 0x0002FEA0
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x00031CA8 File Offset: 0x0002FEA8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string preferredLanguage
		{
			get
			{
				return this._preferredLanguage;
			}
			set
			{
				this._preferredLanguage = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00031CB1 File Offset: 0x0002FEB1
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x00031CB9 File Offset: 0x0002FEB9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ProvisionedPlan> provisionedPlans
		{
			get
			{
				return this._provisionedPlans;
			}
			set
			{
				this._provisionedPlans = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x00031CC2 File Offset: 0x0002FEC2
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x00031CCA File Offset: 0x0002FECA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ProvisioningError> provisioningErrors
		{
			get
			{
				return this._provisioningErrors;
			}
			set
			{
				this._provisioningErrors = value;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00031CD3 File Offset: 0x0002FED3
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x00031CFD File Offset: 0x0002FEFD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public SelfServePasswordResetPolicy selfServePasswordResetPolicy
		{
			get
			{
				if (this._selfServePasswordResetPolicy == null && !this._selfServePasswordResetPolicyInitialized)
				{
					this._selfServePasswordResetPolicy = new SelfServePasswordResetPolicy();
					this._selfServePasswordResetPolicyInitialized = true;
				}
				return this._selfServePasswordResetPolicy;
			}
			set
			{
				this._selfServePasswordResetPolicy = value;
				this._selfServePasswordResetPolicyInitialized = true;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00031D0D File Offset: 0x0002FF0D
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x00031D15 File Offset: 0x0002FF15
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00031D1E File Offset: 0x0002FF1E
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x00031D26 File Offset: 0x0002FF26
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string street
		{
			get
			{
				return this._street;
			}
			set
			{
				this._street = value;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00031D2F File Offset: 0x0002FF2F
		// (set) Token: 0x06001B44 RID: 6980 RVA: 0x00031D37 File Offset: 0x0002FF37
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> technicalNotificationMails
		{
			get
			{
				return this._technicalNotificationMails;
			}
			set
			{
				this._technicalNotificationMails = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x00031D40 File Offset: 0x0002FF40
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x00031D48 File Offset: 0x0002FF48
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string telephoneNumber
		{
			get
			{
				return this._telephoneNumber;
			}
			set
			{
				this._telephoneNumber = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x00031D51 File Offset: 0x0002FF51
		// (set) Token: 0x06001B48 RID: 6984 RVA: 0x00031D59 File Offset: 0x0002FF59
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string tenantType
		{
			get
			{
				return this._tenantType;
			}
			set
			{
				this._tenantType = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00031D62 File Offset: 0x0002FF62
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x00031D6A File Offset: 0x0002FF6A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<VerifiedDomain> verifiedDomains
		{
			get
			{
				return this._verifiedDomains;
			}
			set
			{
				this._verifiedDomains = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x00031D73 File Offset: 0x0002FF73
		// (set) Token: 0x06001B4C RID: 6988 RVA: 0x00031D7B File Offset: 0x0002FF7B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ServiceInfo> serviceInfo
		{
			get
			{
				return this._serviceInfo;
			}
			set
			{
				if (value != null)
				{
					this._serviceInfo = value;
				}
			}
		}

		// Token: 0x04001C58 RID: 7256
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AssignedPlan> _assignedPlans = new Collection<AssignedPlan>();

		// Token: 0x04001C59 RID: 7257
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _city;

		// Token: 0x04001C5A RID: 7258
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _companyLastDirSyncTime;

		// Token: 0x04001C5B RID: 7259
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _companyTags = new Collection<string>();

		// Token: 0x04001C5C RID: 7260
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x04001C5D RID: 7261
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _countryLetterCode;

		// Token: 0x04001C5E RID: 7262
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001C5F RID: 7263
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001C60 RID: 7264
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _marketingNotificationEmails = new Collection<string>();

		// Token: 0x04001C61 RID: 7265
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _postalCode;

		// Token: 0x04001C62 RID: 7266
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _preferredLanguage;

		// Token: 0x04001C63 RID: 7267
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisionedPlan> _provisionedPlans = new Collection<ProvisionedPlan>();

		// Token: 0x04001C64 RID: 7268
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x04001C65 RID: 7269
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private SelfServePasswordResetPolicy _selfServePasswordResetPolicy;

		// Token: 0x04001C66 RID: 7270
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _selfServePasswordResetPolicyInitialized;

		// Token: 0x04001C67 RID: 7271
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _state;

		// Token: 0x04001C68 RID: 7272
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _street;

		// Token: 0x04001C69 RID: 7273
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _technicalNotificationMails = new Collection<string>();

		// Token: 0x04001C6A RID: 7274
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _telephoneNumber;

		// Token: 0x04001C6B RID: 7275
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _tenantType;

		// Token: 0x04001C6C RID: 7276
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<VerifiedDomain> _verifiedDomains = new Collection<VerifiedDomain>();

		// Token: 0x04001C6D RID: 7277
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceInfo> _serviceInfo = new Collection<ServiceInfo>();
	}
}
