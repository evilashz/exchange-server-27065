using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000792 RID: 1938
	[Serializable]
	public abstract class TemplateProvisioningPolicy : ADProvisioningPolicy
	{
		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x06006094 RID: 24724 RVA: 0x00148398 File Offset: 0x00146598
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass),
					new BitMaskAndFilter(ADProvisioningPolicySchema.PolicyType, 1UL)
				});
			}
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x001483D5 File Offset: 0x001465D5
		internal override void StampPersistableDefaultValues()
		{
			this[ADProvisioningPolicySchema.PolicyType] = ProvisioningPolicyType.Template;
			base.StampPersistableDefaultValues();
		}

		// Token: 0x17002291 RID: 8849
		// (get) Token: 0x06006096 RID: 24726 RVA: 0x001483EE File Offset: 0x001465EE
		internal sealed override IEnumerable<IProvisioningEnforcement> ProvisioningEnforcementRules
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x001483F1 File Offset: 0x001465F1
		internal sealed override ProvisioningValidationError[] ProvisioningCustomValidate(IConfigurable roPresentationObject)
		{
			return base.ProvisioningCustomValidate(roPresentationObject);
		}

		// Token: 0x040040D6 RID: 16598
		internal static readonly ADObjectId RdnTemplateContainer = ADProvisioningPolicy.RdnContainer.GetChildId("Template");
	}
}
