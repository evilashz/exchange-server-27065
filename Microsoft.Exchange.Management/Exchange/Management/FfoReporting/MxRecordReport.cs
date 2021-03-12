using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003C0 RID: 960
	[DataServiceKey("PointsToService")]
	[Serializable]
	public class MxRecordReport : FfoReportObject
	{
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x0008CE9E File Offset: 0x0008B09E
		// (set) Token: 0x06002286 RID: 8838 RVA: 0x0008CEA6 File Offset: 0x0008B0A6
		[Column(Name = "Organization")]
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; internal set; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0008CEAF File Offset: 0x0008B0AF
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x0008CEB7 File Offset: 0x0008B0B7
		[DalConversion("DefaultSerializer", "IsAcceptedDomain", new string[]
		{

		})]
		[Column(Name = "IsAcceptedDomain")]
		public bool IsAcceptedDomain { get; internal set; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x0008CEC0 File Offset: 0x0008B0C0
		// (set) Token: 0x0600228A RID: 8842 RVA: 0x0008CEC8 File Offset: 0x0008B0C8
		[DalConversion("DefaultSerializer", "MxRecordExists", new string[]
		{

		})]
		[Column(Name = "RecordExists")]
		public bool RecordExists { get; internal set; }

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x0008CED1 File Offset: 0x0008B0D1
		// (set) Token: 0x0600228C RID: 8844 RVA: 0x0008CED9 File Offset: 0x0008B0D9
		[Column(Name = "PointsToService")]
		[DalConversion("DefaultSerializer", "IsMxRecordPointingToService", new string[]
		{

		})]
		public bool PointsToService { get; internal set; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x0008CEE2 File Offset: 0x0008B0E2
		// (set) Token: 0x0600228E RID: 8846 RVA: 0x0008CEEA File Offset: 0x0008B0EA
		[Column(Name = "HighestPriorityMailhost")]
		[DalConversion("DefaultSerializer", "HighestPriorityMailhost", new string[]
		{

		})]
		public string HighestPriorityMailhost { get; internal set; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x0008CEF3 File Offset: 0x0008B0F3
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x0008CEFB File Offset: 0x0008B0FB
		[Column(Name = "HighestPriorityMailhostIpAddress")]
		[DalConversion("DefaultSerializer", "HighestPriorityMailhostIpAddress", new string[]
		{

		})]
		public string HighestPriorityMailhostIpAddress { get; internal set; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x0008CF04 File Offset: 0x0008B104
		// (set) Token: 0x06002292 RID: 8850 RVA: 0x0008CF0C File Offset: 0x0008B10C
		[DalConversion("ValueFromTask", "Domain", new string[]
		{

		})]
		[ODataInput("Domain")]
		public string Domain { get; internal set; }
	}
}
