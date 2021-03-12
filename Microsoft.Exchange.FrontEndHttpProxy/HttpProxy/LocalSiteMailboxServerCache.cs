using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000031 RID: 49
	internal class LocalSiteMailboxServerCache
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00008DBC File Offset: 0x00006FBC
		public static LocalSiteMailboxServerCache Instance
		{
			get
			{
				if (LocalSiteMailboxServerCache.instance == null)
				{
					lock (LocalSiteMailboxServerCache.staticLock)
					{
						if (LocalSiteMailboxServerCache.instance == null)
						{
							LocalSiteMailboxServerCache.instance = new LocalSiteMailboxServerCache();
						}
					}
				}
				return LocalSiteMailboxServerCache.instance;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008E14 File Offset: 0x00007014
		public BackEndServer TryGetRandomE15Server(IRequestContext requestContext)
		{
			if (!LocalSiteMailboxServerCache.CacheLocalSiteLiveE15Servers)
			{
				return null;
			}
			Guid[] array = null;
			try
			{
				this.localSiteServersLock.Wait();
				array = this.localSiteLiveE15Servers.ToArray();
			}
			finally
			{
				this.localSiteServersLock.Release();
			}
			if (array.Length > 0)
			{
				int num = this.random.Next(array.Length);
				int num2 = num;
				BackEndServer backEndServer;
				for (;;)
				{
					Guid database = array[num];
					if (MailboxServerCache.Instance.TryGet(database, requestContext, out backEndServer))
					{
						break;
					}
					num2++;
					if (num2 >= array.Length)
					{
						num2 = 0;
					}
					if (num2 == num)
					{
						goto IL_90;
					}
				}
				ExTraceGlobals.VerboseTracer.TraceDebug<BackEndServer>((long)this.GetHashCode(), "[LocalSiteMailboxServerCache::TryGetRandomE15Server]: Found server {0} from local site E15 server list.", backEndServer);
				return backEndServer;
			}
			IL_90:
			return null;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008EC4 File Offset: 0x000070C4
		internal void Add(Guid database, BackEndServer backEndServer, string resourceForest)
		{
			if (LocalSiteMailboxServerCache.CacheLocalSiteLiveE15Servers && this.IsLocalSiteE15MailboxServer(backEndServer, resourceForest))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<Guid, BackEndServer>((long)this.GetHashCode(), "[LocalSiteMailboxServerCache::Add]: Adding Database {0} on Server {1} to local E15 mailbox server collection.", database, backEndServer);
				try
				{
					this.localSiteServersLock.Wait();
					if (!this.localSiteLiveE15Servers.Contains(database))
					{
						this.localSiteLiveE15Servers.Add(database);
					}
				}
				finally
				{
					this.localSiteServersLock.Release();
				}
				this.UpdateLocalSiteMailboxServerListCounter();
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008F44 File Offset: 0x00007144
		internal void Remove(Guid database)
		{
			if (LocalSiteMailboxServerCache.CacheLocalSiteLiveE15Servers)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<Guid>((long)this.GetHashCode(), "[LocalSiteMailboxServerCache::Remove]: Removing Database {0} from the local E15 mailbox server collection.", database);
				try
				{
					this.localSiteServersLock.Wait();
					this.localSiteLiveE15Servers.Remove(database);
				}
				finally
				{
					this.localSiteServersLock.Release();
				}
				this.UpdateLocalSiteMailboxServerListCounter();
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008FAC File Offset: 0x000071AC
		private bool IsLocalSiteE15MailboxServer(BackEndServer server, string resourceForest)
		{
			if (!server.IsE15OrHigher)
			{
				return false;
			}
			if ((!Utilities.IsPartnerHostedOnly && !VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.NoCrossForestServerLocate.Enabled) || string.IsNullOrEmpty(resourceForest) || string.Equals(HttpProxyGlobals.LocalMachineForest.Member, resourceForest, StringComparison.OrdinalIgnoreCase))
			{
				ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\Cache\\LocalSiteMailboxServerCache.cs", "IsLocalSiteE15MailboxServer", 226);
				Site other = null;
				try
				{
					other = currentServiceTopology.GetSite(server.Fqdn, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\Cache\\LocalSiteMailboxServerCache.cs", "IsLocalSiteE15MailboxServer", 231);
				}
				catch (ServerNotFoundException)
				{
					return false;
				}
				catch (ServerNotInSiteException)
				{
					return false;
				}
				if (HttpProxyGlobals.LocalSite.Member.Equals(other))
				{
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00009070 File Offset: 0x00007270
		private void UpdateLocalSiteMailboxServerListCounter()
		{
			PerfCounters.HttpProxyCacheCountersInstance.BackEndServerCacheLocalServerListCount.RawValue = (long)this.localSiteLiveE15Servers.Count;
		}

		// Token: 0x04000095 RID: 149
		private static readonly bool CacheLocalSiteLiveE15Servers = VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.CacheLocalSiteLiveE15Servers.Enabled;

		// Token: 0x04000096 RID: 150
		private static LocalSiteMailboxServerCache instance;

		// Token: 0x04000097 RID: 151
		private static object staticLock = new object();

		// Token: 0x04000098 RID: 152
		private List<Guid> localSiteLiveE15Servers = new List<Guid>();

		// Token: 0x04000099 RID: 153
		private SemaphoreSlim localSiteServersLock = new SemaphoreSlim(1);

		// Token: 0x0400009A RID: 154
		private Random random = new Random();
	}
}
