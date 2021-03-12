using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalanceClient
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CapacitySummarySchema : ObjectSchema
	{
		// Token: 0x04000002 RID: 2
		public static readonly PropertyDefinition Identity = ADObjectSchema.Name;

		// Token: 0x04000003 RID: 3
		public static readonly PropertyDefinition LogicalSize = new SimpleProviderPropertyDefinition("LogicalSize", ExchangeObjectVersion.Exchange2012, typeof(ByteQuantifiedSize), PropertyDefinitionFlags.TaskPopulated, ByteQuantifiedSize.Zero, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000004 RID: 4
		public static readonly PropertyDefinition MaximumSize = new SimpleProviderPropertyDefinition("MaximumSize", ExchangeObjectVersion.Exchange2012, typeof(ByteQuantifiedSize), PropertyDefinitionFlags.TaskPopulated, ByteQuantifiedSize.Zero, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000005 RID: 5
		public static readonly PropertyDefinition PhysicalSize = new SimpleProviderPropertyDefinition("PhysicalSize", ExchangeObjectVersion.Exchange2012, typeof(ByteQuantifiedSize), PropertyDefinitionFlags.TaskPopulated, ByteQuantifiedSize.Zero, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000006 RID: 6
		public static readonly PropertyDefinition RetrievedTimestamp = new SimpleProviderPropertyDefinition("RetrievedTimestamp", ExchangeObjectVersion.Exchange2012, typeof(DateTime), PropertyDefinitionFlags.TaskPopulated, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000007 RID: 7
		public static readonly PropertyDefinition TotalMailboxCount = new SimpleProviderPropertyDefinition("TotalMailboxCount", ExchangeObjectVersion.Exchange2012, typeof(long), PropertyDefinitionFlags.TaskPopulated, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
