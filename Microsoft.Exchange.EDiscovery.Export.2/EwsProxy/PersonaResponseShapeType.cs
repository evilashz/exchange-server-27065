using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002DF RID: 735
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PersonaResponseShapeType
	{
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00028189 File Offset: 0x00026389
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x00028191 File Offset: 0x00026391
		public DefaultShapeNamesType BaseShape
		{
			get
			{
				return this.baseShapeField;
			}
			set
			{
				this.baseShapeField = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0002819A File Offset: 0x0002639A
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x000281A2 File Offset: 0x000263A2
		[XmlArrayItem("FieldURI", typeof(PathToUnindexedFieldType), IsNullable = false)]
		[XmlArrayItem("Path", IsNullable = false)]
		[XmlArrayItem("IndexedFieldURI", typeof(PathToIndexedFieldType), IsNullable = false)]
		[XmlArrayItem("ExtendedFieldURI", typeof(PathToExtendedFieldType), IsNullable = false)]
		public BasePathToElementType[] AdditionalProperties
		{
			get
			{
				return this.additionalPropertiesField;
			}
			set
			{
				this.additionalPropertiesField = value;
			}
		}

		// Token: 0x040010D0 RID: 4304
		private DefaultShapeNamesType baseShapeField;

		// Token: 0x040010D1 RID: 4305
		private BasePathToElementType[] additionalPropertiesField;
	}
}
