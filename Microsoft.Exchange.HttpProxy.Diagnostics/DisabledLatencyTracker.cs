using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000005 RID: 5
	public class DisabledLatencyTracker : LatencyTracker
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002318 File Offset: 0x00000518
		public override void LogElapsedTimeInDetailedLatencyInfo(string key)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000231A File Offset: 0x0000051A
		public override void LogElapsedTimeInMilliseconds(LogKey key)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000231C File Offset: 0x0000051C
		public override void LogLatency(LogKey key, Action operationToTrack)
		{
			operationToTrack();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002324 File Offset: 0x00000524
		public override T LogLatency<T>(LogKey key, Func<T> operationToTrack)
		{
			return operationToTrack();
		}
	}
}
