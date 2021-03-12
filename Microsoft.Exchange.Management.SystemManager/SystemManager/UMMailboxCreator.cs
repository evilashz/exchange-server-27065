using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200006F RID: 111
	public class UMMailboxCreator : IDataObjectCreator
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0000F27C File Offset: 0x0000D47C
		public object Create(DataTable table)
		{
			return new UMMailbox(new ADUser());
		}
	}
}
