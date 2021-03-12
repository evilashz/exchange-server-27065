using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000164 RID: 356
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class FastTransferFolderCopyTo
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x00014A93 File Offset: 0x00012C93
		internal static IFastTransferProcessor<FastTransferDownloadContext> CreateDownloadStateMachine(IFolder folder, FastTransferFolderContentBase.IncludeSubObject includeSubObject)
		{
			return new FastTransferFolderContentWithDelProp(folder, includeSubObject);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00014A9C File Offset: 0x00012C9C
		internal static IFastTransferProcessor<FastTransferUploadContext> CreateUploadStateMachine(IFolder folder)
		{
			IFastTransferProcessor<FastTransferUploadContext> result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				FastTransferFolderContentWithDelProp fastTransferFolderContentWithDelProp = new FastTransferFolderContentWithDelProp(folder);
				disposeGuard.Add<FastTransferFolderContentWithDelProp>(fastTransferFolderContentWithDelProp);
				FastTransferSkipDnPrefix fastTransferSkipDnPrefix = new FastTransferSkipDnPrefix(fastTransferFolderContentWithDelProp);
				disposeGuard.Add<FastTransferSkipDnPrefix>(fastTransferSkipDnPrefix);
				disposeGuard.Success();
				result = fastTransferSkipDnPrefix;
			}
			return result;
		}
	}
}
