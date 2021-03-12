using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002D6 RID: 726
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ExchangeConfigurationUnit : Organization, IProvisioningCacheInvalidation
	{
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x00094F1F File Offset: 0x0009311F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeConfigurationUnit.MostDerivedClass;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x00094F26 File Offset: 0x00093126
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeConfigurationUnit.schema;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x00094F2D File Offset: 0x0009312D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2007;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x00094F34 File Offset: 0x00093134
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x00094F4B File Offset: 0x0009314B
		public MultiValuedProperty<DNWithBinary> OtherWellKnownObjects
		{
			get
			{
				return (MultiValuedProperty<DNWithBinary>)this.propertyBag[ExchangeConfigurationUnitSchema.OtherWellKnownObjects];
			}
			internal set
			{
				this.propertyBag[ExchangeConfigurationUnitSchema.OtherWellKnownObjects] = value;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x00094F5E File Offset: 0x0009315E
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x00094F70 File Offset: 0x00093170
		public bool IsFederated
		{
			get
			{
				return (bool)this[OrganizationSchema.IsFederated];
			}
			internal set
			{
				this[OrganizationSchema.IsFederated] = value;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x00094F83 File Offset: 0x00093183
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x00094F95 File Offset: 0x00093195
		[Parameter(Mandatory = false)]
		public bool IsHotmailMigration
		{
			get
			{
				return (bool)this[OrganizationSchema.IsHotmailMigration];
			}
			set
			{
				this[OrganizationSchema.IsHotmailMigration] = value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x00094FA8 File Offset: 0x000931A8
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x00094FBA File Offset: 0x000931BA
		public bool HideAdminAccessWarning
		{
			get
			{
				return (bool)this[OrganizationSchema.HideAdminAccessWarning];
			}
			internal set
			{
				this[OrganizationSchema.HideAdminAccessWarning] = value;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x00094FCD File Offset: 0x000931CD
		// (set) Token: 0x060021B6 RID: 8630 RVA: 0x00094FDF File Offset: 0x000931DF
		public bool SMTPAddressCheckWithAcceptedDomain
		{
			get
			{
				return (bool)this[OrganizationSchema.SMTPAddressCheckWithAcceptedDomain];
			}
			internal set
			{
				this[OrganizationSchema.SMTPAddressCheckWithAcceptedDomain] = value;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x00094FF2 File Offset: 0x000931F2
		public bool ShowAdminAccessWarning
		{
			get
			{
				return (bool)this[OrganizationSchema.ShowAdminAccessWarning];
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x00095004 File Offset: 0x00093204
		// (set) Token: 0x060021B9 RID: 8633 RVA: 0x00095016 File Offset: 0x00093216
		public bool SkipToUAndParentalControlCheck
		{
			get
			{
				return (bool)this[OrganizationSchema.SkipToUAndParentalControlCheck];
			}
			internal set
			{
				this[OrganizationSchema.SkipToUAndParentalControlCheck] = value;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x00095029 File Offset: 0x00093229
		// (set) Token: 0x060021BB RID: 8635 RVA: 0x0009503B File Offset: 0x0009323B
		public bool IsUpgradingOrganization
		{
			get
			{
				return (bool)this[OrganizationSchema.IsUpgradingOrganization];
			}
			internal set
			{
				this[OrganizationSchema.IsUpgradingOrganization] = value;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x0009504E File Offset: 0x0009324E
		// (set) Token: 0x060021BD RID: 8637 RVA: 0x00095060 File Offset: 0x00093260
		public bool IsPilotingOrganization
		{
			get
			{
				return (bool)this[OrganizationSchema.IsPilotingOrganization];
			}
			internal set
			{
				this[OrganizationSchema.IsPilotingOrganization] = value;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x00095073 File Offset: 0x00093273
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x00095085 File Offset: 0x00093285
		public bool IsUpdatingServicePlan
		{
			get
			{
				return (bool)this[OrganizationSchema.IsUpdatingServicePlan];
			}
			internal set
			{
				this[OrganizationSchema.IsUpdatingServicePlan] = value;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x00095098 File Offset: 0x00093298
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x000950AA File Offset: 0x000932AA
		internal bool IsStaticConfigurationShared
		{
			get
			{
				return (bool)this[OrganizationSchema.IsStaticConfigurationShared];
			}
			set
			{
				this[OrganizationSchema.IsStaticConfigurationShared] = value;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x000950BD File Offset: 0x000932BD
		// (set) Token: 0x060021C3 RID: 8643 RVA: 0x000950CF File Offset: 0x000932CF
		public bool IsUpgradeOperationInProgress
		{
			get
			{
				return (bool)this[OrganizationSchema.IsUpgradeOperationInProgress];
			}
			internal set
			{
				this[OrganizationSchema.IsUpgradeOperationInProgress] = value;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x000950E2 File Offset: 0x000932E2
		public bool SyncMEUSMTPToMServ
		{
			get
			{
				return (int)this[OrganizationSchema.ObjectVersion] < 13000 || (bool)this[OrganizationSchema.SMTPAddressCheckWithAcceptedDomain];
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0009510D File Offset: 0x0009330D
		// (set) Token: 0x060021C6 RID: 8646 RVA: 0x0009511F File Offset: 0x0009331F
		[Parameter(Mandatory = false)]
		public bool SyncMBXAndDLToMServ
		{
			get
			{
				return (bool)this[OrganizationSchema.SyncMBXAndDLToMServ];
			}
			set
			{
				this[OrganizationSchema.SyncMBXAndDLToMServ] = value;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x00095132 File Offset: 0x00093332
		public bool IsOrganizationReadyForMservSync
		{
			get
			{
				return this.OrganizationStatus != OrganizationStatus.Invalid && this.OrganizationStatus != OrganizationStatus.PendingCompletion;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x0009514A File Offset: 0x0009334A
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x0009515C File Offset: 0x0009335C
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)this[OrganizationSchema.ReleaseTrack];
			}
			set
			{
				this[OrganizationSchema.ReleaseTrack] = value;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x0009516F File Offset: 0x0009336F
		// (set) Token: 0x060021CB RID: 8651 RVA: 0x00095181 File Offset: 0x00093381
		[Parameter(Mandatory = false)]
		public OrganizationStatus OrganizationStatus
		{
			get
			{
				return (OrganizationStatus)((int)this[ExchangeConfigurationUnitSchema.OrganizationStatus]);
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.OrganizationStatus] = (int)value;
				this.WhenOrganizationStatusSet = new DateTime?(DateTime.UtcNow);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x000951A4 File Offset: 0x000933A4
		// (set) Token: 0x060021CD RID: 8653 RVA: 0x000951B6 File Offset: 0x000933B6
		public DateTime? WhenOrganizationStatusSet
		{
			get
			{
				return (DateTime?)this[ExchangeConfigurationUnitSchema.WhenOrganizationStatusSet];
			}
			private set
			{
				this[ExchangeConfigurationUnitSchema.WhenOrganizationStatusSet] = value;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000951C9 File Offset: 0x000933C9
		public new OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)this[ExchangeConfigurationUnitSchema.OrganizationId];
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x000951DB File Offset: 0x000933DB
		// (set) Token: 0x060021D0 RID: 8656 RVA: 0x000951ED File Offset: 0x000933ED
		[Parameter(Mandatory = false)]
		public string IOwnMigrationTenant
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.IOwnMigrationTenant];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.IOwnMigrationTenant] = value;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000951FB File Offset: 0x000933FB
		// (set) Token: 0x060021D2 RID: 8658 RVA: 0x0009520D File Offset: 0x0009340D
		[Parameter(Mandatory = false)]
		public string IOwnMigrationStatusReport
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.IOwnMigrationStatusReport];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.IOwnMigrationStatusReport] = value;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0009521B File Offset: 0x0009341B
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x0009522D File Offset: 0x0009342D
		[Parameter(Mandatory = false)]
		public IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
		{
			get
			{
				return (IOwnMigrationStatusFlagsEnum)((byte)this[ExchangeConfigurationUnitSchema.IOwnMigrationStatus]);
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.IOwnMigrationStatus] = value;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x00095240 File Offset: 0x00093440
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x00095252 File Offset: 0x00093452
		public string ServicePlan
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.ServicePlan];
			}
			internal set
			{
				this[ExchangeConfigurationUnitSchema.ServicePlan] = value;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x00095260 File Offset: 0x00093460
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x00095272 File Offset: 0x00093472
		public string TargetServicePlan
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.TargetServicePlan];
			}
			internal set
			{
				this[ExchangeConfigurationUnitSchema.TargetServicePlan] = value;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00095280 File Offset: 0x00093480
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x00095292 File Offset: 0x00093492
		public string ProgramId
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.ProgramId];
			}
			internal set
			{
				this[ExchangeConfigurationUnitSchema.ProgramId] = value;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x000952A0 File Offset: 0x000934A0
		// (set) Token: 0x060021DC RID: 8668 RVA: 0x000952B2 File Offset: 0x000934B2
		public string OfferId
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.OfferId];
			}
			internal set
			{
				this[ExchangeConfigurationUnitSchema.OfferId] = value;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x000952C0 File Offset: 0x000934C0
		// (set) Token: 0x060021DE RID: 8670 RVA: 0x000952D2 File Offset: 0x000934D2
		public string ExternalDirectoryOrganizationId
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId] = value;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x000952E0 File Offset: 0x000934E0
		// (set) Token: 0x060021E0 RID: 8672 RVA: 0x000952F2 File Offset: 0x000934F2
		internal string ResellerId
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.ResellerId];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.ResellerId] = value;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x00095300 File Offset: 0x00093500
		internal string DirSyncId
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.DirSyncId];
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x00095312 File Offset: 0x00093512
		// (set) Token: 0x060021E3 RID: 8675 RVA: 0x00095324 File Offset: 0x00093524
		public bool ExcludedFromBackSync
		{
			get
			{
				return (bool)this[OrganizationSchema.ExcludedFromBackSync];
			}
			set
			{
				this[OrganizationSchema.ExcludedFromBackSync] = value;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x00095337 File Offset: 0x00093537
		// (set) Token: 0x060021E5 RID: 8677 RVA: 0x00095349 File Offset: 0x00093549
		public bool ExcludedFromForwardSyncEDU2BPOS
		{
			get
			{
				return (bool)this[OrganizationSchema.ExcludedFromForwardSyncEDU2BPOS];
			}
			set
			{
				this[OrganizationSchema.ExcludedFromForwardSyncEDU2BPOS] = value;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0009535C File Offset: 0x0009355C
		// (set) Token: 0x060021E7 RID: 8679 RVA: 0x0009536E File Offset: 0x0009356E
		[Parameter(Mandatory = false)]
		public bool MSOSyncEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.MSOSyncEnabled];
			}
			set
			{
				this[OrganizationSchema.MSOSyncEnabled] = value;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x00095381 File Offset: 0x00093581
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x00095393 File Offset: 0x00093593
		[Parameter(Mandatory = false)]
		public override Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[OrganizationSchema.DefaultPublicFolderIssueWarningQuota];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderIssueWarningQuota] = value;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x000953A6 File Offset: 0x000935A6
		// (set) Token: 0x060021EB RID: 8683 RVA: 0x000953B8 File Offset: 0x000935B8
		[Parameter(Mandatory = false)]
		public override Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[OrganizationSchema.DefaultPublicFolderMaxItemSize];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderMaxItemSize] = value;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x000953CB File Offset: 0x000935CB
		// (set) Token: 0x060021ED RID: 8685 RVA: 0x000953DD File Offset: 0x000935DD
		[Parameter(Mandatory = false)]
		public override Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[OrganizationSchema.DefaultPublicFolderProhibitPostQuota];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderProhibitPostQuota] = value;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x000953F0 File Offset: 0x000935F0
		// (set) Token: 0x060021EF RID: 8687 RVA: 0x00095402 File Offset: 0x00093602
		internal ADObjectId ExchangeUpgradeBucket
		{
			get
			{
				return (ADObjectId)this[ExchangeConfigurationUnitSchema.ExchangeUpgradeBucket];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.ExchangeUpgradeBucket] = value;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x00095410 File Offset: 0x00093610
		// (set) Token: 0x060021F1 RID: 8689 RVA: 0x00095422 File Offset: 0x00093622
		internal ExchangeObjectVersion AdminDisplayVersion
		{
			get
			{
				return (ExchangeObjectVersion)this[OrganizationSchema.AdminDisplayVersion];
			}
			set
			{
				this[OrganizationSchema.AdminDisplayVersion] = value;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x00095430 File Offset: 0x00093630
		// (set) Token: 0x060021F3 RID: 8691 RVA: 0x00095442 File Offset: 0x00093642
		[Parameter(Mandatory = false)]
		public bool IsDirSyncRunning
		{
			get
			{
				return (bool)this[OrganizationSchema.IsDirSyncRunning];
			}
			set
			{
				this[OrganizationSchema.IsDirSyncRunning] = value;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x00095455 File Offset: 0x00093655
		// (set) Token: 0x060021F5 RID: 8693 RVA: 0x00095467 File Offset: 0x00093667
		public bool IsDirSyncStatusPending
		{
			get
			{
				return (bool)this[OrganizationSchema.IsDirSyncStatusPending];
			}
			private set
			{
				this[OrganizationSchema.IsDirSyncStatusPending] = value;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x0009547A File Offset: 0x0009367A
		internal new OrganizationConfigXML ConfigXML
		{
			get
			{
				if (base.ConfigXML == null)
				{
					base.ConfigXML = new OrganizationConfigXML();
				}
				return base.ConfigXML;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x00095495 File Offset: 0x00093695
		// (set) Token: 0x060021F8 RID: 8696 RVA: 0x000954A7 File Offset: 0x000936A7
		[Parameter(Mandatory = false)]
		public UpgradeStatusTypes UpgradeStatus
		{
			get
			{
				return (UpgradeStatusTypes)this[OrganizationSchema.UpgradeStatus];
			}
			set
			{
				this[OrganizationSchema.UpgradeStatus] = value;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x000954BA File Offset: 0x000936BA
		// (set) Token: 0x060021FA RID: 8698 RVA: 0x000954CC File Offset: 0x000936CC
		[Parameter(Mandatory = false)]
		public UpgradeRequestTypes UpgradeRequest
		{
			get
			{
				return (UpgradeRequestTypes)this[OrganizationSchema.UpgradeRequest];
			}
			set
			{
				this[OrganizationSchema.UpgradeRequest] = value;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x000954DF File Offset: 0x000936DF
		// (set) Token: 0x060021FC RID: 8700 RVA: 0x000954F1 File Offset: 0x000936F1
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> CompanyTags
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.CompanyTags];
			}
			set
			{
				this[OrganizationSchema.CompanyTags] = value;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000954FF File Offset: 0x000936FF
		// (set) Token: 0x060021FE RID: 8702 RVA: 0x00095511 File Offset: 0x00093711
		[Parameter(Mandatory = false)]
		public string Location
		{
			get
			{
				return (string)this[OrganizationSchema.Location];
			}
			set
			{
				this[OrganizationSchema.Location] = value;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x0009551F File Offset: 0x0009371F
		// (set) Token: 0x06002200 RID: 8704 RVA: 0x00095531 File Offset: 0x00093731
		public MultiValuedProperty<Capability> PersistedCapabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)this[OrganizationSchema.PersistedCapabilities];
			}
			set
			{
				this[OrganizationSchema.PersistedCapabilities] = value;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x0009553F File Offset: 0x0009373F
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x00095554 File Offset: 0x00093754
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncStatus
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.DirSyncStatus];
			}
			set
			{
				if (value != null)
				{
					foreach (string text in value)
					{
						if (!string.IsNullOrEmpty(text) && text.StartsWith("Pending", StringComparison.OrdinalIgnoreCase))
						{
							this[OrganizationSchema.IsDirSyncStatusPending] = true;
							break;
						}
						this[OrganizationSchema.IsDirSyncStatusPending] = false;
					}
				}
				this[OrganizationSchema.DirSyncStatus] = value;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x000955E8 File Offset: 0x000937E8
		// (set) Token: 0x06002204 RID: 8708 RVA: 0x000955FA File Offset: 0x000937FA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AsynchronousOperationIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.AsynchronousOperationIds];
			}
			set
			{
				this[OrganizationSchema.AsynchronousOperationIds] = value;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x00095608 File Offset: 0x00093808
		// (set) Token: 0x06002206 RID: 8710 RVA: 0x0009561A File Offset: 0x0009381A
		[Parameter(Mandatory = false)]
		public string DirSyncServiceInstance
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.DirSyncServiceInstance];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.DirSyncServiceInstance] = value;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x00095628 File Offset: 0x00093828
		// (set) Token: 0x06002208 RID: 8712 RVA: 0x00095652 File Offset: 0x00093852
		public MailboxRelease MailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[OrganizationSchema.MailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				this[OrganizationSchema.MailboxRelease] = value.ToString();
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x0009566C File Offset: 0x0009386C
		// (set) Token: 0x0600220A RID: 8714 RVA: 0x00095696 File Offset: 0x00093896
		public MailboxRelease PreviousMailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[OrganizationSchema.PreviousMailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				this[OrganizationSchema.PreviousMailboxRelease] = value.ToString();
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000956B0 File Offset: 0x000938B0
		// (set) Token: 0x0600220C RID: 8716 RVA: 0x000956DA File Offset: 0x000938DA
		public MailboxRelease PilotMailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[OrganizationSchema.PilotMailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				this[OrganizationSchema.PilotMailboxRelease] = value.ToString();
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x000956F2 File Offset: 0x000938F2
		// (set) Token: 0x0600220E RID: 8718 RVA: 0x00095700 File Offset: 0x00093900
		internal ADObjectId OrganizationalUnitLink
		{
			get
			{
				return ExchangeConfigurationUnit.OrganizationalUnitLinkGetter(this.propertyBag);
			}
			set
			{
				this[ADObjectSchema.OrganizationalUnitRoot] = value;
				if (value == null)
				{
					this[ADObjectSchema.ConfigurationUnit] = null;
				}
				else
				{
					this[ADObjectSchema.ConfigurationUnit] = this[ADObjectSchema.Id];
				}
				MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)this[ExchangeConfigurationUnitSchema.OrganizationalUnitLink];
				if (multiValuedProperty.Count == 0)
				{
					multiValuedProperty.Add(value);
					return;
				}
				multiValuedProperty[0] = value;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x00095769 File Offset: 0x00093969
		internal new ADObjectId OrganizationalUnitRoot
		{
			get
			{
				return ((OrganizationId)this[ExchangeConfigurationUnitSchema.OrganizationId]).OrganizationalUnit;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x00095780 File Offset: 0x00093980
		internal new ADObjectId ConfigurationUnit
		{
			get
			{
				return ((OrganizationId)this[ExchangeConfigurationUnitSchema.OrganizationId]).ConfigurationUnit;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00095798 File Offset: 0x00093998
		internal bool IsDirSyncEnabled
		{
			get
			{
				List<DirSyncState> list = new List<DirSyncState>();
				list.Add(DirSyncState.Disabled);
				return ExchangeConfigurationUnit.IsOrganizationDirSyncRunning(this.IsDirSyncRunning, this.DirSyncStatus, list);
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x000957C4 File Offset: 0x000939C4
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x000957D6 File Offset: 0x000939D6
		internal bool IsTemplateTenant
		{
			get
			{
				return (bool)this[OrganizationSchema.IsTemplateTenant];
			}
			set
			{
				this[OrganizationSchema.IsTemplateTenant] = value;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x000957E9 File Offset: 0x000939E9
		internal TenantRelocationStatus RelocationStatus
		{
			get
			{
				return (TenantRelocationStatus)((byte)this[ExchangeConfigurationUnitSchema.RelocationStatus]);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x000957FB File Offset: 0x000939FB
		// (set) Token: 0x06002216 RID: 8726 RVA: 0x0009580D File Offset: 0x00093A0D
		internal Schedule SafeLockdownSchedule
		{
			get
			{
				return (Schedule)this[ExchangeConfigurationUnitSchema.SafeLockdownSchedule];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.SafeLockdownSchedule] = value;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x0009581B File Offset: 0x00093A1B
		// (set) Token: 0x06002218 RID: 8728 RVA: 0x0009582D File Offset: 0x00093A2D
		internal byte[] TenantRelocationCompletionTargetVector
		{
			get
			{
				return (byte[])this[ExchangeConfigurationUnitSchema.TenantRelocationCompletionTargetVector];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.TenantRelocationCompletionTargetVector] = value;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x0009583B File Offset: 0x00093A3B
		// (set) Token: 0x0600221A RID: 8730 RVA: 0x0009584D File Offset: 0x00093A4D
		internal string TargetForest
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.TargetForest];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.TargetForest] = value;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x0009585B File Offset: 0x00093A5B
		// (set) Token: 0x0600221C RID: 8732 RVA: 0x0009586D File Offset: 0x00093A6D
		internal string SourceForest
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.SourceForest];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.SourceForest] = value;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x0009587B File Offset: 0x00093A7B
		// (set) Token: 0x0600221E RID: 8734 RVA: 0x0009588D File Offset: 0x00093A8D
		internal string RelocationSourceForestRaw
		{
			get
			{
				return (string)this[ExchangeConfigurationUnitSchema.RelocationSourceForestRaw];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.RelocationSourceForestRaw] = value;
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0009589B File Offset: 0x00093A9B
		internal static bool RelocationInProgress(ExchangeConfigurationUnit cu)
		{
			return !string.IsNullOrEmpty(cu.TargetForest);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x000958AB File Offset: 0x00093AAB
		internal static bool IsBeingDeleted(OrganizationStatus status)
		{
			return status == OrganizationStatus.PendingRemoval || status == OrganizationStatus.SoftDeleted || status == OrganizationStatus.ReadyForRemoval;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000958BC File Offset: 0x00093ABC
		internal static bool IsOrganizationActive(OrganizationStatus status)
		{
			return status == OrganizationStatus.Active || status == OrganizationStatus.Suspended || status == OrganizationStatus.LockedOut;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000958D0 File Offset: 0x00093AD0
		internal static bool IsInactiveRelocationNode(ExchangeConfigurationUnit cu)
		{
			TenantRelocationState tenantRelocationState;
			bool flag;
			return (!string.IsNullOrEmpty(cu.TargetForest) && cu.RelocationStatus == TenantRelocationStatus.Retired) || (!string.IsNullOrEmpty(cu.RelocationSourceForestRaw) && cu.RelocationStatusDetailsRaw != RelocationStatusDetails.Active && TenantRelocationStateCache.TryGetTenantRelocationStateByObjectId(cu.Id, out tenantRelocationState, out flag) && tenantRelocationState.SourceForestState != TenantRelocationStatus.Retired);
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x00095933 File Offset: 0x00093B33
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x00095945 File Offset: 0x00093B45
		internal bool AutoCompletionEnabled
		{
			get
			{
				return (bool)this[ExchangeConfigurationUnitSchema.AutoCompletionEnabled];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.AutoCompletionEnabled] = value;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x00095958 File Offset: 0x00093B58
		// (set) Token: 0x06002226 RID: 8742 RVA: 0x0009596A File Offset: 0x00093B6A
		internal bool LargeTenantModeEnabled
		{
			get
			{
				return (bool)this[ExchangeConfigurationUnitSchema.LargeTenantModeEnabled];
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.LargeTenantModeEnabled] = value;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x0009597D File Offset: 0x00093B7D
		internal RelocationStatusDetailsSource RelocationStatusDetailsSource
		{
			get
			{
				return (RelocationStatusDetailsSource)((byte)this[ExchangeConfigurationUnitSchema.RelocationStatusDetailsSource]);
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x0009598F File Offset: 0x00093B8F
		// (set) Token: 0x06002229 RID: 8745 RVA: 0x000959A1 File Offset: 0x00093BA1
		internal RelocationStatusDetails RelocationStatusDetailsRaw
		{
			get
			{
				return (RelocationStatusDetails)((byte)this[ExchangeConfigurationUnitSchema.RelocationStatusDetailsRaw]);
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.RelocationStatusDetailsRaw] = value;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x000959B4 File Offset: 0x00093BB4
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x000959C6 File Offset: 0x00093BC6
		public RelocationStatusDetailsDestination RelocationStatusDetailsDestination
		{
			get
			{
				return (RelocationStatusDetailsDestination)((byte)this[ExchangeConfigurationUnitSchema.RelocationStatusDetailsDestination]);
			}
			internal set
			{
				this[ExchangeConfigurationUnitSchema.RelocationStatusDetailsDestination] = value;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x000959D9 File Offset: 0x00093BD9
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x000959EC File Offset: 0x00093BEC
		internal RelocationStateRequested RelocationStateRequested
		{
			get
			{
				return (RelocationStateRequested)((int)this[ExchangeConfigurationUnitSchema.RelocationStateRequested]);
			}
			set
			{
				this[ExchangeConfigurationUnitSchema.RelocationStateRequested] = (int)value;
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00095A00 File Offset: 0x00093C00
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			ADObjectId organizationalUnitLink = this.OrganizationalUnitLink;
			if (organizationalUnitLink != null && !string.IsNullOrEmpty(organizationalUnitLink.DistinguishedName) && !organizationalUnitLink.DistinguishedName.ToUpper().StartsWith("OU="))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorInvalidOrganizationId(base.Id.DistinguishedName, organizationalUnitLink.DistinguishedName, base.Id.DistinguishedName), this.Identity, string.Empty));
			}
			if (((base.SupportedSharedConfigurations != null && base.SupportedSharedConfigurations.Count != 0) || base.SharedConfigurationInfo != null) && (string.IsNullOrEmpty(this.OfferId) || string.IsNullOrEmpty(this.ProgramId)))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorOfferProgramIdMandatoryOnSharedConfig, this.Identity, string.Empty));
			}
			if (base.IsDehydrated || this.IsStaticConfigurationShared)
			{
				if (base.SupportedSharedConfigurations.Count == 0)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorIsDehydratedSetOnNonTinyTenant, this.Identity, string.Empty));
					return;
				}
			}
			else if (base.SupportedSharedConfigurations != null && base.SupportedSharedConfigurations.Count != 0)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorNonTinyTenantShouldNotHaveSharedConfig, this.Identity, string.Empty));
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00095B3C File Offset: 0x00093D3C
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(OrganizationSchema.DefaultPublicFolderProhibitPostQuota))
			{
				this.DefaultPublicFolderProhibitPostQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromGB(2UL));
			}
			if (!base.IsModified(OrganizationSchema.DefaultPublicFolderIssueWarningQuota))
			{
				this.DefaultPublicFolderIssueWarningQuota = this.DefaultPublicFolderProhibitPostQuota * 85 / 100;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00095BA0 File Offset: 0x00093DA0
		internal static object CuOrganizationIdGetter(IPropertyBag propertyBag)
		{
			OrganizationId organizationId = (OrganizationId)ADObject.OrganizationIdGetter(propertyBag);
			if (organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				ADObjectId adobjectId = ExchangeConfigurationUnit.OrganizationalUnitLinkGetter(propertyBag);
				if (adobjectId != null)
				{
					organizationId = new OrganizationId(adobjectId, (ADObjectId)propertyBag[ADObjectSchema.Id]);
				}
			}
			return organizationId;
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x00095BE8 File Offset: 0x00093DE8
		internal static SinglePropertyFilter OrganizationNameFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			string arg = (string)((ComparisonFilter)filter).PropertyValue;
			string propertyValue = string.Format("/o=First Organization/ou=Exchange Administrative Group (FYDIBOHF23SPDLT)/cn={0}/cn=Configuration", arg);
			return new ComparisonFilter(ComparisonOperator.Equal, OrganizationSchema.LegacyExchangeDN, propertyValue);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x00095C50 File Offset: 0x00093E50
		internal static string OrganizationNameGetter(IPropertyBag propertyBag)
		{
			string result = null;
			if (propertyBag[OrganizationSchema.LegacyExchangeDN] != null)
			{
				result = (string)propertyBag[OrganizationSchema.LegacyExchangeDN];
			}
			return result;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x00095C80 File Offset: 0x00093E80
		private static ADObjectId OrganizationalUnitLinkGetter(IPropertyBag propertyBag)
		{
			ADObjectId result = null;
			if (propertyBag[ExchangeConfigurationUnitSchema.OrganizationalUnitLink] != null)
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)propertyBag[ExchangeConfigurationUnitSchema.OrganizationalUnitLink];
				if (multiValuedProperty.Count > 0)
				{
					result = multiValuedProperty[0];
				}
			}
			return result;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x00095CC0 File Offset: 0x00093EC0
		internal static string ProgramIdGetter(IPropertyBag propertyBag)
		{
			string result = null;
			if (propertyBag[ExchangeConfigurationUnitSchema.ResellerId] != null)
			{
				string text = (string)propertyBag[ExchangeConfigurationUnitSchema.ResellerId];
				int num = text.IndexOf(".");
				if (num > 1)
				{
					result = text.Substring(0, num);
				}
			}
			return result;
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x00095D08 File Offset: 0x00093F08
		internal static QueryFilter ProgramIdFilterBuilder(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			return new TextFilter(ExchangeConfigurationUnitSchema.ResellerId, (((string)comparisonFilter.PropertyValue) ?? string.Empty) + ".", MatchOptions.Prefix, MatchFlags.IgnoreCase);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x00095D70 File Offset: 0x00093F70
		internal static QueryFilter DirSyncServiceInstanceFilterBuilder(SinglePropertyFilter filter)
		{
			if (filter is ComparisonFilter)
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				object propertyValue = comparisonFilter.PropertyValue;
				return new ComparisonFilter(comparisonFilter.ComparisonOperator, ExchangeConfigurationUnitSchema.DirSyncServiceInstanceRaw, (propertyValue != null) ? propertyValue.ToString().ToLower() : null);
			}
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(ExchangeConfigurationUnitSchema.DirSyncServiceInstanceRaw);
			}
			if (filter is TextFilter)
			{
				TextFilter textFilter = filter as TextFilter;
				string text = textFilter.Text;
				return new TextFilter(ExchangeConfigurationUnitSchema.DirSyncServiceInstanceRaw, (text != null) ? text.ToLower() : null, textFilter.MatchOptions, textFilter.MatchFlags);
			}
			throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x00095E28 File Offset: 0x00094028
		internal static string OfferIdGetter(IPropertyBag propertyBag)
		{
			string result = null;
			if (propertyBag[ExchangeConfigurationUnitSchema.ResellerId] != null)
			{
				string text = (string)propertyBag[ExchangeConfigurationUnitSchema.ResellerId];
				int num = text.IndexOf(".");
				if (num > 1 && text.Length > num + 1)
				{
					result = text.Substring(num + 1);
				}
			}
			return result;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x00095E7B File Offset: 0x0009407B
		internal static void ProgramIdSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ExchangeConfigurationUnitSchema.ResellerId] = string.Format("{0}.{1}", (string)value, (string)propertyBag[ExchangeConfigurationUnitSchema.OfferId]);
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x00095EA8 File Offset: 0x000940A8
		internal static void OfferIdSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ExchangeConfigurationUnitSchema.ResellerId] = string.Format("{0}.{1}", (string)propertyBag[ExchangeConfigurationUnitSchema.ProgramId], (string)value);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00095ED8 File Offset: 0x000940D8
		internal static QueryFilter ExternalDirectoryOrganizationIdFilterBuilder(SinglePropertyFilter filter)
		{
			if (filter is ComparisonFilter)
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				object propertyValue = comparisonFilter.PropertyValue;
				return new ComparisonFilter(comparisonFilter.ComparisonOperator, ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationIdRaw, (propertyValue != null) ? propertyValue.ToString() : null);
			}
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationIdRaw);
			}
			if (filter is TextFilter)
			{
				TextFilter textFilter = filter as TextFilter;
				string text = textFilter.Text;
				return new TextFilter(ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationIdRaw, (text != null) ? text.ToLower() : null, textFilter.MatchOptions, textFilter.MatchFlags);
			}
			throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId.Name, filter.GetType(), typeof(ComparisonFilter)));
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00095F88 File Offset: 0x00094188
		internal static string ExternalDirectoryOrganizationIdGetter(IPropertyBag propertyBag)
		{
			if ((bool)propertyBag[OrganizationSchema.IsTemplateTenant])
			{
				return TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationId;
			}
			return (string)propertyBag[ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationIdRaw];
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00095FB2 File Offset: 0x000941B2
		internal static void ExternalDirectoryOrganizationIdSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationIdRaw] = value;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x00095FC0 File Offset: 0x000941C0
		internal static bool IsOrganizationDirSyncRunning(bool isDirSyncRunning, MultiValuedProperty<string> dirSyncStatus, List<DirSyncState> dirSyncStoppedStates)
		{
			ExTraceGlobals.ADObjectTracer.TraceDebug(0L, "<ExchangeConfigurationUnit::IsOrganizationDirSyncRunning> enter");
			bool flag = false;
			if (dirSyncStatus != null && dirSyncStatus.Count > 0)
			{
				foreach (string text in dirSyncStatus)
				{
					ExTraceGlobals.ADObjectTracer.TraceDebug<string>(0L, "Check dirSyncStatusString \"{0}\"", text);
					DirSyncStatusValue dirSyncStatusValue = SyncValueConvertor.ConvertStringToDirSyncStatus(text);
					if (dirSyncStatusValue != null)
					{
						if (!dirSyncStoppedStates.Contains(dirSyncStatusValue.State))
						{
							ExTraceGlobals.ADObjectTracer.TraceDebug<string>(0L, "Organization is either dirsync enabled or in pending state. (dirSyncStatusValue.State = {0})", dirSyncStatusValue.State.ToString());
							flag = true;
							break;
						}
						ExTraceGlobals.ADObjectTracer.TraceDebug(0L, "DirSyncStatus.State is DirSyncState.Disabled");
					}
					else
					{
						ExTraceGlobals.ADObjectTracer.TraceWarning<string>(0L, "Ignore invalid DirSyncStatus value \"{0}\"", text);
					}
				}
			}
			if (!flag)
			{
				ExTraceGlobals.ADObjectTracer.TraceDebug<string>(0L, "Set return value to be isDirSyncRunning value ({0})", isDirSyncRunning.ToString());
				flag = isDirSyncRunning;
			}
			ExTraceGlobals.ADObjectTracer.TraceDebug<string>(0L, "<ExchangeConfigurationUnit::IsOrganizationDirSyncRunning> return ({0})", flag.ToString());
			return flag;
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x000960DC File Offset: 0x000942DC
		// (set) Token: 0x0600223F RID: 8767 RVA: 0x000960EE File Offset: 0x000942EE
		public ADObjectId ManagementSiteLink
		{
			get
			{
				return (ADObjectId)this[ExchangeConfigurationUnitSchema.ManagementSiteLink];
			}
			internal set
			{
				this[ExchangeConfigurationUnitSchema.ManagementSiteLink] = value;
			}
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000960FC File Offset: 0x000942FC
		internal void TestOnlySetWhenOrganizationStatusSet(DateTime value)
		{
			this.WhenOrganizationStatusSet = new DateTime?(value);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0009610C File Offset: 0x0009430C
		internal bool HasSharedConfigurationBL()
		{
			if (base.SharedConfigurationInfo == null)
			{
				return false;
			}
			ADRawEntry[] array = this.m_Session.Find(base.Id, QueryScope.Base, new ExistsFilter(OrganizationSchema.SupportedSharedConfigurationsBL), null, 1, new PropertyDefinition[]
			{
				ADObjectSchema.Id
			});
			return array != null && array.Length == 1;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00096164 File Offset: 0x00094364
		internal bool TryGetIdByWellKnownGuid(Guid wkGuid, out ADObjectId id)
		{
			if (wkGuid.Equals(Guid.Empty))
			{
				throw new ArgumentException("wkGuid");
			}
			if (this.wellKnownGuidToDn == null)
			{
				this.InitWellKnownObjectStructures();
			}
			id = null;
			string distinguishedName = null;
			if (this.wellKnownGuidToDn.TryGetValue(wkGuid, out distinguishedName))
			{
				id = new ADObjectId(distinguishedName);
			}
			return id != null;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000961C0 File Offset: 0x000943C0
		internal bool TryGetWellKnownGuidById(ADObjectId id, out Guid wkGuid)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (string.IsNullOrEmpty(id.DistinguishedName))
			{
				throw new ArgumentException("id");
			}
			if (this.dnToWellKnownGuid == null)
			{
				this.InitWellKnownObjectStructures();
			}
			wkGuid = Guid.Empty;
			return this.dnToWellKnownGuid.TryGetValue(id.DistinguishedName, out wkGuid);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00096220 File Offset: 0x00094420
		private void InitWellKnownObjectStructures()
		{
			lock (this.wellKnownObjectsLock)
			{
				if (this.dnToWellKnownGuid == null)
				{
					this.dnToWellKnownGuid = new Dictionary<string, Guid>(this.OtherWellKnownObjects.Count);
					this.wellKnownGuidToDn = new Dictionary<Guid, string>(this.OtherWellKnownObjects.Count);
					foreach (DNWithBinary dnwithBinary in this.OtherWellKnownObjects)
					{
						byte[] binary = dnwithBinary.Binary;
						if (binary != null && binary.Length == 16)
						{
							Guid guid = new Guid(binary);
							string distinguishedName = dnwithBinary.DistinguishedName;
							this.dnToWellKnownGuid.Add(distinguishedName, guid);
							this.wellKnownGuidToDn.Add(guid, distinguishedName);
						}
					}
				}
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0009630C File Offset: 0x0009450C
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			if (this.OrganizationId == null || this.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				return false;
			}
			bool flag = false;
			if (base.ObjectState == ObjectState.Deleted)
			{
				flag = true;
				keys = new Guid[]
				{
					CannedProvisioningCacheKeys.OrganizationCUContainer,
					CannedProvisioningCacheKeys.SharedConfigurationStateCacheKey,
					CannedProvisioningCacheKeys.IsDehydratedConfigurationCacheKey
				};
			}
			else if (base.ObjectState == ObjectState.Changed)
			{
				if (base.IsChanged(OrganizationSchema.SMTPAddressCheckWithAcceptedDomain) || base.IsChanged(OrganizationSchema.SkipToUAndParentalControlCheck) || base.IsChanged(OrganizationSchema.IsHotmailMigration) || base.IsChanged(OrganizationSchema.AllowDeleteOfExternalIdentityUponRemove) || base.IsChanged(OrganizationSchema.UseServicePlanAsCounterInstanceName) || base.IsChanged(ExchangeConfigurationUnitSchema.ServicePlan) || base.IsChanged(OrganizationSchema.SoftDeletedFeatureStatus))
				{
					flag = true;
					if (this.IsHydratedFlagChanged)
					{
						keys = new Guid[]
						{
							CannedProvisioningCacheKeys.OrganizationCUContainer,
							CannedProvisioningCacheKeys.SharedConfigurationStateCacheKey,
							CannedProvisioningCacheKeys.IsDehydratedConfigurationCacheKey
						};
					}
					else
					{
						keys = new Guid[]
						{
							CannedProvisioningCacheKeys.OrganizationCUContainer
						};
					}
				}
				else if (this.IsHydratedFlagChanged)
				{
					flag = true;
					keys = new Guid[]
					{
						CannedProvisioningCacheKeys.SharedConfigurationStateCacheKey,
						CannedProvisioningCacheKeys.IsDehydratedConfigurationCacheKey
					};
				}
			}
			else if (base.ObjectState == ObjectState.New)
			{
				keys = new Guid[]
				{
					CannedProvisioningCacheKeys.SharedConfigurationStateCacheKey,
					CannedProvisioningCacheKeys.IsDehydratedConfigurationCacheKey
				};
			}
			if (flag)
			{
				orgId = this.OrganizationId;
			}
			return flag;
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x000964E0 File Offset: 0x000946E0
		private bool IsHydratedFlagChanged
		{
			get
			{
				return base.IsChanged(OrganizationSchema.SupportedSharedConfigurations) || base.IsChanged(OrganizationSchema.SharedConfigurationInfo) || base.IsChanged(OrganizationSchema.IsDehydrated) || base.IsChanged(OrganizationSchema.IsStaticConfigurationShared) || base.IsChanged(OrganizationSchema.EnableAsSharedConfiguration) || base.IsChanged(OrganizationSchema.ImmutableConfiguration);
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0009653B File Offset: 0x0009473B
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x04001543 RID: 5443
		public const MailboxRelease CurrentMailboxRelease = MailboxRelease.E15;

		// Token: 0x04001544 RID: 5444
		internal new static readonly string MostDerivedClass = "msExchConfigurationUnitContainer";

		// Token: 0x04001545 RID: 5445
		private static readonly ExchangeConfigurationUnitSchema schema = ObjectSchema.GetInstance<ExchangeConfigurationUnitSchema>();

		// Token: 0x04001546 RID: 5446
		private Dictionary<string, Guid> dnToWellKnownGuid;

		// Token: 0x04001547 RID: 5447
		private Dictionary<Guid, string> wellKnownGuidToDn;

		// Token: 0x04001548 RID: 5448
		private object wellKnownObjectsLock = new object();
	}
}
