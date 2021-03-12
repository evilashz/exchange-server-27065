using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.ValidationRules;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000750 RID: 1872
	[Serializable]
	public class RoleGroup : ADPresentationObject
	{
		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x06005AE6 RID: 23270 RVA: 0x0013E4E6 File Offset: 0x0013C6E6
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return RoleGroup.schema;
			}
		}

		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x0013E4ED File Offset: 0x0013C6ED
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x0013E4F4 File Offset: 0x0013C6F4
		public RoleGroup()
		{
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x0013E4FC File Offset: 0x0013C6FC
		public RoleGroup(ADGroup dataObject, Result<ExchangeRoleAssignment>[] roleAssignmentResults) : base(dataObject)
		{
			if (roleAssignmentResults != null)
			{
				foreach (Result<ExchangeRoleAssignment> result in roleAssignmentResults)
				{
					ExchangeRoleAssignment data = result.Data;
					this.RoleAssignments.Add(data.Id);
					if (data.Role != null && !this.Roles.Contains(data.Role))
					{
						this.Roles.Add(data.Role);
					}
				}
			}
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x0013E575 File Offset: 0x0013C775
		internal static RoleGroup FromDataObject(ADGroup dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new RoleGroup(dataObject, null);
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x0013E584 File Offset: 0x0013C784
		internal static bool ContainsRoleAssignments(IPropertyBag propertyBag)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)propertyBag[RoleGroupSchema.RoleAssignments];
			return multiValuedProperty.Count != 0;
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x0013E5AE File Offset: 0x0013C7AE
		internal void PopulateCapabilitiesProperty()
		{
			this.Capabilities = CapabilityIdentifierEvaluatorFactory.GetCapabilities(base.DataObject);
		}

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x06005AED RID: 23277 RVA: 0x0013E5C1 File Offset: 0x0013C7C1
		// (set) Token: 0x06005AEE RID: 23278 RVA: 0x0013E5D3 File Offset: 0x0013C7D3
		public MultiValuedProperty<ADObjectId> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[RoleGroupSchema.ManagedBy];
			}
			set
			{
				this[RoleGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x06005AEF RID: 23279 RVA: 0x0013E5E1 File Offset: 0x0013C7E1
		// (set) Token: 0x06005AF0 RID: 23280 RVA: 0x0013E5F3 File Offset: 0x0013C7F3
		public MultiValuedProperty<ADObjectId> RoleAssignments
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[RoleGroupSchema.RoleAssignments];
			}
			private set
			{
				this[RoleGroupSchema.RoleAssignments] = value;
			}
		}

		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x06005AF1 RID: 23281 RVA: 0x0013E601 File Offset: 0x0013C801
		// (set) Token: 0x06005AF2 RID: 23282 RVA: 0x0013E613 File Offset: 0x0013C813
		public MultiValuedProperty<ADObjectId> Roles
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[RoleGroupSchema.Roles];
			}
			private set
			{
				this[RoleGroupSchema.Roles] = value;
			}
		}

		// Token: 0x17001F8B RID: 8075
		// (get) Token: 0x06005AF3 RID: 23283 RVA: 0x0013E621 File Offset: 0x0013C821
		// (set) Token: 0x06005AF4 RID: 23284 RVA: 0x0013E633 File Offset: 0x0013C833
		public string DisplayName
		{
			get
			{
				return (string)this[RoleGroupSchema.DisplayName];
			}
			private set
			{
				this[RoleGroupSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x06005AF5 RID: 23285 RVA: 0x0013E641 File Offset: 0x0013C841
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this[RoleGroupSchema.ExternalDirectoryObjectId];
			}
		}

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x0013E653 File Offset: 0x0013C853
		// (set) Token: 0x06005AF7 RID: 23287 RVA: 0x0013E665 File Offset: 0x0013C865
		public MultiValuedProperty<ADObjectId> Members
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[RoleGroupSchema.Members];
			}
			private set
			{
				this[RoleGroupSchema.Members] = value;
			}
		}

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x0013E673 File Offset: 0x0013C873
		// (set) Token: 0x06005AF9 RID: 23289 RVA: 0x0013E685 File Offset: 0x0013C885
		public string SamAccountName
		{
			get
			{
				return (string)this[RoleGroupSchema.SamAccountName];
			}
			private set
			{
				this[RoleGroupSchema.SamAccountName] = value;
			}
		}

		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x06005AFA RID: 23290 RVA: 0x0013E693 File Offset: 0x0013C893
		// (set) Token: 0x06005AFB RID: 23291 RVA: 0x0013E6A5 File Offset: 0x0013C8A5
		public string Description
		{
			get
			{
				return (string)this[RoleGroupSchema.Description];
			}
			set
			{
				this[RoleGroupSchema.Description] = value;
			}
		}

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x06005AFC RID: 23292 RVA: 0x0013E6B3 File Offset: 0x0013C8B3
		public RoleGroupType RoleGroupType
		{
			get
			{
				return (RoleGroupType)this[RoleGroupSchema.RoleGroupType];
			}
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x06005AFD RID: 23293 RVA: 0x0013E6C5 File Offset: 0x0013C8C5
		public string LinkedGroup
		{
			get
			{
				return (string)this[RoleGroupSchema.LinkedGroup];
			}
		}

		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x06005AFE RID: 23294 RVA: 0x0013E6D7 File Offset: 0x0013C8D7
		// (set) Token: 0x06005AFF RID: 23295 RVA: 0x0013E6E9 File Offset: 0x0013C8E9
		public MultiValuedProperty<Capability> Capabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)this[RoleGroupSchema.Capabilities];
			}
			private set
			{
				this[RoleGroupSchema.Capabilities] = value;
			}
		}

		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x06005B00 RID: 23296 RVA: 0x0013E6F7 File Offset: 0x0013C8F7
		public string LinkedPartnerGroupId
		{
			get
			{
				return (string)this[RoleGroupSchema.LinkedPartnerGroupId];
			}
		}

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x06005B01 RID: 23297 RVA: 0x0013E709 File Offset: 0x0013C909
		public string LinkedPartnerOrganizationId
		{
			get
			{
				return (string)this[RoleGroupSchema.LinkedPartnerOrganizationId];
			}
		}

		// Token: 0x04003D36 RID: 15670
		public const string OrganizationManagement = "Organization Management";

		// Token: 0x04003D37 RID: 15671
		public const string RecipientManagement = "Recipient Management";

		// Token: 0x04003D38 RID: 15672
		public const string ViewOnlyOrganizationManagement = "View-Only Organization Management";

		// Token: 0x04003D39 RID: 15673
		public const string PublicFolderManagement = "Public Folder Management";

		// Token: 0x04003D3A RID: 15674
		public const string UMManagement = "UM Management";

		// Token: 0x04003D3B RID: 15675
		public const string HelpDesk = "Help Desk";

		// Token: 0x04003D3C RID: 15676
		public const string RecordsManagement = "Records Management";

		// Token: 0x04003D3D RID: 15677
		public const string DiscoveryManagement = "Discovery Management";

		// Token: 0x04003D3E RID: 15678
		public const string ServerManagement = "Server Management";

		// Token: 0x04003D3F RID: 15679
		public const string DelegatedSetup = "Delegated Setup";

		// Token: 0x04003D40 RID: 15680
		public const string HygieneManagement = "Hygiene Management";

		// Token: 0x04003D41 RID: 15681
		public const string ManagementForestOperator = "Management Forest Operator";

		// Token: 0x04003D42 RID: 15682
		public const string ManagementForestTier1Support = "Management Forest Tier 1 Support";

		// Token: 0x04003D43 RID: 15683
		public const string ViewOnlyManagementForestOperator = "View-Only Mgmt Forest Operator";

		// Token: 0x04003D44 RID: 15684
		public const string ManagementForestMonitoring = "Management Forest Monitoring";

		// Token: 0x04003D45 RID: 15685
		public const string DataCenterManagement = "DataCenter Management";

		// Token: 0x04003D46 RID: 15686
		public const string ViewOnlyLocalServerAccess = "View-Only Local Server Access";

		// Token: 0x04003D47 RID: 15687
		public const string DestructiveAccess = "Destructive Access";

		// Token: 0x04003D48 RID: 15688
		public const string ElevatedPermissions = "Elevated Permissions";

		// Token: 0x04003D49 RID: 15689
		public const string ServiceAccounts = "Service Accounts";

		// Token: 0x04003D4A RID: 15690
		public const string Operations = "Operations";

		// Token: 0x04003D4B RID: 15691
		public const string ViewOnly = "View-Only";

		// Token: 0x04003D4C RID: 15692
		public const string ComplianceManagement = "Compliance Management";

		// Token: 0x04003D4D RID: 15693
		public const string ViewOnlyPII = "View-Only PII";

		// Token: 0x04003D4E RID: 15694
		public const string CapacityDestructiveAccess = "Capacity Destructive Access";

		// Token: 0x04003D4F RID: 15695
		public const string CapacityServerAdmins = "Capacity Server Admins";

		// Token: 0x04003D50 RID: 15696
		public const string CapacityFrontendServerAdmins = "Cafe Server Admins";

		// Token: 0x04003D51 RID: 15697
		public const string CustomerChangeAccess = "Customer Change Access";

		// Token: 0x04003D52 RID: 15698
		public const string CustomerDataAccess = "Customer Data Access";

		// Token: 0x04003D53 RID: 15699
		public const string AccessToCustomerDataDCOnly = "Access To Customer Data - DC Only";

		// Token: 0x04003D54 RID: 15700
		public const string DatacenterOperationsDCOnly = "Datacenter Operations - DC Only";

		// Token: 0x04003D55 RID: 15701
		public const string CustomerDestructiveAccess = "Customer Destructive Access";

		// Token: 0x04003D56 RID: 15702
		public const string CustomerPIIAccess = "Customer PII Access";

		// Token: 0x04003D57 RID: 15703
		public const string DedicatedSupportAccess = "Dedicated Support Access";

		// Token: 0x04003D58 RID: 15704
		public const string ECSAdminServerAccess = "ECS Admin - Server Access";

		// Token: 0x04003D59 RID: 15705
		public const string ECSPIIAccessServerAccess = "ECS PII Access - Server Access";

		// Token: 0x04003D5A RID: 15706
		public const string ECSAdmin = "ECS Admin";

		// Token: 0x04003D5B RID: 15707
		public const string ECSPIIAccess = "ECS PII Access";

		// Token: 0x04003D5C RID: 15708
		public const string ManagementAdminAccess = "Management Admin Access";

		// Token: 0x04003D5D RID: 15709
		public const string ManagementCACoreAdmin = "Management CA Core Admin";

		// Token: 0x04003D5E RID: 15710
		public const string ManagementChangeAccess = "Management Change Access";

		// Token: 0x04003D5F RID: 15711
		public const string ManagementDestructiveAccess = "Management Destructive Access";

		// Token: 0x04003D60 RID: 15712
		public const string ManagementServerAdmins = "Management Server Admins";

		// Token: 0x04003D61 RID: 15713
		public const string CapacityDCAdmins = "Capacity DC Admins";

		// Token: 0x04003D62 RID: 15714
		public const string NetworkingAdminAccess = "Networking Admin Access";

		// Token: 0x04003D63 RID: 15715
		public const string NetworkingChangeAccess = "Networking Change Access";

		// Token: 0x04003D64 RID: 15716
		public const string CommunicationManagers = "Communications Manager";

		// Token: 0x04003D65 RID: 15717
		public const string MailboxManagement = "Mailbox Management";

		// Token: 0x04003D66 RID: 15718
		public const string FfoAntiSpamAdmins = "Ffo AntiSpam Admins";

		// Token: 0x04003D67 RID: 15719
		public const string AppLockerExemption = "AppLocker Exemption";

		// Token: 0x04003D68 RID: 15720
		public const string MsoPartnerTenantAdmin = "AdminAgents";

		// Token: 0x04003D69 RID: 15721
		public const string MsoPartnerTenantHelpdesk = "HelpdeskAgents";

		// Token: 0x04003D6A RID: 15722
		public const string MsoManagedTenantAdmin = "TenantAdmins";

		// Token: 0x04003D6B RID: 15723
		public const string MsoMailTenantAdmin = "ExchangeServiceAdmins";

		// Token: 0x04003D6C RID: 15724
		public const string MsoManagedTenantHelpdesk = "HelpdeskAdmins";

		// Token: 0x04003D6D RID: 15725
		public static readonly Guid OrganizationManagementWkGuid = WellKnownGuid.EoaWkGuid;

		// Token: 0x04003D6E RID: 15726
		public static readonly Guid RecipientManagementWkGuid = WellKnownGuid.EmaWkGuid;

		// Token: 0x04003D6F RID: 15727
		public static readonly Guid ViewOnlyOrganizationManagementWkGuid = WellKnownGuid.EraWkGuid;

		// Token: 0x04003D70 RID: 15728
		public static readonly Guid PublicFolderManagementWkGuid = WellKnownGuid.EpaWkGuid;

		// Token: 0x04003D71 RID: 15729
		public static readonly Guid UMManagementWkGuid = WellKnownGuid.RgUmManagementWkGuid;

		// Token: 0x04003D72 RID: 15730
		public static readonly Guid HelpDeskWkGuid = WellKnownGuid.RgHelpDeskWkGuid;

		// Token: 0x04003D73 RID: 15731
		public static readonly Guid RecordsManagementWkGuid = WellKnownGuid.RgRecordsManagementWkGuid;

		// Token: 0x04003D74 RID: 15732
		public static readonly Guid DiscoveryManagementWkGuid = WellKnownGuid.RgDiscoveryManagementWkGuid;

		// Token: 0x04003D75 RID: 15733
		public static readonly Guid ServerManagementWkGuid = WellKnownGuid.RgServerManagementWkGuid;

		// Token: 0x04003D76 RID: 15734
		public static readonly Guid DelegatedSetupWkGuid = WellKnownGuid.RgDelegatedSetupWkGuid;

		// Token: 0x04003D77 RID: 15735
		public static readonly Guid HygieneManagementWkGuid = WellKnownGuid.RgHygieneManagementWkGuid;

		// Token: 0x04003D78 RID: 15736
		public static readonly Guid ManagementForestOperatorWkGuid = WellKnownGuid.RgManagementForestOperatorWkGuid;

		// Token: 0x04003D79 RID: 15737
		public static readonly Guid ManagementForestTier1SupportWkGuid = WellKnownGuid.RgManagementForestTier1SupportWkGuid;

		// Token: 0x04003D7A RID: 15738
		public static readonly Guid ViewOnlyManagementForestOperatorWkGuid = WellKnownGuid.RgViewOnlyManagementForestOperatorWkGuid;

		// Token: 0x04003D7B RID: 15739
		public static readonly Guid ManagementForestMonitoringWkGuid = WellKnownGuid.RgManagementForestMonitoringWkGuid;

		// Token: 0x04003D7C RID: 15740
		public static readonly Guid DataCenterManagementWkGuid = WellKnownGuid.RgDataCenterManagementWkGuid;

		// Token: 0x04003D7D RID: 15741
		public static readonly Guid ViewOnlyLocalServerAccessWkGuid = WellKnownGuid.RgViewOnlyLocalServerAccessWkGuid;

		// Token: 0x04003D7E RID: 15742
		public static readonly Guid DestructiveAccessWkGuid = WellKnownGuid.RgDestructiveAccessWkGuid;

		// Token: 0x04003D7F RID: 15743
		public static readonly Guid ElevatedPermissionsWkGuid = WellKnownGuid.RgElevatedPermissionsWkGuid;

		// Token: 0x04003D80 RID: 15744
		public static readonly Guid ServiceAccountsWkGuid = WellKnownGuid.RgServiceAccountsWkGuid;

		// Token: 0x04003D81 RID: 15745
		public static readonly Guid OperationsWkGuid = WellKnownGuid.RgOperationsWkGuid;

		// Token: 0x04003D82 RID: 15746
		public static readonly Guid ViewOnlyWkGuid = WellKnownGuid.RgViewOnlyWkGuid;

		// Token: 0x04003D83 RID: 15747
		public static readonly Guid CapacityDestructiveAccessWkGuid = WellKnownGuid.RgCapacityDestructiveAccessWkGuid;

		// Token: 0x04003D84 RID: 15748
		public static readonly Guid CapacityServerAdminsWkGuid = WellKnownGuid.RgCapacityServerAdminsWkGuid;

		// Token: 0x04003D85 RID: 15749
		public static readonly Guid CapacityFrontendServerAdminsWkGuid = WellKnownGuid.RgCapacityFrontendServerAdminsWkGuid;

		// Token: 0x04003D86 RID: 15750
		public static readonly Guid CustomerChangeAccessWkGuid = WellKnownGuid.RgCustomerChangeAccessWkGuid;

		// Token: 0x04003D87 RID: 15751
		public static readonly Guid CustomerDataAccessWkGuid = WellKnownGuid.RgCustomerDataAccessWkGuid;

		// Token: 0x04003D88 RID: 15752
		public static readonly Guid AccessToCustomerDataDCOnlyWkGuid = WellKnownGuid.RgAccessToCustomerDataDCOnlyWkGuid;

		// Token: 0x04003D89 RID: 15753
		public static readonly Guid DatacenterOperationsDCOnlyWkGuid = WellKnownGuid.RgDatacenterOperationsDCOnlyWkGuid;

		// Token: 0x04003D8A RID: 15754
		public static readonly Guid CustomerDestructiveAccessWkGuid = WellKnownGuid.RgCustomerDestructiveAccessWkGuid;

		// Token: 0x04003D8B RID: 15755
		public static readonly Guid CustomerPIIAccessWkGuid = WellKnownGuid.RgCustomerPIIAccessWkGuid;

		// Token: 0x04003D8C RID: 15756
		public static readonly Guid ManagementAdminAccessWkGuid = WellKnownGuid.RgManagementAdminAccessWkGuid;

		// Token: 0x04003D8D RID: 15757
		public static readonly Guid ManagementCACoreAdminWkGuid = WellKnownGuid.RgManagementCACoreAdminWkGuid;

		// Token: 0x04003D8E RID: 15758
		public static readonly Guid ManagementChangeAccessWkGuid = WellKnownGuid.RgManagementChangeAccessWkGuid;

		// Token: 0x04003D8F RID: 15759
		public static readonly Guid ManagementDestructiveAccessWkGuid = WellKnownGuid.RgManagementDestructiveAccessWkGuid;

		// Token: 0x04003D90 RID: 15760
		public static readonly Guid ManagementServerAdminsWkGuid = WellKnownGuid.RgManagementServerAdminsWkGuid;

		// Token: 0x04003D91 RID: 15761
		public static readonly Guid CapacityDCAdminsWkGuid = WellKnownGuid.RgCapacityDCAdminsWkGuid;

		// Token: 0x04003D92 RID: 15762
		public static readonly Guid NetworkingAdminAccessWkGuid = WellKnownGuid.RgNetworkingAdminAccessWkGuid;

		// Token: 0x04003D93 RID: 15763
		public static readonly Guid NetworkingChangeAccessWkGuid = WellKnownGuid.RgNetworkingChangeAccessWkGuid;

		// Token: 0x04003D94 RID: 15764
		public static readonly Guid MailboxManagementWkGuid = WellKnownGuid.RgMailboxManagementWkGuid;

		// Token: 0x04003D95 RID: 15765
		public static readonly Guid FfoAntiSpamAdminsWkGuid = WellKnownGuid.RgFfoAntiSpamAdminsWkGuid;

		// Token: 0x04003D96 RID: 15766
		public static readonly Guid AppLockerExemptionWkGuid = WellKnownGuid.RgAppLockerExemptionWkGuid;

		// Token: 0x04003D97 RID: 15767
		internal static Dictionary<int, string> RoleGroupStringIds = new Dictionary<int, string>
		{
			{
				1,
				"ExchangeOrgAdminDescription"
			},
			{
				2,
				"ExchangeRecipientAdminDescription"
			},
			{
				3,
				"ExchangeViewOnlyAdminDescription"
			},
			{
				4,
				"ExchangePublicFolderAdminDescription"
			},
			{
				5,
				"ExchangeUMManagementDescription"
			},
			{
				6,
				"ExchangeHelpDeskDescription"
			},
			{
				7,
				"ExchangeRecordsManagementDescription"
			},
			{
				8,
				"ExchangeDiscoveryManagementDescription"
			},
			{
				9,
				"ExchangeServerManagementDescription"
			},
			{
				10,
				"ExchangeDelegatedSetupDescription"
			},
			{
				11,
				"ExchangeHygieneManagementDescription"
			},
			{
				12,
				"ExchangeManagementForestOperatorDescription"
			},
			{
				13,
				"ExchangeManagementForestTier1SupportDescription"
			},
			{
				14,
				"ExchangeViewOnlyManagementForestOperatorDescription"
			},
			{
				15,
				"ExchangeManagementForestMonitoringDescription"
			},
			{
				23,
				"MsoManagedTenantAdminGroupDescription"
			},
			{
				24,
				"MsoMailTenantAdminGroupDescription"
			},
			{
				25,
				"MsoManagedTenantHelpdeskGroupDescription"
			},
			{
				26,
				"ComplianceManagementGroupDescription"
			},
			{
				27,
				"ViewOnlyPIIGroupDescription"
			}
		};

		// Token: 0x04003D98 RID: 15768
		public static readonly RoleGroupInitInfo OrganizationManagement_InitInfo = new RoleGroupInitInfo("Organization Management", 1, WellKnownGuid.EoaWkGuid);

		// Token: 0x04003D99 RID: 15769
		public static readonly RoleGroupInitInfo RecipientManagement_InitInfo = new RoleGroupInitInfo("Recipient Management", 2, WellKnownGuid.EmaWkGuid);

		// Token: 0x04003D9A RID: 15770
		public static readonly RoleGroupInitInfo ViewOnlyOrganizationManagement_InitInfo = new RoleGroupInitInfo("View-Only Organization Management", 3, WellKnownGuid.EraWkGuid);

		// Token: 0x04003D9B RID: 15771
		public static readonly RoleGroupInitInfo PublicFolderManagement_InitInfo = new RoleGroupInitInfo("Public Folder Management", 4, WellKnownGuid.EpaWkGuid);

		// Token: 0x04003D9C RID: 15772
		public static readonly RoleGroupInitInfo UMManagement_InitInfo = new RoleGroupInitInfo("UM Management", 5, WellKnownGuid.RgUmManagementWkGuid);

		// Token: 0x04003D9D RID: 15773
		public static readonly RoleGroupInitInfo HelpDesk_InitInfo = new RoleGroupInitInfo("Help Desk", 6, WellKnownGuid.RgHelpDeskWkGuid);

		// Token: 0x04003D9E RID: 15774
		public static readonly RoleGroupInitInfo RecordsManagement_InitInfo = new RoleGroupInitInfo("Records Management", 7, WellKnownGuid.RgRecordsManagementWkGuid);

		// Token: 0x04003D9F RID: 15775
		public static readonly RoleGroupInitInfo DiscoveryManagement_InitInfo = new RoleGroupInitInfo("Discovery Management", 8, WellKnownGuid.RgDiscoveryManagementWkGuid);

		// Token: 0x04003DA0 RID: 15776
		public static readonly RoleGroupInitInfo ServerManagement_InitInfo = new RoleGroupInitInfo("Server Management", 9, WellKnownGuid.RgServerManagementWkGuid);

		// Token: 0x04003DA1 RID: 15777
		public static readonly RoleGroupInitInfo DelegatedSetup_InitInfo = new RoleGroupInitInfo("Delegated Setup", 10, WellKnownGuid.RgDelegatedSetupWkGuid);

		// Token: 0x04003DA2 RID: 15778
		public static readonly RoleGroupInitInfo HygieneManagement_InitInfo = new RoleGroupInitInfo("Hygiene Management", 11, WellKnownGuid.RgHygieneManagementWkGuid);

		// Token: 0x04003DA3 RID: 15779
		public static readonly RoleGroupInitInfo ManagementForestOperator_InitInfo = new RoleGroupInitInfo("Management Forest Operator", 12, WellKnownGuid.RgManagementForestOperatorWkGuid);

		// Token: 0x04003DA4 RID: 15780
		public static readonly RoleGroupInitInfo ManagementForestTier1Support_InitInfo = new RoleGroupInitInfo("Management Forest Tier 1 Support", 13, WellKnownGuid.RgManagementForestTier1SupportWkGuid);

		// Token: 0x04003DA5 RID: 15781
		public static readonly RoleGroupInitInfo ViewOnlyManagementForestOperator_InitInfo = new RoleGroupInitInfo("View-Only Mgmt Forest Operator", 14, WellKnownGuid.RgViewOnlyManagementForestOperatorWkGuid);

		// Token: 0x04003DA6 RID: 15782
		public static readonly RoleGroupInitInfo ManagementForestMonitoring_InitInfo = new RoleGroupInitInfo("Management Forest Monitoring", 15, WellKnownGuid.RgManagementForestMonitoringWkGuid);

		// Token: 0x04003DA7 RID: 15783
		public static readonly RoleGroupInitInfo DataCenterManagement_InitInfo = new RoleGroupInitInfo("DataCenter Management", 16, WellKnownGuid.RgDataCenterManagementWkGuid);

		// Token: 0x04003DA8 RID: 15784
		public static readonly RoleGroupInitInfo ViewOnlyLocalServerAccess_InitInfo = new RoleGroupInitInfo("View-Only Local Server Access", 17, WellKnownGuid.RgViewOnlyLocalServerAccessWkGuid);

		// Token: 0x04003DA9 RID: 15785
		public static readonly RoleGroupInitInfo DestructiveAccess_InitInfo = new RoleGroupInitInfo("Destructive Access", 18, WellKnownGuid.RgDestructiveAccessWkGuid);

		// Token: 0x04003DAA RID: 15786
		public static readonly RoleGroupInitInfo ElevatedPermissions_InitInfo = new RoleGroupInitInfo("Elevated Permissions", 19, WellKnownGuid.RgElevatedPermissionsWkGuid);

		// Token: 0x04003DAB RID: 15787
		public static readonly RoleGroupInitInfo ServiceAccounts_InitInfo = new RoleGroupInitInfo("Service Accounts", 20, WellKnownGuid.RgServiceAccountsWkGuid);

		// Token: 0x04003DAC RID: 15788
		public static readonly RoleGroupInitInfo Operations_InitInfo = new RoleGroupInitInfo("Operations", 21, WellKnownGuid.RgOperationsWkGuid);

		// Token: 0x04003DAD RID: 15789
		public static readonly RoleGroupInitInfo ViewOnly_InitInfo = new RoleGroupInitInfo("View-Only", 22, WellKnownGuid.RgViewOnlyWkGuid);

		// Token: 0x04003DAE RID: 15790
		public static readonly RoleGroupInitInfo ComplianceManagement_InitInfo = new RoleGroupInitInfo("Compliance Management", 26, WellKnownGuid.RgComplianceManagementWkGuid);

		// Token: 0x04003DAF RID: 15791
		public static readonly RoleGroupInitInfo ViewOnlyPII_InitInfo = new RoleGroupInitInfo("View-Only PII", 27, WellKnownGuid.RgViewOnlyPIIWkGuid);

		// Token: 0x04003DB0 RID: 15792
		public static readonly RoleGroupInitInfo CapacityDestructiveAccess_InitInfo = new RoleGroupInitInfo("Capacity Destructive Access", 28, WellKnownGuid.RgCapacityDestructiveAccessWkGuid);

		// Token: 0x04003DB1 RID: 15793
		public static readonly RoleGroupInitInfo CapacityServerAdmins_InitInfo = new RoleGroupInitInfo("Capacity Server Admins", 29, WellKnownGuid.RgCapacityServerAdminsWkGuid);

		// Token: 0x04003DB2 RID: 15794
		public static readonly RoleGroupInitInfo CapacityFrontendServerAdmins_InitInfo = new RoleGroupInitInfo("Cafe Server Admins", 43, WellKnownGuid.RgCapacityFrontendServerAdminsWkGuid);

		// Token: 0x04003DB3 RID: 15795
		public static readonly RoleGroupInitInfo CustomerChangeAccess_InitInfo = new RoleGroupInitInfo("Customer Change Access", 30, WellKnownGuid.RgCustomerChangeAccessWkGuid);

		// Token: 0x04003DB4 RID: 15796
		public static readonly RoleGroupInitInfo CustomerDataAccess_InitInfo = new RoleGroupInitInfo("Customer Data Access", 31, WellKnownGuid.RgCustomerDataAccessWkGuid);

		// Token: 0x04003DB5 RID: 15797
		public static readonly RoleGroupInitInfo AccessToCustomerDataDCOnly_InitInfo = new RoleGroupInitInfo("Access To Customer Data - DC Only", 52, WellKnownGuid.RgAccessToCustomerDataDCOnlyWkGuid);

		// Token: 0x04003DB6 RID: 15798
		public static readonly RoleGroupInitInfo DatacenterOperationsDCOnly_InitInfo = new RoleGroupInitInfo("Datacenter Operations - DC Only", 53, WellKnownGuid.RgDatacenterOperationsDCOnlyWkGuid);

		// Token: 0x04003DB7 RID: 15799
		public static readonly RoleGroupInitInfo CustomerDestructiveAccess_InitInfo = new RoleGroupInitInfo("Customer Destructive Access", 32, WellKnownGuid.RgCustomerDestructiveAccessWkGuid);

		// Token: 0x04003DB8 RID: 15800
		public static readonly RoleGroupInitInfo CustomerPIIAccess_InitInfo = new RoleGroupInitInfo("Customer PII Access", 33, WellKnownGuid.RgCustomerPIIAccessWkGuid);

		// Token: 0x04003DB9 RID: 15801
		public static readonly RoleGroupInitInfo DedicatedSupportAccess_InitInfo = new RoleGroupInitInfo("Dedicated Support Access", 45, WellKnownGuid.RgDedicatedSupportAccessWkGuid);

		// Token: 0x04003DBA RID: 15802
		public static readonly RoleGroupInitInfo ECSAdminServerAccess_InitInfo = new RoleGroupInitInfo("ECS Admin - Server Access", 48, WellKnownGuid.RgECSAdminServerAccessWkGuid);

		// Token: 0x04003DBB RID: 15803
		public static readonly RoleGroupInitInfo ECSPIIAccessServerAccess_InitInfo = new RoleGroupInitInfo("ECS PII Access - Server Access", 49, WellKnownGuid.RgECSPIIAccessServerAccessWkGuid);

		// Token: 0x04003DBC RID: 15804
		public static readonly RoleGroupInitInfo ECSAdmin_InitInfo = new RoleGroupInitInfo("ECS Admin", 50, WellKnownGuid.RgECSAdminWkGuid);

		// Token: 0x04003DBD RID: 15805
		public static readonly RoleGroupInitInfo ECSPIIAccess_InitInfo = new RoleGroupInitInfo("ECS PII Access", 51, WellKnownGuid.RgECSPIIAccessWkGuid);

		// Token: 0x04003DBE RID: 15806
		public static readonly RoleGroupInitInfo ManagementAdminAccess_InitInfo = new RoleGroupInitInfo("Management Admin Access", 34, WellKnownGuid.RgManagementAdminAccessWkGuid);

		// Token: 0x04003DBF RID: 15807
		public static readonly RoleGroupInitInfo ManagementCACoreAdmin_InitInfo = new RoleGroupInitInfo("Management CA Core Admin", 41, WellKnownGuid.RgManagementCACoreAdminWkGuid);

		// Token: 0x04003DC0 RID: 15808
		public static readonly RoleGroupInitInfo ManagementChangeAccess_InitInfo = new RoleGroupInitInfo("Management Change Access", 35, WellKnownGuid.RgManagementChangeAccessWkGuid);

		// Token: 0x04003DC1 RID: 15809
		public static readonly RoleGroupInitInfo ManagementDestructiveAccess_InitInfo = new RoleGroupInitInfo("Management Destructive Access", 39, WellKnownGuid.RgManagementDestructiveAccessWkGuid);

		// Token: 0x04003DC2 RID: 15810
		public static readonly RoleGroupInitInfo ManagementServerAdmins_InitInfo = new RoleGroupInitInfo("Management Server Admins", 36, WellKnownGuid.RgManagementServerAdminsWkGuid);

		// Token: 0x04003DC3 RID: 15811
		public static readonly RoleGroupInitInfo CapacityDCAdmins_InitInfo = new RoleGroupInitInfo("Capacity DC Admins", 37, WellKnownGuid.RgCapacityDCAdminsWkGuid);

		// Token: 0x04003DC4 RID: 15812
		public static readonly RoleGroupInitInfo NetworkingAdminAccess_InitInfo = new RoleGroupInitInfo("Networking Admin Access", 38, WellKnownGuid.RgNetworkingAdminAccessWkGuid);

		// Token: 0x04003DC5 RID: 15813
		public static readonly RoleGroupInitInfo NetworkingChangeAccess_InitInfo = new RoleGroupInitInfo("Networking Change Access", 46, WellKnownGuid.RgNetworkingChangeAccessWkGuid);

		// Token: 0x04003DC6 RID: 15814
		public static readonly RoleGroupInitInfo CommunicationManagers_InitInfo = new RoleGroupInitInfo("Communications Manager", 40, WellKnownGuid.RgCommunicationManagersWkGuid);

		// Token: 0x04003DC7 RID: 15815
		public static readonly RoleGroupInitInfo MailboxManagement_InitInfo = new RoleGroupInitInfo("Mailbox Management", 42, WellKnownGuid.RgMailboxManagementWkGuid);

		// Token: 0x04003DC8 RID: 15816
		public static readonly RoleGroupInitInfo FfoAntiSpamAdmins_InitInfo = new RoleGroupInitInfo("Ffo AntiSpam Admins", 44, WellKnownGuid.RgFfoAntiSpamAdminsWkGuid);

		// Token: 0x04003DC9 RID: 15817
		public static readonly RoleGroupInitInfo AppLockerExemption_InitInfo = new RoleGroupInitInfo("AppLocker Exemption", 47, WellKnownGuid.RgAppLockerExemptionWkGuid);

		// Token: 0x04003DCA RID: 15818
		private static RoleGroupSchema schema = ObjectSchema.GetInstance<RoleGroupSchema>();

		// Token: 0x02000751 RID: 1873
		internal enum RoleGroupTypeIds
		{
			// Token: 0x04003DCC RID: 15820
			Unknown,
			// Token: 0x04003DCD RID: 15821
			OrganizationManagement,
			// Token: 0x04003DCE RID: 15822
			RecipientManagement,
			// Token: 0x04003DCF RID: 15823
			ViewOnlyOrganizationManagement,
			// Token: 0x04003DD0 RID: 15824
			PublicFolderManagement,
			// Token: 0x04003DD1 RID: 15825
			UMManagement,
			// Token: 0x04003DD2 RID: 15826
			HelpDesk,
			// Token: 0x04003DD3 RID: 15827
			RecordsManagement,
			// Token: 0x04003DD4 RID: 15828
			DiscoveryManagement,
			// Token: 0x04003DD5 RID: 15829
			ServerManagement,
			// Token: 0x04003DD6 RID: 15830
			DelegatedSetup,
			// Token: 0x04003DD7 RID: 15831
			HygieneManagement,
			// Token: 0x04003DD8 RID: 15832
			ManagementForestOperator,
			// Token: 0x04003DD9 RID: 15833
			ManagementForestTier1Support,
			// Token: 0x04003DDA RID: 15834
			ViewOnlyManagementForestOperator,
			// Token: 0x04003DDB RID: 15835
			ManagementForestMonitoring,
			// Token: 0x04003DDC RID: 15836
			DataCenterManagement,
			// Token: 0x04003DDD RID: 15837
			ViewOnlyLocalServerAccess,
			// Token: 0x04003DDE RID: 15838
			DestructiveAccess,
			// Token: 0x04003DDF RID: 15839
			ElevatedPermissions,
			// Token: 0x04003DE0 RID: 15840
			ServiceAccounts,
			// Token: 0x04003DE1 RID: 15841
			Operations,
			// Token: 0x04003DE2 RID: 15842
			ViewOnly,
			// Token: 0x04003DE3 RID: 15843
			MsoManagedTenantAdmin,
			// Token: 0x04003DE4 RID: 15844
			MsoMailTenantAdmin,
			// Token: 0x04003DE5 RID: 15845
			MsoManagedTenantHelpdesk,
			// Token: 0x04003DE6 RID: 15846
			ComplianceManagement,
			// Token: 0x04003DE7 RID: 15847
			ViewOnlyPII,
			// Token: 0x04003DE8 RID: 15848
			CapacityDestructiveAccess,
			// Token: 0x04003DE9 RID: 15849
			CapacityServerAdmins,
			// Token: 0x04003DEA RID: 15850
			CustomerChangeAccess,
			// Token: 0x04003DEB RID: 15851
			CustomerDataAccess,
			// Token: 0x04003DEC RID: 15852
			CustomerDestructiveAccess,
			// Token: 0x04003DED RID: 15853
			CustomerPIIAccess,
			// Token: 0x04003DEE RID: 15854
			ManagementAdminAccess,
			// Token: 0x04003DEF RID: 15855
			ManagementChangeAccess,
			// Token: 0x04003DF0 RID: 15856
			ManagementServerAdmins,
			// Token: 0x04003DF1 RID: 15857
			CapacityDCAdmins,
			// Token: 0x04003DF2 RID: 15858
			NetworkingAdminAccess,
			// Token: 0x04003DF3 RID: 15859
			ManagementDestructiveAccess,
			// Token: 0x04003DF4 RID: 15860
			CommunicationManagers,
			// Token: 0x04003DF5 RID: 15861
			ManagementCACoreAdmin,
			// Token: 0x04003DF6 RID: 15862
			MailboxManagement,
			// Token: 0x04003DF7 RID: 15863
			CapacityFrontendServerAdmins,
			// Token: 0x04003DF8 RID: 15864
			FfoAntiSpamAdmins,
			// Token: 0x04003DF9 RID: 15865
			DedicatedSupportAccess,
			// Token: 0x04003DFA RID: 15866
			NetworkingChangeAccess,
			// Token: 0x04003DFB RID: 15867
			AppLockerExemption,
			// Token: 0x04003DFC RID: 15868
			ECSAdminServerAccess,
			// Token: 0x04003DFD RID: 15869
			ECSPIIAccessServerAccess,
			// Token: 0x04003DFE RID: 15870
			ECSAdmin,
			// Token: 0x04003DFF RID: 15871
			ECSPIIAccess,
			// Token: 0x04003E00 RID: 15872
			AccessToCustomerDataDCOnly,
			// Token: 0x04003E01 RID: 15873
			DatacenterOperationsDCOnly
		}
	}
}
