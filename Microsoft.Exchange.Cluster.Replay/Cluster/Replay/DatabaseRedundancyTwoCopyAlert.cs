using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001FB RID: 507
	internal class DatabaseRedundancyTwoCopyAlert : MonitoringAlert
	{
		// Token: 0x06001405 RID: 5125 RVA: 0x00050EF4 File Offset: 0x0004F0F4
		public DatabaseRedundancyTwoCopyAlert(string databaseName, Guid dbGuid) : base(databaseName, dbGuid)
		{
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00050EFE File Offset: 0x0004F0FE
		protected override TimeSpan DatabaseHealthCheckGreenTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckTwoCopyGreenTransitionSuppressionInSec);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00050F0B File Offset: 0x0004F10B
		protected override TimeSpan DatabaseHealthCheckGreenPeriodicInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckTwoCopyGreenPeriodicIntervalInSec);
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00050F18 File Offset: 0x0004F118
		protected override TimeSpan DatabaseHealthCheckRedTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckTwoCopyRedTransitionSuppressionInSec);
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00050F25 File Offset: 0x0004F125
		protected override TimeSpan DatabaseHealthCheckRedPeriodicInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckTwoCopyRedPeriodicIntervalInSec);
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00050F32 File Offset: 0x0004F132
		protected override bool IsValidationSuccessful(IHealthValidationResultMinimal serverValidationResult)
		{
			return serverValidationResult.HealthyCopiesCount > 2;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00050F3D File Offset: 0x0004F13D
		protected override void RaiseGreenEvent(IHealthValidationResultMinimal result)
		{
			ReplayCrimsonEvents.DatabaseLevelRedundancyTwoCopyCheckPassed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00050F6B File Offset: 0x0004F16B
		protected override void RaiseRedEvent(IHealthValidationResultMinimal result)
		{
			ReplayCrimsonEvents.DatabaseLevelRedundancyTwoCopyCheckFailed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
		}
	}
}
