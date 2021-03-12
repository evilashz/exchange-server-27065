using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200013E RID: 318
	[Serializable]
	public sealed class SyncWorkItem : WorkItemBase
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00020078 File Offset: 0x0001E278
		public SyncWorkItem(string externalIdentity, bool processNow, TenantContext tenantContext, SyncChangeInfo[] changeInfoList, string syncSvcUrl, bool fullSyncForTenant, Workload workload, bool hasPersistentBackup = false) : this(externalIdentity, default(DateTime), processNow, tenantContext, changeInfoList, syncSvcUrl, SyncWorkItem.DefaultMaxExecuteDelayTime, fullSyncForTenant, workload, hasPersistentBackup)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000200C4 File Offset: 0x0001E2C4
		internal SyncWorkItem(string externalIdentity, DateTime executeTimeUTC, bool processNow, TenantContext tenantContext, SyncChangeInfo[] changeInfoList, string syncSvcUrl, TimeSpan maxExecuteDelayTime, bool fullSyncForTenant, Workload workload, bool hasPersistentBackup = false) : base(externalIdentity, executeTimeUTC, processNow, tenantContext, hasPersistentBackup)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("externalIdentity", externalIdentity);
			ArgumentValidator.ThrowIfNullOrEmpty("syncSvcUrl", syncSvcUrl);
			ArgumentValidator.ThrowIfNegativeTimeSpan("maxExecuteDelayTime", maxExecuteDelayTime);
			base.ExternalIdentity = externalIdentity;
			this.syncSvcUrl = syncSvcUrl;
			this.maxExecuteDelayTime = maxExecuteDelayTime;
			this.FirstChangeArriveUTC = DateTime.UtcNow;
			this.LastChangeArriveUTC = this.FirstChangeArriveUTC;
			this.FullSyncForTenant = fullSyncForTenant;
			this.Workload = workload;
			Dictionary<ConfigurationObjectType, List<SyncChangeInfo>> dictionary = null;
			if (changeInfoList != null && changeInfoList.Any<SyncChangeInfo>())
			{
				bool flag = changeInfoList.Any((SyncChangeInfo p) => p.ObjectId != null);
				if (flag && fullSyncForTenant)
				{
					throw new ArgumentException("The changeInfoList contains object-level sync. But the sync type is tenant-level or type-level full sync.");
				}
				dictionary = new Dictionary<ConfigurationObjectType, List<SyncChangeInfo>>();
				foreach (SyncChangeInfo syncChangeInfo in changeInfoList)
				{
					if (dictionary.ContainsKey(syncChangeInfo.ObjectType))
					{
						dictionary[syncChangeInfo.ObjectType].Add(syncChangeInfo);
					}
					else
					{
						dictionary.Add(syncChangeInfo.ObjectType, new List<SyncChangeInfo>
						{
							syncChangeInfo
						});
					}
				}
			}
			if (dictionary == null || !dictionary.Any<KeyValuePair<ConfigurationObjectType, List<SyncChangeInfo>>>())
			{
				this.WorkItemInfo = new Dictionary<ConfigurationObjectType, List<SyncChangeInfo>>
				{
					{
						ConfigurationObjectType.Policy,
						new List<SyncChangeInfo>
						{
							new SyncChangeInfo(ConfigurationObjectType.Policy)
						}
					},
					{
						ConfigurationObjectType.Rule,
						new List<SyncChangeInfo>
						{
							new SyncChangeInfo(ConfigurationObjectType.Rule)
						}
					},
					{
						ConfigurationObjectType.Binding,
						new List<SyncChangeInfo>
						{
							new SyncChangeInfo(ConfigurationObjectType.Binding)
						}
					},
					{
						ConfigurationObjectType.Association,
						new List<SyncChangeInfo>
						{
							new SyncChangeInfo(ConfigurationObjectType.Association)
						}
					}
				};
				return;
			}
			this.WorkItemInfo = dictionary;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00020287 File Offset: 0x0001E487
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0002028F File Offset: 0x0001E48F
		public Dictionary<ConfigurationObjectType, List<SyncChangeInfo>> WorkItemInfo { get; internal set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00020298 File Offset: 0x0001E498
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x000202A0 File Offset: 0x0001E4A0
		public string SyncSvcUrl
		{
			get
			{
				return this.syncSvcUrl;
			}
			internal set
			{
				ArgumentValidator.ThrowIfNullOrEmpty("SyncSvcUrl", value);
				this.syncSvcUrl = value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x000202B4 File Offset: 0x0001E4B4
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x000202BC File Offset: 0x0001E4BC
		public bool FullSyncForTenant { get; internal set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x000202C5 File Offset: 0x0001E4C5
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x000202CD File Offset: 0x0001E4CD
		public Workload Workload { get; internal set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x000202D6 File Offset: 0x0001E4D6
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x000202DE File Offset: 0x0001E4DE
		internal DateTime FirstChangeArriveUTC { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x000202E7 File Offset: 0x0001E4E7
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x000202EF File Offset: 0x0001E4EF
		internal DateTime LastChangeArriveUTC { get; set; }

		// Token: 0x0600097D RID: 2429 RVA: 0x00020350 File Offset: 0x0001E550
		public override bool Merge(WorkItemBase newWorkItem)
		{
			ArgumentValidator.ThrowIfNull("newWorkItem", newWorkItem);
			ArgumentValidator.ThrowIfWrongType("newWorkItem", newWorkItem, typeof(SyncWorkItem));
			SyncWorkItem syncWorkItem = (SyncWorkItem)newWorkItem;
			if (this.FullSyncForTenant)
			{
				return syncWorkItem.FullSyncForTenant;
			}
			if (syncWorkItem.FullSyncForTenant)
			{
				this.WorkItemInfo = syncWorkItem.WorkItemInfo;
				this.FullSyncForTenant = true;
				return true;
			}
			foreach (KeyValuePair<ConfigurationObjectType, List<SyncChangeInfo>> keyValuePair in syncWorkItem.WorkItemInfo)
			{
				ConfigurationObjectType key = keyValuePair.Key;
				List<SyncChangeInfo> value = keyValuePair.Value;
				if (this.WorkItemInfo.ContainsKey(key))
				{
					List<SyncChangeInfo> source = this.WorkItemInfo[key];
					if (value.First<SyncChangeInfo>().ObjectId != null != (source.First<SyncChangeInfo>().ObjectId != null))
					{
						return false;
					}
				}
			}
			foreach (KeyValuePair<ConfigurationObjectType, List<SyncChangeInfo>> keyValuePair2 in syncWorkItem.WorkItemInfo)
			{
				ConfigurationObjectType key2 = keyValuePair2.Key;
				List<SyncChangeInfo> value2 = keyValuePair2.Value;
				if (!this.WorkItemInfo.ContainsKey(key2))
				{
					this.WorkItemInfo.Add(key2, value2);
				}
				else
				{
					List<SyncChangeInfo> list = this.WorkItemInfo[key2];
					using (List<SyncChangeInfo>.Enumerator enumerator3 = value2.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							SyncChangeInfo newChangeInfo = enumerator3.Current;
							SyncChangeInfo syncChangeInfo = list.FirstOrDefault((SyncChangeInfo p) => p.ObjectId == newChangeInfo.ObjectId);
							if (syncChangeInfo != null)
							{
								if ((null != syncChangeInfo.Version && null != newChangeInfo.Version && syncChangeInfo.Version.CompareTo(newChangeInfo.Version) < 0) || (newChangeInfo.ObjectId != null && ChangeType.Delete == newChangeInfo.ChangeType))
								{
									syncChangeInfo.Version = newChangeInfo.Version;
									syncChangeInfo.ChangeType = newChangeInfo.ChangeType;
								}
							}
							else
							{
								list.Add(newChangeInfo);
							}
						}
					}
				}
			}
			base.TryCount = ((base.TryCount < syncWorkItem.TryCount) ? base.TryCount : syncWorkItem.TryCount);
			base.ProcessNow |= syncWorkItem.ProcessNow;
			if (this.LastChangeArriveUTC < syncWorkItem.LastChangeArriveUTC)
			{
				this.LastChangeArriveUTC = syncWorkItem.LastChangeArriveUTC;
				base.TenantContext = syncWorkItem.TenantContext;
				this.SyncSvcUrl = syncWorkItem.SyncSvcUrl;
			}
			if (this.FirstChangeArriveUTC > syncWorkItem.FirstChangeArriveUTC)
			{
				this.FirstChangeArriveUTC = syncWorkItem.FirstChangeArriveUTC;
			}
			if (this.LastChangeArriveUTC - this.FirstChangeArriveUTC < this.maxExecuteDelayTime && base.ExecuteTimeUTC < syncWorkItem.ExecuteTimeUTC)
			{
				base.ExecuteTimeUTC = syncWorkItem.ExecuteTimeUTC;
			}
			return true;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000206D4 File Offset: 0x0001E8D4
		public override bool IsEqual(WorkItemBase newWorkItem)
		{
			return this == newWorkItem;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000206DA File Offset: 0x0001E8DA
		public override Guid GetPrimaryKey()
		{
			return base.TenantContext.TenantId;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000206E8 File Offset: 0x0001E8E8
		internal override bool RoughCompare(object other)
		{
			SyncWorkItem syncWorkItem = other as SyncWorkItem;
			return syncWorkItem != null && (this.FullSyncForTenant == syncWorkItem.FullSyncForTenant && this.SyncSvcUrl.Equals(syncWorkItem.SyncSvcUrl, StringComparison.OrdinalIgnoreCase)) && base.RoughCompare(syncWorkItem);
		}

		// Token: 0x040004DA RID: 1242
		private static readonly TimeSpan DefaultMaxExecuteDelayTime = TimeSpan.FromSeconds(30.0);

		// Token: 0x040004DB RID: 1243
		private readonly TimeSpan maxExecuteDelayTime;

		// Token: 0x040004DC RID: 1244
		private string syncSvcUrl;
	}
}
