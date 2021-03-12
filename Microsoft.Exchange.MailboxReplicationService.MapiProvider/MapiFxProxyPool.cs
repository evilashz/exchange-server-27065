using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000C RID: 12
	internal class MapiFxProxyPool : FxProxyPool<MapiFxProxyPool.FolderEntry, MapiFxProxyPool.MessageEntry>
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000083C0 File Offset: 0x000065C0
		public MapiFxProxyPool(MapiDestinationMailbox destMailbox, ICollection<byte[]> folderIds) : base(folderIds)
		{
			this.destMailbox = destMailbox;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000083D0 File Offset: 0x000065D0
		protected override MapiFxProxyPool.FolderEntry CreateFolder(FolderRec folderRec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000083D8 File Offset: 0x000065D8
		protected override MapiFxProxyPool.FolderEntry OpenFolder(byte[] folderID)
		{
			MapiFolder folder = (MapiFolder)this.destMailbox.OpenMapiEntry(folderID, folderID, OpenEntryFlags.Modify | OpenEntryFlags.DontThrowIfEntryIsMissing);
			return MapiFxProxyPool.FolderEntry.Wrap(folder);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008403 File Offset: 0x00006603
		protected override void MailboxSetItemProperties(ItemPropertiesBase props)
		{
			throw new NotImplementedException(string.Format("MapiFxProxyPool.SetItemProperties({0})", props.GetType().Name));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000841F File Offset: 0x0000661F
		protected override byte[] FolderGetObjectData(MapiFxProxyPool.FolderEntry folder)
		{
			return folder.Proxy.GetObjectData();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000842C File Offset: 0x0000662C
		protected override void FolderProcessRequest(MapiFxProxyPool.FolderEntry entry, FxOpcodes opcode, byte[] request)
		{
			entry.Proxy.ProcessRequest(opcode, request);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000843C File Offset: 0x0000663C
		protected override void FolderSetProps(MapiFxProxyPool.FolderEntry folder, PropValueData[] pvda)
		{
			PropValue[] native = DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(pvda);
			folder.WrappedObject.SetProps(native);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000845D File Offset: 0x0000665D
		protected override void FolderSetItemProperties(MapiFxProxyPool.FolderEntry folder, ItemPropertiesBase props)
		{
			throw new NotImplementedException(string.Format("MapiFxProxyPool.FolderSetItemProperties({0})", props.GetType().Name));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000847C File Offset: 0x0000667C
		protected override MapiFxProxyPool.MessageEntry FolderOpenMessage(MapiFxProxyPool.FolderEntry folder, byte[] entryID)
		{
			MapiMessage message = (MapiMessage)folder.WrappedObject.OpenEntry(entryID, OpenEntryFlags.Modify);
			return MapiFxProxyPool.MessageEntry.Wrap(message);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000084A4 File Offset: 0x000066A4
		protected override MapiFxProxyPool.MessageEntry FolderCreateMessage(MapiFxProxyPool.FolderEntry folder, bool isAssociated)
		{
			CreateMessageFlags createMessageFlags = CreateMessageFlags.None;
			if (isAssociated)
			{
				createMessageFlags |= CreateMessageFlags.Associated;
			}
			MapiMessage message = folder.WrappedObject.CreateMessage(createMessageFlags);
			return MapiFxProxyPool.MessageEntry.Wrap(message);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000084D0 File Offset: 0x000066D0
		protected override void FolderDeleteMessage(MapiFxProxyPool.FolderEntry folder, byte[] entryID)
		{
			folder.WrappedObject.DeleteMessages(DeleteMessagesFlags.ForceHardDelete, new byte[][]
			{
				entryID
			});
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000084F8 File Offset: 0x000066F8
		protected override byte[] MessageGetObjectData(MapiFxProxyPool.MessageEntry message)
		{
			if (message != null)
			{
				return message.Proxy.GetObjectData();
			}
			if (this.destMailbox.InTransitStatus != InTransitStatus.NotInTransit)
			{
				return null;
			}
			byte[] inboxFolderEntryId = this.destMailbox.MapiStore.GetInboxFolderEntryId();
			byte[] result;
			using (MapiFxProxyPool.FolderEntry folderEntry = this.OpenFolder(inboxFolderEntryId))
			{
				using (MapiFxProxyPool.MessageEntry messageEntry = this.FolderCreateMessage(folderEntry, false))
				{
					result = this.MessageGetObjectData(messageEntry);
				}
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00008580 File Offset: 0x00006780
		protected override void MessageProcessRequest(MapiFxProxyPool.MessageEntry message, FxOpcodes opcode, byte[] request)
		{
			message.Proxy.ProcessRequest(opcode, request);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00008590 File Offset: 0x00006790
		protected override void MessageSetProps(MapiFxProxyPool.MessageEntry entry, PropValueData[] pvda)
		{
			PropValue[] native = DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(pvda);
			entry.WrappedObject.SetProps(native);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000085B1 File Offset: 0x000067B1
		protected override void MessageSetItemProperties(MapiFxProxyPool.MessageEntry message, ItemPropertiesBase props)
		{
			throw new NotImplementedException(string.Format("MapiFxProxyPool.MessageSetItemProperties({0})", props.GetType().Name));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000085D0 File Offset: 0x000067D0
		protected override byte[] MessageSaveChanges(MapiFxProxyPool.MessageEntry entry)
		{
			entry.WrappedObject.SaveChanges();
			return entry.WrappedObject.GetProp(PropTag.EntryId).GetBytes();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008600 File Offset: 0x00006800
		protected override void MessageWriteToMime(MapiFxProxyPool.MessageEntry entry, byte[] buffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000023 RID: 35
		private MapiDestinationMailbox destMailbox;

		// Token: 0x0200000D RID: 13
		internal abstract class MapiEntry<T> : DisposableWrapper<T> where T : MapiProp
		{
			// Token: 0x060000B3 RID: 179 RVA: 0x00008608 File Offset: 0x00006808
			protected MapiEntry(T entry) : base(entry, true)
			{
				T wrappedObject = base.WrappedObject;
				this.proxy = wrappedObject.GetFxProxyCollector();
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x00008637 File Offset: 0x00006837
			public IMapiFxProxy Proxy
			{
				get
				{
					return this.proxy;
				}
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x0000863F File Offset: 0x0000683F
			protected override void InternalDispose(bool disposing)
			{
				if (disposing && this.proxy != null)
				{
					this.proxy.Dispose();
					this.proxy = null;
				}
				base.InternalDispose(disposing);
			}

			// Token: 0x04000024 RID: 36
			private IMapiFxProxy proxy;
		}

		// Token: 0x0200000E RID: 14
		internal class FolderEntry : MapiFxProxyPool.MapiEntry<MapiFolder>
		{
			// Token: 0x060000B6 RID: 182 RVA: 0x00008665 File Offset: 0x00006865
			protected FolderEntry(MapiFolder folder) : base(folder)
			{
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x0000866E File Offset: 0x0000686E
			public static MapiFxProxyPool.FolderEntry Wrap(MapiFolder folder)
			{
				if (folder != null)
				{
					return new MapiFxProxyPool.FolderEntry(folder);
				}
				return null;
			}
		}

		// Token: 0x0200000F RID: 15
		internal class MessageEntry : MapiFxProxyPool.MapiEntry<MapiMessage>
		{
			// Token: 0x060000B8 RID: 184 RVA: 0x0000867B File Offset: 0x0000687B
			protected MessageEntry(MapiMessage message) : base(message)
			{
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00008684 File Offset: 0x00006884
			public static MapiFxProxyPool.MessageEntry Wrap(MapiMessage message)
			{
				if (message != null)
				{
					return new MapiFxProxyPool.MessageEntry(message);
				}
				return null;
			}
		}
	}
}
