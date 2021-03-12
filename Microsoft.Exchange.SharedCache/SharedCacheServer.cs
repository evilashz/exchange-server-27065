using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.SharedCache;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.SharedCache;
using Microsoft.Exchange.SharedCache.EventLog;
using Microsoft.Exchange.SharedCache.Exceptions;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x0200001D RID: 29
	internal sealed class SharedCacheServer : SharedCacheRpcServer
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000401E File Offset: 0x0000221E
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004025 File Offset: 0x00002225
		public static bool IsRunning
		{
			get
			{
				return SharedCacheServer.serverStarted;
			}
			private set
			{
				SharedCacheServer.serverStarted = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000402D File Offset: 0x0000222D
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004034 File Offset: 0x00002234
		public static bool AccessibleByLocalSystemOnly
		{
			get
			{
				return SharedCacheServer.accessibleByLocalSystemOnly;
			}
			private set
			{
				SharedCacheServer.accessibleByLocalSystemOnly = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000403C File Offset: 0x0000223C
		internal static ReadOnlyDictionary<Guid, ISharedCache> RegisteredCaches
		{
			get
			{
				return new ReadOnlyDictionary<Guid, ISharedCache>(SharedCacheServer.registeredCaches);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004048 File Offset: 0x00002248
		public static bool StartInsecureMode()
		{
			Diagnostics.Logger.LogEvent(MSExchangeSharedCacheEventLogConstants.Tuple_ServiceStarting, null, new object[0]);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			bool flag = SharedCacheServer.InternalStart(securityIdentifier);
			if (flag)
			{
				SharedCacheServer.AccessibleByLocalSystemOnly = false;
				Diagnostics.Logger.LogEvent(MSExchangeSharedCacheEventLogConstants.Tuple_ServiceStarted, null, new object[0]);
			}
			return flag;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000409C File Offset: 0x0000229C
		public static bool Start()
		{
			Diagnostics.Logger.LogEvent(MSExchangeSharedCacheEventLogConstants.Tuple_ServiceStarting, null, new object[0]);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			bool flag = SharedCacheServer.InternalStart(securityIdentifier);
			if (flag)
			{
				SharedCacheServer.AccessibleByLocalSystemOnly = true;
				Diagnostics.Logger.LogEvent(MSExchangeSharedCacheEventLogConstants.Tuple_ServiceStarted, null, new object[0]);
			}
			return flag;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000040F4 File Offset: 0x000022F4
		public static void Stop()
		{
			lock (SharedCacheServer.syncLock)
			{
				if (!SharedCacheServer.IsRunning)
				{
					throw new InvalidOperationException("Server not running");
				}
				if (SharedCacheServer.server != null)
				{
					Diagnostics.Logger.LogEvent(MSExchangeSharedCacheEventLogConstants.Tuple_ServiceStopping, null, new object[0]);
					RpcServerBase.StopServer(SharedCacheRpcServer.RpcIntfHandle);
					SharedCacheServer.server = null;
					foreach (ISharedCache sharedCache in SharedCacheServer.registeredCaches.Values)
					{
						sharedCache.Dispose();
					}
					ExTraceGlobals.ServerTracer.TraceDebug(0L, "Unregistering from Get-ExchangeDiagnosticsInfo");
					ExchangeDiagnosticsHelper.UnRegisterDiagnosticsComponents();
					SharedCacheServer.registeredCaches = null;
					SharedCacheServer.IsRunning = false;
					Diagnostics.Logger.LogEvent(MSExchangeSharedCacheEventLogConstants.Tuple_ServiceStopped, null, new object[0]);
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000041F0 File Offset: 0x000023F0
		public static void RegisterCache(Guid cacheGuid, ISharedCache sharedCache)
		{
			if (sharedCache == null)
			{
				throw new ArgumentNullException("sharedCache");
			}
			lock (SharedCacheServer.syncLock)
			{
				if (!SharedCacheServer.IsRunning)
				{
					throw new InvalidOperationException("Server not running");
				}
				try
				{
					SharedCacheServer.registeredCaches.Add(cacheGuid, sharedCache);
					Diagnostics.Logger.LogEvent(MSExchangeSharedCacheEventLogConstants.Tuple_CacheRegistered, null, new object[]
					{
						sharedCache.Name,
						cacheGuid.ToString()
					});
				}
				catch (ArgumentException)
				{
					throw new CacheAlreadyRegisteredException(cacheGuid);
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000042A0 File Offset: 0x000024A0
		public static void UnregisterCache(Guid cacheGuid)
		{
			lock (SharedCacheServer.syncLock)
			{
				if (!SharedCacheServer.IsRunning)
				{
					throw new InvalidOperationException("Server not running");
				}
				ISharedCache sharedCache = null;
				if (!SharedCacheServer.registeredCaches.TryGetValue(cacheGuid, out sharedCache))
				{
					ExTraceGlobals.ServerTracer.TraceWarning(0L, "Didn't unregister cache as cache wasn't found: " + cacheGuid.ToString());
					throw new CacheNotRegisteredException(cacheGuid);
				}
				sharedCache.Dispose();
				SharedCacheServer.registeredCaches.Remove(cacheGuid);
				ExTraceGlobals.ServerTracer.TraceDebug(0L, "Unregistered cache: " + cacheGuid.ToString());
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004374 File Offset: 0x00002574
		public override void Get(Guid cacheGuid, string key, ref CacheResponse response)
		{
			response = this.GetCacheAndExecute(cacheGuid, SharedCacheServer.GenerateDiagnosticsString("Get", key), (ISharedCache cache) => cache.Get(key));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000043E8 File Offset: 0x000025E8
		public override void Insert(Guid cacheGuid, string key, byte[] value, long entryValidAsOfTime, ref CacheResponse response)
		{
			response = this.GetCacheAndExecute(cacheGuid, SharedCacheServer.GenerateDiagnosticsString("Insert", key), delegate(ISharedCache cache)
			{
				DateTime entryValidAsOfTime2 = DateTime.FromBinary(entryValidAsOfTime);
				return cache.Insert(key, value, entryValidAsOfTime2);
			});
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004450 File Offset: 0x00002650
		public override void Delete(Guid cacheGuid, string key, ref CacheResponse response)
		{
			response = this.GetCacheAndExecute(cacheGuid, SharedCacheServer.GenerateDiagnosticsString("Delete", key), (ISharedCache cache) => cache.Delete(key));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000044B4 File Offset: 0x000026B4
		internal static bool TryGetCacheByName(string name, out ISharedCache sharedCache)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			sharedCache = SharedCacheServer.RegisteredCaches.FirstOrDefault((KeyValuePair<Guid, ISharedCache> c) => c.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).Value;
			return sharedCache != null;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004508 File Offset: 0x00002708
		internal static bool IsCacheRegistered(Guid cacheGuid)
		{
			bool result;
			lock (SharedCacheServer.syncLock)
			{
				if (SharedCacheServer.registeredCaches != null)
				{
					result = SharedCacheServer.registeredCaches.ContainsKey(cacheGuid);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000455C File Offset: 0x0000275C
		private static bool InternalStart(SecurityIdentifier securityIdentifier)
		{
			bool result;
			lock (SharedCacheServer.syncLock)
			{
				if (SharedCacheServer.IsRunning)
				{
					throw new InvalidOperationException("Server already running");
				}
				ExTraceGlobals.ServerTracer.TraceDebug(0L, "Registering our components with Get-ExchangeDiagnosticsInfo");
				ExchangeDiagnosticsHelper.RegisterDiagnosticsComponents();
				FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.Read, AccessControlType.Allow);
				FileSecurity fileSecurity = new FileSecurity();
				fileSecurity.SetOwner(securityIdentifier);
				fileSecurity.SetAccessRule(accessRule);
				try
				{
					SharedCacheServer.server = (SharedCacheServer)RpcServerBase.RegisterServer(typeof(SharedCacheServer), fileSecurity, 1, true);
					SharedCacheServer.registeredCaches = new Dictionary<Guid, ISharedCache>(1);
					SharedCacheServer.IsRunning = true;
					result = true;
				}
				catch (RpcException ex)
				{
					ExTraceGlobals.ServerTracer.TraceError(0L, ex.ToString());
					throw;
				}
			}
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004634 File Offset: 0x00002834
		private static string GenerateDiagnosticsString(string operation, string key)
		{
			return string.Format("{0}~{1}", operation, key);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004644 File Offset: 0x00002844
		private CacheResponse GetCacheAndExecute(Guid cacheGuid, string diagnosticsString, Func<ISharedCache, CacheResponse> action)
		{
			ISharedCache sharedCache = null;
			CacheResponse cacheResponse = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			ExTraceGlobals.ServerTracer.TraceDebug((long)this.GetHashCode(), diagnosticsString);
			try
			{
				if (!SharedCacheServer.registeredCaches.TryGetValue(cacheGuid, out sharedCache))
				{
					throw new CacheNotRegisteredException(cacheGuid);
				}
				cacheResponse = action(sharedCache);
			}
			catch (SharedCacheExceptionBase sharedCacheExceptionBase)
			{
				cacheResponse = CacheResponse.Create(sharedCacheExceptionBase.ResponseCode);
				cacheResponse.AppendDiagInfo("Exception", sharedCacheExceptionBase.ToString());
				if (sharedCache != null)
				{
					sharedCache.PerformanceCounters.IncrementFailedRequests();
				}
			}
			catch (Exception ex)
			{
				cacheResponse = CacheResponse.Create(ResponseCode.InternalServerError);
				cacheResponse.AppendDiagInfo("Exception", ex.ToString());
				if (sharedCache != null)
				{
					sharedCache.PerformanceCounters.IncrementFailedRequests();
				}
				Diagnostics.ReportException(ex, MSExchangeSharedCacheEventLogConstants.Tuple_UnhandledException, false, this, "Unhandled exception during GetCacheAndExecute(diag=" + diagnosticsString + "): {0}");
			}
			finally
			{
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				cacheResponse.AppendDiagInfo("Op", diagnosticsString);
				cacheResponse.AppendDiagInfo("TS", elapsedMilliseconds.ToString() + "ms");
				if (sharedCache != null)
				{
					sharedCache.PerformanceCounters.UpdateAverageLatency(elapsedMilliseconds);
				}
			}
			return cacheResponse;
		}

		// Token: 0x04000056 RID: 86
		private static SharedCacheServer server = null;

		// Token: 0x04000057 RID: 87
		private static object syncLock = new object();

		// Token: 0x04000058 RID: 88
		private static bool serverStarted = false;

		// Token: 0x04000059 RID: 89
		private static bool accessibleByLocalSystemOnly = true;

		// Token: 0x0400005A RID: 90
		private static Dictionary<Guid, ISharedCache> registeredCaches;
	}
}
