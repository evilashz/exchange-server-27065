using System;
using System.DirectoryServices.Protocols;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000004 RID: 4
	internal class ExDirectoryException : Exception
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002359 File Offset: 0x00000559
		public ExDirectoryException(DirectoryOperationException e) : base(e.Message, e)
		{
			if (e.Response != null)
			{
				this.resultCode = e.Response.ResultCode;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002381 File Offset: 0x00000581
		public ExDirectoryException(Exception e) : base(e.Message, e)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002390 File Offset: 0x00000590
		public ExDirectoryException(string message, Exception e) : base(message, e)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000239A File Offset: 0x0000059A
		public ExDirectoryException(ResultCode resultCode, string message) : base(message)
		{
			this.resultCode = resultCode;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023AA File Offset: 0x000005AA
		public ResultCode ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x04000006 RID: 6
		private ResultCode resultCode;
	}
}
