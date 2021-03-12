using System;
using System.Collections.Concurrent;
using System.ServiceModel;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000113 RID: 275
	internal static class PolicySyncUtils
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x000173BC File Offset: 0x000155BC
		public static void DisposeWcfClientGracefully(ICommunicationObject client, ExecutionLog logProvider, bool skipDispose = false)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					if (client == null)
					{
						return;
					}
					bool flag = false;
					try
					{
						if (client.State != CommunicationState.Faulted)
						{
							client.Close();
							flag = true;
						}
					}
					catch (FaultException<PolicySyncTransientFault> faultException)
					{
						logProvider.LogOneEntry(PolicySyncUtils.clientComponentName, null, ExecutionLog.EventType.Warning, "Transient Exception: " + faultException.InnerException.ToString(), null);
					}
					catch (FaultException<PolicySyncPermanentFault> faultException2)
					{
						logProvider.LogOneEntry(PolicySyncUtils.clientComponentName, null, ExecutionLog.EventType.Warning, "Permanent Exception: " + faultException2.InnerException.ToString(), null);
					}
					finally
					{
						if (!flag)
						{
							client.Abort();
						}
						if (!skipDispose)
						{
							((IDisposable)client).Dispose();
						}
					}
				});
			}
			catch (GrayException ex)
			{
				logProvider.LogOneEntry(PolicySyncUtils.clientComponentName, null, ExecutionLog.EventType.Warning, "Grey Exception:" + ex.ToString(), null);
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00017434 File Offset: 0x00015634
		public static bool Implies(bool a, bool b)
		{
			return (a && b) || !a;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00017460 File Offset: 0x00015660
		public static V GetOrAddSafe<T, V>(this ConcurrentDictionary<T, Lazy<V>> dictionary, T key, Func<T, V> valueFactory)
		{
			Lazy<V> orAdd = dictionary.GetOrAdd(key, new Lazy<V>(() => valueFactory(key)));
			return orAdd.Value;
		}

		// Token: 0x04000427 RID: 1063
		private static readonly string clientComponentName = "UnifiedPolicySyncAgent";

		// Token: 0x02000114 RID: 276
		public class ServiceProxyPoolErrorData
		{
			// Token: 0x0600078B RID: 1931 RVA: 0x000174B1 File Offset: 0x000156B1
			public ServiceProxyPoolErrorData()
			{
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x000174B9 File Offset: 0x000156B9
			public ServiceProxyPoolErrorData(string periodicKey, string debugMessage, int numberOfRetries)
			{
				this.PeriodicKey = periodicKey;
				this.DebugMessage = debugMessage;
				this.NumberOfRetries = numberOfRetries;
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x0600078D RID: 1933 RVA: 0x000174D6 File Offset: 0x000156D6
			// (set) Token: 0x0600078E RID: 1934 RVA: 0x000174DE File Offset: 0x000156DE
			public string PeriodicKey { get; set; }

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x0600078F RID: 1935 RVA: 0x000174E7 File Offset: 0x000156E7
			// (set) Token: 0x06000790 RID: 1936 RVA: 0x000174EF File Offset: 0x000156EF
			public string DebugMessage { get; set; }

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06000791 RID: 1937 RVA: 0x000174F8 File Offset: 0x000156F8
			// (set) Token: 0x06000792 RID: 1938 RVA: 0x00017500 File Offset: 0x00015700
			public int NumberOfRetries { get; set; }

			// Token: 0x06000793 RID: 1939 RVA: 0x0001750C File Offset: 0x0001570C
			public static PolicySyncUtils.ServiceProxyPoolErrorData GetFromString(string data)
			{
				string[] array = data.Split(new char[]
				{
					';'
				});
				return new PolicySyncUtils.ServiceProxyPoolErrorData
				{
					PeriodicKey = array[0].Trim().Split(new char[]
					{
						':'
					})[1].Trim(),
					DebugMessage = array[1].Trim().Split(new char[]
					{
						':'
					})[1].Trim(),
					NumberOfRetries = int.Parse(array[2].Trim().Split(new char[]
					{
						':'
					})[1].Trim())
				};
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x000175B4 File Offset: 0x000157B4
			public override string ToString()
			{
				return string.Format("PeriodicKey: {0}; DebugMessage: {1}; NumberOfRetries: {2}", this.PeriodicKey, this.DebugMessage, this.NumberOfRetries);
			}
		}
	}
}
