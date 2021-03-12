using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200030C RID: 780
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FolderResponseShapeType
	{
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00028934 File Offset: 0x00026B34
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x0002893C File Offset: 0x00026B3C
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

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00028945 File Offset: 0x00026B45
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x0002894D File Offset: 0x00026B4D
		[XmlArrayItem("Path", IsNullable = false)]
		[XmlArrayItem("IndexedFieldURI", typeof(PathToIndexedFieldType), IsNullable = false)]
		[XmlArrayItem("FieldURI", typeof(PathToUnindexedFieldType), IsNullable = false)]
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

		// Token: 0x0400115A RID: 4442
		private DefaultShapeNamesType baseShapeField;

		// Token: 0x0400115B RID: 4443
		private BasePathToElementType[] additionalPropertiesField;
	}
}
