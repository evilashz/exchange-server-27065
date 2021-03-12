using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Compliance.DAR
{
	// Token: 0x0200045D RID: 1117
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskAggregateStoreObjectExtendedStoreSchema : ObjectSchema
	{
		// Token: 0x04001AEB RID: 6891
		public static readonly ExtendedPropertyDefinition Enabled = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "Enabled", 4);

		// Token: 0x04001AEC RID: 6892
		public static readonly ExtendedPropertyDefinition MaxRunningTasks = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "MaxRunningTasks", 14);

		// Token: 0x04001AED RID: 6893
		public static readonly ExtendedPropertyDefinition RecurrenceType = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "RecurrenceType", 14);

		// Token: 0x04001AEE RID: 6894
		public static readonly ExtendedPropertyDefinition RecurrenceFrequency = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "RecurrenceFrequency", 14);

		// Token: 0x04001AEF RID: 6895
		public static readonly ExtendedPropertyDefinition RecurrenceInterval = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "RecurrenceInterval", 14);
	}
}
