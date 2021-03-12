using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003B2 RID: 946
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class StreamingSubscriptionRequestType
	{
		// Token: 0x040014E3 RID: 5347
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), IsNullable = false)]
		public BaseFolderIdType[] FolderIds;

		// Token: 0x040014E4 RID: 5348
		[XmlArrayItem("EventType", IsNullable = false)]
		public NotificationEventTypeType[] EventTypes;

		// Token: 0x040014E5 RID: 5349
		[XmlAttribute]
		public bool SubscribeToAllFolders;

		// Token: 0x040014E6 RID: 5350
		[XmlIgnore]
		public bool SubscribeToAllFoldersSpecified;
	}
}
