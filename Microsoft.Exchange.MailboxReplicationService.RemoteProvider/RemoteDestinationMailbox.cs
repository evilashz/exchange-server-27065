using System;
using System.Collections.Generic;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000009 RID: 9
	internal class RemoteDestinationMailbox : RemoteMailbox, IDestinationMailbox, IMailbox, IDisposable
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000045C6 File Offset: 0x000027C6
		public RemoteDestinationMailbox(string serverName, string remoteOrgName, NetworkCredential remoteCred, ProxyControlFlags proxyControlFlags, IEnumerable<MRSProxyCapabilities> requiredCapabilities, bool useHttps, LocalMailboxFlags flags) : base(serverName, remoteOrgName, remoteCred, proxyControlFlags, requiredCapabilities, useHttps, flags)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000045DC File Offset: 0x000027DC
		void IDestinationMailbox.CreateFolder(FolderRec sourceFolder, CreateFolderFlags createFolderFlags, out byte[] newFolderId)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.CreateFolder(\"{0}\")", new object[]
			{
				sourceFolder.FolderName
			});
			base.VerifyMailboxConnection();
			if (!base.ServerVersion[8])
			{
				if (sourceFolder.EntryId == null)
				{
					throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IDestinationMailbox_CreateFolder");
				}
				base.MrsProxy.IDestinationMailbox_CreateFolder(base.Handle, sourceFolder, createFolderFlags.HasFlag(CreateFolderFlags.FailIfExists));
				newFolderId = sourceFolder.EntryId;
				return;
			}
			else
			{
				if (!base.ServerVersion[50])
				{
					base.MrsProxy.IDestinationMailbox_CreateFolder2(base.Handle, sourceFolder, createFolderFlags.HasFlag(CreateFolderFlags.FailIfExists), out newFolderId);
					return;
				}
				if (createFolderFlags.HasFlag(CreateFolderFlags.CreatePublicFolderDumpster) && !base.ServerVersion[49])
				{
					throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IDestinationMailbox_CreateFolder with CanStoreCreatePFDumpster");
				}
				base.MrsProxy.IDestinationMailbox_CreateFolder3(base.Handle, sourceFolder, (int)createFolderFlags, out newFolderId);
				return;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004700 File Offset: 0x00002900
		CreateMailboxResult IDestinationMailbox.CreateMailbox(byte[] mailboxData, MailboxSignatureFlags sourceSignatureFlags)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.CreateMailbox", new object[0]);
			base.VerifyMailboxConnection();
			if (!base.ServerVersion[24])
			{
				return base.MrsProxy.IDestinationMailbox_CreateMailbox(base.Handle, mailboxData);
			}
			return base.MrsProxy.IDestinationMailbox_CreateMailbox2(base.Handle, mailboxData, (int)sourceSignatureFlags);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000475D File Offset: 0x0000295D
		void IDestinationMailbox.ProcessMailboxSignature(byte[] mailboxData)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.ProcessMailboxSignature", new object[0]);
			base.MrsProxy.IDestinationMailbox_ProcessMailboxSignature(base.Handle, mailboxData);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004788 File Offset: 0x00002988
		void IDestinationMailbox.DeleteFolder(FolderRec folderRec)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.DeleteFolder(\"{0}\")", new object[]
			{
				folderRec.FolderName
			});
			base.VerifyMailboxConnection();
			base.MrsProxy.IDestinationMailbox_DeleteFolder(base.Handle, folderRec);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000047D0 File Offset: 0x000029D0
		IDestinationFolder IDestinationMailbox.GetFolder(byte[] entryId)
		{
			base.VerifyMailboxConnection();
			long num = base.MrsProxy.IDestinationMailbox_GetFolder(base.Handle, entryId);
			if (num == 0L)
			{
				return null;
			}
			return new RemoteDestinationFolder(base.MrsProxy, num, entryId, this);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000480C File Offset: 0x00002A0C
		IFxProxy IDestinationMailbox.GetFxProxy()
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.GetFxProxy", new object[0]);
			base.VerifyMailboxConnection();
			byte[] data;
			long handle = base.MrsProxy.IDestinationMailbox_GetFxProxy(base.Handle, out data);
			IDataMessage getDataResponseMsg = FxProxyGetObjectDataResponseMessage.Deserialize(DataMessageOpcode.FxProxyGetObjectDataResponse, data, base.MrsProxyClient.UseCompression);
			BufferedTransmitter destination = new BufferedTransmitter(new RemoteDataImport(base.MrsProxy, handle, getDataResponseMsg), base.ExportBufferSizeKB, true, base.MrsProxyClient.UseBuffering, base.MrsProxyClient.UseCompression);
			AsynchronousTransmitter destination2 = new AsynchronousTransmitter(destination, true);
			return new FxProxyTransmitter(destination2, true);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000048A0 File Offset: 0x00002AA0
		IFxProxyPool IDestinationMailbox.GetFxProxyPool(ICollection<byte[]> folderIds)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.GetFxProxyPool", new object[0]);
			base.VerifyMailboxConnection();
			List<byte[]> list = new List<byte[]>(folderIds);
			byte[] data;
			long handle = base.MrsProxy.IDestinationMailbox_GetFxProxyPool(base.Handle, list.ToArray(), out data);
			IDataMessage getDataResponseMsg = FxProxyPoolGetFolderDataResponseMessage.Deserialize(DataMessageOpcode.FxProxyPoolGetFolderDataResponse, data, base.MrsProxyClient.UseCompression);
			BufferedTransmitter destination = new BufferedTransmitter(new RemoteDataImport(base.MrsProxy, handle, getDataResponseMsg), base.ExportBufferSizeKB, true, base.MrsProxyClient.UseBuffering, base.MrsProxyClient.UseCompression);
			AsynchronousTransmitter destination2 = new AsynchronousTransmitter(destination, true);
			return new FxProxyPoolTransmitter(destination2, true, base.ServerVersion);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004949 File Offset: 0x00002B49
		bool IDestinationMailbox.MailboxExists()
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.MailboxExists", new object[0]);
			base.VerifyMailboxConnection();
			return base.MrsProxy.IDestinationMailbox_MailboxExists(base.Handle);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004977 File Offset: 0x00002B77
		void IDestinationMailbox.MoveFolder(byte[] folderId, byte[] oldParentId, byte[] newParentId)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.MoveFolder", new object[0]);
			base.VerifyMailboxConnection();
			base.MrsProxy.IDestinationMailbox_MoveFolder(base.Handle, folderId, oldParentId, newParentId);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000049A8 File Offset: 0x00002BA8
		PropProblemData[] IDestinationMailbox.SetProps(PropValueData[] pva)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.SetProps", new object[0]);
			base.VerifyMailboxConnection();
			return base.MrsProxy.IDestinationMailbox_SetProps(base.Handle, pva);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000049D8 File Offset: 0x00002BD8
		void IDestinationMailbox.SetMailboxSecurityDescriptor(RawSecurityDescriptor sd)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.SetMailboxSecurityDescriptor", new object[0]);
			base.VerifyMailboxConnection();
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
			base.MrsProxy.IDestinationMailbox_SetMailboxSecurityDescriptor(base.Handle, array);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004A30 File Offset: 0x00002C30
		void IDestinationMailbox.SetUserSecurityDescriptor(RawSecurityDescriptor sd)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.SetUserSecurityDescriptor", new object[0]);
			base.VerifyMailboxConnection();
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
			base.MrsProxy.IDestinationMailbox_SetUserSecurityDescriptor(base.Handle, array);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004A88 File Offset: 0x00002C88
		void IDestinationMailbox.PreFinalSyncDataProcessing(int? sourceMailboxVersion)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.PreFinalSyncDataProcessing({0})", new object[]
			{
				sourceMailboxVersion
			});
			if (!base.ServerVersion[30])
			{
				MrsTracer.ProxyClient.Debug("PreFinalSyncDataProcessing: Downlevel server does not support CopyToWithFlags call, assuming success", new object[0]);
				return;
			}
			base.VerifyMailboxConnection();
			base.MrsProxy.IDestinationMailbox_PreFinalSyncDataProcessing(base.Handle, sourceMailboxVersion);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004AF4 File Offset: 0x00002CF4
		ConstraintCheckResultType IDestinationMailbox.CheckDataGuarantee(DateTime commitTimestamp, out LocalizedString failureReason)
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.CheckDataGuarantee", new object[0]);
			if (!base.ServerVersion[10])
			{
				MrsTracer.ProxyClient.Debug("Downlevel server does not support CheckDataGuarantee call, assuming success", new object[0]);
				failureReason = LocalizedString.Empty;
				return ConstraintCheckResultType.Satisfied;
			}
			base.VerifyMailboxConnection();
			byte[] bytes;
			int result = base.MrsProxy.IDestinationMailbox_CheckDataGuarantee(base.Handle, commitTimestamp, out bytes);
			failureReason = CommonUtils.ByteDeserialize(bytes);
			return (ConstraintCheckResultType)result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004B70 File Offset: 0x00002D70
		void IDestinationMailbox.ForceLogRoll()
		{
			MrsTracer.ProxyClient.Function("RemoteDestinationMailbox.ForceLogRoll", new object[0]);
			if (!base.ServerVersion[14])
			{
				MrsTracer.ProxyClient.Debug("Downlevel server does not support ForceLogRoll call, skipping.", new object[0]);
				return;
			}
			base.VerifyMailboxConnection();
			base.MrsProxy.IDestinationMailbox_ForceLogRoll(base.Handle);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004BD0 File Offset: 0x00002DD0
		List<ReplayAction> IDestinationMailbox.GetActions(string replaySyncState, int maxNumberOfActions)
		{
			MrsTracer.Provider.Function("RemoteDestinationMailbox.GetActions", new object[0]);
			if (!((IMailbox)this).IsMailboxCapabilitySupported(MailboxCapabilities.PagedGetActions))
			{
				MrsTracer.ProxyClient.Debug("Downlevel server does not support GetActions call.", new object[0]);
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IDestinationMailbox_GetActions");
			}
			base.VerifyMailboxConnection();
			return base.MrsProxy.IDestinationMailbox_GetActions(base.Handle, replaySyncState, maxNumberOfActions);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004C50 File Offset: 0x00002E50
		void IDestinationMailbox.SetMailboxSettings(ItemPropertiesBase item)
		{
			MrsTracer.Provider.Function("RemoteDestinationMailbox.SetMailboxSettings", new object[0]);
			if (!base.ServerVersion[59])
			{
				MrsTracer.ProxyClient.Debug("Downlevel server does not support SetMailboxSettings call", new object[0]);
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IDestinationMailbox_SetMailboxSettings");
			}
			base.VerifyMailboxConnection();
			base.MrsProxy.IDestinationMailbox_SetMailboxSettings(base.Handle, item);
		}
	}
}
