using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000FD RID: 253
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ExtendedPropertyAttributedValueType
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00021201 File Offset: 0x0001F401
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00021209 File Offset: 0x0001F409
		public ExtendedPropertyType Value
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

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00021212 File Offset: 0x0001F412
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0002121A File Offset: 0x0001F41A
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

		// Token: 0x04000860 RID: 2144
		private ExtendedPropertyType valueField;

		// Token: 0x04000861 RID: 2145
		private string[] attributionsField;
	}
}
