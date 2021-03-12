using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200003D RID: 61
	internal abstract class BaseUMOfflineTranscriber : DisposableBase
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000290 RID: 656
		// (remove) Token: 0x06000291 RID: 657
		internal abstract event EventHandler<BaseUMOfflineTranscriber.TranscribeCompletedEventArgs> TranscribeCompleted;

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000BA70 File Offset: 0x00009C70
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000BA78 File Offset: 0x00009C78
		internal TopNData TopN { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000BA81 File Offset: 0x00009C81
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000BA89 File Offset: 0x00009C89
		protected internal ContactInfo CallerInfo
		{
			protected get
			{
				return this.callerInfo;
			}
			set
			{
				this.callerInfo = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000BA92 File Offset: 0x00009C92
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000BA9A File Offset: 0x00009C9A
		protected internal UMSubscriber TranscriptionUser
		{
			protected get
			{
				return this.transcriptionUser;
			}
			set
			{
				this.transcriptionUser = value;
				this.calleeInfo = new ADContactInfo((IADOrgPerson)value.ADRecipient);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000BAB9 File Offset: 0x00009CB9
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000BAC1 File Offset: 0x00009CC1
		protected internal string CallingLineId
		{
			protected get
			{
				return this.callingLineId;
			}
			set
			{
				this.callingLineId = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000BACA File Offset: 0x00009CCA
		protected ContactInfo CalleeInfo
		{
			get
			{
				return this.calleeInfo;
			}
		}

		// Token: 0x0600029B RID: 667
		internal abstract void TranscribeFile(string audioFilePath);

		// Token: 0x0600029C RID: 668
		internal abstract void CancelTranscription();

		// Token: 0x0600029D RID: 669
		internal abstract List<KeyValuePair<string, int>> FilterWordsInLexion(List<KeyValuePair<string, int>> rawList, int maxNumberToKeep);

		// Token: 0x0600029E RID: 670
		internal abstract string TestHook_GenerateCustomGrammars();

		// Token: 0x040000D1 RID: 209
		private ContactInfo callerInfo;

		// Token: 0x040000D2 RID: 210
		private ContactInfo calleeInfo;

		// Token: 0x040000D3 RID: 211
		private UMSubscriber transcriptionUser;

		// Token: 0x040000D4 RID: 212
		private string callingLineId;

		// Token: 0x0200003E RID: 62
		internal class TranscribeCompletedEventArgs : AsyncCompletedEventArgs
		{
			// Token: 0x0600029F RID: 671 RVA: 0x0000BAD2 File Offset: 0x00009CD2
			internal TranscribeCompletedEventArgs(List<IUMTranscriptionResult> transcriptionResults, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
			{
				this.transcriptionResults = transcriptionResults;
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000BAE5 File Offset: 0x00009CE5
			internal List<IUMTranscriptionResult> TranscriptionResults
			{
				get
				{
					return this.transcriptionResults;
				}
			}

			// Token: 0x040000D6 RID: 214
			private List<IUMTranscriptionResult> transcriptionResults;
		}
	}
}
