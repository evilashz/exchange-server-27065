using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000016 RID: 22
	internal interface IClusApiHook
	{
		// Token: 0x06000099 RID: 153
		int CallBack(ClusApiHooks api, string hintStr, Func<int> func);
	}
}
