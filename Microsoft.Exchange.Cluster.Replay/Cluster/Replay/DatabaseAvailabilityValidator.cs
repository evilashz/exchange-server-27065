using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F0 RID: 496
	internal class DatabaseAvailabilityValidator : DatabaseValidatorBase
	{
		// Token: 0x060013A3 RID: 5027 RVA: 0x0004FBE0 File Offset: 0x0004DDE0
		public DatabaseAvailabilityValidator(IADDatabase database, int numAvailableCopiesMinimum, ICopyStatusClientLookup statusLookup, IMonitoringADConfig adConfig, PropertyUpdateTracker propertyUpdateTracker = null, bool shouldSkipEvents = true) : base(database, numAvailableCopiesMinimum, statusLookup, adConfig, propertyUpdateTracker, false, false, false, false, shouldSkipEvents)
		{
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0004FC00 File Offset: 0x0004DE00
		protected override DatabaseValidationMultiChecks ActiveCopyChecks
		{
			get
			{
				return DatabaseValidationChecks.DatabaseAvailabilityActiveChecks;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0004FC07 File Offset: 0x0004DE07
		protected override DatabaseValidationMultiChecks PassiveCopyChecks
		{
			get
			{
				return DatabaseValidationChecks.DatabaseAvailabilityPassiveChecks;
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004FC0E File Offset: 0x0004DE0E
		protected override string GetValidationRollupErrorMessage(int healthyCopiesCount, int expectedHealthyCopiesCount, int totalPassiveCopiesCount, int healthyPassiveCopiesCount, string rollupMessage)
		{
			return ReplayStrings.DbAvailabilityValidationErrorsOccurred(base.Database.Name, healthyCopiesCount, expectedHealthyCopiesCount, rollupMessage);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0004FC29 File Offset: 0x0004DE29
		protected override Exception UpdateReplayStateProperties(RegistryStateAccess regState, bool validationChecksPassed)
		{
			return base.UpdateReplayStatePropertiesCommon(regState, validationChecksPassed, "LastCopyAvailabilityChecksPassedTime", "IsLastCopyAvailabilityChecksPassed");
		}
	}
}
