using System;
using Microsoft.Exchange.Cluster.Replay.Monitoring;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A6 RID: 2214
	[Serializable]
	public sealed class ServerRedundancy : IConfigurable
	{
		// Token: 0x06004E04 RID: 19972 RVA: 0x00144218 File Offset: 0x00142418
		internal ServerRedundancy(HealthInfoPersisted healthInfo, ServerHealthInfoPersisted serverHealth, string serverContactedFqdn)
		{
			AmServerName amServerName = new AmServerName(serverHealth.ServerFqdn);
			this.Identity = new ConfigObjectId(amServerName.NetbiosName.ToUpperInvariant());
			this.ServerContactedFqdn = serverContactedFqdn.ToUpperInvariant();
			this.HealthInfoCreateTime = DateTimeHelper.ParseIntoNullableLocalDateTimeIfPossible(healthInfo.CreateTimeUtcStr);
			this.HealthInfoLastUpdateTime = DateTimeHelper.ParseIntoNullableLocalDateTimeIfPossible(healthInfo.LastUpdateTimeUtcStr);
			this.ServerFoundInAD = TransitionInfo.ConstructFromRemoteSerializable(serverHealth.ServerFoundInAD);
			this.IsServerFoundInAD = this.ServerFoundInAD.IsSuccess;
			this.CriticalForMaintainingAvailability = TransitionInfo.ConstructFromRemoteSerializable(serverHealth.CriticalForMaintainingAvailability);
			this.CriticalForMaintainingRedundancy = TransitionInfo.ConstructFromRemoteSerializable(serverHealth.CriticalForMaintainingRedundancy);
			this.CriticalForRestoringAvailability = TransitionInfo.ConstructFromRemoteSerializable(serverHealth.CriticalForRestoringAvailability);
			this.CriticalForRestoringRedundancy = TransitionInfo.ConstructFromRemoteSerializable(serverHealth.CriticalForRestoringRedundancy);
			this.HighForRestoringAvailability = TransitionInfo.ConstructFromRemoteSerializable(serverHealth.HighForRestoringAvailability);
			this.HighForRestoringRedundancy = TransitionInfo.ConstructFromRemoteSerializable(serverHealth.HighForRestoringRedundancy);
			this.SetRepairUrgency();
			this.SetSafeForMaintenance();
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x0014430F File Offset: 0x0014250F
		// (set) Token: 0x06004E06 RID: 19974 RVA: 0x00144317 File Offset: 0x00142517
		public ObjectId Identity { get; private set; }

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x00144320 File Offset: 0x00142520
		// (set) Token: 0x06004E08 RID: 19976 RVA: 0x00144328 File Offset: 0x00142528
		public bool IsServerFoundInAD { get; private set; }

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x00144331 File Offset: 0x00142531
		// (set) Token: 0x06004E0A RID: 19978 RVA: 0x00144339 File Offset: 0x00142539
		public RepairUrgency RepairUrgency { get; private set; }

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x06004E0B RID: 19979 RVA: 0x00144342 File Offset: 0x00142542
		// (set) Token: 0x06004E0C RID: 19980 RVA: 0x0014434A File Offset: 0x0014254A
		public bool SafeForMaintenance { get; private set; }

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x00144353 File Offset: 0x00142553
		// (set) Token: 0x06004E0E RID: 19982 RVA: 0x0014435B File Offset: 0x0014255B
		public string ServerContactedFqdn { get; private set; }

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x06004E0F RID: 19983 RVA: 0x00144364 File Offset: 0x00142564
		// (set) Token: 0x06004E10 RID: 19984 RVA: 0x0014436C File Offset: 0x0014256C
		public DateTime? HealthInfoCreateTime { get; private set; }

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06004E11 RID: 19985 RVA: 0x00144375 File Offset: 0x00142575
		// (set) Token: 0x06004E12 RID: 19986 RVA: 0x0014437D File Offset: 0x0014257D
		public DateTime? HealthInfoLastUpdateTime { get; private set; }

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06004E13 RID: 19987 RVA: 0x00144386 File Offset: 0x00142586
		// (set) Token: 0x06004E14 RID: 19988 RVA: 0x0014438E File Offset: 0x0014258E
		public TransitionInfo ServerFoundInAD { get; private set; }

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06004E15 RID: 19989 RVA: 0x00144397 File Offset: 0x00142597
		// (set) Token: 0x06004E16 RID: 19990 RVA: 0x0014439F File Offset: 0x0014259F
		public TransitionInfo CriticalForMaintainingAvailability { get; set; }

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x06004E17 RID: 19991 RVA: 0x001443A8 File Offset: 0x001425A8
		// (set) Token: 0x06004E18 RID: 19992 RVA: 0x001443B0 File Offset: 0x001425B0
		public TransitionInfo CriticalForMaintainingRedundancy { get; set; }

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x06004E19 RID: 19993 RVA: 0x001443B9 File Offset: 0x001425B9
		// (set) Token: 0x06004E1A RID: 19994 RVA: 0x001443C1 File Offset: 0x001425C1
		public TransitionInfo CriticalForRestoringAvailability { get; set; }

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06004E1B RID: 19995 RVA: 0x001443CA File Offset: 0x001425CA
		// (set) Token: 0x06004E1C RID: 19996 RVA: 0x001443D2 File Offset: 0x001425D2
		public TransitionInfo CriticalForRestoringRedundancy { get; set; }

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06004E1D RID: 19997 RVA: 0x001443DB File Offset: 0x001425DB
		// (set) Token: 0x06004E1E RID: 19998 RVA: 0x001443E3 File Offset: 0x001425E3
		public TransitionInfo HighForRestoringAvailability { get; set; }

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06004E1F RID: 19999 RVA: 0x001443EC File Offset: 0x001425EC
		// (set) Token: 0x06004E20 RID: 20000 RVA: 0x001443F4 File Offset: 0x001425F4
		public TransitionInfo HighForRestoringRedundancy { get; set; }

		// Token: 0x06004E21 RID: 20001 RVA: 0x00144400 File Offset: 0x00142600
		public static string GetRepairUrgencyDisplayString(RepairUrgency urgency)
		{
			string text = LocalizedDescriptionAttribute.FromEnum(typeof(RepairUrgency), urgency);
			if (!string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			return urgency.ToString();
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x06004E22 RID: 20002 RVA: 0x00144438 File Offset: 0x00142638
		internal bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06004E23 RID: 20003 RVA: 0x0014443B File Offset: 0x0014263B
		bool IConfigurable.IsValid
		{
			get
			{
				return this.IsValid;
			}
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06004E24 RID: 20004 RVA: 0x00144443 File Offset: 0x00142643
		internal ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x06004E25 RID: 20005 RVA: 0x00144446 File Offset: 0x00142646
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return this.ObjectState;
			}
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x0014444E File Offset: 0x0014264E
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x00144456 File Offset: 0x00142656
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x0014445D File Offset: 0x0014265D
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x00144464 File Offset: 0x00142664
		private void SetRepairUrgency()
		{
			if (this.CriticalForMaintainingAvailability.IsSuccess || this.CriticalForMaintainingRedundancy.IsSuccess)
			{
				this.RepairUrgency = RepairUrgency.Prohibited;
				return;
			}
			if (this.CriticalForRestoringAvailability.IsSuccess || this.CriticalForRestoringRedundancy.IsSuccess)
			{
				this.RepairUrgency = RepairUrgency.Critical;
				return;
			}
			if (this.HighForRestoringAvailability.IsSuccess || this.HighForRestoringRedundancy.IsSuccess)
			{
				this.RepairUrgency = RepairUrgency.High;
				return;
			}
			this.RepairUrgency = RepairUrgency.Normal;
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x001444E0 File Offset: 0x001426E0
		private void SetSafeForMaintenance()
		{
			if (this.CriticalForMaintainingAvailability.IsSuccess || this.CriticalForMaintainingRedundancy.IsSuccess || this.CriticalForRestoringAvailability.IsSuccess || this.CriticalForRestoringRedundancy.IsSuccess)
			{
				this.SafeForMaintenance = false;
				return;
			}
			this.SafeForMaintenance = true;
		}
	}
}
