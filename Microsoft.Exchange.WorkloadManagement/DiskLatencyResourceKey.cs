using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000023 RID: 35
	internal sealed class DiskLatencyResourceKey : MdbResourceKey
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00005C1C File Offset: 0x00003E1C
		public DiskLatencyResourceKey(Guid databaseGuid) : base(ResourceMetricType.DiskLatency, databaseGuid)
		{
			DatabaseInformation databaseInformation = DatabaseInformationCache.Singleton.Get(databaseGuid);
			this.DatabaseVolumeName = ((databaseInformation != null) ? databaseInformation.DatabaseVolumeName : string.Empty);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005C53 File Offset: 0x00003E53
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005C5B File Offset: 0x00003E5B
		public string DatabaseVolumeName { get; private set; }

		// Token: 0x06000129 RID: 297 RVA: 0x00005C64 File Offset: 0x00003E64
		protected internal override CacheableResourceHealthMonitor CreateMonitor()
		{
			return new DiskLatencyResourceMonitor(this);
		}
	}
}
