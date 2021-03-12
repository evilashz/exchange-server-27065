using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F44 RID: 3908
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AuditLogCollection : IAuditLogCollection
	{
		// Token: 0x06008641 RID: 34369 RVA: 0x0024C231 File Offset: 0x0024A431
		public AuditLogCollection(StoreSession storeSession, StoreId auditLogRootId, Trace tracer) : this(storeSession, auditLogRootId, tracer, null)
		{
		}

		// Token: 0x06008642 RID: 34370 RVA: 0x0024C23D File Offset: 0x0024A43D
		public AuditLogCollection(StoreSession storeSession, StoreId auditLogRootId, Trace tracer, Func<IAuditLogRecord, MessageItem, int> recordFormatter)
		{
			this.storeSession = storeSession;
			this.auditLogRootId = auditLogRootId;
			this.tracer = tracer;
			this.recordFormatter = recordFormatter;
		}

		// Token: 0x06008643 RID: 34371 RVA: 0x0024C262 File Offset: 0x0024A462
		public IEnumerable<IAuditLog> GetAuditLogs()
		{
			return this.InternalGetAuditLogs(false, null);
		}

		// Token: 0x06008644 RID: 34372 RVA: 0x0024C2A0 File Offset: 0x0024A4A0
		public IEnumerable<AuditLog> GetExpiringAuditLogs(DateTime expirationTime)
		{
			return this.InternalGetAuditLogs(true, (AuditLogCollection.LogInfo logInfo) => logInfo.RangeEnd < expirationTime || logInfo.RangeStart <= expirationTime);
		}

		// Token: 0x06008645 RID: 34373 RVA: 0x0024C2D0 File Offset: 0x0024A4D0
		public bool FindLog(DateTime timestamp, bool createIfNotExists, out IAuditLog auditLog)
		{
			auditLog = null;
			using (Folder folder = Folder.Bind(this.storeSession, this.auditLogRootId))
			{
				AuditLogCollection.LogInfo? logInfo = null;
				foreach (AuditLogCollection.LogInfo value in this.FindLogFolders(folder, false))
				{
					if (value.RangeStart <= timestamp && timestamp < value.RangeEnd)
					{
						logInfo = new AuditLogCollection.LogInfo?(value);
						break;
					}
				}
				if (logInfo == null)
				{
					if (!createIfNotExists)
					{
						if (this.IsTraceEnabled)
						{
							this.tracer.TraceDebug<DateTime>((long)this.GetHashCode(), "No matching log subfolder found. Lookup time={0}", timestamp);
						}
						return false;
					}
					AuditLogCollection.LogInfo value2 = default(AuditLogCollection.LogInfo);
					string logFolderNameAndRange = AuditLogCollection.GetLogFolderNameAndRange(timestamp, out value2.RangeStart, out value2.RangeEnd);
					using (Folder folder2 = Folder.Create(this.storeSession, this.auditLogRootId, StoreObjectType.Folder, logFolderNameAndRange, CreateMode.OpenIfExists))
					{
						if (folder2.Id == null)
						{
							folder2.Save();
							folder2.Load();
						}
						value2.FolderId = folder2.Id;
						value2.ItemCount = folder2.ItemCount;
						logInfo = new AuditLogCollection.LogInfo?(value2);
						if (this.IsTraceEnabled)
						{
							this.tracer.TraceDebug<DateTime, DateTime, string>((long)this.GetHashCode(), "New log subfolder created. StartTime=[{0}],EndTime=[{1}],FolderId=[{2}]", value2.RangeStart, value2.RangeEnd, value2.FolderId.ToBase64String());
						}
						goto IL_1AA;
					}
				}
				if (this.IsTraceEnabled)
				{
					this.tracer.TraceDebug<DateTime, DateTime, string>((long)this.GetHashCode(), "Found matching log subfolder StartTime=[{0}],EndTime=[{1}],FolderId=[{2}]", logInfo.Value.RangeStart, logInfo.Value.RangeEnd, logInfo.Value.FolderId.ToBase64String());
				}
				IL_1AA:
				auditLog = new AuditLog(this.storeSession, logInfo.Value.FolderId, logInfo.Value.RangeStart, logInfo.Value.RangeEnd, logInfo.Value.ItemCount, this.recordFormatter);
			}
			return true;
		}

		// Token: 0x06008646 RID: 34374 RVA: 0x0024C528 File Offset: 0x0024A728
		public static string GetLogFolderNameAndRange(DateTime timestamp, out DateTime rangeStart, out DateTime rangeEnd)
		{
			string text = timestamp.ToString(AuditLogCollection.DailyLogFolderNameFormat);
			if (!AuditLogCollection.TryParseLogRange(text, out rangeStart, out rangeEnd))
			{
				throw new InvalidOperationException("Invalid log folder name");
			}
			return text;
		}

		// Token: 0x06008647 RID: 34375 RVA: 0x0024C558 File Offset: 0x0024A758
		public static bool TryParseLogRange(string logName, out DateTime rangeStart, out DateTime rangeEnd)
		{
			rangeStart = default(DateTime);
			rangeEnd = default(DateTime);
			if (string.IsNullOrEmpty(logName))
			{
				return false;
			}
			bool flag = DateTime.TryParseExact(logName, AuditLogCollection.DailyLogFolderNameFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out rangeStart);
			if (flag)
			{
				rangeEnd = rangeStart.AddDays(1.0);
			}
			return flag;
		}

		// Token: 0x17002399 RID: 9113
		// (get) Token: 0x06008648 RID: 34376 RVA: 0x0024C5AA File Offset: 0x0024A7AA
		private bool IsTraceEnabled
		{
			get
			{
				return this.tracer != null && this.tracer.IsTraceEnabled(TraceType.DebugTrace);
			}
		}

		// Token: 0x06008649 RID: 34377 RVA: 0x0024C8CC File Offset: 0x0024AACC
		private IEnumerable<AuditLog> InternalGetAuditLogs(bool includeEmptyFolders, Func<AuditLogCollection.LogInfo, bool> predicate)
		{
			using (Folder rootFolder = Folder.Bind(this.storeSession, this.auditLogRootId))
			{
				foreach (AuditLogCollection.LogInfo logInfo in this.FindLogFolders(rootFolder, includeEmptyFolders))
				{
					if (predicate == null || predicate(logInfo))
					{
						yield return new AuditLog(this.storeSession, logInfo.FolderId, logInfo.RangeStart, logInfo.RangeEnd, logInfo.ItemCount, this.recordFormatter);
					}
				}
				if (rootFolder.ItemCount > 0)
				{
					yield return new AuditLog(this.storeSession, this.auditLogRootId, DateTime.MinValue, DateTime.MaxValue, rootFolder.ItemCount, this.recordFormatter);
				}
			}
			yield break;
		}

		// Token: 0x0600864A RID: 34378 RVA: 0x0024CC0C File Offset: 0x0024AE0C
		private IEnumerable<AuditLogCollection.LogInfo> FindLogFolders(Folder rootFolder, bool includeEmptyFolders)
		{
			if (rootFolder.HasSubfolders)
			{
				using (QueryResult queryResult = rootFolder.FolderQuery(FolderQueryFlags.None, null, AuditLogCollection.SortByDisplayName, AuditLogCollection.FolderProperties))
				{
					object[][] rows;
					do
					{
						rows = queryResult.GetRows(100);
						if (rows != null && rows.Length > 0)
						{
							foreach (object[] row in rows)
							{
								DateTime logRangeStart;
								DateTime logRangeEnd;
								bool validLogSubfolder = AuditLogCollection.TryParseLogRange(row[0] as string, out logRangeStart, out logRangeEnd);
								int itemCount = AuditLogCollection.GetItemCount(row[2]);
								if (validLogSubfolder && (includeEmptyFolders || itemCount > 0))
								{
									yield return new AuditLogCollection.LogInfo
									{
										FolderId = (StoreId)row[1],
										RangeStart = logRangeStart,
										RangeEnd = logRangeEnd,
										ItemCount = itemCount
									};
								}
							}
						}
					}
					while (rows != null && rows.Length > 0);
				}
			}
			yield break;
		}

		// Token: 0x0600864B RID: 34379 RVA: 0x0024CC37 File Offset: 0x0024AE37
		private static int GetItemCount(object itemCountValue)
		{
			if (itemCountValue == null || !(itemCountValue is int))
			{
				return 0;
			}
			return (int)itemCountValue;
		}

		// Token: 0x040059D8 RID: 23000
		private const int FolderQueryBatchSize = 100;

		// Token: 0x040059D9 RID: 23001
		private static string DailyLogFolderNameFormat = "yyyy-MM-dd";

		// Token: 0x040059DA RID: 23002
		private static readonly SortBy[] SortByDisplayName = new SortBy[]
		{
			new SortBy(FolderSchema.DisplayName, SortOrder.Ascending)
		};

		// Token: 0x040059DB RID: 23003
		private static readonly PropertyDefinition[] FolderProperties = new PropertyDefinition[]
		{
			FolderSchema.DisplayName,
			FolderSchema.Id,
			FolderSchema.ItemCount
		};

		// Token: 0x040059DC RID: 23004
		private StoreSession storeSession;

		// Token: 0x040059DD RID: 23005
		private StoreId auditLogRootId;

		// Token: 0x040059DE RID: 23006
		private Trace tracer;

		// Token: 0x040059DF RID: 23007
		private Func<IAuditLogRecord, MessageItem, int> recordFormatter;

		// Token: 0x02000F45 RID: 3909
		private enum FolderPropertiesIndex
		{
			// Token: 0x040059E1 RID: 23009
			IdxDisplayName,
			// Token: 0x040059E2 RID: 23010
			IdxId,
			// Token: 0x040059E3 RID: 23011
			IdxItemCount,
			// Token: 0x040059E4 RID: 23012
			IdxLast
		}

		// Token: 0x02000F46 RID: 3910
		private struct LogInfo
		{
			// Token: 0x040059E5 RID: 23013
			public StoreId FolderId;

			// Token: 0x040059E6 RID: 23014
			public DateTime RangeStart;

			// Token: 0x040059E7 RID: 23015
			public DateTime RangeEnd;

			// Token: 0x040059E8 RID: 23016
			public int ItemCount;
		}
	}
}
