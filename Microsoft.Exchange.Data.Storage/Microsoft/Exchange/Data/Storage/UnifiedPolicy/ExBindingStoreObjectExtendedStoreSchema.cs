using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E98 RID: 3736
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExBindingStoreObjectExtendedStoreSchema : ObjectSchema
	{
		// Token: 0x0400572E RID: 22318
		public static readonly ExtendedPropertyDefinition Name = new ExtendedPropertyDefinition(WellKnownPropertySet.UnifiedPolicy, "Name", 25);

		// Token: 0x0400572F RID: 22319
		public static readonly ExtendedPropertyDefinition MasterIdentity = new ExtendedPropertyDefinition(WellKnownPropertySet.UnifiedPolicy, "MasterIdentity", 25);

		// Token: 0x04005730 RID: 22320
		public static readonly ExtendedPropertyDefinition Workload = new ExtendedPropertyDefinition(WellKnownPropertySet.UnifiedPolicy, "Workload", 14);

		// Token: 0x04005731 RID: 22321
		public static readonly ExtendedPropertyDefinition PolicyVersion = new ExtendedPropertyDefinition(WellKnownPropertySet.UnifiedPolicy, "PolicyVersion", 2);
	}
}
