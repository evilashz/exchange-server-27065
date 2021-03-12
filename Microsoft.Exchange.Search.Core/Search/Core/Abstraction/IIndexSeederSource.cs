using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000029 RID: 41
	internal interface IIndexSeederSource : IDisposable
	{
		// Token: 0x060000E1 RID: 225
		string SeedToEndPoint(string seedingEndPoint, string reason);

		// Token: 0x060000E2 RID: 226
		int GetProgress(string identifier);

		// Token: 0x060000E3 RID: 227
		void Cancel(string identifier, string reason);
	}
}
