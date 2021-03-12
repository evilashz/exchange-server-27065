using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200015A RID: 346
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class FastTransferCopyFolder
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x000127A8 File Offset: 0x000109A8
		internal static IFastTransferProcessor<FastTransferDownloadContext> CreateDownloadStateMachine(IFolder folder, FastTransferFolderContentBase.IncludeSubObject includeSubObject)
		{
			IFastTransferProcessor<FastTransferDownloadContext> result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				FastTransferFolderContent fastTransferFolderContent = new FastTransferFolderContent(folder, includeSubObject, true);
				disposeGuard.Add<FastTransferFolderContent>(fastTransferFolderContent);
				FastTransferDownloadDelimitedObject fastTransferDownloadDelimitedObject = new FastTransferDownloadDelimitedObject(fastTransferFolderContent, PropertyTag.StartTopFld, PropertyTag.EndFolder);
				disposeGuard.Add<FastTransferDownloadDelimitedObject>(fastTransferDownloadDelimitedObject);
				disposeGuard.Success();
				result = fastTransferDownloadDelimitedObject;
			}
			return result;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00012814 File Offset: 0x00010A14
		internal static IFastTransferProcessor<FastTransferUploadContext> CreateUploadStateMachine(IFolder folder)
		{
			IFastTransferProcessor<FastTransferUploadContext> result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				FastTransferFolderContent fastTransferFolderContent = new FastTransferFolderContent(folder, FastTransferFolderContentBase.IncludeSubObject.All, true);
				disposeGuard.Add<FastTransferFolderContent>(fastTransferFolderContent);
				FastTransferUploadDelimitedObject fastTransferUploadDelimitedObject = new FastTransferUploadDelimitedObject(fastTransferFolderContent, PropertyTag.StartTopFld, PropertyTag.EndFolder);
				disposeGuard.Add<FastTransferUploadDelimitedObject>(fastTransferUploadDelimitedObject);
				FastTransferSkipDnPrefix fastTransferSkipDnPrefix = new FastTransferSkipDnPrefix(fastTransferUploadDelimitedObject);
				disposeGuard.Add<FastTransferSkipDnPrefix>(fastTransferSkipDnPrefix);
				disposeGuard.Success();
				result = fastTransferSkipDnPrefix;
			}
			return result;
		}
	}
}
