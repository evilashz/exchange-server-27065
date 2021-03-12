using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.IisTasks
{
	// Token: 0x020000C8 RID: 200
	public abstract class ManageIisWebServiceExtensions : Task
	{
		// Token: 0x1700021B RID: 539
		protected abstract IisWebServiceExtension this[int i]
		{
			get;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000623 RID: 1571
		protected abstract int ExtensionCount { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000624 RID: 1572
		protected abstract string HostName { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000625 RID: 1573
		protected abstract string GroupDescription { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000626 RID: 1574
		protected abstract string GroupID { get; }
	}
}
