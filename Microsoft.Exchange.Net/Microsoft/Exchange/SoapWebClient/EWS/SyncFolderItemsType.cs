using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000447 RID: 1095
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SyncFolderItemsType : BaseRequestType
	{
		// Token: 0x040016D3 RID: 5843
		public ItemResponseShapeType ItemShape;

		// Token: 0x040016D4 RID: 5844
		public TargetFolderIdType SyncFolderId;

		// Token: 0x040016D5 RID: 5845
		public string SyncState;

		// Token: 0x040016D6 RID: 5846
		[XmlArrayItem("ItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemIdType[] Ignore;

		// Token: 0x040016D7 RID: 5847
		public int MaxChangesReturned;

		// Token: 0x040016D8 RID: 5848
		public SyncFolderItemsScopeType SyncScope;

		// Token: 0x040016D9 RID: 5849
		[XmlIgnore]
		public bool SyncScopeSpecified;
	}
}
