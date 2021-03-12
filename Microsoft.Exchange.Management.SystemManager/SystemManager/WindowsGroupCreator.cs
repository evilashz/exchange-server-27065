using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000071 RID: 113
	public class WindowsGroupCreator : IDataObjectCreator
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0000F2A4 File Offset: 0x0000D4A4
		public object Create(DataTable table)
		{
			return new WindowsGroup(new ADGroup());
		}
	}
}
