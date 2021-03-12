using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000010 RID: 16
	public interface ITracer
	{
		// Token: 0x06000052 RID: 82
		void TraceDebug(string message);

		// Token: 0x06000053 RID: 83
		void TraceDebug(string formatString, params object[] args);

		// Token: 0x06000054 RID: 84
		void TraceWarning(string message);

		// Token: 0x06000055 RID: 85
		void TraceWarning(string formatString, params object[] args);

		// Token: 0x06000056 RID: 86
		void TraceError(string message);

		// Token: 0x06000057 RID: 87
		void TraceError(string formatString, params object[] args);
	}
}
