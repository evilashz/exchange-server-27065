using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200022C RID: 556
	public interface ISelectedObjectsProvider
	{
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060019AA RID: 6570
		DataTable SelectedObjects { get; }
	}
}
