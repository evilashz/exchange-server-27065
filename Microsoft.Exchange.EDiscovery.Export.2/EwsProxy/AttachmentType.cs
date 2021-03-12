using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000126 RID: 294
	[DesignerCategory("code")]
	[XmlInclude(typeof(ItemAttachmentType))]
	[DebuggerStepThrough]
	[XmlInclude(typeof(ReferenceAttachmentType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(FileAttachmentType))]
	[Serializable]
	public class AttachmentType
	{
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00021ED1 File Offset: 0x000200D1
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00021ED9 File Offset: 0x000200D9
		public AttachmentIdType AttachmentId
		{
			get
			{
				return this.attachmentIdField;
			}
			set
			{
				this.attachmentIdField = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00021EE2 File Offset: 0x000200E2
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00021EEA File Offset: 0x000200EA
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00021EF3 File Offset: 0x000200F3
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x00021EFB File Offset: 0x000200FB
		public string ContentType
		{
			get
			{
				return this.contentTypeField;
			}
			set
			{
				this.contentTypeField = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00021F04 File Offset: 0x00020104
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x00021F0C File Offset: 0x0002010C
		public string ContentId
		{
			get
			{
				return this.contentIdField;
			}
			set
			{
				this.contentIdField = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00021F15 File Offset: 0x00020115
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x00021F1D File Offset: 0x0002011D
		public string ContentLocation
		{
			get
			{
				return this.contentLocationField;
			}
			set
			{
				this.contentLocationField = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00021F26 File Offset: 0x00020126
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00021F2E File Offset: 0x0002012E
		public int Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00021F37 File Offset: 0x00020137
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00021F3F File Offset: 0x0002013F
		[XmlIgnore]
		public bool SizeSpecified
		{
			get
			{
				return this.sizeFieldSpecified;
			}
			set
			{
				this.sizeFieldSpecified = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00021F48 File Offset: 0x00020148
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00021F50 File Offset: 0x00020150
		public DateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTimeField;
			}
			set
			{
				this.lastModifiedTimeField = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00021F59 File Offset: 0x00020159
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x00021F61 File Offset: 0x00020161
		[XmlIgnore]
		public bool LastModifiedTimeSpecified
		{
			get
			{
				return this.lastModifiedTimeFieldSpecified;
			}
			set
			{
				this.lastModifiedTimeFieldSpecified = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00021F6A File Offset: 0x0002016A
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00021F72 File Offset: 0x00020172
		public bool IsInline
		{
			get
			{
				return this.isInlineField;
			}
			set
			{
				this.isInlineField = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00021F7B File Offset: 0x0002017B
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00021F83 File Offset: 0x00020183
		[XmlIgnore]
		public bool IsInlineSpecified
		{
			get
			{
				return this.isInlineFieldSpecified;
			}
			set
			{
				this.isInlineFieldSpecified = value;
			}
		}

		// Token: 0x04000925 RID: 2341
		private AttachmentIdType attachmentIdField;

		// Token: 0x04000926 RID: 2342
		private string nameField;

		// Token: 0x04000927 RID: 2343
		private string contentTypeField;

		// Token: 0x04000928 RID: 2344
		private string contentIdField;

		// Token: 0x04000929 RID: 2345
		private string contentLocationField;

		// Token: 0x0400092A RID: 2346
		private int sizeField;

		// Token: 0x0400092B RID: 2347
		private bool sizeFieldSpecified;

		// Token: 0x0400092C RID: 2348
		private DateTime lastModifiedTimeField;

		// Token: 0x0400092D RID: 2349
		private bool lastModifiedTimeFieldSpecified;

		// Token: 0x0400092E RID: 2350
		private bool isInlineField;

		// Token: 0x0400092F RID: 2351
		private bool isInlineFieldSpecified;
	}
}
