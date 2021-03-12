using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A1F RID: 2591
	internal class UnifiedPolicySettingStatusSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04004CA8 RID: 19624
		public static readonly ADPropertyDefinition SettingType = new ADPropertyDefinition("SettingType", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchUnifiedPolicySettingType", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CA9 RID: 19625
		public static readonly ADPropertyDefinition ObjectId = new ADPropertyDefinition("ObjectId", ExchangeObjectVersion.Exchange2012, typeof(Guid), "msExchEdgeSyncSourceGuid", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.NonADProperty, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CAA RID: 19626
		public static readonly ADPropertyDefinition ParentObjectId = new ADPropertyDefinition("ParentObjectId", ExchangeObjectVersion.Exchange2012, typeof(Guid?), "msExchParentObjectId", ADPropertyDefinitionFlags.NonADProperty, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CAB RID: 19627
		public static readonly ADPropertyDefinition Container = new ADPropertyDefinition("Container", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchStatusContainer", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CAC RID: 19628
		public static readonly ADPropertyDefinition ErrorCode = new ADPropertyDefinition("ErrorCode", ExchangeObjectVersion.Exchange2012, typeof(int), "msExchangeUnifiedPolicyErrorCode", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.NonADProperty, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CAD RID: 19629
		public static readonly ADPropertyDefinition ErrorMessage = new ADPropertyDefinition("ErrorMessage", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchangeUnifiedPolicyErrorMessage", ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CAE RID: 19630
		public static readonly ADPropertyDefinition ObjectVersion = new ADPropertyDefinition("ObjectVersion", ExchangeObjectVersion.Exchange2012, typeof(Guid), "msExchangeUnifiedPolicyObjectVersion", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.NonADProperty, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CAF RID: 19631
		public static readonly ADPropertyDefinition WhenProcessedUTC = new ADPropertyDefinition("WhenProcessedUTC", ExchangeObjectVersion.Exchange2012, typeof(DateTime), "msExchUnifiedPolicyStatusWhenProcessed", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.NonADProperty, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CB0 RID: 19632
		public static readonly ADPropertyDefinition ObjectStatus = new ADPropertyDefinition("ObjectStatus", ExchangeObjectVersion.Exchange2012, typeof(StatusMode), "msExchangeUnifiedPolicyObjectStatus", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.NonADProperty, StatusMode.Active, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004CB1 RID: 19633
		public static readonly ADPropertyDefinition AdditionalDiagnostics = new ADPropertyDefinition("AdditionalDiagnostics", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchStatusAdditionalDiagnostics", ADPropertyDefinitionFlags.NonADProperty, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
