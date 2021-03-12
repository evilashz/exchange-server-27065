using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000540 RID: 1344
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatabaseRedundancyCheck : ReplicationCheck
	{
		// Token: 0x06003028 RID: 12328 RVA: 0x000C343C File Offset: 0x000C163C
		public DatabaseRedundancyCheck(string serverName, IEventManager eventManager, string momEventSource, DatabaseHealthValidationRunner validationRunner, uint ignoreTransientErrorsThreshold) : base("DatabaseRedundancy", CheckId.DatabasesRedundancy, Strings.DatabaseRedundancyCheckDesc, CheckCategory.SystemHighPriority, eventManager, momEventSource, serverName, new uint?(ignoreTransientErrorsThreshold))
		{
			this.m_validationRunner = validationRunner;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000C3474 File Offset: 0x000C1674
		protected override void InternalRun()
		{
			if (!ReplicationCheckGlobals.DatabaseRedundancyCheckHasRun)
			{
				ReplicationCheckGlobals.DatabaseRedundancyCheckHasRun = true;
			}
			else
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "DatabaseRedundancyCheck skipping because it has already been run once.");
				base.Skip();
			}
			Exception ex = DatabaseRedundancyCheck.InitializeValidationRunner(this.m_validationRunner);
			if (ex != null)
			{
				base.Fail(Strings.DatabasesRedundancyCheckFailed(base.ServerName, ex.Message));
			}
			foreach (IHealthValidationResultMinimal healthValidationResultMinimal in this.m_validationRunner.RunDatabaseRedundancyChecks(null, true))
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

		// Token: 0x0600302A RID: 12330 RVA: 0x000C3564 File Offset: 0x000C1764
		internal static Exception InitializeValidationRunner(DatabaseHealthValidationRunner runner)
		{
			Exception result = null;
			try
			{
				runner.Initialize();
			}
			catch (MonitoringADConfigException ex)
			{
				result = ex;
			}
			catch (AmServerException ex2)
			{
				result = ex2;
			}
			catch (AmServerTransientException ex3)
			{
				result = ex3;
			}
			return result;
		}

		// Token: 0x0400223E RID: 8766
		private DatabaseHealthValidationRunner m_validationRunner;
	}
}
