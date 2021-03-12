using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200051B RID: 1307
	[XmlType("GetUMCallDataRecordsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUMCallDataRecordsResponseMessage : ResponseMessage
	{
		// Token: 0x06002587 RID: 9607 RVA: 0x000A5BA4 File Offset: 0x000A3DA4
		public GetUMCallDataRecordsResponseMessage()
		{
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000A5BAC File Offset: 0x000A3DAC
		internal GetUMCallDataRecordsResponseMessage(ServiceResultCode code, ServiceError error, GetUMCallDataRecordsResponseMessage response) : base(code, error)
		{
			if (response != null)
			{
				this.CallDataRecords = response.CallDataRecords;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x000A5BC5 File Offset: 0x000A3DC5
		// (set) Token: 0x0600258A RID: 9610 RVA: 0x000A5BCD File Offset: 0x000A3DCD
		[XmlArray(ElementName = "CallDataRecords")]
		[DataMember]
		[XmlArrayItem(ElementName = "CDRData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public CDRData[] CallDataRecords { get; set; }

		// Token: 0x0600258B RID: 9611 RVA: 0x000A5BD6 File Offset: 0x000A3DD6
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetUMCallDataRecordsResponseMessage;
		}
	}
}
