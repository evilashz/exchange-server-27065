using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000005 RID: 5
	public enum BudgetMetadata
	{
		// Token: 0x0400000E RID: 14
		ThrottlingRequestType,
		// Token: 0x0400000F RID: 15
		ThrottlingDelay,
		// Token: 0x04000010 RID: 16
		BeginBudgetConnections,
		// Token: 0x04000011 RID: 17
		EndBudgetConnections,
		// Token: 0x04000012 RID: 18
		BeginBudgetHangingConnections,
		// Token: 0x04000013 RID: 19
		EndBudgetHangingConnections,
		// Token: 0x04000014 RID: 20
		BeginBudgetAD,
		// Token: 0x04000015 RID: 21
		EndBudgetAD,
		// Token: 0x04000016 RID: 22
		BeginBudgetCAS,
		// Token: 0x04000017 RID: 23
		EndBudgetCAS,
		// Token: 0x04000018 RID: 24
		BeginBudgetRPC,
		// Token: 0x04000019 RID: 25
		EndBudgetRPC,
		// Token: 0x0400001A RID: 26
		BeginBudgetFindCount,
		// Token: 0x0400001B RID: 27
		EndBudgetFindCount,
		// Token: 0x0400001C RID: 28
		BeginBudgetSubscriptions,
		// Token: 0x0400001D RID: 29
		EndBudgetSubscriptions,
		// Token: 0x0400001E RID: 30
		ThrottlingPolicy,
		// Token: 0x0400001F RID: 31
		MDBResource,
		// Token: 0x04000020 RID: 32
		MDBHealth,
		// Token: 0x04000021 RID: 33
		MDBHistoricalLoad,
		// Token: 0x04000022 RID: 34
		TotalDCRequestCount,
		// Token: 0x04000023 RID: 35
		TotalDCRequestLatency,
		// Token: 0x04000024 RID: 36
		TotalMBXRequestCount,
		// Token: 0x04000025 RID: 37
		TotalMBXRequestLatency,
		// Token: 0x04000026 RID: 38
		MaxConn,
		// Token: 0x04000027 RID: 39
		MaxBurst,
		// Token: 0x04000028 RID: 40
		BeginBalance,
		// Token: 0x04000029 RID: 41
		Cutoff,
		// Token: 0x0400002A RID: 42
		RechargeRate,
		// Token: 0x0400002B RID: 43
		IsServiceAct,
		// Token: 0x0400002C RID: 44
		LiveTime,
		// Token: 0x0400002D RID: 45
		EndBalance
	}
}
