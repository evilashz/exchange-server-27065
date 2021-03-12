using System;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000B7 RID: 183
	internal interface IOutboundTracer
	{
		// Token: 0x06000627 RID: 1575
		void LogInformation(int hashCode, string formatString, params object[] args);

		// Token: 0x06000628 RID: 1576
		void LogWarning(int hashCode, string formatString, params object[] args);

		// Token: 0x06000629 RID: 1577
		void LogError(int hashCode, string formatString, params object[] args);

		// Token: 0x0600062A RID: 1578
		void LogToken(int hashCode, string tokenString);
	}
}
