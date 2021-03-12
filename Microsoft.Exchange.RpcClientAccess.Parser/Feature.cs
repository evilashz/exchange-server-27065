using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020000A7 RID: 167
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Feature
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0000E0E7 File Offset: 0x0000C2E7
		public static RopExecutionException NotImplemented(int bugNumber, string message)
		{
			ExTraceGlobals.NotImplementedTracer.TraceError(bugNumber, Feature.TraceContextGetter(), message);
			return new RopExecutionException(string.Format("#{0}: {1} is not yet implemented", bugNumber, message), (ErrorCode)2147749887U);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000E11A File Offset: 0x0000C31A
		public static void Stubbed(int bugNumber, string message)
		{
			ExTraceGlobals.NotImplementedTracer.TraceWarning(bugNumber, Feature.TraceContextGetter(), message);
		}

		// Token: 0x04000271 RID: 625
		public static Func<long> TraceContextGetter = () => 0L;
	}
}
