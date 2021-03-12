using System;
using System.Collections.Concurrent;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000063 RID: 99
	internal sealed class ScopeNotificationCache
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00019882 File Offset: 0x00017A82
		private ScopeNotificationCache()
		{
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001988C File Offset: 0x00017A8C
		internal static ScopeNotificationCache Instance
		{
			get
			{
				if (ScopeNotificationCache.instance == null)
				{
					lock (ScopeNotificationCache.locker)
					{
						if (ScopeNotificationCache.instance == null)
						{
							ScopeNotificationCache.instance = new ScopeNotificationCache();
						}
					}
				}
				return ScopeNotificationCache.instance;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x000198E4 File Offset: 0x00017AE4
		internal ConcurrentDictionary<string, ScopeNotificationUploadState> ScopeNotificationUploadStates
		{
			get
			{
				if (this.scopeNotificationUploadStates == null)
				{
					this.scopeNotificationUploadStates = new ConcurrentDictionary<string, ScopeNotificationUploadState>(StringComparer.InvariantCultureIgnoreCase);
				}
				return this.scopeNotificationUploadStates;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001991C File Offset: 0x00017B1C
		internal void AddScopeNotificationRawData(ScopeNotificationRawData data)
		{
			this.ScopeNotificationUploadStates.AddOrUpdate(data.NotificationName, new ScopeNotificationUploadState
			{
				Data = data
			}, delegate(string key, ScopeNotificationUploadState existingValue)
			{
				existingValue.Data = data;
				return existingValue;
			});
		}

		// Token: 0x040003FF RID: 1023
		private ConcurrentDictionary<string, ScopeNotificationUploadState> scopeNotificationUploadStates;

		// Token: 0x04000400 RID: 1024
		private static ScopeNotificationCache instance = null;

		// Token: 0x04000401 RID: 1025
		private static object locker = new object();
	}
}
