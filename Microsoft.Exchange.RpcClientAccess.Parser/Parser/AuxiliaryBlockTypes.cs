using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000006 RID: 6
	internal enum AuxiliaryBlockTypes : byte
	{
		// Token: 0x04000010 RID: 16
		PerfRequestId = 1,
		// Token: 0x04000011 RID: 17
		PerfClientInfo,
		// Token: 0x04000012 RID: 18
		PerfServerInfo,
		// Token: 0x04000013 RID: 19
		PerfSessionInfo,
		// Token: 0x04000014 RID: 20
		PerfDefMdbSuccess,
		// Token: 0x04000015 RID: 21
		PerfDefGcSuccess,
		// Token: 0x04000016 RID: 22
		PerfMdbSuccess,
		// Token: 0x04000017 RID: 23
		PerfGcSuccess,
		// Token: 0x04000018 RID: 24
		PerfFailure,
		// Token: 0x04000019 RID: 25
		ClientControl,
		// Token: 0x0400001A RID: 26
		PerfProcessInfo,
		// Token: 0x0400001B RID: 27
		PerfBgDefMdbSuccess,
		// Token: 0x0400001C RID: 28
		PerfBgDefGcSuccess,
		// Token: 0x0400001D RID: 29
		PerfBgMdbSuccess,
		// Token: 0x0400001E RID: 30
		PerfBgGcSuccess,
		// Token: 0x0400001F RID: 31
		PerfBgFailure,
		// Token: 0x04000020 RID: 32
		PerfFgDefMdbSuccess,
		// Token: 0x04000021 RID: 33
		PerfFgDefGcSuccess,
		// Token: 0x04000022 RID: 34
		PerfFgMdbSuccess,
		// Token: 0x04000023 RID: 35
		PerfFgGcSuccess,
		// Token: 0x04000024 RID: 36
		PerfFgFailure,
		// Token: 0x04000025 RID: 37
		OsInfo,
		// Token: 0x04000026 RID: 38
		ExOrgInfo,
		// Token: 0x04000027 RID: 39
		DiagCtxReqId = 64,
		// Token: 0x04000028 RID: 40
		DiagCtxCtxData,
		// Token: 0x04000029 RID: 41
		DiagCtxMapiServer,
		// Token: 0x0400002A RID: 42
		MapiEndpoint,
		// Token: 0x0400002B RID: 43
		PerRpcStatistics,
		// Token: 0x0400002C RID: 44
		ClientSessionInfo,
		// Token: 0x0400002D RID: 45
		ServerCapabilities,
		// Token: 0x0400002E RID: 46
		DiagCtxClientId,
		// Token: 0x0400002F RID: 47
		EndpointCapabilities,
		// Token: 0x04000030 RID: 48
		ExceptionTrace,
		// Token: 0x04000031 RID: 49
		ClientConnectionInfo,
		// Token: 0x04000032 RID: 50
		ServerSessionInfo,
		// Token: 0x04000033 RID: 51
		SetMonitoringContext,
		// Token: 0x04000034 RID: 52
		ClientActivity,
		// Token: 0x04000035 RID: 53
		ProtocolDeviceIdentification,
		// Token: 0x04000036 RID: 54
		MonitoringActivity,
		// Token: 0x04000037 RID: 55
		ServerInformation,
		// Token: 0x04000038 RID: 56
		IdentityCorrelationInfo
	}
}
