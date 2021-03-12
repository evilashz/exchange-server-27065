using System;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000003 RID: 3
	internal class EdgeSyncCycleFailedException : Exception
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002337 File Offset: 0x00000537
		public EdgeSyncCycleFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002340 File Offset: 0x00000540
		public EdgeSyncCycleFailedException(Exception innerException) : base(innerException.Message, innerException)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000234F File Offset: 0x0000054F
		public EdgeSyncCycleFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
