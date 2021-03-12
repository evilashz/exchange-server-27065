using System;
using System.IO;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000002 RID: 2
	internal class BufferedReceiver : DisposableWrapper<IDataImport>, IDataImport, IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public BufferedReceiver(IDataImport destination, bool ownsDestination, bool useBuffering, bool useCompression) : base(destination, ownsDestination)
		{
			this.useBuffering = useBuffering;
			this.useCompression = useCompression;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020EC File Offset: 0x000002EC
		void IDataImport.SendMessage(IDataMessage message)
		{
			if (!this.useBuffering)
			{
				base.WrappedObject.SendMessage(message);
				return;
			}
			BufferBatchMessage bufferBatchMessage = message as BufferBatchMessage;
			if (bufferBatchMessage == null)
			{
				base.WrappedObject.SendMessage(message);
				return;
			}
			if (bufferBatchMessage.Buffer != null)
			{
				byte[] buffer;
				if (this.useCompression)
				{
					buffer = CommonUtils.DecompressData(bufferBatchMessage.Buffer);
				}
				else
				{
					buffer = bufferBatchMessage.Buffer;
				}
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						while (memoryStream.Position < memoryStream.Length)
						{
							int opcode = CommonUtils.ReadInt(binaryReader);
							byte[] data = CommonUtils.ReadBlob(binaryReader);
							IDataMessage message2 = DataMessageSerializer.Deserialize(opcode, data, this.useCompression);
							base.WrappedObject.SendMessage(message2);
						}
					}
				}
			}
			if (bufferBatchMessage.FlushAfterImport)
			{
				base.WrappedObject.SendMessageAndWaitForReply(FlushMessage.Instance);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021E8 File Offset: 0x000003E8
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage message)
		{
			return base.WrappedObject.SendMessageAndWaitForReply(message);
		}

		// Token: 0x04000001 RID: 1
		private bool useBuffering;

		// Token: 0x04000002 RID: 2
		private bool useCompression;
	}
}
