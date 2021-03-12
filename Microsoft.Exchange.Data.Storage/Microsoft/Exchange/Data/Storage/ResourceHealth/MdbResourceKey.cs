using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B29 RID: 2857
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MdbResourceKey : ResourceKey
	{
		// Token: 0x06006780 RID: 26496 RVA: 0x001B5719 File Offset: 0x001B3919
		public MdbResourceKey(ResourceMetricType metric, Guid databaseGuid) : base(metric, MdbResourceKey.GetId(databaseGuid))
		{
			if (databaseGuid == Guid.Empty)
			{
				throw new ArgumentException("Guid.Empty is not a valid MdbGuid value", "mdbGuid");
			}
			this.DatabaseGuid = databaseGuid;
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x06006781 RID: 26497 RVA: 0x001B574C File Offset: 0x001B394C
		// (set) Token: 0x06006782 RID: 26498 RVA: 0x001B5754 File Offset: 0x001B3954
		public Guid DatabaseGuid { get; private set; }

		// Token: 0x06006783 RID: 26499 RVA: 0x001B5760 File Offset: 0x001B3960
		private static string GetId(Guid databaseGuid)
		{
			if (databaseGuid == Guid.Empty)
			{
				throw new ArgumentException("Guid.Empty is not a valid MdbGuid value", "mdbGuid");
			}
			IDatabaseInformation databaseInformation = DatabaseInformationCache.Singleton.Get(databaseGuid);
			if (databaseInformation == null)
			{
				return databaseGuid.ToString();
			}
			return databaseInformation.DatabaseName;
		}
	}
}
