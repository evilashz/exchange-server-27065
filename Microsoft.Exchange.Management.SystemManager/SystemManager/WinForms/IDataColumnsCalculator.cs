using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000060 RID: 96
	public interface IDataColumnsCalculator
	{
		// Token: 0x060003B2 RID: 946
		void Calculate(ResultsLoaderProfile profile, DataTable dataTable, DataRow dataRow);
	}
}
