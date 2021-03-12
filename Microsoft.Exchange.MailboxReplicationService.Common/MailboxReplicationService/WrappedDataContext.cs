using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000F8 RID: 248
	internal class WrappedDataContext : DataContext
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x00012698 File Offset: 0x00010898
		public WrappedDataContext(string ctxString)
		{
			this.ctxString = ctxString;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000126A7 File Offset: 0x000108A7
		public override string ToString()
		{
			return this.ctxString;
		}

		// Token: 0x0400055D RID: 1373
		private readonly string ctxString;
	}
}
