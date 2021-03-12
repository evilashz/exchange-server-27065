using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000BA RID: 186
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationAttachment : IDisposable
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000A0C RID: 2572
		ExDateTime LastModifiedTime { get; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000A0D RID: 2573
		long Size { get; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000A0E RID: 2574
		string Id { get; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000A0F RID: 2575
		Stream Stream { get; }

		// Token: 0x06000A10 RID: 2576
		void Save(string contentId);
	}
}
