using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000017 RID: 23
	internal sealed class CiAgeOfLastNotificationResourceKey : CiResourceKey
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00004760 File Offset: 0x00002960
		public CiAgeOfLastNotificationResourceKey(Guid mdbGuid) : base(ResourceMetricType.CiAgeOfLastNotification, mdbGuid)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000476A File Offset: 0x0000296A
		protected internal override CacheableResourceHealthMonitor CreateMonitor()
		{
			return new CiAgeOfLastNotificationResourceHealthMonitor(this);
		}
	}
}
