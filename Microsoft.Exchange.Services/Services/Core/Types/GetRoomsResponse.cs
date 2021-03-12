using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000510 RID: 1296
	[XmlType(TypeName = "GetRoomsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRoomsResponse : ResponseMessage
	{
		// Token: 0x0600254C RID: 9548 RVA: 0x000A58C2 File Offset: 0x000A3AC2
		public GetRoomsResponse()
		{
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000A58CA File Offset: 0x000A3ACA
		internal GetRoomsResponse(ServiceResultCode code, ServiceError error, EwsRoomType[] rooms) : base(code, error)
		{
			this.Rooms = rooms;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000A58DB File Offset: 0x000A3ADB
		// (set) Token: 0x0600254F RID: 9551 RVA: 0x000A58E2 File Offset: 0x000A3AE2
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

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x000A58E4 File Offset: 0x000A3AE4
		// (set) Token: 0x06002551 RID: 9553 RVA: 0x000A58EC File Offset: 0x000A3AEC
		[XmlArrayItem("Room", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("Rooms", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public EwsRoomType[] Rooms { get; set; }

		// Token: 0x06002552 RID: 9554 RVA: 0x000A58F5 File Offset: 0x000A3AF5
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetRoomsResponseMessage;
		}
	}
}
