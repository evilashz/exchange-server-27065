using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BE4 RID: 3044
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxEvaluationResult : IDisposable
	{
		// Token: 0x06006C16 RID: 27670 RVA: 0x001CFA16 File Offset: 0x001CDC16
		public MailboxEvaluationResult(IRuleEvaluationContext context)
		{
			this.context = context;
			this.folderResults = new List<FolderEvaluationResult>();
			this.targetFolder = this.context.CurrentFolder;
		}

		// Token: 0x17001D5B RID: 7515
		// (get) Token: 0x06006C17 RID: 27671 RVA: 0x001CFA44 File Offset: 0x001CDC44
		public RuleAction.Bounce.BounceCode? BounceCode
		{
			get
			{
				if (this.folderResults.Count > 0)
				{
					return this.folderResults[this.folderResults.Count - 1].BounceCode;
				}
				return null;
			}
		}

		// Token: 0x17001D5C RID: 7516
		// (get) Token: 0x06006C18 RID: 27672 RVA: 0x001CFA86 File Offset: 0x001CDC86
		public IRuleEvaluationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17001D5D RID: 7517
		// (get) Token: 0x06006C19 RID: 27673 RVA: 0x001CFA8E File Offset: 0x001CDC8E
		public Folder TargetFolder
		{
			get
			{
				return this.targetFolder;
			}
		}

		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x06006C1A RID: 27674 RVA: 0x001CFA96 File Offset: 0x001CDC96
		internal IList<FolderEvaluationResult> FolderResults
		{
			get
			{
				return new ReadOnlyCollection<FolderEvaluationResult>(this.folderResults);
			}
		}

		// Token: 0x06006C1B RID: 27675 RVA: 0x001CFAA3 File Offset: 0x001CDCA3
		public void AddFolderResult(FolderEvaluationResult folderResult)
		{
			this.folderResults.Add(folderResult);
			this.targetFolder = folderResult.TargetFolder;
		}

		// Token: 0x06006C1C RID: 27676 RVA: 0x001CFABD File Offset: 0x001CDCBD
		public void Dispose()
		{
			if (this.context != null)
			{
				this.context.Dispose();
			}
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x001CFAD4 File Offset: 0x001CDCD4
		public void Execute(ExecutionStage stage)
		{
			this.context.DeliveryFolder = this.TargetFolder;
			this.context.ExecutionStage = stage;
			foreach (FolderEvaluationResult folderEvaluationResult in this.folderResults)
			{
				folderEvaluationResult.Execute(stage);
			}
		}

		// Token: 0x04003DD4 RID: 15828
		private IRuleEvaluationContext context;

		// Token: 0x04003DD5 RID: 15829
		private List<FolderEvaluationResult> folderResults;

		// Token: 0x04003DD6 RID: 15830
		private Folder targetFolder;
	}
}
