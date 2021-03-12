using System;
using Microsoft.Exchange.Data.Storage.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000016 RID: 22
	internal abstract class CiResourceKey : MdbResourceKey
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00004756 File Offset: 0x00002956
		protected CiResourceKey(ResourceMetricType metric, Guid mdbGuid) : base(metric, mdbGuid)
		{
		}
	}
}
