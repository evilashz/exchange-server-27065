using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000C0 RID: 192
	internal class MimeAttachmentData : AttachmentData
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00009D48 File Offset: 0x00007F48
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00009D50 File Offset: 0x00007F50
		public MimePart AttachmentPart
		{
			[DebuggerStepThrough]
			get
			{
				return this.attachmentPart;
			}
			[DebuggerStepThrough]
			set
			{
				this.attachmentPart = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00009D59 File Offset: 0x00007F59
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00009D61 File Offset: 0x00007F61
		public MimePart DataPart
		{
			[DebuggerStepThrough]
			get
			{
				return this.dataPart;
			}
			[DebuggerStepThrough]
			set
			{
				this.dataPart = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00009D6A File Offset: 0x00007F6A
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00009D72 File Offset: 0x00007F72
		public string FileName
		{
			[DebuggerStepThrough]
			get
			{
				return this.fileName;
			}
			[DebuggerStepThrough]
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00009D7B File Offset: 0x00007F7B
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00009D83 File Offset: 0x00007F83
		public EmailMessage EmbeddedMessage
		{
			[DebuggerStepThrough]
			get
			{
				return this.embeddedMessage;
			}
			[DebuggerStepThrough]
			set
			{
				this.embeddedMessage = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00009D8C File Offset: 0x00007F8C
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00009D94 File Offset: 0x00007F94
		public InternalAttachmentType InternalAttachmentType
		{
			[DebuggerStepThrough]
			get
			{
				return this.internalAttachmentType;
			}
			[DebuggerStepThrough]
			set
			{
				this.internalAttachmentType = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00009D9D File Offset: 0x00007F9D
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00009DA5 File Offset: 0x00007FA5
		public bool Referenced
		{
			[DebuggerStepThrough]
			get
			{
				return this.referenced;
			}
			[DebuggerStepThrough]
			set
			{
				this.referenced = value;
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00009DAE File Offset: 0x00007FAE
		public MimeAttachmentData(MimePart part, MessageImplementation message) : base(message)
		{
			this.attachmentPart = part;
			this.referenced = true;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00009DC5 File Offset: 0x00007FC5
		public void FlushCache()
		{
			this.fileName = null;
			this.dataPart = null;
		}

		// Token: 0x04000270 RID: 624
		private MimePart attachmentPart;

		// Token: 0x04000271 RID: 625
		private MimePart dataPart;

		// Token: 0x04000272 RID: 626
		private string fileName;

		// Token: 0x04000273 RID: 627
		private EmailMessage embeddedMessage;

		// Token: 0x04000274 RID: 628
		private InternalAttachmentType internalAttachmentType;

		// Token: 0x04000275 RID: 629
		private bool referenced;
	}
}
