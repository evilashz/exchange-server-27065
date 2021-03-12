using System;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Hygiene.Data.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000CB RID: 203
	internal class AssignedPlanSchema : CommonSyncProperties
	{
		// Token: 0x04000422 RID: 1058
		public static readonly HygienePropertyDefinition SubscribedPlanIdProp = new HygienePropertyDefinition("subscribedPlanId", typeof(Guid));

		// Token: 0x04000423 RID: 1059
		public static readonly HygienePropertyDefinition CapabilityProp = new HygienePropertyDefinition("capability", typeof(string));

		// Token: 0x04000424 RID: 1060
		public static readonly HygienePropertyDefinition CapabilityStatusProp = new HygienePropertyDefinition("capabilityStatus", typeof(AssignedCapabilityStatus), AssignedCapabilityStatus.Enabled, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000425 RID: 1061
		public static readonly HygienePropertyDefinition AssignedTimeStampProp = new HygienePropertyDefinition("assignedTimestamp", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000426 RID: 1062
		public static readonly HygienePropertyDefinition InitialStateProp = new HygienePropertyDefinition("initialState", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
