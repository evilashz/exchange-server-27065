using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000220 RID: 544
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ExistsType : SearchExpressionType
	{
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00026306 File Offset: 0x00024506
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x0002630E File Offset: 0x0002450E
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
		public BasePathToElementType Item
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

		// Token: 0x04000EA4 RID: 3748
		private BasePathToElementType itemField;
	}
}
