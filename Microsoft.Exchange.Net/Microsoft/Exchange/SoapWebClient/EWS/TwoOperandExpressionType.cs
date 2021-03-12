﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000302 RID: 770
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(IsEqualToType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(IsLessThanType))]
	[XmlInclude(typeof(IsGreaterThanOrEqualToType))]
	[XmlInclude(typeof(IsGreaterThanType))]
	[XmlInclude(typeof(IsNotEqualToType))]
	[XmlInclude(typeof(IsLessThanOrEqualToType))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class TwoOperandExpressionType : SearchExpressionType
	{
		// Token: 0x040012F7 RID: 4855
		[XmlElement("FieldURI", typeof(PathToUnindexedFieldType))]
		[XmlElement("ExtendedFieldURI", typeof(PathToExtendedFieldType))]
		[XmlElement("IndexedFieldURI", typeof(PathToIndexedFieldType))]
		public BasePathToElementType Item;

		// Token: 0x040012F8 RID: 4856
		public FieldURIOrConstantType FieldURIOrConstant;
	}
}
