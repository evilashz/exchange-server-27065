using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200006E RID: 110
	public class UserCreator : IDataObjectCreator
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000F268 File Offset: 0x0000D468
		public object Create(DataTable table)
		{
			return new User(new ADUser());
		}
	}
}
