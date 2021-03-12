using System;
using System.Linq;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x02000188 RID: 392
	internal class CachedAdReader
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x00049238 File Offset: 0x00047438
		private CachedAdReader()
		{
			int value = HighAvailabilityUtility.NonCachedRegReader.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\Parameters", "AdCacheExpirationInSeconds", 900);
			this.DefaultTimeout = TimeSpan.FromSeconds((double)value);
			this.cachedLocalServerObject = new CachedObject<IADServer>(() => AdObjectLookupHelper.FindLocalServer(CachedAdReader.adLookup.ServerLookup), this.DefaultTimeout, true);
			this.cachedLocalDagObject = new CachedObject<IADDatabaseAvailabilityGroup>(delegate()
			{
				if (this.cachedLocalServerObject.GetValue.DatabaseAvailabilityGroup == null)
				{
					return null;
				}
				return CachedAdReader.adLookup.DagLookup.ReadAdObjectByObjectId(this.cachedLocalServerObject.GetValue.DatabaseAvailabilityGroup);
			}, this.DefaultTimeout, false);
			this.cachedAllDatabases = new CachedObject<IADDatabase[]>(() => AdObjectLookupHelper.GetAllDatabases(Dependencies.ReplayAdObjectLookup.DatabaseLookup, this.cachedLocalServerObject.GetValue), this.DefaultTimeout, false);
			this.cachedServersInDag = new CachedObject<IADServer[]>(delegate()
			{
				IADDatabaseAvailabilityGroup getValue = this.cachedLocalDagObject.GetValue;
				if (getValue == null)
				{
					return null;
				}
				return (from serverId in getValue.Servers
				select CachedAdReader.adLookup.ServerLookup.ReadAdObjectByObjectId(serverId)).ToArray<IADServer>();
			}, this.DefaultTimeout, false);
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00049318 File Offset: 0x00047518
		public static CachedAdReader Instance
		{
			get
			{
				if (CachedAdReader.cachedAdReaderInstance == null)
				{
					lock (CachedAdReader.instanceCreationLock)
					{
						CachedAdReader.cachedAdReaderInstance = new CachedAdReader();
					}
				}
				return CachedAdReader.cachedAdReaderInstance;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00049368 File Offset: 0x00047568
		public IADServer LocalServer
		{
			get
			{
				return this.cachedLocalServerObject.GetValue;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00049375 File Offset: 0x00047575
		public IADDatabaseAvailabilityGroup LocalDAG
		{
			get
			{
				return this.cachedLocalDagObject.GetValue;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00049382 File Offset: 0x00047582
		public IADDatabase[] AllLocalDatabases
		{
			get
			{
				return this.cachedAllDatabases.GetValue;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0004938F File Offset: 0x0004758F
		public IADServer[] AllServersInLocalDag
		{
			get
			{
				return this.cachedServersInDag.GetValue;
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000493FC File Offset: 0x000475FC
		public IADDatabase GetDatabaseOnLocalServer(Guid mdbGuid)
		{
			IADDatabase[] allLocalDatabases = this.AllLocalDatabases;
			if (allLocalDatabases == null || allLocalDatabases.Length < 1)
			{
				return null;
			}
			if ((from db in allLocalDatabases
			select db.Guid).Contains(mdbGuid))
			{
				return (from db in allLocalDatabases
				where db.Guid.Equals(mdbGuid)
				select db).FirstOrDefault<IADDatabase>();
			}
			this.cachedAllDatabases.ForceUpdate();
			allLocalDatabases = this.AllLocalDatabases;
			if (!(from db in allLocalDatabases
			select db.Guid).Contains(mdbGuid))
			{
				throw new HighAvailabilityMAUtilityException(string.Format("MDB with Guid {0} does not exists on this server!", mdbGuid.ToString()));
			}
			return (from db in allLocalDatabases
			where db.Guid.Equals(mdbGuid)
			select db).FirstOrDefault<IADDatabase>();
		}

		// Token: 0x040008AA RID: 2218
		public readonly TimeSpan DefaultTimeout;

		// Token: 0x040008AB RID: 2219
		private static CachedAdReader cachedAdReaderInstance = null;

		// Token: 0x040008AC RID: 2220
		private static object instanceCreationLock = new object();

		// Token: 0x040008AD RID: 2221
		private static IReplayAdObjectLookup adLookup = Dependencies.ReplayAdObjectLookup;

		// Token: 0x040008AE RID: 2222
		private CachedObject<IADServer> cachedLocalServerObject;

		// Token: 0x040008AF RID: 2223
		private CachedObject<IADDatabaseAvailabilityGroup> cachedLocalDagObject;

		// Token: 0x040008B0 RID: 2224
		private CachedObject<IADDatabase[]> cachedAllDatabases;

		// Token: 0x040008B1 RID: 2225
		private CachedObject<IADServer[]> cachedServersInDag;
	}
}
