using System;
using System.Linq;
using System.Text;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200012D RID: 301
	internal static class FxUtils
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x00014FAC File Offset: 0x000131AC
		public static void TransferFxBuffers(FastTransferDownloadContext downloadContext, FxCollectorSerializer collectorSerializer)
		{
			ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[31680]);
			for (int i = downloadContext.GetNextBuffer(buffer); i > 0; i = downloadContext.GetNextBuffer(buffer))
			{
				if (i < buffer.Array.Length)
				{
					byte[] array = new byte[i];
					Array.Copy(buffer.Array, 0, array, 0, i);
					collectorSerializer.TransferBuffer(array);
				}
				else
				{
					collectorSerializer.TransferBuffer(buffer.Array);
				}
				buffer = new ArraySegment<byte>(new byte[31680]);
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00015034 File Offset: 0x00013234
		public static void CopyItem(MessageRec messageRec, IMessage message, IFolderProxy folderProxy, PropTag[] propsToExclude)
		{
			using (IMessageProxy messageProxy = folderProxy.OpenMessage(messageRec.EntryId))
			{
				FxCollectorSerializer fxCollectorSerializer = new FxCollectorSerializer(messageProxy);
				fxCollectorSerializer.Config(0, 1);
				using (FastTransferDownloadContext fastTransferDownloadContext = FastTransferDownloadContext.CreateForDownload(FastTransferSendOption.Unicode | FastTransferSendOption.UseCpId | FastTransferSendOption.ForceUnicode, 1U, Encoding.Default, NullResourceTracker.Instance, new PropertyFilterFactory(false, false, (from ptag in propsToExclude
				select new PropertyTag((uint)ptag)).ToArray<PropertyTag>()), false))
				{
					FastTransferMessageCopyTo fastTransferObject = new FastTransferMessageCopyTo(false, message, true);
					fastTransferDownloadContext.PushInitial(fastTransferObject);
					FxUtils.TransferFxBuffers(fastTransferDownloadContext, fxCollectorSerializer);
					messageProxy.SaveChanges();
				}
			}
		}

		// Token: 0x040005E8 RID: 1512
		private const int OutlookMaxFxBufferSize = 31680;
	}
}
