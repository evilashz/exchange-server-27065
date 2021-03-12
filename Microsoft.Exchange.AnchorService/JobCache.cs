using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000024 RID: 36
	internal class JobCache : IDiagnosable
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00006434 File Offset: 0x00004634
		internal JobCache(AnchorContext anchorContext)
		{
			this.Context = anchorContext;
			this.sharedDataLock = new object();
			this.cacheEntries = new Dictionary<ADObjectId, CacheEntryBase>(32);
			this.cacheUpdated = new AutoResetEvent(false);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006467 File Offset: 0x00004667
		internal AutoResetEvent CacheUpdated
		{
			get
			{
				return this.cacheUpdated;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000646F File Offset: 0x0000466F
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00006477 File Offset: 0x00004677
		private AnchorContext Context { get; set; }

		// Token: 0x06000195 RID: 405 RVA: 0x00006480 File Offset: 0x00004680
		public string GetDiagnosticComponentName()
		{
			return "JobCache";
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000064B0 File Offset: 0x000046B0
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xml = null;
			AnchorUtil.RunOperationWithCulture(CultureInfo.InvariantCulture, delegate
			{
				xml = this.InternalGetDiagnosticInfo(parameters.Argument);
			});
			return xml;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000064F4 File Offset: 0x000046F4
		internal bool TryAdd(ADUser user, bool wakeCache)
		{
			AnchorUtil.ThrowOnNullArgument(user, "user");
			return this.TryUpdate(this.Context.CreateCacheEntry(user), wakeCache);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006514 File Offset: 0x00004714
		internal bool TryUpdate(CacheEntryBase cacheEntry, bool wakeCache)
		{
			AnchorUtil.ThrowOnNullArgument(cacheEntry, "cacheEntry");
			bool flag = cacheEntry.Validate();
			if (this.Contains(cacheEntry.ObjectId))
			{
				if (!flag)
				{
					lock (this.sharedDataLock)
					{
						this.Context.Logger.Log(MigrationEventType.Warning, "Removing CacheEntry {0} because it's invalid ...", new object[]
						{
							cacheEntry
						});
						this.cacheEntries.Remove(cacheEntry.ObjectId);
						goto IL_9E;
					}
				}
				this.Context.Logger.Log(MigrationEventType.Verbose, "CacheEntry {0} already exists in cache, skipping add", new object[]
				{
					cacheEntry
				});
				return true;
			}
			IL_9E:
			if (!flag)
			{
				return false;
			}
			lock (this.sharedDataLock)
			{
				this.cacheEntries[cacheEntry.ObjectId] = cacheEntry;
			}
			if (wakeCache && this.Context.Config.GetConfig<bool>("ShouldWakeOnCacheUpdate"))
			{
				this.Context.Logger.Log(MigrationEventType.Information, "triggering cache update", new object[0]);
				this.cacheUpdated.Set();
			}
			return true;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006654 File Offset: 0x00004854
		internal void Remove(CacheEntryBase cacheEntry)
		{
			AnchorUtil.ThrowOnNullArgument(cacheEntry, "cacheEntry");
			this.Context.Logger.Log(MigrationEventType.Warning, "Deactivating CacheEntry {0} ...", new object[]
			{
				cacheEntry
			});
			cacheEntry.Deactivate();
			lock (this.sharedDataLock)
			{
				this.cacheEntries.Remove(cacheEntry.ObjectId);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000066D4 File Offset: 0x000048D4
		internal void Clear()
		{
			this.Context.Logger.Log(MigrationEventType.Warning, "Clearing cache", new object[0]);
			lock (this.sharedDataLock)
			{
				this.cacheEntries.Clear();
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006738 File Offset: 0x00004938
		internal List<CacheEntryBase> Get()
		{
			List<CacheEntryBase> list;
			lock (this.sharedDataLock)
			{
				list = new List<CacheEntryBase>(this.cacheEntries.Count);
				foreach (KeyValuePair<ADObjectId, CacheEntryBase> keyValuePair in this.cacheEntries)
				{
					list.Add(keyValuePair.Value);
				}
			}
			return list;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000067D0 File Offset: 0x000049D0
		internal bool Contains(ADObjectId objectId)
		{
			bool result;
			lock (this.sharedDataLock)
			{
				result = this.cacheEntries.ContainsKey(objectId);
			}
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006818 File Offset: 0x00004A18
		private XElement InternalGetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			ICollection<CacheEntryBase> collection = this.Get();
			XElement xelement2 = new XElement("CacheEntryCollection", new XElement("count", collection.Count));
			xelement.Add(xelement2);
			foreach (CacheEntryBase cacheEntryBase in collection)
			{
				XElement diagnosticInfo = cacheEntryBase.GetDiagnosticInfo(argument);
				xelement2.Add(diagnosticInfo);
			}
			return xelement;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000068BC File Offset: 0x00004ABC
		private bool TryGet(ADObjectId objectId, out CacheEntryBase cacheEntry)
		{
			bool result;
			lock (this.sharedDataLock)
			{
				result = this.cacheEntries.TryGetValue(objectId, out cacheEntry);
			}
			return result;
		}

		// Token: 0x04000075 RID: 117
		public const int ExpectedNumMbxsPerDatabase = 2;

		// Token: 0x04000076 RID: 118
		public const int ExpectedNumDBsPerServer = 16;

		// Token: 0x04000077 RID: 119
		private readonly AutoResetEvent cacheUpdated;

		// Token: 0x04000078 RID: 120
		private readonly object sharedDataLock;

		// Token: 0x04000079 RID: 121
		private Dictionary<ADObjectId, CacheEntryBase> cacheEntries;
	}
}
