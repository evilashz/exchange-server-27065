using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A1C RID: 2588
	internal class ScopeStorageSchema : UnifiedPolicyStorageBaseSchema
	{
		// Token: 0x04004CA0 RID: 19616
		public static ADPropertyDefinition Scope = new ADPropertyDefinition("AppliedScope", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchScope", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.NonADProperty, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CA1 RID: 19617
		public static ADPropertyDefinition EnforcementMode = new ADPropertyDefinition("Mode", ExchangeObjectVersion.Exchange2012, typeof(Mode), "msExchIMAP4Settings", ADPropertyDefinitionFlags.NonADProperty, Mode.Enforce, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
