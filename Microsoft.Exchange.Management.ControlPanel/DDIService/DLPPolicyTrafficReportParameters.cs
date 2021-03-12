using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020001BB RID: 443
	[DataContract]
	public class DLPPolicyTrafficReportParameters : WebServiceParameters
	{
		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x0006E4ED File Offset: 0x0006C6ED
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MailTrafficPolicyReport";
			}
		}

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x0006E4F4 File Offset: 0x0006C6F4
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x0006E4FB File Offset: 0x0006C6FB
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x0006E512 File Offset: 0x0006C712
		[DataMember]
		public string EventType
		{
			get
			{
				return base["EventType"].StringArrayJoin(",");
			}
			set
			{
				base["EventType"] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x0006E525 File Offset: 0x0006C725
		// (set) Token: 0x060023F3 RID: 9203 RVA: 0x0006E53C File Offset: 0x0006C73C
		[DataMember]
		public string Direction
		{
			get
			{
				return base["Direction"].StringArrayJoin(",");
			}
			set
			{
				base["Direction"] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x0006E54F File Offset: 0x0006C74F
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x0006E561 File Offset: 0x0006C761
		[DataMember]
		public DateTime? StartDate
		{
			get
			{
				return (DateTime?)base["StartDate"];
			}
			set
			{
				base["StartDate"] = value;
			}
		}

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x0006E574 File Offset: 0x0006C774
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x0006E586 File Offset: 0x0006C786
		[DataMember]
		public DateTime? EndDate
		{
			get
			{
				return (DateTime?)base["EndDate"];
			}
			set
			{
				base["EndDate"] = value;
			}
		}
	}
}
