using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000225 RID: 549
	internal class ServerValidationResult : IHealthValidationResultMinimal
	{
		// Token: 0x060014C6 RID: 5318 RVA: 0x00052CE8 File Offset: 0x00050EE8
		public ServerValidationResult(string serverName, Guid serverGuid)
		{
			this.m_serverName = serverName;
			this.m_serverGuid = serverGuid;
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00052CFE File Offset: 0x00050EFE
		public Guid IdentityGuid
		{
			get
			{
				return this.m_serverGuid;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x00052D06 File Offset: 0x00050F06
		public string Identity
		{
			get
			{
				return this.m_serverName;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00052D0E File Offset: 0x00050F0E
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x00052D16 File Offset: 0x00050F16
		public int HealthyCopiesCount { get; set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00052D1F File Offset: 0x00050F1F
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x00052D27 File Offset: 0x00050F27
		public int HealthyPassiveCopiesCount { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x00052D30 File Offset: 0x00050F30
		// (set) Token: 0x060014CE RID: 5326 RVA: 0x00052D38 File Offset: 0x00050F38
		public int TotalPassiveCopiesCount { get; set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00052D41 File Offset: 0x00050F41
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x00052D49 File Offset: 0x00050F49
		public bool IsValidationSuccessful { get; set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00052D52 File Offset: 0x00050F52
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00052D5A File Offset: 0x00050F5A
		public bool IsSiteValidationSuccessful { get; set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00052D63 File Offset: 0x00050F63
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x00052D6B File Offset: 0x00050F6B
		public bool IsAnyCachedCopyStatusStale { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00052D74 File Offset: 0x00050F74
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x00052D7C File Offset: 0x00050F7C
		public string ErrorMessage { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00052D85 File Offset: 0x00050F85
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00052D8D File Offset: 0x00050F8D
		public string ErrorMessageWithoutFullStatus { get; set; }

		// Token: 0x0400080C RID: 2060
		private readonly string m_serverName;

		// Token: 0x0400080D RID: 2061
		private readonly Guid m_serverGuid;
	}
}
