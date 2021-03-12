using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000006 RID: 6
	internal sealed class DocumentProcessingContext
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000306D File Offset: 0x0000126D
		internal DocumentProcessingContext(MailboxSession session)
		{
			this.session = session;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000307C File Offset: 0x0000127C
		public MailboxSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x04000020 RID: 32
		private readonly MailboxSession session;
	}
}
