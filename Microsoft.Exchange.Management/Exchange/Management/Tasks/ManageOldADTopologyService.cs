using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E0 RID: 736
	public class ManageOldADTopologyService : ManageADTopologyService
	{
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x0007206F File Offset: 0x0007026F
		internal string ServiceName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x00072077 File Offset: 0x00070277
		protected override string Name
		{
			get
			{
				return "ADTopologyService";
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x0007207E File Offset: 0x0007027E
		protected string CurrentName
		{
			get
			{
				return "MSExchangeADTopology";
			}
		}
	}
}
