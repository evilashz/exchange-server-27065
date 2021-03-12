using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000009 RID: 9
	public enum LogKey
	{
		// Token: 0x04000013 RID: 19
		DateTime,
		// Token: 0x04000014 RID: 20
		RequestId,
		// Token: 0x04000015 RID: 21
		MajorVersion,
		// Token: 0x04000016 RID: 22
		MinorVersion,
		// Token: 0x04000017 RID: 23
		BuildVersion,
		// Token: 0x04000018 RID: 24
		RevisionVersion,
		// Token: 0x04000019 RID: 25
		ClientRequestId,
		// Token: 0x0400001A RID: 26
		Protocol,
		// Token: 0x0400001B RID: 27
		UrlHost,
		// Token: 0x0400001C RID: 28
		UrlStem,
		// Token: 0x0400001D RID: 29
		ProtocolAction,
		// Token: 0x0400001E RID: 30
		AuthenticationType,
		// Token: 0x0400001F RID: 31
		IsAuthenticated,
		// Token: 0x04000020 RID: 32
		AuthenticatedUser,
		// Token: 0x04000021 RID: 33
		Organization,
		// Token: 0x04000022 RID: 34
		AnchorMailbox,
		// Token: 0x04000023 RID: 35
		UserAgent,
		// Token: 0x04000024 RID: 36
		ClientIpAddress,
		// Token: 0x04000025 RID: 37
		ServerHostName,
		// Token: 0x04000026 RID: 38
		HttpStatus,
		// Token: 0x04000027 RID: 39
		BackendStatus,
		// Token: 0x04000028 RID: 40
		ErrorCode,
		// Token: 0x04000029 RID: 41
		Method,
		// Token: 0x0400002A RID: 42
		ProxyAction,
		// Token: 0x0400002B RID: 43
		TargetServer,
		// Token: 0x0400002C RID: 44
		TargetServerVersion,
		// Token: 0x0400002D RID: 45
		RoutingType,
		// Token: 0x0400002E RID: 46
		RoutingHint,
		// Token: 0x0400002F RID: 47
		BackendCookie,
		// Token: 0x04000030 RID: 48
		ServerLocatorHost,
		// Token: 0x04000031 RID: 49
		ServerLocatorLatency,
		// Token: 0x04000032 RID: 50
		RequestBytes,
		// Token: 0x04000033 RID: 51
		ResponseBytes,
		// Token: 0x04000034 RID: 52
		TargetOutstandingRequests,
		// Token: 0x04000035 RID: 53
		AuthModulePerfContext,
		// Token: 0x04000036 RID: 54
		HttpPipelineLatency,
		// Token: 0x04000037 RID: 55
		CalculateTargetBackEndLatency,
		// Token: 0x04000038 RID: 56
		GlsLatencyBreakup,
		// Token: 0x04000039 RID: 57
		TotalGlsLatency,
		// Token: 0x0400003A RID: 58
		AccountForestLatencyBreakup,
		// Token: 0x0400003B RID: 59
		TotalAccountForestLatency,
		// Token: 0x0400003C RID: 60
		ResourceForestLatencyBreakup,
		// Token: 0x0400003D RID: 61
		TotalResourceForestLatency,
		// Token: 0x0400003E RID: 62
		ADLatency,
		// Token: 0x0400003F RID: 63
		SharedCacheLatencyBreakup,
		// Token: 0x04000040 RID: 64
		TotalSharedCacheLatency,
		// Token: 0x04000041 RID: 65
		ActivityContextLifeTime,
		// Token: 0x04000042 RID: 66
		ModuleToHandlerSwitchingLatency,
		// Token: 0x04000043 RID: 67
		ClientReqStreamLatency,
		// Token: 0x04000044 RID: 68
		BackendReqInitLatency,
		// Token: 0x04000045 RID: 69
		BackendReqStreamLatency,
		// Token: 0x04000046 RID: 70
		BackendProcessingLatency,
		// Token: 0x04000047 RID: 71
		BackendRespInitLatency,
		// Token: 0x04000048 RID: 72
		BackendRespStreamLatency,
		// Token: 0x04000049 RID: 73
		ClientRespStreamLatency,
		// Token: 0x0400004A RID: 74
		KerberosAuthHeaderLatency,
		// Token: 0x0400004B RID: 75
		HandlerCompletionLatency,
		// Token: 0x0400004C RID: 76
		RequestHandlerLatency,
		// Token: 0x0400004D RID: 77
		HandlerToModuleSwitchingLatency,
		// Token: 0x0400004E RID: 78
		ProxyTime,
		// Token: 0x0400004F RID: 79
		CoreLatency,
		// Token: 0x04000050 RID: 80
		RoutingLatency,
		// Token: 0x04000051 RID: 81
		HttpProxyOverhead,
		// Token: 0x04000052 RID: 82
		TotalRequestTime,
		// Token: 0x04000053 RID: 83
		RouteRefresherLatency,
		// Token: 0x04000054 RID: 84
		UrlQuery,
		// Token: 0x04000055 RID: 85
		BackEndGenericInfo,
		// Token: 0x04000056 RID: 86
		GenericInfo,
		// Token: 0x04000057 RID: 87
		GenericErrors
	}
}
