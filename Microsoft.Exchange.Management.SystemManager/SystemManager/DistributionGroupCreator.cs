using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000070 RID: 112
	public class DistributionGroupCreator : IDataObjectCreator
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0000F290 File Offset: 0x0000D490
		public object Create(DataTable table)
		{
			return new DistributionGroup(new ADGroup());
		}
	}
}
