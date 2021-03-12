using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000B RID: 11
	internal class StorageFxProxyPool : FxProxyPool<StorageFxProxyPool.FolderEntry, StorageFxProxyPool.MessageEntry>
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000087CE File Offset: 0x000069CE
		public StorageFxProxyPool(StorageDestinationMailbox destMailbox, ICollection<byte[]> folderIds) : base(folderIds)
		{
			this.destMailbox = destMailbox;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000087E0 File Offset: 0x000069E0
		protected override StorageFxProxyPool.FolderEntry CreateFolder(FolderRec folderRec)
		{
			IDestinationMailbox destinationMailbox = this.destMailbox;
			byte[] folderID;
			destinationMailbox.CreateFolder(folderRec, CreateFolderFlags.None, out folderID);
			return this.OpenFolder(folderID);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00008808 File Offset: 0x00006A08
		protected override StorageFxProxyPool.FolderEntry OpenFolder(byte[] folderId)
		{
			StorageDestinationFolder folder = this.destMailbox.GetFolder<StorageDestinationFolder>(folderId);
			if (folder == null)
			{
				return null;
			}
			return StorageFxProxyPool.FolderEntry.Wrap(folder, this.destMailbox.Flags.HasFlag(LocalMailboxFlags.Move));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000884C File Offset: 0x00006A4C
		protected override void MailboxSetItemProperties(ItemPropertiesBase props)
		{
			if (props != null)
			{
				props.Apply(this.destMailbox.PSHandler, (MailboxSession)this.destMailbox.StoreSession);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008872 File Offset: 0x00006A72
		protected override byte[] FolderGetObjectData(StorageFxProxyPool.FolderEntry folder)
		{
			return MapiUtils.MapiFolderObjectData;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008879 File Offset: 0x00006A79
		protected override void FolderProcessRequest(StorageFxProxyPool.FolderEntry entry, FxOpcodes opcode, byte[] request)
		{
			entry.Proxy.ProcessRequest(opcode, request);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00008888 File Offset: 0x00006A88
		protected override void FolderSetProps(StorageFxProxyPool.FolderEntry folder, PropValueData[] pvda)
		{
			this.SetProps(folder.WrappedObject.FxFolder.PropertyBag, pvda);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000088A1 File Offset: 0x00006AA1
		protected override void FolderSetItemProperties(StorageFxProxyPool.FolderEntry folder, ItemPropertiesBase props)
		{
			if (props != null)
			{
				props.Apply(folder.WrappedObject.CoreFolder);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000088B7 File Offset: 0x00006AB7
		protected override StorageFxProxyPool.MessageEntry FolderOpenMessage(StorageFxProxyPool.FolderEntry folder, byte[] entryID)
		{
			throw new NotImplementedException("FolderOpenMessage");
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000088C4 File Offset: 0x00006AC4
		protected override StorageFxProxyPool.MessageEntry FolderCreateMessage(StorageFxProxyPool.FolderEntry folder, bool isAssociated)
		{
			IMessage message = folder.WrappedObject.FxFolder.CreateMessage(isAssociated);
			return StorageFxProxyPool.MessageEntry.Wrap(message as MessageAdaptor, this.destMailbox.Flags.HasFlag(LocalMailboxFlags.Move));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008910 File Offset: 0x00006B10
		protected override void FolderDeleteMessage(StorageFxProxyPool.FolderEntry folder, byte[] entryID)
		{
			folder.WrappedObject.CoreFolder.DeleteItems(DeleteItemFlags.HardDelete, new StoreObjectId[]
			{
				StoreObjectId.FromProviderSpecificId(entryID)
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00008940 File Offset: 0x00006B40
		protected override byte[] MessageGetObjectData(StorageFxProxyPool.MessageEntry message)
		{
			return StorageMessageProxy.ObjectData;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00008947 File Offset: 0x00006B47
		protected override void MessageProcessRequest(StorageFxProxyPool.MessageEntry message, FxOpcodes opcode, byte[] request)
		{
			message.Proxy.ProcessRequest(opcode, request);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00008956 File Offset: 0x00006B56
		protected override void MessageSetProps(StorageFxProxyPool.MessageEntry entry, PropValueData[] pvda)
		{
			if (entry.MimeStream != null || entry.CachedItemProperties.Count > 0)
			{
				entry.CachedPropValues.AddRange(pvda);
				return;
			}
			this.SetProps(entry.WrappedObject.PropertyBag, pvda);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000898D File Offset: 0x00006B8D
		protected override void MessageSetItemProperties(StorageFxProxyPool.MessageEntry message, ItemPropertiesBase props)
		{
			if (props != null)
			{
				message.CachedItemProperties.Add(props);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000089A0 File Offset: 0x00006BA0
		protected override byte[] MessageSaveChanges(StorageFxProxyPool.MessageEntry entry)
		{
			CoreItem referencedObject = entry.WrappedObject.ReferenceCoreItem.ReferencedObject;
			if (entry.MimeStream != null || entry.CachedItemProperties.Count > 0)
			{
				using (Item item = new Item(referencedObject, true))
				{
					if (entry.MimeStream != null)
					{
						InboundConversionOptions scopedInboundConversionOptions = MapiUtils.GetScopedInboundConversionOptions(this.destMailbox.StoreSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
						using (entry.MimeStream)
						{
							ItemConversion.ConvertAnyMimeToItem(item, entry.MimeStream, scopedInboundConversionOptions);
						}
					}
					foreach (ItemPropertiesBase itemPropertiesBase in entry.CachedItemProperties)
					{
						itemPropertiesBase.Apply((MailboxSession)this.destMailbox.StoreSession, item);
					}
				}
			}
			if (entry.CachedPropValues.Count > 0)
			{
				this.SetProps(entry.WrappedObject.PropertyBag, entry.CachedPropValues.ToArray());
			}
			entry.WrappedObject.Save();
			referencedObject.PropertyBag.Load(StorageFxProxyPool.EntryIdPropDef);
			return referencedObject.PropertyBag[StorageFxProxyPool.EntryIdPropDef[0]] as byte[];
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00008AFC File Offset: 0x00006CFC
		protected override void MessageWriteToMime(StorageFxProxyPool.MessageEntry entry, byte[] buffer)
		{
			if (entry.MimeStream == null)
			{
				entry.MimeStream = TemporaryStorage.Create();
			}
			entry.MimeStream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00008B24 File Offset: 0x00006D24
		private static MessageFlags UpdateMessageFlags(IPropertyBag propertyBag, MessageFlags flagsFromSource)
		{
			MessageFlags messageFlags = MessageFlags.None;
			AnnotatedPropertyValue annotatedProperty = propertyBag.GetAnnotatedProperty(PropertyTag.MessageFlags);
			if (!annotatedProperty.PropertyValue.IsError)
			{
				messageFlags = (MessageFlags)((int)annotatedProperty.PropertyValue.Value);
			}
			if (flagsFromSource.HasFlag(MessageFlags.Read))
			{
				messageFlags |= MessageFlags.Read;
			}
			else
			{
				messageFlags &= ~MessageFlags.Read;
			}
			if (flagsFromSource.HasFlag(MessageFlags.Unsent))
			{
				messageFlags |= MessageFlags.Unsent;
			}
			else
			{
				messageFlags &= ~MessageFlags.Unsent;
			}
			return messageFlags;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00008BA4 File Offset: 0x00006DA4
		private static bool ShouldUpdateIconIndex(IPropertyBag propertyBag, IconIndex iconIndexFromSource)
		{
			if (iconIndexFromSource.Equals(IconIndex.BaseMail))
			{
				AnnotatedPropertyValue annotatedProperty = propertyBag.GetAnnotatedProperty(new PropertyTag(276824067U));
				if (annotatedProperty.PropertyValue.IsError)
				{
					return false;
				}
				IconIndex iconIndex = (IconIndex)annotatedProperty.PropertyValue.Value;
				if (iconIndex != IconIndex.MailReplied && iconIndex != IconIndex.MailForwarded)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00008C14 File Offset: 0x00006E14
		[Conditional("Debug")]
		private void ValidateMessageEntry(StorageFxProxyPool.MessageEntry entry)
		{
			bool isOlcSync = this.destMailbox.IsOlcSync;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00008C24 File Offset: 0x00006E24
		private void SetProps(IPropertyBag propertyBag, PropValueData[] pvda)
		{
			if (pvda == null)
			{
				return;
			}
			int i = 0;
			while (i < pvda.Length)
			{
				PropValueData propValueData = pvda[i];
				object obj = propValueData.Value;
				if (obj is DateTime)
				{
					obj = new ExDateTime(ExTimeZone.TimeZoneFromKind(((DateTime)obj).Kind), (DateTime)obj);
				}
				PropTag propTag = (PropTag)propValueData.PropTag;
				PropTag propTag2 = propTag;
				if (propTag2 == PropTag.MessageFlags)
				{
					MessageFlags messageFlags = StorageFxProxyPool.UpdateMessageFlags(propertyBag, (MessageFlags)((int)obj));
					obj = (int)messageFlags;
					goto IL_8B;
				}
				if (propTag2 != (PropTag)276824067U)
				{
					goto IL_8B;
				}
				if (StorageFxProxyPool.ShouldUpdateIconIndex(propertyBag, (IconIndex)obj))
				{
					goto Block_5;
				}
				IL_B5:
				i++;
				continue;
				Block_5:
				try
				{
					IL_8B:
					propertyBag.SetProperty(new PropertyValue(new PropertyTag((uint)propValueData.PropTag), obj));
				}
				catch (ArgumentException ex)
				{
					throw new ExArgumentException(ex.Message, ex);
				}
				goto IL_B5;
			}
		}

		// Token: 0x0400001D RID: 29
		private static readonly PropertyTagPropertyDefinition[] EntryIdPropDef = new PropertyTagPropertyDefinition[]
		{
			PropertyTagPropertyDefinition.CreateCustom("EntryId", 268370178U)
		};

		// Token: 0x0400001E RID: 30
		private StorageDestinationMailbox destMailbox;

		// Token: 0x0200000C RID: 12
		internal abstract class StorageEntry<T> : DisposableWrapper<T> where T : class, IDisposable
		{
			// Token: 0x060000C4 RID: 196 RVA: 0x00008D34 File Offset: 0x00006F34
			protected StorageEntry(T entry) : base(entry, true)
			{
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x00008D3E File Offset: 0x00006F3E
			// (set) Token: 0x060000C6 RID: 198 RVA: 0x00008D46 File Offset: 0x00006F46
			public IMapiFxProxy Proxy { get; protected set; }

			// Token: 0x060000C7 RID: 199 RVA: 0x00008D4F File Offset: 0x00006F4F
			protected override void InternalDispose(bool disposing)
			{
				if (disposing && this.Proxy != null)
				{
					this.Proxy.Dispose();
					this.Proxy = null;
				}
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x0200000D RID: 13
		internal class FolderEntry : StorageFxProxyPool.StorageEntry<StorageDestinationFolder>
		{
			// Token: 0x060000C8 RID: 200 RVA: 0x00008D75 File Offset: 0x00006F75
			private FolderEntry(StorageDestinationFolder folder, bool isMoveUser) : base(folder)
			{
				base.Proxy = new StorageFolderProxy(folder, isMoveUser);
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x00008D8B File Offset: 0x00006F8B
			public static StorageFxProxyPool.FolderEntry Wrap(StorageDestinationFolder folder, bool isMoveUser)
			{
				if (folder != null)
				{
					return new StorageFxProxyPool.FolderEntry(folder, isMoveUser);
				}
				return null;
			}
		}

		// Token: 0x0200000E RID: 14
		internal class MessageEntry : StorageFxProxyPool.StorageEntry<MessageAdaptor>
		{
			// Token: 0x060000CA RID: 202 RVA: 0x00008D99 File Offset: 0x00006F99
			private MessageEntry(MessageAdaptor message, bool isMoveUser) : base(message)
			{
				base.Proxy = new StorageMessageProxy(message, isMoveUser);
				this.MimeStream = null;
				this.CachedPropValues = new List<PropValueData>(10);
				this.CachedItemProperties = new List<ItemPropertiesBase>(1);
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000CB RID: 203 RVA: 0x00008DCF File Offset: 0x00006FCF
			// (set) Token: 0x060000CC RID: 204 RVA: 0x00008DD7 File Offset: 0x00006FD7
			internal Stream MimeStream { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000CD RID: 205 RVA: 0x00008DE0 File Offset: 0x00006FE0
			// (set) Token: 0x060000CE RID: 206 RVA: 0x00008DE8 File Offset: 0x00006FE8
			internal List<PropValueData> CachedPropValues { get; private set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000CF RID: 207 RVA: 0x00008DF1 File Offset: 0x00006FF1
			// (set) Token: 0x060000D0 RID: 208 RVA: 0x00008DF9 File Offset: 0x00006FF9
			internal List<ItemPropertiesBase> CachedItemProperties { get; private set; }

			// Token: 0x060000D1 RID: 209 RVA: 0x00008E02 File Offset: 0x00007002
			public static StorageFxProxyPool.MessageEntry Wrap(MessageAdaptor message, bool isMoveUser)
			{
				if (message != null)
				{
					return new StorageFxProxyPool.MessageEntry(message, isMoveUser);
				}
				return null;
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x00008E10 File Offset: 0x00007010
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					if (this.MimeStream != null)
					{
						this.MimeStream.Dispose();
						this.MimeStream = null;
					}
					base.InternalDispose(disposing);
				}
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00008E36 File Offset: 0x00007036
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<StorageFxProxyPool.MessageEntry>(this);
			}
		}
	}
}
