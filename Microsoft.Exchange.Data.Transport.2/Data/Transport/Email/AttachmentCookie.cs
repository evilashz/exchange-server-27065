using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000BA RID: 186
	internal struct AttachmentCookie
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00009611 File Offset: 0x00007811
		public int Index
		{
			[DebuggerStepThrough]
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00009619 File Offset: 0x00007819
		public MessageImplementation MessageImplementation
		{
			[DebuggerStepThrough]
			get
			{
				return this.messageImplementation;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00009621 File Offset: 0x00007821
		public AttachmentCookie(int index, MessageImplementation messageImplementation)
		{
			this.index = index;
			this.messageImplementation = messageImplementation;
		}

		// Token: 0x0400025F RID: 607
		private int index;

		// Token: 0x04000260 RID: 608
		private MessageImplementation messageImplementation;
	}
}
