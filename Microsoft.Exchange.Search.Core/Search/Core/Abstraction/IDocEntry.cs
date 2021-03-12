using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000019 RID: 25
	internal interface IDocEntry : IEquatable<IDocEntry>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000072 RID: 114
		Guid MailboxGuid { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000073 RID: 115
		string RawItemId { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000074 RID: 116
		int DocumentId { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000075 RID: 117
		string EntryId { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000076 RID: 118
		long IndexId { get; }
	}
}
