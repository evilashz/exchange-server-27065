using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000075 RID: 117
	public class MailUserCreator : IDataObjectCreator
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		public object Create(DataTable table)
		{
			return new MailUser(new ADUser());
		}
	}
}
