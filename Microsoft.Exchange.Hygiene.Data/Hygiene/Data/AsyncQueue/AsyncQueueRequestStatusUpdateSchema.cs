using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000024 RID: 36
	internal class AsyncQueueRequestStatusUpdateSchema
	{
		// Token: 0x04000092 RID: 146
		internal static readonly HygienePropertyDefinition RequestIdProperty = AsyncQueueCommonSchema.RequestIdProperty;

		// Token: 0x04000093 RID: 147
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = AsyncQueueCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000094 RID: 148
		internal static readonly HygienePropertyDefinition RequestStepIdProperty = AsyncQueueCommonSchema.RequestStepIdProperty;

		// Token: 0x04000095 RID: 149
		internal static readonly HygienePropertyDefinition CurrentStatusProperty = new HygienePropertyDefinition("FromStatus", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000096 RID: 150
		internal static readonly HygienePropertyDefinition StatusProperty = new HygienePropertyDefinition("Status", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000097 RID: 151
		internal static readonly HygienePropertyDefinition ProcessInstanceNameProperty = AsyncQueueCommonSchema.ProcessInstanceNameProperty;

		// Token: 0x04000098 RID: 152
		internal static readonly HygienePropertyDefinition RetryIntervalProperty = new HygienePropertyDefinition("RetryInterval", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000099 RID: 153
		internal static readonly HygienePropertyDefinition RequestCompleteProperty = new HygienePropertyDefinition("RequestComplete", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400009A RID: 154
		internal static readonly HygienePropertyDefinition CookieProperty = AsyncQueueCommonSchema.CookieProperty;

		// Token: 0x0400009B RID: 155
		internal static readonly HygienePropertyDefinition RequestStatusProperty = AsyncQueueCommonSchema.RequestStatusProperty;

		// Token: 0x0400009C RID: 156
		internal static readonly HygienePropertyDefinition RequestStartDatetimeProperty = new HygienePropertyDefinition("RequestStartDatetime", typeof(DateTime?));

		// Token: 0x0400009D RID: 157
		internal static readonly HygienePropertyDefinition RequestEndDatetimeProperty = new HygienePropertyDefinition("RequestEndDatetime", typeof(DateTime?));
	}
}
