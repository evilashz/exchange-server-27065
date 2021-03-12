using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Storage.IPFiltering
{
	// Token: 0x02000123 RID: 291
	internal class IPFilterList
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0002FFEC File Offset: 0x0002E1EC
		public int Count
		{
			get
			{
				return this.singleAddresses.Count + this.rangeAddresses.Count;
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00030008 File Offset: 0x0002E208
		public void Add(IPFilterRange range)
		{
			this.syncLock.TryEnterWriteLock(int.MaxValue);
			try
			{
				if (range.LowerBound == range.UpperBound)
				{
					IPFilterList.Insert(this.singleAddresses, range);
				}
				else
				{
					IPFilterList.Insert(this.rangeAddresses, range);
				}
			}
			finally
			{
				this.syncLock.ExitWriteLock();
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00030074 File Offset: 0x0002E274
		public IPFilterRange Search(IPvxAddress address)
		{
			IPFilterRange address2 = new IPFilterRange(address);
			this.syncLock.TryEnterReadLock(int.MaxValue);
			try
			{
				IPFilterRange ipfilterRange = this.SearchSingles(address2);
				if (ipfilterRange != null)
				{
					return ipfilterRange;
				}
				ipfilterRange = this.SearchRanges(address2);
				if (ipfilterRange != null)
				{
					return ipfilterRange;
				}
			}
			finally
			{
				this.syncLock.ExitReadLock();
			}
			return null;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000300E4 File Offset: 0x0002E2E4
		public bool ContainsAdminIPRange(IPFilterRange range)
		{
			this.syncLock.TryEnterReadLock(int.MaxValue);
			bool result;
			try
			{
				foreach (IPFilterRange ipfilterRange in this.singleAddresses)
				{
					if (range.AdminCreated && ipfilterRange.Equals(range))
					{
						return true;
					}
				}
				foreach (IPFilterRange ipfilterRange2 in this.rangeAddresses)
				{
					if (range.AdminCreated && ipfilterRange2.Equals(range))
					{
						return true;
					}
				}
				result = false;
			}
			finally
			{
				this.syncLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000301C4 File Offset: 0x0002E3C4
		public void Remove(IPFilterRange target)
		{
			this.syncLock.TryEnterWriteLock(int.MaxValue);
			try
			{
				if (target.LowerBound == target.UpperBound)
				{
					IPFilterList.RemoveFromList(this.singleAddresses, target);
				}
				else
				{
					IPFilterList.RemoveFromList(this.rangeAddresses, target);
				}
			}
			finally
			{
				this.syncLock.ExitWriteLock();
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00030230 File Offset: 0x0002E430
		public void Cleanup(DateTime now)
		{
			this.syncLock.TryEnterWriteLock(int.MaxValue);
			try
			{
				for (int i = 0; i < this.singleAddresses.Count; i++)
				{
					IPFilterRange ipfilterRange = this.singleAddresses[i];
					if (ipfilterRange.IsExpired(now))
					{
						this.singleAddresses.RemoveAt(i);
						i--;
					}
				}
				for (int j = 0; j < this.rangeAddresses.Count; j++)
				{
					IPFilterRange ipfilterRange2 = this.rangeAddresses[j];
					if (ipfilterRange2.IsExpired(now))
					{
						this.rangeAddresses.RemoveAt(j);
						j--;
					}
				}
			}
			finally
			{
				this.syncLock.ExitWriteLock();
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000302E4 File Offset: 0x0002E4E4
		internal void Sort()
		{
			this.singleAddresses.Sort();
			this.rangeAddresses.Sort();
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000302FC File Offset: 0x0002E4FC
		internal void Add(IPFilterRow row)
		{
			IPFilterRange ipfilterRange = IPFilterRange.FromRowWithoutComment(row);
			if (ipfilterRange.LowerBound == ipfilterRange.UpperBound)
			{
				this.singleAddresses.Add(ipfilterRange);
				return;
			}
			this.rangeAddresses.Add(ipfilterRange);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003033C File Offset: 0x0002E53C
		private static void Insert(List<IPFilterRange> list, IPFilterRange range)
		{
			int num = list.BinarySearch(range);
			if (num == ~list.Count)
			{
				list.Add(range);
				return;
			}
			if (num < 0)
			{
				num = ~num;
			}
			list.Insert(num, range);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00030374 File Offset: 0x0002E574
		private static void RemoveFromList(List<IPFilterRange> list, IPFilterRange target)
		{
			int num = list.BinarySearch(target);
			if (num >= 0)
			{
				for (int i = num; i < list.Count; i++)
				{
					IPFilterRange ipfilterRange = list[i];
					if (ipfilterRange.Identity == target.Identity)
					{
						list.RemoveAt(i);
						return;
					}
					if (ipfilterRange.LowerBound != target.LowerBound)
					{
						break;
					}
				}
				for (int j = num - 1; j >= 0; j--)
				{
					IPFilterRange ipfilterRange2 = list[j];
					if (ipfilterRange2.Identity == target.Identity)
					{
						list.RemoveAt(j);
						return;
					}
					if (ipfilterRange2.LowerBound != target.LowerBound)
					{
						return;
					}
				}
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00030418 File Offset: 0x0002E618
		private IPFilterRange SearchSingles(IPFilterRange address)
		{
			int num = this.singleAddresses.BinarySearch(address);
			if (num < 0)
			{
				return null;
			}
			DateTime utcNow = DateTime.UtcNow;
			for (int i = num; i < this.singleAddresses.Count; i++)
			{
				IPFilterRange ipfilterRange = this.singleAddresses[i];
				if (ipfilterRange.LowerBound != address.LowerBound)
				{
					break;
				}
				if (!ipfilterRange.IsExpired(utcNow))
				{
					return ipfilterRange;
				}
			}
			for (int j = num - 1; j >= 0; j--)
			{
				IPFilterRange ipfilterRange2 = this.singleAddresses[j];
				if (ipfilterRange2.LowerBound != address.LowerBound)
				{
					break;
				}
				if (!ipfilterRange2.IsExpired(utcNow))
				{
					return ipfilterRange2;
				}
			}
			return null;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000304C4 File Offset: 0x0002E6C4
		private IPFilterRange SearchRanges(IPFilterRange address)
		{
			int num = this.rangeAddresses.BinarySearch(address);
			if (num < ~this.rangeAddresses.Count)
			{
				return null;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (num >= 0)
			{
				for (int i = num; i < this.rangeAddresses.Count; i++)
				{
					IPFilterRange ipfilterRange = this.rangeAddresses[i];
					if (ipfilterRange.LowerBound != address.LowerBound)
					{
						break;
					}
					if (!ipfilterRange.IsExpired(utcNow))
					{
						return ipfilterRange;
					}
				}
			}
			else
			{
				num = ~num;
			}
			for (int j = num - 1; j >= 0; j--)
			{
				IPFilterRange ipfilterRange2 = this.rangeAddresses[j];
				if (!ipfilterRange2.IsExpired(utcNow) && ipfilterRange2.Contains(address.LowerBound))
				{
					return ipfilterRange2;
				}
			}
			return null;
		}

		// Token: 0x040005A4 RID: 1444
		private List<IPFilterRange> singleAddresses = new List<IPFilterRange>();

		// Token: 0x040005A5 RID: 1445
		private List<IPFilterRange> rangeAddresses = new List<IPFilterRange>();

		// Token: 0x040005A6 RID: 1446
		private ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();
	}
}
