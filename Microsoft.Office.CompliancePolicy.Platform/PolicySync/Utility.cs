using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200013A RID: 314
	internal static class Utility
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x0001F264 File Offset: 0x0001D464
		public static void DoWorkAndLogIfFail(Action doWork, ExecutionLog logger, string tenantId, string correlationId, ExecutionLog.EventType eventType, string tag, string contextData, bool reThrowKnownException, bool reThrowGrayException = false)
		{
			try
			{
				ExecutionWrapper.DoRetriableWorkAndLogIfFail(doWork, 0U, (Exception ex) => ex is SyncAgentTransientException, logger, "UnifiedPolicySyncAgent", tenantId, correlationId, eventType, tag, contextData, true);
			}
			catch (GrayException ex)
			{
				GrayException ex2;
				logger.LogError("UnifiedPolicySyncAgent", tenantId, correlationId, ex2, contextData, new KeyValuePair<string, object>[0]);
				if (reThrowGrayException)
				{
					throw ex2;
				}
			}
			catch (SyncAgentExceptionBase syncAgentExceptionBase)
			{
				logger.LogError("UnifiedPolicySyncAgent", tenantId, correlationId, syncAgentExceptionBase, contextData, new KeyValuePair<string, object>[0]);
				if (reThrowKnownException)
				{
					throw syncAgentExceptionBase;
				}
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001F300 File Offset: 0x0001D500
		public static IDictionary<string, string> Merge(this IDictionary<string, string> currentDict, IDictionary<string, string> newDict)
		{
			ArgumentValidator.ThrowIfNull("newDict", newDict);
			foreach (KeyValuePair<string, string> item in newDict)
			{
				if (!currentDict.ContainsKey(item.Key))
				{
					currentDict.Add(item);
				}
			}
			return currentDict;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001F364 File Offset: 0x0001D564
		public static Dictionary<K, V> CreateDictionaryFromList<K, V>(List<KeyValuePair<K, V>> list)
		{
			ArgumentValidator.ThrowIfNull("list", list);
			Dictionary<K, V> dictionary = new Dictionary<K, V>();
			foreach (KeyValuePair<K, V> keyValuePair in list)
			{
				if (!dictionary.ContainsKey(keyValuePair.Key))
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		public static string GetThreadPoolStatus()
		{
			int num;
			int num2;
			ThreadPool.GetMaxThreads(out num, out num2);
			int num3;
			int num4;
			ThreadPool.GetAvailableThreads(out num3, out num4);
			return string.Format("ThreadPool Status: max worker thread: {0}; available worker thread: {1}; max IO thread: {2}; available IO thread: {3}.", new object[]
			{
				num,
				num3,
				num2,
				num4
			});
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001F43C File Offset: 0x0001D63C
		public static object GetWholeProperty(object obj, string propertyName)
		{
			object result = null;
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			PropertyInfo property = obj.GetType().GetProperty(propertyName, bindingAttr);
			if (property != null)
			{
				result = property.GetValue(obj);
			}
			return result;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001F470 File Offset: 0x0001D670
		public static bool GetIncrementalProperty(object obj, string propertyName, out object result)
		{
			bool result2 = false;
			result = null;
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			PropertyInfo property = obj.GetType().GetProperty(propertyName, bindingAttr);
			if (property != null)
			{
				object value = property.GetValue(obj);
				if (value != null)
				{
					PropertyInfo property2 = value.GetType().GetProperty("Changed", bindingAttr);
					if (property2 != null && (bool)property2.GetValue(value))
					{
						result2 = true;
						result = value.GetType().GetProperty("Value", bindingAttr).GetValue(value);
					}
				}
			}
			return result2;
		}
	}
}
