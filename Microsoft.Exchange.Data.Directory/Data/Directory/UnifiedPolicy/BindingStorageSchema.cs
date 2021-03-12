using System;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A16 RID: 2582
	internal class BindingStorageSchema : UnifiedPolicyStorageBaseSchema
	{
		// Token: 0x04004C79 RID: 19577
		public static ADPropertyDefinition PolicyId = new ADPropertyDefinition("PolicyId", ExchangeObjectVersion.Exchange2012, typeof(Guid), "msExchBindingPolicyId", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.NonADProperty, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C7A RID: 19578
		public static ADPropertyDefinition Scopes = new ADPropertyDefinition("Scopes", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchBindingScopes", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.NonADProperty, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C7B RID: 19579
		public static ADPropertyDefinition DeletedScopes = new ADPropertyDefinition("_DELETED_Scopes", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchBindingDeletedScopes", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.NonADProperty, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C7C RID: 19580
		public static ADPropertyDefinition AppliedScopes = new ADPropertyDefinition("AppliedScopes", ExchangeObjectVersion.Exchange2012, typeof(ScopeStorage), "msExchBindingAppliedScopes", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.NonADProperty, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C7D RID: 19581
		public static ADPropertyDefinition RemovedScopes = new ADPropertyDefinition("_DELETED_AppliedScopes", ExchangeObjectVersion.Exchange2012, typeof(ScopeStorage), "msExchBindingRemovedScopes", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.NonADProperty, null, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 1024)
		}, PropertyDefinitionConstraint.None, null, null);
	}
}
