using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000090 RID: 144
	internal interface IPartitionedDataProvider : IConfigDataProvider
	{
		// Token: 0x0600050C RID: 1292
		int GetNumberOfPersistentCopiesPerPartition(int physicalInstanceId);

		// Token: 0x0600050D RID: 1293
		int GetNumberOfPhysicalPartitions();

		// Token: 0x0600050E RID: 1294
		object[] GetAllPhysicalPartitions();

		// Token: 0x0600050F RID: 1295
		Dictionary<int, bool[]> GetStatusOfAllPhysicalPartitionCopies();

		// Token: 0x06000510 RID: 1296
		string GetPartitionedDatabaseCopyName(int physicalPartition, int fssCopyId);

		// Token: 0x06000511 RID: 1297
		void UpdateLatency(int physicalPartition, int fssCopyId, LatencyBucket bucket);
	}
}
