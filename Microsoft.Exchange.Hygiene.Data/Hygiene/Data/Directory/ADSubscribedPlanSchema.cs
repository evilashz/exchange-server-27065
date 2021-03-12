using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C6 RID: 198
	internal class ADSubscribedPlanSchema : CommonSyncProperties
	{
		// Token: 0x0400040A RID: 1034
		public static readonly HygienePropertyDefinition AccountIdProperty = new HygienePropertyDefinition("accountId", typeof(ADObjectId));

		// Token: 0x0400040B RID: 1035
		public static readonly HygienePropertyDefinition CapabilityProperty = AssignedPlanSchema.CapabilityProp;

		// Token: 0x0400040C RID: 1036
		public static readonly HygienePropertyDefinition ServiceTypeProperty = new HygienePropertyDefinition("serviceType", typeof(string));

		// Token: 0x0400040D RID: 1037
		public static readonly HygienePropertyDefinition MaximumOverageUnitsDetailProperty = new HygienePropertyDefinition("MaximumOverageUnitsDetail", typeof(string));

		// Token: 0x0400040E RID: 1038
		public static readonly HygienePropertyDefinition PrepaidUnitsDetailProperty = new HygienePropertyDefinition("PrepaidUnitsDetail", typeof(string));

		// Token: 0x0400040F RID: 1039
		public static readonly HygienePropertyDefinition TotalTrialUnitsDetailProperty = new HygienePropertyDefinition("TotalTrialUnitsDetail", typeof(string));
	}
}
