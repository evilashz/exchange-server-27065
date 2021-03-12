using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000074 RID: 116
	public class DynamicDistributionGroupCreator : IDataObjectCreator
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		public object Create(DataTable table)
		{
			return new DynamicDistributionGroup(new ADDynamicGroup());
		}
	}
}
