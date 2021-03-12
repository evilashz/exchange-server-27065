using System;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003BA RID: 954
	[Serializable]
	public class MailTrafficReport : TrafficObject
	{
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x0008C9FB File Offset: 0x0008ABFB
		// (set) Token: 0x060021FA RID: 8698 RVA: 0x0008CA03 File Offset: 0x0008AC03
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x0008CA0C File Offset: 0x0008AC0C
		// (set) Token: 0x060021FC RID: 8700 RVA: 0x0008CA14 File Offset: 0x0008AC14
		[ODataInput("Domain")]
		[DalConversion("DefaultSerializer", "Domain", new string[]
		{

		})]
		[Redact]
		public string Domain { get; private set; }

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x0008CA1D File Offset: 0x0008AC1D
		// (set) Token: 0x060021FE RID: 8702 RVA: 0x0008CA25 File Offset: 0x0008AC25
		[DalConversion("DateFromIntSerializer", "DateKey", new string[]
		{
			"HourKey"
		})]
		public DateTime Date { get; private set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x0008CA2E File Offset: 0x0008AC2E
		// (set) Token: 0x06002200 RID: 8704 RVA: 0x0008CA36 File Offset: 0x0008AC36
		[DalConversion("DefaultSerializer", "EventType", new string[]
		{

		})]
		[ODataInput("EventType")]
		public string EventType { get; private set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x0008CA3F File Offset: 0x0008AC3F
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x0008CA47 File Offset: 0x0008AC47
		[DalConversion("DefaultSerializer", "Direction", new string[]
		{

		})]
		[ODataInput("Direction")]
		public string Direction { get; private set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x0008CA50 File Offset: 0x0008AC50
		// (set) Token: 0x06002204 RID: 8708 RVA: 0x0008CA58 File Offset: 0x0008AC58
		[ODataInput("Action")]
		[DalConversion("DefaultSerializer", "Action", new string[]
		{

		})]
		public string Action { get; private set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x0008CA61 File Offset: 0x0008AC61
		// (set) Token: 0x06002206 RID: 8710 RVA: 0x0008CA69 File Offset: 0x0008AC69
		[DalConversion("DefaultSerializer", "MessageCount", new string[]
		{

		})]
		public int MessageCount { get; private set; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x0008CA72 File Offset: 0x0008AC72
		// (set) Token: 0x06002208 RID: 8712 RVA: 0x0008CA7A File Offset: 0x0008AC7A
		[ODataInput("SummarizeBy")]
		public string SummarizeBy { get; private set; }
	}
}
