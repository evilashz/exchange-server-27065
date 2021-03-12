using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A18 RID: 2584
	internal class PolicyStorageSchema : UnifiedPolicyStorageBaseSchema
	{
		// Token: 0x04004C81 RID: 19585
		public static ADPropertyDefinition EnforcementMode = new ADPropertyDefinition("Mode", ExchangeObjectVersion.Exchange2012, typeof(Mode), "msExchIMAP4Settings", ADPropertyDefinitionFlags.None, Mode.Enforce, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C82 RID: 19586
		public static ADPropertyDefinition Scenario = new ADPropertyDefinition("PolicyScenario", ExchangeObjectVersion.Exchange2012, typeof(PolicyScenario), "msExchPOP3Settings", ADPropertyDefinitionFlags.None, PolicyScenario.Retention, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C83 RID: 19587
		public static ADPropertyDefinition DefaultRuleId = new ADPropertyDefinition("DefaultRuleId", ExchangeObjectVersion.Exchange2012, typeof(Guid?), "msExchCanaryData1", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C84 RID: 19588
		public static ADPropertyDefinition Comments = new ADPropertyDefinition("Comments", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchDefaultPublicFolderMailbox", ADPropertyDefinitionFlags.None, string.Empty, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C85 RID: 19589
		public static ADPropertyDefinition Description = new ADPropertyDefinition("Description", ExchangeObjectVersion.Exchange2012, typeof(string), "adminDescription", ADPropertyDefinitionFlags.None, string.Empty, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C86 RID: 19590
		public static readonly ADPropertyDefinition IsEnabled = new ADPropertyDefinition("IsEnabled", ExchangeObjectVersion.Exchange2012, typeof(bool), "msExchHideFromAddressLists", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C87 RID: 19591
		public static readonly ADPropertyDefinition CreatedBy = new ADPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchShadowPostalCode", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C88 RID: 19592
		public static readonly ADPropertyDefinition LastModifiedBy = new ADPropertyDefinition("LastModifiedBy", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchShadowPhysicalDeliveryOfficeName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
