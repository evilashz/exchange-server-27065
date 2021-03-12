using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000162 RID: 354
	internal class MoveObjectInfo<T> : DisposeTrackableBase where T : class
	{
		// Token: 0x06000CB7 RID: 3255 RVA: 0x0001E22C File Offset: 0x0001C42C
		public MoveObjectInfo(Guid mdbGuid, MapiStore store, byte[] messageId, string folderName, string messageClass, string subject, byte[] searchKey)
		{
			this.store = store;
			this.MessageId = messageId;
			this.FolderId = null;
			this.message = null;
			this.folderName = folderName;
			this.messageClass = messageClass;
			this.searchKey = searchKey;
			this.subject = subject;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0001E27B File Offset: 0x0001C47B
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0001E283 File Offset: 0x0001C483
		public byte[] MessageId { get; private set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0001E28C File Offset: 0x0001C48C
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0001E294 File Offset: 0x0001C494
		public byte[] FolderId { get; private set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0001E29D File Offset: 0x0001C49D
		public bool MessageFound
		{
			get
			{
				return this.message != null;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0001E2AC File Offset: 0x0001C4AC
		public DateTime CreationTimestamp
		{
			get
			{
				if (this.message == null)
				{
					return DateTime.MinValue;
				}
				return this.message.GetProp(PropTag.CreationTime).GetDateTime();
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0001E2DF File Offset: 0x0001C4DF
		public static List<T> LoadAll(Guid mdbGuid, MapiStore store, string folderName)
		{
			return MoveObjectInfo<T>.LoadAll(null, mdbGuid, store, folderName);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0001E2EA File Offset: 0x0001C4EA
		public static List<T> LoadAll(byte[] searchKey, Guid mdbGuid, MapiStore store, string folderName)
		{
			return MoveObjectInfo<T>.LoadAll(searchKey, mdbGuid, store, folderName, null, null);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0001E2F7 File Offset: 0x0001C4F7
		public static List<T> LoadAll(byte[] searchKey, Guid mdbGuid, MapiStore store, string folderName, MoveObjectInfo<T>.IsSupportedObjectTypeDelegate isSupportedObjectType, MoveObjectInfo<T>.EmptyTDelegate emptyT)
		{
			return MoveObjectInfo<T>.LoadAll(searchKey, null, mdbGuid, store, folderName, isSupportedObjectType, emptyT);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0001E308 File Offset: 0x0001C508
		public static List<T> LoadAll(byte[] searchKey, Restriction additionalRestriction, Guid mdbGuid, MapiStore store, string folderName, MoveObjectInfo<T>.IsSupportedObjectTypeDelegate isSupportedObjectType, MoveObjectInfo<T>.EmptyTDelegate emptyT)
		{
			List<T> list = new List<T>();
			using (MapiFolder mapiFolder = MapiUtils.OpenFolderUnderRoot(store, folderName, false))
			{
				if (mapiFolder == null)
				{
					return list;
				}
				using (MapiTable contentsTable = mapiFolder.GetContentsTable(ContentsTableFlags.DeferredErrors))
				{
					PropTag propTag = PropTag.ReplyTemplateID;
					contentsTable.SortTable(new SortOrder(propTag, SortFlags.Ascend), SortTableFlags.None);
					List<PropTag> list2 = new List<PropTag>();
					list2.Add(PropTag.EntryId);
					list2.Add(propTag);
					Restriction restriction = null;
					if (searchKey != null)
					{
						restriction = Restriction.EQ(propTag, searchKey);
					}
					if (additionalRestriction != null)
					{
						if (restriction == null)
						{
							restriction = additionalRestriction;
						}
						else
						{
							restriction = Restriction.And(new Restriction[]
							{
								restriction,
								additionalRestriction
							});
						}
					}
					foreach (PropValue[] array2 in MapiUtils.QueryAllRows(contentsTable, restriction, list2))
					{
						byte[] bytes = array2[0].GetBytes();
						byte[] bytes2 = array2[1].GetBytes();
						OpenEntryFlags flags = OpenEntryFlags.Modify | OpenEntryFlags.DontThrowIfEntryIsMissing;
						using (MapiMessage mapiMessage = (MapiMessage)store.OpenEntry(bytes, flags))
						{
							if (mapiMessage != null)
							{
								T t = default(T);
								if (isSupportedObjectType != null)
								{
									if (isSupportedObjectType(mapiMessage, store))
									{
										t = MoveObjectInfo<T>.ReadObjectFromMessage(mapiMessage, false);
									}
									if (t == null && emptyT != null)
									{
										t = emptyT(bytes2);
									}
								}
								else
								{
									t = MoveObjectInfo<T>.ReadObjectFromMessage(mapiMessage, false);
								}
								if (t != null)
								{
									list.Add(t);
								}
								else
								{
									MrsTracer.Common.Error("Unable to deserialize message '{0}'.", new object[]
									{
										bytes
									});
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		public T ReadObject(ReadObjectFlags flags)
		{
			List<T> list = this.ReadObjectChunks(flags | ReadObjectFlags.LastChunkOnly);
			if (list == null || list.Count <= 0)
			{
				return default(T);
			}
			return list[list.Count - 1];
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0001E534 File Offset: 0x0001C734
		public List<T> ReadObjectChunks(ReadObjectFlags flags)
		{
			if ((flags & ReadObjectFlags.Refresh) != ReadObjectFlags.None)
			{
				this.message.Dispose();
				this.message = null;
			}
			if (this.message == null && !this.OpenMessage())
			{
				return null;
			}
			List<T> list = new List<T>();
			T t;
			if ((flags & ReadObjectFlags.LastChunkOnly) == ReadObjectFlags.None)
			{
				using (MapiTable attachmentTable = this.message.GetAttachmentTable())
				{
					if (attachmentTable != null)
					{
						PropValue[][] array = attachmentTable.QueryAllRows(null, MoveObjectInfo<T>.AttachmentTagsToLoad);
						foreach (PropValue[] array3 in array)
						{
							int @int = array3[0].GetInt();
							using (MapiAttach mapiAttach = this.message.OpenAttach(@int))
							{
								using (MapiStream mapiStream = mapiAttach.OpenStream(PropTag.AttachDataBin, OpenPropertyFlags.BestAccess))
								{
									t = MoveObjectInfo<T>.DeserializeFromStream(mapiStream, (flags & ReadObjectFlags.DontThrowOnCorruptData) == ReadObjectFlags.None);
								}
								if (t != null)
								{
									list.Add(t);
								}
							}
						}
					}
				}
			}
			t = MoveObjectInfo<T>.ReadObjectFromMessage(this.message, (flags & ReadObjectFlags.DontThrowOnCorruptData) == ReadObjectFlags.None);
			if (t != null)
			{
				list.Add(t);
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0001E67C File Offset: 0x0001C87C
		public bool CheckIfUnderlyingMessageHasChanged()
		{
			PropValue prop = this.message.GetProp(PropTag.ChangeKey);
			bool result;
			using (MapiMessage mapiMessage = (MapiMessage)this.store.OpenEntry(this.MessageId))
			{
				PropValue prop2 = mapiMessage.GetProp(PropTag.ChangeKey);
				result = !PropValue.Equals(prop, prop2);
			}
			return result;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0001E6E8 File Offset: 0x0001C8E8
		public bool CheckObjectType(MoveObjectInfo<T>.IsSupportedObjectTypeDelegate isSupportedObjectType)
		{
			return isSupportedObjectType(this.message, this.store);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		public bool OpenMessage()
		{
			if (this.MessageId == null)
			{
				this.MessageId = this.FindMessageId();
				if (this.MessageId == null)
				{
					return false;
				}
			}
			OpenEntryFlags flags = OpenEntryFlags.Modify | OpenEntryFlags.DontThrowIfEntryIsMissing;
			this.message = (MapiMessage)this.store.OpenEntry(this.MessageId, flags);
			return this.message != null;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0001E758 File Offset: 0x0001C958
		public void DeleteMessage()
		{
			if (this.MessageId != null)
			{
				using (MapiFolder mapiFolder = MapiUtils.OpenFolderUnderRoot(this.store, this.folderName, false))
				{
					if (mapiFolder != null)
					{
						mapiFolder.DeleteMessages(DeleteMessagesFlags.ForceHardDelete, new byte[][]
						{
							this.MessageId
						});
					}
				}
				if (this.message != null)
				{
					this.message.Dispose();
					this.message = null;
				}
				this.MessageId = null;
				this.FolderId = null;
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		public void DeleteOldMessages()
		{
			using (MapiFolder mapiFolder = MapiUtils.OpenFolderUnderRoot(this.store, this.folderName, false))
			{
				if (mapiFolder != null)
				{
					for (int i = 0; i < 100; i++)
					{
						byte[] array = this.FindMessageId();
						if (array == null)
						{
							break;
						}
						mapiFolder.DeleteMessages(DeleteMessagesFlags.None, new byte[][]
						{
							array
						});
					}
				}
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0001E84C File Offset: 0x0001CA4C
		public void SaveObject(T obj, MoveObjectInfo<T>.GetAdditionalProperties getAdditionalPropertiesCallback)
		{
			this.SaveObjectChunks(new List<T>(1)
			{
				obj
			}, 1, getAdditionalPropertiesCallback);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0001E870 File Offset: 0x0001CA70
		public void SaveObjectChunks(List<T> chunks, int maxChunks, MoveObjectInfo<T>.GetAdditionalProperties getAdditionalPropertiesCallback)
		{
			if (chunks.Count > maxChunks)
			{
				MrsTracer.Common.Warning("Too many chunks supplied, truncating", new object[0]);
				chunks.RemoveRange(0, chunks.Count - maxChunks);
			}
			bool flag = false;
			if (this.message == null)
			{
				using (MapiFolder mapiFolder = MapiUtils.OpenFolderUnderRoot(this.store, this.folderName, true))
				{
					this.FolderId = mapiFolder.GetProp(PropTag.EntryId).GetBytes();
					this.message = mapiFolder.CreateMessage();
				}
				this.message.SetProps(new PropValue[]
				{
					new PropValue(PropTag.MessageClass, this.messageClass),
					new PropValue(PropTag.Subject, this.subject),
					new PropValue(PropTag.ReplyTemplateID, this.searchKey)
				});
				flag = true;
			}
			if (chunks.Count > 1)
			{
				using (MapiTable attachmentTable = this.message.GetAttachmentTable())
				{
					if (attachmentTable != null)
					{
						int num = attachmentTable.GetRowCount() - (maxChunks - chunks.Count);
						if (num > 0)
						{
							attachmentTable.SetColumns(MoveObjectInfo<T>.AttachmentTagsToLoad);
							PropValue[][] array = attachmentTable.QueryRows(num);
							for (int i = 0; i < num; i++)
							{
								this.message.DeleteAttach(array[i][0].GetInt());
							}
						}
					}
				}
				for (int j = 0; j < chunks.Count - 1; j++)
				{
					int num2;
					using (MapiAttach mapiAttach = this.message.CreateAttach(out num2))
					{
						using (MapiStream mapiStream = mapiAttach.OpenStream(PropTag.AttachDataBin, OpenPropertyFlags.Create))
						{
							MoveObjectInfo<T>.SerializeToStream(chunks[j], mapiStream);
						}
						mapiAttach.SetProps(new PropValue[]
						{
							new PropValue(PropTag.AttachFileName, string.Format("MOI_Chunk_{0:yyyymmdd_HHmmssfff}", DateTime.UtcNow)),
							new PropValue(PropTag.AttachMethod, AttachMethods.ByValue)
						});
						mapiAttach.SaveChanges();
					}
				}
			}
			T obj = chunks[chunks.Count - 1];
			if (getAdditionalPropertiesCallback != null)
			{
				this.message.SetProps(getAdditionalPropertiesCallback(this.store));
			}
			using (MapiStream mapiStream2 = this.message.OpenStream(PropTag.Body, OpenPropertyFlags.Create))
			{
				MoveObjectInfo<T>.SerializeToStream(obj, mapiStream2);
			}
			this.message.SaveChanges();
			if (flag)
			{
				this.MessageId = this.message.GetProp(PropTag.EntryId).GetBytes();
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0001EB70 File Offset: 0x0001CD70
		public void SaveObject(T obj)
		{
			this.SaveObject(obj, null);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0001EB7A File Offset: 0x0001CD7A
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.message != null)
				{
					this.message.Dispose();
					this.message = null;
				}
				this.store = null;
				this.FolderId = null;
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0001EBA7 File Offset: 0x0001CDA7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MoveObjectInfo<T>>(this);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0001EBB0 File Offset: 0x0001CDB0
		private static void SerializeToStream(T obj, Stream stream)
		{
			if (obj == null)
			{
				return;
			}
			if (typeof(T) == typeof(string))
			{
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					streamWriter.Write((string)((object)obj));
					CommonUtils.AppendNewLinesAndFlush(streamWriter);
					return;
				}
			}
			XMLSerializableBase.SerializeToStream(obj, stream, false);
			using (StreamWriter streamWriter2 = new StreamWriter(stream))
			{
				CommonUtils.AppendNewLinesAndFlush(streamWriter2);
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0001EC50 File Offset: 0x0001CE50
		private static T DeserializeFromStream(Stream stream, bool throwOnDeserializationError)
		{
			if (typeof(T) == typeof(string))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					return (T)((object)streamReader.ReadToEnd());
				}
			}
			return XMLSerializableBase.Deserialize<T>(stream, throwOnDeserializationError);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
		private static T ReadObjectFromMessage(MapiMessage mapiMessage, bool throwOnDeserializationError)
		{
			T result;
			using (MapiStream mapiStream = mapiMessage.OpenStream(PropTag.Body, OpenPropertyFlags.BestAccess))
			{
				result = MoveObjectInfo<T>.DeserializeFromStream(mapiStream, throwOnDeserializationError);
			}
			return result;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0001ECF0 File Offset: 0x0001CEF0
		private byte[] FindMessageId()
		{
			using (MapiFolder mapiFolder = MapiUtils.OpenFolderUnderRoot(this.store, this.folderName, false))
			{
				if (mapiFolder == null)
				{
					return null;
				}
				this.FolderId = mapiFolder.GetProp(PropTag.EntryId).GetBytes();
				using (MapiTable contentsTable = mapiFolder.GetContentsTable(ContentsTableFlags.DeferredErrors))
				{
					PropTag propTag = PropTag.ReplyTemplateID;
					contentsTable.SetColumns(new PropTag[]
					{
						PropTag.EntryId
					});
					contentsTable.SortTable(new SortOrder(propTag, SortFlags.Ascend), SortTableFlags.None);
					if (contentsTable.FindRow(Restriction.EQ(propTag, this.searchKey), BookMark.Beginning, FindRowFlag.None))
					{
						PropValue[][] array = contentsTable.QueryRows(1);
						if (array == null || array.Length == 0 || array[0].Length == 0)
						{
							return null;
						}
						return array[0][0].GetBytes();
					}
				}
			}
			return null;
		}

		// Token: 0x04000740 RID: 1856
		private static readonly PropTag[] AttachmentTagsToLoad = new PropTag[]
		{
			PropTag.AttachNum
		};

		// Token: 0x04000741 RID: 1857
		private MapiMessage message;

		// Token: 0x04000742 RID: 1858
		private MapiStore store;

		// Token: 0x04000743 RID: 1859
		private string messageClass;

		// Token: 0x04000744 RID: 1860
		private string folderName;

		// Token: 0x04000745 RID: 1861
		private byte[] searchKey;

		// Token: 0x04000746 RID: 1862
		private string subject;

		// Token: 0x02000163 RID: 355
		// (Invoke) Token: 0x06000CD4 RID: 3284
		public delegate bool IsSupportedObjectTypeDelegate(MapiMessage msg, MapiStore store);

		// Token: 0x02000164 RID: 356
		// (Invoke) Token: 0x06000CD8 RID: 3288
		public delegate PropValue[] GetAdditionalProperties(MapiStore store);

		// Token: 0x02000165 RID: 357
		// (Invoke) Token: 0x06000CDC RID: 3292
		public delegate T EmptyTDelegate(byte[] searchKey);
	}
}
