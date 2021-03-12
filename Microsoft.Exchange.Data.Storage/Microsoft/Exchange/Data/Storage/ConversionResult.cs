using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005C8 RID: 1480
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversionResult
	{
		// Token: 0x06003CEE RID: 15598 RVA: 0x000F90C4 File Offset: 0x000F72C4
		internal ConversionResult()
		{
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000F90CC File Offset: 0x000F72CC
		internal void AddSubResult(ConversionResult subResult)
		{
			this.itemWasModified |= subResult.itemWasModified;
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x000F90E1 File Offset: 0x000F72E1
		// (set) Token: 0x06003CF1 RID: 15601 RVA: 0x000F90E9 File Offset: 0x000F72E9
		public bool ItemWasModified
		{
			get
			{
				return this.itemWasModified;
			}
			internal set
			{
				this.itemWasModified = value;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x000F90F2 File Offset: 0x000F72F2
		// (set) Token: 0x06003CF3 RID: 15603 RVA: 0x000F90FA File Offset: 0x000F72FA
		public long BodySize
		{
			get
			{
				return this.bodySize;
			}
			internal set
			{
				this.bodySize = value;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000F9103 File Offset: 0x000F7303
		// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x000F910B File Offset: 0x000F730B
		public int RecipientCount
		{
			get
			{
				return this.recipientCount;
			}
			internal set
			{
				this.recipientCount = value;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000F9114 File Offset: 0x000F7314
		// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x000F911C File Offset: 0x000F731C
		public int AttachmentCount
		{
			get
			{
				return this.attachmentCount;
			}
			internal set
			{
				this.attachmentCount = value;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000F9125 File Offset: 0x000F7325
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x000F912D File Offset: 0x000F732D
		public long AccumulatedAttachmentSize
		{
			get
			{
				return this.accumulatedAttachmentSize;
			}
			internal set
			{
				this.accumulatedAttachmentSize = value;
			}
		}

		// Token: 0x0400206A RID: 8298
		private long bodySize;

		// Token: 0x0400206B RID: 8299
		private int recipientCount;

		// Token: 0x0400206C RID: 8300
		private int attachmentCount;

		// Token: 0x0400206D RID: 8301
		private long accumulatedAttachmentSize;

		// Token: 0x0400206E RID: 8302
		private bool itemWasModified;
	}
}
