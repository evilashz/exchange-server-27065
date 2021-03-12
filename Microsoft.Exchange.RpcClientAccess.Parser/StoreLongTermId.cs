using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200008F RID: 143
	internal struct StoreLongTermId : IEquatable<StoreLongTermId>, IComparable<StoreLongTermId>
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000CFE9 File Offset: 0x0000B1E9
		public StoreLongTermId(Guid guid, byte[] globCount)
		{
			Util.ThrowOnNullArgument(globCount, "globCount");
			if (globCount.Length != 6)
			{
				throw new ArgumentException(string.Format("GlobCount needs to be {0} bytes", 6));
			}
			this.guid = guid;
			this.globCount = globCount;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000D020 File Offset: 0x0000B220
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000D028 File Offset: 0x0000B228
		public byte[] GlobCount
		{
			get
			{
				return this.globCount;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000D030 File Offset: 0x0000B230
		internal static int ArraySize
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000D034 File Offset: 0x0000B234
		internal static int Size
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D038 File Offset: 0x0000B238
		public static bool operator <=(StoreLongTermId storeLongTermId1, StoreLongTermId storeLongTermId2)
		{
			return storeLongTermId1.CompareTo(storeLongTermId2) <= 0;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D048 File Offset: 0x0000B248
		public static bool operator >=(StoreLongTermId storeLongTermId1, StoreLongTermId storeLongTermId2)
		{
			return storeLongTermId1.CompareTo(storeLongTermId2) >= 0;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D058 File Offset: 0x0000B258
		public static implicit operator GuidGlobCount(StoreLongTermId storeLongTermId)
		{
			ulong num = 0UL;
			for (int i = 0; i < 6; i++)
			{
				num += (ulong)storeLongTermId.GlobCount[i] << 8 * (5 - i);
			}
			return new GuidGlobCount(storeLongTermId.Guid, num);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D097 File Offset: 0x0000B297
		public override bool Equals(object obj)
		{
			return obj is StoreLongTermId && this.Equals((StoreLongTermId)obj);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		public override int GetHashCode()
		{
			return this.guid.GetHashCode() ^ ArrayComparer<byte>.Comparer.GetHashCode(this.globCount);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D0E2 File Offset: 0x0000B2E2
		internal static StoreLongTermId Parse(Reader reader)
		{
			return StoreLongTermId.Parse(reader, true);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		internal static StoreLongTermId Parse(Reader reader, bool includePadding)
		{
			Guid guid = reader.ReadGuid();
			byte[] array = reader.ReadBytes(6U);
			if (includePadding)
			{
				reader.ReadArraySegment(2U);
			}
			return new StoreLongTermId(guid, array);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D11A File Offset: 0x0000B31A
		internal static StoreLongTermId Parse(byte[] rawId)
		{
			return StoreLongTermId.Parse(rawId, false);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D124 File Offset: 0x0000B324
		internal static StoreLongTermId Parse(byte[] rawId, bool includePadding)
		{
			Util.ThrowOnNullArgument(rawId, "rawId");
			int num = includePadding ? StoreLongTermId.Size : StoreLongTermId.ArraySize;
			if (rawId.Length != num)
			{
				throw new BufferParseException("The buffer representing StoreLongTermId is invalid.");
			}
			StoreLongTermId result;
			using (Reader reader = Reader.CreateBufferReader(rawId))
			{
				result = StoreLongTermId.Parse(reader, includePadding);
			}
			return result;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D18C File Offset: 0x0000B38C
		internal void Serialize(Writer writer)
		{
			this.Serialize(writer, true);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D196 File Offset: 0x0000B396
		internal void Serialize(Writer writer, bool includePadding)
		{
			writer.WriteGuid(this.guid);
			writer.WriteBytes(this.globCount);
			if (includePadding)
			{
				writer.WriteUInt16(0);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D1BA File Offset: 0x0000B3BA
		internal byte[] ToBytes()
		{
			return this.ToBytes(false);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		internal byte[] ToBytes(bool includePadding)
		{
			int num = includePadding ? StoreLongTermId.Size : StoreLongTermId.ArraySize;
			byte[] array = new byte[num];
			using (Writer writer = new BufferWriter(array))
			{
				writer.WriteGuid(this.guid);
				writer.WriteBytes(this.globCount);
			}
			return array;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D224 File Offset: 0x0000B424
		public bool Equals(StoreLongTermId other)
		{
			return this.guid == other.guid && ArrayComparer<byte>.Comparer.Equals(this.globCount, other.globCount);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D254 File Offset: 0x0000B454
		public int CompareTo(StoreLongTermId other)
		{
			int num = this.Guid.CompareTo(other.Guid);
			if (num == 0)
			{
				num = ArrayComparer<byte>.Comparer.Compare(this.GlobCount, other.GlobCount);
			}
			return num;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D294 File Offset: 0x0000B494
		public override string ToString()
		{
			if (this.GlobCount == null)
			{
				return string.Format("{0} - null", this.Guid);
			}
			return string.Format("{0} - {1:X2}{2:X2}{3:X2}{4:X2}{5:X2}{6:X2}", new object[]
			{
				this.Guid,
				this.GlobCount[0],
				this.GlobCount[1],
				this.GlobCount[2],
				this.GlobCount[3],
				this.GlobCount[4],
				this.GlobCount[5]
			});
		}

		// Token: 0x04000200 RID: 512
		private const int GuidSize = 16;

		// Token: 0x04000201 RID: 513
		private const int GlobCountLength = 6;

		// Token: 0x04000202 RID: 514
		private const int PaddingSize = 2;

		// Token: 0x04000203 RID: 515
		public const int SizeWithoutPadding = 22;

		// Token: 0x04000204 RID: 516
		public const int SizeWithPadding = 24;

		// Token: 0x04000205 RID: 517
		private readonly Guid guid;

		// Token: 0x04000206 RID: 518
		private readonly byte[] globCount;

		// Token: 0x04000207 RID: 519
		public static readonly StoreLongTermId Null = new StoreLongTermId(Guid.Empty, new byte[6]);
	}
}
