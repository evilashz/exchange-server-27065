using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200056C RID: 1388
	[XmlType("SyncPeopleResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncPeopleResponseMessage : SyncPersonaContactsResponseBase
	{
		// Token: 0x060026DC RID: 9948 RVA: 0x000A6B5B File Offset: 0x000A4D5B
		public SyncPeopleResponseMessage()
		{
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000A6B63 File Offset: 0x000A4D63
		internal SyncPeopleResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000A6B6D File Offset: 0x000A4D6D
		public override ResponseType GetResponseType()
		{
			return ResponseType.SyncPeopleResponseMessage;
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x000A6B71 File Offset: 0x000A4D71
		// (set) Token: 0x060026E0 RID: 9952 RVA: 0x000A6B79 File Offset: 0x000A4D79
		[DataMember(EmitDefaultValue = false)]
		[XmlArray(ElementName = "People")]
		[XmlArrayItem(ElementName = "Persona", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public Persona[] People { get; set; }
	}
}
