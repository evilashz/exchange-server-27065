using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000541 RID: 1345
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatabaseAvailabilityCheck : ReplicationCheck
	{
		// Token: 0x0600302B RID: 12331 RVA: 0x000C35B4 File Offset: 0x000C17B4
		public DatabaseAvailabilityCheck(string serverName, IEventManager eventManager, string momEventSource, DatabaseHealthValidationRunner validationRunner, uint ignoreTransientErrorsThreshold) : base("DatabaseAvailability", CheckId.DatabasesAvailability, Strings.DatabaseAvailabilityCheckDesc, CheckCategory.SystemHighPriority, eventManager, momEventSource, serverName, new uint?(ignoreTransientErrorsThreshold))
		{
			this.m_validationRunner = validationRunner;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000C35EC File Offset: 0x000C17EC
		protected override void InternalRun()
		{
			if (!ReplicationCheckGlobals.DatabaseAvailabilityCheckHasRun)
			{
				ReplicationCheckGlobals.DatabaseAvailabilityCheckHasRun = true;
			}
			else
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "DatabaseAvailabilityCheck skipping because it has already been run once.");
				base.Skip();
			}
			Exception ex = DatabaseRedundancyCheck.InitializeValidationRunner(this.m_validationRunner);
			if (ex != null)
			{
				base.Fail(Strings.DatabasesAvailabilityCheckFailed(base.ServerName, ex.Message));
			}
			foreach (IHealthValidationResultMinimal healthValidationResultMinimal in this.m_validationRunner.RunDatabaseAvailabilityChecks())
			{
				base.InstanceIdentity = healthValidationResultMinimal.IdentityGuid.ToString().ToLowerInvariant();
				if (healthValidationResultMinimal.IsValidationSuccessful)
				{
					base.ReportPassedInstance();
				}
				else
				{
					base.FailContinue(new LocalizedString(healthValidationResultMinimal.ErrorMessageWithoutFullStatus));
				}
				base.InstanceIdentity = null;
			}
		}

		// Token: 0x0400223F RID: 8767
		private DatabaseHealthValidationRunner m_validationRunner;
	}
}
