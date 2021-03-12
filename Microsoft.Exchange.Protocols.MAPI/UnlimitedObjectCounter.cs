using System;
using System.Threading;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000074 RID: 116
	internal class UnlimitedObjectCounter : IMapiObjectCounter
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0001BBC1 File Offset: 0x00019DC1
		private UnlimitedObjectCounter()
		{
			this.objectCounter = 0L;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0001BBD1 File Offset: 0x00019DD1
		public static IMapiObjectCounter Instance
		{
			get
			{
				return UnlimitedObjectCounter.instance;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		public long GetCount()
		{
			return Interlocked.Read(ref this.objectCounter);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001BBE5 File Offset: 0x00019DE5
		public void IncrementCount()
		{
			Interlocked.Increment(ref this.objectCounter);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001BBF3 File Offset: 0x00019DF3
		public void DecrementCount()
		{
			Interlocked.Decrement(ref this.objectCounter);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001BC01 File Offset: 0x00019E01
		public void CheckObjectQuota(bool mustBeStrictlyUnderQuota)
		{
		}

		// Token: 0x0400024E RID: 590
		private static IMapiObjectCounter instance = new UnlimitedObjectCounter();

		// Token: 0x0400024F RID: 591
		private long objectCounter;
	}
}
