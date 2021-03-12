using System;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003B9 RID: 953
	[Serializable]
	public class MailTrafficPolicyReport : TrafficObject
	{
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x0008C949 File Offset: 0x0008AB49
		// (set) Token: 0x060021E5 RID: 8677 RVA: 0x0008C951 File Offset: 0x0008AB51
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0008C95A File Offset: 0x0008AB5A
		// (set) Token: 0x060021E7 RID: 8679 RVA: 0x0008C962 File Offset: 0x0008AB62
		[DalConversion("DefaultSerializer", "Domain", new string[]
		{

		})]
		[Redact]
		[ODataInput("Domain")]
		public string Domain { get; private set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x0008C96B File Offset: 0x0008AB6B
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x0008C973 File Offset: 0x0008AB73
		[DalConversion("DateFromIntSerializer", "DateKey", new string[]
		{
			"HourKey"
		})]
		public DateTime Date { get; private set; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x0008C97C File Offset: 0x0008AB7C
		// (set) Token: 0x060021EB RID: 8683 RVA: 0x0008C984 File Offset: 0x0008AB84
		[DalConversion("DefaultSerializer", "PolicyName", new string[]
		{

		})]
		[ODataInput("DlpPolicy")]
		public string DlpPolicy { get; private set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x0008C98D File Offset: 0x0008AB8D
		// (set) Token: 0x060021ED RID: 8685 RVA: 0x0008C995 File Offset: 0x0008AB95
		[DalConversion("DefaultSerializer", "RuleName", new string[]
		{

		})]
		[ODataInput("TransportRule")]
		public string TransportRule { get; private set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x0008C99E File Offset: 0x0008AB9E
		// (set) Token: 0x060021EF RID: 8687 RVA: 0x0008C9A6 File Offset: 0x0008ABA6
		[DalConversion("DefaultSerializer", "Action", new string[]
		{

		})]
		[ODataInput("Action")]
		public string Action { get; private set; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x0008C9AF File Offset: 0x0008ABAF
		// (set) Token: 0x060021F1 RID: 8689 RVA: 0x0008C9B7 File Offset: 0x0008ABB7
		[ODataInput("EventType")]
		[DalConversion("DefaultSerializer", "EventType", new string[]
		{

		})]
		public string EventType { get; private set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x0008C9C0 File Offset: 0x0008ABC0
		// (set) Token: 0x060021F3 RID: 8691 RVA: 0x0008C9C8 File Offset: 0x0008ABC8
		[DalConversion("DefaultSerializer", "Direction", new string[]
		{

		})]
		[ODataInput("Direction")]
		public string Direction { get; private set; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x0008C9D1 File Offset: 0x0008ABD1
		// (set) Token: 0x060021F5 RID: 8693 RVA: 0x0008C9D9 File Offset: 0x0008ABD9
		[DalConversion("DefaultSerializer", "MessageCount", new string[]
		{

		})]
		public int MessageCount { get; private set; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x0008C9E2 File Offset: 0x0008ABE2
		// (set) Token: 0x060021F7 RID: 8695 RVA: 0x0008C9EA File Offset: 0x0008ABEA
		[ODataInput("SummarizeBy")]
		public string SummarizeBy { get; private set; }
	}
}
