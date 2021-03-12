using System;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x0200038D RID: 909
	[Serializable]
	public class FfoMigrationReport : FfoReportObject
	{
		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x00087CAC File Offset: 0x00085EAC
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x00087CB4 File Offset: 0x00085EB4
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x00087CBD File Offset: 0x00085EBD
		// (set) Token: 0x06001FB1 RID: 8113 RVA: 0x00087CC5 File Offset: 0x00085EC5
		[DalConversion("DefaultSerializer", "Report", new string[]
		{

		})]
		public string Report { get; private set; }
	}
}
