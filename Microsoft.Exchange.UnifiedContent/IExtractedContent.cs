using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x02000006 RID: 6
	internal interface IExtractedContent
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35
		string FileName { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36
		TextExtractionStatus TextExtractionStatus { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37
		int RefId { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38
		Dictionary<string, object> Properties { get; }

		// Token: 0x06000027 RID: 39
		IList<IExtractedContent> GetChildren();

		// Token: 0x06000028 RID: 40
		Stream GetContentReadStream();

		// Token: 0x06000029 RID: 41
		bool IsModified(Stream rawStream);

		// Token: 0x0600002A RID: 42
		bool IsModified(uint hash);
	}
}
