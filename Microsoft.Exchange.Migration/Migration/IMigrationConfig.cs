using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationConfig
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002E7 RID: 743
		long Version { get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002E8 RID: 744
		long MinimumSupportedVersion { get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002E9 RID: 745
		long MaximumSupportedVersion { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002EA RID: 746
		long CurrentSupportedVersion { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002EB RID: 747
		long SupportedVersionUpgrade { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002EC RID: 748
		int MaxNumberOfBatches { get; }

		// Token: 0x060002ED RID: 749
		bool IsSupported(MigrationFeature features);

		// Token: 0x060002EE RID: 750
		bool IsDisplaySupported(MigrationFeature features);

		// Token: 0x060002EF RID: 751
		void CheckFeaturesAvailableToUpgrade(MigrationFeature features);

		// Token: 0x060002F0 RID: 752
		bool EnableFeatures(IMigrationDataProvider dataProvider, MigrationFeature features);

		// Token: 0x060002F1 RID: 753
		void DisableFeatures(IMigrationDataProvider dataProvider, MigrationFeature features, bool force);

		// Token: 0x060002F2 RID: 754
		void CheckAndUpgradeToSupportedFeaturesAndVersion(IMigrationDataProvider dataProvider);

		// Token: 0x060002F3 RID: 755
		bool CanCreateNewJobOfType(MigrationType migrationType, bool isStaged, out LocalizedException exception);
	}
}
