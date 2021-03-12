using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200005F RID: 95
	public interface ILoadableFromProfile
	{
		// Token: 0x060003B1 RID: 945
		bool IsLoadableFrom(ResultsLoaderProfile profile, DataRow row);
	}
}
