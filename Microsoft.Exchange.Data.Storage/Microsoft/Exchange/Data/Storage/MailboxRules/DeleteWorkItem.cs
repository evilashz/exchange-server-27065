using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BDD RID: 3037
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DeleteWorkItem : WorkItem
	{
		// Token: 0x06006BE3 RID: 27619 RVA: 0x001CE932 File Offset: 0x001CCB32
		public DeleteWorkItem(IRuleEvaluationContext context, int actionIndex) : base(context, actionIndex)
		{
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x06006BE4 RID: 27620 RVA: 0x001CE93C File Offset: 0x001CCB3C
		public override bool ShouldExecuteOnThisStage
		{
			get
			{
				return base.ShouldExecuteOnThisStage || ExecutionStage.OnPublicFolderBefore == base.Context.ExecutionStage;
			}
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x06006BE5 RID: 27621 RVA: 0x001CE956 File Offset: 0x001CCB56
		public override ExecutionStage Stage
		{
			get
			{
				return ExecutionStage.OnPromotedMessage | ExecutionStage.OnPublicFolderBefore;
			}
		}

		// Token: 0x06006BE6 RID: 27622 RVA: 0x001CE95C File Offset: 0x001CCB5C
		public override void Execute()
		{
			if (!this.ShouldExecuteOnThisStage)
			{
				return;
			}
			base.Context.TraceDebug("Delete action: Creating not-read notification.");
			using (MessageItem messageItem = this.CreateNrn())
			{
				base.Context.TraceDebug("Delete action: Preparing not-read notification.");
				base.Context.SetMailboxOwnerAsSender(messageItem);
				base.SetRecipientsResponsibility(messageItem);
				base.Context.TraceDebug("Delete action: Submitting not-read notification.");
				base.SubmitMessage(messageItem);
				base.Context.TraceDebug("Delete action: Not-read notification submitted.");
			}
		}

		// Token: 0x06006BE7 RID: 27623 RVA: 0x001CE9F0 File Offset: 0x001CCBF0
		internal MessageItem CreateNrn()
		{
			return RuleMessageUtils.CreateNotReadNotification(base.Context.Message);
		}
	}
}
