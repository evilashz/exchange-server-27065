using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001FE RID: 510
	internal class DatabaseStaleStatusAlert : MonitoringAlert
	{
		// Token: 0x06001425 RID: 5157 RVA: 0x00051719 File Offset: 0x0004F919
		public DatabaseStaleStatusAlert(string databaseName, Guid dbGuid) : base(databaseName, dbGuid)
		{
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00051723 File Offset: 0x0004F923
		protected override TimeSpan DatabaseHealthCheckGreenTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckStaleStatusGreenTransitionSuppressionInSec);
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00051730 File Offset: 0x0004F930
		protected override TimeSpan DatabaseHealthCheckGreenPeriodicInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckStaleStatusGreenPeriodicIntervalInSec);
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0005173D File Offset: 0x0004F93D
		protected override TimeSpan DatabaseHealthCheckRedTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckStaleStatusRedTransitionSuppressionInSec);
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0005174A File Offset: 0x0004F94A
		protected override TimeSpan DatabaseHealthCheckRedPeriodicInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckStaleStatusRedPeriodicIntervalInSec);
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00051757 File Offset: 0x0004F957
		protected override bool IsValidationSuccessful(IHealthValidationResultMinimal result)
		{
			return !result.IsAnyCachedCopyStatusStale;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00051762 File Offset: 0x0004F962
		protected override void RaiseGreenEvent(IHealthValidationResultMinimal result)
		{
			MonitoringAlert.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "DatabaseStaleStatusAlert: RaiseGreenEvent() called for DB '{0}' ({1})", result.Identity, result.IdentityGuid);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00051786 File Offset: 0x0004F986
		protected override void RaiseRedEvent(IHealthValidationResultMinimal result)
		{
			MonitoringAlert.Tracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "DatabaseStaleStatusAlert: RaiseRedEvent() called for DB '{0}' ({1})", result.Identity, result.IdentityGuid);
			ReplayCrimsonEvents.DatabaseStaleStatusCheckFailed.Log<Guid, string>(base.IdentityGuid, base.Identity);
		}
	}
}
