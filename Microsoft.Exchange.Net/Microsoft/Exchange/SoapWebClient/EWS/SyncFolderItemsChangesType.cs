using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002DC RID: 732
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SyncFolderItemsChangesType
	{
		// Token: 0x0400125D RID: 4701
		[XmlElement("ReadFlagChange", typeof(SyncFolderItemsReadFlagType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("Delete", typeof(SyncFolderItemsDeleteType))]
		[XmlElement("Create", typeof(SyncFolderItemsCreateOrUpdateType))]
		[XmlElement("Update", typeof(SyncFolderItemsCreateOrUpdateType))]
		public object[] Items;

		// Token: 0x0400125E RID: 4702
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType2[] ItemsElementName;
	}
}
