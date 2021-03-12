using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E2 RID: 1250
	[XmlType("FindPeopleResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class FindPeopleResponseMessage : ResponseMessage
	{
		// Token: 0x06002482 RID: 9346 RVA: 0x000A4F09 File Offset: 0x000A3109
		public FindPeopleResponseMessage()
		{
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000A4F11 File Offset: 0x000A3111
		internal FindPeopleResponseMessage(ServiceResultCode code, ServiceError error, FindPeopleResult findPeopleResult) : base(code, error)
		{
			if (code == ServiceResultCode.Success)
			{
				this.ResultSet = findPeopleResult.PersonaList;
				this.TotalNumberOfPeopleInView = findPeopleResult.TotalNumberOfPeopleInView;
				this.FirstLoadedRowIndex = findPeopleResult.FirstLoadedRowIndex;
				this.FirstMatchingRowIndex = findPeopleResult.FirstMatchingRowIndex;
			}
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000A4F4F File Offset: 0x000A314F
		public override ResponseType GetResponseType()
		{
			return ResponseType.FindPeopleResponseMessage;
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x000A4F53 File Offset: 0x000A3153
		// (set) Token: 0x06002486 RID: 9350 RVA: 0x000A4F5B File Offset: 0x000A315B
		[DataMember]
		[XmlArray(ElementName = "People")]
		[XmlArrayItem(ElementName = "Persona", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public Persona[] ResultSet { get; set; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000A4F64 File Offset: 0x000A3164
		// (set) Token: 0x06002488 RID: 9352 RVA: 0x000A4F6C File Offset: 0x000A316C
		[DataMember]
		public int TotalNumberOfPeopleInView { get; set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x000A4F75 File Offset: 0x000A3175
		// (set) Token: 0x0600248A RID: 9354 RVA: 0x000A4F7D File Offset: 0x000A317D
		[DataMember]
		public int FirstMatchingRowIndex { get; set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x000A4F86 File Offset: 0x000A3186
		// (set) Token: 0x0600248C RID: 9356 RVA: 0x000A4F8E File Offset: 0x000A318E
		[DataMember]
		public int FirstLoadedRowIndex { get; set; }
	}
}
