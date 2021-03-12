using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200000F RID: 15
	internal interface IUpgradeConstraintAdapter
	{
		// Token: 0x06000035 RID: 53
		void AddUpgradeConstraint(IMigrationDataProvider dataProvider, MigrationSession session);

		// Token: 0x06000036 RID: 54
		void AddUpgradeConstraintIfNeeded(IMigrationDataProvider dataProvider, MigrationSession session);

		// Token: 0x06000037 RID: 55
		void MarkUpgradeConstraintForExpiry(IMigrationDataProvider dataProvider, DateTime? expirationDate);
	}
}
