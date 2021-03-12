using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200045A RID: 1114
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ResolveNamesType : BaseRequestType
	{
		// Token: 0x06000F7B RID: 3963 RVA: 0x00026F18 File Offset: 0x00025118
		public ResolveNamesType()
		{
			this.SearchScope = ResolveNamesSearchScopeType.ActiveDirectoryContacts;
		}

		// Token: 0x040016FD RID: 5885
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] ParentFolderIds;

		// Token: 0x040016FE RID: 5886
		public string UnresolvedEntry;

		// Token: 0x040016FF RID: 5887
		[XmlAttribute]
		public bool ReturnFullContactData;

		// Token: 0x04001700 RID: 5888
		[DefaultValue(ResolveNamesSearchScopeType.ActiveDirectoryContacts)]
		[XmlAttribute]
		public ResolveNamesSearchScopeType SearchScope;

		// Token: 0x04001701 RID: 5889
		[XmlAttribute]
		public DefaultShapeNamesType ContactDataShape;

		// Token: 0x04001702 RID: 5890
		[XmlIgnore]
		public bool ContactDataShapeSpecified;
	}
}
