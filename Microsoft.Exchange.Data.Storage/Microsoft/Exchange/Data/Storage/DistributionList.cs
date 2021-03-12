using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004E0 RID: 1248
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DistributionList : ContactBase, IDistributionList, IContactBase, IItem, IStoreObject, IRecipientBaseCollection<DistributionListMember>, IList<DistributionListMember>, ICollection<DistributionListMember>, IEnumerable<DistributionListMember>, IEnumerable, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x0600365A RID: 13914 RVA: 0x000DB80D File Offset: 0x000D9A0D
		internal DistributionList(ICoreItem coreItem) : base(coreItem)
		{
			if (base.IsNew)
			{
				this.Initialize();
			}
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000DB824 File Offset: 0x000D9A24
		private static ParticipantEntryId[] ParseEntryIds(byte[][] entryIds)
		{
			ParticipantEntryId[] array = new ParticipantEntryId[entryIds.Length];
			for (int i = 0; i < entryIds.Length; i++)
			{
				array[i] = ParticipantEntryId.TryFromEntryId(entryIds[i]);
			}
			return array;
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x000DB854 File Offset: 0x000D9A54
		private static void ParseEntryIdStream(Stream dlStream, out ParticipantEntryId[] mainEntryIds, out ParticipantEntryId[] oneOffEntryIds, out byte[][] mainIds, out byte[][] extraBytes, out bool alwaysStream)
		{
			using (BinaryReader binaryReader = new BinaryReader(dlStream, Encoding.Unicode))
			{
				binaryReader.ReadUInt16();
				binaryReader.ReadUInt16();
				binaryReader.ReadInt32();
				int num = binaryReader.ReadInt32();
				alwaysStream = ((num & 1) == 1);
				int num2 = binaryReader.ReadInt32();
				if (num2 > StorageLimits.Instance.DistributionListMaxNumberOfEntries)
				{
					throw new CorruptDataException(ServerStrings.ExPDLCorruptOutlookBlob("TooManyEntries"));
				}
				binaryReader.ReadInt32();
				binaryReader.ReadInt32();
				binaryReader.ReadInt32();
				mainIds = new byte[num2][];
				byte[][] array = new byte[num2][];
				extraBytes = new byte[num2][];
				for (int i = 0; i < num2; i++)
				{
					int num3 = binaryReader.ReadInt32();
					mainIds[i] = ((num3 > 0) ? binaryReader.ReadBytes(num3) : Array<byte>.Empty);
					num3 = binaryReader.ReadInt32();
					array[i] = ((num3 > 0) ? binaryReader.ReadBytes(num3) : Array<byte>.Empty);
					num3 = binaryReader.ReadInt32();
					extraBytes[i] = ((num3 > 0) ? binaryReader.ReadBytes(num3) : Array<byte>.Empty);
				}
				mainEntryIds = DistributionList.ParseEntryIds(mainIds);
				oneOffEntryIds = DistributionList.ParseEntryIds(array);
			}
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000DB984 File Offset: 0x000D9B84
		private static bool NeedToStream(PropertyDefinition propertyDefinition, byte[][] value)
		{
			int num = 0;
			foreach (byte[] array in value)
			{
				num += array.Length;
			}
			return num > StorageLimits.Instance.DistributionListMaxMembersPropertySize;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000DB9C0 File Offset: 0x000D9BC0
		private static void SerializeEntryIdsOnStream(bool alwaysStream, Stream dlStream, ParticipantEntryId[] mainEntryIds, ParticipantEntryId[] oneOffEntryIds, byte[][] extraBytes)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			using (BinaryWriter binaryWriter = new BinaryWriter(dlStream, Encoding.Unicode))
			{
				binaryWriter.Write(1);
				binaryWriter.Write(0);
				binaryWriter.Write(14000);
				binaryWriter.Write(alwaysStream ? 1 : 0);
				binaryWriter.Write(mainEntryIds.Length);
				binaryWriter.Write(num);
				binaryWriter.Write(num2);
				binaryWriter.Write(num3);
				for (int i = 0; i < mainEntryIds.Length; i++)
				{
					byte[] array = mainEntryIds[i].ToByteArray();
					num += array.Length;
					binaryWriter.Write(array.Length);
					if (array.Length > 0)
					{
						binaryWriter.Write(array);
					}
					array = oneOffEntryIds[i].ToByteArray();
					num2 += array.Length;
					binaryWriter.Write(array.Length);
					if (array.Length > 0)
					{
						binaryWriter.Write(array);
					}
					num3 += extraBytes[i].Length;
					binaryWriter.Write(extraBytes[i].Length);
					if (extraBytes[i].Length > 0)
					{
						binaryWriter.Write(extraBytes[i]);
					}
				}
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				int offset = 16;
				binaryWriter.Seek(offset, SeekOrigin.Begin);
				binaryWriter.Write(num);
				binaryWriter.Write(num2);
				binaryWriter.Write(num3);
			}
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000DBB18 File Offset: 0x000D9D18
		private static byte[][] EncodeEntryIds(ParticipantEntryId[] entryIds)
		{
			byte[][] array = new byte[entryIds.Length][];
			for (int i = 0; i < entryIds.Length; i++)
			{
				if (entryIds[i] != null)
				{
					array[i] = entryIds[i].ToByteArray();
				}
				else
				{
					array[i] = Array<byte>.Empty;
				}
			}
			return array;
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000DBB58 File Offset: 0x000D9D58
		public static bool IsDL(RecipientType recipientType)
		{
			EnumValidator.ThrowIfInvalid<RecipientType>(recipientType);
			switch (recipientType)
			{
			case RecipientType.Group:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.DynamicDistributionGroup:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000DBB90 File Offset: 0x000D9D90
		public static Participant[] ExpandDeep(StoreSession storeSession, StoreObjectId distributionListId, bool shouldAddNonExistPDL)
		{
			Dictionary<StoreObjectId, Participant> dictionary = new Dictionary<StoreObjectId, Participant>();
			Queue<StoreObjectId> queue = new Queue<StoreObjectId>();
			List<Participant> list = new List<Participant>();
			queue.Enqueue(distributionListId);
			while (queue.Count > 0)
			{
				StoreObjectId storeObjectId = queue.Dequeue();
				DistributionList distributionList = null;
				try
				{
					distributionList = DistributionList.Bind(storeSession, storeObjectId);
				}
				catch (ObjectNotFoundException arg)
				{
					if (storeSession.ItemBinder != null)
					{
						Item item = storeSession.ItemBinder.BindItem(storeObjectId, IdConverter.IsFromPublicStore(storeObjectId), IdConverter.GetParentIdFromMessageId(storeObjectId));
						distributionList = (item as DistributionList);
						if (item != null && distributionList == null)
						{
							item.Dispose();
						}
					}
					if (distributionList == null)
					{
						ExTraceGlobals.StorageTracer.TraceDebug<ObjectNotFoundException>(0L, "DistributionList::ExpandDeep. A PDL member in PDL doesn't exist. Ignore it and continue to expand other members. Exception = {0}.", arg);
						if (shouldAddNonExistPDL && dictionary.ContainsKey(storeObjectId))
						{
							list.Add(new Participant(dictionary[storeObjectId].DisplayName, null, "MAPIPDL"));
						}
					}
				}
				if (distributionList != null)
				{
					using (distributionList)
					{
						foreach (DistributionListMember distributionListMember in distributionList)
						{
							if (!(distributionListMember.Participant == null))
							{
								if (distributionListMember.IsDistributionList() == true && distributionListMember.Participant.Origin is StoreParticipantOrigin && distributionListMember.Participant.ValidationStatus == ParticipantValidationStatus.NoError)
								{
									StoreObjectId originItemId = ((StoreParticipantOrigin)distributionListMember.Participant.Origin).OriginItemId;
									if (!dictionary.ContainsKey(originItemId) && !originItemId.Equals(distributionListId))
									{
										queue.Enqueue(originItemId);
										dictionary.Add(originItemId, distributionListMember.Participant);
									}
								}
								else
								{
									list.Add(distributionListMember.Participant);
								}
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000DBD78 File Offset: 0x000D9F78
		public static Participant[] ExpandDeep(StoreSession storeSession, StoreObjectId distributionListId)
		{
			return DistributionList.ExpandDeep(storeSession, distributionListId, false);
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000DBD82 File Offset: 0x000D9F82
		public new static DistributionList Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<DistributionList>(session, storeId, DistributionListSchema.Instance, propsToReturn);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000DBD91 File Offset: 0x000D9F91
		public new static DistributionList Bind(StoreSession session, StoreId storeId)
		{
			return DistributionList.Bind(session, storeId, null);
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000DBD9B File Offset: 0x000D9F9B
		public new static DistributionList Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propsToReturn)
		{
			return DistributionList.Bind(session, storeId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000DBDAA File Offset: 0x000D9FAA
		public static DistributionList Create(StoreSession session, StoreId contactFolderId)
		{
			return ItemBuilder.CreateNewItem<DistributionList>(session, contactFolderId, ItemCreateInfo.DistributionListInfo);
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000DBDB8 File Offset: 0x000D9FB8
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DistributionList>(this);
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x000DBDC0 File Offset: 0x000D9FC0
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return DistributionListSchema.Instance;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x000DBDD2 File Offset: 0x000D9FD2
		public override bool IsDirty
		{
			get
			{
				this.CheckDisposed("IsDirty::get");
				return base.IsDirty || this.areMembersChanged;
			}
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000DBDEF File Offset: 0x000D9FEF
		public Participant GetAsParticipant()
		{
			return base.GetValueOrDefault<Participant>(InternalSchema.DistributionListParticipant);
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000DBDFC File Offset: 0x000D9FFC
		public void Sort(IComparer<DistributionListMember> comparer)
		{
			this.CheckDisposed("Sort");
			this.EnsureExpanded();
			this.members.Sort(comparer);
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000DBE1C File Offset: 0x000DA01C
		public DistributionListMember Add(Participant participant)
		{
			this.CheckDisposed("Add");
			if (participant == null)
			{
				throw new ArgumentNullException("participant");
			}
			this.EnsureExpanded();
			this.MarkMembersChanged();
			DistributionListMember distributionListMember = new DistributionListMember(this, participant);
			this.members.Add(distributionListMember);
			return distributionListMember;
		}

		// Token: 0x170010D5 RID: 4309
		public DistributionListMember this[RecipientId id]
		{
			get
			{
				this.CheckDisposed("this[RecipientBaseId]::get");
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000DBE7B File Offset: 0x000DA07B
		public void Remove(RecipientId id)
		{
			this.CheckDisposed("Remove(RecipientId)");
			throw new NotSupportedException();
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000DBE8D File Offset: 0x000DA08D
		public int IndexOf(DistributionListMember item)
		{
			this.CheckDisposed("IndexOf");
			this.EnsureExpanded();
			return this.members.IndexOf(item);
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000DBEAC File Offset: 0x000DA0AC
		public void Insert(int index, DistributionListMember item)
		{
			this.CheckDisposed("Insert");
			throw new NotSupportedException();
		}

		// Token: 0x170010D6 RID: 4310
		public DistributionListMember this[int index]
		{
			get
			{
				this.CheckDisposed("this[index]::get");
				this.EnsureExpanded();
				return this.members[index];
			}
			set
			{
				this.CheckDisposed("this[index]::set");
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000DBEEF File Offset: 0x000DA0EF
		public void RemoveAt(int index)
		{
			this.CheckDisposed("RemoveAt");
			this.EnsureExpanded();
			this.MarkMembersChanged();
			this.UpdateContactsRemoved(this.members[index]);
			this.members.RemoveAt(index);
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000DBF26 File Offset: 0x000DA126
		public void Add(DistributionListMember item)
		{
			this.CheckDisposed("Add(DistributionListMember)");
			this.EnsureExpanded();
			this.MarkMembersChanged();
			this.members.Add(DistributionListMember.CopyFrom(this, item));
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000DBF54 File Offset: 0x000DA154
		public void Clear()
		{
			this.CheckDisposed("Clear");
			this.MarkMembersChanged();
			this.EnsureExpanded();
			foreach (DistributionListMember distributionListMember in this.members)
			{
				if (!(distributionListMember.Participant == null))
				{
					this.UpdateContactsRemoved(distributionListMember);
				}
			}
			if (this.members != null)
			{
				this.members.Clear();
				return;
			}
			this.members = new List<DistributionListMember>();
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000DBFEC File Offset: 0x000DA1EC
		public bool Contains(DistributionListMember item)
		{
			this.CheckDisposed("Contains");
			this.EnsureExpanded();
			return this.members.Contains(item);
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000DC00B File Offset: 0x000DA20B
		public void CopyTo(DistributionListMember[] array, int arrayIndex)
		{
			this.CheckDisposed("CopyTo");
			this.EnsureExpanded();
			this.members.CopyTo(array, arrayIndex);
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000DC02B File Offset: 0x000DA22B
		public int Count
		{
			get
			{
				this.CheckDisposed("Count::get");
				this.EnsureExpanded();
				return this.members.Count;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000DC049 File Offset: 0x000DA249
		bool ICollection<DistributionListMember>.IsReadOnly
		{
			get
			{
				this.CheckDisposed("IsReadOnly::get");
				return base.IsReadOnly;
			}
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000DC05C File Offset: 0x000DA25C
		public bool Remove(IDistributionListMember item)
		{
			return this.Remove((DistributionListMember)item);
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000DC06A File Offset: 0x000DA26A
		public bool Remove(DistributionListMember item)
		{
			this.CheckDisposed("Remove");
			this.EnsureExpanded();
			this.MarkMembersChanged();
			this.UpdateContactsRemoved(item);
			return this.members.Remove(item);
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000DC096 File Offset: 0x000DA296
		public IEnumerator<DistributionListMember> GetEnumerator()
		{
			this.CheckDisposed("GetEnumerator");
			this.EnsureExpanded();
			return this.members.GetEnumerator();
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000DC0B9 File Offset: 0x000DA2B9
		public IEnumerator<IDistributionListMember> IGetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000DC0C1 File Offset: 0x000DA2C1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000DC0C9 File Offset: 0x000DA2C9
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x000DC0D1 File Offset: 0x000DA2D1
		internal bool AlwaysStream
		{
			get
			{
				return this.alwaysStream;
			}
			set
			{
				this.alwaysStream = value;
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000DC0DC File Offset: 0x000DA2DC
		internal bool ChecksumIsCurrent()
		{
			object obj = base.PropertyBag.TryGetProperty(DistributionListSchema.DLChecksum);
			return !(obj is PropertyError) && (int)obj == (int)this.computedChecksum;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000DC114 File Offset: 0x000DA314
		private static uint ComputeChecksum(byte[][] memberIds)
		{
			uint num = 0U;
			foreach (byte[] bytes in memberIds)
			{
				num = ComputeCRC.Compute(num, bytes);
			}
			return num;
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000DC140 File Offset: 0x000DA340
		private void MarkMembersChanged()
		{
			this.areMembersChanged = true;
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000DC14C File Offset: 0x000DA34C
		private IList<StoreObjectId> FindContacts()
		{
			List<StoreObjectId> list = new List<StoreObjectId>();
			foreach (DistributionListMember distributionListMember in this)
			{
				if (!(distributionListMember.Participant == null) && distributionListMember.IsDistributionList() != true)
				{
					StoreParticipantOrigin storeParticipantOrigin = distributionListMember.Participant.Origin as StoreParticipantOrigin;
					if (storeParticipantOrigin != null)
					{
						list.Add(storeParticipantOrigin.OriginItemId);
					}
				}
			}
			return list;
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000DC1E4 File Offset: 0x000DA3E4
		private void UpdateContactsRemoved(DistributionListMember member)
		{
			if (member.IsDistributionList() != true)
			{
				StoreParticipantOrigin storeParticipantOrigin = member.Participant.Origin as StoreParticipantOrigin;
				if (storeParticipantOrigin != null)
				{
					if (this.contactsRemoved == null)
					{
						this.contactsRemoved = new List<StoreObjectId>();
					}
					this.contactsRemoved.Add(storeParticipantOrigin.OriginItemId);
				}
			}
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000DC248 File Offset: 0x000DA448
		private void SetFakeEntryInLegacyPDL()
		{
			string text = "Unknown";
			ParticipantEntryId participantEntryId = new OneOffParticipantEntryId(ClientStrings.LegacyPDLFakeEntry, text, text);
			byte[][] value = DistributionList.EncodeEntryIds(new ParticipantEntryId[]
			{
				participantEntryId
			});
			this[DistributionListSchema.Members] = value;
			this[DistributionListSchema.OneOffMembers] = value;
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000DC297 File Offset: 0x000DA497
		private void GetEntryIds(out ParticipantEntryId[] mainEntryIds, out ParticipantEntryId[] oneOffEntryIds, out byte[][] extraBytes)
		{
			DistributionList.GetEntryIds(base.PropertyBag, out mainEntryIds, out oneOffEntryIds, out extraBytes, out this.computedChecksum, out this.alwaysStream);
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000DC2B4 File Offset: 0x000DA4B4
		internal static void GetEntryIds(ICorePropertyBag propertyBag, out ParticipantEntryId[] mainEntryIds, out ParticipantEntryId[] oneOffEntryIds, out byte[][] extraBytes, out uint computedCheckSum, out bool alwaysStream)
		{
			mainEntryIds = new ParticipantEntryId[0];
			oneOffEntryIds = new ParticipantEntryId[0];
			extraBytes = new byte[0][];
			computedCheckSum = 0U;
			alwaysStream = false;
			PropertyError propertyError = propertyBag.TryGetProperty(DistributionListSchema.DLStream) as PropertyError;
			if (propertyError == null || PropertyError.IsPropertyValueTooBig(propertyError))
			{
				long num = -1L;
				try
				{
					using (Stream stream = propertyBag.OpenPropertyStream(DistributionListSchema.DLStream, PropertyOpenMode.ReadOnly))
					{
						if (stream != null && stream.Length > 0L)
						{
							num = stream.Length;
							byte[][] memberIds;
							DistributionList.ParseEntryIdStream(stream, out mainEntryIds, out oneOffEntryIds, out memberIds, out extraBytes, out alwaysStream);
							computedCheckSum = DistributionList.ComputeChecksum(memberIds);
							return;
						}
						ExTraceGlobals.StorageTracer.TraceWarning<string>(0L, "DistributionList::GetEntryIds. DLStream property is {0}.", (stream == null) ? "null" : "empty");
					}
				}
				catch (EndOfStreamException innerException)
				{
					string arg = (propertyError == null) ? "<null>" : propertyError.ToLocalizedString();
					LocalizedString message = ServerStrings.ExPDLCorruptOutlookBlob(string.Format("EndOfStreamException: propertyError={0}, streamLength={1}", arg, num.ToString()));
					throw new CorruptDataException(message, innerException);
				}
				catch (OutOfMemoryException innerException2)
				{
					throw new CorruptDataException(ServerStrings.ExPDLCorruptOutlookBlob("OutOfMemoryException"), innerException2);
				}
			}
			mainEntryIds = DistributionList.ParseEntryIds(propertyBag.GetValueOrDefault<byte[][]>(DistributionListSchema.Members, DistributionList.EmptyEntryIds));
			oneOffEntryIds = DistributionList.ParseEntryIds(propertyBag.GetValueOrDefault<byte[][]>(DistributionListSchema.OneOffMembers, DistributionList.EmptyEntryIds));
			extraBytes = new byte[mainEntryIds.Length][];
			computedCheckSum = DistributionList.ComputeChecksum(propertyBag.GetValueOrDefault<byte[][]>(DistributionListSchema.Members, DistributionList.EmptyEntryIds));
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000DC438 File Offset: 0x000DA638
		private void EnsureExpanded()
		{
			if (this.members != null)
			{
				return;
			}
			ParticipantEntryId[] array;
			ParticipantEntryId[] array2;
			byte[][] array3;
			this.GetEntryIds(out array, out array2, out array3);
			this.PreprocessEntryIds(array, ref array2);
			this.members = new List<DistributionListMember>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.members.Add(new DistributionListMember(this, array[i], (OneOffParticipantEntryId)array2[i], array3[i]));
			}
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000DC49E File Offset: 0x000DA69E
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.members = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000DC4B4 File Offset: 0x000DA6B4
		private void Initialize()
		{
			base.Load(new PropertyDefinition[]
			{
				InternalSchema.ItemClass
			});
			string text = base.TryGetProperty(InternalSchema.ItemClass) as string;
			if (!ObjectClass.IsDistributionList(text))
			{
				if (!string.IsNullOrEmpty(text))
				{
					ExTraceGlobals.StorageTracer.TraceWarning<string, string>(0L, "DistributionList::Initialize. Overwriting ItemClass from \"{0}\" to \"{1}\".", text, "IPM.DistList");
				}
				this[InternalSchema.ItemClass] = "IPM.DistList";
			}
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000DC530 File Offset: 0x000DA730
		private void NormalizeADEntryIds(ParticipantEntryId[] mainEntryIds, ParticipantEntryId[] oneOffEntryIds)
		{
			List<string> list = new List<string>();
			List<int> list2 = new List<int>();
			for (int i = 0; i < mainEntryIds.Length; i++)
			{
				ADParticipantEntryId adparticipantEntryId = mainEntryIds[i] as ADParticipantEntryId;
				OneOffParticipantEntryId oneOffParticipantEntryId = (OneOffParticipantEntryId)oneOffEntryIds[i];
				if (adparticipantEntryId != null && (oneOffParticipantEntryId == null || adparticipantEntryId.IsDL == null))
				{
					list.Add(adparticipantEntryId.LegacyDN);
					list2.Add(i);
				}
				else if (oneOffParticipantEntryId != null && oneOffParticipantEntryId.EmailAddressType == "EX")
				{
					list.Add(oneOffParticipantEntryId.EmailAddress);
					list2.Add(i);
				}
			}
			if (list.Count > 0)
			{
				object[][] array = DistributionList.LookupInAD(() => base.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid), list, DistributionList.adLookupProperties);
				for (int j = 0; j < list.Count; j++)
				{
					if (array[j] != null)
					{
						int num = list2[j];
						if (mainEntryIds[num] is ADParticipantEntryId && mainEntryIds[num].IsDL == null)
						{
							bool flag = DistributionList.IsDL((RecipientType)array[j][2]);
							mainEntryIds[num] = new ADParticipantEntryId(list[j], new LegacyRecipientDisplayType?(flag ? LegacyRecipientDisplayType.DistributionList : LegacyRecipientDisplayType.MailUser), true);
						}
						OneOffParticipantEntryId oneOffParticipantEntryId2 = oneOffEntryIds[num] as OneOffParticipantEntryId;
						oneOffEntryIds[num] = new OneOffParticipantEntryId((oneOffParticipantEntryId2 != null) ? oneOffParticipantEntryId2.EmailDisplayName : ((string)array[j][0]), array[j][1].ToString(), "SMTP");
					}
				}
			}
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000DC6B0 File Offset: 0x000DA8B0
		protected override void OnAfterSave(ConflictResolutionResult acrResults)
		{
			base.OnAfterSave(acrResults);
			if (acrResults.SaveStatus != SaveResult.IrresolvableConflict)
			{
				this.members = null;
			}
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000DC6CC File Offset: 0x000DA8CC
		private void SetEntryIds(ParticipantEntryId[] mainEntryIds, ParticipantEntryId[] oneOffEntryIds, byte[][] extraBytes)
		{
			bool flag = this.alwaysStream;
			byte[][] array = DistributionList.EncodeEntryIds(mainEntryIds);
			byte[][] value = DistributionList.EncodeEntryIds(oneOffEntryIds);
			this.computedChecksum = DistributionList.ComputeChecksum(array);
			if (!flag)
			{
				flag = DistributionList.NeedToStream(DistributionListSchema.Members, array);
			}
			if (!flag)
			{
				flag = DistributionList.NeedToStream(DistributionListSchema.OneOffMembers, value);
			}
			if (flag)
			{
				using (Stream stream = base.OpenPropertyStream(DistributionListSchema.DLStream, PropertyOpenMode.Create))
				{
					DistributionList.SerializeEntryIdsOnStream(this.alwaysStream, stream, mainEntryIds, oneOffEntryIds, extraBytes);
					this.SetFakeEntryInLegacyPDL();
					goto IL_93;
				}
			}
			base.Delete(DistributionListSchema.DLStream);
			this[DistributionListSchema.Members] = array;
			this[DistributionListSchema.OneOffMembers] = value;
			IL_93:
			this[DistributionListSchema.DLChecksum] = (int)this.computedChecksum;
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000DC794 File Offset: 0x000DA994
		private void DeleteEntryIds()
		{
			base.Delete(DistributionListSchema.DLStream);
			base.Delete(DistributionListSchema.Members);
			base.Delete(DistributionListSchema.OneOffMembers);
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000DC7B7 File Offset: 0x000DA9B7
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			this.OnBeforeSaveUpdateOneOffIds();
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000DC7C8 File Offset: 0x000DA9C8
		private void OnBeforeSaveUpdateOneOffIds()
		{
			if (this.areMembersChanged)
			{
				if (this.members != null && this.members.Count > 0)
				{
					ParticipantEntryId[] mainEntryIds;
					ParticipantEntryId[] oneOffEntryIds;
					byte[][] extraBytes;
					this.ProcessAndReturnEntryIds(out mainEntryIds, out oneOffEntryIds, out extraBytes);
					this.SetEntryIds(mainEntryIds, oneOffEntryIds, extraBytes);
					this.areMembersChanged = false;
					return;
				}
				this.DeleteEntryIds();
			}
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000DC818 File Offset: 0x000DAA18
		internal void ProcessAndReturnEntryIds(out ParticipantEntryId[] mainEntryIds, out ParticipantEntryId[] oneOffEntryIds, out byte[][] extraBytes)
		{
			mainEntryIds = new ParticipantEntryId[this.members.Count];
			oneOffEntryIds = new ParticipantEntryId[this.members.Count];
			extraBytes = new byte[this.members.Count][];
			for (int i = 0; i < this.members.Count; i++)
			{
				mainEntryIds[i] = this.members[i].MainEntryId;
				oneOffEntryIds[i] = this.members[i].OneOffEntryId;
				extraBytes[i] = this.members[i].ExtraBytes;
			}
			this.PostprocessEntryIds(mainEntryIds, oneOffEntryIds);
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000DC8BA File Offset: 0x000DAABA
		private void PostprocessEntryIds(ParticipantEntryId[] mainEntryIds, ParticipantEntryId[] oneOffEntryIds)
		{
			this.NormalizeADEntryIds(mainEntryIds, oneOffEntryIds);
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000DC8C4 File Offset: 0x000DAAC4
		private void PreprocessEntryIds(ParticipantEntryId[] mainEntryIds, ref ParticipantEntryId[] oneOffEntryIds)
		{
			OneOffParticipantEntryId[] array = new OneOffParticipantEntryId[mainEntryIds.Length];
			if (!this.ChecksumIsCurrent())
			{
				int i = 0;
				while (i < mainEntryIds.Length)
				{
					StoreParticipantEntryId storeParticipantEntryId = mainEntryIds[i] as StoreParticipantEntryId;
					OneOffParticipantEntryId oneOffParticipantEntryId = mainEntryIds[i] as OneOffParticipantEntryId;
					if (storeParticipantEntryId != null)
					{
						try
						{
							using (Item item = Microsoft.Exchange.Data.Storage.Item.Bind(base.Session, storeParticipantEntryId.ToUniqueItemId(), ContactSchema.Instance.AutoloadProperties))
							{
								Contact contact = item as Contact;
								DistributionList distributionList = item as DistributionList;
								if (contact != null)
								{
									Participant participant = contact.EmailAddresses[storeParticipantEntryId.EmailAddressIndex];
									if (participant != null)
									{
										Participant participant2 = new Participant(contact.DisplayName, participant.EmailAddress, participant.RoutingType, participant.Origin, Array<PropValue>.Empty);
										array[i] = (OneOffParticipantEntryId)ParticipantEntryId.FromParticipant(participant2, ParticipantEntryIdConsumer.SupportsNone);
									}
								}
								else if (distributionList != null)
								{
									array[i] = (OneOffParticipantEntryId)ParticipantEntryId.FromParticipant(distributionList.GetAsParticipant(), ParticipantEntryIdConsumer.SupportsNone);
								}
							}
							goto IL_F1;
						}
						catch (ObjectNotFoundException)
						{
							goto IL_F1;
						}
						goto IL_EA;
					}
					goto IL_EA;
					IL_F1:
					i++;
					continue;
					IL_EA:
					if (oneOffParticipantEntryId != null)
					{
						array[i] = oneOffParticipantEntryId;
						goto IL_F1;
					}
					goto IL_F1;
				}
			}
			else
			{
				for (int j = 0; j < mainEntryIds.Length; j++)
				{
					array[j] = (oneOffEntryIds[j] as OneOffParticipantEntryId);
				}
			}
			OneOffParticipantEntryId[] array2 = (OneOffParticipantEntryId[])array.Clone();
			this.NormalizeADEntryIds(mainEntryIds, array2);
			bool flag = oneOffEntryIds.Length == array.Length;
			int num = 0;
			while (flag && num < oneOffEntryIds.Length)
			{
				OneOffParticipantEntryId oneOffParticipantEntryId2 = oneOffEntryIds[num] as OneOffParticipantEntryId;
				if (oneOffParticipantEntryId2 == null)
				{
					flag = false;
				}
				else if (array2[num] != null)
				{
					flag = ParticipantComparer.EmailAddressIgnoringRoutingType.Equals(array2[num].ToParticipant(), oneOffParticipantEntryId2.ToParticipant());
				}
				num++;
			}
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k] == null)
				{
					if (array2[k] != null)
					{
						array[k] = array2[k];
					}
					else if (flag)
					{
						array[k] = (OneOffParticipantEntryId)oneOffEntryIds[k];
					}
				}
			}
			oneOffEntryIds = array;
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000DCAE8 File Offset: 0x000DACE8
		private static object[][] LookupInAD(Func<IRecipientSession> adRecipientSessionFactory, List<string> legacyDNs, params PropertyDefinition[] properties)
		{
			object[][] result;
			try
			{
				Result<ADRawEntry>[] array = adRecipientSessionFactory().FindByLegacyExchangeDNs(legacyDNs.ToArray(), properties);
				if (array.Length != legacyDNs.Count)
				{
					ExDiagnostics.FailFast(string.Format(CultureInfo.InvariantCulture, "Number of results in IRecipientSession.FindByLegacyExchangeDNs() is unexpected: {0} instead of {1}", new object[]
					{
						array.Length,
						legacyDNs.Count
					}), false);
				}
				result = Array.ConvertAll<Result<ADRawEntry>, object[]>(array, delegate(Result<ADRawEntry> entry)
				{
					if (entry.Data == null)
					{
						return null;
					}
					return entry.Data.GetProperties(properties);
				});
			}
			catch (DataSourceOperationException ex)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "DistributionList.LookupInAD. Failed due to directory exception {0}.", new object[]
				{
					ex
				});
			}
			catch (DataSourceTransientException ex2)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "DistributionList.LookupInAD. Failed due to directory exception {0}.", new object[]
				{
					ex2
				});
			}
			return result;
		}

		// Token: 0x04001D21 RID: 7457
		private const ushort StreamBlobVersion = 1;

		// Token: 0x04001D22 RID: 7458
		private const int StreamBlobBuildVersion = 14000;

		// Token: 0x04001D23 RID: 7459
		private const int StreamBlobFlags = 0;

		// Token: 0x04001D24 RID: 7460
		private const int AlwaysStreamFlag = 1;

		// Token: 0x04001D25 RID: 7461
		private static readonly byte[][] EmptyEntryIds = Array<byte[]>.Empty;

		// Token: 0x04001D26 RID: 7462
		private bool areMembersChanged;

		// Token: 0x04001D27 RID: 7463
		private List<DistributionListMember> members;

		// Token: 0x04001D28 RID: 7464
		private bool alwaysStream;

		// Token: 0x04001D29 RID: 7465
		private List<StoreObjectId> contactsRemoved;

		// Token: 0x04001D2A RID: 7466
		private uint computedChecksum;

		// Token: 0x04001D2B RID: 7467
		private static PropertyDefinition[] adLookupProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.RecipientType
		};
	}
}
