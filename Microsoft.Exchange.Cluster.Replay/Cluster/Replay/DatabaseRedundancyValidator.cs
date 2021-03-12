using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000200 RID: 512
	internal class DatabaseRedundancyValidator : DatabaseValidatorBase
	{
		// Token: 0x06001435 RID: 5173 RVA: 0x00051940 File Offset: 0x0004FB40
		public DatabaseRedundancyValidator(IADDatabase database, int numHealthyCopiesMinimum, ICopyStatusClientLookup statusLookup, IMonitoringADConfig adConfig, PropertyUpdateTracker propertyUpdateTracker = null, bool shouldSkipEvents = true) : base(database, numHealthyCopiesMinimum, statusLookup, adConfig, propertyUpdateTracker, false, true, true, true, shouldSkipEvents)
		{
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00051960 File Offset: 0x0004FB60
		public DatabaseRedundancyValidator(IADDatabase database, int numHealthyCopiesMinimum, ICopyStatusClientLookup statusLookup, IMonitoringADConfig adConfig, PropertyUpdateTracker propertyUpdateTracker, bool isCopyRemoval, bool ignoreActivationDisfavored, bool ignoreMaintenanceChecks, bool ignoreTooManyActivesCheck, bool shouldSkipEvents = true) : base(database, numHealthyCopiesMinimum, statusLookup, adConfig, propertyUpdateTracker, isCopyRemoval, ignoreActivationDisfavored, ignoreMaintenanceChecks, ignoreTooManyActivesCheck, shouldSkipEvents)
		{
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00051984 File Offset: 0x0004FB84
		protected override DatabaseValidationMultiChecks ActiveCopyChecks
		{
			get
			{
				return DatabaseValidationChecks.DatabaseRedundancyDatabaseLevelActiveChecks;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0005198B File Offset: 0x0004FB8B
		protected override DatabaseValidationMultiChecks PassiveCopyChecks
		{
			get
			{
				return DatabaseValidationChecks.DatabaseRedundancyDatabaseLevelPassiveChecks;
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00051992 File Offset: 0x0004FB92
		protected override string GetValidationRollupErrorMessage(int healthyCopiesCount, int expectedHealthyCopiesCount, int totalPassiveCopiesCount, int healthyPassiveCopiesCount, string rollupMessage)
		{
			return ReplayStrings.DbRedundancyValidationErrorsOccurred(base.Database.Name, healthyCopiesCount, expectedHealthyCopiesCount, rollupMessage);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x000519AD File Offset: 0x0004FBAD
		protected override Exception UpdateReplayStateProperties(RegistryStateAccess regState, bool validationChecksPassed)
		{
			return base.UpdateReplayStatePropertiesCommon(regState, validationChecksPassed, "LastCopyRedundancyChecksPassedTime", "IsLastCopyRedundancyChecksPassed");
		}
	}
}
