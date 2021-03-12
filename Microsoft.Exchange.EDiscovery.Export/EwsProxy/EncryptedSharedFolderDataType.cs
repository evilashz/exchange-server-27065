using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001E5 RID: 485
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class EncryptedSharedFolderDataType
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00025828 File Offset: 0x00023A28
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00025830 File Offset: 0x00023A30
		public XmlElement Token
		{
			get
			{
				return this.tokenField;
			}
			set
			{
				this.tokenField = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00025839 File Offset: 0x00023A39
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00025841 File Offset: 0x00023A41
		public XmlElement Data
		{
			get
			{
				return this.dataField;
			}
			set
			{
				this.dataField = value;
			}
		}

		// Token: 0x04000DC3 RID: 3523
		private XmlElement tokenField;

		// Token: 0x04000DC4 RID: 3524
		private XmlElement dataField;
	}
}
