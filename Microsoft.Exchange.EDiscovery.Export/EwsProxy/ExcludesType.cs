using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200021E RID: 542
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ExcludesType : SearchExpressionType
	{
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x000262C3 File Offset: 0x000244C3
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x000262CB File Offset: 0x000244CB
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
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

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x000262D4 File Offset: 0x000244D4
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x000262DC File Offset: 0x000244DC
		public ExcludesValueType Bitmask
		{
			get
			{
				return this.bitmaskField;
			}
			set
			{
				this.bitmaskField = value;
			}
		}

		// Token: 0x04000EA1 RID: 3745
		private BasePathToElementType itemField;

		// Token: 0x04000EA2 RID: 3746
		private ExcludesValueType bitmaskField;
	}
}
