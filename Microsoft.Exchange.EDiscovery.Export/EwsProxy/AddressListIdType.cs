using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000F2 RID: 242
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AddressListIdType : BaseFolderIdType
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00020EBC File Offset: 0x0001F0BC
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00020EC4 File Offset: 0x0001F0C4
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

		// Token: 0x04000801 RID: 2049
		private string idField;
	}
}
