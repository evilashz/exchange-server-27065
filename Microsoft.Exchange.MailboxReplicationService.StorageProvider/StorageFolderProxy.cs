using System;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000A RID: 10
	internal class StorageFolderProxy : StorageFxProxy<StorageDestinationFolder>
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00008761 File Offset: 0x00006961
		public StorageFolderProxy(StorageDestinationFolder folder, bool isMoveUser)
		{
			base.IsMoveUser = isMoveUser;
			base.TargetObject = folder;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00008777 File Offset: 0x00006977
		protected override byte[] GetObjectDataImplementation()
		{
			return MapiUtils.MapiFolderObjectData;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00008780 File Offset: 0x00006980
		protected override IFastTransferProcessor<FastTransferUploadContext> GetFxProcessor(uint transferMethod)
		{
			IFastTransferProcessor<FastTransferUploadContext> result;
			if (transferMethod == 1U)
			{
				result = FastTransferFolderCopyTo.CreateUploadStateMachine(base.TargetObject.FxFolder);
			}
			else
			{
				if (transferMethod != 3U)
				{
					throw new FastTransferBufferException("transferMethod", (int)transferMethod);
				}
				result = new FastTransferMessageIterator(new MessageIteratorClient(base.TargetObject.CoreFolder), true);
			}
			return result;
		}
	}
}
