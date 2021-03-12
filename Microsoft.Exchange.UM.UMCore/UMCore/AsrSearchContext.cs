using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200001C RID: 28
	internal abstract class AsrSearchContext
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000076EC File Offset: 0x000058EC
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000076F4 File Offset: 0x000058F4
		internal List<List<IUMRecognitionPhrase>> RejectedResults
		{
			get
			{
				return this.rejectedResults;
			}
			set
			{
				this.rejectedResults = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000076FD File Offset: 0x000058FD
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00007705 File Offset: 0x00005905
		internal List<List<IUMRecognitionPhrase>> ResultsToPlay
		{
			get
			{
				return this.resultsToPlay;
			}
			set
			{
				this.resultsToPlay = value;
			}
		}

		// Token: 0x06000175 RID: 373
		internal abstract bool CanShowExactMatches();

		// Token: 0x06000176 RID: 374
		internal abstract void PrepareForNBestPhase2();

		// Token: 0x06000177 RID: 375
		internal abstract void PrepareForPromptForAliasQA(List<IUMRecognitionPhrase> alternates);

		// Token: 0x06000178 RID: 376
		internal abstract void PrepareForCollisionQA(List<List<IUMRecognitionPhrase>> alternates);

		// Token: 0x06000179 RID: 377
		internal abstract void PrepareForConfirmViaListQA(List<List<IUMRecognitionPhrase>> results);

		// Token: 0x0600017A RID: 378
		internal abstract bool PrepareForConfirmQA(List<List<IUMRecognitionPhrase>> results);

		// Token: 0x0600017B RID: 379
		internal abstract bool PrepareForConfirmAgainQA(List<List<IUMRecognitionPhrase>> results);

		// Token: 0x0600017C RID: 380
		internal abstract List<List<IUMRecognitionPhrase>> ProcessMultipleResults(List<List<IUMRecognitionPhrase>> results);

		// Token: 0x0600017D RID: 381
		internal abstract void OnNameSpoken();

		// Token: 0x04000073 RID: 115
		private List<List<IUMRecognitionPhrase>> resultsToPlay;

		// Token: 0x04000074 RID: 116
		private List<List<IUMRecognitionPhrase>> rejectedResults;
	}
}
