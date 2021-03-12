using System;
using System.IO;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay.Dumpster
{
	// Token: 0x02000177 RID: 375
	internal class SafetyNetInfoCache
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00040649 File Offset: 0x0003E849
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.DumpsterTracer;
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00040650 File Offset: 0x0003E850
		public SafetyNetInfoCache(string dbGuidStr, string dbName)
		{
			this.m_dbGuidStr = dbGuidStr;
			this.m_stateSafetyNetInfo = new SafetyNetRegKey(dbGuidStr, dbName);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0004066C File Offset: 0x0003E86C
		public bool IsRedeliveryRequired(bool readThrough, bool fThrow)
		{
			SafetyNetInfoHashTable safetyNetInfosReadThrough;
			if (readThrough)
			{
				safetyNetInfosReadThrough = this.GetSafetyNetInfosReadThrough();
			}
			else
			{
				Exception safetyNetInfosCached = this.GetSafetyNetInfosCached(out safetyNetInfosReadThrough);
				if (safetyNetInfosCached != null)
				{
					if (fThrow)
					{
						throw safetyNetInfosCached;
					}
					SafetyNetInfoCache.Tracer.TraceError<string>((long)this.GetHashCode(), "IsRedeliveryRequired() for DB '{0}' returning true as a safeguard since cluster DB read failed.", this.m_dbGuidStr);
					return true;
				}
			}
			return safetyNetInfosReadThrough.RedeliveryRequired;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x000406BC File Offset: 0x0003E8BC
		public string RedeliveryServers
		{
			get
			{
				string result = string.Empty;
				SafetyNetInfoHashTable safetyNetInfoHashTable;
				if (this.GetSafetyNetInfosCached(out safetyNetInfoHashTable) == null)
				{
					result = safetyNetInfoHashTable.RedeliveryServers;
				}
				return result;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x000406E4 File Offset: 0x0003E8E4
		public DateTime RedeliveryStartTime
		{
			get
			{
				DateTime result = DateTime.MinValue;
				SafetyNetInfoHashTable safetyNetInfoHashTable;
				if (this.GetSafetyNetInfosCached(out safetyNetInfoHashTable) == null)
				{
					result = safetyNetInfoHashTable.RedeliveryStartTime;
				}
				return result;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0004070C File Offset: 0x0003E90C
		public DateTime RedeliveryEndTime
		{
			get
			{
				DateTime result = DateTime.MinValue;
				SafetyNetInfoHashTable safetyNetInfoHashTable;
				if (this.GetSafetyNetInfosCached(out safetyNetInfoHashTable) == null)
				{
					result = safetyNetInfoHashTable.RedeliveryEndTime;
				}
				return result;
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00040734 File Offset: 0x0003E934
		public Exception GetSafetyNetInfosCached(out SafetyNetInfoHashTable safetyNetInfos)
		{
			SafetyNetInfoCache.InnerTable innerTable = this.m_currentTable;
			DateTime utcNow = DateTime.UtcNow;
			Exception ex = null;
			safetyNetInfos = null;
			if (this.IsReadThroughNeeded(innerTable))
			{
				lock (this)
				{
					innerTable = this.m_currentTable;
					if (this.IsReadThroughNeeded(innerTable))
					{
						try
						{
							innerTable = this.ReadNewTable();
							this.m_currentTable = innerTable;
							this.m_lastTableLoadException = null;
						}
						catch (DumpsterRedeliveryException ex2)
						{
							ex = ex2;
						}
						catch (IOException ex3)
						{
							ex = ex3;
						}
						catch (ClusterException ex4)
						{
							ex = ex4;
						}
						if (ex != null)
						{
							this.m_lastTableLoadException = ex;
							SafetyNetInfoCache.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "GetSafetyNetInfosCached() for '{0}' failed to load the new table: {1}", this.m_dbGuidStr, this.m_lastTableLoadException);
						}
					}
				}
			}
			if (innerTable != null)
			{
				safetyNetInfos = innerTable.Table;
			}
			return ex;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00040820 File Offset: 0x0003EA20
		public SafetyNetInfoHashTable GetSafetyNetInfosReadThrough()
		{
			SafetyNetInfoHashTable table;
			lock (this)
			{
				this.m_currentTable = this.ReadNewTable();
				table = this.m_currentTable.Table;
			}
			return table;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00040870 File Offset: 0x0003EA70
		public void Reset(SafetyNetInfo safetyNetInfo)
		{
			lock (this)
			{
				safetyNetInfo.RedeliveryRequired = false;
				this.Update(safetyNetInfo);
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x000408B4 File Offset: 0x0003EAB4
		public void Update(SafetyNetInfo safetyNetInfo)
		{
			lock (this)
			{
				if (safetyNetInfo.IsModified())
				{
					this.m_stateSafetyNetInfo.WriteRequestInfo(safetyNetInfo);
					safetyNetInfo.ClearModified();
					this.m_currentTable = null;
				}
				else
				{
					SafetyNetInfoCache.Tracer.TraceDebug<string, SafetyNetInfo>((long)this.GetHashCode(), "Update() for DB '{0}' skipping ClusDB update since redelivery request has not been modified. SafetyNetInfo = {1}", this.m_dbGuidStr, safetyNetInfo);
				}
			}
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0004092C File Offset: 0x0003EB2C
		public void Delete(SafetyNetInfo safetyNetInfo)
		{
			lock (this)
			{
				this.m_stateSafetyNetInfo.DeleteRequest(safetyNetInfo);
				this.m_currentTable = null;
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00040974 File Offset: 0x0003EB74
		private bool IsReadThroughNeeded(SafetyNetInfoCache.InnerTable cachedTable)
		{
			return cachedTable == null || cachedTable.CreateTimeUtc < DateTime.UtcNow - SafetyNetInfoCache.CacheTTL;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00040998 File Offset: 0x0003EB98
		private SafetyNetInfoCache.InnerTable ReadNewTable()
		{
			SafetyNetInfoCache.InnerTable innerTable = new SafetyNetInfoCache.InnerTable();
			innerTable.Table = new SafetyNetInfoHashTable();
			foreach (SafetyNetRequestKey safetyNetRequestKey in this.m_stateSafetyNetInfo.ReadRequestKeys())
			{
				SafetyNetInfo prevInfo = null;
				if (this.m_currentTable != null)
				{
					this.m_currentTable.Table.TryGetValue(safetyNetRequestKey, out prevInfo);
				}
				SafetyNetInfo safetyNetInfo = this.m_stateSafetyNetInfo.ReadRequestInfo(safetyNetRequestKey, prevInfo);
				if (safetyNetInfo != null)
				{
					innerTable.Table.Add(safetyNetRequestKey, safetyNetInfo);
				}
			}
			innerTable.CreateTimeUtc = DateTime.UtcNow;
			return innerTable;
		}

		// Token: 0x04000641 RID: 1601
		private static readonly TimeSpan CacheTTL = TimeSpan.FromSeconds((double)RegistryParameters.DumpsterInfoCacheTTLInSec);

		// Token: 0x04000642 RID: 1602
		private readonly string m_dbGuidStr;

		// Token: 0x04000643 RID: 1603
		private SafetyNetRegKey m_stateSafetyNetInfo;

		// Token: 0x04000644 RID: 1604
		private SafetyNetInfoCache.InnerTable m_currentTable;

		// Token: 0x04000645 RID: 1605
		private Exception m_lastTableLoadException;

		// Token: 0x02000178 RID: 376
		private class InnerTable
		{
			// Token: 0x04000646 RID: 1606
			public SafetyNetInfoHashTable Table;

			// Token: 0x04000647 RID: 1607
			public DateTime CreateTimeUtc;
		}
	}
}
