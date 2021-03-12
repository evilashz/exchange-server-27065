using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F3 RID: 499
	internal class DatabaseLevelAlerts
	{
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x000506FE File Offset: 0x0004E8FE
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x00050706 File Offset: 0x0004E906
		public DatabaseAlertInfoTable<DatabaseRedundancyTwoCopyAlert> TwoCopy { get; private set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0005070F File Offset: 0x0004E90F
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x00050717 File Offset: 0x0004E917
		public DatabaseAlertInfoTable<DatabaseRedundancyAlert> OneRedundantCopy { get; private set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00050720 File Offset: 0x0004E920
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x00050728 File Offset: 0x0004E928
		public DatabaseAlertInfoTable<DatabaseRedundancySiteAlert> OneRedundantCopySite { get; private set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00050731 File Offset: 0x0004E931
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x00050739 File Offset: 0x0004E939
		public DatabaseAlertInfoTable<DatabaseAvailabilityAlert> OneAvailableCopy { get; private set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00050742 File Offset: 0x0004E942
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x0005074A File Offset: 0x0004E94A
		public DatabaseAlertInfoTable<DatabaseAvailabilitySiteAlert> OneAvailableCopySite { get; private set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x00050753 File Offset: 0x0004E953
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x0005075B File Offset: 0x0004E95B
		public DatabaseAlertInfoTable<DatabaseStaleStatusAlert> StaleStatus { get; private set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00050764 File Offset: 0x0004E964
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x0005076C File Offset: 0x0004E96C
		public PotentialOneRedundantCopyAlertTable PotentialOneRedundantCopy { get; private set; }

		// Token: 0x060013CE RID: 5070 RVA: 0x000507E8 File Offset: 0x0004E9E8
		public DatabaseLevelAlerts()
		{
			this.TwoCopy = new DatabaseAlertInfoTable<DatabaseRedundancyTwoCopyAlert>((IHealthValidationResultMinimal validationResult) => new DatabaseRedundancyTwoCopyAlert(validationResult.Identity, validationResult.IdentityGuid));
			this.OneRedundantCopy = new DatabaseAlertInfoTable<DatabaseRedundancyAlert>((IHealthValidationResultMinimal validationResult) => new DatabaseRedundancyAlert(validationResult.Identity, validationResult.IdentityGuid));
			this.OneRedundantCopySite = new DatabaseAlertInfoTable<DatabaseRedundancySiteAlert>((IHealthValidationResultMinimal validationResult) => new DatabaseRedundancySiteAlert(validationResult.Identity, validationResult.IdentityGuid));
			this.OneAvailableCopy = new DatabaseAlertInfoTable<DatabaseAvailabilityAlert>((IHealthValidationResultMinimal validationResult) => new DatabaseAvailabilityAlert(validationResult.Identity, validationResult.IdentityGuid));
			this.OneAvailableCopySite = new DatabaseAlertInfoTable<DatabaseAvailabilitySiteAlert>((IHealthValidationResultMinimal validationResult) => new DatabaseAvailabilitySiteAlert(validationResult.Identity, validationResult.IdentityGuid));
			this.StaleStatus = new DatabaseAlertInfoTable<DatabaseStaleStatusAlert>((IHealthValidationResultMinimal validationResult) => new DatabaseStaleStatusAlert(validationResult.Identity, validationResult.IdentityGuid));
			this.PotentialOneRedundantCopy = new PotentialOneRedundantCopyAlertTable();
			this.m_alertsArray = new IDatabaseAlertInfoTable[]
			{
				this.TwoCopy,
				this.OneRedundantCopy,
				this.OneRedundantCopySite,
				this.OneAvailableCopy,
				this.OneAvailableCopySite,
				this.StaleStatus,
				this.PotentialOneRedundantCopy
			};
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00050944 File Offset: 0x0004EB44
		internal void ResetState(Guid dbGuid)
		{
			for (int i = 0; i < this.m_alertsArray.Length; i++)
			{
				this.m_alertsArray[i].ResetState(dbGuid);
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00050974 File Offset: 0x0004EB74
		internal void Cleanup(HashSet<Guid> currentlyKnownDatabaseGuids)
		{
			for (int i = 0; i < this.m_alertsArray.Length; i++)
			{
				this.m_alertsArray[i].Cleanup(currentlyKnownDatabaseGuids);
			}
		}

		// Token: 0x040007A2 RID: 1954
		private IDatabaseAlertInfoTable[] m_alertsArray;
	}
}
