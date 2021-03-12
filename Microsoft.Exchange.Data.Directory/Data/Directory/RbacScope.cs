using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200017A RID: 378
	[Serializable]
	internal class RbacScope : ADScope
	{
		// Token: 0x06001040 RID: 4160 RVA: 0x0004ED98 File Offset: 0x0004CF98
		public RbacScope(ScopeType scopeType, ManagementScope managementScope)
		{
			this.scopeType = scopeType;
			this.managementScope = managementScope;
			switch (scopeType)
			{
			case ScopeType.CustomRecipientScope:
			case ScopeType.CustomConfigScope:
			case ScopeType.PartnerDelegatedTenantScope:
			case ScopeType.ExclusiveRecipientScope:
			case ScopeType.ExclusiveConfigScope:
				if (managementScope == null)
				{
					throw new ArgumentNullException("managementScope");
				}
				if (managementScope.QueryFilter == null)
				{
					throw new ArgumentException("managementScope.QueryFilter");
				}
				return;
			}
			if (managementScope != null)
			{
				throw new ArgumentException("managementScope");
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0004EE14 File Offset: 0x0004D014
		public RbacScope(ScopeType scopeType, ADObjectId ouId)
		{
			this.scopeType = scopeType;
			this.ouId = ouId;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004EE2A File Offset: 0x0004D02A
		public RbacScope(ScopeType scopeType)
		{
			this.scopeType = scopeType;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004EE39 File Offset: 0x0004D039
		public RbacScope(ScopeType scopeType, ISecurityAccessToken securityAccessToken)
		{
			this.scopeType = scopeType;
			this.securityAccessToken = securityAccessToken;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0004EE4F File Offset: 0x0004D04F
		public RbacScope(ScopeType scopeType, ManagementScope managementScope, bool isFromEndUserRole) : this(scopeType, managementScope)
		{
			this.isFromEndUserRole = isFromEndUserRole;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0004EE60 File Offset: 0x0004D060
		public RbacScope(ScopeType scopeType, ADObjectId ouId, bool isFromEndUserRole) : this(scopeType, ouId)
		{
			this.isFromEndUserRole = isFromEndUserRole;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004EE71 File Offset: 0x0004D071
		public RbacScope(ScopeType scopeType, bool isFromEndUserRole) : this(scopeType)
		{
			this.isFromEndUserRole = isFromEndUserRole;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004EE81 File Offset: 0x0004D081
		public RbacScope(ScopeType scopeType, ISecurityAccessToken securityAccessToken, bool isFromEndUserRole) : this(scopeType, securityAccessToken)
		{
			this.isFromEndUserRole = isFromEndUserRole;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0004EE92 File Offset: 0x0004D092
		internal bool IsFromEndUserRole
		{
			get
			{
				return this.isFromEndUserRole;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0004EE9A File Offset: 0x0004D09A
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x0004EEA2 File Offset: 0x0004D0A2
		internal QueryFilter SelfFilter { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0004EEAB File Offset: 0x0004D0AB
		// (set) Token: 0x0600104C RID: 4172 RVA: 0x0004EEB3 File Offset: 0x0004D0B3
		internal ADObjectId SelfRoot { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0004EEBC File Offset: 0x0004D0BC
		public ScopeType ScopeType
		{
			get
			{
				return this.scopeType;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0004EEC4 File Offset: 0x0004D0C4
		public ScopeRestrictionType ScopeRestrictionType
		{
			get
			{
				if (this.managementScope != null)
				{
					return this.managementScope.ScopeRestrictionType;
				}
				return ScopeRestrictionType.NotApplicable;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0004EEDB File Offset: 0x0004D0DB
		public bool Exclusive
		{
			get
			{
				return this.scopeType == ScopeType.ExclusiveRecipientScope || this.scopeType == ScopeType.ExclusiveConfigScope;
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0004EEF3 File Offset: 0x0004D0F3
		internal static bool IsScopeTypeSmaller(ScopeType a, ScopeType b)
		{
			return RbacScope.IsScopeTypeSmaller(a, b, false);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0004EF00 File Offset: 0x0004D100
		internal static bool IsScopeTypeSmaller(ScopeType a, ScopeType b, bool hiddenFromGAL)
		{
			switch (a)
			{
			case ScopeType.None:
				if (hiddenFromGAL)
				{
					return b != ScopeType.None && b != ScopeType.MyGAL;
				}
				return b != ScopeType.None;
			case ScopeType.NotApplicable:
			case ScopeType.Organization:
			case ScopeType.OrganizationConfig:
				return false;
			case ScopeType.MyGAL:
				if (hiddenFromGAL)
				{
					return b != ScopeType.None && b != ScopeType.MyGAL;
				}
				return b == ScopeType.Organization;
			case ScopeType.Self:
			case ScopeType.MyDirectReports:
			case ScopeType.MyDistributionGroups:
			case ScopeType.MyExecutive:
			case ScopeType.MailboxICanDelegate:
				return b == ScopeType.Organization || (!hiddenFromGAL && b == ScopeType.MyGAL);
			case ScopeType.OU:
			case ScopeType.CustomRecipientScope:
			case ScopeType.ExclusiveRecipientScope:
				return b == ScopeType.Organization;
			case ScopeType.CustomConfigScope:
			case ScopeType.PartnerDelegatedTenantScope:
			case ScopeType.ExclusiveConfigScope:
				return b == ScopeType.OrganizationConfig;
			default:
				return false;
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0004EFA2 File Offset: 0x0004D1A2
		internal bool IsScopeTypeSmallerOrEqualThan(ScopeType scopeType)
		{
			return scopeType == this.scopeType || RbacScope.IsScopeTypeSmaller(this.scopeType, scopeType);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0004EFBB File Offset: 0x0004D1BB
		internal bool IsScopeTypeSmallerThan(ScopeType scopeType)
		{
			return this.IsScopeTypeSmallerThan(scopeType, false);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0004EFC5 File Offset: 0x0004D1C5
		internal bool IsScopeTypeSmallerThan(ScopeType scopeType, bool hiddenFromGAL)
		{
			return RbacScope.IsScopeTypeSmaller(this.scopeType, scopeType, hiddenFromGAL);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0004F074 File Offset: 0x0004D274
		internal void PopulateRootAndFilter(OrganizationId organizationId, IReadOnlyPropertyBag propertyBag)
		{
			if (this.Root != null || this.Filter != null)
			{
				return;
			}
			if (this.isFromEndUserRole && propertyBag == null)
			{
				throw new ArgumentNullException("propertyBag");
			}
			if (organizationId != null)
			{
				this.SelfRoot = organizationId.OrganizationalUnit;
			}
			if (propertyBag != null)
			{
				this.SelfFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, propertyBag[ADObjectSchema.Id]);
			}
			switch (this.scopeType)
			{
			case ScopeType.None:
				this.Root = null;
				this.Filter = ADScope.NoObjectFilter;
				return;
			case ScopeType.NotApplicable:
				this.Root = null;
				this.Filter = null;
				return;
			case ScopeType.Organization:
				this.Root = organizationId.OrganizationalUnit;
				this.Filter = null;
				return;
			case ScopeType.MyGAL:
			{
				AddressBookBase globalAddressList = this.GetGlobalAddressList(organizationId);
				this.Root = organizationId.OrganizationalUnit;
				if (globalAddressList == null)
				{
					this.Filter = ADScope.NoObjectFilter;
					return;
				}
				this.Filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, globalAddressList.Id);
				return;
			}
			case ScopeType.Self:
				this.Root = organizationId.OrganizationalUnit;
				this.Filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, propertyBag[ADObjectSchema.Id]);
				return;
			case ScopeType.MyDirectReports:
				this.Root = organizationId.OrganizationalUnit;
				this.Filter = new ComparisonFilter(ComparisonOperator.Equal, ADOrgPersonSchema.Manager, propertyBag[ADObjectSchema.Id]);
				return;
			case ScopeType.OU:
				this.Root = this.ouId;
				this.Filter = null;
				return;
			case ScopeType.CustomRecipientScope:
			case ScopeType.CustomConfigScope:
			case ScopeType.PartnerDelegatedTenantScope:
			case ScopeType.ExclusiveRecipientScope:
			case ScopeType.ExclusiveConfigScope:
				this.Root = this.managementScope.RecipientRoot;
				this.Filter = this.managementScope.QueryFilter;
				return;
			case ScopeType.MyDistributionGroups:
			{
				this.Root = organizationId.OrganizationalUnit;
				QueryFilter[] array = new QueryFilter[3];
				array[0] = new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.ManagedBy, propertyBag[ADObjectSchema.Id]);
				array[1] = new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.CoManagedBy, propertyBag[ADObjectSchema.Id]);
				array[2] = new CSharpFilter<IReadOnlyPropertyBag>(delegate(IReadOnlyPropertyBag obj)
				{
					ADGroup adgroup = obj as ADGroup;
					return adgroup != null && adgroup.IsExecutingUserGroupOwner;
				});
				this.Filter = new OrFilter(array);
				return;
			}
			case ScopeType.MyExecutive:
				break;
			case ScopeType.OrganizationConfig:
				this.Root = organizationId.ConfigurationUnit;
				this.Filter = null;
				return;
			case ScopeType.MailboxICanDelegate:
			{
				this.Root = organizationId.OrganizationalUnit;
				QueryFilter[] array2 = new QueryFilter[2];
				array2[0] = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MasterAccountSid, this.securityAccessToken.UserSid);
				array2[1] = new CSharpFilter<IReadOnlyPropertyBag>(delegate(IReadOnlyPropertyBag obj)
				{
					RawSecurityDescriptor rawSecurityDescriptor = ((ADObject)obj).ReadSecurityDescriptor();
					if (rawSecurityDescriptor != null)
					{
						using (AuthzContextHandle authzContext = AuthzAuthorization.GetAuthzContext(new SecurityIdentifier(this.securityAccessToken.UserSid), false))
						{
							bool[] array3 = AuthzAuthorization.CheckExtendedRights(authzContext, rawSecurityDescriptor, new Guid[]
							{
								WellKnownGuid.PersonalInfoPropSetGuid
							}, null, AccessMask.WriteProp);
							return array3[0];
						}
						return false;
					}
					return false;
				});
				this.Filter = new OrFilter(array2);
				return;
			}
			default:
				this.Root = null;
				this.Filter = ADScope.NoObjectFilter;
				break;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0004F320 File Offset: 0x0004D520
		private AddressBookBase GetGlobalAddressList(OrganizationId organizationId)
		{
			AddressBookBase globalAddressList;
			using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(this.securityAccessToken, AuthzFlags.AuthzSkipTokenGroups))
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false);
				globalAddressList = AddressBookBase.GetGlobalAddressList(clientSecurityContext, DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 512, "GetGlobalAddressList", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\RbacScope.cs"), DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, ConfigScopes.TenantSubTree, 513, "GetGlobalAddressList", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\RbacScope.cs"), true);
			}
			return globalAddressList;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0004F3A8 File Offset: 0x0004D5A8
		internal bool IsPresentInCollection(IList<ADScope> collection)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				RbacScope rbacScope = (RbacScope)collection[i];
				if (this.ScopeType == rbacScope.ScopeType && this.managementScope == rbacScope.managementScope && ADObjectId.Equals(this.ouId, rbacScope.ouId))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000950 RID: 2384
		private readonly bool isFromEndUserRole;

		// Token: 0x04000951 RID: 2385
		private ScopeType scopeType;

		// Token: 0x04000952 RID: 2386
		private ADObjectId ouId;

		// Token: 0x04000953 RID: 2387
		private ManagementScope managementScope;

		// Token: 0x04000954 RID: 2388
		private ISecurityAccessToken securityAccessToken;
	}
}
