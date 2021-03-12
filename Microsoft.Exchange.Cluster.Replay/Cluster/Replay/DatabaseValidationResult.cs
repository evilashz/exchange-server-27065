using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000224 RID: 548
	internal class DatabaseValidationResult : IHealthValidationResult, IHealthValidationResultMinimal
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00052998 File Offset: 0x00050B98
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0005299F File Offset: 0x00050B9F
		public DatabaseValidationResult(string databaseName, Guid databaseGuid, AmServerName targetServer, int numHealthyCopiesMin) : this(databaseName, databaseGuid, targetServer, numHealthyCopiesMin, 0)
		{
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000529AD File Offset: 0x00050BAD
		public DatabaseValidationResult(string databaseName, Guid databaseGuid, AmServerName targetServer, int numHealthyCopiesMin, int numHealthyPassiveCopiesMin)
		{
			this.m_databaseName = databaseName;
			this.m_databaseGuid = databaseGuid;
			this.m_targetServerName = targetServer;
			this.m_numHealthyCopiesMin = numHealthyCopiesMin;
			this.m_numHealthyPassiveCopiesMin = numHealthyPassiveCopiesMin;
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x000529DA File Offset: 0x00050BDA
		public Guid IdentityGuid
		{
			get
			{
				return this.m_databaseGuid;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x000529E2 File Offset: 0x00050BE2
		public string Identity
		{
			get
			{
				return this.m_databaseName;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x000529EA File Offset: 0x00050BEA
		public Guid DatabaseGuid
		{
			get
			{
				return this.m_databaseGuid;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x000529F2 File Offset: 0x00050BF2
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x000529FA File Offset: 0x00050BFA
		public int MinimumNumHealthyCopies
		{
			get
			{
				return this.m_numHealthyCopiesMin;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x00052A02 File Offset: 0x00050C02
		public int MinimumNumHealthyPassiveCopies
		{
			get
			{
				return this.m_numHealthyPassiveCopiesMin;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00052A0A File Offset: 0x00050C0A
		public bool IsSiteValidationSuccessful
		{
			get
			{
				if (this.m_isSiteValidationSuccessful != null)
				{
					return this.m_isSiteValidationSuccessful.Value;
				}
				this.m_isSiteValidationSuccessful = new bool?(this.IsSiteValidationSuccessfulImpl());
				return this.m_isSiteValidationSuccessful.Value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x00052A41 File Offset: 0x00050C41
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x00052A49 File Offset: 0x00050C49
		public bool IsTargetCopyHealthy { get; private set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00052A52 File Offset: 0x00050C52
		// (set) Token: 0x060014AF RID: 5295 RVA: 0x00052A5A File Offset: 0x00050C5A
		public bool IsAnyCachedCopyStatusStale { get; set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00052A63 File Offset: 0x00050C63
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x00052A6B File Offset: 0x00050C6B
		public bool IsActiveCopyHealthy { get; private set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x00052A74 File Offset: 0x00050C74
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x00052A7C File Offset: 0x00050C7C
		public int HealthyCopiesCount { get; private set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00052A85 File Offset: 0x00050C85
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00052A8D File Offset: 0x00050C8D
		public int HealthyPassiveCopiesCount { get; private set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00052A96 File Offset: 0x00050C96
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x00052A9E File Offset: 0x00050C9E
		public int TotalPassiveCopiesCount { get; private set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00052AA7 File Offset: 0x00050CA7
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x00052AAF File Offset: 0x00050CAF
		public bool IsValidationSuccessful { get; private set; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00052AB8 File Offset: 0x00050CB8
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x00052AC0 File Offset: 0x00050CC0
		public CopyStatusClientCachedEntry TargetCopyStatus { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00052AC9 File Offset: 0x00050CC9
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x00052AD1 File Offset: 0x00050CD1
		public CopyStatusClientCachedEntry ActiveCopyStatus { get; set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00052ADA File Offset: 0x00050CDA
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x00052AE2 File Offset: 0x00050CE2
		public string ErrorMessage { get; set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00052AEB File Offset: 0x00050CEB
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x00052AF3 File Offset: 0x00050CF3
		public string ErrorMessageWithoutFullStatus { get; set; }

		// Token: 0x060014C2 RID: 5314 RVA: 0x00052AFC File Offset: 0x00050CFC
		public void ReportHealthyCopy(AmServerName specificServer, string adSiteName)
		{
			DatabaseValidationResult.Tracer.TraceDebug<AmServerName>((long)this.GetHashCode(), "ReportHealthy: {0}", specificServer);
			this.HealthyCopiesCount++;
			if (specificServer.Equals(this.m_targetServerName))
			{
				this.IsTargetCopyHealthy = true;
			}
			if (this.ActiveCopyStatus != null && specificServer.Equals(this.ActiveCopyStatus.ActiveServer))
			{
				this.IsActiveCopyHealthy = true;
			}
			else
			{
				this.HealthyPassiveCopiesCount++;
				this.TotalPassiveCopiesCount++;
			}
			if (this.HealthyCopiesCount < this.m_numHealthyCopiesMin || this.HealthyPassiveCopiesCount < this.m_numHealthyPassiveCopiesMin)
			{
				this.IsValidationSuccessful = false;
			}
			else
			{
				this.IsValidationSuccessful = true;
			}
			if (!string.IsNullOrEmpty(adSiteName))
			{
				this.IncrementHealthyCountInSite(adSiteName, true);
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00052BC0 File Offset: 0x00050DC0
		public void ReportFailedCopy(AmServerName specificServer, string adSiteName)
		{
			DatabaseValidationResult.Tracer.TraceDebug<AmServerName>((long)this.GetHashCode(), "ReportFailed: {0}", specificServer);
			if (this.ActiveCopyStatus == null || !specificServer.Equals(this.ActiveCopyStatus.ActiveServer))
			{
				this.TotalPassiveCopiesCount++;
			}
			if (!string.IsNullOrEmpty(adSiteName))
			{
				this.IncrementHealthyCountInSite(adSiteName, false);
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00052C20 File Offset: 0x00050E20
		private void IncrementHealthyCountInSite(string adSiteName, bool isCopyHealthy)
		{
			if (this.m_healthyCountPerSite == null)
			{
				this.m_healthyCountPerSite = new Dictionary<string, int>(5);
			}
			int num = isCopyHealthy ? 1 : 0;
			int num2;
			this.m_healthyCountPerSite.TryGetValue(adSiteName, out num2);
			this.m_healthyCountPerSite[adSiteName] = num2 + num;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00052C68 File Offset: 0x00050E68
		private bool IsSiteValidationSuccessfulImpl()
		{
			if (this.m_healthyCountPerSite == null || this.m_healthyCountPerSite.Count == 0)
			{
				return false;
			}
			int num = 0;
			foreach (KeyValuePair<string, int> keyValuePair in this.m_healthyCountPerSite)
			{
				if (keyValuePair.Value >= 1)
				{
					num++;
				}
				if (num >= 2)
				{
					break;
				}
			}
			return num >= 2;
		}

		// Token: 0x040007FA RID: 2042
		private readonly int m_numHealthyCopiesMin;

		// Token: 0x040007FB RID: 2043
		private readonly int m_numHealthyPassiveCopiesMin;

		// Token: 0x040007FC RID: 2044
		private readonly string m_databaseName;

		// Token: 0x040007FD RID: 2045
		private readonly Guid m_databaseGuid;

		// Token: 0x040007FE RID: 2046
		private readonly AmServerName m_targetServerName;

		// Token: 0x040007FF RID: 2047
		private Dictionary<string, int> m_healthyCountPerSite;

		// Token: 0x04000800 RID: 2048
		private bool? m_isSiteValidationSuccessful;
	}
}
