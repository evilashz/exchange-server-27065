using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200037C RID: 892
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetAttachmentType : BaseRequestType
	{
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x00029E07 File Offset: 0x00028007
		// (set) Token: 0x06001C3F RID: 7231 RVA: 0x00029E0F File Offset: 0x0002800F
		public AttachmentResponseShapeType AttachmentShape
		{
			get
			{
				return this.attachmentShapeField;
			}
			set
			{
				this.attachmentShapeField = value;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00029E18 File Offset: 0x00028018
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x00029E20 File Offset: 0x00028020
		[XmlArrayItem("AttachmentId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RequestAttachmentIdType[] AttachmentIds
		{
			get
			{
				return this.attachmentIdsField;
			}
			set
			{
				this.attachmentIdsField = value;
			}
		}

		// Token: 0x040012B7 RID: 4791
		private AttachmentResponseShapeType attachmentShapeField;

		// Token: 0x040012B8 RID: 4792
		private RequestAttachmentIdType[] attachmentIdsField;
	}
}
