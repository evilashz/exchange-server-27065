using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000794 RID: 1940
	[Serializable]
	public abstract class EnforcementProvisioningPolicy : ADProvisioningPolicy
	{
		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x0600609B RID: 24731 RVA: 0x00148420 File Offset: 0x00146620
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass),
					new BitMaskAndFilter(ADProvisioningPolicySchema.PolicyType, 2UL)
				});
			}
		}

		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x0600609C RID: 24732 RVA: 0x0014845D File Offset: 0x0014665D
		internal override ADObjectSchema Schema
		{
			get
			{
				return EnforcementProvisioningPolicy.schema;
			}
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x00148464 File Offset: 0x00146664
		internal override void StampPersistableDefaultValues()
		{
			this[ADProvisioningPolicySchema.PolicyType] = ProvisioningPolicyType.Enforcement;
			base.StampPersistableDefaultValues();
		}

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x0600609E RID: 24734 RVA: 0x0014847D File Offset: 0x0014667D
		internal sealed override IEnumerable<IProvisioningTemplate> ProvisioningTemplateRules
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600609F RID: 24735 RVA: 0x00148480 File Offset: 0x00146680
		internal sealed override void ProvisionCustomDefaultProperties(IConfigurable provisionedDefault)
		{
			base.ProvisionCustomDefaultProperties(provisionedDefault);
		}

		// Token: 0x040040D7 RID: 16599
		private static EnforcementProvisioningPolicySchema schema = ObjectSchema.GetInstance<EnforcementProvisioningPolicySchema>();

		// Token: 0x040040D8 RID: 16600
		internal static readonly ADObjectId RdnEnforcementContainer = ADProvisioningPolicy.RdnContainer.GetChildId("Enforcement");
	}
}
