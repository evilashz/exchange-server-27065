using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005B2 RID: 1458
	[DataServiceKey("objectId")]
	public class TenantDetail : DirectoryObject
	{
		// Token: 0x060015C8 RID: 5576 RVA: 0x0002D7EC File Offset: 0x0002B9EC
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

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0002D8A2 File Offset: 0x0002BAA2
		// (set) Token: 0x060015CA RID: 5578 RVA: 0x0002D8AA File Offset: 0x0002BAAA
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

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0002D8B3 File Offset: 0x0002BAB3
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x0002D8BB File Offset: 0x0002BABB
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

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0002D8C4 File Offset: 0x0002BAC4
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x0002D8CC File Offset: 0x0002BACC
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

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0002D8D5 File Offset: 0x0002BAD5
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x0002D8DD File Offset: 0x0002BADD
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

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0002D8E6 File Offset: 0x0002BAE6
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x0002D8EE File Offset: 0x0002BAEE
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

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0002D8F7 File Offset: 0x0002BAF7
		// (set) Token: 0x060015D4 RID: 5588 RVA: 0x0002D8FF File Offset: 0x0002BAFF
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

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0002D908 File Offset: 0x0002BB08
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x0002D910 File Offset: 0x0002BB10
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

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0002D919 File Offset: 0x0002BB19
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x0002D921 File Offset: 0x0002BB21
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

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0002D92A File Offset: 0x0002BB2A
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x0002D932 File Offset: 0x0002BB32
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

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x0002D93B File Offset: 0x0002BB3B
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x0002D943 File Offset: 0x0002BB43
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

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0002D94C File Offset: 0x0002BB4C
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x0002D954 File Offset: 0x0002BB54
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

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0002D95D File Offset: 0x0002BB5D
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x0002D965 File Offset: 0x0002BB65
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

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0002D96E File Offset: 0x0002BB6E
		// (set) Token: 0x060015E2 RID: 5602 RVA: 0x0002D976 File Offset: 0x0002BB76
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

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0002D97F File Offset: 0x0002BB7F
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x0002D987 File Offset: 0x0002BB87
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

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0002D990 File Offset: 0x0002BB90
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x0002D998 File Offset: 0x0002BB98
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

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x0002D9A1 File Offset: 0x0002BBA1
		// (set) Token: 0x060015E8 RID: 5608 RVA: 0x0002D9A9 File Offset: 0x0002BBA9
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

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0002D9B2 File Offset: 0x0002BBB2
		// (set) Token: 0x060015EA RID: 5610 RVA: 0x0002D9BA File Offset: 0x0002BBBA
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

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0002D9C3 File Offset: 0x0002BBC3
		// (set) Token: 0x060015EC RID: 5612 RVA: 0x0002D9CB File Offset: 0x0002BBCB
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

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		// (set) Token: 0x060015EE RID: 5614 RVA: 0x0002D9DC File Offset: 0x0002BBDC
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

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0002D9E5 File Offset: 0x0002BBE5
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x0002D9ED File Offset: 0x0002BBED
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

		// Token: 0x040019DF RID: 6623
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AssignedPlan> _assignedPlans = new Collection<AssignedPlan>();

		// Token: 0x040019E0 RID: 6624
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _city;

		// Token: 0x040019E1 RID: 6625
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _companyLastDirSyncTime;

		// Token: 0x040019E2 RID: 6626
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _companyTags = new Collection<string>();

		// Token: 0x040019E3 RID: 6627
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x040019E4 RID: 6628
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _countryLetterCode;

		// Token: 0x040019E5 RID: 6629
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x040019E6 RID: 6630
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x040019E7 RID: 6631
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _marketingNotificationEmails = new Collection<string>();

		// Token: 0x040019E8 RID: 6632
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _postalCode;

		// Token: 0x040019E9 RID: 6633
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _preferredLanguage;

		// Token: 0x040019EA RID: 6634
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisionedPlan> _provisionedPlans = new Collection<ProvisionedPlan>();

		// Token: 0x040019EB RID: 6635
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x040019EC RID: 6636
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _state;

		// Token: 0x040019ED RID: 6637
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _street;

		// Token: 0x040019EE RID: 6638
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _technicalNotificationMails = new Collection<string>();

		// Token: 0x040019EF RID: 6639
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _telephoneNumber;

		// Token: 0x040019F0 RID: 6640
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _tenantType;

		// Token: 0x040019F1 RID: 6641
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<VerifiedDomain> _verifiedDomains = new Collection<VerifiedDomain>();

		// Token: 0x040019F2 RID: 6642
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceInfo> _serviceInfo = new Collection<ServiceInfo>();
	}
}
