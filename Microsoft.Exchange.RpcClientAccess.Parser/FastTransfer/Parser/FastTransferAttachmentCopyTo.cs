using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000156 RID: 342
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class FastTransferAttachmentCopyTo
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x00011F60 File Offset: 0x00010160
		internal static IFastTransferProcessor<FastTransferDownloadContext> CreateDownloadStateMachine(IAttachment attachment)
		{
			return new FastTransferAttachmentContent(attachment, true);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00011F6C File Offset: 0x0001016C
		internal static IFastTransferProcessor<FastTransferUploadContext> CreateUploadStateMachine(IAttachment attachment)
		{
			IFastTransferProcessor<FastTransferUploadContext> result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				FastTransferAttachmentContent fastTransferAttachmentContent = new FastTransferAttachmentContent(attachment, true);
				disposeGuard.Add<FastTransferAttachmentContent>(fastTransferAttachmentContent);
				FastTransferSkipDnPrefix fastTransferSkipDnPrefix = new FastTransferSkipDnPrefix(fastTransferAttachmentContent);
				disposeGuard.Add<FastTransferSkipDnPrefix>(fastTransferSkipDnPrefix);
				disposeGuard.Success();
				result = fastTransferSkipDnPrefix;
			}
			return result;
		}
	}
}
