using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000DB RID: 219
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class NonEmptyArrayOfExtendedPropertyType
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002059A File Offset: 0x0001E79A
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x000205A2 File Offset: 0x0001E7A2
		[XmlElement("ExtendedProperty")]
		public ExtendedPropertyType[] Items
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

		// Token: 0x040005E4 RID: 1508
		private ExtendedPropertyType[] itemsField;
	}
}
