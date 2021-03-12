using System;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000088 RID: 136
	internal class FaultInjection
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0001934D File Offset: 0x0001754D
		public static void GenerateFault(FaultInjection.LIDs faultLid)
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest((uint)faultLid);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001935C File Offset: 0x0001755C
		public static T TraceTest<T>(FaultInjection.LIDs faultLid)
		{
			T result = default(T);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<T>((uint)faultLid, ref result);
			return result;
		}

		// Token: 0x02000089 RID: 137
		internal enum LIDs : uint
		{
			// Token: 0x04000363 RID: 867
			ShouldFailSmtpAnchorMailboxADLookup = 1378318050U,
			// Token: 0x04000364 RID: 868
			ProxyToLowerVersionEws = 2357603645U,
			// Token: 0x04000365 RID: 869
			ProxyToLowerVersionEwsOAuthIdentityActAsUserNullSid = 3431345469U,
			// Token: 0x04000366 RID: 870
			ExceptionDuringProxyDownLevelCheckNullSid_ChangeValue = 3548785981U,
			// Token: 0x04000367 RID: 871
			AnchorMailboxDatabaseCacheEntry = 4134939965U
		}
	}
}
