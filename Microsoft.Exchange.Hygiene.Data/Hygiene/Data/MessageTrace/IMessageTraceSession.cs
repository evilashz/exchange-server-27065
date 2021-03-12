using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000148 RID: 328
	internal interface IMessageTraceSession
	{
		// Token: 0x06000C90 RID: 3216
		int GetNumberOfPersistentCopiesPerPartition(int physicalInstanceId);

		// Token: 0x06000C91 RID: 3217
		int GetNumberOfPhysicalPartitions();

		// Token: 0x06000C92 RID: 3218
		void Save(MessageTraceBatch messageTraceBatch);

		// Token: 0x06000C93 RID: 3219
		void Save(IEnumerable<MessageTrafficTypeMapping> messageTrafficTypeMappingBatch, int? persistentStoreCopyId);

		// Token: 0x06000C94 RID: 3220
		Dictionary<int, bool[]> GetStatusOfAllPhysicalPartitionCopies();

		// Token: 0x06000C95 RID: 3221
		object GetPartitionId(string hashKey);
	}
}
