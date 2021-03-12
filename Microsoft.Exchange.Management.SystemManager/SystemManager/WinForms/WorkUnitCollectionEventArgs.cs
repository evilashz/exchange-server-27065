using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200016C RID: 364
	public class WorkUnitCollectionEventArgs : EventArgs
	{
		// Token: 0x06000EE6 RID: 3814 RVA: 0x00039492 File Offset: 0x00037692
		internal WorkUnitCollectionEventArgs(WorkUnitCollection workUnits)
		{
			this.workUnits = workUnits;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x000394A1 File Offset: 0x000376A1
		internal WorkUnitCollection WorkUnits
		{
			get
			{
				return this.workUnits;
			}
		}

		// Token: 0x040005FA RID: 1530
		private WorkUnitCollection workUnits;
	}
}
