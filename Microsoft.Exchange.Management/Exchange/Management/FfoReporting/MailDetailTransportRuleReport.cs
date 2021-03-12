using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003B7 RID: 951
	[Serializable]
	public class MailDetailTransportRuleReport : DetailObject
	{
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0008C7E5 File Offset: 0x0008A9E5
		// (set) Token: 0x060021BB RID: 8635 RVA: 0x0008C7ED File Offset: 0x0008A9ED
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x0008C7F6 File Offset: 0x0008A9F6
		// (set) Token: 0x060021BD RID: 8637 RVA: 0x0008C7FE File Offset: 0x0008A9FE
		[DalConversion("DefaultSerializer", "Received", new string[]
		{

		})]
		public DateTime Date { get; private set; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x0008C807 File Offset: 0x0008AA07
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x0008C80F File Offset: 0x0008AA0F
		[ODataInput("MessageId")]
		[DalConversion("DefaultSerializer", "ClientMessageId", new string[]
		{

		})]
		public string MessageId { get; private set; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0008C818 File Offset: 0x0008AA18
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x0008C820 File Offset: 0x0008AA20
		[Redact]
		[DalConversion("DefaultSerializer", "Domain", new string[]
		{

		})]
		[ODataInput("Domain")]
		public string Domain { get; private set; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0008C829 File Offset: 0x0008AA29
		// (set) Token: 0x060021C3 RID: 8643 RVA: 0x0008C831 File Offset: 0x0008AA31
		[DalConversion("DefaultSerializer", "MessageSubject", new string[]
		{

		})]
		[Redact]
		public string Subject { get; private set; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0008C83A File Offset: 0x0008AA3A
		// (set) Token: 0x060021C5 RID: 8645 RVA: 0x0008C842 File Offset: 0x0008AA42
		[DalConversion("DefaultSerializer", "MessageSize", new string[]
		{

		})]
		public int MessageSize { get; private set; }

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x0008C84B File Offset: 0x0008AA4B
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x0008C853 File Offset: 0x0008AA53
		[DalConversion("DefaultSerializer", "Direction", new string[]
		{

		})]
		[ODataInput("Direction")]
		public string Direction { get; private set; }

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x0008C85C File Offset: 0x0008AA5C
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x0008C864 File Offset: 0x0008AA64
		[ODataInput("SenderAddress")]
		[Redact(RedactAs = typeof(SmtpAddress))]
		[DalConversion("DefaultSerializer", "SenderAddress", new string[]
		{

		})]
		public string SenderAddress { get; private set; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x0008C86D File Offset: 0x0008AA6D
		// (set) Token: 0x060021CB RID: 8651 RVA: 0x0008C875 File Offset: 0x0008AA75
		[Redact(RedactAs = typeof(SmtpAddress))]
		[ODataInput("RecipientAddress")]
		[DalConversion("DefaultSerializer", "RecipientAddress", new string[]
		{

		})]
		public string RecipientAddress { get; private set; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x0008C87E File Offset: 0x0008AA7E
		// (set) Token: 0x060021CD RID: 8653 RVA: 0x0008C886 File Offset: 0x0008AA86
		[ODataInput("TransportRule")]
		[DalConversion("DefaultSerializer", "TransportRuleName", new string[]
		{

		})]
		public string TransportRule { get; private set; }

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x0008C88F File Offset: 0x0008AA8F
		// (set) Token: 0x060021CF RID: 8655 RVA: 0x0008C897 File Offset: 0x0008AA97
		[DalConversion("DefaultSerializer", "EventType", new string[]
		{

		})]
		[ODataInput("EventType")]
		public string EventType { get; private set; }

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x0008C8A0 File Offset: 0x0008AAA0
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x0008C8A8 File Offset: 0x0008AAA8
		[ODataInput("Action")]
		[DalConversion("DefaultSerializer", "Action", new string[]
		{

		})]
		public string Action { get; private set; }

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0008C8B1 File Offset: 0x0008AAB1
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x0008C8B9 File Offset: 0x0008AAB9
		[DalConversion("DefaultSerializer", "InternalMessageId", new string[]
		{

		})]
		public Guid MessageTraceId { get; private set; }
	}
}
