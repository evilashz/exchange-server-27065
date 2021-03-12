using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002F5 RID: 757
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class SearchParametersType
	{
		// Token: 0x040012D6 RID: 4822
		public RestrictionType Restriction;

		// Token: 0x040012D7 RID: 4823
		[XmlArrayItem("FolderId", typeof(FolderIdType), IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), IsNullable = false)]
		public BaseFolderIdType[] BaseFolderIds;

		// Token: 0x040012D8 RID: 4824
		[XmlAttribute]
		public SearchFolderTraversalType Traversal;

		// Token: 0x040012D9 RID: 4825
		[XmlIgnore]
		public bool TraversalSpecified;
	}
}
