using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003C1 RID: 961
	[DataServiceKey("Name")]
	[Serializable]
	public class OutboundConnectorReport : FfoReportObject
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x0008CF1D File Offset: 0x0008B11D
		// (set) Token: 0x06002295 RID: 8853 RVA: 0x0008CF25 File Offset: 0x0008B125
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		[Column(Name = "Organization")]
		public string Organization { get; internal set; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x0008CF2E File Offset: 0x0008B12E
		// (set) Token: 0x06002297 RID: 8855 RVA: 0x0008CF36 File Offset: 0x0008B136
		[Column(Name = "Name")]
		[DalConversion("DefaultSerializer", "Name", new string[]
		{

		})]
		public string Name { get; internal set; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x0008CF3F File Offset: 0x0008B13F
		// (set) Token: 0x06002299 RID: 8857 RVA: 0x0008CF47 File Offset: 0x0008B147
		[ODataInput("Domain")]
		[DalConversion("ValueFromTask", "Domain", new string[]
		{

		})]
		public string Domain { get; internal set; }

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x0008CF50 File Offset: 0x0008B150
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x0008CF58 File Offset: 0x0008B158
		[DalConversion("DefaultSerializer", "IsAcceptedDomain", new string[]
		{

		})]
		[Column(Name = "IsAcceptedDomain")]
		public bool IsAcceptedDomain { get; internal set; }
	}
}
