using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000029 RID: 41
	internal class NullUpgradeConstraintAdapter : IUpgradeConstraintAdapter
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x000092FC File Offset: 0x000074FC
		public void AddUpgradeConstraint(IMigrationDataProvider dataProvider, MigrationSession session)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000092FE File Offset: 0x000074FE
		public void AddUpgradeConstraintIfNeeded(IMigrationDataProvider dataProvider, MigrationSession session)
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009300 File Offset: 0x00007500
		public void MarkUpgradeConstraintForExpiry(IMigrationDataProvider dataProvider, DateTime? expirationDate)
		{
		}
	}
}
