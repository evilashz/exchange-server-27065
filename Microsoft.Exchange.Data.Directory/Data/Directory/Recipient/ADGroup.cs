using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001F3 RID: 499
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ADGroup : ADMailboxRecipient, IADGroup, IADMailboxRecipient, IADRecipient, IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag, IADMailStorage, IADSecurityPrincipal, IADDistributionList
	{
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x0006C128 File Offset: 0x0006A328
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADGroup.schema;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0006C12F File Offset: 0x0006A32F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADGroup.MostDerivedClass;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x0006C136 File Offset: 0x0006A336
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0006C149 File Offset: 0x0006A349
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0006C150 File Offset: 0x0006A350
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0006C154 File Offset: 0x0006A354
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			RecipientType recipientType = base.RecipientType;
			if (RecipientType.MailUniversalDistributionGroup == recipientType || RecipientType.MailNonUniversalGroup == recipientType || RecipientType.MailUniversalSecurityGroup == recipientType)
			{
				if (!base.BypassModerationCheck && this.ReportToManagerEnabled && this.ManagedBy == null)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorReportToManagedEnabledWithoutManager(this.Identity.ToString(), ADGroupSchema.ReportToManagerEnabled.Name), this.Identity, string.Empty));
				}
				if (this.ReportToManagerEnabled && this.ReportToOriginatorEnabled)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorReportToBothManagerAndOriginator(this.Identity.ToString(), ADGroupSchema.ReportToManagerEnabled.Name, ADGroupSchema.ReportToOriginatorEnabled.Name), this.Identity, string.Empty));
				}
			}
			if (this.MemberDepartRestriction == MemberUpdateType.ApprovalRequired)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorGroupMemberDepartRestrictionApprovalRequired(this.Identity.ToString()), this.Identity, string.Empty));
			}
			if (this.ManagedBy.Count == 0 && this.MemberJoinRestriction == MemberUpdateType.ApprovalRequired)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorJoinApprovalRequiredWithoutManager(this.Identity.ToString()), this.Identity, string.Empty));
			}
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0006C27C File Offset: 0x0006A47C
		internal override bool ShouldValidatePropertyLinkInSameOrganization(ADPropertyDefinition property)
		{
			return (!OrganizationId.ForestWideOrgId.Equals(base.OrganizationId) || !property.Equals(ADGroupSchema.Members) || base.RecipientTypeDetails == RecipientTypeDetails.MailNonUniversalGroup || base.RecipientTypeDetails == RecipientTypeDetails.MailUniversalDistributionGroup || base.RecipientTypeDetails == RecipientTypeDetails.MailUniversalSecurityGroup) && base.ShouldValidatePropertyLinkInSameOrganization(property);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0006C2DB File Offset: 0x0006A4DB
		internal ADGroup(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0006C2E5 File Offset: 0x0006A4E5
		internal ADGroup(IRecipientSession session, string commonName, ADObjectId containerId)
		{
			this.m_Session = session;
			base.SetId(containerId.GetChildId(commonName));
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0006C30D File Offset: 0x0006A50D
		internal ADGroup(IRecipientSession session, string commonName, ADObjectId containerId, GroupTypeFlags groupType)
		{
			this.m_Session = session;
			base.SetId(containerId.GetChildId(commonName));
			base.SetObjectClass(this.MostDerivedObjectClass);
			this.GroupType = groupType;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0006C33D File Offset: 0x0006A53D
		public ADGroup()
		{
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0006C345 File Offset: 0x0006A545
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x0006C357 File Offset: 0x0006A557
		public string ExpansionServer
		{
			get
			{
				return (string)this[ADGroupSchema.ExpansionServer];
			}
			set
			{
				this[ADGroupSchema.ExpansionServer] = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x0006C365 File Offset: 0x0006A565
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x0006C377 File Offset: 0x0006A577
		public GroupTypeFlags GroupType
		{
			get
			{
				return (GroupTypeFlags)this[ADGroupSchema.GroupType];
			}
			set
			{
				this[ADGroupSchema.GroupType] = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0006C38A File Offset: 0x0006A58A
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x0006C3A4 File Offset: 0x0006A5A4
		public bool LocalizationDisabled
		{
			get
			{
				return ((int)this[ADGroupSchema.LocalizationFlags] & 1) == 1;
			}
			set
			{
				if (value)
				{
					this[ADGroupSchema.LocalizationFlags] = ((int)this[ADGroupSchema.LocalizationFlags] | 1);
					return;
				}
				this[ADGroupSchema.LocalizationFlags] = ((int)this[ADGroupSchema.LocalizationFlags] & -2);
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0006C3FA File Offset: 0x0006A5FA
		// (set) Token: 0x0600198F RID: 6543 RVA: 0x0006C40C File Offset: 0x0006A60C
		internal bool IsExecutingUserGroupOwner
		{
			get
			{
				return (bool)this[ADGroupSchema.IsExecutingUserGroupOwner];
			}
			set
			{
				this[ADGroupSchema.IsExecutingUserGroupOwner] = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0006C41F File Offset: 0x0006A61F
		// (set) Token: 0x06001991 RID: 6545 RVA: 0x0006C431 File Offset: 0x0006A631
		public ADObjectId HomeMTA
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.HomeMTA];
			}
			set
			{
				this[ADRecipientSchema.HomeMTA] = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x0006C43F File Offset: 0x0006A63F
		public bool HiddenGroupMembershipEnabled
		{
			get
			{
				return (bool)this[ADGroupSchema.HiddenGroupMembershipEnabled];
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0006C451 File Offset: 0x0006A651
		public ADObjectId HomeMtaServerId
		{
			get
			{
				return (ADObjectId)this[ADGroupSchema.HomeMtaServerId];
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x0006C463 File Offset: 0x0006A663
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x0006C475 File Offset: 0x0006A675
		public SecurityIdentifier ForeignGroupSid
		{
			get
			{
				return (SecurityIdentifier)this[ADGroupSchema.ForeignGroupSid];
			}
			internal set
			{
				this[ADGroupSchema.ForeignGroupSid] = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x0006C483 File Offset: 0x0006A683
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x0006C495 File Offset: 0x0006A695
		public string LinkedGroup
		{
			get
			{
				return (string)this[ADGroupSchema.LinkedGroup];
			}
			internal set
			{
				this.propertyBag.SetField(ADGroupSchema.LinkedGroup, value);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0006C4A9 File Offset: 0x0006A6A9
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x0006C4BB File Offset: 0x0006A6BB
		public bool IsOrganizationalGroup
		{
			get
			{
				return (bool)this[ADGroupSchema.IsOrganizationalGroup];
			}
			internal set
			{
				this[ADGroupSchema.IsOrganizationalGroup] = value;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x0006C4CE File Offset: 0x0006A6CE
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x0006C4E0 File Offset: 0x0006A6E0
		internal MultiValuedProperty<Capability> RawCapabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)this[ADRecipientSchema.RawCapabilities];
			}
			set
			{
				this[ADRecipientSchema.RawCapabilities] = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x0006C4EE File Offset: 0x0006A6EE
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x0006C500 File Offset: 0x0006A700
		public int GroupMemberCount
		{
			get
			{
				return (int)this[ADGroupSchema.GroupMemberCount];
			}
			set
			{
				this[ADGroupSchema.GroupMemberCount] = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x0006C513 File Offset: 0x0006A713
		// (set) Token: 0x0600199F RID: 6559 RVA: 0x0006C525 File Offset: 0x0006A725
		public int GroupExternalMemberCount
		{
			get
			{
				return (int)this[ADGroupSchema.GroupExternalMemberCount];
			}
			set
			{
				this[ADGroupSchema.GroupExternalMemberCount] = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0006C538 File Offset: 0x0006A738
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x0006C54A File Offset: 0x0006A74A
		internal string LinkedPartnerGroupId
		{
			get
			{
				return (string)this[ADGroupSchema.LinkedPartnerGroupId];
			}
			set
			{
				this[ADGroupSchema.LinkedPartnerGroupId] = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x0006C558 File Offset: 0x0006A758
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x0006C56A File Offset: 0x0006A76A
		internal string LinkedPartnerOrganizationId
		{
			get
			{
				return (string)this[ADGroupSchema.LinkedPartnerOrganizationId];
			}
			set
			{
				this[ADGroupSchema.LinkedPartnerOrganizationId] = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x0006C578 File Offset: 0x0006A778
		public RoleGroupType RoleGroupType
		{
			get
			{
				return (RoleGroupType)this[ADGroupSchema.RoleGroupType];
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x0006C58A File Offset: 0x0006A78A
		public string Description
		{
			get
			{
				return (string)this[ADGroupSchema.RoleGroupDescription];
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x0006C59C File Offset: 0x0006A79C
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x0006C5AE File Offset: 0x0006A7AE
		public ADObjectId RawManagedBy
		{
			get
			{
				return (ADObjectId)this[ADGroupSchema.RawManagedBy];
			}
			internal set
			{
				this[ADGroupSchema.RawManagedBy] = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0006C5BC File Offset: 0x0006A7BC
		// (set) Token: 0x060019A9 RID: 6569 RVA: 0x0006C5CE File Offset: 0x0006A7CE
		public MultiValuedProperty<ADObjectId> CoManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADGroupSchema.CoManagedBy];
			}
			internal set
			{
				this[ADGroupSchema.CoManagedBy] = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0006C5DC File Offset: 0x0006A7DC
		// (set) Token: 0x060019AB RID: 6571 RVA: 0x0006C5EE File Offset: 0x0006A7EE
		public MultiValuedProperty<ADObjectId> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADGroupSchema.ManagedBy];
			}
			internal set
			{
				this[ADGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x0006C5FC File Offset: 0x0006A7FC
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x0006C604 File Offset: 0x0006A804
		ADObjectId IADDistributionList.ManagedBy
		{
			get
			{
				return this.RawManagedBy;
			}
			set
			{
				this.RawManagedBy = value;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x0006C60D File Offset: 0x0006A80D
		// (set) Token: 0x060019AF RID: 6575 RVA: 0x0006C61F File Offset: 0x0006A81F
		public MultiValuedProperty<ADObjectId> Members
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADGroupSchema.Members];
			}
			set
			{
				this[ADGroupSchema.Members] = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x0006C62D File Offset: 0x0006A82D
		// (set) Token: 0x060019B1 RID: 6577 RVA: 0x0006C63F File Offset: 0x0006A83F
		public MemberUpdateType MemberJoinRestriction
		{
			get
			{
				return (MemberUpdateType)this[ADGroupSchema.MemberJoinRestriction];
			}
			set
			{
				this[ADGroupSchema.MemberJoinRestriction] = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x0006C652 File Offset: 0x0006A852
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x0006C664 File Offset: 0x0006A864
		public MemberUpdateType MemberDepartRestriction
		{
			get
			{
				return (MemberUpdateType)this[ADGroupSchema.MemberDepartRestriction];
			}
			set
			{
				this[ADGroupSchema.MemberDepartRestriction] = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0006C677 File Offset: 0x0006A877
		// (set) Token: 0x060019B5 RID: 6581 RVA: 0x0006C689 File Offset: 0x0006A889
		public bool ReportToManagerEnabled
		{
			get
			{
				return (bool)this[ADGroupSchema.ReportToManagerEnabled];
			}
			set
			{
				this[ADGroupSchema.ReportToManagerEnabled] = value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0006C69C File Offset: 0x0006A89C
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x0006C6AE File Offset: 0x0006A8AE
		public bool ReportToOriginatorEnabled
		{
			get
			{
				return (bool)this[ADGroupSchema.ReportToOriginatorEnabled];
			}
			set
			{
				this[ADGroupSchema.ReportToOriginatorEnabled] = value;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0006C6C1 File Offset: 0x0006A8C1
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x0006C6D3 File Offset: 0x0006A8D3
		public DeliveryReportsReceiver SendDeliveryReportsTo
		{
			get
			{
				return (DeliveryReportsReceiver)this[ADGroupSchema.SendDeliveryReportsTo];
			}
			set
			{
				this[ADGroupSchema.SendDeliveryReportsTo] = value;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x0006C6E6 File Offset: 0x0006A8E6
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x0006C6F8 File Offset: 0x0006A8F8
		public bool SendOofMessageToOriginatorEnabled
		{
			get
			{
				return (bool)this[ADGroupSchema.SendOofMessageToOriginatorEnabled];
			}
			set
			{
				this[ADGroupSchema.SendOofMessageToOriginatorEnabled] = value;
			}
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0006C70C File Offset: 0x0006A90C
		internal static object SendDeliveryReportsToGetter(IPropertyBag propertyBag)
		{
			bool? flag = (bool?)propertyBag[IADDistributionListSchema.ReportToManagerEnabled];
			bool? flag2 = (bool?)propertyBag[IADDistributionListSchema.ReportToOriginatorEnabled];
			if (flag ?? false)
			{
				return DeliveryReportsReceiver.Manager;
			}
			if (flag2 ?? false)
			{
				return DeliveryReportsReceiver.Originator;
			}
			return DeliveryReportsReceiver.None;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0006C780 File Offset: 0x0006A980
		internal static void SendDeliveryReportsToSetter(object value, IPropertyBag propertyBag)
		{
			switch ((DeliveryReportsReceiver)(value ?? DeliveryReportsReceiver.None))
			{
			case DeliveryReportsReceiver.Manager:
				propertyBag[IADDistributionListSchema.ReportToManagerEnabled] = true;
				propertyBag[IADDistributionListSchema.ReportToOriginatorEnabled] = null;
				return;
			case DeliveryReportsReceiver.Originator:
				propertyBag[IADDistributionListSchema.ReportToManagerEnabled] = null;
				propertyBag[IADDistributionListSchema.ReportToOriginatorEnabled] = true;
				return;
			}
			propertyBag[IADDistributionListSchema.ReportToManagerEnabled] = null;
			propertyBag[IADDistributionListSchema.ReportToOriginatorEnabled] = null;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0006C808 File Offset: 0x0006AA08
		internal static QueryFilter SendDeliveryReportsToFilterBuilder(SinglePropertyFilter filter)
		{
			DeliveryReportsReceiver deliveryReportsReceiver = (DeliveryReportsReceiver)ADObject.PropertyValueFromEqualityFilter(filter);
			switch (deliveryReportsReceiver)
			{
			case DeliveryReportsReceiver.None:
				return new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.NotEqual, ADGroupSchema.ReportToOriginatorEnabled, true),
					new ComparisonFilter(ComparisonOperator.NotEqual, ADGroupSchema.ReportToManagerEnabled, true)
				});
			case DeliveryReportsReceiver.Manager:
				return new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.ReportToManagerEnabled, true);
			case DeliveryReportsReceiver.Originator:
				return new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.ReportToOriginatorEnabled, true),
					new ComparisonFilter(ComparisonOperator.NotEqual, ADGroupSchema.ReportToManagerEnabled, true)
				});
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedPropertyValue(ADGroupSchema.SendDeliveryReportsTo.Name, deliveryReportsReceiver));
			}
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0006C8D0 File Offset: 0x0006AAD0
		internal static object IsSecurityPrincipalGetter(IPropertyBag propertyBag)
		{
			if (((MultiValuedProperty<string>)propertyBag[ADObjectSchema.ObjectClass]).Contains("user"))
			{
				return true;
			}
			return ((GroupTypeFlags)propertyBag[ADGroupSchema.GroupType] & GroupTypeFlags.SecurityEnabled) != GroupTypeFlags.None;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0006C924 File Offset: 0x0006AB24
		internal static QueryFilter IsSecurityPrincipalFilterBuilder(SinglePropertyFilter filter)
		{
			bool flag = (bool)ADObject.PropertyValueFromEqualityFilter(filter);
			uint num = 2147483648U;
			QueryFilter queryFilter = new OrFilter(new QueryFilter[]
			{
				new BitMaskAndFilter(ADGroupSchema.GroupType, (ulong)num),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADUser.MostDerivedClass)
			});
			if (!flag)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0006C97E File Offset: 0x0006AB7E
		internal static object HomeMtaServerIdGetter(IPropertyBag propertyBag)
		{
			return ADGroup.GetServerIdFromHomeMta((ADObjectId)propertyBag[ADRecipientSchema.HomeMTA]);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0006C998 File Offset: 0x0006AB98
		internal static ADObjectId GetServerIdFromHomeMta(ADObjectId homeMTA)
		{
			ADObjectId result;
			try
			{
				if (homeMTA == null || string.IsNullOrEmpty(homeMTA.DistinguishedName))
				{
					result = null;
				}
				else
				{
					if (homeMTA.Depth - homeMTA.DomainId.Depth > 8)
					{
						homeMTA = homeMTA.DescendantDN(8);
					}
					result = homeMTA;
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("HomeMtaServerId", ex.Message), ADGroupSchema.HomeMtaServerId, homeMTA), ex);
			}
			return result;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0006CA10 File Offset: 0x0006AC10
		internal static QueryFilter ManagedByFilterBuilder(SinglePropertyFilter filter)
		{
			QueryFilter result;
			if (filter is ComparisonFilter)
			{
				ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
				if (comparisonFilter.ComparisonOperator == ComparisonOperator.Equal)
				{
					result = new OrFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.RawManagedBy, comparisonFilter.PropertyValue),
						new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.CoManagedBy, comparisonFilter.PropertyValue)
					});
				}
				else
				{
					if (comparisonFilter.ComparisonOperator != ComparisonOperator.NotEqual)
					{
						throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
					}
					result = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.NotEqual, ADGroupSchema.RawManagedBy, comparisonFilter.PropertyValue),
						new ComparisonFilter(ComparisonOperator.NotEqual, ADGroupSchema.CoManagedBy, comparisonFilter.PropertyValue)
					});
				}
			}
			else
			{
				if (!(filter is ExistsFilter))
				{
					throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForPropertyMultiple(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter), typeof(ExistsFilter)));
				}
				ExistsFilter existsFilter = (ExistsFilter)filter;
				result = new OrFilter(new QueryFilter[]
				{
					new ExistsFilter(ADGroupSchema.RawManagedBy),
					new ExistsFilter(ADGroupSchema.CoManagedBy)
				});
			}
			return result;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0006CB4C File Offset: 0x0006AD4C
		internal static object ManagedByGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				ADObjectId adobjectId = propertyBag[ADGroupSchema.RawManagedBy] as ADObjectId;
				MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
				if (adobjectId != null)
				{
					multiValuedProperty.Add(adobjectId);
				}
				MultiValuedProperty<ADObjectId> multiValuedProperty2 = propertyBag[ADGroupSchema.CoManagedBy] as MultiValuedProperty<ADObjectId>;
				foreach (ADObjectId item in multiValuedProperty2)
				{
					if (!multiValuedProperty.Contains(item))
					{
						multiValuedProperty.Add(item);
					}
				}
				multiValuedProperty.ResetChangeTracking();
				result = multiValuedProperty;
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("ManagedBy", ex.Message), ADGroupSchema.ManagedBy, propertyBag[ADGroupSchema.RawManagedBy]), ex);
			}
			return result;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0006CC20 File Offset: 0x0006AE20
		internal static void ManagedBySetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)value;
			if (multiValuedProperty == null || multiValuedProperty.Count == 0)
			{
				propertyBag[ADGroupSchema.RawManagedBy] = null;
				propertyBag[ADGroupSchema.CoManagedBy] = null;
				return;
			}
			propertyBag[ADGroupSchema.RawManagedBy] = multiValuedProperty[0];
			MultiValuedProperty<ADObjectId> multiValuedProperty2 = new MultiValuedProperty<ADObjectId>();
			for (int i = 1; i < multiValuedProperty.Count; i++)
			{
				multiValuedProperty2.Add(multiValuedProperty[i]);
			}
			propertyBag[ADGroupSchema.CoManagedBy] = multiValuedProperty2;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0006CC9C File Offset: 0x0006AE9C
		internal static QueryFilter RoleGroupTypeFilterBuilder(SinglePropertyFilter filter)
		{
			RoleGroupType roleGroupType = (RoleGroupType)ADObject.PropertyValueFromEqualityFilter(filter);
			QueryFilter queryFilter = new ExistsFilter(ADGroupSchema.ForeignGroupSid);
			QueryFilter result = new ExistsFilter(ADGroupSchema.LinkedPartnerGroupAndOrganizationId);
			if (roleGroupType == RoleGroupType.Linked)
			{
				return queryFilter;
			}
			if (roleGroupType == RoleGroupType.PartnerLinked)
			{
				return result;
			}
			return new NotFilter(queryFilter);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0006CCE0 File Offset: 0x0006AEE0
		internal static object RoleGroupTypeGetter(IPropertyBag propertyBag)
		{
			SecurityIdentifier right = (SecurityIdentifier)propertyBag[ADGroupSchema.ForeignGroupSid];
			LinkedPartnerGroupInformation linkedPartnerGroupInformation = (LinkedPartnerGroupInformation)propertyBag[ADGroupSchema.LinkedPartnerGroupAndOrganizationId];
			if (null != right)
			{
				return RoleGroupType.Linked;
			}
			if (linkedPartnerGroupInformation != null)
			{
				return RoleGroupType.PartnerLinked;
			}
			return RoleGroupType.Standard;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0006CD30 File Offset: 0x0006AF30
		internal ADPagedReader<ADRawEntry> Expand(int pageSize, params PropertyDefinition[] properties)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MemberOfGroup, base.Id);
			return base.Session.FindPagedADRawEntry(null, QueryScope.SubTree, filter, null, pageSize, properties);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0006CD60 File Offset: 0x0006AF60
		ADPagedReader<ADRawEntry> IADDistributionList.Expand(int pageSize, params PropertyDefinition[] properties)
		{
			return this.Expand(pageSize, properties);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0006CD6C File Offset: 0x0006AF6C
		internal ADPagedReader<ADRecipient> Expand(int pageSize)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MemberOfGroup, base.Id);
			return base.Session.FindPaged(null, QueryScope.SubTree, filter, null, pageSize);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0006CD9B File Offset: 0x0006AF9B
		ADPagedReader<ADRecipient> IADDistributionList.Expand(int pageSize)
		{
			return this.Expand(pageSize);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0006CDA4 File Offset: 0x0006AFA4
		internal ADPagedReader<TEntry> Expand<TEntry>(int pageSize, params PropertyDefinition[] properties) where TEntry : MiniRecipient, new()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MemberOfGroup, base.Id);
			return base.Session.FindPagedMiniRecipient<TEntry>(null, QueryScope.SubTree, filter, null, pageSize, properties);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0006CDD4 File Offset: 0x0006AFD4
		ADPagedReader<TEntry> IADDistributionList.Expand<TEntry>(int pageSize, params PropertyDefinition[] properties)
		{
			return this.Expand<TEntry>(pageSize, properties);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0006CDE0 File Offset: 0x0006AFE0
		internal ADPagedReader<ADRecipient> ExpandGroupOnly(int pageSize)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADGroup.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADDynamicGroup.MostDerivedClass)
				}),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MemberOfGroup, base.Id)
			});
			return base.Session.FindPaged(null, QueryScope.SubTree, filter, null, pageSize);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0006CE58 File Offset: 0x0006B058
		internal bool ContainsMember(ADObjectId memberId, bool directOnly)
		{
			ADRecipient adrecipient = base.Session.Read(memberId);
			return adrecipient != null && adrecipient.IsMemberOf(base.Id, directOnly);
		}

		// Token: 0x04000B63 RID: 2915
		public const string SamAccountNameInvalidCharacters = "\"\\/[]:|<>+=;?,*\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f";

		// Token: 0x04000B64 RID: 2916
		private static readonly ADGroupSchema schema = ObjectSchema.GetInstance<ADGroupSchema>();

		// Token: 0x04000B65 RID: 2917
		internal static string MostDerivedClass = "group";
	}
}
