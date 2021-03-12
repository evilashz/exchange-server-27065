using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	internal class CacheFileCookie : IEquatable<CacheFileCookie>
	{
		// Token: 0x06000320 RID: 800 RVA: 0x000098C4 File Offset: 0x00007AC4
		public CacheFileCookie()
		{
			this.EntityName = string.Empty;
			this.PartitionIndex = 0;
			this.Cookie = string.Empty;
			this.CacheCookieRows = new SortedList<int, CacheCookieRow>();
			this.StartFileOffset = 0L;
			this.EndFileOffset = 0L;
			this.PreCookieOffset = -1L;
			this.NextCookieOffset = -1L;
			this.CookieOffset = -1L;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00009928 File Offset: 0x00007B28
		public CacheFileCookie(CacheFileCookie copy)
		{
			this.EntityName = copy.EntityName;
			this.PartitionIndex = copy.PartitionIndex;
			this.Cookie = copy.Cookie;
			this.CacheCookieRows = CacheFileCookie.CopyCacheCookieRows(copy.CacheCookieRows);
			this.StartFileOffset = copy.StartFileOffset;
			this.EndFileOffset = copy.EndFileOffset;
			this.PreCookieOffset = copy.PreCookieOffset;
			this.NextCookieOffset = copy.NextCookieOffset;
			this.CookieOffset = copy.CookieOffset;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000099AC File Offset: 0x00007BAC
		public CacheFileCookie(string entityName, int partitionIndex) : this()
		{
			this.EntityName = entityName;
			this.PartitionIndex = partitionIndex;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000099C2 File Offset: 0x00007BC2
		// (set) Token: 0x06000324 RID: 804 RVA: 0x000099CA File Offset: 0x00007BCA
		public string EntityName { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000099D3 File Offset: 0x00007BD3
		// (set) Token: 0x06000326 RID: 806 RVA: 0x000099DB File Offset: 0x00007BDB
		public int PartitionIndex { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000099E4 File Offset: 0x00007BE4
		// (set) Token: 0x06000328 RID: 808 RVA: 0x000099EC File Offset: 0x00007BEC
		public string Cookie { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000099F5 File Offset: 0x00007BF5
		// (set) Token: 0x0600032A RID: 810 RVA: 0x000099FD File Offset: 0x00007BFD
		public SortedList<int, CacheCookieRow> CacheCookieRows { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00009A06 File Offset: 0x00007C06
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00009A0E File Offset: 0x00007C0E
		public long StartFileOffset { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00009A17 File Offset: 0x00007C17
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00009A1F File Offset: 0x00007C1F
		public long EndFileOffset { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00009A28 File Offset: 0x00007C28
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00009A30 File Offset: 0x00007C30
		public long PreCookieOffset { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00009A39 File Offset: 0x00007C39
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00009A41 File Offset: 0x00007C41
		public long NextCookieOffset { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00009A4A File Offset: 0x00007C4A
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00009A52 File Offset: 0x00007C52
		public long CookieOffset { get; set; }

		// Token: 0x06000335 RID: 821 RVA: 0x00009A5C File Offset: 0x00007C5C
		public static SortedList<int, CacheCookieRow> CopyCacheCookieRows(SortedList<int, CacheCookieRow> crList2)
		{
			if (crList2 == null)
			{
				return null;
			}
			SortedList<int, CacheCookieRow> sortedList = new SortedList<int, CacheCookieRow>();
			foreach (CacheCookieRow cacheCookieRow in crList2.Values)
			{
				sortedList.Add(cacheCookieRow.CopyIndex, cacheCookieRow);
			}
			return sortedList;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00009ABC File Offset: 0x00007CBC
		public static bool CacheCookieRowEquals(SortedList<int, CacheCookieRow> crList1, SortedList<int, CacheCookieRow> crList2)
		{
			if (crList1 == null || crList2 == null)
			{
				return crList1 == null && crList2 == null;
			}
			if (crList1.Count<KeyValuePair<int, CacheCookieRow>>() != crList2.Count<KeyValuePair<int, CacheCookieRow>>())
			{
				return false;
			}
			int num = 0;
			foreach (CacheCookieRow cacheCookieRow in crList1.Values)
			{
				if (!cacheCookieRow.Equals(crList2[num]))
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009B40 File Offset: 0x00007D40
		public bool Equals(CacheFileCookie c2)
		{
			return c2 != null && (this.EntityName == c2.EntityName && this.PartitionIndex == c2.PartitionIndex && this.Cookie == c2.Cookie && this.StartFileOffset == c2.StartFileOffset && this.EndFileOffset == c2.EndFileOffset && this.PreCookieOffset == c2.PreCookieOffset && this.NextCookieOffset == c2.NextCookieOffset && this.CookieOffset == c2.CookieOffset) && CacheFileCookie.CacheCookieRowEquals(this.CacheCookieRows, c2.CacheCookieRows);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009BDF File Offset: 0x00007DDF
		public override bool Equals(object obj)
		{
			return obj != null && obj is CacheFileCookie && this.Equals(obj as CacheFileCookie);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009BFC File Offset: 0x00007DFC
		public override int GetHashCode()
		{
			string text = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", new object[]
			{
				this.EntityName,
				this.PartitionIndex,
				this.Cookie,
				this.StartFileOffset,
				this.EndFileOffset,
				this.CookieOffset
			});
			return text.GetHashCode();
		}
	}
}
