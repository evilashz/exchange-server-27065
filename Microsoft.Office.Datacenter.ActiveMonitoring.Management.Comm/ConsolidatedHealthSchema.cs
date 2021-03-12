using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x02000011 RID: 17
	internal class ConsolidatedHealthSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000074 RID: 116
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000075 RID: 117
		public static readonly SimpleProviderPropertyDefinition State = new SimpleProviderPropertyDefinition("State", ExchangeObjectVersion.Exchange2010, typeof(MonitorServerComponentState), PropertyDefinitionFlags.None, MonitorServerComponentState.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000076 RID: 118
		public static readonly SimpleProviderPropertyDefinition HealthSet = new SimpleProviderPropertyDefinition("HealthSet", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000077 RID: 119
		public static readonly SimpleProviderPropertyDefinition HealthGroup = new SimpleProviderPropertyDefinition("HealthGroup", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000078 RID: 120
		public static readonly SimpleProviderPropertyDefinition AlertValue = new SimpleProviderPropertyDefinition("AlertValue", ExchangeObjectVersion.Exchange2010, typeof(MonitorAlertState), PropertyDefinitionFlags.None, MonitorAlertState.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000079 RID: 121
		public static readonly SimpleProviderPropertyDefinition LastTransitionTime = new SimpleProviderPropertyDefinition("LastTransitionTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400007A RID: 122
		public static readonly SimpleProviderPropertyDefinition MonitorCount = new SimpleProviderPropertyDefinition("MonitorCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400007B RID: 123
		public static readonly SimpleProviderPropertyDefinition HaImpactingMonitorCount = new SimpleProviderPropertyDefinition("HaImpactingMonitorCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
