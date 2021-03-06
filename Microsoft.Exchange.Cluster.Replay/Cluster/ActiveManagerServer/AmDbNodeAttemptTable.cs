using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000036 RID: 54
	internal class AmDbNodeAttemptTable
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000EF3A File Offset: 0x0000D13A
		internal bool IsOkayForAction(IADDatabase db, AmServerName nodeName, AmDbActionCode actionCode)
		{
			return this.IsOkayForAction(db, nodeName, actionCode, RegistryParameters.FailureItemStromCoolingDurationInSec);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000EF4C File Offset: 0x0000D14C
		internal bool IsOkayForAction(IADDatabase db, AmServerName nodeName, AmDbActionCode actionCode, int coolingDuration)
		{
			bool result = true;
			if (actionCode == null)
			{
				return true;
			}
			if (!actionCode.IsAutomaticFailureItem)
			{
				return true;
			}
			if (db.ReplicationType == ReplicationType.None)
			{
				return true;
			}
			lock (this.m_locker)
			{
				Dictionary<AmServerName, AmDbAttemptInfo> dictionary = null;
				if (this.m_dbMap.TryGetValue(db.Guid, out dictionary))
				{
					AmDbAttemptInfo amDbAttemptInfo = null;
					if (dictionary.TryGetValue(nodeName, out amDbAttemptInfo))
					{
						DateTime lastAttemptTime = amDbAttemptInfo.LastAttemptTime;
						if ((DateTime)ExDateTime.Now < lastAttemptTime.AddSeconds((double)coolingDuration))
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		internal void MarkFailedTime(Guid dbGuid, AmServerName nodeName, AmDbActionCode actionCode)
		{
			lock (this.m_locker)
			{
				Dictionary<AmServerName, AmDbAttemptInfo> dictionary = null;
				if (!this.m_dbMap.TryGetValue(dbGuid, out dictionary))
				{
					dictionary = new Dictionary<AmServerName, AmDbAttemptInfo>();
					this.m_dbMap[dbGuid] = dictionary;
				}
				AmDbAttemptInfo value = new AmDbAttemptInfo(dbGuid, actionCode, (DateTime)ExDateTime.Now);
				dictionary[nodeName] = value;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000F06C File Offset: 0x0000D26C
		internal void ClearFailedTime(Guid dbGuid, AmServerName nodeName)
		{
			lock (this.m_locker)
			{
				if (nodeName != null)
				{
					Dictionary<AmServerName, AmDbAttemptInfo> dictionary = null;
					if (this.m_dbMap.TryGetValue(dbGuid, out dictionary))
					{
						dictionary.Remove(nodeName);
					}
				}
				else
				{
					this.m_dbMap.Remove(dbGuid);
				}
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		internal void ClearFailedTime(Guid dbGuid)
		{
			this.ClearFailedTime(dbGuid, null);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		internal void ClearFailedTime(AmServerName nodeName)
		{
			lock (this.m_locker)
			{
				foreach (Guid dbGuid in this.GetKeysCopy())
				{
					this.ClearFailedTime(dbGuid, nodeName);
				}
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000F14C File Offset: 0x0000D34C
		internal void ClearFailedTime()
		{
			lock (this.m_locker)
			{
				foreach (Guid dbGuid in this.GetKeysCopy())
				{
					this.ClearFailedTime(dbGuid);
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		private Guid[] GetKeysCopy()
		{
			Guid[] array = new Guid[this.m_dbMap.Keys.Count];
			this.m_dbMap.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x040000FF RID: 255
		private object m_locker = new object();

		// Token: 0x04000100 RID: 256
		private Dictionary<Guid, Dictionary<AmServerName, AmDbAttemptInfo>> m_dbMap = new Dictionary<Guid, Dictionary<AmServerName, AmDbAttemptInfo>>();
	}
}
