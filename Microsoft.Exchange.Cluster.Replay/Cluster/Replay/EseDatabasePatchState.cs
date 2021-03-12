using System;
using System.Text;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001BD RID: 445
	[Serializable]
	internal class EseDatabasePatchState : IEquatable<EseDatabasePatchState>
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00046E54 File Offset: 0x00045054
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.IncrementalReseederTracer;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00046E5B File Offset: 0x0004505B
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x00046E63 File Offset: 0x00045063
		public Version Version { get; private set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00046E6C File Offset: 0x0004506C
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00046E74 File Offset: 0x00045074
		public EseDatabasePatchComponent ComponentId { get; private set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00046E7D File Offset: 0x0004507D
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x00046E85 File Offset: 0x00045085
		public Guid DatabaseGuid { get; private set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00046E8E File Offset: 0x0004508E
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x00046E96 File Offset: 0x00045096
		public string ServerName { get; private set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x00046E9F File Offset: 0x0004509F
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x00046EA7 File Offset: 0x000450A7
		public long PageSizeBytes { get; private set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x00046EB0 File Offset: 0x000450B0
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x00046EB8 File Offset: 0x000450B8
		public bool IsPatchCompleted { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00046EC1 File Offset: 0x000450C1
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x00046EC9 File Offset: 0x000450C9
		public int NumPagesToPatch { get; set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00046ED2 File Offset: 0x000450D2
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x00046EDA File Offset: 0x000450DA
		public long FirstDivergedLogGen { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00046EE3 File Offset: 0x000450E3
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x00046EEB File Offset: 0x000450EB
		public long LowestRequiredLogGen { get; set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00046EF4 File Offset: 0x000450F4
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x00046EFC File Offset: 0x000450FC
		public long HighestRequiredLogGen { get; set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x00046F05 File Offset: 0x00045105
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x00046F0D File Offset: 0x0004510D
		public EseDatabasePatchState.PatchPhase CurrentPatchPhase { get; set; }

		// Token: 0x0600116D RID: 4461 RVA: 0x00046F16 File Offset: 0x00045116
		public EseDatabasePatchState(Guid dbGuid, EseDatabasePatchComponent patchComponent, long pageSizeBytes)
		{
			this.Version = EseDatabasePatchState.VersionNumber;
			this.ServerName = Environment.MachineName;
			this.ComponentId = patchComponent;
			this.DatabaseGuid = dbGuid;
			this.PageSizeBytes = pageSizeBytes;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00046F4C File Offset: 0x0004514C
		public bool Equals(EseDatabasePatchState other)
		{
			return other != null && (object.ReferenceEquals(other, this) || (this.Version.Equals(other.Version) && this.ComponentId == other.ComponentId && this.DatabaseGuid.Equals(other.DatabaseGuid) && SharedHelper.StringIEquals(this.ServerName, other.ServerName) && this.PageSizeBytes == other.PageSizeBytes && this.CurrentPatchPhase == other.CurrentPatchPhase && this.IsPatchCompleted == other.IsPatchCompleted && this.NumPagesToPatch == other.NumPagesToPatch && this.FirstDivergedLogGen == other.FirstDivergedLogGen && this.LowestRequiredLogGen == other.LowestRequiredLogGen && this.HighestRequiredLogGen == other.HighestRequiredLogGen));
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00047020 File Offset: 0x00045220
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.AppendFormat("DatabaseGuid={0}, ", this.DatabaseGuid);
			stringBuilder.AppendFormat("Version={0}, ", this.Version);
			stringBuilder.AppendFormat("ComponentId={0}, ", this.ComponentId);
			stringBuilder.AppendFormat("ServerName={0}, ", this.ServerName);
			stringBuilder.AppendFormat("PageSizeBytes={0}, ", this.PageSizeBytes);
			stringBuilder.AppendFormat("CurrentPatchPhase={0}, ", this.CurrentPatchPhase);
			stringBuilder.AppendFormat("IsPatchCompleted={0}, ", this.IsPatchCompleted);
			stringBuilder.AppendFormat("NumPagesToPatch={0}, ", this.NumPagesToPatch);
			stringBuilder.AppendFormat("FirstDivergedLogGen={0}, ", this.FirstDivergedLogGen);
			stringBuilder.AppendFormat("LowestRequiredLogGen={0}, ", this.LowestRequiredLogGen);
			stringBuilder.AppendFormat("HighestRequiredLogGen={0}", this.HighestRequiredLogGen);
			return stringBuilder.ToString();
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00047134 File Offset: 0x00045334
		public void Validate(Guid dbGuid, long pageSizeBytes, string patchFile)
		{
			bool flag = true;
			if (flag && (this.Version.Major != EseDatabasePatchState.VersionNumber.Major || this.Version.Minor > EseDatabasePatchState.VersionNumber.Minor))
			{
				EseDatabasePatchState.Tracer.TraceError<Version, Version>((long)this.GetHashCode(), "Patch file validation failed! Version number is not supported. Actual: {0}; Supported: {1}", this.Version, EseDatabasePatchState.VersionNumber);
				flag = false;
			}
			if (flag && !SharedHelper.StringIEquals(this.ServerName, Environment.MachineName))
			{
				EseDatabasePatchState.Tracer.TraceError<string, string>((long)this.GetHashCode(), "Patch file validation failed! Actual ServerName: {0}; Expected: {1}", this.ServerName, Environment.MachineName);
				flag = false;
			}
			if (flag && this.PageSizeBytes != pageSizeBytes)
			{
				EseDatabasePatchState.Tracer.TraceError<long, long>((long)this.GetHashCode(), "Patch file validation failed! Actual PageSizeBytes: {0}; Expected: {1}", this.PageSizeBytes, pageSizeBytes);
				flag = false;
			}
			if (flag && !dbGuid.Equals(this.DatabaseGuid))
			{
				EseDatabasePatchState.Tracer.TraceError<Guid, Guid>((long)this.GetHashCode(), "Patch file validation failed! Actual DatabaseGuid: {0}; Expected: {1}", this.DatabaseGuid, dbGuid);
				flag = false;
			}
			if (!flag)
			{
				throw new PagePatchInvalidFileException(patchFile);
			}
		}

		// Token: 0x04000699 RID: 1689
		public static readonly Version VersionNumber = new Version(1, 0);

		// Token: 0x020001BE RID: 446
		public enum PatchPhase
		{
			// Token: 0x040006A6 RID: 1702
			GatheringPatchData,
			// Token: 0x040006A7 RID: 1703
			GatheringComplete,
			// Token: 0x040006A8 RID: 1704
			PatchingPages,
			// Token: 0x040006A9 RID: 1705
			PatchingPagesComplete,
			// Token: 0x040006AA RID: 1706
			MovingOldLogs,
			// Token: 0x040006AB RID: 1707
			MovingNewLogs,
			// Token: 0x040006AC RID: 1708
			LogReplacementComplete
		}
	}
}
