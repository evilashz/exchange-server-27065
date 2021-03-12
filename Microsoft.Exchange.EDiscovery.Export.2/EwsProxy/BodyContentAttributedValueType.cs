using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000E9 RID: 233
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class BodyContentAttributedValueType
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00020D85 File Offset: 0x0001EF85
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x00020D8D File Offset: 0x0001EF8D
		public BodyContentType Value
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

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00020D96 File Offset: 0x0001EF96
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x00020D9E File Offset: 0x0001EF9E
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

		// Token: 0x040007E6 RID: 2022
		private BodyContentType valueField;

		// Token: 0x040007E7 RID: 2023
		private string[] attributionsField;
	}
}
