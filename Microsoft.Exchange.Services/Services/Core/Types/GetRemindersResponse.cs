using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200050E RID: 1294
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetRemindersResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetRemindersResponse : ResponseMessage
	{
		// Token: 0x06002540 RID: 9536 RVA: 0x000A585D File Offset: 0x000A3A5D
		public GetRemindersResponse()
		{
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000A5865 File Offset: 0x000A3A65
		internal GetRemindersResponse(ServiceResultCode code, ServiceError error, ReminderType[] reminders) : base(code, error)
		{
			this.Reminders = reminders;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x000A5876 File Offset: 0x000A3A76
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x000A587E File Offset: 0x000A3A7E
		[XmlArrayItem("Reminder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public ReminderType[] Reminders { get; set; }

		// Token: 0x06002544 RID: 9540 RVA: 0x000A5887 File Offset: 0x000A3A87
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetRemindersResponseMessage;
		}
	}
}
