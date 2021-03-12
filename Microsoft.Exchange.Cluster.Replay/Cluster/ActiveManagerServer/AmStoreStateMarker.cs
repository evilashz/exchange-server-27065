using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000073 RID: 115
	internal class AmStoreStateMarker
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0001A19B File Offset: 0x0001839B
		internal AmStoreStateMarker()
		{
			this.m_storeStateMap = new Dictionary<AmServerName, AmSystemEventCode>(4);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001A1BC File Offset: 0x000183BC
		internal void SetStoreState(AmServerName serverName, AmSystemEventCode eventCode)
		{
			lock (this.m_locker)
			{
				this.m_storeStateMap[serverName] = eventCode;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001A204 File Offset: 0x00018404
		internal bool CheckIfStoreStartMarkedAndClear(AmServerName serverName)
		{
			bool result;
			lock (this.m_locker)
			{
				result = this.CheckIfEventMarked(serverName, AmSystemEventCode.StoreServiceStarted, true);
			}
			return result;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001A24C File Offset: 0x0001844C
		internal bool CheckIfStoreStopMarkedAndClear(AmServerName serverName)
		{
			bool result;
			lock (this.m_locker)
			{
				result = this.CheckIfEventMarked(serverName, AmSystemEventCode.StoreServiceStopped, true);
			}
			return result;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001A294 File Offset: 0x00018494
		internal bool IsStoreGracefullyStoppedOn(AmServerName serverName)
		{
			bool result;
			lock (this.m_locker)
			{
				result = this.CheckIfEventMarked(serverName, AmSystemEventCode.StoreServiceStopped, false);
			}
			return result;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001A2DC File Offset: 0x000184DC
		internal bool Clear(AmServerName serverName)
		{
			bool result;
			lock (this.m_locker)
			{
				result = this.m_storeStateMap.Remove(serverName);
			}
			return result;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001A324 File Offset: 0x00018524
		internal void ClearAllStoreStartRequests()
		{
			lock (this.m_locker)
			{
				List<AmServerName> list = new List<AmServerName>();
				foreach (AmServerName amServerName in this.m_storeStateMap.Keys)
				{
					if (this.CheckIfEventMarked(amServerName, AmSystemEventCode.StoreServiceStarted, false))
					{
						list.Add(amServerName);
					}
				}
				foreach (AmServerName serverName in list)
				{
					this.CheckIfStoreStartMarkedAndClear(serverName);
				}
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001A3F8 File Offset: 0x000185F8
		internal void Clear()
		{
			lock (this.m_locker)
			{
				this.m_storeStateMap.Clear();
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001A440 File Offset: 0x00018640
		private bool CheckIfEventMarked(AmServerName serverName, AmSystemEventCode compareEventCode, bool isRemove)
		{
			AmSystemEventCode amSystemEventCode;
			if (this.m_storeStateMap.TryGetValue(serverName, out amSystemEventCode) && amSystemEventCode == compareEventCode)
			{
				if (isRemove)
				{
					this.m_storeStateMap.Remove(serverName);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0400020E RID: 526
		private object m_locker = new object();

		// Token: 0x0400020F RID: 527
		private Dictionary<AmServerName, AmSystemEventCode> m_storeStateMap;
	}
}
