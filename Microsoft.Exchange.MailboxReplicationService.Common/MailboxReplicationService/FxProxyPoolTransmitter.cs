using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000125 RID: 293
	internal class FxProxyPoolTransmitter : DisposableWrapper<IDataImport>, IFxProxyPool, IDisposable
	{
		// Token: 0x06000A26 RID: 2598 RVA: 0x000149EC File Offset: 0x00012BEC
		public FxProxyPoolTransmitter(IDataImport destination, bool ownsDestination, VersionInformation destinationCapabilities) : base(destination, ownsDestination)
		{
			this.currentEntries = new Stack<FxProxyPoolTransmitter.EntryWrapper>();
			this.pendingOperations = new Queue<IDataMessage>();
			this.destinationCapabilities = destinationCapabilities;
			this.folderDataMap = null;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00014A1A File Offset: 0x00012C1A
		private FxProxyPoolTransmitter.EntryWrapper CurrentEntry
		{
			get
			{
				if (this.currentEntries.Count <= 0)
				{
					return null;
				}
				return this.currentEntries.Peek();
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00014A38 File Offset: 0x00012C38
		IFolderProxy IFxProxyPool.CreateFolder(FolderRec folder)
		{
			this.EnsureFolderDataCached();
			this.FlushBufferedOperations();
			if (this.folderDataMap == null)
			{
				this.folderDataMap = new EntryIdMap<byte[]>();
			}
			if (!this.folderDataMap.ContainsKey(folder.EntryId))
			{
				this.folderDataMap.Add(folder.EntryId, MapiUtils.MapiFolderObjectData);
			}
			return new FxProxyPoolTransmitter.FolderWrapper(folder, this);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00014A94 File Offset: 0x00012C94
		IFolderProxy IFxProxyPool.GetFolderProxy(byte[] folderId)
		{
			this.EnsureFolderDataCached();
			this.FlushBufferedOperations();
			if (this.folderDataMap.ContainsKey(folderId))
			{
				return new FxProxyPoolTransmitter.FolderWrapper(folderId, this);
			}
			return null;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00014AB9 File Offset: 0x00012CB9
		EntryIdMap<byte[]> IFxProxyPool.GetFolderData()
		{
			this.EnsureFolderDataCached();
			this.FlushBufferedOperations();
			return this.folderDataMap;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00014ACD File Offset: 0x00012CCD
		void IFxProxyPool.Flush()
		{
			this.EnsureFolderDataCached();
			this.FlushBufferedOperations();
			base.WrappedObject.SendMessageAndWaitForReply(FlushMessage.Instance);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00014AEC File Offset: 0x00012CEC
		List<byte[]> IFxProxyPool.GetUploadedMessageIDs()
		{
			this.EnsureFolderDataCached();
			this.FlushBufferedOperations();
			IDataMessage dataMessage = base.WrappedObject.SendMessageAndWaitForReply(FxProxyPoolGetUploadedIDsRequestMessage.Instance);
			FxProxyPoolGetUploadedIDsResponseMessage fxProxyPoolGetUploadedIDsResponseMessage = dataMessage as FxProxyPoolGetUploadedIDsResponseMessage;
			if (fxProxyPoolGetUploadedIDsResponseMessage == null)
			{
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			return fxProxyPoolGetUploadedIDsResponseMessage.EntryIDs;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00014B31 File Offset: 0x00012D31
		void IFxProxyPool.SetItemProperties(ItemPropertiesBase props)
		{
			this.EnsureFolderDataCached();
			this.FlushBufferedOperations();
			this.BufferOrSendMessage(new FxProxyPoolSetItemPropertiesMessage(props));
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00014B4B File Offset: 0x00012D4B
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				while (this.currentEntries.Count > 0)
				{
					this.currentEntries.Pop().Dispose();
				}
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00014B78 File Offset: 0x00012D78
		private void EnsureFolderDataCached()
		{
			if (this.folderDataMap != null)
			{
				return;
			}
			IDataMessage dataMessage = base.WrappedObject.SendMessageAndWaitForReply(FxProxyPoolGetFolderDataRequestMessage.Instance);
			FxProxyPoolGetFolderDataResponseMessage fxProxyPoolGetFolderDataResponseMessage = dataMessage as FxProxyPoolGetFolderDataResponseMessage;
			if (fxProxyPoolGetFolderDataResponseMessage == null)
			{
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			this.folderDataMap = fxProxyPoolGetFolderDataResponseMessage.FolderData;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00014BC0 File Offset: 0x00012DC0
		private void FlushBufferedOperations()
		{
			while (this.pendingOperations.Count > 0)
			{
				IDataMessage message = this.pendingOperations.Dequeue();
				base.WrappedObject.SendMessage(message);
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00014BF8 File Offset: 0x00012DF8
		private void BufferOrSendMessage(IDataMessage msg)
		{
			if (msg is FxProxyPoolCloseEntryMessage || msg is FxProxyPoolOpenFolderMessage || msg is FxProxyPoolCreateItemMessage || msg is FxProxyPoolOpenItemMessage)
			{
				this.pendingOperations.Enqueue(msg);
				return;
			}
			this.FlushBufferedOperations();
			base.WrappedObject.SendMessage(msg);
		}

		// Token: 0x040005E1 RID: 1505
		private EntryIdMap<byte[]> folderDataMap;

		// Token: 0x040005E2 RID: 1506
		private Stack<FxProxyPoolTransmitter.EntryWrapper> currentEntries;

		// Token: 0x040005E3 RID: 1507
		private Queue<IDataMessage> pendingOperations;

		// Token: 0x040005E4 RID: 1508
		private VersionInformation destinationCapabilities;

		// Token: 0x02000126 RID: 294
		private abstract class EntryWrapper : DisposeTrackableBase, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000A32 RID: 2610 RVA: 0x00014C44 File Offset: 0x00012E44
			public EntryWrapper(FxProxyPoolTransmitter pool)
			{
				this.pool = pool;
				this.Pool.currentEntries.Push(this);
			}

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06000A33 RID: 2611
			protected abstract byte[] ObjectData { get; }

			// Token: 0x17000325 RID: 805
			// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00014C64 File Offset: 0x00012E64
			protected FxProxyPoolTransmitter Pool
			{
				get
				{
					return this.pool;
				}
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x00014C6C File Offset: 0x00012E6C
			byte[] IMapiFxProxy.GetObjectData()
			{
				return this.ObjectData;
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x00014C74 File Offset: 0x00012E74
			void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] request)
			{
				this.Pool.BufferOrSendMessage(new FxProxyImportBufferMessage(opCode, request));
			}

			// Token: 0x06000A37 RID: 2615 RVA: 0x00014C88 File Offset: 0x00012E88
			void IMapiFxProxyEx.SetProps(PropValueData[] pvda)
			{
				this.Pool.BufferOrSendMessage(new FxProxyPoolSetPropsMessage(pvda));
			}

			// Token: 0x06000A38 RID: 2616 RVA: 0x00014C9C File Offset: 0x00012E9C
			void IMapiFxProxyEx.SetItemProperties(ItemPropertiesBase props)
			{
				if (this.Pool.destinationCapabilities[56])
				{
					this.Pool.BufferOrSendMessage(new FxProxyPoolSetItemPropertiesMessage(props));
					return;
				}
				FolderAcl folderAcl = props as FolderAcl;
				if (folderAcl != null)
				{
					this.Pool.BufferOrSendMessage(new FxProxyPoolSetExtendedAclMessage(folderAcl.Flags, folderAcl.Value));
					return;
				}
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(this.Pool.destinationCapabilities.ComputerName, this.Pool.destinationCapabilities.ToString(), "IFxProxyPool.SetItemProperties");
			}

			// Token: 0x06000A39 RID: 2617 RVA: 0x00014D20 File Offset: 0x00012F20
			protected override void InternalDispose(bool disposing)
			{
				this.Pool.BufferOrSendMessage(FxProxyPoolCloseEntryMessage.Instance);
				this.Pool.currentEntries.Pop();
			}

			// Token: 0x06000A3A RID: 2618 RVA: 0x00014D43 File Offset: 0x00012F43
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<FxProxyPoolTransmitter.EntryWrapper>(this);
			}

			// Token: 0x040005E5 RID: 1509
			private FxProxyPoolTransmitter pool;
		}

		// Token: 0x02000127 RID: 295
		private class FolderWrapper : FxProxyPoolTransmitter.EntryWrapper, IFolderProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000A3B RID: 2619 RVA: 0x00014D4B File Offset: 0x00012F4B
			public FolderWrapper(FolderRec folderRec, FxProxyPoolTransmitter pool) : base(pool)
			{
				this.entryId = folderRec.EntryId;
				base.Pool.BufferOrSendMessage(new FxProxyPoolCreateFolderMessage(folderRec));
			}

			// Token: 0x06000A3C RID: 2620 RVA: 0x00014D71 File Offset: 0x00012F71
			public FolderWrapper(byte[] entryId, FxProxyPoolTransmitter pool) : base(pool)
			{
				this.entryId = entryId;
				base.Pool.BufferOrSendMessage(new FxProxyPoolOpenFolderMessage(entryId));
			}

			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00014D92 File Offset: 0x00012F92
			protected override byte[] ObjectData
			{
				get
				{
					return base.Pool.folderDataMap[this.entryId];
				}
			}

			// Token: 0x06000A3E RID: 2622 RVA: 0x00014DAA File Offset: 0x00012FAA
			IMessageProxy IFolderProxy.OpenMessage(byte[] entryId)
			{
				return new FxProxyPoolTransmitter.MessageWrapper(new FxProxyPoolOpenItemMessage(entryId), base.Pool);
			}

			// Token: 0x06000A3F RID: 2623 RVA: 0x00014DBD File Offset: 0x00012FBD
			IMessageProxy IFolderProxy.CreateMessage(bool isAssociated)
			{
				if (isAssociated)
				{
					return new FxProxyPoolTransmitter.MessageWrapper(FxProxyPoolCreateItemMessage.FAI, base.Pool);
				}
				return new FxProxyPoolTransmitter.MessageWrapper(FxProxyPoolCreateItemMessage.Regular, base.Pool);
			}

			// Token: 0x06000A40 RID: 2624 RVA: 0x00014DE3 File Offset: 0x00012FE3
			void IFolderProxy.DeleteMessage(byte[] entryId)
			{
				base.Pool.BufferOrSendMessage(new FxProxyPoolDeleteItemMessage(entryId));
			}

			// Token: 0x040005E6 RID: 1510
			private byte[] entryId;
		}

		// Token: 0x02000128 RID: 296
		private class MessageWrapper : FxProxyPoolTransmitter.EntryWrapper, IMessageProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000A41 RID: 2625 RVA: 0x00014DF6 File Offset: 0x00012FF6
			public MessageWrapper(IDataMessage openMessage, FxProxyPoolTransmitter pool) : base(pool)
			{
				base.Pool.BufferOrSendMessage(openMessage);
			}

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00014E0B File Offset: 0x0001300B
			protected override byte[] ObjectData
			{
				get
				{
					return base.Pool.folderDataMap[CommonUtils.MessageData];
				}
			}

			// Token: 0x06000A43 RID: 2627 RVA: 0x00014E22 File Offset: 0x00013022
			void IMessageProxy.SaveChanges()
			{
				base.Pool.BufferOrSendMessage(FxProxyPoolSaveChangesMessage.Instance);
			}

			// Token: 0x06000A44 RID: 2628 RVA: 0x00014E34 File Offset: 0x00013034
			void IMessageProxy.WriteToMime(byte[] buffer)
			{
				base.Pool.BufferOrSendMessage(new FxProxyPoolWriteToMimeMessage(buffer));
			}
		}
	}
}
