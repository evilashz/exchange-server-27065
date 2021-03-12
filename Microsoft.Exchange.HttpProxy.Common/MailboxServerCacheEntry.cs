using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.SharedCache.Client;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000F RID: 15
	public class MailboxServerCacheEntry : ISharedCacheEntry
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000032E9 File Offset: 0x000014E9
		public MailboxServerCacheEntry()
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000032F1 File Offset: 0x000014F1
		public MailboxServerCacheEntry(string fqdn, int version, string resourceForest, DateTime lastRefreshTime, bool isSourceCachedData) : this(new BackEndServer(fqdn, version), resourceForest, lastRefreshTime, isSourceCachedData)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003305 File Offset: 0x00001505
		public MailboxServerCacheEntry(BackEndServer backEndServer, string resourceForest, DateTime lastRefreshTime, bool isSourceCachedData)
		{
			this.BackEndServer = backEndServer;
			this.ResourceForest = resourceForest;
			this.LastRefreshTime = lastRefreshTime;
			this.IsSourceCachedData = isSourceCachedData;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000332A File Offset: 0x0000152A
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003332 File Offset: 0x00001532
		public BackEndServer BackEndServer { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000333B File Offset: 0x0000153B
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00003343 File Offset: 0x00001543
		public string ResourceForest { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000334C File Offset: 0x0000154C
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003354 File Offset: 0x00001554
		public DateTime LastRefreshTime { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000335D File Offset: 0x0000155D
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00003365 File Offset: 0x00001565
		public bool IsSourceCachedData { get; set; }

		// Token: 0x06000046 RID: 70 RVA: 0x0000336E File Offset: 0x0000156E
		public bool IsDueForRefresh(TimeSpan refreshTimeSpan)
		{
			return DateTime.UtcNow - this.LastRefreshTime > refreshTimeSpan;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003386 File Offset: 0x00001586
		public bool ShouldRefresh(TimeSpan refreshTimeSpan, bool isSourceCachedData)
		{
			return !isSourceCachedData || this.IsSourceCachedData || this.IsDueForRefresh(refreshTimeSpan);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000339C File Offset: 0x0000159C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"CacheEntry(BackEndServer ",
				this.BackEndServer.ToString(),
				"|ResourceForest ",
				this.ResourceForest,
				"|LastRefreshTime ",
				this.LastRefreshTime.ToString("o"),
				"|IsSourceCachedData ",
				this.IsSourceCachedData.ToString(),
				")"
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003420 File Offset: 0x00001620
		public void FromByteArray(byte[] bytes)
		{
			if (bytes == null || bytes.Length < 14)
			{
				throw new ArgumentException(string.Format("It's not a valid byte array for MailboxServerCacheEntry, which has at least {0} bytes", 14));
			}
			byte b = bytes[0];
			int num = 1;
			this.LastRefreshTime = DateTime.FromBinary(BitConverter.ToInt64(bytes, num));
			num += 8;
			this.IsSourceCachedData = BitConverter.ToBoolean(bytes, num);
			num++;
			int num2 = BitConverter.ToInt32(bytes, num);
			if (num2 > 0)
			{
				this.BackEndServer = new BackEndServer(Encoding.ASCII.GetString(bytes, num + 8, num2 - 4), BitConverter.ToInt32(bytes, num + 4));
			}
			num += num2 + 4;
			if (num < bytes.Length)
			{
				this.ResourceForest = Encoding.ASCII.GetString(bytes, num, bytes.Length - num);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000034D0 File Offset: 0x000016D0
		public byte[] ToByteArray()
		{
			byte[] array = new byte[14];
			array[0] = 1;
			int num = 1;
			Buffer.BlockCopy(BitConverter.GetBytes(this.LastRefreshTime.ToBinary()), 0, array, num, 8);
			num += 8;
			array[num] = BitConverter.GetBytes(this.IsSourceCachedData)[0];
			num++;
			IEnumerable<byte> enumerable = null;
			int value = 0;
			if (this.BackEndServer != null)
			{
				enumerable = BitConverter.GetBytes(this.BackEndServer.Version);
				enumerable = enumerable.Concat(Encoding.ASCII.GetBytes(this.BackEndServer.Fqdn));
				value = enumerable.Count<byte>();
			}
			Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, num, 4);
			IEnumerable<byte> enumerable2 = (enumerable != null) ? array.Concat(enumerable) : array;
			if (!string.IsNullOrEmpty(this.ResourceForest))
			{
				enumerable2 = enumerable2.Concat(Encoding.ASCII.GetBytes(this.ResourceForest));
			}
			return enumerable2.ToArray<byte>();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000035AC File Offset: 0x000017AC
		public override int GetHashCode()
		{
			int num = this.LastRefreshTime.GetHashCode() ^ this.IsSourceCachedData.GetHashCode();
			if (!string.IsNullOrEmpty(this.ResourceForest))
			{
				num ^= this.ResourceForest.GetHashCode();
			}
			if (this.BackEndServer != null)
			{
				num ^= this.BackEndServer.Version;
				if (!string.IsNullOrEmpty(this.BackEndServer.Fqdn))
				{
					num ^= this.BackEndServer.Fqdn.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003634 File Offset: 0x00001834
		public override bool Equals(object obj)
		{
			MailboxServerCacheEntry mailboxServerCacheEntry = obj as MailboxServerCacheEntry;
			if (mailboxServerCacheEntry != null && this.LastRefreshTime == mailboxServerCacheEntry.LastRefreshTime && this.IsSourceCachedData == mailboxServerCacheEntry.IsSourceCachedData && ((string.IsNullOrEmpty(this.ResourceForest) && string.IsNullOrEmpty(mailboxServerCacheEntry.ResourceForest)) || string.Equals(this.ResourceForest, mailboxServerCacheEntry.ResourceForest)))
			{
				if (this.BackEndServer == null)
				{
					return mailboxServerCacheEntry.BackEndServer == null;
				}
				if (mailboxServerCacheEntry.BackEndServer != null)
				{
					return this.BackEndServer.Version == mailboxServerCacheEntry.BackEndServer.Version && ((string.IsNullOrEmpty(this.BackEndServer.Fqdn) && string.IsNullOrEmpty(mailboxServerCacheEntry.BackEndServer.Fqdn)) || string.Equals(this.BackEndServer.Fqdn, mailboxServerCacheEntry.BackEndServer.Fqdn));
				}
			}
			return false;
		}

		// Token: 0x04000058 RID: 88
		private const int MinimumLength = 14;

		// Token: 0x04000059 RID: 89
		private const byte CurrentSerializationVersion = 1;
	}
}
