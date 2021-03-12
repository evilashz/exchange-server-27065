using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000127 RID: 295
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FileAttachmentType : AttachmentType
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00021F94 File Offset: 0x00020194
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x00021F9C File Offset: 0x0002019C
		public bool IsContactPhoto
		{
			get
			{
				return this.isContactPhotoField;
			}
			set
			{
				this.isContactPhotoField = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00021FA5 File Offset: 0x000201A5
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x00021FAD File Offset: 0x000201AD
		[XmlIgnore]
		public bool IsContactPhotoSpecified
		{
			get
			{
				return this.isContactPhotoFieldSpecified;
			}
			set
			{
				this.isContactPhotoFieldSpecified = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00021FB6 File Offset: 0x000201B6
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x00021FBE File Offset: 0x000201BE
		[XmlElement(DataType = "base64Binary")]
		public byte[] Content
		{
			get
			{
				return this.contentField;
			}
			set
			{
				this.contentField = value;
			}
		}

		// Token: 0x04000930 RID: 2352
		private bool isContactPhotoField;

		// Token: 0x04000931 RID: 2353
		private bool isContactPhotoFieldSpecified;

		// Token: 0x04000932 RID: 2354
		private byte[] contentField;
	}
}
