using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000749 RID: 1865
	internal class ResubmitRequestSchema : ObjectSchema
	{
		// Token: 0x04003CE0 RID: 15584
		public static readonly SimpleProviderPropertyDefinition ResubmitRequestIdentity = new SimpleProviderPropertyDefinition("ResubmitRequestIdentity", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003CE1 RID: 15585
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.WriteOnce, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003CE2 RID: 15586
		public static readonly SimpleProviderPropertyDefinition Destination = new SimpleProviderPropertyDefinition("Destination", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.WriteOnce, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003CE3 RID: 15587
		public static readonly SimpleProviderPropertyDefinition StartTime = new SimpleProviderPropertyDefinition("StartTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003CE4 RID: 15588
		public static readonly SimpleProviderPropertyDefinition EndTime = new SimpleProviderPropertyDefinition("EndTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003CE5 RID: 15589
		public static readonly SimpleProviderPropertyDefinition CreationTime = new SimpleProviderPropertyDefinition("CreationTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003CE6 RID: 15590
		public static readonly SimpleProviderPropertyDefinition State = new SimpleProviderPropertyDefinition("State", ExchangeObjectVersion.Exchange2010, typeof(ResubmitRequestState), PropertyDefinitionFlags.WriteOnce, ResubmitRequestState.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003CE7 RID: 15591
		public static readonly SimpleProviderPropertyDefinition DiagnosticInformation = new SimpleProviderPropertyDefinition("DiagnosticInformation", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
