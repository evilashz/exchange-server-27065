using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000201 RID: 513
	internal class DatabaseReplicationNotStalledValidator : DatabaseValidatorBase
	{
		// Token: 0x0600143B RID: 5179 RVA: 0x000519C4 File Offset: 0x0004FBC4
		public DatabaseReplicationNotStalledValidator(IADDatabase database, ICopyStatusClientLookup statusLookup, IMonitoringADConfig adConfig, PropertyUpdateTracker propertyUpdateTracker, bool shouldSkipEvents = true) : base(database, 0, 1, statusLookup, adConfig, propertyUpdateTracker, false, true, true, true, shouldSkipEvents)
		{
			if (propertyUpdateTracker == null)
			{
				throw new ArgumentNullException("propertyUpdateTracker shouldn't be null for DatabaseReplicationNotStalledValidator");
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x000519F3 File Offset: 0x0004FBF3
		protected override DatabaseValidationMultiChecks ActiveCopyChecks
		{
			get
			{
				return DatabaseValidationChecks.DatabaseConnectedActiveChecks;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x000519FA File Offset: 0x0004FBFA
		protected override DatabaseValidationMultiChecks PassiveCopyChecks
		{
			get
			{
				return DatabaseValidationChecks.DatabaseConnectedPassiveChecks;
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00051A01 File Offset: 0x0004FC01
		protected override string GetValidationRollupErrorMessage(int healthyCopiesCount, int expectedHealthyCopiesCount, int totalPassiveCopiesCount, int healthyPassiveCopiesCount, string rollupMessage)
		{
			return ReplayStrings.PotentialRedundancyValidationDBReplicationStalled(base.Database.Name, totalPassiveCopiesCount, rollupMessage);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00051A1B File Offset: 0x0004FC1B
		protected override Exception UpdateReplayStateProperties(RegistryStateAccess regState, bool validationChecksPassed)
		{
			return null;
		}
	}
}
