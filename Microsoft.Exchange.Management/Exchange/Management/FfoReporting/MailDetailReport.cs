using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003B5 RID: 949
	[Serializable]
	public class MailDetailReport : TrafficObject
	{
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0008C63D File Offset: 0x0008A83D
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x0008C645 File Offset: 0x0008A845
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0008C64E File Offset: 0x0008A84E
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x0008C656 File Offset: 0x0008A856
		[DalConversion("DefaultSerializer", "Domain", new string[]
		{

		})]
		[Redact]
		[ODataInput("Domain")]
		public string Domain { get; private set; }

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0008C65F File Offset: 0x0008A85F
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x0008C667 File Offset: 0x0008A867
		[DalConversion("DefaultSerializer", "Date", new string[]
		{

		})]
		public DateTime Date { get; private set; }

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0008C670 File Offset: 0x0008A870
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x0008C678 File Offset: 0x0008A878
		[DalConversion("DefaultSerializer", "MessageId", new string[]
		{

		})]
		public string MessageId { get; private set; }

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x0008C681 File Offset: 0x0008A881
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x0008C689 File Offset: 0x0008A889
		[ODataInput("Direction")]
		[DalConversion("DefaultSerializer", "Direction", new string[]
		{

		})]
		public string Direction { get; private set; }

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0008C692 File Offset: 0x0008A892
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x0008C69A File Offset: 0x0008A89A
		[DalConversion("DefaultSerializer", "RecipientAddress", new string[]
		{

		})]
		[Redact(RedactAs = typeof(SmtpAddress))]
		public string RecipientAddress { get; private set; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0008C6A3 File Offset: 0x0008A8A3
		// (set) Token: 0x06002195 RID: 8597 RVA: 0x0008C6AB File Offset: 0x0008A8AB
		[DalConversion("DefaultSerializer", "SenderAddress", new string[]
		{

		})]
		[Redact(RedactAs = typeof(SmtpAddress))]
		public string SenderAddress { get; private set; }

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x0008C6B4 File Offset: 0x0008A8B4
		// (set) Token: 0x06002197 RID: 8599 RVA: 0x0008C6BC File Offset: 0x0008A8BC
		[Redact]
		[DalConversion("DefaultSerializer", "SenderIP", new string[]
		{

		})]
		public string SenderIP { get; private set; }

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x0008C6C5 File Offset: 0x0008A8C5
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x0008C6CD File Offset: 0x0008A8CD
		[DalConversion("DefaultSerializer", "Subject", new string[]
		{

		})]
		[Redact]
		public string Subject { get; private set; }

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x0008C6D6 File Offset: 0x0008A8D6
		// (set) Token: 0x0600219B RID: 8603 RVA: 0x0008C6DE File Offset: 0x0008A8DE
		[DalConversion("DefaultSerializer", "MessageSize", new string[]
		{

		})]
		public int MessageSize { get; private set; }

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x0008C6E7 File Offset: 0x0008A8E7
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x0008C6EF File Offset: 0x0008A8EF
		[DalConversion("DefaultSerializer", "InternalMessageId", new string[]
		{

		})]
		public Guid MessageTraceId { get; private set; }

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x0008C6F8 File Offset: 0x0008A8F8
		// (set) Token: 0x0600219F RID: 8607 RVA: 0x0008C700 File Offset: 0x0008A900
		[ODataInput("EventType")]
		public string EventType { get; private set; }
	}
}
