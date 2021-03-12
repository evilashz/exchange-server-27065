using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000076 RID: 118
	public class RemoteMailboxCreator : IDataObjectCreator
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0000F308 File Offset: 0x0000D508
		public object Create(DataTable table)
		{
			return new RemoteMailbox(new ADUser());
		}
	}
}
