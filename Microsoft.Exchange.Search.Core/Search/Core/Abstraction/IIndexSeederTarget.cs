using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200002A RID: 42
	internal interface IIndexSeederTarget : IDisposable
	{
		// Token: 0x060000E4 RID: 228
		string GetSeedingEndPoint();
	}
}
