using System;
using System.Resources;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000575 RID: 1397
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class RoleAssignmentPolicy : MailboxPolicy, IProvisioningCacheInvalidation
	{
		// Token: 0x06003E86 RID: 16006 RVA: 0x000ED0F5 File Offset: 0x000EB2F5
		internal static QueryFilter IsDefaultFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(RoleAssignmentPolicySchema.Flags, 1UL));
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x000ED10C File Offset: 0x000EB30C
		internal static object DescriptionGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[RoleAssignmentPolicySchema.RawDescription];
			if (text.StartsWith(RoleAssignmentPolicy.LocalizedRoleAssignmentPolicyDescriptionPrefix))
			{
				try
				{
					text = RoleAssignmentPolicy.resourceManager.GetString(text.Substring(1));
				}
				catch (MissingManifestResourceException)
				{
				}
			}
			return text;
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x000ED160 File Offset: 0x000EB360
		internal static void DescriptionSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[RoleAssignmentPolicySchema.RawDescription] = value;
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06003E89 RID: 16009 RVA: 0x000ED16E File Offset: 0x000EB36E
		internal override ADObjectSchema Schema
		{
			get
			{
				return RoleAssignmentPolicy.schema;
			}
		}

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000ED175 File Offset: 0x000EB375
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RoleAssignmentPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06003E8B RID: 16011 RVA: 0x000ED17C File Offset: 0x000EB37C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return RoleAssignmentPolicySchema.Exchange2009_R4;
			}
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x000ED184 File Offset: 0x000EB384
		internal override bool CheckForAssociatedUsers()
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, base.Id.DistinguishedName),
				new ExistsFilter(RoleAssignmentPolicySchema.AssociatedUsers)
			});
			RoleAssignmentPolicy[] array = base.Session.Find<RoleAssignmentPolicy>(null, QueryScope.SubTree, filter, null, 1);
			return array != null && array.Length > 0;
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000ED1E1 File Offset: 0x000EB3E1
		// (set) Token: 0x06003E8E RID: 16014 RVA: 0x000ED1F3 File Offset: 0x000EB3F3
		public override bool IsDefault
		{
			get
			{
				return (bool)this[RoleAssignmentPolicySchema.IsDefault];
			}
			set
			{
				this[RoleAssignmentPolicySchema.IsDefault] = value;
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06003E8F RID: 16015 RVA: 0x000ED206 File Offset: 0x000EB406
		// (set) Token: 0x06003E90 RID: 16016 RVA: 0x000ED218 File Offset: 0x000EB418
		public string Description
		{
			get
			{
				return (string)this[RoleAssignmentPolicySchema.Description];
			}
			set
			{
				this[RoleAssignmentPolicySchema.Description] = value;
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x000ED226 File Offset: 0x000EB426
		// (set) Token: 0x06003E92 RID: 16018 RVA: 0x000ED238 File Offset: 0x000EB438
		public MultiValuedProperty<ADObjectId> RoleAssignments
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[RoleAssignmentPolicySchema.RoleAssignments];
			}
			private set
			{
				this[RoleAssignmentPolicySchema.RoleAssignments] = value;
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x000ED246 File Offset: 0x000EB446
		// (set) Token: 0x06003E94 RID: 16020 RVA: 0x000ED258 File Offset: 0x000EB458
		public MultiValuedProperty<ADObjectId> AssignedRoles
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[RoleAssignmentPolicySchema.AssignedRoles];
			}
			private set
			{
				this[RoleAssignmentPolicySchema.AssignedRoles] = value;
			}
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x000ED268 File Offset: 0x000EB468
		internal void PopulateRoles(Result<ExchangeRoleAssignment>[] roleAssignmentResults)
		{
			if (roleAssignmentResults != null)
			{
				foreach (Result<ExchangeRoleAssignment> result in roleAssignmentResults)
				{
					ExchangeRoleAssignment data = result.Data;
					this.RoleAssignments.Add(data.Id);
					if (data.Role != null && !this.AssignedRoles.Contains(data.Role))
					{
						this.AssignedRoles.Add(data.Role);
					}
				}
			}
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000ED2DA File Offset: 0x000EB4DA
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000ED2E4 File Offset: 0x000EB4E4
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			bool flag = false;
			if (base.OrganizationId == null)
			{
				return flag;
			}
			if (base.ObjectState == ObjectState.New || base.ObjectState == ObjectState.Deleted)
			{
				flag = true;
			}
			if (!flag && base.ObjectState == ObjectState.Changed && (base.IsChanged(RoleAssignmentPolicySchema.AssociatedUsers) || base.IsChanged(RoleAssignmentPolicySchema.Flags) || base.IsChanged(RoleAssignmentPolicySchema.IsDefault) || base.IsChanged(RoleAssignmentPolicySchema.RawDescription) || base.IsChanged(RoleAssignmentPolicySchema.Description) || base.IsChanged(RoleAssignmentPolicySchema.RoleAssignments) || base.IsChanged(RoleAssignmentPolicySchema.AssignedRoles)))
			{
				flag = true;
			}
			if (flag)
			{
				orgId = base.OrganizationId;
				keys = new Guid[2];
				keys[0] = CannedProvisioningCacheKeys.MailboxRoleAssignmentPolicyCacheKey;
				keys[1] = CannedProvisioningCacheKeys.DefaultRoleAssignmentPolicyId;
			}
			return flag;
		}

		// Token: 0x04002A4A RID: 10826
		public static readonly string DefaultRoleAssignmentPolicyName = "Default Role Assignment Policy";

		// Token: 0x04002A4B RID: 10827
		private static readonly string LocalizedRoleAssignmentPolicyDescriptionPrefix = "%RoleAssignmentPolicyDescription_";

		// Token: 0x04002A4C RID: 10828
		public static readonly string PrecannedRoleAssignmentPolicyDescription = RoleAssignmentPolicy.LocalizedRoleAssignmentPolicyDescriptionPrefix + "Default";

		// Token: 0x04002A4D RID: 10829
		private static ExchangeResourceManager resourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.Directory.Strings", typeof(DirectoryStrings).Assembly);

		// Token: 0x04002A4E RID: 10830
		internal static readonly ADObjectId RdnContainer = new ADObjectId("CN=Policies,CN=RBAC");

		// Token: 0x04002A4F RID: 10831
		private static RoleAssignmentPolicySchema schema = ObjectSchema.GetInstance<RoleAssignmentPolicySchema>();

		// Token: 0x04002A50 RID: 10832
		private static string mostDerivedClass = "msExchRBACPolicy";
	}
}
