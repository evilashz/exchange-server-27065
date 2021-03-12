using System;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000025 RID: 37
	internal class FaultInjection
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000092A8 File Offset: 0x000074A8
		public static T TraceTest<T>(FaultInjection.LIDs faultLid)
		{
			T result = default(T);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<T>((uint)faultLid, ref result);
			return result;
		}

		// Token: 0x02000026 RID: 38
		internal enum LIDs : uint
		{
			// Token: 0x04000177 RID: 375
			SerializedOAuthIdentity_ChangeValue = 2697342269U,
			// Token: 0x04000178 RID: 376
			OAuthAuthenticationRequest_SleepTime = 2177248573U,
			// Token: 0x04000179 RID: 377
			X509CertSubject_ChangeValue = 2634427709U,
			// Token: 0x0400017A RID: 378
			EnforceOAuthCommonAccessTokenVersion1_ChangeValue = 3481677117U,
			// Token: 0x0400017B RID: 379
			ExceptionDuringOAuthCATGeneration_ChangeValue = 3011915069U,
			// Token: 0x0400017C RID: 380
			ExceptionDuringRehydration_ChangeValue = 4085656893U
		}
	}
}
