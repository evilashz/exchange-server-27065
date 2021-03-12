using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000253 RID: 595
	[Cmdlet("Set", "Organization", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetOrganization : SetSystemConfigurationObjectTask<OrganizationIdParameter, ExchangeConfigurationUnit>
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0005C690 File Offset: 0x0005A890
		private List<ExchangeConfigurationUnit> SharedConfigurationUnits
		{
			get
			{
				if (this.sharedConfigurationUnits == null)
				{
					this.sharedConfigurationUnits = new List<ExchangeConfigurationUnit>();
					foreach (OrganizationIdParameter organization in this.SharedConfiguration)
					{
						ExchangeConfigurationUnit exchangeConfigUnitFromOrganizationId = OrganizationTaskHelper.GetExchangeConfigUnitFromOrganizationId(organization, DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromAllTenantsPartitionId(base.CurrentOrganizationId.PartitionId), 57, "SharedConfigurationUnits", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\SetOrganizationTask.cs"), new Task.TaskErrorLoggingDelegate(base.WriteError), true);
						this.sharedConfigurationUnits.Add(exchangeConfigUnitFromOrganizationId);
					}
				}
				return this.sharedConfigurationUnits;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0005C740 File Offset: 0x0005A940
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOrganization(this.Identity.ToString());
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0005C752 File Offset: 0x0005A952
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x0005C75A File Offset: 0x0005A95A
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "SharedConfiguration", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveRelocationConstraint", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "SharedConfigurationInfo", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "SharedConfigurationRemove", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "AddRelocationConstraint", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override OrganizationIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0005C763 File Offset: 0x0005A963
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x0005C77A File Offset: 0x0005A97A
		[Parameter(Mandatory = true, ParameterSetName = "SharedConfiguration")]
		public MultiValuedProperty<OrganizationIdParameter> SharedConfiguration
		{
			get
			{
				return (MultiValuedProperty<OrganizationIdParameter>)base.Fields[OrganizationSchema.SupportedSharedConfigurations];
			}
			set
			{
				base.Fields[OrganizationSchema.SupportedSharedConfigurations] = value;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0005C78D File Offset: 0x0005A98D
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x0005C795 File Offset: 0x0005A995
		[Parameter(Mandatory = false, ParameterSetName = "SharedConfiguration")]
		public SwitchParameter ClearPreviousSharedConfigurations { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0005C79E File Offset: 0x0005A99E
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x0005C7A6 File Offset: 0x0005A9A6
		[Parameter(Mandatory = false, ParameterSetName = "SharedConfigurationRemove")]
		public SwitchParameter RemoveSharedConfigurations { get; set; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x0005C7AF File Offset: 0x0005A9AF
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x0005C7D0 File Offset: 0x0005A9D0
		[Parameter(Mandatory = true, ParameterSetName = "SharedConfigurationInfo")]
		public bool EnableAsSharedConfiguration
		{
			get
			{
				return (bool)(base.Fields[OrganizationSchema.EnableAsSharedConfiguration] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.EnableAsSharedConfiguration] = value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0005C7E8 File Offset: 0x0005A9E8
		// (set) Token: 0x06001615 RID: 5653 RVA: 0x0005C809 File Offset: 0x0005AA09
		[Parameter(Mandatory = false)]
		public bool ImmutableConfiguration
		{
			get
			{
				return (bool)(base.Fields[OrganizationSchema.ImmutableConfiguration] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.ImmutableConfiguration] = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0005C821 File Offset: 0x0005AA21
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x0005C842 File Offset: 0x0005AA42
		[Parameter(Mandatory = false)]
		public bool IsDehydrated
		{
			get
			{
				return (bool)(base.Fields[OrganizationSchema.IsDehydrated] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsDehydrated] = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0005C85A File Offset: 0x0005AA5A
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x0005C87B File Offset: 0x0005AA7B
		[Parameter(Mandatory = false)]
		public bool IsStaticConfigurationShared
		{
			get
			{
				return (bool)(base.Fields[OrganizationSchema.IsStaticConfigurationShared] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsStaticConfigurationShared] = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0005C893 File Offset: 0x0005AA93
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x0005C8B4 File Offset: 0x0005AAB4
		[Parameter(Mandatory = false)]
		public bool IsUpdatingServicePlan
		{
			get
			{
				return (bool)(base.Fields[OrganizationSchema.IsUpdatingServicePlan] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsUpdatingServicePlan] = value;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0005C8CC File Offset: 0x0005AACC
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x0005C8D4 File Offset: 0x0005AAD4
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0005C8DD File Offset: 0x0005AADD
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x0005C8F4 File Offset: 0x0005AAF4
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Capability> PersistedCapabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)base.Fields["TenantSKUCapability"];
			}
			set
			{
				if (value != null)
				{
					base.VerifyValues<Capability>(CapabilityHelper.AllowedSKUCapabilities, value.ToArray());
				}
				base.Fields["TenantSKUCapability"] = value;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0005C91B File Offset: 0x0005AB1B
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x0005C932 File Offset: 0x0005AB32
		[Parameter(Mandatory = false)]
		public ExchangeUpgradeBucketIdParameter ExchangeUpgradeBucket
		{
			get
			{
				return (ExchangeUpgradeBucketIdParameter)base.Fields[ExchangeConfigurationUnitSchema.ExchangeUpgradeBucket];
			}
			set
			{
				base.Fields[ExchangeConfigurationUnitSchema.ExchangeUpgradeBucket] = value;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0005C945 File Offset: 0x0005AB45
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x0005C96B File Offset: 0x0005AB6B
		[Parameter(Mandatory = false)]
		public SwitchParameter ExcludedFromBackSync
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.ExcludedFromBackSync] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.ExcludedFromBackSync] = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0005C983 File Offset: 0x0005AB83
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x0005C9A9 File Offset: 0x0005ABA9
		[Parameter(Mandatory = false)]
		public SwitchParameter ExcludedFromForwardSyncEDU2BPOS
		{
			get
			{
				return (SwitchParameter)(base.Fields[OrganizationSchema.ExcludedFromForwardSyncEDU2BPOS] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.ExcludedFromForwardSyncEDU2BPOS] = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0005C9C1 File Offset: 0x0005ABC1
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x0005C9D8 File Offset: 0x0005ABD8
		[Parameter(Mandatory = false)]
		public int DefaultMovePriority
		{
			get
			{
				return (int)base.Fields[OrganizationSchema.DefaultMovePriority];
			}
			set
			{
				base.Fields[OrganizationSchema.DefaultMovePriority] = value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0005C9F0 File Offset: 0x0005ABF0
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x0005CA07 File Offset: 0x0005AC07
		[Parameter(Mandatory = false)]
		public string UpgradeMessage
		{
			get
			{
				return (string)base.Fields[OrganizationSchema.UpgradeMessage];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeMessage] = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x0005CA1A File Offset: 0x0005AC1A
		// (set) Token: 0x0600162B RID: 5675 RVA: 0x0005CA31 File Offset: 0x0005AC31
		[Parameter(Mandatory = false)]
		public string UpgradeDetails
		{
			get
			{
				return (string)base.Fields[OrganizationSchema.UpgradeDetails];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeDetails] = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x0005CA44 File Offset: 0x0005AC44
		// (set) Token: 0x0600162D RID: 5677 RVA: 0x0005CA5B File Offset: 0x0005AC5B
		[Parameter(Mandatory = false)]
		public UpgradeConstraintArray UpgradeConstraints
		{
			get
			{
				return (UpgradeConstraintArray)base.Fields[OrganizationSchema.UpgradeConstraints];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeConstraints] = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0005CA6E File Offset: 0x0005AC6E
		// (set) Token: 0x0600162F RID: 5679 RVA: 0x0005CA85 File Offset: 0x0005AC85
		[Parameter(Mandatory = false)]
		public UpgradeStage? UpgradeStage
		{
			get
			{
				return (UpgradeStage?)base.Fields[OrganizationSchema.UpgradeStage];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeStage] = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0005CA9D File Offset: 0x0005AC9D
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x0005CAB4 File Offset: 0x0005ACB4
		[Parameter(Mandatory = false)]
		public DateTime? UpgradeStageTimeStamp
		{
			get
			{
				return (DateTime?)base.Fields[OrganizationSchema.UpgradeStageTimeStamp];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeStageTimeStamp] = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0005CACC File Offset: 0x0005ACCC
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x0005CAE3 File Offset: 0x0005ACE3
		[Parameter(Mandatory = false)]
		public int? UpgradeE14MbxCountForCurrentStage
		{
			get
			{
				return (int?)base.Fields[OrganizationSchema.UpgradeE14MbxCountForCurrentStage];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeE14MbxCountForCurrentStage] = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0005CAFB File Offset: 0x0005ACFB
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x0005CB12 File Offset: 0x0005AD12
		[Parameter(Mandatory = false)]
		public int? UpgradeE14RequestCountForCurrentStage
		{
			get
			{
				return (int?)base.Fields[OrganizationSchema.UpgradeE14RequestCountForCurrentStage];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeE14RequestCountForCurrentStage] = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0005CB2A File Offset: 0x0005AD2A
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x0005CB41 File Offset: 0x0005AD41
		[Parameter(Mandatory = false)]
		public DateTime? UpgradeLastE14CountsUpdateTime
		{
			get
			{
				return (DateTime?)base.Fields[OrganizationSchema.UpgradeLastE14CountsUpdateTime];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeLastE14CountsUpdateTime] = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0005CB59 File Offset: 0x0005AD59
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x0005CB70 File Offset: 0x0005AD70
		[Parameter(Mandatory = false)]
		public bool? UpgradeConstraintsDisabled
		{
			get
			{
				return (bool?)base.Fields[OrganizationSchema.UpgradeConstraintsDisabled];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeConstraintsDisabled] = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0005CB88 File Offset: 0x0005AD88
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x0005CB9F File Offset: 0x0005AD9F
		[Parameter(Mandatory = false)]
		public int? UpgradeUnitsOverride
		{
			get
			{
				return (int?)base.Fields[OrganizationSchema.UpgradeUnitsOverride];
			}
			set
			{
				base.Fields[OrganizationSchema.UpgradeUnitsOverride] = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0005CBB7 File Offset: 0x0005ADB7
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0005CBCE File Offset: 0x0005ADCE
		[Parameter(Mandatory = false)]
		public int MaxOfflineAddressBooks
		{
			get
			{
				return (int)base.Fields[OrganizationSchema.MaxOfflineAddressBooks];
			}
			set
			{
				base.Fields[OrganizationSchema.MaxOfflineAddressBooks] = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0005CBE6 File Offset: 0x0005ADE6
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x0005CBFD File Offset: 0x0005ADFD
		[Parameter(Mandatory = false)]
		public int MaxAddressBookPolicies
		{
			get
			{
				return (int)base.Fields[OrganizationSchema.MaxAddressBookPolicies];
			}
			set
			{
				base.Fields[OrganizationSchema.MaxAddressBookPolicies] = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0005CC18 File Offset: 0x0005AE18
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x0005CC47 File Offset: 0x0005AE47
		[Parameter(Mandatory = false)]
		public MailboxRelease MailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)base.Fields[OrganizationSchema.MailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				base.Fields[OrganizationSchema.MailboxRelease] = value.ToString();
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0005CC64 File Offset: 0x0005AE64
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x0005CC93 File Offset: 0x0005AE93
		[Parameter(Mandatory = false)]
		public MailboxRelease PreviousMailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)base.Fields[OrganizationSchema.PreviousMailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				base.Fields[OrganizationSchema.PreviousMailboxRelease] = value.ToString();
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0005CCB0 File Offset: 0x0005AEB0
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x0005CCDF File Offset: 0x0005AEDF
		[Parameter(Mandatory = false)]
		public MailboxRelease PilotMailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)base.Fields[OrganizationSchema.PilotMailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				base.Fields[OrganizationSchema.PilotMailboxRelease] = value.ToString();
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0005CCFC File Offset: 0x0005AEFC
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x0005CD1D File Offset: 0x0005AF1D
		[Parameter(Mandatory = true, ParameterSetName = "AddRelocationConstraint")]
		public SwitchParameter AddRelocationConstraint
		{
			get
			{
				return (SwitchParameter)(base.Fields["AddRelocationConstraint"] ?? false);
			}
			set
			{
				base.Fields["AddRelocationConstraint"] = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0005CD35 File Offset: 0x0005AF35
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x0005CD56 File Offset: 0x0005AF56
		[Parameter(Mandatory = true, ParameterSetName = "RemoveRelocationConstraint")]
		public SwitchParameter RemoveRelocationConstraint
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveRelocationConstraint"] ?? false);
			}
			set
			{
				base.Fields["RemoveRelocationConstraint"] = value;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0005CD6E File Offset: 0x0005AF6E
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x0005CD85 File Offset: 0x0005AF85
		[Parameter(Mandatory = true, ParameterSetName = "RemoveRelocationConstraint")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "AddRelocationConstraint")]
		public PersistableRelocationConstraintType RelocationConstraintType
		{
			get
			{
				return (PersistableRelocationConstraintType)base.Fields["RelocationConstraintType"];
			}
			set
			{
				base.Fields["RelocationConstraintType"] = value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0005CD9D File Offset: 0x0005AF9D
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x0005CDB4 File Offset: 0x0005AFB4
		[Parameter(Mandatory = true, ParameterSetName = "AddRelocationConstraint")]
		[ValidateNotNullOrEmpty]
		public int RelocationConstraintExpirationInDays
		{
			get
			{
				return (int)base.Fields["RelocationConstraintExpirationInDays"];
			}
			set
			{
				base.Fields["RelocationConstraintExpirationInDays"] = value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0005CDCC File Offset: 0x0005AFCC
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x0005CDED File Offset: 0x0005AFED
		[Parameter(Mandatory = false)]
		public bool IsLicensingEnforced
		{
			get
			{
				return (bool)(base.Fields[OrganizationSchema.IsLicensingEnforced] ?? false);
			}
			set
			{
				base.Fields[OrganizationSchema.IsLicensingEnforced] = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0005CE05 File Offset: 0x0005B005
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x0005CE1C File Offset: 0x0005B01C
		[Parameter(Mandatory = false)]
		public string ProgramId
		{
			get
			{
				return (string)base.Fields[ExchangeConfigurationUnitSchema.ProgramId];
			}
			set
			{
				base.Fields[ExchangeConfigurationUnitSchema.ProgramId] = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0005CE2F File Offset: 0x0005B02F
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x0005CE46 File Offset: 0x0005B046
		[Parameter(Mandatory = false)]
		public string OfferId
		{
			get
			{
				return (string)base.Fields[ExchangeConfigurationUnitSchema.OfferId];
			}
			set
			{
				base.Fields[ExchangeConfigurationUnitSchema.OfferId] = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0005CE59 File Offset: 0x0005B059
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x0005CE70 File Offset: 0x0005B070
		[Parameter(Mandatory = false)]
		public string ServicePlan
		{
			get
			{
				return (string)base.Fields[ExchangeConfigurationUnitSchema.ServicePlan];
			}
			set
			{
				base.Fields[ExchangeConfigurationUnitSchema.ServicePlan] = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0005CE83 File Offset: 0x0005B083
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x0005CE9A File Offset: 0x0005B09A
		[Parameter(Mandatory = false)]
		public string TargetServicePlan
		{
			get
			{
				return (string)base.Fields[ExchangeConfigurationUnitSchema.TargetServicePlan];
			}
			set
			{
				base.Fields[ExchangeConfigurationUnitSchema.TargetServicePlan] = value;
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0005CEB0 File Offset: 0x0005B0B0
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			if (this.Identity == null)
			{
				OrganizationIdParameter identity = new OrganizationIdParameter(base.CurrentOrgContainerId);
				this.Identity = identity;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0005CEE8 File Offset: 0x0005B0E8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.DataObject.IsModified(ExchangeConfigurationUnitSchema.DirSyncServiceInstance) && !string.IsNullOrEmpty(this.DataObject.DirSyncServiceInstance) && !ServiceInstanceId.IsValidServiceInstanceId(this.DataObject.DirSyncServiceInstance))
			{
				base.WriteError(new InvalidServiceInstanceIdException(this.DataObject.DirSyncServiceInstance), ExchangeErrorCategory.Client, null);
			}
			if (this.DataObject.IsChanged(OrganizationSchema.SupportedSharedConfigurations) && !this.RemoveSharedConfigurations)
			{
				foreach (ExchangeConfigurationUnit exchangeConfigurationUnit in this.SharedConfigurationUnits)
				{
					if (exchangeConfigurationUnit.SharedConfigurationInfo == null)
					{
						base.WriteError(new SharedConfigurationValidationException(Strings.SharedConfigurationInfoNotPresent(exchangeConfigurationUnit.Identity.ToString())), ExchangeErrorCategory.Client, null);
					}
					ServicePlanConfiguration instance = ServicePlanConfiguration.GetInstance();
					if (!exchangeConfigurationUnit.ProgramId.Equals(this.DataObject.ProgramId, StringComparison.OrdinalIgnoreCase) || (!this.IsHydratedOfferIdMatched(this.DataObject.ProgramId, this.DataObject.OfferId, exchangeConfigurationUnit, instance) && !this.IsPilotOfferIdMatched(this.DataObject.ProgramId, this.DataObject.OfferId, exchangeConfigurationUnit, instance) && !this.IsHydratedPilotOfferIdMatched(this.DataObject.ProgramId, this.DataObject.OfferId, exchangeConfigurationUnit, instance)))
					{
						base.WriteError(new SharedConfigurationValidationException(Strings.OfferIdMatchError(this.Identity.ToString(), this.DataObject.ProgramId, this.DataObject.OfferId, exchangeConfigurationUnit.Identity.ToString(), exchangeConfigurationUnit.ProgramId, exchangeConfigurationUnit.OfferId)), ExchangeErrorCategory.Client, null);
					}
					if (!exchangeConfigurationUnit.EnableAsSharedConfiguration)
					{
						base.WriteError(new SharedConfigurationValidationException(Strings.SharedConfigurationNotEnabled(this.Identity.ToString(), exchangeConfigurationUnit.Identity.ToString())), ExchangeErrorCategory.Client, null);
					}
				}
				if (this.DataObject.SharedConfigurationInfo != null)
				{
					base.WriteError(new SharedConfigurationValidationException(Strings.SharedConfigurationInfoExists(this.Identity.ToString(), this.DataObject.SharedConfigurationInfo.ToString())), ExchangeErrorCategory.Client, null);
				}
			}
			if (this.DataObject.IsChanged(OrganizationSchema.EnableAsSharedConfiguration) && !this.DataObject.EnableAsSharedConfiguration)
			{
				if (this.DataObject.SharedConfigurationInfo == null)
				{
					base.WriteError(new SharedConfigurationValidationException(Strings.SharedConfigurationInfoNotPresent(this.DataObject.Identity.ToString())), ExchangeErrorCategory.Client, null);
				}
				ExchangeConfigurationUnit[] array = OrganizationTaskHelper.FindSharedConfigurations(this.DataObject.SharedConfigurationInfo, this.DataObject.OrganizationId.PartitionId);
				if (array == null || array.Length < 2)
				{
					this.confirmLastSharedConfiguration = true;
				}
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0005D1C4 File Offset: 0x0005B3C4
		protected override IConfigurable ResolveDataObject()
		{
			ADObject adobject = (ADObject)base.ResolveDataObject();
			base.CurrentOrganizationId = adobject.OrganizationId;
			return adobject;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0005D1EC File Offset: 0x0005B3EC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ExchangeConfigurationUnit exchangeConfigurationUnit = (ExchangeConfigurationUnit)base.PrepareDataObject();
			if (base.Fields.IsModified(OrganizationSchema.DefaultMovePriority))
			{
				exchangeConfigurationUnit.DefaultMovePriority = this.DefaultMovePriority;
			}
			if (base.Fields.IsModified("TenantSKUCapability"))
			{
				CapabilityHelper.SetTenantSKUCapabilities(this.PersistedCapabilities, exchangeConfigurationUnit.PersistedCapabilities);
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeMessage))
			{
				exchangeConfigurationUnit.UpgradeMessage = this.UpgradeMessage;
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeDetails))
			{
				exchangeConfigurationUnit.UpgradeDetails = this.UpgradeDetails;
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeConstraints))
			{
				exchangeConfigurationUnit.UpgradeConstraints = this.UpgradeConstraints;
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeStage))
			{
				exchangeConfigurationUnit.UpgradeStage = this.UpgradeStage;
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeStageTimeStamp))
			{
				exchangeConfigurationUnit.UpgradeStageTimeStamp = this.UpgradeStageTimeStamp;
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeE14RequestCountForCurrentStage))
			{
				exchangeConfigurationUnit.UpgradeE14RequestCountForCurrentStage = this.UpgradeE14RequestCountForCurrentStage;
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeE14MbxCountForCurrentStage))
			{
				exchangeConfigurationUnit.UpgradeE14MbxCountForCurrentStage = this.UpgradeE14MbxCountForCurrentStage;
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeLastE14CountsUpdateTime))
			{
				exchangeConfigurationUnit.UpgradeLastE14CountsUpdateTime = this.UpgradeLastE14CountsUpdateTime;
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeConstraintsDisabled))
			{
				exchangeConfigurationUnit.UpgradeConstraintsDisabled = this.UpgradeConstraintsDisabled;
			}
			if (base.Fields.IsModified(OrganizationSchema.UpgradeUnitsOverride))
			{
				exchangeConfigurationUnit.UpgradeUnitsOverride = this.UpgradeUnitsOverride;
			}
			if (base.Fields.IsModified(OrganizationSchema.MaxAddressBookPolicies))
			{
				exchangeConfigurationUnit.MaxAddressBookPolicies = new int?(this.MaxAddressBookPolicies);
			}
			if (base.Fields.IsModified(OrganizationSchema.MaxOfflineAddressBooks))
			{
				exchangeConfigurationUnit.MaxOfflineAddressBooks = new int?(this.MaxOfflineAddressBooks);
			}
			if (this.RemoveSharedConfigurations)
			{
				exchangeConfigurationUnit.SupportedSharedConfigurations.Clear();
			}
			if (base.Fields.IsModified(OrganizationSchema.SupportedSharedConfigurations))
			{
				if (this.ClearPreviousSharedConfigurations)
				{
					exchangeConfigurationUnit.SupportedSharedConfigurations.Clear();
				}
				foreach (ExchangeConfigurationUnit exchangeConfigurationUnit2 in this.SharedConfigurationUnits)
				{
					if (!exchangeConfigurationUnit.SupportedSharedConfigurations.Contains(exchangeConfigurationUnit2.OrganizationId.ConfigurationUnit))
					{
						exchangeConfigurationUnit.SupportedSharedConfigurations.Add(exchangeConfigurationUnit2.Identity);
					}
				}
			}
			if (base.Fields.IsModified(OrganizationSchema.EnableAsSharedConfiguration))
			{
				exchangeConfigurationUnit.EnableAsSharedConfiguration = this.EnableAsSharedConfiguration;
			}
			if (base.Fields.IsModified(OrganizationSchema.ImmutableConfiguration))
			{
				exchangeConfigurationUnit.ImmutableConfiguration = this.ImmutableConfiguration;
			}
			if (base.Fields.IsModified(OrganizationSchema.IsDehydrated))
			{
				exchangeConfigurationUnit.IsDehydrated = this.IsDehydrated;
			}
			if (base.Fields.IsModified(OrganizationSchema.IsStaticConfigurationShared))
			{
				exchangeConfigurationUnit.IsStaticConfigurationShared = this.IsStaticConfigurationShared;
			}
			if (base.Fields.IsModified(OrganizationSchema.IsUpdatingServicePlan))
			{
				exchangeConfigurationUnit.IsUpdatingServicePlan = this.IsUpdatingServicePlan;
			}
			if (base.Fields.IsModified(ExchangeConfigurationUnitSchema.ProgramId))
			{
				exchangeConfigurationUnit.ProgramId = this.ProgramId;
			}
			if (base.Fields.IsModified(ExchangeConfigurationUnitSchema.OfferId))
			{
				exchangeConfigurationUnit.OfferId = this.OfferId;
			}
			if (base.Fields.IsModified(ExchangeConfigurationUnitSchema.ServicePlan))
			{
				exchangeConfigurationUnit.ServicePlan = this.ServicePlan;
			}
			if (base.Fields.IsModified(ExchangeConfigurationUnitSchema.TargetServicePlan))
			{
				exchangeConfigurationUnit.TargetServicePlan = this.TargetServicePlan;
			}
			if (!exchangeConfigurationUnit.HostingDeploymentEnabled && Datacenter.IsPartnerHostedOnly(false))
			{
				exchangeConfigurationUnit.HostingDeploymentEnabled = true;
			}
			if (base.Fields.IsModified(OrganizationSchema.ExcludedFromBackSync))
			{
				exchangeConfigurationUnit.ExcludedFromBackSync = this.ExcludedFromBackSync;
			}
			if (base.Fields.IsModified(OrganizationSchema.ExcludedFromForwardSyncEDU2BPOS))
			{
				exchangeConfigurationUnit.ExcludedFromForwardSyncEDU2BPOS = this.ExcludedFromForwardSyncEDU2BPOS;
			}
			if (base.Fields.IsModified(ExchangeConfigurationUnitSchema.ExchangeUpgradeBucket))
			{
				if (this.ExchangeUpgradeBucket != null)
				{
					ExchangeUpgradeBucket exchangeUpgradeBucket = (ExchangeUpgradeBucket)base.GetDataObject<ExchangeUpgradeBucket>(this.ExchangeUpgradeBucket, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorExchangeUpgradeBucketNotFound(this.ExchangeUpgradeBucket.ToString())), new LocalizedString?(Strings.ErrorExchangeUpgradeBucketNotUnique(this.ExchangeUpgradeBucket.ToString())));
					UpgradeBucketTaskHelper.ValidateOrganizationVersion(exchangeConfigurationUnit, exchangeUpgradeBucket, new Task.ErrorLoggerDelegate(base.WriteError));
					UpgradeBucketTaskHelper.ValidateOrganizationAddition(base.GlobalConfigSession, exchangeConfigurationUnit.OrganizationId, exchangeUpgradeBucket, new Task.ErrorLoggerDelegate(base.WriteError));
					exchangeConfigurationUnit.ExchangeUpgradeBucket = (ADObjectId)exchangeUpgradeBucket.Identity;
				}
				else
				{
					exchangeConfigurationUnit.ExchangeUpgradeBucket = null;
				}
			}
			if (base.Fields.IsModified(OrganizationSchema.MailboxRelease))
			{
				exchangeConfigurationUnit.MailboxRelease = this.MailboxRelease;
			}
			if (base.Fields.IsModified(OrganizationSchema.PreviousMailboxRelease))
			{
				exchangeConfigurationUnit.PreviousMailboxRelease = this.PreviousMailboxRelease;
			}
			if (base.Fields.IsModified(OrganizationSchema.PilotMailboxRelease))
			{
				exchangeConfigurationUnit.PilotMailboxRelease = this.PilotMailboxRelease;
			}
			if (base.Fields.IsModified("AddRelocationConstraint"))
			{
				RelocationConstraint constraintToAdd = new RelocationConstraint((RelocationConstraintType)this.RelocationConstraintType, DateTime.UtcNow.AddDays((double)this.RelocationConstraintExpirationInDays));
				RelocationConstraintArray persistedRelocationConstraints = SetOrganization.PopulateRelocationConstraintsList(exchangeConfigurationUnit.PersistedRelocationConstraints, this.RelocationConstraintType, constraintToAdd);
				exchangeConfigurationUnit.PersistedRelocationConstraints = persistedRelocationConstraints;
			}
			else if (base.Fields.IsModified("RemoveRelocationConstraint"))
			{
				RelocationConstraintArray persistedRelocationConstraints2 = SetOrganization.PopulateRelocationConstraintsList(exchangeConfigurationUnit.PersistedRelocationConstraints, this.RelocationConstraintType, null);
				exchangeConfigurationUnit.PersistedRelocationConstraints = persistedRelocationConstraints2;
			}
			OrganizationStatus organizationStatus;
			if (exchangeConfigurationUnit.IsModified(ExchangeConfigurationUnitSchema.OrganizationStatus) && exchangeConfigurationUnit.TryGetOriginalValue<OrganizationStatus>(ExchangeConfigurationUnitSchema.OrganizationStatus, out organizationStatus))
			{
				if (OrganizationStatus.Active == exchangeConfigurationUnit.OrganizationStatus && (OrganizationStatus.Suspended == organizationStatus || OrganizationStatus.LockedOut == organizationStatus))
				{
					exchangeConfigurationUnit.IsTenantAccessBlocked = false;
				}
				else if ((OrganizationStatus.Suspended == exchangeConfigurationUnit.OrganizationStatus || OrganizationStatus.LockedOut == exchangeConfigurationUnit.OrganizationStatus) && OrganizationStatus.Active == organizationStatus)
				{
					exchangeConfigurationUnit.IsTenantAccessBlocked = true;
				}
			}
			if (base.Fields.IsModified(OrganizationSchema.IsLicensingEnforced))
			{
				exchangeConfigurationUnit.IsLicensingEnforced = this.IsLicensingEnforced;
			}
			TaskLogger.LogExit();
			return exchangeConfigurationUnit;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0005D7D8 File Offset: 0x0005B9D8
		private static RelocationConstraintArray PopulateRelocationConstraintsList(RelocationConstraintArray oldRelocationConstraintsArray, PersistableRelocationConstraintType constraintTypeToSkip, RelocationConstraint constraintToAdd)
		{
			string b = constraintTypeToSkip.ToString();
			List<RelocationConstraint> list = new List<RelocationConstraint>();
			if (oldRelocationConstraintsArray != null && oldRelocationConstraintsArray.RelocationConstraints != null)
			{
				foreach (RelocationConstraint relocationConstraint in oldRelocationConstraintsArray.RelocationConstraints)
				{
					if (relocationConstraint.Name != b)
					{
						list.Add(relocationConstraint);
					}
				}
			}
			if (constraintToAdd != null)
			{
				list.Add(constraintToAdd);
			}
			list.Sort();
			return new RelocationConstraintArray(list.ToArray());
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0005D850 File Offset: 0x0005BA50
		private bool IsHydratedOfferIdMatched(string programId, string offerId, ExchangeConfigurationUnit sharedConfigurationUnit, ServicePlanConfiguration config)
		{
			string offerId2;
			if (!config.TryGetHydratedOfferId(programId, offerId, out offerId2))
			{
				offerId2 = this.DataObject.OfferId;
			}
			return sharedConfigurationUnit.OfferId.Equals(offerId2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0005D884 File Offset: 0x0005BA84
		private bool IsPilotOfferIdMatched(string programId, string offerId, ExchangeConfigurationUnit sharedConfigurationUnit, ServicePlanConfiguration config)
		{
			string dehydratedOfferId;
			string offerId2;
			if (!config.TryGetReversePilotOfferId(programId, offerId, out dehydratedOfferId) || !config.TryGetHydratedOfferId(programId, dehydratedOfferId, out offerId2))
			{
				offerId2 = this.DataObject.OfferId;
			}
			return sharedConfigurationUnit.OfferId.Equals(offerId2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0005D8C4 File Offset: 0x0005BAC4
		private bool IsHydratedPilotOfferIdMatched(string programId, string offerId, ExchangeConfigurationUnit sharedConfigurationUnit, ServicePlanConfiguration config)
		{
			if (this.DataObject.IsStaticConfigurationShared)
			{
				string pilotOfferId;
				string dehydratedOfferId;
				string offerId2;
				if (!config.TryGetReverseHydratedOfferId(programId, offerId, out pilotOfferId) || !config.TryGetReversePilotOfferId(programId, pilotOfferId, out dehydratedOfferId) || !config.TryGetHydratedOfferId(programId, dehydratedOfferId, out offerId2))
				{
					offerId2 = this.DataObject.OfferId;
				}
				return sharedConfigurationUnit.OfferId.Equals(offerId2, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0005D920 File Offset: 0x0005BB20
		protected override void ProvisioningUpdateConfigurationObject()
		{
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0005D922 File Offset: 0x0005BB22
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.Force || !this.confirmLastSharedConfiguration || base.ShouldContinue(Strings.ConfirmDisableLastSharedConfiguration(this.Identity.ToString())))
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x040009A5 RID: 2469
		private const string ParameterSetSharedConfiguration = "SharedConfiguration";

		// Token: 0x040009A6 RID: 2470
		private const string ParameterSetSharedConfigurationInfo = "SharedConfigurationInfo";

		// Token: 0x040009A7 RID: 2471
		private const string ParameterSetSharedConfigurationRemove = "SharedConfigurationRemove";

		// Token: 0x040009A8 RID: 2472
		private const string ParameterSetAddRelocationConstraint = "AddRelocationConstraint";

		// Token: 0x040009A9 RID: 2473
		private const string ParameterSetRemoveRelocationConstraint = "RemoveRelocationConstraint";

		// Token: 0x040009AA RID: 2474
		private const string RelocationConstraintTypeParameter = "RelocationConstraintType";

		// Token: 0x040009AB RID: 2475
		private const string RelocationConstraintExpirationInDaysParameter = "RelocationConstraintExpirationInDays";

		// Token: 0x040009AC RID: 2476
		private List<ExchangeConfigurationUnit> sharedConfigurationUnits;

		// Token: 0x040009AD RID: 2477
		private bool confirmLastSharedConfiguration;
	}
}
