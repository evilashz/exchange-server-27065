using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000507 RID: 1287
	[DataContract(Name = "GetPeopleConnectStateResponseMessage", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetPeopleConnectStateResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public sealed class GetPeopleConnectStateResponseMessage : ResponseMessage
	{
		// Token: 0x06002527 RID: 9511 RVA: 0x000A574F File Offset: 0x000A394F
		public GetPeopleConnectStateResponseMessage()
		{
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000A5757 File Offset: 0x000A3957
		internal GetPeopleConnectStateResponseMessage(ServiceResultCode code, ServiceError error, PeopleConnectionState result) : base(code, error)
		{
			this.PeopleConnectionState = result;
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x000A5768 File Offset: 0x000A3968
		// (set) Token: 0x0600252A RID: 9514 RVA: 0x000A5770 File Offset: 0x000A3970
		[DataMember]
		[XmlElement]
		public PeopleConnectionState PeopleConnectionState { get; set; }
	}
}
