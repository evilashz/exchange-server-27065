using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000131 RID: 305
	public interface IExchangeCommandFilterBuilder
	{
		// Token: 0x06000C2B RID: 3115
		void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row);
	}
}
