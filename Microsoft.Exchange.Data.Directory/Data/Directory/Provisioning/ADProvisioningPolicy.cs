using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000790 RID: 1936
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ADProvisioningPolicy : ADConfigurationObject, IProvisioningCacheInvalidation
	{
		// Token: 0x17002285 RID: 8837
		// (get) Token: 0x0600607E RID: 24702 RVA: 0x0014811D File Offset: 0x0014631D
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADProvisioningPolicy.schema;
			}
		}

		// Token: 0x17002286 RID: 8838
		// (get) Token: 0x0600607F RID: 24703 RVA: 0x00148124 File Offset: 0x00146324
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADProvisioningPolicy.MostDerivedClass;
			}
		}

		// Token: 0x17002287 RID: 8839
		// (get) Token: 0x06006080 RID: 24704 RVA: 0x0014812B File Offset: 0x0014632B
		internal virtual ICollection<Type> SupportedPresentationObjectTypes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002288 RID: 8840
		// (get) Token: 0x06006081 RID: 24705 RVA: 0x0014812E File Offset: 0x0014632E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17002289 RID: 8841
		// (get) Token: 0x06006082 RID: 24706 RVA: 0x00148135 File Offset: 0x00146335
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				if (base.GetType() == typeof(ADProvisioningPolicy))
				{
					return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADProvisioningPolicy.MostDerivedClass);
				}
				return base.ImplicitFilter;
			}
		}

		// Token: 0x1700228A RID: 8842
		// (get) Token: 0x06006083 RID: 24707 RVA: 0x00148165 File Offset: 0x00146365
		internal virtual IEnumerable<IProvisioningTemplate> ProvisioningTemplateRules
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700228B RID: 8843
		// (get) Token: 0x06006084 RID: 24708 RVA: 0x00148168 File Offset: 0x00146368
		internal virtual IEnumerable<IProvisioningEnforcement> ProvisioningEnforcementRules
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x0014816C File Offset: 0x0014636C
		internal virtual void ProvisionCustomDefaultProperties(IConfigurable provisionedDefault)
		{
			if (provisionedDefault == null)
			{
				throw new ArgumentNullException("provisionedDefault");
			}
			if (this.SupportedPresentationObjectTypes == null || !this.SupportedPresentationObjectTypes.Contains(provisionedDefault.GetType()))
			{
				throw new InvalidOperationException(DirectoryStrings.ErrorPolicyDontSupportedPresentationObject(provisionedDefault.GetType(), base.GetType()));
			}
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x001481C0 File Offset: 0x001463C0
		internal virtual ProvisioningValidationError[] ProvisioningCustomValidate(IConfigurable roPresentationObject)
		{
			if (roPresentationObject == null)
			{
				throw new ArgumentNullException("roPresentationObject");
			}
			if (this.SupportedPresentationObjectTypes == null || !this.SupportedPresentationObjectTypes.Contains(roPresentationObject.GetType()))
			{
				throw new InvalidOperationException(DirectoryStrings.ErrorPolicyDontSupportedPresentationObject(roPresentationObject.GetType(), base.GetType()));
			}
			return null;
		}

		// Token: 0x1700228C RID: 8844
		// (get) Token: 0x06006088 RID: 24712 RVA: 0x0014821B File Offset: 0x0014641B
		public ProvisioningPolicyType PolicyType
		{
			get
			{
				return (ProvisioningPolicyType)this[ADProvisioningPolicySchema.PolicyType];
			}
		}

		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x06006089 RID: 24713 RVA: 0x0014822D File Offset: 0x0014642D
		// (set) Token: 0x0600608A RID: 24714 RVA: 0x0014823F File Offset: 0x0014643F
		public MultiValuedProperty<string> TargetObjects
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADProvisioningPolicySchema.TargetObjects];
			}
			private set
			{
				this[ADProvisioningPolicySchema.TargetObjects] = value;
			}
		}

		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x0600608B RID: 24715 RVA: 0x0014824D File Offset: 0x0014644D
		// (set) Token: 0x0600608C RID: 24716 RVA: 0x0014825F File Offset: 0x0014645F
		public MultiValuedProperty<ADObjectId> Scopes
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADProvisioningPolicySchema.Scopes];
			}
			internal set
			{
				this[ADProvisioningPolicySchema.Scopes] = value;
			}
		}

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x0600608D RID: 24717 RVA: 0x0014826D File Offset: 0x0014646D
		// (set) Token: 0x0600608E RID: 24718 RVA: 0x00148275 File Offset: 0x00146475
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x00148280 File Offset: 0x00146480
		internal override void StampPersistableDefaultValues()
		{
			if (this.SupportedPresentationObjectTypes != null && !base.IsModified(ADProvisioningPolicySchema.TargetObjects) && this.TargetObjects.Count == 0)
			{
				foreach (Type poType in this.SupportedPresentationObjectTypes)
				{
					this.TargetObjects.Add(ProvisioningHelper.GetProvisioningObjectTag(poType));
				}
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x00148300 File Offset: 0x00146500
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			if (base.OrganizationId == null || base.OrganizationId.Equals(OrganizationId.ForestWideOrgId) || base.ObjectState == ObjectState.Unchanged)
			{
				return false;
			}
			orgId = base.OrganizationId;
			keys = new Guid[1];
			keys[0] = CannedProvisioningCacheKeys.EnforcementProvisioningPolicies;
			return true;
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x00148361 File Offset: 0x00146561
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x040040D3 RID: 16595
		private static ADProvisioningPolicySchema schema = ObjectSchema.GetInstance<ADProvisioningPolicySchema>();

		// Token: 0x040040D4 RID: 16596
		internal static readonly ADObjectId RdnContainer = new ADObjectId("CN=Provisioning Policy Container");

		// Token: 0x040040D5 RID: 16597
		internal static readonly string MostDerivedClass = "msExchProvisioningPolicy";
	}
}
