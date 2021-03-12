using System;
using System.Threading;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000054 RID: 84
	internal static class SessionId
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000B03A File Offset: 0x0000923A
		public static ulong GetNextSessionId()
		{
			return (ulong)Interlocked.Increment(ref SessionId.nextSessionId);
		}

		// Token: 0x04000161 RID: 353
		private static long nextSessionId = DateTime.UtcNow.Ticks;
	}
}
