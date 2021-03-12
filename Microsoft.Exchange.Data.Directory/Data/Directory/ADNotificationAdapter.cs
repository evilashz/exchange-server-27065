using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000037 RID: 55
	internal static class ADNotificationAdapter
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00011FB8 File Offset: 0x000101B8
		public static ADNotificationRequestCookie RegisterChangeNotification<T>(ADObjectId baseDN, ADNotificationCallback callback) where T : ADConfigurationObject, new()
		{
			return ADNotificationAdapter.RegisterChangeNotification<T>(baseDN, callback, null, 10);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00011FC4 File Offset: 0x000101C4
		public static ADNotificationRequestCookie RegisterChangeNotification<T>(ADObjectId baseDN, ADNotificationCallback callback, object context, int retryCount) where T : ADConfigurationObject, new()
		{
			object wrappedContext;
			ADNotificationAdapter.CreateWrappedContextForRegisterChangeNotification(ref baseDN, callback, context, out wrappedContext);
			return ADNotificationAdapter.RegisterChangeNotification<T>(baseDN, wrappedContext, true, retryCount);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00012014 File Offset: 0x00010214
		public static ADNotificationRequestCookie RegisterChangeNotification<T>(ADObjectId baseDN, object wrappedContext, bool wrapAsADOperation, int retryCount) where T : ADConfigurationObject, new()
		{
			ADNotificationRequestCookie cookie = null;
			if (wrapAsADOperation)
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					cookie = ADNotificationAdapter.RegisterChangeNotification<T>(baseDN, new ADNotificationCallback(ADNotificationAdapter.OnNotification), wrappedContext);
				}, retryCount);
			}
			else
			{
				cookie = ADNotificationAdapter.RegisterChangeNotification<T>(baseDN, new ADNotificationCallback(ADNotificationAdapter.OnNotification), wrappedContext);
			}
			return cookie;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00012083 File Offset: 0x00010283
		public static ADNotificationRequestCookie RegisterChangeNotification<T>(ADObjectId baseDN, ADNotificationCallback callback, object context) where T : ADConfigurationObject, new()
		{
			return ADNotificationAdapter.RegisterChangeNotification<T>(Activator.CreateInstance<T>(), baseDN, callback, context);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00012094 File Offset: 0x00010294
		private static ADNotificationRequestCookie RegisterChangeNotification<T>(T dummyObject, ADObjectId baseDN, ADNotificationCallback callback, object context) where T : ADConfigurationObject, new()
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (baseDN == null || string.IsNullOrEmpty(baseDN.DistinguishedName))
			{
				throw new ArgumentNullException("baseDN");
			}
			string forestFQDN = baseDN.GetPartitionId().ForestFQDN;
			if (!baseDN.IsDescendantOf(ADSession.GetConfigurationNamingContext(forestFQDN)) && !ADSession.IsTenantIdentity(baseDN, forestFQDN))
			{
				throw new ArgumentException(DirectoryStrings.ExArgumentException("baseDN", baseDN), "baseDN");
			}
			ADNotificationRequest adnotificationRequest = new ADNotificationRequest(typeof(T), dummyObject.MostDerivedObjectClass, baseDN, callback, context);
			ADNotificationListener.RegisterChangeNotification(adnotificationRequest);
			return new ADNotificationRequestCookie(new ADNotificationRequest[]
			{
				adnotificationRequest
			});
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00012150 File Offset: 0x00010350
		public static ADOperationResult TryRegisterChangeNotification<T>(ADObjectId baseDN, ADNotificationCallback callback, int retryCount, out ADNotificationRequestCookie cookie) where T : ADConfigurationObject, new()
		{
			return ADNotificationAdapter.TryRegisterChangeNotification<T>(() => baseDN, callback, null, retryCount, out cookie);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000121D0 File Offset: 0x000103D0
		public static ADOperationResult TryRegisterChangeNotification<T>(Func<ADObjectId> baseDNGetter, ADNotificationCallback callback, object context, int retryCount, out ADNotificationRequestCookie cookie) where T : ADConfigurationObject, new()
		{
			cookie = null;
			ADNotificationRequestCookie returnedCookie = cookie;
			ADOperationResult result = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADObjectId baseDN = (baseDNGetter == null) ? null : baseDNGetter();
				object wrappedContext;
				ADNotificationAdapter.CreateWrappedContextForRegisterChangeNotification(ref baseDN, callback, context, out wrappedContext);
				returnedCookie = ADNotificationAdapter.RegisterChangeNotification<T>(baseDN, wrappedContext, false, 0);
			}, retryCount);
			cookie = returnedCookie;
			return result;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00012222 File Offset: 0x00010422
		public static void UnregisterChangeNotification(ADNotificationRequestCookie request)
		{
			ADNotificationAdapter.UnregisterChangeNotification(request, false);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001222C File Offset: 0x0001042C
		public static void UnregisterChangeNotification(ADNotificationRequestCookie requestCookie, bool block)
		{
			if (requestCookie == null)
			{
				throw new ArgumentNullException("requestCookie");
			}
			foreach (ADNotificationRequest request in requestCookie.Requests)
			{
				ADNotificationListener.UnRegisterChangeNotification(request, block);
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00012267 File Offset: 0x00010467
		public static ADNotificationRequestCookie RegisterExchangeTopologyChangeNotification(ADNotificationCallback callback, object context)
		{
			return ADNotificationAdapter.RegisterExchangeTopologyChangeNotification(callback, context, ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00012274 File Offset: 0x00010474
		public static ADNotificationRequestCookie RegisterExchangeTopologyChangeNotification(ADNotificationCallback callback, object context, ExchangeTopologyScope topologyType)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 387, "RegisterExchangeTopologyChangeNotification", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ADNotificationAdapter.cs");
			ADObjectId childId = topologyConfigurationSession.ConfigurationNamingContext.GetChildId("CN", "Sites");
			ADObjectId childId2 = childId.GetChildId("CN", "Inter-Site Transports").GetChildId("CN", "IP");
			ADObjectId orgContainerId = topologyConfigurationSession.GetOrgContainerId();
			ADNotificationRequest[] requests;
			switch (topologyType)
			{
			case ExchangeTopologyScope.Complete:
				requests = new ADNotificationRequest[]
				{
					ADNotificationAdapter.RegisterChangeNotification<ADServer>(childId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADSite>(childId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADSiteLink>(childId2, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADSubnet>(childId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADVirtualDirectory>(orgContainerId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<Server>(orgContainerId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ReceiveConnector>(orgContainerId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADEmailTransport>(orgContainerId, callback, context).Requests[0]
				};
				break;
			case ExchangeTopologyScope.ServerAndSiteTopology:
				requests = new ADNotificationRequest[]
				{
					ADNotificationAdapter.RegisterChangeNotification<ADSite>(childId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADSiteLink>(childId2, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<Server>(orgContainerId, callback, context).Requests[0]
				};
				break;
			case ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology:
				requests = new ADNotificationRequest[]
				{
					ADNotificationAdapter.RegisterChangeNotification<ADServer>(childId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADSite>(childId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADSiteLink>(childId2, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<Server>(orgContainerId, callback, context).Requests[0]
				};
				break;
			case ExchangeTopologyScope.ADAndExchangeServerAndSiteAndVirtualDirectoryTopology:
				requests = new ADNotificationRequest[]
				{
					ADNotificationAdapter.RegisterChangeNotification<ADServer>(childId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADSite>(childId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADSiteLink>(childId2, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<Server>(orgContainerId, callback, context).Requests[0],
					ADNotificationAdapter.RegisterChangeNotification<ADVirtualDirectory>(orgContainerId, callback, context).Requests[0]
				};
				break;
			default:
				throw new ArgumentException("topologyType", "topologyType");
			}
			return new ADNotificationRequestCookie(requests);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000124D2 File Offset: 0x000106D2
		public static T ReadConfiguration<T>(ADConfigurationReader<T> configurationReader)
		{
			return ADNotificationAdapter.ReadConfiguration<T>(configurationReader, 10);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000124F8 File Offset: 0x000106F8
		public static T ReadConfiguration<T>(ADConfigurationReader<T> configurationReader, int retryCount)
		{
			if (configurationReader == null)
			{
				throw new ArgumentNullException("configurationReader");
			}
			if (retryCount < 0)
			{
				throw new ArgumentOutOfRangeException("retryCount", "Number of retries must be equal to or larger than zero.");
			}
			T result = default(T);
			ADNotificationAdapter.RunADOperation(delegate()
			{
				result = configurationReader();
			}, retryCount);
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00012560 File Offset: 0x00010760
		public static bool TryReadConfiguration<T>(ADConfigurationReader<T> configurationReader, out T result)
		{
			ADOperationResult adoperationResult;
			return ADNotificationAdapter.TryReadConfiguration<T>(configurationReader, out result, 10, out adoperationResult);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00012578 File Offset: 0x00010778
		public static bool TryReadConfiguration<T>(ADConfigurationReader<T> configurationReader, out T result, out ADOperationResult operationStatus)
		{
			return ADNotificationAdapter.TryReadConfiguration<T>(configurationReader, out result, 10, out operationStatus);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000125A0 File Offset: 0x000107A0
		public static bool TryReadConfiguration<T>(ADConfigurationReader<T> configurationReader, out T result, int retryCount, out ADOperationResult operationStatus)
		{
			if (configurationReader == null)
			{
				throw new ArgumentNullException("configurationReader");
			}
			if (retryCount < 0)
			{
				throw new ArgumentOutOfRangeException("retryCount", "Number of retries must be equal to or larger than zero.");
			}
			result = default(T);
			T objectReturned = result;
			operationStatus = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				objectReturned = configurationReader();
			}, retryCount);
			result = objectReturned;
			if (operationStatus.Succeeded)
			{
				return result != null;
			}
			if (operationStatus.Exception is ComputerNameNotCurrentlyAvailableException)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_FIND_LOCAL_SERVER_FAILED, Environment.MachineName, new object[]
				{
					operationStatus.Exception.Message,
					Environment.MachineName
				});
			}
			return false;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001266D File Offset: 0x0001086D
		public static void ReadConfigurationPaged<T>(ADConfigurationReader<ADPagedReader<T>> configurationReader, ADConfigurationProcessor<T> configurationProcessor) where T : IConfigurable, new()
		{
			ADNotificationAdapter.ReadConfigurationPaged<T>(configurationReader, configurationProcessor, 10);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00012710 File Offset: 0x00010910
		public static void ReadConfigurationPaged<T>(ADConfigurationReader<ADPagedReader<T>> configurationReader, ADConfigurationProcessor<T> configurationProcessor, int retryCount) where T : IConfigurable, new()
		{
			if (configurationProcessor == null)
			{
				throw new ArgumentNullException("configurationProcessor");
			}
			ADPagedReader<T> pagedReader = ADNotificationAdapter.ReadConfiguration<ADPagedReader<T>>(configurationReader, retryCount);
			IEnumerator<T> enumerator = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				enumerator = pagedReader.GetEnumerator();
			}, retryCount);
			Breadcrumbs<Exception> exceptions = new Breadcrumbs<Exception>(32);
			try
			{
				for (;;)
				{
					bool hasMore = false;
					ADNotificationAdapter.RunADOperation(delegate()
					{
						try
						{
							hasMore = enumerator.MoveNext();
						}
						catch (Exception bc)
						{
							exceptions.Drop(bc);
							enumerator.Dispose();
							enumerator = pagedReader.GetEnumerator();
							throw;
						}
					}, retryCount);
					if (!hasMore)
					{
						break;
					}
					configurationProcessor(enumerator.Current);
				}
			}
			finally
			{
				enumerator.Dispose();
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000127C4 File Offset: 0x000109C4
		public static bool TryReadConfigurationPaged<T>(ADConfigurationReader<ADPagedReader<T>> configurationReader, ADConfigurationProcessor<T> configurationProcessor) where T : IConfigurable, new()
		{
			ADOperationResult adoperationResult;
			return ADNotificationAdapter.TryReadConfigurationPaged<T>(configurationReader, configurationProcessor, 10, out adoperationResult);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000127DC File Offset: 0x000109DC
		public static bool TryReadConfigurationPaged<T>(ADConfigurationReader<ADPagedReader<T>> configurationReader, ADConfigurationProcessor<T> configurationProcessor, out ADOperationResult operationStatus) where T : IConfigurable, new()
		{
			return ADNotificationAdapter.TryReadConfigurationPaged<T>(configurationReader, configurationProcessor, 10, out operationStatus);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00012880 File Offset: 0x00010A80
		public static bool TryReadConfigurationPaged<T>(ADConfigurationReader<ADPagedReader<T>> configurationReader, ADConfigurationProcessor<T> configurationProcessor, int retryCount, out ADOperationResult operationStatus) where T : IConfigurable, new()
		{
			if (configurationProcessor == null)
			{
				throw new ArgumentNullException("configurationProcessor");
			}
			ADPagedReader<T> pagedReader;
			if (!ADNotificationAdapter.TryReadConfiguration<ADPagedReader<T>>(configurationReader, out pagedReader, retryCount, out operationStatus))
			{
				return false;
			}
			IEnumerator<T> enumerator = null;
			operationStatus = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				enumerator = pagedReader.GetEnumerator();
			}, retryCount);
			if (!operationStatus.Succeeded)
			{
				return false;
			}
			Breadcrumbs<Exception> exceptions = new Breadcrumbs<Exception>(32);
			try
			{
				for (;;)
				{
					bool hasMore = false;
					operationStatus = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						try
						{
							hasMore = enumerator.MoveNext();
						}
						catch (Exception bc)
						{
							exceptions.Drop(bc);
							enumerator.Dispose();
							enumerator = pagedReader.GetEnumerator();
							throw;
						}
					}, retryCount);
					if (!operationStatus.Succeeded)
					{
						break;
					}
					if (!hasMore)
					{
						goto IL_AB;
					}
					configurationProcessor(enumerator.Current);
				}
				return false;
				IL_AB:;
			}
			finally
			{
				enumerator.Dispose();
			}
			return true;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001295C File Offset: 0x00010B5C
		public static void RunADOperation(ADOperation adOperation)
		{
			ADNotificationAdapter.RunADOperation(adOperation, 10);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00012968 File Offset: 0x00010B68
		public static void RunADOperation(ADOperation adOperation, int retryCount)
		{
			if (adOperation == null)
			{
				throw new ArgumentNullException("adOperation");
			}
			if (retryCount < 0)
			{
				throw new ArgumentOutOfRangeException("retryCount", "Number of retries must be equal to or larger than zero.");
			}
			for (int i = 0; i <= retryCount; i++)
			{
				try
				{
					adOperation();
					break;
				}
				catch (ADInvalidCredentialException ex)
				{
					if (i == retryCount)
					{
						throw new ADTransientException(ex.LocalizedString, ex);
					}
				}
				catch (TransientException)
				{
					if (i == retryCount)
					{
						throw;
					}
				}
				Thread.Sleep(i * 1000);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000129F0 File Offset: 0x00010BF0
		public static ADOperationResult TryRunADOperation(ADOperation adOperation)
		{
			return ADNotificationAdapter.TryRunADOperation(adOperation, 10);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000129FC File Offset: 0x00010BFC
		public static ADOperationResult TryRunADOperation(ADOperation adOperation, int retryCount)
		{
			try
			{
				ADNotificationAdapter.RunADOperation(adOperation, retryCount);
			}
			catch (TransientException ex)
			{
				ExTraceGlobals.ADNotificationsTracer.TraceDebug<TransientException>(0L, "AD operation failed with exception: {0}", ex);
				return new ADOperationResult(ADOperationErrorCode.RetryableError, ex);
			}
			catch (DataSourceOperationException ex2)
			{
				ExTraceGlobals.ADNotificationsTracer.TraceDebug<DataSourceOperationException>(0L, "AD operation failed with exception: {0}", ex2);
				return new ADOperationResult(ADOperationErrorCode.PermanentError, ex2);
			}
			catch (DataValidationException ex3)
			{
				ExTraceGlobals.ADNotificationsTracer.TraceDebug<DataValidationException>(0L, "AD operation failed with exception: {0}", ex3);
				return new ADOperationResult(ADOperationErrorCode.PermanentError, ex3);
			}
			return ADOperationResult.Success;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00012A98 File Offset: 0x00010C98
		private static void OnNotification(ADNotificationEventArgs args)
		{
			if (args != null)
			{
				ADNotificationAdapter.WrappedContext wrappedContext = args.Context as ADNotificationAdapter.WrappedContext;
				if (wrappedContext != null && wrappedContext.Callback != null)
				{
					wrappedContext.Callback.SerializedRun(args);
				}
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00012ACC File Offset: 0x00010CCC
		private static object CreateWrappingContext(ADNotificationCallback callback, object context)
		{
			ADNotificationAdapter.WrappedADNotificationCallback callback2 = new ADNotificationAdapter.WrappedADNotificationCallback(callback);
			return new ADNotificationAdapter.WrappedContext(callback2, context);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012AE8 File Offset: 0x00010CE8
		private static void CreateWrappedContextForRegisterChangeNotification(ref ADObjectId baseDN, ADNotificationCallback callback, object context, out object wrappedContext)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (baseDN == null)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 1089, "CreateWrappedContextForRegisterChangeNotification", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ADNotificationAdapter.cs");
				baseDN = topologyConfigurationSession.GetOrgContainerId();
			}
			ExTraceGlobals.ADNotificationsTracer.TraceDebug<ADObjectId, string, string>(0L, "new change notification registration for {0} from {1}.{2}", baseDN, callback.Method.DeclaringType.FullName, callback.Method.Name);
			wrappedContext = ADNotificationAdapter.CreateWrappingContext(callback, context);
		}

		// Token: 0x040000EA RID: 234
		private const int DefaultReadRetryCount = 10;

		// Token: 0x040000EB RID: 235
		private const int RetryInterval = 1000;

		// Token: 0x02000038 RID: 56
		private class WrappedContext
		{
			// Token: 0x0600036C RID: 876 RVA: 0x00012B66 File Offset: 0x00010D66
			public WrappedContext(ADNotificationAdapter.WrappedADNotificationCallback callback, object context)
			{
				this.Callback = callback;
				this.Context = context;
			}

			// Token: 0x040000EC RID: 236
			public readonly ADNotificationAdapter.WrappedADNotificationCallback Callback;

			// Token: 0x040000ED RID: 237
			public readonly object Context;
		}

		// Token: 0x02000039 RID: 57
		private class WrappedADNotificationCallback
		{
			// Token: 0x0600036D RID: 877 RVA: 0x00012B7C File Offset: 0x00010D7C
			public WrappedADNotificationCallback(ADNotificationCallback callback)
			{
				this.callback = callback;
				this.waitHandle = new AutoResetEvent(true);
				this.hasQueuedRun = false;
			}

			// Token: 0x0600036E RID: 878 RVA: 0x00012BA0 File Offset: 0x00010DA0
			public void SerializedRun(ADNotificationEventArgs args)
			{
				bool flag = false;
				lock (this)
				{
					if (!this.hasQueuedRun)
					{
						this.hasQueuedRun = true;
						flag = true;
					}
				}
				if (flag)
				{
					bool timedOut = !this.waitHandle.WaitOne(600000, false);
					this.RunCallback(args, timedOut);
				}
			}

			// Token: 0x0600036F RID: 879 RVA: 0x00012C08 File Offset: 0x00010E08
			private void RunCallback(ADNotificationEventArgs args, bool timedOut)
			{
				if (!timedOut)
				{
					lock (this)
					{
						this.hasQueuedRun = false;
					}
					try
					{
						try
						{
							this.callback(args);
						}
						catch (ADInvalidCredentialException)
						{
							ExTraceGlobals.ADNotificationsTracer.TraceError(0L, "Some component's AD Notification callback didn't catch ADInvalidCredentialException.");
						}
						catch (ADTransientException)
						{
							ExTraceGlobals.ADNotificationsTracer.TraceError(0L, "Some component's AD Notification callback didn't catch ADTransientException.");
						}
						catch (DataValidationException)
						{
							ExTraceGlobals.ADNotificationsTracer.TraceError(0L, "Some component's AD Notification callback didn't catch DataValidationException.");
						}
						return;
					}
					finally
					{
						this.waitHandle.Set();
					}
				}
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_AD_NOTIFICATION_CALLBACK_TIMED_OUT, args.Type.Name, new object[]
				{
					args.Type.Name
				});
				ExTraceGlobals.ADNotificationsTracer.TraceError(0L, "Some component's AD Notification callback hasn't returned after 10 minutes. config. change notifications ARE NO LONGER BEING DELIVERED TO SUCH COMPONENT.");
			}

			// Token: 0x040000EE RID: 238
			private const int ClientCallbackTimeout = 600000;

			// Token: 0x040000EF RID: 239
			private ADNotificationCallback callback;

			// Token: 0x040000F0 RID: 240
			private AutoResetEvent waitHandle;

			// Token: 0x040000F1 RID: 241
			private bool hasQueuedRun;
		}
	}
}
