using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200011E RID: 286
	internal abstract class FxProxyPool<TFolderEntry, TMessageEntry> : DisposeTrackableBase, IFxProxyPool, IDisposable where TFolderEntry : class, IDisposable where TMessageEntry : class, IDisposable
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x000143DD File Offset: 0x000125DD
		public FxProxyPool(ICollection<byte[]> folderIds)
		{
			this.folderIds = folderIds;
			this.uploadedMessageIDs = null;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000143F4 File Offset: 0x000125F4
		IFolderProxy IFxProxyPool.CreateFolder(FolderRec folderRec)
		{
			TFolderEntry tfolderEntry = this.OpenFolder(folderRec.EntryId);
			if (tfolderEntry == null)
			{
				tfolderEntry = this.CreateFolder(folderRec);
			}
			return new FxProxyPool<TFolderEntry, TMessageEntry>.FolderProxy(tfolderEntry, this);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00014428 File Offset: 0x00012628
		IFolderProxy IFxProxyPool.GetFolderProxy(byte[] folderId)
		{
			TFolderEntry tfolderEntry = this.OpenFolder(folderId);
			if (tfolderEntry != null)
			{
				return new FxProxyPool<TFolderEntry, TMessageEntry>.FolderProxy(tfolderEntry, this);
			}
			return null;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00014450 File Offset: 0x00012650
		EntryIdMap<byte[]> IFxProxyPool.GetFolderData()
		{
			EntryIdMap<byte[]> entryIdMap = new EntryIdMap<byte[]>();
			if (this.folderIds != null)
			{
				foreach (byte[] array in this.folderIds)
				{
					using (TFolderEntry tfolderEntry = this.OpenFolder(array))
					{
						entryIdMap[array] = ((tfolderEntry != null) ? this.FolderGetObjectData(tfolderEntry) : null);
					}
				}
			}
			byte[] array2 = this.MessageGetObjectData(default(TMessageEntry));
			if (array2 != null)
			{
				entryIdMap[CommonUtils.MessageData] = array2;
			}
			return entryIdMap;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00014510 File Offset: 0x00012710
		List<byte[]> IFxProxyPool.GetUploadedMessageIDs()
		{
			return this.uploadedMessageIDs;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00014518 File Offset: 0x00012718
		void IFxProxyPool.Flush()
		{
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001451A File Offset: 0x0001271A
		void IFxProxyPool.SetItemProperties(ItemPropertiesBase props)
		{
			this.MailboxSetItemProperties(props);
		}

		// Token: 0x060009F6 RID: 2550
		protected abstract TFolderEntry CreateFolder(FolderRec folderRec);

		// Token: 0x060009F7 RID: 2551
		protected abstract TFolderEntry OpenFolder(byte[] folderID);

		// Token: 0x060009F8 RID: 2552
		protected abstract void MailboxSetItemProperties(ItemPropertiesBase props);

		// Token: 0x060009F9 RID: 2553
		protected abstract byte[] FolderGetObjectData(TFolderEntry folder);

		// Token: 0x060009FA RID: 2554
		protected abstract void FolderProcessRequest(TFolderEntry folder, FxOpcodes opcode, byte[] request);

		// Token: 0x060009FB RID: 2555
		protected abstract void FolderSetProps(TFolderEntry folder, PropValueData[] pvda);

		// Token: 0x060009FC RID: 2556
		protected abstract void FolderSetItemProperties(TFolderEntry folder, ItemPropertiesBase props);

		// Token: 0x060009FD RID: 2557
		protected abstract TMessageEntry FolderOpenMessage(TFolderEntry folder, byte[] entryID);

		// Token: 0x060009FE RID: 2558
		protected abstract TMessageEntry FolderCreateMessage(TFolderEntry folder, bool isAssociated);

		// Token: 0x060009FF RID: 2559
		protected abstract void FolderDeleteMessage(TFolderEntry folder, byte[] entryID);

		// Token: 0x06000A00 RID: 2560
		protected abstract byte[] MessageGetObjectData(TMessageEntry message);

		// Token: 0x06000A01 RID: 2561
		protected abstract void MessageProcessRequest(TMessageEntry message, FxOpcodes opcode, byte[] request);

		// Token: 0x06000A02 RID: 2562
		protected abstract void MessageSetProps(TMessageEntry message, PropValueData[] pvda);

		// Token: 0x06000A03 RID: 2563
		protected abstract void MessageSetItemProperties(TMessageEntry message, ItemPropertiesBase props);

		// Token: 0x06000A04 RID: 2564
		protected abstract byte[] MessageSaveChanges(TMessageEntry message);

		// Token: 0x06000A05 RID: 2565
		protected abstract void MessageWriteToMime(TMessageEntry message, byte[] buffer);

		// Token: 0x06000A06 RID: 2566 RVA: 0x00014523 File Offset: 0x00012723
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FxProxyPool<TFolderEntry, TMessageEntry>>(this);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001452B File Offset: 0x0001272B
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x040005DB RID: 1499
		private ICollection<byte[]> folderIds;

		// Token: 0x040005DC RID: 1500
		private List<byte[]> uploadedMessageIDs;

		// Token: 0x02000121 RID: 289
		private class FolderProxy : DisposableWrapper<TFolderEntry>, IFolderProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000A0D RID: 2573 RVA: 0x0001452D File Offset: 0x0001272D
			public FolderProxy(TFolderEntry folder, FxProxyPool<TFolderEntry, TMessageEntry> owner) : base(folder, true)
			{
				this.owner = owner;
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x0001453E File Offset: 0x0001273E
			byte[] IMapiFxProxy.GetObjectData()
			{
				return this.owner.FolderGetObjectData(base.WrappedObject);
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x00014551 File Offset: 0x00012751
			void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] request)
			{
				this.owner.FolderProcessRequest(base.WrappedObject, opCode, request);
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x00014566 File Offset: 0x00012766
			void IMapiFxProxyEx.SetProps(PropValueData[] pvda)
			{
				this.owner.FolderSetProps(base.WrappedObject, pvda);
			}

			// Token: 0x06000A11 RID: 2577 RVA: 0x0001457A File Offset: 0x0001277A
			void IMapiFxProxyEx.SetItemProperties(ItemPropertiesBase props)
			{
				this.owner.FolderSetItemProperties(base.WrappedObject, props);
			}

			// Token: 0x06000A12 RID: 2578 RVA: 0x00014590 File Offset: 0x00012790
			IMessageProxy IFolderProxy.OpenMessage(byte[] entryId)
			{
				TMessageEntry message = this.owner.FolderOpenMessage(base.WrappedObject, entryId);
				return new FxProxyPool<TFolderEntry, TMessageEntry>.MessageProxy(message, this.owner);
			}

			// Token: 0x06000A13 RID: 2579 RVA: 0x000145BC File Offset: 0x000127BC
			IMessageProxy IFolderProxy.CreateMessage(bool isAssociated)
			{
				TMessageEntry message = this.owner.FolderCreateMessage(base.WrappedObject, isAssociated);
				return new FxProxyPool<TFolderEntry, TMessageEntry>.MessageProxy(message, this.owner);
			}

			// Token: 0x06000A14 RID: 2580 RVA: 0x000145E8 File Offset: 0x000127E8
			void IFolderProxy.DeleteMessage(byte[] entryId)
			{
				this.owner.FolderDeleteMessage(base.WrappedObject, entryId);
			}

			// Token: 0x040005DD RID: 1501
			private FxProxyPool<TFolderEntry, TMessageEntry> owner;
		}

		// Token: 0x02000123 RID: 291
		private class MessageProxy : DisposableWrapper<TMessageEntry>, IMessageProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000A17 RID: 2583 RVA: 0x000145FC File Offset: 0x000127FC
			public MessageProxy(TMessageEntry message, FxProxyPool<TFolderEntry, TMessageEntry> owner) : base(message, true)
			{
				this.owner = owner;
			}

			// Token: 0x06000A18 RID: 2584 RVA: 0x0001460D File Offset: 0x0001280D
			byte[] IMapiFxProxy.GetObjectData()
			{
				return this.owner.MessageGetObjectData(base.WrappedObject);
			}

			// Token: 0x06000A19 RID: 2585 RVA: 0x00014620 File Offset: 0x00012820
			void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] request)
			{
				this.owner.MessageProcessRequest(base.WrappedObject, opCode, request);
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x00014635 File Offset: 0x00012835
			void IMapiFxProxyEx.SetProps(PropValueData[] pvda)
			{
				this.owner.MessageSetProps(base.WrappedObject, pvda);
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x00014649 File Offset: 0x00012849
			void IMapiFxProxyEx.SetItemProperties(ItemPropertiesBase props)
			{
				this.owner.MessageSetItemProperties(base.WrappedObject, props);
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x00014660 File Offset: 0x00012860
			void IMessageProxy.SaveChanges()
			{
				byte[] item = this.owner.MessageSaveChanges(base.WrappedObject);
				if (this.owner.uploadedMessageIDs == null)
				{
					this.owner.uploadedMessageIDs = new List<byte[]>();
				}
				this.owner.uploadedMessageIDs.Add(item);
			}

			// Token: 0x06000A1D RID: 2589 RVA: 0x000146AD File Offset: 0x000128AD
			void IMessageProxy.WriteToMime(byte[] buffer)
			{
				this.owner.MessageWriteToMime(base.WrappedObject, buffer);
			}

			// Token: 0x040005DE RID: 1502
			private FxProxyPool<TFolderEntry, TMessageEntry> owner;
		}
	}
}
