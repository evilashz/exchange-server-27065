using System;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008F9 RID: 2297
	internal interface IContextParameters
	{
		// Token: 0x06005182 RID: 20866
		T Get<T>(string name);

		// Token: 0x06005183 RID: 20867
		void Set<T>(string name, T value);
	}
}
