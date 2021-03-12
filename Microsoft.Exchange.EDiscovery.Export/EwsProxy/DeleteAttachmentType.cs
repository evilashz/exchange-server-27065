using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200037D RID: 893
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DeleteAttachmentType : BaseRequestType
	{
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x00029E31 File Offset: 0x00028031
		// (set) Token: 0x06001C44 RID: 7236 RVA: 0x00029E39 File Offset: 0x00028039
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

		// Token: 0x040012B9 RID: 4793
		private RequestAttachmentIdType[] attachmentIdsField;
	}
}
