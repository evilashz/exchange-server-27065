using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000073 RID: 115
	public class ContactCreator : IDataObjectCreator
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		public object Create(DataTable table)
		{
			return new Contact(new ADContact());
		}
	}
}
