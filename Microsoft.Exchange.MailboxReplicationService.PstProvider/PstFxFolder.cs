using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000007 RID: 7
	internal class PstFxFolder : IFolder, IDisposable
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003C2F File Offset: 0x00001E2F
		public PstFxFolder(PstMailbox pstMailbox, IFolder iPstFolder)
		{
			this.pstMailbox = pstMailbox;
			this.iPstFolder = iPstFolder;
			this.propertyBag = new PSTPropertyBag(pstMailbox, iPstFolder.PropertyBag);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003C57 File Offset: 0x00001E57
		public IPropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003C5F File Offset: 0x00001E5F
		public bool IsContentAvailable
		{
			get
			{
				return this.iPstFolder.MessageIds.Count != 0 || this.iPstFolder.SubFolderIds.Count != 0;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003C8B File Offset: 0x00001E8B
		public PstMailbox PstMailbox
		{
			get
			{
				return this.pstMailbox;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003C93 File Offset: 0x00001E93
		public IFolder IPstFolder
		{
			get
			{
				return this.iPstFolder;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003C9B File Offset: 0x00001E9B
		public void Dispose()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public PropertyValue[] GetProps(PropertyTag[] pta)
		{
			PropertyValue[] array = new PropertyValue[pta.Length];
			for (int i = 0; i < pta.Length; i++)
			{
				if (pta[i] == PropertyTag.EntryId)
				{
					array[i] = new PropertyValue(PropertyTag.EntryId, PstMailbox.CreateEntryIdFromNodeId(this.pstMailbox.IPst.MessageStore.Guid, this.iPstFolder.Id));
				}
				else if (pta[i] == PropertyTag.ParentEntryId)
				{
					array[i] = new PropertyValue(PropertyTag.ParentEntryId, PstMailbox.CreateEntryIdFromNodeId(this.pstMailbox.IPst.MessageStore.Guid, this.iPstFolder.ParentId));
				}
				else if (pta[i] == PropertyTag.LastModificationTime)
				{
					array[i] = this.propertyBag.GetProperty(PropertyTag.LastModificationTime);
					if (array[i].IsError)
					{
						array[i] = new PropertyValue(PropertyTag.LastModificationTime, ExDateTime.MinValue);
					}
				}
				else if (pta[i] == PropertyTag.FolderType)
				{
					array[i] = this.propertyBag.GetProperty(PropertyTag.FolderType);
					if (array[i].IsError)
					{
						array[i] = new PropertyValue(PropertyTag.FolderType, (this.iPstFolder.Id == this.iPstFolder.ParentId) ? 0 : 1);
					}
				}
				else if (pta[i] == PropertyTag.DisplayName)
				{
					array[i] = this.propertyBag.GetProperty(PropertyTag.DisplayName);
					if (array[i].IsError)
					{
						array[i] = new PropertyValue(PropertyTag.DisplayName, (this.iPstFolder.Id == this.iPstFolder.ParentId) ? "Root of PST" : string.Format("[{0}]", this.iPstFolder.Id));
					}
				}
				else if (pta[i] == PstMailbox.MessageSizeExtended)
				{
					array[i] = new PropertyValue(PstMailbox.MessageSizeExtended, (long)this.iPstFolder.TotalComputedMessageSize);
				}
				else
				{
					array[i] = this.propertyBag.GetProperty(pta[i]);
				}
			}
			return array;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003F60 File Offset: 0x00002160
		public PropertyValue GetProp(PropertyTag ptag)
		{
			PropertyValue[] props = this.GetProps(new PropertyTag[]
			{
				ptag
			});
			return props[0];
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003F95 File Offset: 0x00002195
		public IFolder CreateFolder()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003F9C File Offset: 0x0000219C
		public IMessage CreateMessage(bool isAssociatedMessage)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004164 File Offset: 0x00002364
		public IEnumerable<IFolder> GetFolders()
		{
			foreach (uint subFolderId in this.iPstFolder.SubFolderIds)
			{
				IFolder subFolder = this.pstMailbox.IPst.ReadFolder(subFolderId);
				yield return new PstFxFolder(this.pstMailbox, subFolder);
			}
			yield break;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004344 File Offset: 0x00002544
		public IEnumerable<IMessage> GetContents()
		{
			foreach (uint messageId in this.iPstFolder.MessageIds)
			{
				IMessage message = this.pstMailbox.IPst.ReadMessage(messageId);
				yield return new PSTMessage(this.pstMailbox, message);
			}
			yield break;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004524 File Offset: 0x00002724
		public IEnumerable<IMessage> GetAssociatedContents()
		{
			foreach (uint associatedMessageId in this.iPstFolder.AssociatedMessageIds)
			{
				IMessage message = this.pstMailbox.IPst.ReadMessage(associatedMessageId);
				yield return new PSTMessage(this.pstMailbox, message);
			}
			yield break;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004541 File Offset: 0x00002741
		public void Save()
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004543 File Offset: 0x00002743
		public string[] GetReplicaDatabases(out ushort localSiteDatabaseCount)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000454A File Offset: 0x0000274A
		public StoreLongTermId GetLongTermId()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400001B RID: 27
		private PstMailbox pstMailbox;

		// Token: 0x0400001C RID: 28
		private IFolder iPstFolder;

		// Token: 0x0400001D RID: 29
		private PSTPropertyBag propertyBag;
	}
}
