using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200050F RID: 1295
	[XmlType(TypeName = "GetRoomListsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRoomListsResponse : ResponseMessage
	{
		// Token: 0x06002545 RID: 9541 RVA: 0x000A588B File Offset: 0x000A3A8B
		public GetRoomListsResponse()
		{
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x000A5893 File Offset: 0x000A3A93
		// (set) Token: 0x06002547 RID: 9543 RVA: 0x000A589A File Offset: 0x000A3A9A
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

		// Token: 0x06002548 RID: 9544 RVA: 0x000A589C File Offset: 0x000A3A9C
		internal GetRoomListsResponse(ServiceResultCode code, ServiceError error, EmailAddressWrapper[] roomLists) : base(code, error)
		{
			this.RoomLists = roomLists;
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000A58AD File Offset: 0x000A3AAD
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x000A58B5 File Offset: 0x000A3AB5
		[XmlArrayItem("Address", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlArray("RoomLists", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public EmailAddressWrapper[] RoomLists { get; set; }

		// Token: 0x0600254B RID: 9547 RVA: 0x000A58BE File Offset: 0x000A3ABE
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetRoomListsResponseMessage;
		}
	}
}
