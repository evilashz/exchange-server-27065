using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000261 RID: 609
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class DeleteAttachmentResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00027205 File Offset: 0x00025405
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x0002720D File Offset: 0x0002540D
		public RootItemIdType RootItemId
		{
			get
			{
				return this.rootItemIdField;
			}
			set
			{
				this.rootItemIdField = value;
			}
		}

		// Token: 0x04000F9D RID: 3997
		private RootItemIdType rootItemIdField;
	}
}
