using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000F9 RID: 249
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PostalAddressAttributedValueType
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00021040 File Offset: 0x0001F240
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x00021048 File Offset: 0x0001F248
		public PersonaPostalAddressType Value
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

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00021051 File Offset: 0x0001F251
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x00021059 File Offset: 0x0001F259
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

		// Token: 0x04000840 RID: 2112
		private PersonaPostalAddressType valueField;

		// Token: 0x04000841 RID: 2113
		private string[] attributionsField;
	}
}
