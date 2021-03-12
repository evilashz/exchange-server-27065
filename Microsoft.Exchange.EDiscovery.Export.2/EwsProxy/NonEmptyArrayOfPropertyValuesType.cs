using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000E7 RID: 231
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class NonEmptyArrayOfPropertyValuesType
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000206D1 File Offset: 0x0001E8D1
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x000206D9 File Offset: 0x0001E8D9
		[XmlElement("Value")]
		public string[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x04000782 RID: 1922
		private string[] itemsField;
	}
}
