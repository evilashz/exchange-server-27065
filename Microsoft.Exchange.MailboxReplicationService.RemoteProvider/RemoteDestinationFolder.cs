using System;
using System.Security.AccessControl;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000006 RID: 6
	internal class RemoteDestinationFolder : RemoteFolder, IDestinationFolder, IFolder, IDisposable
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000029B7 File Offset: 0x00000BB7
		public RemoteDestinationFolder(IMailboxReplicationProxyService mrsProxy, long handle, byte[] folderId, RemoteDestinationMailbox mailbox) : base(mrsProxy, handle, folderId, mailbox)
		{
			this.exportBufferSizeKB = mailbox.ExportBufferSizeKB;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000029D4 File Offset: 0x00000BD4
		IFxProxy IDestinationFolder.GetFxProxy(FastTransferFlags flags)
		{
			byte[] data;
			long handle;
			if (base.ServerVersion[30])
			{
				handle = base.MrsProxy.IDestinationFolder_GetFxProxy2(base.Handle, (int)flags, out data);
			}
			else
			{
				handle = base.MrsProxy.IDestinationFolder_GetFxProxy(base.Handle, out data);
			}
			IDataMessage getDataResponseMsg = FxProxyGetObjectDataResponseMessage.Deserialize(DataMessageOpcode.FxProxyGetObjectDataResponse, data, base.MrsProxyClient.UseCompression);
			BufferedTransmitter destination = new BufferedTransmitter(new RemoteDataImport(base.MrsProxy, handle, getDataResponseMsg), this.exportBufferSizeKB, true, base.MrsProxyClient.UseBuffering, base.MrsProxyClient.UseCompression);
			AsynchronousTransmitter destination2 = new AsynchronousTransmitter(destination, true);
			return new FxProxyTransmitter(destination2, true);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A73 File Offset: 0x00000C73
		bool IDestinationFolder.SetSearchCriteria(RestrictionData restriction, byte[][] entryIds, SearchCriteriaFlags flags)
		{
			return base.MrsProxy.IDestinationFolder_SetSearchCriteria(base.Handle, restriction, entryIds, (int)flags);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A8C File Offset: 0x00000C8C
		PropProblemData[] IDestinationFolder.SetSecurityDescriptor(SecurityProp secProp, RawSecurityDescriptor sd)
		{
			byte[] array;
			if (sd != null)
			{
				array = new byte[sd.BinaryLength];
				sd.GetBinaryForm(array, 0);
			}
			else
			{
				array = null;
			}
			return base.MrsProxy.IDestinationFolder_SetSecurityDescriptor(base.Handle, (int)secProp, array);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002AF4 File Offset: 0x00000CF4
		void IDestinationFolder.SetReadFlagsOnMessages(SetReadFlags flags, byte[][] entryIds)
		{
			CommonUtils.ProcessInBatches<byte[]>(entryIds, 1000, delegate(byte[][] batch)
			{
				this.MrsProxy.IDestinationFolder_SetReadFlagsOnMessages(this.Handle, (int)flags, batch);
			});
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B2C File Offset: 0x00000D2C
		void IDestinationFolder.SetMessageProps(byte[] entryId, PropValueData[] propValues)
		{
			if (!base.ServerVersion[40])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IDestinationFolder_SetMessageProps");
			}
			base.MrsProxy.IDestinationFolder_SetMessageProps(base.Handle, entryId, propValues);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B7C File Offset: 0x00000D7C
		void IDestinationFolder.SetRules(RuleData[] rules)
		{
			if (!base.ServerVersion[8])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IDestinationFolder_SetRules");
			}
			base.MrsProxy.IDestinationFolder_SetRules(base.Handle, rules);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002BCC File Offset: 0x00000DCC
		void IDestinationFolder.SetACL(SecurityProp secProp, PropValueData[][] aclData)
		{
			if (!base.ServerVersion[8])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IDestinationFolder_SetACL");
			}
			base.MrsProxy.IDestinationFolder_SetACL(base.Handle, (int)secProp, aclData);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C1C File Offset: 0x00000E1C
		void IDestinationFolder.SetExtendedAcl(AclFlags aclFlags, PropValueData[][] aclData)
		{
			if (!base.ServerVersion[51])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IDestinationFolder_SetExtendedAcl");
			}
			base.MrsProxy.IDestinationFolder_SetExtendedAcl(base.Handle, (int)aclFlags, aclData);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C6C File Offset: 0x00000E6C
		void IDestinationFolder.Flush()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C6E File Offset: 0x00000E6E
		Guid IDestinationFolder.LinkMailPublicFolder(LinkMailPublicFolderFlags flags, byte[] objectId)
		{
			return base.MrsProxy.IDestinationFolder_LinkMailPublicFolder(base.Handle, flags, objectId);
		}

		// Token: 0x04000008 RID: 8
		private readonly int exportBufferSizeKB;
	}
}
