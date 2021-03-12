using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000C1 RID: 193
	internal class TnefAttachmentData : AttachmentData
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00009DD5 File Offset: 0x00007FD5
		public TnefPropertyBag Properties
		{
			[DebuggerStepThrough]
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00009DDD File Offset: 0x00007FDD
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00009DE5 File Offset: 0x00007FE5
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00009DEE File Offset: 0x00007FEE
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00009DF6 File Offset: 0x00007FF6
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

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00009DFF File Offset: 0x00007FFF
		public int OriginalIndex
		{
			[DebuggerStepThrough]
			get
			{
				return this.originalIndex;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00009E07 File Offset: 0x00008007
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00009E0F File Offset: 0x0000800F
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

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00009E18 File Offset: 0x00008018
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00009E20 File Offset: 0x00008020
		public AttachmentMethod AttachmentMethod
		{
			[DebuggerStepThrough]
			get
			{
				return this.attachmentMethod;
			}
			[DebuggerStepThrough]
			set
			{
				this.attachmentMethod = value;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00009E29 File Offset: 0x00008029
		public TnefAttachmentData(int attachmentIndex, MessageImplementation message) : base(message)
		{
			this.properties = new TnefPropertyBag(this);
			this.originalIndex = attachmentIndex;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00009E45 File Offset: 0x00008045
		public override void Invalidate()
		{
			this.originalIndex = int.MinValue;
			base.Invalidate();
		}

		// Token: 0x04000276 RID: 630
		private TnefPropertyBag properties;

		// Token: 0x04000277 RID: 631
		private EmailMessage embeddedMessage;

		// Token: 0x04000278 RID: 632
		private InternalAttachmentType internalAttachmentType;

		// Token: 0x04000279 RID: 633
		private int originalIndex;

		// Token: 0x0400027A RID: 634
		private AttachmentMethod attachmentMethod;

		// Token: 0x0400027B RID: 635
		private string fileName;
	}
}
