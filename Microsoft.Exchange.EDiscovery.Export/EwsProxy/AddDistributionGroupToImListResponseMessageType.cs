using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000FF RID: 255
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AddDistributionGroupToImListResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00021244 File Offset: 0x0001F444
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x0002124C File Offset: 0x0001F44C
		public ImGroupType ImGroup
		{
			get
			{
				return this.imGroupField;
			}
			set
			{
				this.imGroupField = value;
			}
		}

		// Token: 0x04000863 RID: 2147
		private ImGroupType imGroupField;
	}
}
