using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200006D RID: 109
	public class DatabaseAvailabilityGroupNetworkCreator : IDataObjectCreator
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000F259 File Offset: 0x0000D459
		public object Create(DataTable table)
		{
			return new DatabaseAvailabilityGroupNetwork();
		}
	}
}
