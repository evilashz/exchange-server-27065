using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200001F RID: 31
	internal interface IWMIDataProvider
	{
		// Token: 0x06000050 RID: 80
		Dictionary<string, object>[] Run(string wmiQuery);
	}
}
