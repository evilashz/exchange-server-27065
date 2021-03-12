using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002FA RID: 762
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ContainsExpressionType : SearchExpressionType
	{
		// Token: 0x040012DC RID: 4828
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		public BasePathToElementType Item;

		// Token: 0x040012DD RID: 4829
		public ConstantValueType Constant;

		// Token: 0x040012DE RID: 4830
		[XmlAttribute]
		public ContainmentModeType ContainmentMode;

		// Token: 0x040012DF RID: 4831
		[XmlIgnore]
		public bool ContainmentModeSpecified;

		// Token: 0x040012E0 RID: 4832
		[XmlAttribute]
		public ContainmentComparisonType ContainmentComparison;

		// Token: 0x040012E1 RID: 4833
		[XmlIgnore]
		public bool ContainmentComparisonSpecified;
	}
}
