using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005D1 RID: 1489
	[DataServiceKey("objectId")]
	public class TenantDetail : DirectoryObject
	{
		// Token: 0x060017EC RID: 6124 RVA: 0x0002F244 File Offset: 0x0002D444
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static TenantDetail CreateTenantDetail(string objectId, Collection<AssignedPlan> assignedPlans, Collection<string> marketingNotificationEmails, Collection<ProvisionedPlan> provisionedPlans, Collection<ProvisioningError> provisioningErrors, Collection<string> technicalNotificationMails, Collection<VerifiedDomain> verifiedDomains)
		{
			TenantDetail tenantDetail = new TenantDetail();
			tenantDetail.objectId = objectId;
			if (assignedPlans == null)
			{
				throw new ArgumentNullException("assignedPlans");
			}
			tenantDetail.assignedPlans = assignedPlans;
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

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0002F2E3 File Offset: 0x0002D4E3
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x0002F2EB File Offset: 0x0002D4EB
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

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0002F2F4 File Offset: 0x0002D4F4
		// (set) Token: 0x060017F0 RID: 6128 RVA: 0x0002F2FC File Offset: 0x0002D4FC
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

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0002F305 File Offset: 0x0002D505
		// (set) Token: 0x060017F2 RID: 6130 RVA: 0x0002F30D File Offset: 0x0002D50D
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

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0002F316 File Offset: 0x0002D516
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x0002F31E File Offset: 0x0002D51E
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

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0002F327 File Offset: 0x0002D527
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x0002F32F File Offset: 0x0002D52F
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

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0002F338 File Offset: 0x0002D538
		// (set) Token: 0x060017F8 RID: 6136 RVA: 0x0002F340 File Offset: 0x0002D540
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

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0002F349 File Offset: 0x0002D549
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x0002F351 File Offset: 0x0002D551
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

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0002F35A File Offset: 0x0002D55A
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x0002F362 File Offset: 0x0002D562
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

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0002F36B File Offset: 0x0002D56B
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x0002F373 File Offset: 0x0002D573
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

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0002F37C File Offset: 0x0002D57C
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x0002F384 File Offset: 0x0002D584
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

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0002F38D File Offset: 0x0002D58D
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x0002F395 File Offset: 0x0002D595
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

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0002F39E File Offset: 0x0002D59E
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x0002F3A6 File Offset: 0x0002D5A6
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

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0002F3AF File Offset: 0x0002D5AF
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x0002F3B7 File Offset: 0x0002D5B7
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

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0002F3C0 File Offset: 0x0002D5C0
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x0002F3C8 File Offset: 0x0002D5C8
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

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0002F3D1 File Offset: 0x0002D5D1
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x0002F3D9 File Offset: 0x0002D5D9
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

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0002F3E2 File Offset: 0x0002D5E2
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x0002F3EA File Offset: 0x0002D5EA
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

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0002F3F3 File Offset: 0x0002D5F3
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x0002F3FB File Offset: 0x0002D5FB
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

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0002F404 File Offset: 0x0002D604
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x0002F40C File Offset: 0x0002D60C
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

		// Token: 0x04001ADA RID: 6874
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AssignedPlan> _assignedPlans = new Collection<AssignedPlan>();

		// Token: 0x04001ADB RID: 6875
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _city;

		// Token: 0x04001ADC RID: 6876
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _companyLastDirSyncTime;

		// Token: 0x04001ADD RID: 6877
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x04001ADE RID: 6878
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _countryLetterCode;

		// Token: 0x04001ADF RID: 6879
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001AE0 RID: 6880
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001AE1 RID: 6881
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _marketingNotificationEmails = new Collection<string>();

		// Token: 0x04001AE2 RID: 6882
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _postalCode;

		// Token: 0x04001AE3 RID: 6883
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _preferredLanguage;

		// Token: 0x04001AE4 RID: 6884
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisionedPlan> _provisionedPlans = new Collection<ProvisionedPlan>();

		// Token: 0x04001AE5 RID: 6885
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x04001AE6 RID: 6886
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _state;

		// Token: 0x04001AE7 RID: 6887
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _street;

		// Token: 0x04001AE8 RID: 6888
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _technicalNotificationMails = new Collection<string>();

		// Token: 0x04001AE9 RID: 6889
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _telephoneNumber;

		// Token: 0x04001AEA RID: 6890
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _tenantType;

		// Token: 0x04001AEB RID: 6891
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<VerifiedDomain> _verifiedDomains = new Collection<VerifiedDomain>();
	}
}
