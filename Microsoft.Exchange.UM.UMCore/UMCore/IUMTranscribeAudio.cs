using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C7 RID: 711
	internal interface IUMTranscribeAudio
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600158E RID: 5518
		UMSubscriber TranscriptionUser { get; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600158F RID: 5519
		string AttachmentPath { get; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001590 RID: 5520
		TimeSpan Duration { get; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001591 RID: 5521
		// (set) Token: 0x06001592 RID: 5522
		ITranscriptionData TranscriptionData { get; set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001593 RID: 5523
		bool EnableTopNGrammar { get; }
	}
}
