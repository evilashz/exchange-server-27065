using System;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000027 RID: 39
	public interface IHashProvider
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000090 RID: 144
		bool IsInitialized { get; }

		// Token: 0x06000091 RID: 145
		bool Initialize();

		// Token: 0x06000092 RID: 146
		string HashString(string input);
	}
}
