using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200002A RID: 42
	internal class OrganizationUpgradeConstraintAdapter : IUpgradeConstraintAdapter
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000930C File Offset: 0x0000750C
		public void AddUpgradeConstraintIfNeeded(IMigrationDataProvider dataProvider, MigrationSession session)
		{
			TimeSpan config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MigrationUpgradeConstraintEnforcementPeriod");
			if (ExDateTime.UtcNow - session.LastUpgradeConstraintEnforcedTimestamp < config)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "Constraint was checked and enforced on {0}, which means it's below the enforcement period of {1}, no need to do anything else.", new object[]
				{
					session.LastUpgradeConstraintEnforcedTimestamp,
					config
				});
				return;
			}
			this.AddUpgradeConstraint(dataProvider, session);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009370 File Offset: 0x00007570
		public void AddUpgradeConstraint(IMigrationDataProvider dataProvider, MigrationSession session)
		{
			UpgradeConstraint constraint = new UpgradeConstraint("MigrationService", "Organization has migration batches and can't be upgraded.");
			dataProvider.ADProvider.UpdateMigrationUpgradeConstraint(constraint);
			session.SetLastUpdateConstraintEnforcedTimestamp(dataProvider, ExDateTime.UtcNow);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000093A8 File Offset: 0x000075A8
		public void MarkUpgradeConstraintForExpiry(IMigrationDataProvider dataProvider, DateTime? expirationDate)
		{
			if (expirationDate == null)
			{
				TimeSpan config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MigrationUpgradeConstraintExpirationPeriod");
				expirationDate = new DateTime?(DateTime.UtcNow.Date.Add(config));
			}
			UpgradeConstraint constraint = new UpgradeConstraint("MigrationService", "Organization has migration batches and can't be upgraded.", expirationDate.Value);
			dataProvider.ADProvider.UpdateMigrationUpgradeConstraint(constraint);
		}

		// Token: 0x040000A7 RID: 167
		internal const string MigrationConstraintName = "MigrationService";

		// Token: 0x040000A8 RID: 168
		internal const string OrganizationHasActiveBatches = "Organization has migration batches and can't be upgraded.";
	}
}
