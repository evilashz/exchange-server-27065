using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000004 RID: 4
	internal class RemoteDataImport : RemoteObject, IDataImport, IDisposable
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000225C File Offset: 0x0000045C
		public RemoteDataImport(IMailboxReplicationProxyService mrsProxy, long handle, IDataMessage getDataResponseMsg) : base(mrsProxy, handle)
		{
			this.getDataResponseMsg = getDataResponseMsg;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002270 File Offset: 0x00000470
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage message)
		{
			if (message is FlushMessage)
			{
				base.MrsProxy.IDataImport_Flush(base.Handle);
				return null;
			}
			if (message is FxProxyGetObjectDataRequestMessage && this.getDataResponseMsg is FxProxyGetObjectDataResponseMessage)
			{
				return this.getDataResponseMsg;
			}
			if (message is FxProxyPoolGetFolderDataRequestMessage && this.getDataResponseMsg is FxProxyPoolGetFolderDataResponseMessage)
			{
				return this.getDataResponseMsg;
			}
			throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IDataImport_GetObjectData");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022F0 File Offset: 0x000004F0
		void IDataImport.SendMessage(IDataMessage message)
		{
			DataMessageOpcode opcode;
			byte[] data;
			message.Serialize(base.MrsProxyClient.UseCompression, out opcode, out data);
			base.MrsProxy.IDataImport_ImportBuffer(base.Handle, (int)opcode, data);
		}

		// Token: 0x04000003 RID: 3
		private IDataMessage getDataResponseMsg;
	}
}
