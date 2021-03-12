using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000014 RID: 20
	internal class EndResponseItem : IResponseItem
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00005464 File Offset: 0x00003664
		public EndResponseItem(BaseSession.SendCompleteDelegate sendCompleteDelegate)
		{
			this.endResponseDelegate = sendCompleteDelegate;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005473 File Offset: 0x00003673
		public BaseSession.SendCompleteDelegate SendCompleteDelegate
		{
			get
			{
				return this.endResponseDelegate;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000547B File Offset: 0x0000367B
		public int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
		{
			buffer = null;
			offset = 0;
			return 0;
		}

		// Token: 0x04000093 RID: 147
		private BaseSession.SendCompleteDelegate endResponseDelegate;
	}
}
