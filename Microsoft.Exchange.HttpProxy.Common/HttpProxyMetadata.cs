using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000018 RID: 24
	internal enum HttpProxyMetadata
	{
		// Token: 0x0400008A RID: 138
		AnchorMailbox,
		// Token: 0x0400008B RID: 139
		RoutingType,
		// Token: 0x0400008C RID: 140
		RoutingHint,
		// Token: 0x0400008D RID: 141
		BackEndCookie,
		// Token: 0x0400008E RID: 142
		HandlerToModuleSwitchingLatency,
		// Token: 0x0400008F RID: 143
		Protocol,
		// Token: 0x04000090 RID: 144
		ProxyAction,
		// Token: 0x04000091 RID: 145
		CalculateTargetBackendLatency,
		// Token: 0x04000092 RID: 146
		ModuleToHandlerSwitchingLatency,
		// Token: 0x04000093 RID: 147
		KerberosAuthHeaderLatency,
		// Token: 0x04000094 RID: 148
		HandlerCompletionLatency,
		// Token: 0x04000095 RID: 149
		RequestHandlerLatency,
		// Token: 0x04000096 RID: 150
		TargetServer,
		// Token: 0x04000097 RID: 151
		TargetServerVersion,
		// Token: 0x04000098 RID: 152
		TotalProxyingLatency,
		// Token: 0x04000099 RID: 153
		TotalRequestTime,
		// Token: 0x0400009A RID: 154
		TargetOutstandingRequests,
		// Token: 0x0400009B RID: 155
		AuthModulePerfContext,
		// Token: 0x0400009C RID: 156
		ServerLocatorLatency,
		// Token: 0x0400009D RID: 157
		ServerLocatorHost,
		// Token: 0x0400009E RID: 158
		ProtocolAction,
		// Token: 0x0400009F RID: 159
		UrlHost,
		// Token: 0x040000A0 RID: 160
		UrlStem,
		// Token: 0x040000A1 RID: 161
		UrlQuery,
		// Token: 0x040000A2 RID: 162
		BackEndStatus,
		// Token: 0x040000A3 RID: 163
		GlsLatencyBreakup,
		// Token: 0x040000A4 RID: 164
		TotalGlsLatency,
		// Token: 0x040000A5 RID: 165
		AccountForestLatencyBreakup,
		// Token: 0x040000A6 RID: 166
		TotalAccountForestLatency,
		// Token: 0x040000A7 RID: 167
		ResourceForestLatencyBreakup,
		// Token: 0x040000A8 RID: 168
		TotalResourceForestLatency,
		// Token: 0x040000A9 RID: 169
		SharedCacheLatencyBreakup,
		// Token: 0x040000AA RID: 170
		TotalSharedCacheLatency,
		// Token: 0x040000AB RID: 171
		ClientRequestStreamingLatency,
		// Token: 0x040000AC RID: 172
		BackendRequestInitLatency,
		// Token: 0x040000AD RID: 173
		BackendRequestStreamingLatency,
		// Token: 0x040000AE RID: 174
		BackendProcessingLatency,
		// Token: 0x040000AF RID: 175
		BackendResponseInitLatency,
		// Token: 0x040000B0 RID: 176
		BackendResponseStreamingLatency,
		// Token: 0x040000B1 RID: 177
		ClientResponseStreamingLatency,
		// Token: 0x040000B2 RID: 178
		CoreLatency,
		// Token: 0x040000B3 RID: 179
		RoutingLatency,
		// Token: 0x040000B4 RID: 180
		HttpProxyOverhead,
		// Token: 0x040000B5 RID: 181
		RouteRefresherLatency
	}
}
