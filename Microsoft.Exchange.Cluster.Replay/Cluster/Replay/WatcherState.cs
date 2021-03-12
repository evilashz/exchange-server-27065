using System;
using System.IO;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200037A RID: 890
	internal class WatcherState : FileSystemWatcher
	{
		// Token: 0x060023B4 RID: 9140 RVA: 0x000A76E5 File Offset: 0x000A58E5
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x04000F49 RID: 3913
		internal ShipControl M_thisShip;
	}
}
