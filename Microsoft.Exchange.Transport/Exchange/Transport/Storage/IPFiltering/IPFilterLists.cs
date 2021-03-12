using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Storage.IPFiltering
{
	// Token: 0x02000124 RID: 292
	internal static class IPFilterLists
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x000305A6 File Offset: 0x0002E7A6
		public static IPFilterList AddressAllowList
		{
			get
			{
				return IPFilterLists.addressAllowList;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x000305AD File Offset: 0x0002E7AD
		public static IPFilterList AddressDenyList
		{
			get
			{
				return IPFilterLists.addressDenyList;
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000305B4 File Offset: 0x0002E7B4
		public static void Load()
		{
			using (DataTableCursor cursor = Database.Table.GetCursor())
			{
				using (Transaction transaction = cursor.BeginTransaction())
				{
					IPFilterLists.BulkLoader bulkLoader = new IPFilterLists.BulkLoader();
					bulkLoader.Scan(transaction, cursor, true);
				}
			}
			IPFilterLists.AddressAllowList.Sort();
			IPFilterLists.AddressDenyList.Sort();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0003062C File Offset: 0x0002E82C
		public static void Cleanup()
		{
			DateTime utcNow = DateTime.UtcNow;
			IPFilterLists.AddressDenyList.Cleanup(utcNow);
			IPFilterLists.AddressAllowList.Cleanup(utcNow);
			IPFilterLists.cleanupScanner.Cleanup(utcNow);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00030660 File Offset: 0x0002E860
		public static int AddRestriction(IPFilterRange range)
		{
			IPFilterRow ipfilterRow = new IPFilterRow();
			ipfilterRow.LowerBound = range.LowerBound;
			ipfilterRow.UpperBound = range.UpperBound;
			ipfilterRow.ExpiresOn = range.ExpiresOn;
			ipfilterRow.TypeFlags = range.TypeFlags;
			ipfilterRow.Comment = range.Comment;
			ipfilterRow.Commit();
			range.PurgeComment();
			range.Identity = ipfilterRow.Identity;
			if (range.PolicyType == PolicyType.Allow)
			{
				IPFilterLists.AddressAllowList.Add(range);
			}
			else
			{
				IPFilterLists.AddressDenyList.Add(range);
			}
			return ipfilterRow.Identity;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000306F0 File Offset: 0x0002E8F0
		public static int AdminRemove(int identity, int filter)
		{
			using (DataTableCursor cursor = Database.Table.GetCursor())
			{
				using (Transaction transaction = cursor.BeginTransaction())
				{
					IPFilterRow ipfilterRow = new IPFilterRow(identity);
					if (ipfilterRow.TrySeekCurrent(cursor))
					{
						ipfilterRow = IPFilterRow.LoadFromRow(cursor);
						if ((ipfilterRow.TypeFlags & 240) == (filter & 240))
						{
							IPFilterRange range = IPFilterRange.FromRowWithoutComment(ipfilterRow);
							ipfilterRow.MarkToDelete();
							ipfilterRow.Commit(transaction, cursor);
							IPFilterLists.RemoveFromMemoryLists(range);
							transaction.Commit();
							return 1;
						}
					}
				}
			}
			return 0;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003079C File Offset: 0x0002E99C
		public static List<IPFilterRow> AdminGetItems(int startIdentity, int flags, IPvxAddress address, int count)
		{
			if (startIdentity < 0)
			{
				throw new InvalidOperationException("startIdentity must be zero or grater");
			}
			if ((flags & -4081) != 0)
			{
				throw new InvalidOperationException("flags can only specifiy 0x0ff0 nybbles");
			}
			if (count <= 0)
			{
				throw new InvalidOperationException("count must be positive");
			}
			List<IPFilterRow> result;
			using (DataTableCursor cursor = Database.Table.GetCursor())
			{
				using (Transaction transaction = cursor.BeginTransaction())
				{
					IPFilterRow ipfilterRow = new IPFilterRow(startIdentity);
					if (!ipfilterRow.TrySeekCurrentPrefix(cursor, 1))
					{
						result = new List<IPFilterRow>();
					}
					else
					{
						IPFilterLists.IPFilterAdminScanner ipfilterAdminScanner = new IPFilterLists.IPFilterAdminScanner();
						result = ipfilterAdminScanner.AdminGetItems(transaction, cursor, flags, address, count);
					}
				}
			}
			return result;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00030850 File Offset: 0x0002EA50
		private static void RemoveFromMemoryLists(IPFilterRange range)
		{
			if (range.PolicyType == PolicyType.Allow)
			{
				IPFilterLists.AddressAllowList.Remove(range);
				return;
			}
			IPFilterLists.AddressDenyList.Remove(range);
		}

		// Token: 0x040005A7 RID: 1447
		private static IPFilterList addressAllowList = new IPFilterList();

		// Token: 0x040005A8 RID: 1448
		private static IPFilterList addressDenyList = new IPFilterList();

		// Token: 0x040005A9 RID: 1449
		private static IPFilterLists.CleanupScanner cleanupScanner = new IPFilterLists.CleanupScanner();

		// Token: 0x02000125 RID: 293
		private class BulkLoader : ChunkingScanner
		{
			// Token: 0x06000D39 RID: 3385 RVA: 0x00030894 File Offset: 0x0002EA94
			protected override ChunkingScanner.ScanControl HandleRecord(DataTableCursor cursor)
			{
				IPFilterRow ipfilterRow = IPFilterRow.LoadFromRow(cursor);
				if (ipfilterRow.ExpiresOn > DateTime.UtcNow)
				{
					if (ipfilterRow.Policy == PolicyType.Allow)
					{
						IPFilterLists.AddressAllowList.Add(ipfilterRow);
					}
					else
					{
						IPFilterLists.AddressDenyList.Add(ipfilterRow);
					}
				}
				return ChunkingScanner.ScanControl.Continue;
			}
		}

		// Token: 0x02000126 RID: 294
		private class IPFilterAdminScanner : ChunkingScanner
		{
			// Token: 0x06000D3B RID: 3387 RVA: 0x000308E4 File Offset: 0x0002EAE4
			public List<IPFilterRow> AdminGetItems(Transaction transaction, DataTableCursor cursor, int flags, IPvxAddress filter, int maximum)
			{
				this.mode = IPFilterLists.IPFilterAdminScanner.Mode.GetItems;
				this.list = new List<IPFilterRow>();
				this.filterFlags = flags;
				this.filterAddress = filter;
				this.maxCount = maximum;
				base.ScanFromCurrentPosition(transaction, cursor, true);
				return this.list;
			}

			// Token: 0x06000D3C RID: 3388 RVA: 0x00030920 File Offset: 0x0002EB20
			protected override ChunkingScanner.ScanControl HandleRecord(DataTableCursor cursor)
			{
				IPFilterRow ipfilterRow = IPFilterRow.LoadFromRow(cursor);
				if (!this.MatchesOrWild(240, ipfilterRow.TypeFlags))
				{
					return ChunkingScanner.ScanControl.Continue;
				}
				if (!this.MatchesOrWild(3840, ipfilterRow.TypeFlags))
				{
					return ChunkingScanner.ScanControl.Continue;
				}
				if (this.filterAddress != IPvxAddress.None)
				{
					IPFilterRange ipfilterRange = IPFilterRange.FromRowWithoutComment(ipfilterRow);
					if (!ipfilterRange.Contains(this.filterAddress))
					{
						return ChunkingScanner.ScanControl.Continue;
					}
				}
				if (this.mode == IPFilterLists.IPFilterAdminScanner.Mode.GetItems)
				{
					this.list.Add(ipfilterRow);
					if (this.list.Count >= this.maxCount)
					{
						return ChunkingScanner.ScanControl.Stop;
					}
				}
				else
				{
					if (this.mode != IPFilterLists.IPFilterAdminScanner.Mode.RemoveItems)
					{
						throw new InvalidOperationException();
					}
					IPFilterRange range = IPFilterRange.FromRowWithoutComment(ipfilterRow);
					ipfilterRow.MarkToDelete();
					using (Transaction transaction = cursor.Connection.BeginTransaction())
					{
						ipfilterRow.Commit(transaction, cursor);
						transaction.Commit();
					}
					IPFilterLists.RemoveFromMemoryLists(range);
					this.removeCount++;
				}
				return ChunkingScanner.ScanControl.Continue;
			}

			// Token: 0x06000D3D RID: 3389 RVA: 0x00030A1C File Offset: 0x0002EC1C
			private bool MatchesOrWild(int nybbleMask, int flags)
			{
				int num = this.filterFlags & nybbleMask;
				return num == nybbleMask || num == (flags & nybbleMask);
			}

			// Token: 0x040005AA RID: 1450
			private IPFilterLists.IPFilterAdminScanner.Mode mode;

			// Token: 0x040005AB RID: 1451
			private List<IPFilterRow> list;

			// Token: 0x040005AC RID: 1452
			private int filterFlags;

			// Token: 0x040005AD RID: 1453
			private int maxCount;

			// Token: 0x040005AE RID: 1454
			private int removeCount;

			// Token: 0x040005AF RID: 1455
			private IPvxAddress filterAddress;

			// Token: 0x02000127 RID: 295
			private enum Mode
			{
				// Token: 0x040005B1 RID: 1457
				GetItems,
				// Token: 0x040005B2 RID: 1458
				RemoveItems
			}
		}

		// Token: 0x02000128 RID: 296
		private sealed class CleanupScanner : ChunkingScanner
		{
			// Token: 0x06000D40 RID: 3392 RVA: 0x00030A50 File Offset: 0x0002EC50
			public void Cleanup(DateTime now)
			{
				this.now = now;
				this.entriesRemoved = 0;
				using (DataTableCursor cursor = Database.Table.GetCursor())
				{
					using (Transaction transaction = cursor.BeginTransaction())
					{
						this.Scan(transaction, cursor, true);
						transaction.Commit();
					}
				}
			}

			// Token: 0x06000D41 RID: 3393 RVA: 0x00030AC0 File Offset: 0x0002ECC0
			protected override ChunkingScanner.ScanControl HandleRecord(DataTableCursor cursor)
			{
				IPFilterRow ipfilterRow = IPFilterRow.LoadFromRow(cursor);
				IPFilterRange ipfilterRange = IPFilterRange.FromRowWithoutComment(ipfilterRow);
				if (ipfilterRange.PolicyType == PolicyType.Deny && !ipfilterRange.AdminCreated && this.now > ipfilterRange.ExpiresOn + IPFilterLists.CleanupScanner.ExpirationThreshold)
				{
					ipfilterRow.MarkToDelete();
					using (Transaction transaction = cursor.Connection.BeginTransaction())
					{
						ipfilterRow.Commit(transaction, cursor);
						transaction.Commit();
					}
					this.entriesRemoved++;
				}
				if (this.entriesRemoved >= 10000)
				{
					return ChunkingScanner.ScanControl.Stop;
				}
				return ChunkingScanner.ScanControl.Continue;
			}

			// Token: 0x040005B3 RID: 1459
			private const int MaxEntriesCleanedPerRun = 10000;

			// Token: 0x040005B4 RID: 1460
			private static readonly TimeSpan ExpirationThreshold = new TimeSpan(14, 0, 0, 0);

			// Token: 0x040005B5 RID: 1461
			private DateTime now;

			// Token: 0x040005B6 RID: 1462
			private int entriesRemoved;
		}
	}
}
