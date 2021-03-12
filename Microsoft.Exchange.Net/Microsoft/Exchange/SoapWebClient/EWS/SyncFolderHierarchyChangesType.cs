using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002E2 RID: 738
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SyncFolderHierarchyChangesType
	{
		// Token: 0x0400126C RID: 4716
		[XmlElement("Update", typeof(SyncFolderHierarchyCreateOrUpdateType))]
		[XmlElement("Delete", typeof(SyncFolderHierarchyDeleteType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("Create", typeof(SyncFolderHierarchyCreateOrUpdateType))]
		public object[] Items;

		// Token: 0x0400126D RID: 4717
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType1[] ItemsElementName;
	}
}
