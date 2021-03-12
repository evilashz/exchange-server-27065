using System;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000F RID: 15
	internal class StorageMessageProxy : StorageFxProxy<MessageAdaptor>
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00008E3E File Offset: 0x0000703E
		public StorageMessageProxy(MessageAdaptor message, bool isMoveUser)
		{
			base.IsMoveUser = isMoveUser;
			base.TargetObject = message;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00008E54 File Offset: 0x00007054
		protected override byte[] GetObjectDataImplementation()
		{
			return StorageMessageProxy.ObjectData;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00008E5C File Offset: 0x0000705C
		protected override IFastTransferProcessor<FastTransferUploadContext> GetFxProcessor(uint transferMethod)
		{
			if (transferMethod == 1U)
			{
				return new FastTransferMessageCopyTo(false, base.TargetObject, true);
			}
			throw new FastTransferBufferException("transferMethod", (int)transferMethod);
		}

		// Token: 0x04000023 RID: 35
		public static readonly byte[] ObjectData = StorageFxProxy<MessageAdaptor>.CreateObjectData(InterfaceIds.IMessageGuid);
	}
}
