using System;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003BC RID: 956
	[Serializable]
	public class MailTrafficTopReport : TrafficObject
	{
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x0008CC5E File Offset: 0x0008AE5E
		// (set) Token: 0x06002242 RID: 8770 RVA: 0x0008CC66 File Offset: 0x0008AE66
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x0008CC6F File Offset: 0x0008AE6F
		// (set) Token: 0x06002244 RID: 8772 RVA: 0x0008CC77 File Offset: 0x0008AE77
		[ODataInput("Domain")]
		[DalConversion("DefaultSerializer", "Domain", new string[]
		{

		})]
		[Redact]
		public string Domain { get; private set; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x0008CC80 File Offset: 0x0008AE80
		// (set) Token: 0x06002246 RID: 8774 RVA: 0x0008CC88 File Offset: 0x0008AE88
		[DalConversion("DateFromIntSerializer", "DateKey", new string[]
		{
			"HourKey"
		})]
		public DateTime Date { get; private set; }

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x0008CC91 File Offset: 0x0008AE91
		// (set) Token: 0x06002248 RID: 8776 RVA: 0x0008CC99 File Offset: 0x0008AE99
		[Redact]
		[DalConversion("DefaultSerializer", "Name", new string[]
		{

		})]
		public string Name { get; private set; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x0008CCA2 File Offset: 0x0008AEA2
		// (set) Token: 0x0600224A RID: 8778 RVA: 0x0008CCAA File Offset: 0x0008AEAA
		[DalConversion("DefaultSerializer", "EventType", new string[]
		{

		})]
		[ODataInput("EventType")]
		public string EventType { get; private set; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0008CCB3 File Offset: 0x0008AEB3
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x0008CCBB File Offset: 0x0008AEBB
		[ODataInput("Direction")]
		[DalConversion("DefaultSerializer", "Direction", new string[]
		{

		})]
		public string Direction { get; private set; }

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x0008CCC4 File Offset: 0x0008AEC4
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x0008CCCC File Offset: 0x0008AECC
		[DalConversion("DefaultSerializer", "MessageCount", new string[]
		{

		})]
		public int MessageCount { get; private set; }

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0008CCD5 File Offset: 0x0008AED5
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x0008CCDD File Offset: 0x0008AEDD
		[ODataInput("SummarizeBy")]
		public string SummarizeBy { get; private set; }
	}
}
