using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001EE RID: 494
	internal class PotentialOneRedundantCopyAlertTable : DatabaseAlertInfoTable<DatabasePotentialOneCopyAlert>
	{
		// Token: 0x06001386 RID: 4998 RVA: 0x0004F06C File Offset: 0x0004D26C
		public PotentialOneRedundantCopyAlertTable() : base(new Func<IHealthValidationResultMinimal, DatabasePotentialOneCopyAlert>(PotentialOneRedundantCopyAlertTable.CreateDatabaseOneRedundantCopyAlert))
		{
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0004F080 File Offset: 0x0004D280
		protected override MonitoringAlert GetExistingOrNewAlertInfo(IHealthValidationResultMinimal result)
		{
			DatabasePotentialOneCopyAlert databasePotentialOneCopyAlert = (DatabasePotentialOneCopyAlert)base.GetExistingOrNewAlertInfo(result);
			string activeServerName = PotentialOneRedundantCopyAlertTable.GetActiveServerName(result);
			if (databasePotentialOneCopyAlert.TargetServer != activeServerName)
			{
				DatabaseAlertInfoTable<DatabasePotentialOneCopyAlert>.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "PotentialOneRedundantCopyAlertTable::GetExistingOrNewAlertInfo: TargetServer has been changed, create one new alert for {0}, new TargetServer is {1}", result.Identity, activeServerName);
				base.RemoveDatabaseAlert(result.IdentityGuid);
				databasePotentialOneCopyAlert = (DatabasePotentialOneCopyAlert)base.GetExistingOrNewAlertInfo(result);
			}
			return databasePotentialOneCopyAlert;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0004F0E8 File Offset: 0x0004D2E8
		private static DatabasePotentialOneCopyAlert CreateDatabaseOneRedundantCopyAlert(IHealthValidationResultMinimal validationResult)
		{
			string activeServerName = PotentialOneRedundantCopyAlertTable.GetActiveServerName(validationResult);
			return new DatabasePotentialOneCopyAlert(validationResult.Identity, validationResult.IdentityGuid, activeServerName);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004F110 File Offset: 0x0004D310
		private static string GetActiveServerName(IHealthValidationResultMinimal validationResult)
		{
			IHealthValidationResult healthValidationResult = (IHealthValidationResult)validationResult;
			string result = null;
			if (healthValidationResult.ActiveCopyStatus != null)
			{
				result = healthValidationResult.ActiveCopyStatus.ServerContacted.NetbiosName;
			}
			else if (healthValidationResult.TargetCopyStatus != null && healthValidationResult.TargetCopyStatus.ActiveServer != null)
			{
				result = healthValidationResult.TargetCopyStatus.ActiveServer.NetbiosName;
			}
			return result;
		}
	}
}
