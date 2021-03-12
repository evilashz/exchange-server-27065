using System;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000114 RID: 276
	public interface IRunnable
	{
		// Token: 0x06001FD3 RID: 8147
		bool IsRunnable(DataRow input, DataTable dataTable);
	}
}
