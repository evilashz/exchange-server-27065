using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000228 RID: 552
	internal class RequestLogBodyEvent : EventArgs
	{
		// Token: 0x06000F2D RID: 3885 RVA: 0x00043F2B File Offset: 0x0004212B
		internal RequestLogBodyEvent(Body itemBody)
		{
			this.itemBody = itemBody;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00043F3A File Offset: 0x0004213A
		internal Body ItemBody
		{
			get
			{
				return this.itemBody;
			}
		}

		// Token: 0x04000A6C RID: 2668
		private Body itemBody;
	}
}
