using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000290 RID: 656
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRoomsInternalRequestWrapper
	{
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x00053D28 File Offset: 0x00051F28
		// (set) Token: 0x06001796 RID: 6038 RVA: 0x00053D30 File Offset: 0x00051F30
		[DataMember(Name = "roomList")]
		public string RoomList { get; set; }
	}
}
