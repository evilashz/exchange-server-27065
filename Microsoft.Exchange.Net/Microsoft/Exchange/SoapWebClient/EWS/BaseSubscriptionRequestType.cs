using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003B4 RID: 948
	[XmlInclude(typeof(PushSubscriptionRequestType))]
	[XmlInclude(typeof(PullSubscriptionRequestType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public abstract class BaseSubscriptionRequestType
	{
		// Token: 0x040014EF RID: 5359
		[XmlArrayItem("FolderId", typeof(FolderIdType), IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), IsNullable = false)]
		public BaseFolderIdType[] FolderIds;

		// Token: 0x040014F0 RID: 5360
		[XmlArrayItem("EventType", IsNullable = false)]
		public NotificationEventTypeType[] EventTypes;

		// Token: 0x040014F1 RID: 5361
		public string Watermark;

		// Token: 0x040014F2 RID: 5362
		[XmlAttribute]
		public bool SubscribeToAllFolders;

		// Token: 0x040014F3 RID: 5363
		[XmlIgnore]
		public bool SubscribeToAllFoldersSpecified;
	}
}
