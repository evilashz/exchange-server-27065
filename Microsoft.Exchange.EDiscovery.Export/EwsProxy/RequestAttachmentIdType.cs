using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000B7 RID: 183
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(AttachmentIdType))]
	[DebuggerStepThrough]
	[Serializable]
	public class RequestAttachmentIdType : BaseItemIdType
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001FDDD File Offset: 0x0001DFDD
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0001FDE5 File Offset: 0x0001DFE5
		[XmlAttribute]
		public string Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x04000559 RID: 1369
		private string idField;
	}
}
