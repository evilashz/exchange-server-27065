using System;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000088 RID: 136
	internal class AmEvtRejoinNodeForDac : AmEvtBase
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x0001AEA4 File Offset: 0x000190A4
		internal AmEvtRejoinNodeForDac(IADDatabaseAvailabilityGroup dag, IADServer localServer)
		{
			this.Dag = dag;
			this.LocalServer = localServer;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001AEBA File Offset: 0x000190BA
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0001AEC2 File Offset: 0x000190C2
		internal IADDatabaseAvailabilityGroup Dag { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001AECB File Offset: 0x000190CB
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001AED3 File Offset: 0x000190D3
		internal IADServer LocalServer { get; set; }

		// Token: 0x06000544 RID: 1348 RVA: 0x0001AEDC File Offset: 0x000190DC
		public override string ToString()
		{
			return string.Format("{0}: Params: (Dag={1})", base.GetType().Name, this.Dag);
		}
	}
}
