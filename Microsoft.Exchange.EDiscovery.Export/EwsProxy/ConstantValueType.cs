using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200021A RID: 538
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ConstantValueType
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00026291 File Offset: 0x00024491
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x00026299 File Offset: 0x00024499
		[XmlAttribute]
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04000E90 RID: 3728
		private string valueField;
	}
}
