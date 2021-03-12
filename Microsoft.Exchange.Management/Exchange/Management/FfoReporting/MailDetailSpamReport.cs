using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003B6 RID: 950
	[Serializable]
	public class MailDetailSpamReport : DetailObject
	{
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x0008C711 File Offset: 0x0008A911
		// (set) Token: 0x060021A2 RID: 8610 RVA: 0x0008C719 File Offset: 0x0008A919
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x0008C722 File Offset: 0x0008A922
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x0008C72A File Offset: 0x0008A92A
		[DalConversion("DefaultSerializer", "Received", new string[]
		{

		})]
		public DateTime Date { get; private set; }

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x0008C733 File Offset: 0x0008A933
		// (set) Token: 0x060021A6 RID: 8614 RVA: 0x0008C73B File Offset: 0x0008A93B
		[DalConversion("DefaultSerializer", "ClientMessageId", new string[]
		{

		})]
		[ODataInput("MessageId")]
		public string MessageId { get; private set; }

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x0008C744 File Offset: 0x0008A944
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x0008C74C File Offset: 0x0008A94C
		[Redact]
		[DalConversion("DefaultSerializer", "Domain", new string[]
		{

		})]
		[ODataInput("Domain")]
		public string Domain { get; private set; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x0008C755 File Offset: 0x0008A955
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x0008C75D File Offset: 0x0008A95D
		[DalConversion("DefaultSerializer", "MessageSubject", new string[]
		{

		})]
		[Redact]
		public string Subject { get; private set; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x0008C766 File Offset: 0x0008A966
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x0008C76E File Offset: 0x0008A96E
		[DalConversion("DefaultSerializer", "MessageSize", new string[]
		{

		})]
		public int MessageSize { get; private set; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x0008C777 File Offset: 0x0008A977
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x0008C77F File Offset: 0x0008A97F
		[ODataInput("Direction")]
		[DalConversion("DefaultSerializer", "Direction", new string[]
		{

		})]
		public string Direction { get; private set; }

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x0008C788 File Offset: 0x0008A988
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x0008C790 File Offset: 0x0008A990
		[Redact(RedactAs = typeof(SmtpAddress))]
		[ODataInput("SenderAddress")]
		[DalConversion("DefaultSerializer", "SenderAddress", new string[]
		{

		})]
		public string SenderAddress { get; private set; }

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0008C799 File Offset: 0x0008A999
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x0008C7A1 File Offset: 0x0008A9A1
		[Redact(RedactAs = typeof(SmtpAddress))]
		[DalConversion("DefaultSerializer", "RecipientAddress", new string[]
		{

		})]
		[ODataInput("RecipientAddress")]
		public string RecipientAddress { get; private set; }

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x0008C7AA File Offset: 0x0008A9AA
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x0008C7B2 File Offset: 0x0008A9B2
		[DalConversion("DefaultSerializer", "EventType", new string[]
		{

		})]
		[ODataInput("EventType")]
		public string EventType { get; private set; }

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x0008C7BB File Offset: 0x0008A9BB
		// (set) Token: 0x060021B6 RID: 8630 RVA: 0x0008C7C3 File Offset: 0x0008A9C3
		[DalConversion("DefaultSerializer", "Action", new string[]
		{

		})]
		[ODataInput("Action")]
		public string Action { get; private set; }

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0008C7CC File Offset: 0x0008A9CC
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x0008C7D4 File Offset: 0x0008A9D4
		[DalConversion("DefaultSerializer", "InternalMessageId", new string[]
		{

		})]
		public Guid MessageTraceId { get; private set; }
	}
}
