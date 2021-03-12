using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000129 RID: 297
	internal class FxProxyReceiver : DisposableWrapper<IFxProxy>, IDataImport, IDisposable
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x00014E47 File Offset: 0x00013047
		public FxProxyReceiver(IFxProxy destination, bool ownsDestination) : base(destination, ownsDestination)
		{
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00014E51 File Offset: 0x00013051
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage message)
		{
			if (message is FxProxyGetObjectDataRequestMessage)
			{
				return new FxProxyGetObjectDataResponseMessage(base.WrappedObject.GetObjectData());
			}
			if (message is FlushMessage)
			{
				base.WrappedObject.Flush();
				return null;
			}
			throw new UnexpectedErrorPermanentException(-2147024809);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00014E8C File Offset: 0x0001308C
		void IDataImport.SendMessage(IDataMessage message)
		{
			FxProxyImportBufferMessage fxProxyImportBufferMessage = message as FxProxyImportBufferMessage;
			if (fxProxyImportBufferMessage != null)
			{
				base.WrappedObject.ProcessRequest(fxProxyImportBufferMessage.Opcode, fxProxyImportBufferMessage.Buffer);
				return;
			}
			throw new UnexpectedErrorPermanentException(-2147024809);
		}
	}
}
