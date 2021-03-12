using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000BF RID: 191
	internal class AttachmentData
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00009D10 File Offset: 0x00007F10
		public MessageImplementation MessageImplementation
		{
			[DebuggerStepThrough]
			get
			{
				return this.messageImplementation;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00009D18 File Offset: 0x00007F18
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00009D20 File Offset: 0x00007F20
		public object Attachment
		{
			[DebuggerStepThrough]
			get
			{
				return this.attachment;
			}
			[DebuggerStepThrough]
			set
			{
				this.attachment = value;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00009D29 File Offset: 0x00007F29
		protected AttachmentData(MessageImplementation messageImplementation)
		{
			this.messageImplementation = messageImplementation;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00009D38 File Offset: 0x00007F38
		public virtual void Invalidate()
		{
			this.messageImplementation = null;
			this.attachment = null;
		}

		// Token: 0x0400026E RID: 622
		private MessageImplementation messageImplementation;

		// Token: 0x0400026F RID: 623
		private object attachment;
	}
}
