using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002E5 RID: 741
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ConversationResponseShapeType
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x00028320 File Offset: 0x00026520
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x00028328 File Offset: 0x00026528
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

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x00028331 File Offset: 0x00026531
		// (set) Token: 0x06001910 RID: 6416 RVA: 0x00028339 File Offset: 0x00026539
		[XmlArrayItem("Path", IsNullable = false)]
		[XmlArrayItem("ExtendedFieldURI", typeof(PathToExtendedFieldType), IsNullable = false)]
		[XmlArrayItem("IndexedFieldURI", typeof(PathToIndexedFieldType), IsNullable = false)]
		[XmlArrayItem("FieldURI", typeof(PathToUnindexedFieldType), IsNullable = false)]
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

		// Token: 0x040010FC RID: 4348
		private DefaultShapeNamesType baseShapeField;

		// Token: 0x040010FD RID: 4349
		private BasePathToElementType[] additionalPropertiesField;
	}
}
