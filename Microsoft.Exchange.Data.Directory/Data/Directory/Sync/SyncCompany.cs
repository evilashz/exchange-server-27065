using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200081F RID: 2079
	internal class SyncCompany : SyncObject
	{
		// Token: 0x060066B0 RID: 26288 RVA: 0x0016B94C File Offset: 0x00169B4C
		public SyncCompany(SyncDirection syncDirection) : base(syncDirection)
		{
		}

		// Token: 0x17002438 RID: 9272
		// (get) Token: 0x060066B1 RID: 26289 RVA: 0x0016B955 File Offset: 0x00169B55
		public override SyncObjectSchema Schema
		{
			get
			{
				return SyncCompany.schema;
			}
		}

		// Token: 0x060066B2 RID: 26290 RVA: 0x0016B95C File Offset: 0x00169B5C
		public override bool IsValid(bool isFullSyncObject)
		{
			return !isFullSyncObject || this.InitialDomainNameRetrieved != null;
		}

		// Token: 0x17002439 RID: 9273
		// (get) Token: 0x060066B3 RID: 26291 RVA: 0x0016B96F File Offset: 0x00169B6F
		internal override DirectoryObjectClass ObjectClass
		{
			get
			{
				return DirectoryObjectClass.Company;
			}
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x0016B972 File Offset: 0x00169B72
		protected override DirectoryObject CreateDirectoryObject()
		{
			return new Company();
		}

		// Token: 0x1700243A RID: 9274
		// (get) Token: 0x060066B5 RID: 26293 RVA: 0x0016B979 File Offset: 0x00169B79
		// (set) Token: 0x060066B6 RID: 26294 RVA: 0x0016B98B File Offset: 0x00169B8B
		public SyncProperty<RightsManagementTenantConfigurationValue> RightsManagementTenantConfiguration
		{
			get
			{
				return (SyncProperty<RightsManagementTenantConfigurationValue>)base[SyncCompanySchema.RightsManagementTenantConfiguration];
			}
			set
			{
				base[SyncCompanySchema.RightsManagementTenantConfiguration] = value;
			}
		}

		// Token: 0x1700243B RID: 9275
		// (get) Token: 0x060066B7 RID: 26295 RVA: 0x0016B999 File Offset: 0x00169B99
		// (set) Token: 0x060066B8 RID: 26296 RVA: 0x0016B9AB File Offset: 0x00169BAB
		public SyncProperty<MultiValuedProperty<RightsManagementTenantKeyValue>> RightsManagementTenantKey
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<RightsManagementTenantKeyValue>>)base[SyncCompanySchema.RightsManagementTenantKey];
			}
			set
			{
				base[SyncCompanySchema.RightsManagementTenantKey] = value;
			}
		}

		// Token: 0x1700243C RID: 9276
		// (get) Token: 0x060066B9 RID: 26297 RVA: 0x0016B9B9 File Offset: 0x00169BB9
		// (set) Token: 0x060066BA RID: 26298 RVA: 0x0016B9CB File Offset: 0x00169BCB
		public SyncProperty<MultiValuedProperty<ServiceInfoValue>> ServiceInfo
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<ServiceInfoValue>>)base[SyncCompanySchema.ServiceInfo];
			}
			set
			{
				base[SyncCompanySchema.ServiceInfo] = value;
			}
		}

		// Token: 0x1700243D RID: 9277
		// (get) Token: 0x060066BB RID: 26299 RVA: 0x0016B9D9 File Offset: 0x00169BD9
		// (set) Token: 0x060066BC RID: 26300 RVA: 0x0016B9EB File Offset: 0x00169BEB
		public SyncProperty<string> CompanyPartnership
		{
			get
			{
				return (SyncProperty<string>)base[SyncCompanySchema.CompanyPartnership];
			}
			set
			{
				base[SyncCompanySchema.CompanyPartnership] = value;
			}
		}

		// Token: 0x1700243E RID: 9278
		// (get) Token: 0x060066BD RID: 26301 RVA: 0x0016B9F9 File Offset: 0x00169BF9
		// (set) Token: 0x060066BE RID: 26302 RVA: 0x0016BA0B File Offset: 0x00169C0B
		public SyncProperty<string> Description
		{
			get
			{
				return (SyncProperty<string>)base[SyncCompanySchema.Description];
			}
			set
			{
				base[SyncCompanySchema.Description] = value;
			}
		}

		// Token: 0x1700243F RID: 9279
		// (get) Token: 0x060066BF RID: 26303 RVA: 0x0016BA19 File Offset: 0x00169C19
		// (set) Token: 0x060066C0 RID: 26304 RVA: 0x0016BA2B File Offset: 0x00169C2B
		public SyncProperty<string> DisplayName
		{
			get
			{
				return (SyncProperty<string>)base[SyncCompanySchema.DisplayName];
			}
			set
			{
				base[SyncCompanySchema.DisplayName] = value;
			}
		}

		// Token: 0x17002440 RID: 9280
		// (get) Token: 0x060066C1 RID: 26305 RVA: 0x0016BA39 File Offset: 0x00169C39
		// (set) Token: 0x060066C2 RID: 26306 RVA: 0x0016BA4B File Offset: 0x00169C4B
		public SyncProperty<MultiValuedProperty<CompanyVerifiedDomainValue>> VerifiedDomain
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<CompanyVerifiedDomainValue>>)base[SyncCompanySchema.VerifiedDomain];
			}
			set
			{
				base[SyncCompanySchema.VerifiedDomain] = value;
			}
		}

		// Token: 0x17002441 RID: 9281
		// (get) Token: 0x060066C3 RID: 26307 RVA: 0x0016BA59 File Offset: 0x00169C59
		// (set) Token: 0x060066C4 RID: 26308 RVA: 0x0016BA6B File Offset: 0x00169C6B
		public SyncProperty<MultiValuedProperty<AssignedPlanValue>> AssignedPlan
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<AssignedPlanValue>>)base[SyncCompanySchema.AssignedPlan];
			}
			set
			{
				base[SyncCompanySchema.AssignedPlan] = value;
			}
		}

		// Token: 0x17002442 RID: 9282
		// (get) Token: 0x060066C5 RID: 26309 RVA: 0x0016BA79 File Offset: 0x00169C79
		// (set) Token: 0x060066C6 RID: 26310 RVA: 0x0016BA8B File Offset: 0x00169C8B
		public SyncProperty<string> C
		{
			get
			{
				return (SyncProperty<string>)base[SyncCompanySchema.C];
			}
			set
			{
				base[SyncCompanySchema.C] = value;
			}
		}

		// Token: 0x17002443 RID: 9283
		// (get) Token: 0x060066C7 RID: 26311 RVA: 0x0016BA99 File Offset: 0x00169C99
		// (set) Token: 0x060066C8 RID: 26312 RVA: 0x0016BAAB File Offset: 0x00169CAB
		public SyncProperty<bool> IsDirSyncRunning
		{
			get
			{
				return (SyncProperty<bool>)base[SyncCompanySchema.IsDirSyncRunning];
			}
			set
			{
				base[SyncCompanySchema.IsDirSyncRunning] = value;
			}
		}

		// Token: 0x17002444 RID: 9284
		// (get) Token: 0x060066C9 RID: 26313 RVA: 0x0016BAB9 File Offset: 0x00169CB9
		// (set) Token: 0x060066CA RID: 26314 RVA: 0x0016BACB File Offset: 0x00169CCB
		public SyncProperty<MultiValuedProperty<string>> DirSyncStatus
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncCompanySchema.DirSyncStatus];
			}
			set
			{
				base[SyncCompanySchema.DirSyncStatus] = value;
			}
		}

		// Token: 0x17002445 RID: 9285
		// (get) Token: 0x060066CB RID: 26315 RVA: 0x0016BAD9 File Offset: 0x00169CD9
		// (set) Token: 0x060066CC RID: 26316 RVA: 0x0016BAEB File Offset: 0x00169CEB
		public SyncProperty<MultiValuedProperty<string>> DirSyncStatusAck
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncCompanySchema.DirSyncStatusAck];
			}
			set
			{
				base[SyncCompanySchema.DirSyncStatusAck] = value;
			}
		}

		// Token: 0x17002446 RID: 9286
		// (get) Token: 0x060066CD RID: 26317 RVA: 0x0016BAF9 File Offset: 0x00169CF9
		// (set) Token: 0x060066CE RID: 26318 RVA: 0x0016BB0B File Offset: 0x00169D0B
		public SyncProperty<int?> TenantType
		{
			get
			{
				return (SyncProperty<int?>)base[SyncCompanySchema.TenantType];
			}
			set
			{
				base[SyncCompanySchema.TenantType] = value;
			}
		}

		// Token: 0x17002447 RID: 9287
		// (get) Token: 0x060066CF RID: 26319 RVA: 0x0016BB19 File Offset: 0x00169D19
		// (set) Token: 0x060066D0 RID: 26320 RVA: 0x0016BB2B File Offset: 0x00169D2B
		public SyncProperty<int> QuotaAmount
		{
			get
			{
				return (SyncProperty<int>)base[SyncCompanySchema.QuotaAmount];
			}
			set
			{
				base[SyncCompanySchema.QuotaAmount] = value;
			}
		}

		// Token: 0x17002448 RID: 9288
		// (get) Token: 0x060066D1 RID: 26321 RVA: 0x0016BB39 File Offset: 0x00169D39
		// (set) Token: 0x060066D2 RID: 26322 RVA: 0x0016BB4B File Offset: 0x00169D4B
		public SyncProperty<MultiValuedProperty<string>> CompanyTags
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncCompanySchema.CompanyTags];
			}
			set
			{
				base[SyncCompanySchema.CompanyTags] = value;
			}
		}

		// Token: 0x17002449 RID: 9289
		// (get) Token: 0x060066D3 RID: 26323 RVA: 0x0016BB59 File Offset: 0x00169D59
		public SyncProperty<MultiValuedProperty<Capability>> PersistedCapabilities
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<Capability>>)base[SyncCompanySchema.PersistedCapabilities];
			}
		}

		// Token: 0x1700244A RID: 9290
		// (get) Token: 0x060066D4 RID: 26324 RVA: 0x0016BB6B File Offset: 0x00169D6B
		public string InitialDomainName
		{
			get
			{
				string result;
				if ((result = this.InitialDomainNameRetrieved) == null)
				{
					if (base.Id == null)
					{
						return base.ContextId;
					}
					result = base.Id.ToString();
				}
				return result;
			}
		}

		// Token: 0x1700244B RID: 9291
		// (get) Token: 0x060066D5 RID: 26325 RVA: 0x0016BB94 File Offset: 0x00169D94
		public bool IsInitialDomainOutBoundOnly
		{
			get
			{
				if (!this.VerifiedDomain.HasValue || this.VerifiedDomain.Value == null)
				{
					return false;
				}
				foreach (CompanyVerifiedDomainValue companyVerifiedDomainValue in this.VerifiedDomain.Value)
				{
					if (companyVerifiedDomainValue.Initial && !string.IsNullOrEmpty(companyVerifiedDomainValue.Name))
					{
						return (companyVerifiedDomainValue.Capabilities & SyncCompany.OutBoundOnlyFlag) != 0;
					}
				}
				return false;
			}
		}

		// Token: 0x060066D6 RID: 26326 RVA: 0x0016BC30 File Offset: 0x00169E30
		internal static object PersistedCapabilityGetter(IPropertyBag propertyBag)
		{
			return SyncCompany.GetEffectivePersistedCapabilities(propertyBag);
		}

		// Token: 0x060066D7 RID: 26327 RVA: 0x0016BC38 File Offset: 0x00169E38
		private static MultiValuedProperty<Capability> GetEffectivePersistedCapabilities(IPropertyBag propertyBag)
		{
			MultiValuedProperty<AssignedPlanValue> multiValuedProperty = (MultiValuedProperty<AssignedPlanValue>)propertyBag[SyncCompanySchema.AssignedPlan];
			MultiValuedProperty<Capability> multiValuedProperty2 = new MultiValuedProperty<Capability>();
			if (multiValuedProperty != null && multiValuedProperty.Count != 0)
			{
				foreach (AssignedPlanValue assignedPlanValue in multiValuedProperty)
				{
					Capability exchangeCapability = SyncUser.GetExchangeCapability(assignedPlanValue.Capability);
					if (assignedPlanValue.CapabilityStatus != AssignedCapabilityStatus.Deleted && exchangeCapability != Capability.None && !multiValuedProperty2.Contains(exchangeCapability))
					{
						multiValuedProperty2.Add(exchangeCapability);
					}
				}
			}
			return multiValuedProperty2;
		}

		// Token: 0x1700244C RID: 9292
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x0016BCCC File Offset: 0x00169ECC
		private string InitialDomainNameRetrieved
		{
			get
			{
				if (!this.VerifiedDomain.HasValue || this.VerifiedDomain.Value == null)
				{
					return null;
				}
				foreach (CompanyVerifiedDomainValue companyVerifiedDomainValue in this.VerifiedDomain.Value)
				{
					if (companyVerifiedDomainValue.Initial && !string.IsNullOrEmpty(companyVerifiedDomainValue.Name))
					{
						return companyVerifiedDomainValue.Name;
					}
				}
				return null;
			}
		}

		// Token: 0x040043D8 RID: 17368
		private static readonly SyncCompanySchema schema = ObjectSchema.GetInstance<SyncCompanySchema>();

		// Token: 0x040043D9 RID: 17369
		public static int OutBoundOnlyFlag = 4;
	}
}
