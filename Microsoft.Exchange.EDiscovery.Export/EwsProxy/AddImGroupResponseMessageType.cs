using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000100 RID: 256
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AddImGroupResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0002125D File Offset: 0x0001F45D
		// (set) Token: 0x06000BB2 RID: 2994 RVA: 0x00021265 File Offset: 0x0001F465
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

		// Token: 0x04000864 RID: 2148
		private ImGroupType imGroupField;
	}
}
