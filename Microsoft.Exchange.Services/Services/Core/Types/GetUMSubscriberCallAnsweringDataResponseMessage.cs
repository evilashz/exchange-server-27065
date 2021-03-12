using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000520 RID: 1312
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetUMSubscriberCallAnsweringDataResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUMSubscriberCallAnsweringDataResponseMessage : ResponseMessage
	{
		// Token: 0x060025A0 RID: 9632 RVA: 0x000A5CBB File Offset: 0x000A3EBB
		public GetUMSubscriberCallAnsweringDataResponseMessage()
		{
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000A5CC4 File Offset: 0x000A3EC4
		internal GetUMSubscriberCallAnsweringDataResponseMessage(ServiceResultCode code, ServiceError error, GetUMSubscriberCallAnsweringDataResponseMessage response) : base(code, error)
		{
			if (response != null)
			{
				this.IsOOF = response.IsOOF;
				this.IsTranscriptionEnabledInMailboxConfig = response.IsTranscriptionEnabledInMailboxConfig;
				this.IsMailboxQuotaExceeded = response.IsMailboxQuotaExceeded;
				this.Greeting = response.Greeting;
				this.GreetingName = response.GreetingName;
				this.TaskTimedOut = response.TaskTimedOut;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000A5D24 File Offset: 0x000A3F24
		// (set) Token: 0x060025A3 RID: 9635 RVA: 0x000A5D2C File Offset: 0x000A3F2C
		[DataMember(Name = "IsOOF")]
		[XmlElement("IsOOF")]
		public bool IsOOF { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x000A5D35 File Offset: 0x000A3F35
		// (set) Token: 0x060025A5 RID: 9637 RVA: 0x000A5D3D File Offset: 0x000A3F3D
		[XmlElement("IsTranscriptionEnabledInMailboxConfig")]
		[DataMember(Name = "IsTranscriptionEnabledInMailboxConfig")]
		public TranscriptionEnabledSetting IsTranscriptionEnabledInMailboxConfig { get; set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x000A5D46 File Offset: 0x000A3F46
		// (set) Token: 0x060025A7 RID: 9639 RVA: 0x000A5D4E File Offset: 0x000A3F4E
		[XmlElement("IsMailboxQuotaExceeded")]
		[DataMember(Name = "IsMailboxQuotaExceeded")]
		public bool IsMailboxQuotaExceeded { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x000A5D57 File Offset: 0x000A3F57
		// (set) Token: 0x060025A9 RID: 9641 RVA: 0x000A5D5F File Offset: 0x000A3F5F
		[DataMember(Name = "TaskTimedOut")]
		[XmlElement("TaskTimedOut")]
		public bool TaskTimedOut { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x000A5D68 File Offset: 0x000A3F68
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x000A5D70 File Offset: 0x000A3F70
		[XmlElement("Greeting")]
		[DataMember(Name = "Greeting")]
		public byte[] Greeting { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x000A5D79 File Offset: 0x000A3F79
		// (set) Token: 0x060025AD RID: 9645 RVA: 0x000A5D81 File Offset: 0x000A3F81
		[XmlElement("GreetingName")]
		[DataMember(Name = "GreetingName")]
		public string GreetingName { get; set; }

		// Token: 0x060025AE RID: 9646 RVA: 0x000A5D8A File Offset: 0x000A3F8A
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetUMSubscriberCallAnsweringDataResponseMessage;
		}
	}
}
