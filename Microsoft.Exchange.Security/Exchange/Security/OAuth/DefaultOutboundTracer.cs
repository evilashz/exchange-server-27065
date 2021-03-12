using System;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000B8 RID: 184
	internal class DefaultOutboundTracer : IOutboundTracer
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x0002E7AE File Offset: 0x0002C9AE
		protected DefaultOutboundTracer()
		{
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0002E7B6 File Offset: 0x0002C9B6
		public static IOutboundTracer Instance
		{
			get
			{
				return DefaultOutboundTracer.instance;
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002E7BD File Offset: 0x0002C9BD
		public void LogInformation(int hashCode, string formatString, params object[] args)
		{
			ExTraceGlobals.OAuthTracer.TraceDebug((long)hashCode, formatString, args);
			this.LogInformation2(hashCode, formatString, args);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002E7D6 File Offset: 0x0002C9D6
		protected virtual void LogInformation2(int hashCode, string formatString, params object[] args)
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0002E7D8 File Offset: 0x0002C9D8
		public void LogWarning(int hashCode, string formatString, params object[] args)
		{
			ExTraceGlobals.OAuthTracer.TraceWarning((long)hashCode, formatString, args);
			this.LogWarning2(hashCode, formatString, args);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0002E7F1 File Offset: 0x0002C9F1
		protected virtual void LogWarning2(int hashCode, string formatString, params object[] args)
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0002E7F3 File Offset: 0x0002C9F3
		public void LogError(int hashCode, string formatString, params object[] args)
		{
			ExTraceGlobals.OAuthTracer.TraceError((long)hashCode, formatString, args);
			this.LogError2(hashCode, formatString, args);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0002E80C File Offset: 0x0002CA0C
		protected virtual void LogError2(int hashCode, string formatString, params object[] args)
		{
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002E80E File Offset: 0x0002CA0E
		public void LogToken(int hashCode, string tokenString)
		{
			ExTraceGlobals.OAuthTracer.TraceError<string>((long)hashCode, "the final token is {0}", tokenString);
			this.LogToken2(hashCode, tokenString);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0002E82A File Offset: 0x0002CA2A
		protected virtual void LogToken2(int hashCode, string tokenString)
		{
		}

		// Token: 0x04000619 RID: 1561
		private static IOutboundTracer instance = new DefaultOutboundTracer();
	}
}
