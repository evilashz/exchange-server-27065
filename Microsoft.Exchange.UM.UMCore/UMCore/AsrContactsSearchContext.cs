using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200001D RID: 29
	internal class AsrContactsSearchContext : AsrSearchContext
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00007716 File Offset: 0x00005916
		internal AsrContactsSearchContext(AsrContactsManager manager)
		{
			this.manager = manager;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007725 File Offset: 0x00005925
		internal override bool CanShowExactMatches()
		{
			return this.manager.MatchedNameSelectionMethod != DisambiguationFieldEnum.PromptForAlias || this.manager.CurrentSearchTarget != SearchTarget.GlobalAddressList || this.manager.AuthenticatedCaller;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007753 File Offset: 0x00005953
		internal override void PrepareForNBestPhase2()
		{
			this.manager.PrepareForNBestPhase2();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007760 File Offset: 0x00005960
		internal override void PrepareForPromptForAliasQA(List<IUMRecognitionPhrase> alternates)
		{
			base.ResultsToPlay = new List<List<IUMRecognitionPhrase>>();
			base.ResultsToPlay.Add(alternates);
			this.manager.PrepareForPromptForAliasQA(alternates);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007785 File Offset: 0x00005985
		internal override void PrepareForCollisionQA(List<List<IUMRecognitionPhrase>> alternates)
		{
			base.ResultsToPlay = alternates;
			this.manager.PrepareForCollisionQA(alternates);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000779A File Offset: 0x0000599A
		internal override void PrepareForConfirmViaListQA(List<List<IUMRecognitionPhrase>> alternates)
		{
			base.ResultsToPlay = alternates;
			this.manager.PrepareForConfirmViaListQA(alternates);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000077AF File Offset: 0x000059AF
		internal override bool PrepareForConfirmQA(List<List<IUMRecognitionPhrase>> alternates)
		{
			return this.manager.PrepareForConfirmQA(alternates);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000077BD File Offset: 0x000059BD
		internal override bool PrepareForConfirmAgainQA(List<List<IUMRecognitionPhrase>> alternates)
		{
			base.ResultsToPlay = alternates;
			return this.manager.PrepareForConfirmAgainQA(alternates);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000077D2 File Offset: 0x000059D2
		internal override List<List<IUMRecognitionPhrase>> ProcessMultipleResults(List<List<IUMRecognitionPhrase>> results)
		{
			return this.manager.ProcessMultipleResults(results);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000077E0 File Offset: 0x000059E0
		internal override void OnNameSpoken()
		{
			this.manager.OnNameSpoken();
		}

		// Token: 0x04000075 RID: 117
		private AsrContactsManager manager;
	}
}
