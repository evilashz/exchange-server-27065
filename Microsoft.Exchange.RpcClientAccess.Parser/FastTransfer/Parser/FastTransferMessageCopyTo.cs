using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000169 RID: 361
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferMessageCopyTo : FastTransferCopyTo
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x00016CA3 File Offset: 0x00014EA3
		public FastTransferMessageCopyTo(bool isShallowCopy, IMessage message, bool isTopLevel) : base(isShallowCopy, message.PropertyBag, isTopLevel)
		{
			Util.ThrowOnNullArgument(message, "message");
			this.message = message;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00016CC5 File Offset: 0x00014EC5
		protected override IPropertyFilter GetDownloadPropertiesFilter(FastTransferDownloadContext context)
		{
			return context.PropertyFilterFactory.GetMessageCopyToFilter(base.IsTopLevel);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00016CD8 File Offset: 0x00014ED8
		protected override IEnumerator<FastTransferStateMachine?> DownloadContents(FastTransferDownloadContext context)
		{
			if (!base.IsShallowCopy)
			{
				IMessagePropertyFilter messageCopyToFilter = context.PropertyFilterFactory.GetMessageCopyToFilter(base.IsTopLevel);
				return FastTransferMessageChange.SerializeRecipientsAndAttachments(context, this.message, messageCopyToFilter.IncludeRecipients, messageCopyToFilter.IncludeAttachments);
			}
			return null;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00016DAC File Offset: 0x00014FAC
		protected override IEnumerator<FastTransferStateMachine?> UploadContents(FastTransferUploadContext context)
		{
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(FastTransferMessageChange.ParseRecipientsAndAttachments(context, this.message)));
			yield break;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00016DCF File Offset: 0x00014FCF
		protected override void InternalDispose()
		{
			this.message.Dispose();
			base.InternalDispose();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00016DE2 File Offset: 0x00014FE2
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferMessageCopyTo>(this);
		}

		// Token: 0x04000381 RID: 897
		private readonly IMessage message;
	}
}
