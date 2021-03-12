using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000016 RID: 22
	internal class AsyncQueueLogPropertySchema
	{
		// Token: 0x04000045 RID: 69
		internal static readonly HygienePropertyDefinition LogTimeProperty = new HygienePropertyDefinition("LogTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000046 RID: 70
		internal static readonly HygienePropertyDefinition LogTypeProperty = new HygienePropertyDefinition("LogType", typeof(string));

		// Token: 0x04000047 RID: 71
		internal static readonly HygienePropertyDefinition LogIndexProperty = new HygienePropertyDefinition("LogIndex", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000048 RID: 72
		internal static readonly HygienePropertyDefinition LogDataProperty = new HygienePropertyDefinition("LogData", typeof(string));
	}
}
