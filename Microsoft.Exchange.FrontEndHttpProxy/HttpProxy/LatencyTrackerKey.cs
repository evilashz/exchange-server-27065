using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200009D RID: 157
	internal enum LatencyTrackerKey
	{
		// Token: 0x0400039C RID: 924
		CalculateTargetBackEndLatency,
		// Token: 0x0400039D RID: 925
		CalculateTargetBackEndSecondRoundLatency,
		// Token: 0x0400039E RID: 926
		HandlerToModuleSwitchingLatency,
		// Token: 0x0400039F RID: 927
		ModuleToHandlerSwitchingLatency,
		// Token: 0x040003A0 RID: 928
		RequestHandlerLatency,
		// Token: 0x040003A1 RID: 929
		ProxyModuleInitLatency,
		// Token: 0x040003A2 RID: 930
		ProxyModuleLatency,
		// Token: 0x040003A3 RID: 931
		AuthenticationLatency,
		// Token: 0x040003A4 RID: 932
		BackendRequestInitLatency,
		// Token: 0x040003A5 RID: 933
		BackendProcessingLatency,
		// Token: 0x040003A6 RID: 934
		BackendResponseInitLatency,
		// Token: 0x040003A7 RID: 935
		HandlerCompletionLatency,
		// Token: 0x040003A8 RID: 936
		StreamingLatency,
		// Token: 0x040003A9 RID: 937
		RouteRefresherLatency
	}
}
