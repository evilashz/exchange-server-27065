using System;

namespace Microsoft.Exchange.Rpc.LogSearch
{
	// Token: 0x0200027A RID: 634
	internal class LogSearchException : Exception
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x00029978 File Offset: 0x00028D78
		public LogSearchException(int result)
		{
			this.errorCode = result;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00029994 File Offset: 0x00028D94
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x04000D09 RID: 3337
		private int errorCode;
	}
}
