using System;
using System.Threading;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000039 RID: 57
	internal class IDGenerator
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000E27C File Offset: 0x0000C47C
		public static long GetNextID()
		{
			return Interlocked.Increment(ref IDGenerator.id);
		}

		// Token: 0x04000136 RID: 310
		private static long id;
	}
}
