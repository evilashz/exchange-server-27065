using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SharedCache.Exceptions;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x0200001C RID: 28
	internal class CacheEntry
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003E62 File Offset: 0x00002062
		public CacheEntry()
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003E6C File Offset: 0x0000206C
		public CacheEntry(byte[] fromBytes)
		{
			ArgumentValidator.ThrowIfNull("fromBytes", fromBytes);
			Exception ex = null;
			try
			{
				int num = 0;
				this.Version = BitConverter.ToInt32(fromBytes, num);
				num += 4;
				this.CreatedAt = DateTime.FromBinary(BitConverter.ToInt64(fromBytes, num));
				num += 8;
				this.UpdatedAt = DateTime.FromBinary(BitConverter.ToInt64(fromBytes, num));
				num += 8;
				this.Value = new byte[fromBytes.Length - num];
				Array.Copy(fromBytes, num, this.Value, 0, this.Value.Length);
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (RankException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw new CorruptCacheEntryException("Cache entry couldn't be deserialized", ex);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003F28 File Offset: 0x00002128
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003F30 File Offset: 0x00002130
		public byte[] Value { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003F39 File Offset: 0x00002139
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003F41 File Offset: 0x00002141
		public DateTime CreatedAt { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003F4A File Offset: 0x0000214A
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003F52 File Offset: 0x00002152
		public DateTime UpdatedAt { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003F5B File Offset: 0x0000215B
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003F63 File Offset: 0x00002163
		private int Version { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003F6C File Offset: 0x0000216C
		private int TotalSizeInBytes
		{
			get
			{
				return 20 + ((this.Value != null) ? this.Value.Length : 0);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003F84 File Offset: 0x00002184
		public byte[] ToByteArray()
		{
			byte[] array = new byte[this.TotalSizeInBytes];
			int num = 0;
			Buffer.BlockCopy(BitConverter.GetBytes(this.Version), 0, array, num, 4);
			num += 4;
			Buffer.BlockCopy(BitConverter.GetBytes(this.CreatedAt.ToBinary()), 0, array, num, 8);
			num += 8;
			Buffer.BlockCopy(BitConverter.GetBytes(this.UpdatedAt.ToBinary()), 0, array, num, 8);
			num += 8;
			if (this.Value != null)
			{
				Buffer.BlockCopy(this.Value, 0, array, num, this.Value.Length);
			}
			return array;
		}

		// Token: 0x04000051 RID: 81
		private const int CurrentVersion = 1;
	}
}
