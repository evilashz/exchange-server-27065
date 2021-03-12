using System;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000227 RID: 551
	internal class SearchExceptionEvent : EventArgs
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x00043F05 File Offset: 0x00042105
		internal SearchExceptionEvent(int? sourceIndex, Exception exception)
		{
			this.sourceIndex = sourceIndex;
			this.exception = exception;
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00043F1B File Offset: 0x0004211B
		internal int? SourceIndex
		{
			get
			{
				return this.sourceIndex;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00043F23 File Offset: 0x00042123
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04000A6A RID: 2666
		private int? sourceIndex;

		// Token: 0x04000A6B RID: 2667
		private Exception exception;
	}
}
