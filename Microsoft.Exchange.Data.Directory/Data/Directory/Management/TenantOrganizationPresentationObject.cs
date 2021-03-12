using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000774 RID: 1908
	[Serializable]
	public sealed class TenantOrganizationPresentationObject : ADPresentationObject
	{
		// Token: 0x170020CB RID: 8395
		// (get) Token: 0x06005D9F RID: 23967 RVA: 0x00142F54 File Offset: 0x00141154
		public static ADObjectId DefaultManagementSiteId
		{
			get
			{
				if (TenantOrganizationPresentationObject.defaultManagementSiteId == null)
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(TenantOrganizationPresentationObject.ExchangeCrossForestKey))
					{
						TenantOrganizationPresentationObject.defaultManagementSiteId = new ADObjectId((string)registryKey.GetValue(TenantOrganizationPresentationObject.DefaultManagementSiteLink, null));
					}
				}
				return TenantOrganizationPresentationObject.defaultManagementSiteId;
			}
		}

		// Token: 0x170020CC RID: 8396
		// (get) Token: 0x06005DA0 RID: 23968 RVA: 0x00142FB4 File Offset: 0x001411B4
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return TenantOrganizationPresentationObject.schema;
			}
		}

		// Token: 0x06005DA1 RID: 23969 RVA: 0x00142FBB File Offset: 0x001411BB
		public TenantOrganizationPresentationObject()
		{
		}

		// Token: 0x06005DA2 RID: 23970 RVA: 0x00142FC3 File Offset: 0x001411C3
		public TenantOrganizationPresentationObject(ExchangeConfigurationUnit dataObject) : base(dataObject)
		{
			this.PopulateTaskPopulatedProperties();
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x00142FE8 File Offset: 0x001411E8
		private void PopulateTaskPopulatedProperties()
		{
			RelocationConstraintArray relocationConstraintArray = (RelocationConstraintArray)this[OrganizationSchema.PersistedRelocationConstraints];
			MultiValuedProperty<RelocationConstraint> relocationConstraints = new MultiValuedProperty<RelocationConstraint>();
			if (relocationConstraintArray != null && relocationConstraintArray.RelocationConstraints != null)
			{
				Array.ForEach<RelocationConstraint>(relocationConstraintArray.RelocationConstraints, delegate(RelocationConstraint x)
				{
					relocationConstraints.Add(x);
				});
			}
			if (this.AdminDisplayVersion != null)
			{
				ExchangeBuild exchangeBuild = this.AdminDisplayVersion.ExchangeBuild;
				if (this.AdminDisplayVersion.ExchangeBuild.ToString().StartsWith("14"))
				{
					relocationConstraints.Add(new RelocationConstraint(RelocationConstraintType.TenantVersionConstraint, DateTime.MaxValue));
				}
			}
			if (this.OrganizationStatus != OrganizationStatus.Active || this.IsUpgradingOrganization || this.IsPilotingOrganization || this.IsUpgradeOperationInProgress || this.IsFfoMigrationInProgress || this.IsUpdatingServicePlan)
			{
				relocationConstraints.Add(new RelocationConstraint(RelocationConstraintType.TenantInTransitionConstraint, DateTime.MaxValue));
			}
			if (!this.IsValid)
			{
				relocationConstraints.Add(new RelocationConstraint(RelocationConstraintType.ValidationErrorConstraint, DateTime.MaxValue));
			}
			if (this.EnableAsSharedConfiguration || this.ImmutableConfiguration)
			{
				relocationConstraints.Add(new RelocationConstraint(RelocationConstraintType.SCTConstraint, DateTime.MaxValue));
			}
			if (this.UpgradeE14MbxCountForCurrentStage != null && this.UpgradeE14MbxCountForCurrentStage.Value != 0)
			{
				relocationConstraints.Add(new RelocationConstraint(RelocationConstraintType.E14MailboxesPresentContraint, DateTime.MaxValue));
			}
			if (!string.IsNullOrEmpty((string)this[ExchangeConfigurationUnitSchema.TargetForest]) || !string.IsNullOrEmpty((string)this[ExchangeConfigurationUnitSchema.RelocationSourceForestRaw]))
			{
				relocationConstraints.Add(new RelocationConstraint(RelocationConstraintType.RelocationInProgressConstraint, DateTime.MaxValue));
			}
			relocationConstraints.Sort();
			this[TenantOrganizationPresentationObjectSchema.RelocationConstraints] = relocationConstraints;
			if (this.IsTemplateTenant)
			{
				this.IsSharingConfiguration = true;
			}
		}

		// Token: 0x170020CD RID: 8397
		// (get) Token: 0x06005DA4 RID: 23972 RVA: 0x001431C7 File Offset: 0x001413C7
		// (set) Token: 0x06005DA5 RID: 23973 RVA: 0x001431D9 File Offset: 0x001413D9
		public OrganizationStatus OrganizationStatus
		{
			get
			{
				return (OrganizationStatus)((int)this[TenantOrganizationPresentationObjectSchema.OrganizationStatus]);
			}
			internal set
			{
				this[TenantOrganizationPresentationObjectSchema.OrganizationStatus] = (int)value;
			}
		}

		// Token: 0x170020CE RID: 8398
		// (get) Token: 0x06005DA6 RID: 23974 RVA: 0x001431EC File Offset: 0x001413EC
		public DateTime? WhenOrganizationStatusSet
		{
			get
			{
				return (DateTime?)this[TenantOrganizationPresentationObjectSchema.WhenOrganizationStatusSet];
			}
		}

		// Token: 0x170020CF RID: 8399
		// (get) Token: 0x06005DA7 RID: 23975 RVA: 0x001431FE File Offset: 0x001413FE
		public bool IsUpgradingOrganization
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsUpgradingOrganization];
			}
		}

		// Token: 0x170020D0 RID: 8400
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x00143210 File Offset: 0x00141410
		public bool IsPilotingOrganization
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsPilotingOrganization];
			}
		}

		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x06005DA9 RID: 23977 RVA: 0x00143222 File Offset: 0x00141422
		public bool IsUpgradeOperationInProgress
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsUpgradeOperationInProgress];
			}
		}

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x00143234 File Offset: 0x00141434
		public new OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)this[TenantOrganizationPresentationObjectSchema.OrganizationId];
			}
		}

		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x06005DAB RID: 23979 RVA: 0x00143246 File Offset: 0x00141446
		public string ServicePlan
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.ServicePlan];
			}
		}

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x06005DAC RID: 23980 RVA: 0x00143258 File Offset: 0x00141458
		public bool IsUpdatingServicePlan
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsUpdatingServicePlan];
			}
		}

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x06005DAD RID: 23981 RVA: 0x0014326A File Offset: 0x0014146A
		public string TargetServicePlan
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.TargetServicePlan];
			}
		}

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x06005DAE RID: 23982 RVA: 0x0014327C File Offset: 0x0014147C
		public bool IsFfoMigrationInProgress
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsFfoMigrationInProgress];
			}
		}

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x06005DAF RID: 23983 RVA: 0x0014328E File Offset: 0x0014148E
		public string ProgramId
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.ProgramId];
			}
		}

		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x001432A0 File Offset: 0x001414A0
		public string OfferId
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.OfferId];
			}
		}

		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x06005DB1 RID: 23985 RVA: 0x001432B2 File Offset: 0x001414B2
		public bool ExcludedFromBackSync
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.ExcludedFromBackSync];
			}
		}

		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x001432C4 File Offset: 0x001414C4
		public bool ExcludedFromForwardSyncEDU2BPOS
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.ExcludedFromForwardSyncEDU2BPOS];
			}
		}

		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x06005DB3 RID: 23987 RVA: 0x001432D6 File Offset: 0x001414D6
		public bool AllowDeleteOfExternalIdentityUponRemove
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.AllowDeleteOfExternalIdentityUponRemove];
			}
		}

		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x001432E8 File Offset: 0x001414E8
		public string ExternalDirectoryOrganizationId
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.ExternalDirectoryOrganizationId];
			}
		}

		// Token: 0x170020DD RID: 8413
		// (get) Token: 0x06005DB5 RID: 23989 RVA: 0x001432FA File Offset: 0x001414FA
		// (set) Token: 0x06005DB6 RID: 23990 RVA: 0x00143311 File Offset: 0x00141511
		public new string Name
		{
			get
			{
				return base.DataObject.OrganizationId.OrganizationalUnit.Name;
			}
			internal set
			{
				this[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x170020DE RID: 8414
		// (get) Token: 0x06005DB7 RID: 23991 RVA: 0x0014331F File Offset: 0x0014151F
		// (set) Token: 0x06005DB8 RID: 23992 RVA: 0x00143336 File Offset: 0x00141536
		public new string DistinguishedName
		{
			get
			{
				return base.DataObject.OrganizationId.OrganizationalUnit.DistinguishedName;
			}
			internal set
			{
				this[ADObjectSchema.DistinguishedName] = value;
			}
		}

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x06005DB9 RID: 23993 RVA: 0x00143344 File Offset: 0x00141544
		public new ObjectId Identity
		{
			get
			{
				ObjectId objectId = base.DataObject.OrganizationId.OrganizationalUnit;
				object obj;
				if (base.TryConvertOutputProperty(objectId, "Identity", out obj))
				{
					objectId = (ObjectId)obj;
				}
				return objectId;
			}
		}

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x06005DBA RID: 23994 RVA: 0x0014337A File Offset: 0x0014157A
		public new Guid Guid
		{
			get
			{
				return base.DataObject.OrganizationId.OrganizationalUnit.ObjectGuid;
			}
		}

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x06005DBB RID: 23995 RVA: 0x00143391 File Offset: 0x00141591
		public int ObjectVersion
		{
			get
			{
				return (int)this[OrganizationSchema.ObjectVersion];
			}
		}

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x06005DBC RID: 23996 RVA: 0x001433A3 File Offset: 0x001415A3
		public int BuildMajor
		{
			get
			{
				return (int)this[OrganizationSchema.BuildMajor];
			}
		}

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x06005DBD RID: 23997 RVA: 0x001433B5 File Offset: 0x001415B5
		public int BuildMinor
		{
			get
			{
				return (int)this[OrganizationSchema.BuildMinor];
			}
		}

		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x06005DBE RID: 23998 RVA: 0x001433C7 File Offset: 0x001415C7
		public bool IsFederated
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsFederated];
			}
		}

		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x06005DBF RID: 23999 RVA: 0x001433D9 File Offset: 0x001415D9
		public bool SkipToUAndParentalControlCheck
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.SkipToUAndParentalControlCheck];
			}
		}

		// Token: 0x170020E6 RID: 8422
		// (get) Token: 0x06005DC0 RID: 24000 RVA: 0x001433EB File Offset: 0x001415EB
		public bool HideAdminAccessWarning
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.HideAdminAccessWarning];
			}
		}

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x06005DC1 RID: 24001 RVA: 0x001433FD File Offset: 0x001415FD
		public bool ShowAdminAccessWarning
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.ShowAdminAccessWarning];
			}
		}

		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x06005DC2 RID: 24002 RVA: 0x0014340F File Offset: 0x0014160F
		public bool SMTPAddressCheckWithAcceptedDomain
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.SMTPAddressCheckWithAcceptedDomain];
			}
		}

		// Token: 0x170020E9 RID: 8425
		// (get) Token: 0x06005DC3 RID: 24003 RVA: 0x00143424 File Offset: 0x00141624
		public ADObjectId ManagementSiteLink
		{
			get
			{
				ADObjectId adobjectId = (ADObjectId)this[TenantOrganizationPresentationObjectSchema.ManagementSiteLink];
				if (adobjectId != null)
				{
					return adobjectId;
				}
				return TenantOrganizationPresentationObject.DefaultManagementSiteId;
			}
		}

		// Token: 0x170020EA RID: 8426
		// (get) Token: 0x06005DC4 RID: 24004 RVA: 0x0014344C File Offset: 0x0014164C
		public bool EnableAsSharedConfiguration
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.EnableAsSharedConfiguration];
			}
		}

		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x06005DC5 RID: 24005 RVA: 0x0014345E File Offset: 0x0014165E
		public bool ImmutableConfiguration
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.ImmutableConfiguration];
			}
		}

		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x00143470 File Offset: 0x00141670
		public MultiValuedProperty<ADObjectId> SupportedSharedConfigurations
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[TenantOrganizationPresentationObjectSchema.SupportedSharedConfigurations];
			}
		}

		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x00143482 File Offset: 0x00141682
		public SharedConfigurationInfo SharedConfigurationInfo
		{
			get
			{
				return (SharedConfigurationInfo)this[TenantOrganizationPresentationObjectSchema.SharedConfigurationInfo];
			}
		}

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x06005DC8 RID: 24008 RVA: 0x00143494 File Offset: 0x00141694
		public bool IsTemplateTenant
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsTemplateTenant];
			}
		}

		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x06005DC9 RID: 24009 RVA: 0x001434A6 File Offset: 0x001416A6
		// (set) Token: 0x06005DCA RID: 24010 RVA: 0x001434B8 File Offset: 0x001416B8
		public bool IsSharingConfiguration
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsSharingConfiguration];
			}
			internal set
			{
				this[TenantOrganizationPresentationObjectSchema.IsSharingConfiguration] = value;
			}
		}

		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x06005DCB RID: 24011 RVA: 0x001434CB File Offset: 0x001416CB
		public bool IsDehydrated
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsDehydrated];
			}
		}

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x06005DCC RID: 24012 RVA: 0x001434DD File Offset: 0x001416DD
		public bool IsStaticConfigurationShared
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsStaticConfigurationShared];
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x06005DCD RID: 24013 RVA: 0x001434EF File Offset: 0x001416EF
		public bool IsLicensingEnforced
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsLicensingEnforced];
			}
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x06005DCE RID: 24014 RVA: 0x00143501 File Offset: 0x00141701
		public bool IsTenantAccessBlocked
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsTenantAccessBlocked];
			}
		}

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x06005DCF RID: 24015 RVA: 0x00143513 File Offset: 0x00141713
		public bool MSOSyncEnabled
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.MSOSyncEnabled];
			}
		}

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x06005DD0 RID: 24016 RVA: 0x00143525 File Offset: 0x00141725
		public int? MaxAddressBookPolicies
		{
			get
			{
				return (int?)this[TenantOrganizationPresentationObjectSchema.MaxAddressBookPolicies];
			}
		}

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x06005DD1 RID: 24017 RVA: 0x00143537 File Offset: 0x00141737
		public int? MaxOfflineAddressBooks
		{
			get
			{
				return (int?)this[TenantOrganizationPresentationObjectSchema.MaxOfflineAddressBooks];
			}
		}

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x00143549 File Offset: 0x00141749
		public bool UseServicePlanAsCounterInstanceName
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.UseServicePlanAsCounterInstanceName];
			}
		}

		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0014355B File Offset: 0x0014175B
		public Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TenantOrganizationPresentationObjectSchema.DefaultPublicFolderIssueWarningQuota];
			}
		}

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x0014356D File Offset: 0x0014176D
		public Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[TenantOrganizationPresentationObjectSchema.DefaultPublicFolderProhibitPostQuota];
			}
		}

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x0014357F File Offset: 0x0014177F
		public MultiValuedProperty<string> AsynchronousOperationIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[TenantOrganizationPresentationObjectSchema.AsynchronousOperationIds];
			}
		}

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x06005DD6 RID: 24022 RVA: 0x00143591 File Offset: 0x00141791
		public bool IsDirSyncRunning
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsDirSyncRunning];
			}
		}

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x001435A3 File Offset: 0x001417A3
		public bool IsDirSyncStatusPending
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.IsDirSyncStatusPending];
			}
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x001435B5 File Offset: 0x001417B5
		public MultiValuedProperty<string> DirSyncStatus
		{
			get
			{
				return (MultiValuedProperty<string>)this[TenantOrganizationPresentationObjectSchema.DirSyncStatus];
			}
		}

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x06005DD9 RID: 24025 RVA: 0x001435C7 File Offset: 0x001417C7
		public ADObjectId ExchangeUpgradeBucket
		{
			get
			{
				return (ADObjectId)this[TenantOrganizationPresentationObjectSchema.ExchangeUpgradeBucket];
			}
		}

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x06005DDA RID: 24026 RVA: 0x001435D9 File Offset: 0x001417D9
		public ExchangeObjectVersion AdminDisplayVersion
		{
			get
			{
				return (ExchangeObjectVersion)this[TenantOrganizationPresentationObjectSchema.AdminDisplayVersion];
			}
		}

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x06005DDB RID: 24027 RVA: 0x001435EB File Offset: 0x001417EB
		public SoftDeletedFeatureStatusFlags SoftDeletedFeatureStatus
		{
			get
			{
				return (SoftDeletedFeatureStatusFlags)this[TenantOrganizationPresentationObjectSchema.SoftDeletedFeatureStatus];
			}
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x06005DDC RID: 24028 RVA: 0x001435FD File Offset: 0x001417FD
		public string DirSyncServiceInstance
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.DirSyncServiceInstance];
			}
		}

		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x06005DDD RID: 24029 RVA: 0x0014360F File Offset: 0x0014180F
		public string Location
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.Location];
			}
		}

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x06005DDE RID: 24030 RVA: 0x00143621 File Offset: 0x00141821
		public MultiValuedProperty<string> CompanyTags
		{
			get
			{
				return (MultiValuedProperty<string>)this[TenantOrganizationPresentationObjectSchema.CompanyTags];
			}
		}

		// Token: 0x17002104 RID: 8452
		// (get) Token: 0x06005DDF RID: 24031 RVA: 0x00143633 File Offset: 0x00141833
		public MultiValuedProperty<Capability> PersistedCapabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)this[TenantOrganizationPresentationObjectSchema.PersistedCapabilities];
			}
		}

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x00143645 File Offset: 0x00141845
		public UpgradeStatusTypes UpgradeStatus
		{
			get
			{
				return (UpgradeStatusTypes)this[TenantOrganizationPresentationObjectSchema.UpgradeStatus];
			}
		}

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x06005DE1 RID: 24033 RVA: 0x00143657 File Offset: 0x00141857
		public UpgradeRequestTypes UpgradeRequest
		{
			get
			{
				return (UpgradeRequestTypes)this[TenantOrganizationPresentationObjectSchema.UpgradeRequest];
			}
		}

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x06005DE2 RID: 24034 RVA: 0x00143669 File Offset: 0x00141869
		public UpgradeConstraintArray UpgradeConstraints
		{
			get
			{
				return (UpgradeConstraintArray)this[TenantOrganizationPresentationObjectSchema.UpgradeConstraints];
			}
		}

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x0014367B File Offset: 0x0014187B
		public string UpgradeMessage
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.UpgradeMessage];
			}
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x06005DE4 RID: 24036 RVA: 0x0014368D File Offset: 0x0014188D
		public string UpgradeDetails
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.UpgradeDetails];
			}
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x06005DE5 RID: 24037 RVA: 0x0014369F File Offset: 0x0014189F
		public UpgradeStage? UpgradeStage
		{
			get
			{
				return (UpgradeStage?)this[TenantOrganizationPresentationObjectSchema.UpgradeStage];
			}
		}

		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x06005DE6 RID: 24038 RVA: 0x001436B1 File Offset: 0x001418B1
		public DateTime? UpgradeStageTimeStamp
		{
			get
			{
				return (DateTime?)this[TenantOrganizationPresentationObjectSchema.UpgradeStageTimeStamp];
			}
		}

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x001436C3 File Offset: 0x001418C3
		public int? UpgradeE14RequestCountForCurrentStage
		{
			get
			{
				return (int?)this[TenantOrganizationPresentationObjectSchema.UpgradeE14RequestCountForCurrentStage];
			}
		}

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x001436D5 File Offset: 0x001418D5
		public int? UpgradeE14MbxCountForCurrentStage
		{
			get
			{
				return (int?)this[TenantOrganizationPresentationObjectSchema.UpgradeE14MbxCountForCurrentStage];
			}
		}

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x001436E7 File Offset: 0x001418E7
		public bool? UpgradeConstraintsDisabled
		{
			get
			{
				return (bool?)this[TenantOrganizationPresentationObjectSchema.UpgradeConstraintsDisabled];
			}
		}

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x001436F9 File Offset: 0x001418F9
		public int? UpgradeUnitsOverride
		{
			get
			{
				return (int?)this[TenantOrganizationPresentationObjectSchema.UpgradeUnitsOverride];
			}
		}

		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x06005DEB RID: 24043 RVA: 0x0014370B File Offset: 0x0014190B
		public DateTime? UpgradeLastE14CountsUpdateTime
		{
			get
			{
				return (DateTime?)this[TenantOrganizationPresentationObjectSchema.UpgradeLastE14CountsUpdateTime];
			}
		}

		// Token: 0x17002111 RID: 8465
		// (get) Token: 0x06005DEC RID: 24044 RVA: 0x0014371D File Offset: 0x0014191D
		public int DefaultMovePriority
		{
			get
			{
				return (int)this[TenantOrganizationPresentationObjectSchema.DefaultMovePriority];
			}
		}

		// Token: 0x17002112 RID: 8466
		// (get) Token: 0x06005DED RID: 24045 RVA: 0x00143730 File Offset: 0x00141930
		public MailboxRelease MailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[TenantOrganizationPresentationObjectSchema.MailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
		}

		// Token: 0x17002113 RID: 8467
		// (get) Token: 0x06005DEE RID: 24046 RVA: 0x0014375C File Offset: 0x0014195C
		public MailboxRelease PreviousMailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[TenantOrganizationPresentationObjectSchema.PreviousMailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
		}

		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x06005DEF RID: 24047 RVA: 0x00143788 File Offset: 0x00141988
		public MailboxRelease PilotMailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[TenantOrganizationPresentationObjectSchema.PilotMailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
		}

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x06005DF0 RID: 24048 RVA: 0x001437B2 File Offset: 0x001419B2
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)this[TenantOrganizationPresentationObjectSchema.ReleaseTrack];
			}
		}

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x06005DF1 RID: 24049 RVA: 0x001437C4 File Offset: 0x001419C4
		public MultiValuedProperty<RelocationConstraint> RelocationConstraints
		{
			get
			{
				return (MultiValuedProperty<RelocationConstraint>)this[TenantOrganizationPresentationObjectSchema.RelocationConstraints];
			}
		}

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x06005DF2 RID: 24050 RVA: 0x001437D6 File Offset: 0x001419D6
		public bool OriginatedInDifferentForest
		{
			get
			{
				return (bool)this[TenantOrganizationPresentationObjectSchema.OriginatedInDifferentForest];
			}
		}

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x06005DF3 RID: 24051 RVA: 0x001437E8 File Offset: 0x001419E8
		public string AdminDisplayName
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.AdminDisplayName];
			}
		}

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x06005DF4 RID: 24052 RVA: 0x001437FA File Offset: 0x001419FA
		public string IOwnMigrationTenant
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.IOwnMigrationTenant];
			}
		}

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x06005DF5 RID: 24053 RVA: 0x0014380C File Offset: 0x00141A0C
		public IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
		{
			get
			{
				return (IOwnMigrationStatusFlagsEnum)this[TenantOrganizationPresentationObjectSchema.IOwnMigrationStatus];
			}
		}

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x0014381E File Offset: 0x00141A1E
		public string IOwnMigrationStatusReport
		{
			get
			{
				return (string)this[TenantOrganizationPresentationObjectSchema.IOwnMigrationStatusReport];
			}
		}

		// Token: 0x04003F84 RID: 16260
		private static readonly string ExchangeCrossForestKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeCrossForest\\";

		// Token: 0x04003F85 RID: 16261
		private static readonly string DefaultManagementSiteLink = "DefaultManagementSiteLink";

		// Token: 0x04003F86 RID: 16262
		private static ADObjectId defaultManagementSiteId;

		// Token: 0x04003F87 RID: 16263
		private static TenantOrganizationPresentationObjectSchema schema = ObjectSchema.GetInstance<TenantOrganizationPresentationObjectSchema>();
	}
}
