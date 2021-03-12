using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IABSessionSettings
	{
		// Token: 0x0600008F RID: 143
		bool TryGet<T>(string propertyName, out T propertyValue);

		// Token: 0x06000090 RID: 144
		T Get<T>(string propertyName);
	}
}
