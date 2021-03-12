using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000157 RID: 343
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ImAddressDictionaryEntryType
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00022DDB File Offset: 0x00020FDB
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x00022DE3 File Offset: 0x00020FE3
		[XmlAttribute]
		public ImAddressKeyType Key
		{
			get
			{
				return this.keyField;
			}
			set
			{
				this.keyField = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00022DEC File Offset: 0x00020FEC
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x00022DF4 File Offset: 0x00020FF4
		[XmlText]
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

		// Token: 0x04000A66 RID: 2662
		private ImAddressKeyType keyField;

		// Token: 0x04000A67 RID: 2663
		private string valueField;
	}
}
