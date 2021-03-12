using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005A5 RID: 1445
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IBody
	{
		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06003B25 RID: 15141
		BodyFormat Format { get; }

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06003B26 RID: 15142
		bool IsBodyDefined { get; }

		// Token: 0x06003B27 RID: 15143
		int GetLastNBytesAsString(int lastNBytesToRead, out string readString);

		// Token: 0x06003B28 RID: 15144
		void CopyBodyInjectingText(IBody targetBody, BodyInjectionFormat injectionFormat, string prefixInjectionText, string suffixInjectionText);

		// Token: 0x06003B29 RID: 15145
		Stream OpenWriteStream(BodyWriteConfiguration configuration);
	}
}
