using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000532 RID: 1330
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "MarkAsJunkResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class MarkAsJunkResponseMessage : ResponseMessage
	{
		// Token: 0x060025EE RID: 9710 RVA: 0x000A6154 File Offset: 0x000A4354
		public MarkAsJunkResponseMessage()
		{
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000A615C File Offset: 0x000A435C
		internal MarkAsJunkResponseMessage(ServiceResultCode code, ServiceError error, ItemId movedItemId) : base(code, error)
		{
			this.MovedItemId = movedItemId;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000A616D File Offset: 0x000A436D
		// (set) Token: 0x060025F1 RID: 9713 RVA: 0x000A6174 File Offset: 0x000A4374
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				return ResponseMessage.namespaces;
			}
			set
			{
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x000A6176 File Offset: 0x000A4376
		// (set) Token: 0x060025F3 RID: 9715 RVA: 0x000A617E File Offset: 0x000A437E
		[DataMember]
		[XmlElement("MovedItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ItemId MovedItemId { get; set; }
	}
}
