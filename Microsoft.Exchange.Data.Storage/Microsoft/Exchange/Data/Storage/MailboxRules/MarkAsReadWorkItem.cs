using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BE5 RID: 3045
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MarkAsReadWorkItem : WorkItem
	{
		// Token: 0x06006C1E RID: 27678 RVA: 0x001CFB44 File Offset: 0x001CDD44
		public MarkAsReadWorkItem(IRuleEvaluationContext context, int actionIndex) : base(context, actionIndex)
		{
		}

		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x06006C1F RID: 27679 RVA: 0x001CFB4E File Offset: 0x001CDD4E
		public override ExecutionStage Stage
		{
			get
			{
				return ExecutionStage.OnCreatedMessage;
			}
		}

		// Token: 0x06006C20 RID: 27680 RVA: 0x001CFB51 File Offset: 0x001CDD51
		public override void Execute()
		{
			base.Context.TraceDebug("Mark-as-read action: Setting DeliverAsRead");
			base.Context.PropertiesForAllMessageCopies[MessageItemSchema.DeliverAsRead] = true;
		}
	}
}
