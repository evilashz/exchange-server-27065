using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x020005A0 RID: 1440
	internal abstract class TransportReportSchema : ObjectSchema
	{
		// Token: 0x04002374 RID: 9076
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(ObjectId), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002375 RID: 9077
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2010, typeof(ObjectState), PropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002376 RID: 9078
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2010, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.ReadOnly, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002377 RID: 9079
		public static readonly SimpleProviderPropertyDefinition StartDate = new SimpleProviderPropertyDefinition("StartDate", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime), PropertyDefinitionFlags.PersistDefaultValue, ExDateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002378 RID: 9080
		public static readonly SimpleProviderPropertyDefinition EndDate = new SimpleProviderPropertyDefinition("EndDate", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime), PropertyDefinitionFlags.PersistDefaultValue, ExDateTime.MaxValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
