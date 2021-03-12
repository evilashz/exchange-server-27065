using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000855 RID: 2133
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class VersionedId : StoreId, IEquatable<VersionedId>, IComparable<VersionedId>, IComparable
	{
		// Token: 0x06004FF8 RID: 20472 RVA: 0x0014D234 File Offset: 0x0014B434
		public VersionedId(StoreObjectId itemId, byte[] changeKey)
		{
			if (changeKey.Length > 255)
			{
				throw new CorruptDataException(ServerStrings.ExChangeKeyTooLong);
			}
			if (itemId == null)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			this.ItemId = itemId;
			this.ChangeKey = (byte[])changeKey.Clone();
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0014D284 File Offset: 0x0014B484
		public VersionedId(byte[] byteArray)
		{
			int num = 0;
			bool flag = false;
			if (byteArray == null)
			{
				throw new ArgumentNullException("byteArray", ServerStrings.ExInvalidIdFormat);
			}
			if (byteArray.Length <= 1)
			{
				flag = true;
			}
			else
			{
				num = (int)byteArray[0];
				if (byteArray.Length <= num)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.ItemId = StoreObjectId.Parse(byteArray, num + 1);
				this.ChangeKey = new byte[num];
				for (int i = 0; i < num; i++)
				{
					this.ChangeKey[i] = byteArray[1 + i];
				}
				return;
			}
			throw new CorruptDataException(ServerStrings.ExInvalidIdFormat);
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0014D30B File Offset: 0x0014B50B
		public VersionedId(string base64String) : this(StoreId.Base64ToByteArray(base64String))
		{
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0014D319 File Offset: 0x0014B519
		internal VersionedId(byte[] storeObjectIdByteArray, byte[] changeKeyByteArray)
		{
			if (changeKeyByteArray.Length > 255)
			{
				throw new CorruptDataException(ServerStrings.ExChangeKeyTooLong);
			}
			this.ItemId = StoreObjectId.Parse(storeObjectIdByteArray, 0);
			this.ChangeKey = (byte[])changeKeyByteArray.Clone();
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x0014D354 File Offset: 0x0014B554
		internal VersionedId(string storeObjectIdString, string changeKeyString) : this(StoreId.Base64ToByteArray(storeObjectIdString), StoreId.Base64ToByteArray(changeKeyString))
		{
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x06004FFD RID: 20477 RVA: 0x0014D368 File Offset: 0x0014B568
		public StoreObjectId ObjectId
		{
			get
			{
				return this.ItemId;
			}
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0014D370 File Offset: 0x0014B570
		public static VersionedId Deserialize(string base64Id)
		{
			if (base64Id == null)
			{
				throw new ArgumentNullException("base64Id");
			}
			return new VersionedId(base64Id);
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x0014D386 File Offset: 0x0014B586
		public static VersionedId Deserialize(byte[] byteArray)
		{
			if (byteArray == null)
			{
				throw new ArgumentNullException("byteArray");
			}
			return new VersionedId(byteArray);
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x0014D39C File Offset: 0x0014B59C
		public static VersionedId Deserialize(string base64UniqueId, string base64ChangeKey)
		{
			if (base64UniqueId == null || base64ChangeKey == null)
			{
				throw new ArgumentNullException();
			}
			return new VersionedId(base64UniqueId, base64ChangeKey);
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0014D3B1 File Offset: 0x0014B5B1
		public static VersionedId Deserialize(byte[] byteArrayUniqueId, byte[] byteArrayChangeKey)
		{
			if (byteArrayUniqueId == null || byteArrayChangeKey == null)
			{
				throw new ArgumentNullException();
			}
			return new VersionedId(byteArrayUniqueId, byteArrayChangeKey);
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0014D3C8 File Offset: 0x0014B5C8
		public static VersionedId Deserialize(byte[] providerSpecificId, byte[] byteArrayChangeKey, StoreObjectType objectType)
		{
			if (providerSpecificId == null || byteArrayChangeKey == null)
			{
				throw new ArgumentNullException();
			}
			StoreObjectId itemId = StoreObjectId.FromProviderSpecificId(providerSpecificId, objectType);
			return new VersionedId(itemId, byteArrayChangeKey);
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0014D3F0 File Offset: 0x0014B5F0
		public static VersionedId FromStoreObjectId(StoreObjectId storeObjectId, byte[] changeKey)
		{
			Util.ThrowOnNullArgument(storeObjectId, "storeObjectId");
			Util.ThrowOnNullArgument(changeKey, "changeKey");
			return new VersionedId(storeObjectId, changeKey);
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0014D410 File Offset: 0x0014B610
		public override bool Equals(object id)
		{
			VersionedId other = id as VersionedId;
			return this.Equals(other);
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0014D42C File Offset: 0x0014B62C
		public override bool Equals(StoreId id)
		{
			VersionedId other = id as VersionedId;
			return this.Equals(other);
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0014D447 File Offset: 0x0014B647
		public bool Equals(VersionedId other)
		{
			return other != null && this.ObjectId.Equals(other.ObjectId) && ArrayComparer<byte>.Comparer.Equals(this.ChangeKey, other.ChangeKey);
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0014D478 File Offset: 0x0014B678
		public int CompareTo(object o)
		{
			if (o == null)
			{
				return 1;
			}
			VersionedId versionedId = o as VersionedId;
			if (versionedId == null)
			{
				throw new ArgumentException();
			}
			return this.CompareTo(versionedId);
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0014D4A4 File Offset: 0x0014B6A4
		public int CompareTo(VersionedId v)
		{
			if (v == null)
			{
				return 1;
			}
			Type type = this.ItemId.GetType();
			Type type2 = v.ItemId.GetType();
			if (!type.Equals(type2))
			{
				return type.FullName.CompareTo(type2.FullName);
			}
			int num = this.ItemId.CompareTo(v.ItemId);
			if (num == 0)
			{
				return ArrayComparer<byte>.Comparer.Compare(this.ChangeKey, v.ChangeKey);
			}
			return num;
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0014D516 File Offset: 0x0014B716
		public override string ToBase64String()
		{
			return Convert.ToBase64String(this.GetBytes());
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x0014D524 File Offset: 0x0014B724
		public override byte[] GetBytes()
		{
			byte[] bytes = this.ItemId.GetBytes();
			byte[] array = new byte[1 + this.ChangeKey.Length + bytes.Length];
			array[0] = (byte)this.ChangeKey.Length;
			this.ChangeKey.CopyTo(array, 1);
			bytes.CopyTo(array, 1 + this.ChangeKey.Length);
			return array;
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x0014D57C File Offset: 0x0014B77C
		public string ChangeKeyAsBase64String()
		{
			return Convert.ToBase64String(this.ChangeKey);
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0014D589 File Offset: 0x0014B789
		public byte[] ChangeKeyAsByteArray()
		{
			return (byte[])this.ChangeKey.Clone();
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x0014D59B File Offset: 0x0014B79B
		public override string ToString()
		{
			return this.ToBase64String();
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x0014D5A4 File Offset: 0x0014B7A4
		public override int GetHashCode()
		{
			int num = this.ItemId.GetHashCode();
			for (int i = 0; i < this.ChangeKey.Length; i++)
			{
				num ^= (int)this.ChangeKey[i] << 8 * (i % 4);
			}
			return num;
		}

		// Token: 0x04002B66 RID: 11110
		protected readonly byte[] ChangeKey;

		// Token: 0x04002B67 RID: 11111
		protected readonly StoreObjectId ItemId;
	}
}
