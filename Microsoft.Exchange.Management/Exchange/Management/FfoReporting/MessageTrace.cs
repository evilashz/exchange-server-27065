using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003BE RID: 958
	[Serializable]
	public class MessageTrace : MtrtObject
	{
		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x0008CD29 File Offset: 0x0008AF29
		// (set) Token: 0x0600225A RID: 8794 RVA: 0x0008CD31 File Offset: 0x0008AF31
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x0008CD3A File Offset: 0x0008AF3A
		// (set) Token: 0x0600225C RID: 8796 RVA: 0x0008CD42 File Offset: 0x0008AF42
		[DalConversion("DefaultSerializer", "ClientMessageId", new string[]
		{

		})]
		[ODataInput("MessageId")]
		public string MessageId { get; internal set; }

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x0008CD4B File Offset: 0x0008AF4B
		// (set) Token: 0x0600225E RID: 8798 RVA: 0x0008CD53 File Offset: 0x0008AF53
		[DalConversion("DefaultSerializer", "Received", new string[]
		{

		})]
		public DateTime Received { get; internal set; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x0008CD5C File Offset: 0x0008AF5C
		// (set) Token: 0x06002260 RID: 8800 RVA: 0x0008CD64 File Offset: 0x0008AF64
		[ODataInput("SenderAddress")]
		[DalConversion("DefaultSerializer", "SenderAddress", new string[]
		{

		})]
		[Redact(RedactAs = typeof(SmtpAddress))]
		public string SenderAddress { get; internal set; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x0008CD6D File Offset: 0x0008AF6D
		// (set) Token: 0x06002262 RID: 8802 RVA: 0x0008CD75 File Offset: 0x0008AF75
		[Redact(RedactAs = typeof(SmtpAddress))]
		[ODataInput("RecipientAddress")]
		[DalConversion("DefaultSerializer", "RecipientAddress", new string[]
		{

		})]
		public string RecipientAddress { get; internal set; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x0008CD7E File Offset: 0x0008AF7E
		// (set) Token: 0x06002264 RID: 8804 RVA: 0x0008CD86 File Offset: 0x0008AF86
		[Redact]
		[DalConversion("DefaultSerializer", "MessageSubject", new string[]
		{

		})]
		public string Subject { get; internal set; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x0008CD8F File Offset: 0x0008AF8F
		// (set) Token: 0x06002266 RID: 8806 RVA: 0x0008CD97 File Offset: 0x0008AF97
		[ODataInput("Status")]
		[DalConversion("DefaultSerializer", "MailDeliveryStatus", new string[]
		{

		})]
		public string Status { get; internal set; }

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x0008CDA0 File Offset: 0x0008AFA0
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x0008CDA8 File Offset: 0x0008AFA8
		[Redact]
		[ODataInput("ToIP")]
		[DalConversion("DefaultSerializer", "ToIP", new string[]
		{

		})]
		public string ToIP { get; internal set; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x0008CDB1 File Offset: 0x0008AFB1
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x0008CDB9 File Offset: 0x0008AFB9
		[ODataInput("FromIP")]
		[DalConversion("DefaultSerializer", "FromIP", new string[]
		{

		})]
		[Redact]
		public string FromIP { get; internal set; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x0008CDC2 File Offset: 0x0008AFC2
		// (set) Token: 0x0600226C RID: 8812 RVA: 0x0008CDCA File Offset: 0x0008AFCA
		[DalConversion("DefaultSerializer", "MessageSize", new string[]
		{

		})]
		public int Size { get; internal set; }

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x0008CDD3 File Offset: 0x0008AFD3
		// (set) Token: 0x0600226E RID: 8814 RVA: 0x0008CDDB File Offset: 0x0008AFDB
		[ODataInput("MessageTraceId")]
		[DalConversion("DefaultSerializer", "InternalMessageId", new string[]
		{

		})]
		public Guid MessageTraceId { get; internal set; }
	}
}
