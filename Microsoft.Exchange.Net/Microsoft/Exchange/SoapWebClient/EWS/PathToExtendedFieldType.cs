using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001BF RID: 447
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PathToExtendedFieldType : BasePathToElementType
	{
		// Token: 0x04000A39 RID: 2617
		[XmlAttribute]
		public DistinguishedPropertySetType DistinguishedPropertySetId;

		// Token: 0x04000A3A RID: 2618
		[XmlIgnore]
		public bool DistinguishedPropertySetIdSpecified;

		// Token: 0x04000A3B RID: 2619
		[XmlAttribute]
		public string PropertySetId;

		// Token: 0x04000A3C RID: 2620
		[XmlAttribute]
		public string PropertyTag;

		// Token: 0x04000A3D RID: 2621
		[XmlAttribute]
		public string PropertyName;

		// Token: 0x04000A3E RID: 2622
		[XmlAttribute]
		public int PropertyId;

		// Token: 0x04000A3F RID: 2623
		[XmlIgnore]
		public bool PropertyIdSpecified;

		// Token: 0x04000A40 RID: 2624
		[XmlAttribute]
		public MapiPropertyTypeType PropertyType;
	}
}
