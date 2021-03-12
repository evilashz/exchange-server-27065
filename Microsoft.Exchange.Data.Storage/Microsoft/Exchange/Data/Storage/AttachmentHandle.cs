using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200034E RID: 846
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AttachmentHandle
	{
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x00097236 File Offset: 0x00095436
		public int AttachNumber
		{
			get
			{
				return this.attachmentNumber;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x0009723E File Offset: 0x0009543E
		// (set) Token: 0x060025A2 RID: 9634 RVA: 0x00097246 File Offset: 0x00095446
		public AttachmentId AttachmentId
		{
			get
			{
				return this.attachmentId;
			}
			set
			{
				this.attachmentId = value;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0009724F File Offset: 0x0009544F
		internal string CharsetDetectionData
		{
			get
			{
				return this.charsetDetectionData;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x00097257 File Offset: 0x00095457
		public bool IsCalendarException
		{
			get
			{
				return this.isCalendarException;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x0009725F File Offset: 0x0009545F
		public bool IsInline
		{
			get
			{
				return this.isInline && this.AttachMethod != 7;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x00097277 File Offset: 0x00095477
		public int AttachMethod
		{
			get
			{
				return this.attachMethod;
			}
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0009727F File Offset: 0x0009547F
		internal AttachmentHandle(int attachmentNumber)
		{
			this.attachmentNumber = attachmentNumber;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x00097290 File Offset: 0x00095490
		internal void UpdateProperties(AttachmentPropertyBag attachmentBag)
		{
			StringBuilder stringBuilder = new StringBuilder();
			attachmentBag.ComputeCharsetDetectionData(stringBuilder);
			this.charsetDetectionData = stringBuilder.ToString();
			this.attachmentId = attachmentBag.AttachmentId;
			this.isCalendarException = attachmentBag.IsCalendarException;
			this.isInline = attachmentBag.IsInline;
			this.attachMethod = attachmentBag.AttachMethod;
			this.cachedPropertyBag = null;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000972ED File Offset: 0x000954ED
		internal void SetCachedPropertyBag(PropertyBag propertyBag)
		{
			this.cachedPropertyBag = propertyBag;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000972F8 File Offset: 0x000954F8
		internal PropertyBag GetAndRemoveCachedPropertyBag()
		{
			PropertyBag result = this.cachedPropertyBag;
			this.cachedPropertyBag = null;
			return result;
		}

		// Token: 0x040016AC RID: 5804
		private readonly int attachmentNumber;

		// Token: 0x040016AD RID: 5805
		private AttachmentId attachmentId;

		// Token: 0x040016AE RID: 5806
		private string charsetDetectionData;

		// Token: 0x040016AF RID: 5807
		private bool isCalendarException;

		// Token: 0x040016B0 RID: 5808
		private bool isInline;

		// Token: 0x040016B1 RID: 5809
		private int attachMethod;

		// Token: 0x040016B2 RID: 5810
		private PropertyBag cachedPropertyBag;
	}
}
