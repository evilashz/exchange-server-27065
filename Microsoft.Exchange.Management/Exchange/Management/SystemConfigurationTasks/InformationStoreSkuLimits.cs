using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009BB RID: 2491
	[Serializable]
	internal class InformationStoreSkuLimits
	{
		// Token: 0x060058C0 RID: 22720 RVA: 0x00172767 File Offset: 0x00170967
		private InformationStoreSkuLimits(int maxStorageGroups, int maxStoresPerGroup, int maxStoresTotal, int maxRestoreStorageGroups)
		{
			this.maxStorageGroups = maxStorageGroups;
			this.maxStoresPerGroup = maxStoresPerGroup;
			this.maxStoresTotal = maxStoresTotal;
			this.maxRestoreStorageGroups = maxRestoreStorageGroups;
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x0017278C File Offset: 0x0017098C
		public InformationStoreSkuLimits(InformationStore informationStore) : this(informationStore.MaxStorageGroups, informationStore.MaxStoresPerGroup, informationStore.MaxStoresTotal, informationStore.MaxRestoreStorageGroups)
		{
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x001727AC File Offset: 0x001709AC
		public void UpdateInformationStore(InformationStore informationStore)
		{
			informationStore.MaxStorageGroups = this.maxStorageGroups;
			informationStore.MaxStoresPerGroup = this.maxStoresPerGroup;
			informationStore.MaxStoresTotal = this.maxStoresTotal;
			informationStore.MaxRestoreStorageGroups = this.maxRestoreStorageGroups;
		}

		// Token: 0x040032DA RID: 13018
		private readonly int maxStorageGroups;

		// Token: 0x040032DB RID: 13019
		private readonly int maxStoresPerGroup;

		// Token: 0x040032DC RID: 13020
		private readonly int maxStoresTotal;

		// Token: 0x040032DD RID: 13021
		private readonly int maxRestoreStorageGroups;

		// Token: 0x040032DE RID: 13022
		public static readonly InformationStoreSkuLimits Enterprise = new InformationStoreSkuLimits(100, 5, 100, 1);

		// Token: 0x040032DF RID: 13023
		public static readonly InformationStoreSkuLimits Standard = new InformationStoreSkuLimits(5, 5, 5, 1);

		// Token: 0x040032E0 RID: 13024
		public static readonly InformationStoreSkuLimits Coexistence = new InformationStoreSkuLimits(5, 5, 5, 1);
	}
}
