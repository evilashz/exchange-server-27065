using System;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000008 RID: 8
	internal class PSTFxProxy : DisposeTrackableBase, IFxProxy, IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00004551 File Offset: 0x00002751
		public PSTFxProxy(object targetObject)
		{
			this.targetObject = targetObject;
			this.targetObjectData = null;
			this.pstMailbox = ((targetObject is PstFxFolder) ? ((PstFxFolder)targetObject).PstMailbox : ((PSTMessage)targetObject).PstMailbox);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000458D File Offset: 0x0000278D
		byte[] IMapiFxProxy.GetObjectData()
		{
			if (this.targetObjectData == null)
			{
				this.targetObjectData = MapiUtils.CreateObjectData((this.targetObject is PstFxFolder) ? InterfaceIds.IMAPIFolderGuid : InterfaceIds.IMessageGuid);
			}
			return this.targetObjectData;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000045C4 File Offset: 0x000027C4
		void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] data)
		{
			switch (opCode)
			{
			case FxOpcodes.Config:
				this.folderProcessor = FastTransferFolderCopyTo.CreateUploadStateMachine((PstFxFolder)this.targetObject);
				this.uploadContext = new FastTransferUploadContext(Encoding.ASCII, NullResourceTracker.Instance, PropertyFilterFactory.IncludeAllFactory, false);
				this.uploadContext.PushInitial(this.folderProcessor);
				return;
			case FxOpcodes.TransferBuffer:
				try
				{
					this.uploadContext.PutNextBuffer(new ArraySegment<byte>(data));
					return;
				}
				catch (PSTExceptionBase innerException)
				{
					throw new MailboxReplicationPermanentException(new LocalizedString("TransferBuffer"), innerException);
				}
				break;
			case FxOpcodes.IsInterfaceOk:
			case FxOpcodes.TellPartnerVersion:
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000466C File Offset: 0x0000286C
		void IFxProxy.Flush()
		{
			try
			{
				((PstFxFolder)this.targetObject).IPstFolder.Save();
			}
			catch (PSTExceptionBase innerException)
			{
				throw new MailboxReplicationPermanentException(new LocalizedString("TransferBuffer"), innerException);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000046B4 File Offset: 0x000028B4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.uploadContext != null)
			{
				this.folderProcessor.Dispose();
				this.uploadContext.Dispose();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000046D7 File Offset: 0x000028D7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PSTFxProxy>(this);
		}

		// Token: 0x0400001E RID: 30
		private IFastTransferProcessor<FastTransferUploadContext> folderProcessor;

		// Token: 0x0400001F RID: 31
		private FastTransferUploadContext uploadContext;

		// Token: 0x04000020 RID: 32
		private object targetObject;

		// Token: 0x04000021 RID: 33
		private byte[] targetObjectData;

		// Token: 0x04000022 RID: 34
		private PstMailbox pstMailbox;
	}
}
