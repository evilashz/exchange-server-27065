using System;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000011 RID: 17
	internal interface IRetrieverAttachment : IDisposable
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000DB RID: 219
		string FileName { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000DC RID: 220
		bool IsInline { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000DD RID: 221
		bool IsImage { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000DE RID: 222
		bool IsSupportedAttachMethod { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DF RID: 223
		long Size { get; }
	}
}
