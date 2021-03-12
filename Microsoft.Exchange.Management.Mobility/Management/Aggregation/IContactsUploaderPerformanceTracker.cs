using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IContactsUploaderPerformanceTracker
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000CE RID: 206
		// (set) Token: 0x060000CD RID: 205
		int ReceivedContactsCount { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CF RID: 207
		// (set) Token: 0x060000D0 RID: 208
		double ExportedDataSize { get; set; }

		// Token: 0x060000D1 RID: 209
		void Start();

		// Token: 0x060000D2 RID: 210
		void Stop();

		// Token: 0x060000D3 RID: 211
		void IncrementContactsRead();

		// Token: 0x060000D4 RID: 212
		void IncrementContactsExported();

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D5 RID: 213
		// (set) Token: 0x060000D6 RID: 214
		string OperationResult { get; set; }

		// Token: 0x060000D7 RID: 215
		void AddTimeBookmark(ContactsUploaderPerformanceTrackerBookmarks activity);
	}
}
