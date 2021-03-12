using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000008 RID: 8
	internal class TransactionOutcomeBaseSchema : ObjectSchema
	{
		// Token: 0x0400002A RID: 42
		public static readonly SimpleProviderPropertyDefinition ClientAccessServer = new SimpleProviderPropertyDefinition("ClientAccessServer", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002B RID: 43
		public static readonly SimpleProviderPropertyDefinition Latency = new SimpleProviderPropertyDefinition("Latency", ExchangeObjectVersion.Exchange2010, typeof(EnhancedTimeSpan), PropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.Zero, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002C RID: 44
		public static readonly SimpleProviderPropertyDefinition ScenarioDescription = new SimpleProviderPropertyDefinition("ScenarioDescription", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002D RID: 45
		public static readonly SimpleProviderPropertyDefinition ScenarioName = new SimpleProviderPropertyDefinition("ScenarioName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002E RID: 46
		public static readonly SimpleProviderPropertyDefinition PerformanceCounterName = new SimpleProviderPropertyDefinition("PerformanceCounterName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002F RID: 47
		public static readonly SimpleProviderPropertyDefinition Result = new SimpleProviderPropertyDefinition("Result", ExchangeObjectVersion.Exchange2010, typeof(CasTransactionResult), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000030 RID: 48
		public static readonly SimpleProviderPropertyDefinition AdditionalInformation = new SimpleProviderPropertyDefinition("AdditionalInformation", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000031 RID: 49
		public static readonly SimpleProviderPropertyDefinition StartTime = new SimpleProviderPropertyDefinition("StartTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime), PropertyDefinitionFlags.None, ExDateTime.Now, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000032 RID: 50
		public static readonly SimpleProviderPropertyDefinition UserName = new SimpleProviderPropertyDefinition("UserName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000033 RID: 51
		public static readonly SimpleProviderPropertyDefinition EventType = new SimpleProviderPropertyDefinition("EventType", ExchangeObjectVersion.Exchange2010, typeof(EventTypeEnumeration), PropertyDefinitionFlags.None, EventTypeEnumeration.Success, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
