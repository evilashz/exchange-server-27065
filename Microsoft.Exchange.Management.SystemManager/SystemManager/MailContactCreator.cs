using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000072 RID: 114
	public class MailContactCreator : IDataObjectCreator
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x0000F2B8 File Offset: 0x0000D4B8
		public object Create(DataTable table)
		{
			return new MailContact(new ADContact());
		}
	}
}
