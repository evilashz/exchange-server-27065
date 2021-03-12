using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200005C RID: 92
	public interface IDataObjectCreator
	{
		// Token: 0x060003AE RID: 942
		object Create(DataTable table);
	}
}
