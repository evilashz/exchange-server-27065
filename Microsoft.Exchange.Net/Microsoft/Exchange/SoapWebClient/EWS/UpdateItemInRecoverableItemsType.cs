using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000474 RID: 1140
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class UpdateItemInRecoverableItemsType : BaseRequestType
	{
		// Token: 0x0400176F RID: 5999
		public ItemIdType ItemId;

		// Token: 0x04001770 RID: 6000
		[XmlArrayItem("AppendToItemField", typeof(AppendToItemFieldType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("SetItemField", typeof(SetItemFieldType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DeleteItemField", typeof(DeleteItemFieldType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemChangeDescriptionType[] Updates;

		// Token: 0x04001771 RID: 6001
		[XmlArrayItem("FileAttachment", typeof(FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemAttachment", typeof(ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public AttachmentType[] Attachments;

		// Token: 0x04001772 RID: 6002
		public bool MakeItemImmutable;

		// Token: 0x04001773 RID: 6003
		[XmlIgnore]
		public bool MakeItemImmutableSpecified;
	}
}
