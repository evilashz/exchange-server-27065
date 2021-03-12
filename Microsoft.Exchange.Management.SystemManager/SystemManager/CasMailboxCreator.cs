using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200006C RID: 108
	public class CasMailboxCreator : IDataObjectCreator
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0000F245 File Offset: 0x0000D445
		public object Create(DataTable table)
		{
			return new CASMailbox(new ADUser());
		}
	}
}
