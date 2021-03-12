using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004EA RID: 1258
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetBirthdayCalendarViewResponseMessage : ResponseMessage
	{
		// Token: 0x060024A7 RID: 9383 RVA: 0x000A50DB File Offset: 0x000A32DB
		public GetBirthdayCalendarViewResponseMessage()
		{
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000A50E3 File Offset: 0x000A32E3
		internal GetBirthdayCalendarViewResponseMessage(ServiceResultCode code, ServiceError error, GetBirthdayCalendarViewResponseMessage response) : base(code, error)
		{
			this.BirthdayEvents = null;
			if (response != null)
			{
				this.BirthdayEvents = response.BirthdayEvents;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x000A5103 File Offset: 0x000A3303
		// (set) Token: 0x060024AA RID: 9386 RVA: 0x000A510B File Offset: 0x000A330B
		[DataMember]
		public BirthdayEvent[] BirthdayEvents { get; set; }

		// Token: 0x060024AB RID: 9387 RVA: 0x000A5114 File Offset: 0x000A3314
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetBirthdayCalendarViewResponseMessage;
		}
	}
}
