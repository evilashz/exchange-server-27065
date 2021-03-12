using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000F6 RID: 246
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class StringAttributedValueType
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00020FC2 File Offset: 0x0001F1C2
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00020FCA File Offset: 0x0001F1CA
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

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00020FD3 File Offset: 0x0001F1D3
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x00020FDB File Offset: 0x0001F1DB
		[XmlArrayItem("Attribution", IsNullable = false)]
		public string[] Attributions
		{
			get
			{
				return this.attributionsField;
			}
			set
			{
				this.attributionsField = value;
			}
		}

		// Token: 0x0400083A RID: 2106
		private string valueField;

		// Token: 0x0400083B RID: 2107
		private string[] attributionsField;
	}
}
