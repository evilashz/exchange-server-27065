using System;

namespace Microsoft.Exchange.Rpc.QueueViewer
{
	// Token: 0x0200039F RID: 927
	internal class QueueViewerException : Exception
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x0004B2B4 File Offset: 0x0004A6B4
		public QueueViewerException(int result)
		{
			this.errorCode = result;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0004B2D0 File Offset: 0x0004A6D0
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x04000F91 RID: 3985
		private int errorCode;
	}
}
