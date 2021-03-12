using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000050 RID: 80
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISyncEmail : ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600037B RID: 891
		ISyncSourceSession SourceSession { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600037C RID: 892
		bool? IsRead { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600037D RID: 893
		string From { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600037E RID: 894
		string Subject { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600037F RID: 895
		ExDateTime? ReceivedTime { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000380 RID: 896
		string MessageClass { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000381 RID: 897
		Importance? Importance { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000382 RID: 898
		string ConversationTopic { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000383 RID: 899
		string ConversationIndex { get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000384 RID: 900
		Sensitivity? Sensitivity { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000385 RID: 901
		int? Size { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000386 RID: 902
		bool? HasAttachments { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000387 RID: 903
		bool? IsDraft { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000388 RID: 904
		string InternetMessageId { get; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000389 RID: 905
		Stream MimeStream { get; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600038A RID: 906
		SyncMessageResponseType? SyncMessageResponseType { get; }
	}
}
