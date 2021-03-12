using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x0200000B RID: 11
	internal class MonitorIdentitySchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400003E RID: 62
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400003F RID: 63
		public static readonly SimpleProviderPropertyDefinition HealthSetName = new SimpleProviderPropertyDefinition("HealthSetName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000040 RID: 64
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000041 RID: 65
		public static readonly SimpleProviderPropertyDefinition TargetResource = new SimpleProviderPropertyDefinition("TargetResource", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000042 RID: 66
		public static readonly SimpleProviderPropertyDefinition ItemType = new SimpleProviderPropertyDefinition("ItemType", ExchangeObjectVersion.Exchange2010, typeof(MonitorItemType), PropertyDefinitionFlags.None, MonitorItemType.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
