using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000FC RID: 252
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class StringArrayAttributedValueType
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x000211D7 File Offset: 0x0001F3D7
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x000211DF File Offset: 0x0001F3DF
		[XmlArrayItem("Value", IsNullable = false)]
		public string[] Values
		{
			get
			{
				return this.valuesField;
			}
			set
			{
				this.valuesField = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x000211E8 File Offset: 0x0001F3E8
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x000211F0 File Offset: 0x0001F3F0
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

		// Token: 0x0400085E RID: 2142
		private string[] valuesField;

		// Token: 0x0400085F RID: 2143
		private string[] attributionsField;
	}
}
