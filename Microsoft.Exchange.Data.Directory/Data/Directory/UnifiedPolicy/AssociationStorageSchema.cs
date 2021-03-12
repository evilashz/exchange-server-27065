using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A12 RID: 2578
	internal class AssociationStorageSchema : UnifiedPolicyStorageBaseSchema
	{
		// Token: 0x04004C68 RID: 19560
		public static ADPropertyDefinition Type = new ADPropertyDefinition("AssociationType", ExchangeObjectVersion.Exchange2012, typeof(AssociationType), "msExchIMAP4Settings", ADPropertyDefinitionFlags.None, AssociationType.SPSiteCollection, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C69 RID: 19561
		public static ADPropertyDefinition AllowOverride = new ADPropertyDefinition("AllowOverride", ExchangeObjectVersion.Exchange2012, typeof(bool), "msExchPOP3Settings", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C6A RID: 19562
		public static ADPropertyDefinition DefaultPolicyId = new ADPropertyDefinition("DefaultPolicyId", ExchangeObjectVersion.Exchange2012, typeof(Guid?), "msExchCanaryData1", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C6B RID: 19563
		public static ADPropertyDefinition Scope = new ADPropertyDefinition("Scope", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchTenantCountry", ADPropertyDefinitionFlags.Mandatory, string.Empty, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C6C RID: 19564
		public static ADPropertyDefinition PolicyIds = new ADPropertyDefinition("PolicyIds", ExchangeObjectVersion.Exchange2012, typeof(Guid), "msExchEwsWellKnownApplicationPolicies", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C6D RID: 19565
		public static ADPropertyDefinition Comments = new ADPropertyDefinition("Comments", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchDefaultPublicFolderMailbox", ADPropertyDefinitionFlags.None, string.Empty, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C6E RID: 19566
		public static ADPropertyDefinition Description = new ADPropertyDefinition("Description", ExchangeObjectVersion.Exchange2012, typeof(string), "adminDescription", ADPropertyDefinitionFlags.None, string.Empty, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024)
		}, PropertyDefinitionConstraint.None, null, null);
	}
}
