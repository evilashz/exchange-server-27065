using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200008B RID: 139
	internal class AmEvtPeriodicDbStateRestore : AmEvtBase
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x0001AF09 File Offset: 0x00019109
		internal AmEvtPeriodicDbStateRestore(object context)
		{
			this.Context = context;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001AF18 File Offset: 0x00019118
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0001AF20 File Offset: 0x00019120
		internal object Context { get; private set; }
	}
}
