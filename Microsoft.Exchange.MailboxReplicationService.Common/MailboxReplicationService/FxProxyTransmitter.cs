using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200012B RID: 299
	internal class FxProxyTransmitter : DisposableWrapper<IDataImport>, IFxProxy, IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x00014EC5 File Offset: 0x000130C5
		public FxProxyTransmitter(IDataImport destination, bool ownsDestination) : base(destination, ownsDestination)
		{
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00014ED0 File Offset: 0x000130D0
		byte[] IMapiFxProxy.GetObjectData()
		{
			IDataMessage dataMessage = base.WrappedObject.SendMessageAndWaitForReply(FxProxyGetObjectDataRequestMessage.Instance);
			FxProxyGetObjectDataResponseMessage fxProxyGetObjectDataResponseMessage = dataMessage as FxProxyGetObjectDataResponseMessage;
			if (fxProxyGetObjectDataResponseMessage == null)
			{
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			return fxProxyGetObjectDataResponseMessage.Buffer;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00014F09 File Offset: 0x00013109
		void IMapiFxProxy.ProcessRequest(FxOpcodes opcode, byte[] request)
		{
			base.WrappedObject.SendMessage(new FxProxyImportBufferMessage(opcode, request));
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00014F1D File Offset: 0x0001311D
		void IFxProxy.Flush()
		{
			base.WrappedObject.SendMessageAndWaitForReply(FlushMessage.Instance);
		}
	}
}
