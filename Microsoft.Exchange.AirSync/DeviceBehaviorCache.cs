using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000062 RID: 98
	internal static class DeviceBehaviorCache
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x00020788 File Offset: 0x0001E988
		internal static void Start()
		{
			if (GlobalSettings.DeviceBehaviorCacheTimeout > 0)
			{
				lock (DeviceBehaviorCache.synchronizationObject)
				{
					if (DeviceBehaviorCache.deviceBehaviorCache != null)
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, DeviceBehaviorCache.deviceBehaviorCache, "DeviceBehaviorCache is already started.");
					}
					else
					{
						DeviceBehaviorCache.deviceBehaviorCache = new MruDictionaryCache<string, DeviceBehavior>(GlobalSettings.DeviceBehaviorCacheInitialSize, GlobalSettings.DeviceBehaviorCacheMaxSize, GlobalSettings.DeviceBehaviorCacheTimeout);
					}
				}
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00020800 File Offset: 0x0001EA00
		internal static void Stop()
		{
			lock (DeviceBehaviorCache.synchronizationObject)
			{
				if (DeviceBehaviorCache.deviceBehaviorCache != null)
				{
					DeviceBehaviorCache.deviceBehaviorCache.Dispose();
					DeviceBehaviorCache.deviceBehaviorCache = null;
				}
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00020850 File Offset: 0x0001EA50
		public static bool TryGetAndRemoveValue(Guid userGuid, DeviceIdentity deviceIdentity, out DeviceBehavior data)
		{
			string token = DeviceBehaviorCache.GetToken(userGuid, deviceIdentity);
			return DeviceBehaviorCache.TryGetAndRemoveValue(token, out data);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0002086C File Offset: 0x0001EA6C
		public static bool TryGetAndRemoveValue(string token, out DeviceBehavior data)
		{
			bool result;
			lock (DeviceBehaviorCache.synchronizationObject)
			{
				if (DeviceBehaviorCache.deviceBehaviorCache != null)
				{
					result = DeviceBehaviorCache.deviceBehaviorCache.TryGetAndRemoveValue(token, out data);
				}
				else
				{
					data = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000208C4 File Offset: 0x0001EAC4
		public static bool TryGetValue(Guid userGuid, DeviceIdentity deviceIdentity, out DeviceBehavior data)
		{
			string token = DeviceBehaviorCache.GetToken(userGuid, deviceIdentity);
			return DeviceBehaviorCache.TryGetValue(token, out data);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000208E0 File Offset: 0x0001EAE0
		public static bool TryGetValue(string token, out DeviceBehavior data)
		{
			bool result;
			lock (DeviceBehaviorCache.synchronizationObject)
			{
				if (DeviceBehaviorCache.deviceBehaviorCache != null)
				{
					result = DeviceBehaviorCache.deviceBehaviorCache.TryGetValue(token, out data);
				}
				else
				{
					data = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00020938 File Offset: 0x0001EB38
		public static bool ContainsKey(Guid userGuid, DeviceIdentity deviceIdentity)
		{
			string token = DeviceBehaviorCache.GetToken(userGuid, deviceIdentity);
			return DeviceBehaviorCache.ContainsKey(token);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00020954 File Offset: 0x0001EB54
		public static bool ContainsKey(string token)
		{
			bool result;
			lock (DeviceBehaviorCache.synchronizationObject)
			{
				if (DeviceBehaviorCache.deviceBehaviorCache != null)
				{
					result = DeviceBehaviorCache.deviceBehaviorCache.ContainsKey(token);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000209A8 File Offset: 0x0001EBA8
		public static void AddOrReplace(Guid userGuid, DeviceIdentity deviceIdentity, DeviceBehavior data)
		{
			string token = DeviceBehaviorCache.GetToken(userGuid, deviceIdentity);
			DeviceBehaviorCache.AddOrReplace(token, data);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000209C4 File Offset: 0x0001EBC4
		public static void AddOrReplace(string token, DeviceBehavior data)
		{
			lock (DeviceBehaviorCache.synchronizationObject)
			{
				if (DeviceBehaviorCache.deviceBehaviorCache != null)
				{
					DeviceBehaviorCache.deviceBehaviorCache[token] = data;
				}
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00020A10 File Offset: 0x0001EC10
		public static string GetToken(Guid userGuid, DeviceIdentity deviceIdentity)
		{
			ArgumentValidator.ThrowIfNull("userGuid", userGuid);
			ArgumentValidator.ThrowIfNull("deviceIdentity", deviceIdentity);
			return string.Concat(new object[]
			{
				userGuid.ToString(),
				'§',
				deviceIdentity.DeviceType,
				'§',
				deviceIdentity.DeviceId
			});
		}

		// Token: 0x040003D9 RID: 985
		private static object synchronizationObject = new object();

		// Token: 0x040003DA RID: 986
		private static MruDictionaryCache<string, DeviceBehavior> deviceBehaviorCache;
	}
}
