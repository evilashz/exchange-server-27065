using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000311 RID: 785
	[XmlInclude(typeof(MovedCopiedEventType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(ModifiedEventType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class BaseObjectChangedEventType : BaseNotificationEventType
	{
		// Token: 0x0400130A RID: 4874
		public DateTime TimeStamp;

		// Token: 0x0400130B RID: 4875
		[XmlElement("ItemId", typeof(ItemIdType))]
		[XmlElement("FolderId", typeof(FolderIdType))]
		public object Item;

		// Token: 0x0400130C RID: 4876
		public FolderIdType ParentFolderId;
	}
}
