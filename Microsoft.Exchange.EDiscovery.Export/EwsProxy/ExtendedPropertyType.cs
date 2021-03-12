using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000DC RID: 220
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ExtendedPropertyType
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x000205B3 File Offset: 0x0001E7B3
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x000205BB File Offset: 0x0001E7BB
		public PathToExtendedFieldType ExtendedFieldURI
		{
			get
			{
				return this.extendedFieldURIField;
			}
			set
			{
				this.extendedFieldURIField = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x000205C4 File Offset: 0x0001E7C4
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x000205CC File Offset: 0x0001E7CC
		[XmlElement("Value", typeof(string))]
		[XmlElement("Values", typeof(NonEmptyArrayOfPropertyValuesType))]
		public object Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x040005E5 RID: 1509
		private PathToExtendedFieldType extendedFieldURIField;

		// Token: 0x040005E6 RID: 1510
		private object itemField;
	}
}
