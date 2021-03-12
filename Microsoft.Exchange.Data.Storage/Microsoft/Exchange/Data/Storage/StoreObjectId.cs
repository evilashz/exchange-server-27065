using System;
using System.IO;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003EC RID: 1004
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class StoreObjectId : StoreId, IEquatable<StoreObjectId>, IComparable<StoreObjectId>, IComparable
	{
		// Token: 0x06002DCA RID: 11722 RVA: 0x000BC9C0 File Offset: 0x000BABC0
		protected StoreObjectId(byte[] entryId, StoreObjectType itemType)
		{
			Util.ThrowOnNullArgument(entryId, "entryId");
			EnumValidator.AssertValid<StoreObjectType>(itemType);
			if (entryId.Length > 255)
			{
				throw new CorruptDataException(ServerStrings.ExEntryIdToLong);
			}
			this.EntryId = entryId;
			this.SetObjectType(itemType);
			this.Validate();
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000BCA10 File Offset: 0x000BAC10
		protected StoreObjectId(byte[] byteArray, int startingIndex)
		{
			Util.ThrowOnNullArgument(byteArray, "byteArray");
			if (byteArray.Length <= startingIndex + 1)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidIdFormat);
			}
			int num = (int)byteArray[startingIndex];
			StoreObjectId.CheckDataFormat(startingIndex, byteArray.Length, num);
			this.EntryId = new byte[num];
			Array.Copy(byteArray, 1 + startingIndex, this.EntryId, 0, num);
			this.SetObjectType((StoreObjectType)byteArray[1 + num + startingIndex]);
			this.Validate();
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000BCA81 File Offset: 0x000BAC81
		private static void CheckDataFormat(int startingIndex, int byteArrayLength, int countEntryIdBytes)
		{
			if (countEntryIdBytes <= 0 || byteArrayLength != countEntryIdBytes + 1 + 1 + startingIndex)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidIdFormat);
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x000BCA9C File Offset: 0x000BAC9C
		public static StoreObjectId DummyId
		{
			get
			{
				return StoreObjectId.dummyId;
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000BCAA3 File Offset: 0x000BACA3
		public StoreObjectType ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x000BCAAB File Offset: 0x000BACAB
		public byte[] ProviderLevelItemId
		{
			get
			{
				return (byte[])this.EntryId.Clone();
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000BCAC0 File Offset: 0x000BACC0
		public byte[] LongTermFolderId
		{
			get
			{
				if (this.IsFolderId)
				{
					byte[] array = new byte[22];
					Array.Copy(this.EntryId, 22, array, 0, 22);
					return array;
				}
				return null;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x000BCAF1 File Offset: 0x000BACF1
		public bool IsFakeId
		{
			get
			{
				return this.EntryId.Length == 0;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000BCAFE File Offset: 0x000BACFE
		public bool IsFolderId
		{
			get
			{
				return IdConverter.IsFolderId(this.EntryId);
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x000BCB0B File Offset: 0x000BAD0B
		internal bool IsMessageId
		{
			get
			{
				return this.IsFakeId || IdConverter.IsMessageId(this.EntryId);
			}
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000BCB24 File Offset: 0x000BAD24
		public static StoreObjectId Deserialize(string base64Id)
		{
			if (base64Id == null)
			{
				throw new ArgumentNullException("base64Id");
			}
			byte[] byteArray = StoreId.Base64ToByteArray(base64Id);
			return StoreObjectId.Deserialize(byteArray);
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000BCB4E File Offset: 0x000BAD4E
		public static StoreObjectId Deserialize(byte[] byteArray)
		{
			if (byteArray == null)
			{
				throw new ArgumentNullException("byteArray");
			}
			return StoreObjectId.Parse(byteArray, 0);
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000BCB65 File Offset: 0x000BAD65
		public static StoreObjectId Deserialize(BinaryReader reader, int byteArrayLength)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return StoreObjectId.Parse(reader, 0, byteArrayLength);
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000BCB7D File Offset: 0x000BAD7D
		public static void Serialize(StoreObjectId storeObjectId, BinaryWriter writer)
		{
			storeObjectId.WriteBytes(writer);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000BCB86 File Offset: 0x000BAD86
		public static StoreObjectId FromProviderSpecificId(byte[] entryId)
		{
			return StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Unknown);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000BCB8F File Offset: 0x000BAD8F
		public static StoreObjectId FromProviderSpecificIdOrNull(byte[] entryId)
		{
			if (entryId != null)
			{
				return StoreObjectId.FromProviderSpecificId(entryId);
			}
			return null;
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000BCB9C File Offset: 0x000BAD9C
		public static bool TryParseFromHexEntryId(string hexEntryId, out StoreObjectId storeObjectId)
		{
			storeObjectId = null;
			try
			{
				if (!string.IsNullOrWhiteSpace(hexEntryId))
				{
					storeObjectId = StoreObjectId.FromHexEntryId(hexEntryId);
					return true;
				}
			}
			catch (FormatException)
			{
			}
			catch (CorruptDataException)
			{
			}
			return false;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000BCBE8 File Offset: 0x000BADE8
		public static StoreObjectId FromHexEntryId(string hexEntryId)
		{
			return StoreObjectId.FromHexEntryId(hexEntryId, StoreObjectType.Unknown);
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000BCBF1 File Offset: 0x000BADF1
		public static StoreObjectId FromHexEntryId(string hexEntryId, StoreObjectType storeObjectType)
		{
			Util.ThrowOnNullArgument(hexEntryId, "hexEntryId");
			EnumValidator.ThrowIfInvalid<StoreObjectType>(storeObjectType);
			if (storeObjectType == StoreObjectType.CalendarItemOccurrence)
			{
				throw new ArgumentException("StoreObjectId shouldn't be created for occurrences.", "storeObjectType");
			}
			return new StoreObjectId(HexConverter.HexStringToByteArray(hexEntryId), storeObjectType);
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000BCC25 File Offset: 0x000BAE25
		public static StoreObjectId FromProviderSpecificId(byte[] entryId, StoreObjectType objectType)
		{
			if (entryId == null)
			{
				throw new ArgumentNullException("entryId");
			}
			EnumValidator.ThrowIfInvalid<StoreObjectType>(objectType);
			if (objectType == StoreObjectType.CalendarItemOccurrence)
			{
				throw new ArgumentException("StoreObjectId shouldn't be created for occurrences.", "objectType");
			}
			return new StoreObjectId(entryId, objectType);
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000BCC58 File Offset: 0x000BAE58
		public static StoreObjectId FromLegacyFavoritePublicFolderId(StoreObjectId other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (!other.IsPublicFolderType())
			{
				throw new ArgumentException("other StoreObjectId is not a public folder type: " + other.GetFolderType());
			}
			if (!StoreObjectId.IsValidFlagsForStoreObjectId(other))
			{
				return StoreObjectId.ToNormalizedPublicFolderId(other);
			}
			StoreObjectId result = other;
			byte[] providerLevelItemId = other.ProviderLevelItemId;
			if (providerLevelItemId[21] == 128)
			{
				providerLevelItemId[21] = 0;
				result = StoreObjectId.FromProviderSpecificId(providerLevelItemId);
			}
			return result;
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000BCCC8 File Offset: 0x000BAEC8
		public static StoreObjectId ToLegacyFavoritePublicFolderId(StoreObjectId other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (!other.IsPublicFolderType())
			{
				throw new ArgumentException("other StoreObjectId is not a legacy public folder type: " + other.GetFolderType());
			}
			byte[] providerLevelItemId = other.ProviderLevelItemId;
			providerLevelItemId[21] = 128;
			return StoreObjectId.FromProviderSpecificId(providerLevelItemId);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000BCD1E File Offset: 0x000BAF1E
		public static bool IsValidFlagsForStoreObjectId(StoreObjectId folderId)
		{
			ArgumentValidator.ThrowIfNull("sourceFolderId", folderId);
			return folderId.EntryId[0] == 0 && folderId.EntryId[1] == 0 && folderId.EntryId[2] == 0 && folderId.EntryId[3] == 0;
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000BCD58 File Offset: 0x000BAF58
		public static StoreObjectId ToNormalizedPublicFolderId(StoreObjectId sourceFolderId)
		{
			ArgumentValidator.ThrowIfNull("sourceFolderId", sourceFolderId);
			if (!sourceFolderId.IsFolderId)
			{
				throw new ArgumentException("sourceFolderId is not a folder id");
			}
			byte[] array = new byte[sourceFolderId.EntryId.Length];
			Array.Copy(StoreObjectId.PublicFolderProviderUIDInBytes, 0, array, 4, 16);
			Array.Copy(StoreObjectId.LegacyPublicFolderTypeInBytes, 0, array, 20, StoreObjectId.LegacyPublicFolderTypeInBytes.Length);
			Array.Copy(sourceFolderId.EntryId, 22, array, 22, 22);
			return StoreObjectId.FromProviderSpecificId(array, sourceFolderId.ObjectType);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000BCDD5 File Offset: 0x000BAFD5
		public bool IsLegacyPublicFolderType()
		{
			return this.GetFolderType() == 3;
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000BCDE0 File Offset: 0x000BAFE0
		public bool IsPublicFolderType()
		{
			return this.IsLegacyPublicFolderType() || this.GetFolderType() == 2;
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000BCDF5 File Offset: 0x000BAFF5
		public bool IsNormalizedPublicFolderId()
		{
			return this.IsFolderId && this.IsPublicFolderType() && Util.CompareByteArraySegments(this.EntryId, 4U, StoreObjectId.PublicFolderProviderUIDInBytes, 0U, 16U);
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000BCE1F File Offset: 0x000BB01F
		public override string ToBase64String()
		{
			return Convert.ToBase64String(this.GetBytes());
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000BCE2C File Offset: 0x000BB02C
		public virtual int GetByteArrayLength()
		{
			return this.InternalGetByteArrayLength();
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000BCE34 File Offset: 0x000BB034
		public override byte[] GetBytes()
		{
			byte[] array = new byte[this.InternalGetByteArrayLength()];
			array[0] = (byte)this.EntryId.Length;
			this.EntryId.CopyTo(array, 1);
			array[1 + this.EntryId.Length] = (byte)this.objectType;
			return array;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000BCE7A File Offset: 0x000BB07A
		protected virtual void WriteBytes(BinaryWriter writer)
		{
			writer.Write((byte)this.EntryId.Length);
			writer.Write(this.EntryId);
			writer.Write((byte)this.objectType);
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000BCEA4 File Offset: 0x000BB0A4
		public string ToHexEntryId()
		{
			return HexConverter.ByteArrayToHexString(this.EntryId);
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000BCEB1 File Offset: 0x000BB0B1
		public virtual StoreObjectId Clone()
		{
			return new StoreObjectId(this.EntryId, this.objectType);
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000BCEC4 File Offset: 0x000BB0C4
		public virtual void UpdateItemType(StoreObjectType newItemType)
		{
			EnumValidator.ThrowIfInvalid<StoreObjectType>(newItemType, "newItemType");
			if (newItemType == StoreObjectType.CalendarItemOccurrence)
			{
				throw new ArgumentException("StoreObjectId shouldn't be created for occurrences.", "newItemType");
			}
			this.objectType = newItemType;
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000BCEF0 File Offset: 0x000BB0F0
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				int num = 0;
				int num2 = this.EntryId.Length;
				int i = 0;
				while (i + 3 < num2)
				{
					num ^= ((int)this.EntryId[i] | (int)this.EntryId[i + 1] << 8 | (int)this.EntryId[i + 2] << 16 | (int)this.EntryId[i + 3] << 24);
					i += 4;
				}
				while (i < num2)
				{
					num ^= (int)this.EntryId[i] << 8 * (i & 3);
					i++;
				}
				this.hashCode = num;
			}
			return this.hashCode;
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000BCF7D File Offset: 0x000BB17D
		public override string ToString()
		{
			return this.ToBase64String();
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000BCF88 File Offset: 0x000BB188
		public override bool Equals(object id)
		{
			StoreObjectId id2 = id as StoreObjectId;
			return this.Equals(id2);
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000BCFA4 File Offset: 0x000BB1A4
		public override bool Equals(StoreId id)
		{
			StoreObjectId id2 = id as StoreObjectId;
			return this.Equals(id2);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000BCFC0 File Offset: 0x000BB1C0
		public virtual bool Equals(StoreObjectId id)
		{
			if (id == null || !base.GetType().Equals(id.GetType()))
			{
				return false;
			}
			if (this.IsNormalizedPublicFolderId() || id.IsNormalizedPublicFolderId())
			{
				return Util.CompareByteArraySegments(this.EntryId, 22U, id.EntryId, 22U, 22U);
			}
			return ArrayComparer<byte>.Comparer.Equals(this.EntryId, id.EntryId);
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000BD023 File Offset: 0x000BB223
		public virtual int CompareTo(object o)
		{
			if (o == null)
			{
				return 1;
			}
			if (!base.GetType().Equals(o.GetType()))
			{
				throw new ArgumentException();
			}
			return this.CompareTo(o as StoreObjectId);
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000BD04F File Offset: 0x000BB24F
		public int CompareTo(StoreObjectId o)
		{
			if (o == null)
			{
				return 1;
			}
			return ArrayComparer<byte>.Comparer.Compare(this.EntryId, o.EntryId);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000BD06C File Offset: 0x000BB26C
		internal string ToBase64ProviderLevelItemId()
		{
			string result = null;
			byte[] providerLevelItemId = this.ProviderLevelItemId;
			if (providerLevelItemId != null)
			{
				result = Convert.ToBase64String(providerLevelItemId);
			}
			return result;
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000BD090 File Offset: 0x000BB290
		internal static bool StoreEntryIdsAreForSamePrivateStore(byte[] storeEntryId1, byte[] storeEntryId2)
		{
			StoreObjectId.StoreEntryId storeEntryId3 = StoreObjectId.ParseStoreEntryId(storeEntryId1);
			StoreObjectId.StoreEntryId storeEntryId4 = StoreObjectId.ParseStoreEntryId(storeEntryId2);
			if (storeEntryId3.ServerName.Contains(".") && !storeEntryId4.ServerName.Contains("."))
			{
				storeEntryId3.ServerName = storeEntryId3.ServerName.Split(new char[]
				{
					'.'
				})[0];
			}
			else if (storeEntryId4.ServerName.Contains(".") && !storeEntryId3.ServerName.Contains("."))
			{
				storeEntryId4.ServerName = storeEntryId4.ServerName.Split(new char[]
				{
					'.'
				})[0];
			}
			return (storeEntryId3.StoreFlags & OpenStoreFlag.Public) != OpenStoreFlag.Public && (storeEntryId4.StoreFlags & OpenStoreFlag.Public) != OpenStoreFlag.Public && storeEntryId3.ServerName.Equals(storeEntryId4.ServerName, StringComparison.OrdinalIgnoreCase) && storeEntryId3.LegacyDn.Equals(storeEntryId4.LegacyDn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000BD188 File Offset: 0x000BB388
		internal static StoreObjectId Parse(byte[] byteArray, int startingIndex)
		{
			if (byteArray == null)
			{
				throw new ArgumentNullException("byteArray");
			}
			if (byteArray.Length <= 1 + startingIndex)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidIdFormat);
			}
			if (byteArray[byteArray.Length - 1] == 16)
			{
				return new OccurrenceStoreObjectId(byteArray, startingIndex);
			}
			return new StoreObjectId(byteArray, startingIndex);
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000BD1C8 File Offset: 0x000BB3C8
		internal static StoreObjectId Parse(BinaryReader reader, int startingIndex, int byteArrayLength)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (byteArrayLength <= 1 + startingIndex)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidIdFormat);
			}
			if (startingIndex > 0)
			{
				reader.ReadBytes(startingIndex);
			}
			byte b = reader.ReadByte();
			byte[] array = reader.ReadBytes((int)b);
			int num = byteArrayLength - (startingIndex + 1 + (int)b);
			if (num < 1)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidIdFormat);
			}
			byte[] array2 = reader.ReadBytes(num);
			StoreObjectType storeObjectType = (StoreObjectType)array2[num - 1];
			if (storeObjectType == StoreObjectType.CalendarItemOccurrence)
			{
				return new OccurrenceStoreObjectId(b, array, array2);
			}
			StoreObjectId.CheckDataFormat(startingIndex, byteArrayLength, (int)b);
			return new StoreObjectId(array, storeObjectType);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000BD254 File Offset: 0x000BB454
		private static StoreObjectId.StoreEntryId ParseStoreEntryId(byte[] entryId)
		{
			StoreObjectId.StoreEntryId result;
			using (ParticipantEntryId.Reader reader = new ParticipantEntryId.Reader(entryId))
			{
				try
				{
					bool flag = false;
					reader.BaseStream.Seek(22L, SeekOrigin.Begin);
					if (reader.BytesRemaining > 0)
					{
						string text = reader.ReadZString(CTSGlobals.AsciiEncoding);
						if (text.Equals("emsmdb.dll", StringComparison.OrdinalIgnoreCase))
						{
							flag = true;
						}
					}
					if (flag)
					{
						reader.BaseStream.Seek(36L, SeekOrigin.Begin);
					}
					else
					{
						reader.BaseStream.Seek(0L, SeekOrigin.Begin);
					}
					result.MapiFlags = reader.ReadInt32();
					result.MapiUid = reader.ReadGuid();
					result.StoreFlags = (OpenStoreFlag)reader.ReadInt32();
					result.ServerName = reader.ReadZString(CTSGlobals.AsciiEncoding);
					if ((result.StoreFlags & OpenStoreFlag.Public) != OpenStoreFlag.Public)
					{
						result.LegacyDn = reader.ReadZString(CTSGlobals.AsciiEncoding);
					}
					else
					{
						result.LegacyDn = string.Empty;
					}
				}
				catch (EndOfStreamException innerException)
				{
					throw new CorruptDataException(ServerStrings.ExInvalidIdFormat, innerException);
				}
			}
			return result;
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000BD368 File Offset: 0x000BB568
		private void SetObjectType(StoreObjectType objectType)
		{
			this.objectType = objectType;
			if (!EnumValidator.IsValidValue<StoreObjectType>(this.objectType))
			{
				this.objectType = StoreObjectType.Unknown;
			}
			if (this.objectType == StoreObjectType.Unknown && this.IsFolderId)
			{
				this.objectType = StoreObjectType.Folder;
				return;
			}
			bool flag = StoreObjectTypeClassifier.IsFolderObjectType(this.objectType);
			if (flag && this.IsMessageId)
			{
				this.objectType = StoreObjectType.Message;
				return;
			}
			if (!flag && this.IsFolderId)
			{
				this.objectType = StoreObjectType.Folder;
				return;
			}
			if (!StoreObjectTypeExclusions.E12KnownObjectType(this.objectType))
			{
				if (this.IsFolderId)
				{
					this.objectType = StoreObjectType.Folder;
					return;
				}
				if (!StoreObjectTypeClassifier.AlwaysReportRealType(this.objectType))
				{
					this.objectType = StoreObjectType.Message;
				}
			}
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000BD40D File Offset: 0x000BB60D
		private void Validate()
		{
			if (this.objectType != StoreObjectType.Mailbox && !this.IsMessageId && !this.IsFolderId)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidStoreObjectId);
			}
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000BD437 File Offset: 0x000BB637
		private int InternalGetByteArrayLength()
		{
			return 1 + this.EntryId.Length + 1;
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000BD445 File Offset: 0x000BB645
		private byte GetFolderType()
		{
			return this.ProviderLevelItemId[20];
		}

		// Token: 0x0400190C RID: 6412
		private const int WrappedIdDllNameIndex = 22;

		// Token: 0x0400190D RID: 6413
		private const int WrappedIdEntryIdIndex = 36;

		// Token: 0x0400190E RID: 6414
		private const int FolderTypeIndex = 20;

		// Token: 0x0400190F RID: 6415
		private const int LegacyPublicFolderType = 3;

		// Token: 0x04001910 RID: 6416
		private const int AlternateLegacyPublicFolderType = 2;

		// Token: 0x04001911 RID: 6417
		private const int ProviderIdIndexInFolderEntryId = 4;

		// Token: 0x04001912 RID: 6418
		private const int ProviderIdLengthInFolderEntryId = 16;

		// Token: 0x04001913 RID: 6419
		private const int PublicFolderFavoriteFlagIndex = 21;

		// Token: 0x04001914 RID: 6420
		private const int PublicFolderFavoriteFlag = 128;

		// Token: 0x04001915 RID: 6421
		private const int NonPublicFolderFavoriteFlag = 0;

		// Token: 0x04001916 RID: 6422
		private const int LongTermFolderIdIndex = 22;

		// Token: 0x04001917 RID: 6423
		public const int LongTermFolderIdLength = 22;

		// Token: 0x04001918 RID: 6424
		protected readonly byte[] EntryId;

		// Token: 0x04001919 RID: 6425
		private static readonly StoreObjectId dummyId = StoreObjectId.FromProviderSpecificId(Array<byte>.Empty);

		// Token: 0x0400191A RID: 6426
		private static readonly byte[] LegacyPublicFolderTypeInBytes = BitConverter.GetBytes(3);

		// Token: 0x0400191B RID: 6427
		private StoreObjectType objectType;

		// Token: 0x0400191C RID: 6428
		[NonSerialized]
		private int hashCode;

		// Token: 0x0400191D RID: 6429
		public static readonly Guid PublicFolderProviderUID = Guid.Parse("9073441A-66AA-CD11-9BC8-00AA002FC45A");

		// Token: 0x0400191E RID: 6430
		public static readonly byte[] PublicFolderProviderUIDInBytes = StoreObjectId.PublicFolderProviderUID.ToByteArray();

		// Token: 0x020003ED RID: 1005
		internal struct StoreEntryId
		{
			// Token: 0x0400191F RID: 6431
			public int MapiFlags;

			// Token: 0x04001920 RID: 6432
			public Guid MapiUid;

			// Token: 0x04001921 RID: 6433
			public OpenStoreFlag StoreFlags;

			// Token: 0x04001922 RID: 6434
			public string ServerName;

			// Token: 0x04001923 RID: 6435
			public string LegacyDn;
		}
	}
}
