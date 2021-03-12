using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000009 RID: 9
	internal class PSTFxProxyPool : FxProxyPool<PSTFxProxyPool.FolderEntry, PSTFxProxyPool.MessageEntry>
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000046DF File Offset: 0x000028DF
		public PSTFxProxyPool(PstDestinationMailbox destPst, ICollection<byte[]> folderIds) : base(folderIds)
		{
			this.destPst = destPst;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000046EF File Offset: 0x000028EF
		protected override PSTFxProxyPool.FolderEntry CreateFolder(FolderRec folderRec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000046F8 File Offset: 0x000028F8
		protected override PSTFxProxyPool.FolderEntry OpenFolder(byte[] folderID)
		{
			uint nodeIdFromEntryId = PstMailbox.GetNodeIdFromEntryId(this.destPst.IPst.MessageStore.Guid, folderID);
			IFolder iPstFolder = this.destPst.IPst.ReadFolder(nodeIdFromEntryId);
			return PSTFxProxyPool.FolderEntry.Wrap(this.destPst, iPstFolder);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000473F File Offset: 0x0000293F
		protected override void MailboxSetItemProperties(ItemPropertiesBase props)
		{
			throw new NotImplementedException(string.Format("PSTFxProxyPool.SetItemProperties({0})", props.GetType().Name));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000475B File Offset: 0x0000295B
		protected override byte[] FolderGetObjectData(PSTFxProxyPool.FolderEntry folder)
		{
			return folder.Proxy.GetObjectData();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004768 File Offset: 0x00002968
		protected override void FolderProcessRequest(PSTFxProxyPool.FolderEntry entry, FxOpcodes opcode, byte[] request)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000476F File Offset: 0x0000296F
		protected override void FolderSetProps(PSTFxProxyPool.FolderEntry folder, PropValueData[] pvda)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004776 File Offset: 0x00002976
		protected override void FolderSetItemProperties(PSTFxProxyPool.FolderEntry folder, ItemPropertiesBase props)
		{
			throw new NotImplementedException(string.Format("PSTFxProxyPool.FolderSetItemProperties({0})", props.GetType().Name));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004792 File Offset: 0x00002992
		protected override PSTFxProxyPool.MessageEntry FolderOpenMessage(PSTFxProxyPool.FolderEntry folder, byte[] entryID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004799 File Offset: 0x00002999
		protected override PSTFxProxyPool.MessageEntry FolderCreateMessage(PSTFxProxyPool.FolderEntry folder, bool isAssociated)
		{
			return folder.CreateMessage(isAssociated);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000047A2 File Offset: 0x000029A2
		protected override void FolderDeleteMessage(PSTFxProxyPool.FolderEntry folder, byte[] entryID)
		{
			folder.DeleteMessage(entryID);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000047AB File Offset: 0x000029AB
		protected override byte[] MessageGetObjectData(PSTFxProxyPool.MessageEntry message)
		{
			if (message == null)
			{
				return MapiUtils.CreateObjectData(InterfaceIds.IMessageGuid);
			}
			return message.Proxy.GetObjectData();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000047C6 File Offset: 0x000029C6
		protected override void MessageProcessRequest(PSTFxProxyPool.MessageEntry message, FxOpcodes opcode, byte[] request)
		{
			message.ProcessRequest(opcode, request);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000047D0 File Offset: 0x000029D0
		protected override void MessageSetProps(PSTFxProxyPool.MessageEntry message, PropValueData[] pvda)
		{
			message.SetProps(pvda);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000047D9 File Offset: 0x000029D9
		protected override void MessageSetItemProperties(PSTFxProxyPool.MessageEntry message, ItemPropertiesBase props)
		{
			throw new NotImplementedException(string.Format("PSTFxProxyPool.MessageSetItemProperties({0})", props.GetType().Name));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000047F5 File Offset: 0x000029F5
		protected override byte[] MessageSaveChanges(PSTFxProxyPool.MessageEntry message)
		{
			return message.SaveChanges();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000047FD File Offset: 0x000029FD
		protected override void MessageWriteToMime(PSTFxProxyPool.MessageEntry entry, byte[] buffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000023 RID: 35
		private PstDestinationMailbox destPst;

		// Token: 0x0200000A RID: 10
		internal abstract class PSTEntry : DisposeTrackableBase
		{
			// Token: 0x060000A1 RID: 161 RVA: 0x00004804 File Offset: 0x00002A04
			protected PSTEntry(object entry)
			{
				this.proxy = new PSTFxProxy(entry);
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004818 File Offset: 0x00002A18
			public IMapiFxProxy Proxy
			{
				get
				{
					return this.proxy;
				}
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x00004820 File Offset: 0x00002A20
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<PSTFxProxyPool.PSTEntry>(this);
			}

			// Token: 0x060000A4 RID: 164 RVA: 0x00004828 File Offset: 0x00002A28
			protected override void InternalDispose(bool disposing)
			{
				if (disposing && this.proxy != null)
				{
					this.proxy.Dispose();
					this.proxy = null;
				}
			}

			// Token: 0x04000024 RID: 36
			private PSTFxProxy proxy;
		}

		// Token: 0x0200000B RID: 11
		internal class FolderEntry : PSTFxProxyPool.PSTEntry
		{
			// Token: 0x060000A5 RID: 165 RVA: 0x00004847 File Offset: 0x00002A47
			private FolderEntry(PstFxFolder folder) : base(folder)
			{
				this.folder = folder;
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x00004857 File Offset: 0x00002A57
			public static PSTFxProxyPool.FolderEntry Wrap(PstMailbox pstMailbox, IFolder iPstFolder)
			{
				if (iPstFolder == null)
				{
					return null;
				}
				return new PSTFxProxyPool.FolderEntry(new PstFxFolder(pstMailbox, iPstFolder));
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x0000486C File Offset: 0x00002A6C
			public PSTFxProxyPool.MessageEntry CreateMessage(bool isAssociated)
			{
				IMessage iPstMessage;
				try
				{
					iPstMessage = (isAssociated ? this.folder.IPstFolder.AddAssociatedMessage() : this.folder.IPstFolder.AddMessage());
				}
				catch (PSTExceptionBase innerException)
				{
					throw new UnableToCreatePSTMessagePermanentException(this.folder.PstMailbox.IPst.FileName, innerException);
				}
				return PSTFxProxyPool.MessageEntry.Wrap(this.folder.PstMailbox, iPstMessage);
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x000048E0 File Offset: 0x00002AE0
			public void DeleteMessage(byte[] entryID)
			{
				uint nodeIdFromEntryId = PstMailbox.GetNodeIdFromEntryId(this.folder.PstMailbox.IPst.MessageStore.Guid, entryID);
				try
				{
					IMessage message = this.folder.PstMailbox.IPst.ReadMessage(nodeIdFromEntryId);
					if (message != null)
					{
						this.folder.PstMailbox.IPst.DeleteMessage(nodeIdFromEntryId);
					}
				}
				catch (InternalItemErrorException)
				{
				}
				catch (PSTExceptionBase innerException)
				{
					throw new UnableToReadPSTMessagePermanentException(this.folder.PstMailbox.IPst.FileName, nodeIdFromEntryId, innerException);
				}
			}

			// Token: 0x04000025 RID: 37
			private PstFxFolder folder;
		}

		// Token: 0x0200000C RID: 12
		internal class MessageEntry : PSTFxProxyPool.PSTEntry
		{
			// Token: 0x060000A9 RID: 169 RVA: 0x00004980 File Offset: 0x00002B80
			private MessageEntry(PSTMessage message) : base(message)
			{
				this.message = message;
				this.uploadContext = null;
				this.messageProcessor = null;
				this.message.PropertyBag.SetProperty(new PropertyValue(PSTFxProxyPool.MessageEntry.MsgStatusPropertyTag, 0));
			}

			// Token: 0x060000AA RID: 170 RVA: 0x000049BE File Offset: 0x00002BBE
			public static PSTFxProxyPool.MessageEntry Wrap(PstMailbox pstMailbox, IMessage iPstMessage)
			{
				if (iPstMessage == null)
				{
					return null;
				}
				return new PSTFxProxyPool.MessageEntry(new PSTMessage(pstMailbox, iPstMessage));
			}

			// Token: 0x060000AB RID: 171 RVA: 0x000049D4 File Offset: 0x00002BD4
			public byte[] SaveChanges()
			{
				this.uploadContext.Flush();
				try
				{
					this.message.Save();
				}
				catch (PSTExceptionBase innerException)
				{
					throw new UnableToCreatePSTMessagePermanentException(this.message.PstMailbox.IPst.FileName, innerException);
				}
				return PstMailbox.CreateEntryIdFromNodeId(this.message.PstMailbox.IPst.MessageStore.Guid, this.message.IPstMessage.Id);
			}

			// Token: 0x060000AC RID: 172 RVA: 0x00004A58 File Offset: 0x00002C58
			public void ProcessRequest(FxOpcodes opCode, byte[] request)
			{
				try
				{
					switch (opCode)
					{
					case FxOpcodes.Config:
						this.messageProcessor = new FastTransferMessageCopyTo(false, this.message, true);
						this.uploadContext = new FastTransferUploadContext(Encoding.ASCII, NullResourceTracker.Instance, PropertyFilterFactory.IncludeAllFactory, false);
						this.uploadContext.PushInitial(this.messageProcessor);
						break;
					case FxOpcodes.TransferBuffer:
						this.uploadContext.PutNextBuffer(new ArraySegment<byte>(request));
						break;
					case FxOpcodes.IsInterfaceOk:
					case FxOpcodes.TellPartnerVersion:
						break;
					default:
						throw new NotSupportedException();
					}
				}
				catch (PSTExceptionBase innerException)
				{
					throw new UnableToCreatePSTMessagePermanentException(this.message.PstMailbox.IPst.FileName, innerException);
				}
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00004B0C File Offset: 0x00002D0C
			public void SetProps(PropValueData[] pvda)
			{
				try
				{
					foreach (PropValueData data in pvda)
					{
						this.message.PropertyBag.SetProperty(PstMailbox.MoMTPvFromPv(DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(data)));
					}
				}
				catch (PSTExceptionBase innerException)
				{
					throw new UnableToCreatePSTMessagePermanentException(this.message.PstMailbox.IPst.FileName, innerException);
				}
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00004B78 File Offset: 0x00002D78
			protected override void InternalDispose(bool disposing)
			{
				base.InternalDispose(disposing);
				if (disposing && this.uploadContext != null)
				{
					this.messageProcessor.Dispose();
					this.uploadContext.Dispose();
					this.messageProcessor = null;
					this.uploadContext = null;
				}
			}

			// Token: 0x04000026 RID: 38
			private const int MsgStatusPropertyValue = 0;

			// Token: 0x04000027 RID: 39
			private static readonly PropertyTag MsgStatusPropertyTag = new PropertyTag(236388355U);

			// Token: 0x04000028 RID: 40
			private PSTMessage message;

			// Token: 0x04000029 RID: 41
			private FastTransferUploadContext uploadContext;

			// Token: 0x0400002A RID: 42
			private IFastTransferProcessor<FastTransferUploadContext> messageProcessor;
		}
	}
}
