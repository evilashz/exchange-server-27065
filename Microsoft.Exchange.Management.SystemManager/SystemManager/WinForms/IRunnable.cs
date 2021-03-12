using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200005A RID: 90
	public interface IRunnable
	{
		// Token: 0x060003AC RID: 940
		bool IsRunnable(DataRow row);
	}
}
