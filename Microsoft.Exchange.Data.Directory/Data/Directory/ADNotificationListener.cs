using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000042 RID: 66
	internal class ADNotificationListener
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00012E9F File Offset: 0x0001109F
		private static bool IsStopping
		{
			get
			{
				return Environment.HasShutdownStarted || ADNotificationListener.stop;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00012EB4 File Offset: 0x000110B4
		internal List<string> ExceptionDNsList
		{
			get
			{
				if (this.exceptionDNsList == null)
				{
					ADObjectId childId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().GetChildId("Administrative Groups").GetChildId(AdministrativeGroup.DefaultName);
					this.exceptionDNsList = new List<string>
					{
						ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().DistinguishedName,
						childId.GetChildId(ServersContainer.DefaultName).DistinguishedName,
						childId.GetChildId(DatabasesContainer.DefaultName).DistinguishedName
					};
				}
				return this.exceptionDNsList;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00012F34 File Offset: 0x00011134
		private ADNotificationListener()
		{
			this.clientNotificationRequests = new List<ADNotificationRequest>();
			this.deletedObjectsNotificationRequests = new List<ADNotificationRequest>();
			this.uniqueDeletedObjectsNotificationRequestTuples = new List<ADNotificationListener.DeletedNotificationTuple>();
			this.session = new ADTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet());
			this.session.ClientSideSearchTimeout = new TimeSpan?(ConnectionPoolManager.NotificationClientSideSearchTimeout);
			this.state = NotificationListenerState.Idle;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00012FB4 File Offset: 0x000111B4
		protected ADNotificationListener(string dummy) : this()
		{
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00012FBC File Offset: 0x000111BC
		public static void Stop()
		{
			ADNotificationListener.stop = true;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00012FC8 File Offset: 0x000111C8
		private void LoadPolicyConfigurationIfRequired(bool forceReload)
		{
			if (!Globals.IsDatacenter)
			{
				return;
			}
			if (this.queryPolicy != null && !forceReload)
			{
				return;
			}
			int num = this.maximumNumberOfListenersPerConnection;
			try
			{
				if (this.session.TryGetDefaultAdQueryPolicy(out this.queryPolicy))
				{
					if (this.queryPolicy.IsValid && this.queryPolicy.MaxNotificationPerConnection != null)
					{
						this.maximumNumberOfListenersPerConnection = this.queryPolicy.MaxNotificationPerConnection.Value;
					}
					else
					{
						this.maximumNumberOfListenersPerConnection = 5;
					}
				}
				else
				{
					this.maximumNumberOfListenersPerConnection = 5;
				}
			}
			catch (DataSourceTransientException arg)
			{
				ExTraceGlobals.ADNotificationsTracer.TraceWarning<int, DataSourceTransientException>(0L, "Got a transient exception while refreshing ADQueryPolicy, ignoring the failure and using the previous value of{0}. {1}", num, arg);
				this.maximumNumberOfListenersPerConnection = num;
			}
			if (num != this.maximumNumberOfListenersPerConnection)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ADNotificationsMaxNumberOfNotificationsPerConnection, "MaximumNumberOfListenersPerConnection", new object[]
				{
					this.maximumNumberOfListenersPerConnection
				});
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000130EC File Offset: 0x000112EC
		private void ProcessChangeNotification(IAsyncResult result)
		{
			ADNotificationListener.RequestInstance requestInstance = result.AsyncState as ADNotificationListener.RequestInstance;
			ExTraceGlobals.ADNotificationsTracer.TraceDebug<int>(0L, "Received a change notification from the AD for {0}", requestInstance.GetHashCode());
			PartialResultsCollection partialResultsCollection = null;
			DirectoryException ex = null;
			HandlingType handlingType = HandlingType.Retry;
			bool flag = false;
			lock (this.RequestsLock)
			{
				if (requestInstance.Connection == null)
				{
					ExTraceGlobals.ADNotificationsTracer.TraceDebug<int>(0L, "Request {0} has been abandoned, returning from callback", requestInstance.GetHashCode());
					return;
				}
				try
				{
					partialResultsCollection = requestInstance.Connection.GetPartialResults(result);
				}
				catch (DirectoryException ex2)
				{
					ex = ex2;
					handlingType = requestInstance.Connection.AnalyzeDirectoryError(ex2).HandlingType;
				}
				if (Globals.IsDatacenter)
				{
					ADNotificationListener.RequestTuple requestTuple = requestInstance.NotifyHandles.FirstOrDefault((ADNotificationListener.RequestTuple x) => result.Equals(x.Handler));
					if (requestTuple != null)
					{
						flag = requestTuple.IsDeletedNotification;
					}
				}
			}
			if (ex != null)
			{
				ExTraceGlobals.ADNotificationsTracer.TraceError<string, string>(0L, "Received error {0} from GetPartialResults, handlingType is {1}", ex.Message, handlingType.ToString());
				switch (handlingType)
				{
				case HandlingType.Retry:
				case HandlingType.RetryOnce:
					ADNotificationListener.ReissueNotificationRequests(false, true);
					return;
				case HandlingType.Throw:
					throw new ADOperationException(DirectoryStrings.ExceptionNotifyErrorGettingResults(requestInstance.Connection.ServerName), ex);
				}
			}
			foreach (object obj in partialResultsCollection)
			{
				SearchResultEntry searchResultEntry = (SearchResultEntry)obj;
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessRateNotificationsReceived, UpdateType.Add, 1U);
				ADObjectId adobjectId;
				string[] array;
				ADObjectId adobjectId2;
				bool flag3;
				if (!ADNotificationListener.TryParseSearchResult(searchResultEntry, out adobjectId, out array, out adobjectId2, out flag3))
				{
					ExTraceGlobals.ADNotificationsTracer.TraceError<string>(0L, "Failed to parse entry {0}", searchResultEntry.DistinguishedName);
				}
				else if (!flag || flag3)
				{
					ADNotificationListener.TraceParsedADNotificationEntry(array, adobjectId, flag3, adobjectId2);
					bool flag4 = false;
					lock (ADNotificationListener.InstanceLockRoot)
					{
						ADNotificationListener instance = ADNotificationListener.GetInstance();
						ExTraceGlobals.ADNotificationsTracer.TraceDebug<string>(0L, "Processing change notification while state is {0}", instance.state.ToString());
						List<ADNotificationRequest> list = new List<ADNotificationRequest>(instance.clientNotificationRequests.Count + instance.deletedObjectsNotificationRequests.Count);
						list.AddRange(instance.clientNotificationRequests);
						list.AddRange(instance.deletedObjectsNotificationRequests);
						using (List<ADNotificationRequest>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ADNotificationRequest clientNotificationRequest = enumerator2.Current;
								if (Array.Exists<string>(array, (string s) => string.Equals(s, clientNotificationRequest.ObjectClass, StringComparison.OrdinalIgnoreCase)))
								{
									if (flag3)
									{
										if (!adobjectId2.IsDescendantOf(clientNotificationRequest.RootId) && adobjectId.ObjectGuid != clientNotificationRequest.RootId.ObjectGuid)
										{
											continue;
										}
									}
									else if (!adobjectId.IsDescendantOf(clientNotificationRequest.RootId))
									{
										continue;
									}
									ADNotificationEventArgs args = new ADNotificationEventArgs(flag3 ? ADNotificationChangeType.Delete : ADNotificationChangeType.ModifyOrAdd, clientNotificationRequest.Context, adobjectId, adobjectId2, clientNotificationRequest.Type);
									flag4 = true;
									ADNotificationListener.InvokeCallback(clientNotificationRequest, args);
								}
							}
						}
					}
					if (flag4)
					{
						ADProviderPerf.UpdateProcessCounter(Counter.ProcessRateNotificationsReported, UpdateType.Add, 1U);
					}
				}
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000134D8 File Offset: 0x000116D8
		private static bool TryParseSearchResult(SearchResultEntry entry, out ADObjectId id, out string[] objectClasses, out ADObjectId lastKnownParent, out bool isDeleted)
		{
			id = null;
			objectClasses = null;
			lastKnownParent = null;
			isDeleted = false;
			Guid empty = Guid.Empty;
			DirectoryAttribute directoryAttribute = entry.Attributes["distinguishedName"];
			if (directoryAttribute == null)
			{
				return false;
			}
			object[] values = directoryAttribute.GetValues(typeof(string));
			if (values == null)
			{
				return false;
			}
			if (values.Length == 0)
			{
				return false;
			}
			string distinguishedName = values[0] as string;
			DirectoryAttribute directoryAttribute2 = entry.Attributes["objectGuid"];
			if (directoryAttribute2 == null)
			{
				return false;
			}
			values = directoryAttribute2.GetValues(typeof(byte[]));
			if (values == null)
			{
				return false;
			}
			if (values.Length == 0)
			{
				return false;
			}
			byte[] array = values[0] as byte[];
			if (array != null)
			{
				empty = new Guid(array);
			}
			id = new ADObjectId(distinguishedName, empty);
			DirectoryAttribute directoryAttribute3 = entry.Attributes["objectClass"];
			if (directoryAttribute3 == null)
			{
				return false;
			}
			values = directoryAttribute3.GetValues(typeof(string));
			if (values.Length == 0)
			{
				return false;
			}
			objectClasses = new string[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				objectClasses[i] = (values[i] as string);
			}
			DirectoryAttribute directoryAttribute4 = entry.Attributes["lastKnownParent"];
			if (directoryAttribute4 != null)
			{
				values = directoryAttribute4.GetValues(typeof(string));
				if (values != null && values.Length > 0)
				{
					lastKnownParent = ADObjectId.ParseExtendedDN((string)directoryAttribute4.GetValues(typeof(string))[0]);
				}
			}
			DirectoryAttribute directoryAttribute5 = entry.Attributes["isDeleted"];
			isDeleted = false;
			if (directoryAttribute5 != null)
			{
				values = directoryAttribute5.GetValues(typeof(string));
				if (values != null && values.Length > 0)
				{
					string text = values[0] as string;
					if (text != null)
					{
						isDeleted = text.Equals("TRUE", StringComparison.OrdinalIgnoreCase);
					}
				}
			}
			return true;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001368C File Offset: 0x0001188C
		private static void ProcessQueuedNotifications(object state)
		{
			if (!ADNotificationListener.exceptionProcessList.Contains(Globals.ProcessName, StringComparer.OrdinalIgnoreCase) && Globals.IsDatacenter)
			{
				lock (ADNotificationListener.InstanceLockRoot)
				{
					ADNotificationListener instance = ADNotificationListener.GetInstance();
					List<ADNotificationRequest> list = new List<ADNotificationRequest>(instance.clientNotificationRequests.Count + instance.deletedObjectsNotificationRequests.Count);
					list.AddRange(instance.clientNotificationRequests);
					list.AddRange(instance.deletedObjectsNotificationRequests);
					foreach (ADNotificationRequest adnotificationRequest in list)
					{
						if ((ExDateTime.UtcNow - adnotificationRequest.LastCallbackTime).Minutes > ADNotificationListener.NotificationThrottlingTimeMinutes)
						{
							ADNotificationEventArgs args = new ADNotificationEventArgs(adnotificationRequest.isDeletedContainer ? ADNotificationChangeType.Delete : ADNotificationChangeType.ModifyOrAdd, adnotificationRequest.Context, adnotificationRequest.RootId, adnotificationRequest.RootId, adnotificationRequest.Type);
							ADNotificationListener.InvokeCallback(adnotificationRequest, args);
						}
					}
				}
			}
			ADNotificationListener.NotificationThrottlingTimeMinutes = RegistrySettings.ExchangeServerCurrentVersion.NotificationThrottlingTimeMinutes;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00013890 File Offset: 0x00011A90
		private static void InvokeCallback(ADNotificationRequest request, ADNotificationEventArgs args)
		{
			request.RefCount++;
			ExTraceGlobals.ADNotificationsTracer.TraceDebug(0L, "Callback thread being spawned for {0} {1} {2}, refcount is now {3}", new object[]
			{
				request.GetHashCode(),
				args.ChangeType.ToString(),
				(args.Id == null) ? "<null>" : args.Id.ToDNString(),
				request.RefCount
			});
			bool flag;
			using (ActivityContext.SuppressThreadScope())
			{
				request.LastCallbackTime = ExDateTime.UtcNow;
				flag = ThreadPool.QueueUserWorkItem(delegate(object eventArgs)
				{
					if (request.IsValid)
					{
						ExTraceGlobals.ADNotificationsTracer.TraceDebug<int>(0L, "Invoking callback for request {0}", request.GetHashCode());
						request.Callback((ADNotificationEventArgs)eventArgs);
					}
					lock (ADNotificationListener.InstanceLockRoot)
					{
						request.RefCount--;
						if (request.RefCount == 0)
						{
							Monitor.PulseAll(ADNotificationListener.InstanceLockRoot);
						}
					}
					ExTraceGlobals.ADNotificationsTracer.TraceDebug<int, int>(0L, "Callback for request {0} completed, refcount is now {1}", request.GetHashCode(), request.RefCount);
				}, args);
			}
			if (!flag)
			{
				request.RefCount--;
				ExTraceGlobals.ADNotificationsTracer.TraceError<int>(0L, "Unable to enqueue callback thread. Refcount is now {0}", request.RefCount);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000139EC File Offset: 0x00011BEC
		private static void UnRegisterChangeNotification(ADNotificationRequest request, bool isDeletedNotification, bool block)
		{
			ADNotificationListener instance = ADNotificationListener.GetInstance();
			bool flag = false;
			ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)instance.GetHashCode(), "Unregistering request {0} {1} {2}. IsDeleted {3}", new object[]
			{
				request.GetHashCode(),
				request.ObjectClass,
				request.RootId.ToDNString(),
				isDeletedNotification
			});
			try
			{
				lock (ADNotificationListener.InstanceLockRoot)
				{
					if (!request.IsValid)
					{
						ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)instance.GetHashCode(), "Attempted to unregister an invalid request");
					}
					else
					{
						instance.WaitForStates(new NotificationListenerState[]
						{
							NotificationListenerState.Idle,
							NotificationListenerState.Listening
						});
						ExTraceGlobals.ADNotificationsTracer.TraceDebug<string>((long)instance.GetHashCode(), "Unregistering while state is {0}", instance.state.ToString());
						if (isDeletedNotification)
						{
							instance.deletedObjectsNotificationRequests.Remove(request);
						}
						else
						{
							instance.clientNotificationRequests.Remove(request);
						}
						request.MakeInvalid();
						flag = (instance.clientNotificationRequests.Count == 0 && instance.deletedObjectsNotificationRequests.Count == 0 && instance.state == NotificationListenerState.Listening);
						ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)instance.GetHashCode(), "There are now {0} regular requests, {1} deleted objects request and the state is {2}.  We {3}need to abandon the AD notifications.", new object[]
						{
							instance.clientNotificationRequests.Count,
							instance.deletedObjectsNotificationRequests.Count,
							instance.state.ToString(),
							flag ? string.Empty : "do not "
						});
						if (flag)
						{
							instance.TransitionToState(NotificationListenerState.Disconnecting, "UnRegisterChangeNotification");
						}
						ExTraceGlobals.FaultInjectionTracer.TraceTest(4240846141U);
						if (block)
						{
							for (;;)
							{
								int refCount = request.RefCount;
								if (refCount == 0)
								{
									break;
								}
								ExTraceGlobals.ADNotificationsTracer.TraceDebug<int>((long)instance.GetHashCode(), "Waiting for {0} callbacks to complete.", refCount);
								Monitor.Wait(ADNotificationListener.InstanceLockRoot);
							}
						}
						if (flag)
						{
							instance.AbandonNotificationRequests();
						}
						else if (isDeletedNotification)
						{
							ADNotificationListener.DeletedNotificationTuple deletedNotificationTuple = instance.uniqueDeletedObjectsNotificationRequestTuples.FirstOrDefault((ADNotificationListener.DeletedNotificationTuple x) => x.BaseDN.Equals(request.RootId.DistinguishedName, StringComparison.OrdinalIgnoreCase));
							if (deletedNotificationTuple != null)
							{
								deletedNotificationTuple.NumberOfListeners--;
								if (deletedNotificationTuple.NumberOfListeners == 0)
								{
									ExTraceGlobals.ADNotificationsTracer.TraceDebug<string>((long)instance.GetHashCode(), "Removing deleted notification listener {0}", deletedNotificationTuple.BaseDN);
									instance.AbandonDeletedNotificationRequest(request.RootId.DistinguishedName);
									instance.uniqueDeletedObjectsNotificationRequestTuples.Remove(deletedNotificationTuple);
								}
							}
						}
						else
						{
							foreach (ADNotificationListener.DeletedNotificationTuple deletedNotificationTuple2 in instance.uniqueDeletedObjectsNotificationRequestTuples.FindAll((ADNotificationListener.DeletedNotificationTuple x) => 0 == x.NumberOfListeners))
							{
								instance.AbandonDeletedNotificationRequest(deletedNotificationTuple2.BaseDN);
							}
							instance.uniqueDeletedObjectsNotificationRequestTuples.RemoveAll((ADNotificationListener.DeletedNotificationTuple x) => 0 == x.NumberOfListeners);
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					lock (ADNotificationListener.InstanceLockRoot)
					{
						instance.TransitionToState(NotificationListenerState.Idle, "UnRegisterChangeNotification");
					}
				}
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00013E18 File Offset: 0x00012018
		protected virtual bool TryIssueNotificationRequests(List<SearchRequest> notificationRequest)
		{
			if (ADNotificationListener.exceptionProcessList.Contains(Globals.ProcessName, StringComparer.OrdinalIgnoreCase) || !Globals.IsDatacenter)
			{
				ADNotificationListener.RequestInstance requestInstance = null;
				RetryManager retryManager = new RetryManager();
				bool result = false;
				PooledLdapConnection notifyConnection = this.session.GetNotifyConnection();
				notifyConnection.Timeout = new TimeSpan(6, 0, 0);
				bool flag = false;
				try
				{
					object requestsLock;
					Monitor.Enter(requestsLock = this.RequestsLock, ref flag);
					if (this.currentRequests != null)
					{
						if (notifyConnection != this.currentRequests.Connection)
						{
							ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)this.GetHashCode(), "Notify Connections are different. Return successfulNotificationRequest = False");
							return result;
						}
						requestInstance = this.currentRequests;
					}
					else
					{
						requestInstance = new ADNotificationListener.RequestInstance();
						requestInstance.NotifyHandles = new List<ADNotificationListener.RequestTuple>();
					}
					SearchRequest request = null;
					try
					{
						foreach (SearchRequest searchRequest in notificationRequest)
						{
							if (!this.ExceptionDNsList.Contains(searchRequest.DistinguishedName) || !Globals.IsDatacenter || Globals.EnableNotification || ExEnvironment.IsTest)
							{
								request = searchRequest;
								searchRequest.Controls.Add(new DirectoryNotificationControl());
								searchRequest.Controls.Add(new ShowDeletedControl());
								using (ActivityContext.SuppressThreadScope())
								{
									requestInstance.NotifyHandles.Add(new ADNotificationListener.RequestTuple
									{
										Handler = notifyConnection.BeginSendRequest(searchRequest, PartialResultProcessing.ReturnPartialResultsAndNotifyCallback, new AsyncCallback(this.ProcessChangeNotification), requestInstance),
										BaseDN = searchRequest.DistinguishedName,
										IsDeletedNotification = (SearchScope.OneLevel == searchRequest.Scope)
									});
								}
								ExTraceGlobals.ADNotificationsTracer.TraceDebug<string>((long)this.GetHashCode(), "Issued AD notification on {0}", searchRequest.DistinguishedName);
							}
						}
						ExTraceGlobals.FaultInjectionTracer.TraceTest(4190514493U);
						requestInstance.Connection = notifyConnection;
						result = true;
						if (this.currentRequests == null)
						{
							this.currentRequests = requestInstance;
						}
					}
					catch (DirectoryException ex)
					{
						notifyConnection.ReturnToPool();
						retryManager.Tried(notifyConnection.ServerName);
						this.session.AnalyzeDirectoryError(notifyConnection, request, ex, retryManager.TotalRetries, retryManager[notifyConnection.ServerName]);
						ExTraceGlobals.ADNotificationsTracer.TraceWarning<DirectoryException>((long)this.GetHashCode(), "Got exception when reissuing notification searches {0}", ex);
						if (this.currentRequests != null)
						{
							foreach (SearchRequest searchRequest2 in notificationRequest)
							{
								bool isDeleted = SearchScope.OneLevel == searchRequest2.Scope;
								this.currentRequests.NotifyHandles.RemoveAll((ADNotificationListener.RequestTuple x) => x.BaseDN.Equals(request.DistinguishedName, StringComparison.OrdinalIgnoreCase) && x.IsDeletedNotification == isDeleted);
							}
						}
					}
				}
				finally
				{
					if (flag)
					{
						object requestsLock;
						Monitor.Exit(requestsLock);
					}
				}
				return result;
			}
			return true;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001416C File Offset: 0x0001236C
		private List<SearchRequest> GetAllNotificationsToIssue()
		{
			this.LoadPolicyConfigurationIfRequired(true);
			List<SearchRequest> list = new List<SearchRequest>();
			string distinguishedName = this.session.GetOrgContainerId().DistinguishedName;
			SearchRequest item = new SearchRequest(distinguishedName, "(objectClass=*)", SearchScope.Subtree, ADNotificationListener.NotificationProperties);
			list.Add(item);
			if (!TopologyProvider.IsAdamTopology())
			{
				distinguishedName = this.session.ConfigurationNamingContext.GetChildId("CN", "Sites").DistinguishedName;
				item = new SearchRequest(distinguishedName, "(objectClass=*)", SearchScope.Subtree, ADNotificationListener.NotificationProperties);
				list.Add(item);
			}
			if (Globals.IsDatacenter)
			{
				using (List<ADNotificationListener.DeletedNotificationTuple>.Enumerator enumerator = this.uniqueDeletedObjectsNotificationRequestTuples.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ADNotificationListener.DeletedNotificationTuple deletedNotificationTuple = enumerator.Current;
						distinguishedName = deletedNotificationTuple.BaseDN;
						item = new SearchRequest(distinguishedName, "(objectClass=*)", SearchScope.OneLevel, ADNotificationListener.NotificationProperties);
						list.Add(item);
					}
					return list;
				}
			}
			distinguishedName = this.session.ConfigurationNamingContext.GetChildId("CN", "Deleted Objects").DistinguishedName;
			item = new SearchRequest(distinguishedName, "(objectClass=*)", SearchScope.Subtree, ADNotificationListener.NotificationProperties);
			list.Add(item);
			return list;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00014294 File Offset: 0x00012494
		private List<SearchRequest> ConstructRequestForDeletedNotification(ADNotificationRequest deletedNotificationRequest)
		{
			if (deletedNotificationRequest == null)
			{
				throw new ArgumentNullException("deletedNotificationRequest");
			}
			return new List<SearchRequest>(1)
			{
				new SearchRequest(deletedNotificationRequest.RootId.DistinguishedName, "(objectClass=*)", SearchScope.OneLevel, ADNotificationListener.NotificationProperties)
			};
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000142DC File Offset: 0x000124DC
		private void AbandonNotificationRequests()
		{
			if (this.currentRequests == null)
			{
				ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)this.GetHashCode(), "AbandonNotificationRequests. Current Request is null.");
				return;
			}
			lock (this.RequestsLock)
			{
				if (this.currentRequests == null)
				{
					ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)this.GetHashCode(), "AbandonNotificationRequests. Current Request is null.");
				}
				else
				{
					ExTraceGlobals.ADNotificationsTracer.TraceDebug<int>((long)this.GetHashCode(), "Abandoning requests for instance {0}", this.currentRequests.GetHashCode());
					foreach (ADNotificationListener.RequestTuple requestTuple in this.currentRequests.NotifyHandles)
					{
						if (!requestTuple.Handler.IsCompleted)
						{
							this.currentRequests.Connection.Abort(requestTuple.Handler);
						}
					}
					PooledLdapConnection connection = this.currentRequests.Connection;
					this.currentRequests.Connection = null;
					this.currentRequests = null;
					connection.ReturnToPool();
				}
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001442C File Offset: 0x0001262C
		private void AbandonDeletedNotificationRequest(string dn)
		{
			if (!Globals.IsDatacenter)
			{
				return;
			}
			if (this.currentRequests == null)
			{
				ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)this.GetHashCode(), "AbandonDeletedNotificationRequest. Current Request is null.");
				return;
			}
			lock (this.RequestsLock)
			{
				if (this.currentRequests != null)
				{
					ExTraceGlobals.ADNotificationsTracer.TraceDebug<string>((long)this.GetHashCode(), "Closing Deleted Object notification requests for Root {0}", dn);
					ADNotificationListener.RequestTuple requestTuple = this.currentRequests.NotifyHandles.FirstOrDefault((ADNotificationListener.RequestTuple x) => x.BaseDN.Equals(dn) && x.IsDeletedNotification);
					if (requestTuple != null)
					{
						this.currentRequests.NotifyHandles.Remove(requestTuple);
						if (!requestTuple.Handler.IsCompleted)
						{
							this.currentRequests.Connection.Abort(requestTuple.Handler);
						}
					}
				}
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00014524 File Offset: 0x00012724
		internal void WaitForStates(params NotificationListenerState[] acceptableStates)
		{
			if (ExTraceGlobals.ADNotificationsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (NotificationListenerState notificationListenerState in acceptableStates)
				{
					stringBuilder.Append(notificationListenerState.ToString());
					stringBuilder.Append(" ");
				}
				ExTraceGlobals.ADNotificationsTracer.TraceDebug<string>((long)this.GetHashCode(), "Waiting for states: {0}", stringBuilder.ToString());
			}
			for (;;)
			{
				foreach (NotificationListenerState notificationListenerState2 in acceptableStates)
				{
					if (this.state == notificationListenerState2)
					{
						return;
					}
				}
				Monitor.Wait(ADNotificationListener.InstanceLockRoot);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000145CB File Offset: 0x000127CB
		private void TransitionToState(NotificationListenerState newState, string methodName)
		{
			this.TransitionToState(newState, methodName, null);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000145D8 File Offset: 0x000127D8
		private void TransitionToState(NotificationListenerState newState, string methodName, params NotificationListenerState[] expectedStates)
		{
			if (string.IsNullOrEmpty(methodName))
			{
				throw new ArgumentNullException("methodName");
			}
			lock (ADNotificationListener.InstanceLockRoot)
			{
				ExTraceGlobals.ADNotificationsTracer.TraceDebug<string, string>(0L, "Transitioning from state {0} to state {1}", this.state.ToString(), newState.ToString());
				this.state = newState;
				this.stateLog.Drop(new ADNotificationListener.TransitionContext(newState, methodName));
				Monitor.PulseAll(ADNotificationListener.InstanceLockRoot);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00014674 File Offset: 0x00012874
		private void LogStateException(string description, Exception e)
		{
			lock (ADNotificationListener.InstanceLockRoot)
			{
				ExTraceGlobals.ADNotificationsTracer.TraceWarning<string, Exception, string>(0L, "{0} Got exception {1} while in state {2}", description, e, this.state.ToString());
				this.stateLog.Drop(new ADNotificationListener.TransitionContext(this.state, description + e.ToString()));
				Monitor.PulseAll(ADNotificationListener.InstanceLockRoot);
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000146FC File Offset: 0x000128FC
		[Conditional("DEBUG")]
		internal void AssertState(params NotificationListenerState[] expectedStates)
		{
			if (expectedStates == null)
			{
				return;
			}
			foreach (NotificationListenerState notificationListenerState in expectedStates)
			{
				if (this.state == notificationListenerState)
				{
					return;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (NotificationListenerState notificationListenerState2 in expectedStates)
			{
				stringBuilder.Append(notificationListenerState2.ToString());
				stringBuilder.Append(" ");
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00014774 File Offset: 0x00012974
		internal static void ReissueNotificationRequests(bool throwOnFailure, bool reissueIfListening)
		{
			ADNotificationListener instance = ADNotificationListener.GetInstance();
			ExTraceGlobals.ADNotificationsTracer.TraceDebug<string>((long)instance.GetHashCode(), "Reissuing AD notifications, will {0}throw on failure", throwOnFailure ? string.Empty : "not ");
			while (!ADNotificationListener.IsStopping)
			{
				NotificationListenerState notificationListenerState = NotificationListenerState.Idle;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				try
				{
					lock (ADNotificationListener.InstanceLockRoot)
					{
						notificationListenerState = instance.state;
						instance.WaitForStates(new NotificationListenerState[]
						{
							NotificationListenerState.Idle,
							NotificationListenerState.Listening,
							NotificationListenerState.Connecting
						});
						ExTraceGlobals.ADNotificationsTracer.TraceDebug<int, int, string>((long)instance.GetHashCode(), "There are {0} client requests, {1} deleted object request and the state is {2}.", instance.clientNotificationRequests.Count, instance.deletedObjectsNotificationRequests.Count, instance.state.ToString());
						if (instance.state == NotificationListenerState.Connecting)
						{
							break;
						}
						if (instance.clientNotificationRequests.Count == 0)
						{
							ExTraceGlobals.ADNotificationsTracer.TraceDebug<string>((long)instance.GetHashCode(), "We {0}need to reissue", (instance.clientNotificationRequests.Count == 0 && instance.deletedObjectsNotificationRequests.Count == 0) ? "do not " : string.Empty);
							break;
						}
						if (instance.state == NotificationListenerState.Listening && !reissueIfListening)
						{
							break;
						}
						flag = true;
						notificationListenerState = NotificationListenerState.Listening;
						instance.TransitionToState(NotificationListenerState.Disconnecting, "ReissueNotificationRequestsDisconnecting");
						ExTraceGlobals.FaultInjectionTracer.TraceTest(4240846141U);
						instance.AbandonNotificationRequests();
						instance.TransitionToState(NotificationListenerState.Connecting, "ReissueNotificationRequestsConnecting", new NotificationListenerState[]
						{
							NotificationListenerState.Disconnecting
						});
						notificationListenerState = NotificationListenerState.Idle;
						try
						{
							List<SearchRequest> allNotificationsToIssue = instance.GetAllNotificationsToIssue();
							if (instance.TryIssueNotificationRequests(allNotificationsToIssue))
							{
								flag2 = true;
								notificationListenerState = NotificationListenerState.Listening;
							}
							else
							{
								flag3 = true;
							}
						}
						catch (ADTransientException ex)
						{
							ExTraceGlobals.ADNotificationsTracer.TraceWarning<ADTransientException>((long)instance.GetHashCode(), "Could not get connection to reissue notification searches, will sleep 5 seconds and try again: {0}", ex);
							instance.LogStateException("Issuing requests:", ex);
							if (throwOnFailure)
							{
								throw;
							}
							flag3 = true;
						}
						catch (Exception ex2)
						{
							instance.LogStateException("Issuing requests:", ex2);
							if (ADNotificationListener.nextWatsonReport.CompareTo(ExDateTime.Now) <= 0)
							{
								Globals.ReportNonTerminatingWatson(ex2);
								ADNotificationListener.nextWatsonReport = ExDateTime.Now.AddHours(1.0);
							}
							throw;
						}
						finally
						{
							if (!flag2)
							{
								notificationListenerState = NotificationListenerState.Idle;
								instance.TransitionToState(NotificationListenerState.Idle, "ReissueNotificationRequestsIdle", new NotificationListenerState[]
								{
									NotificationListenerState.Connecting
								});
							}
						}
						if (!flag3)
						{
							instance.TransitionToState(NotificationListenerState.Listening, "ReissueNotificationRequestsListening", new NotificationListenerState[]
							{
								NotificationListenerState.Connecting
							});
							ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)instance.GetHashCode(), "Invoking ConnectionRestored callbacks now");
							List<ADNotificationRequest> list = new List<ADNotificationRequest>(instance.clientNotificationRequests.Count + instance.deletedObjectsNotificationRequests.Count);
							list.AddRange(instance.clientNotificationRequests);
							list.AddRange(instance.deletedObjectsNotificationRequests);
							foreach (ADNotificationRequest adnotificationRequest in list)
							{
								ADNotificationEventArgs args = new ADNotificationEventArgs(ADNotificationChangeType.ConnectionRestored, adnotificationRequest.Context, null, null, adnotificationRequest.Type);
								ADNotificationListener.InvokeCallback(adnotificationRequest, args);
							}
							break;
						}
					}
					if (flag3)
					{
						NotificationListenerState notificationListenerState2 = instance.state;
						Thread.Sleep(5000);
						if (instance.state != notificationListenerState2 && NotificationListenerState.Listening == instance.state)
						{
							notificationListenerState = NotificationListenerState.Listening;
							break;
						}
					}
				}
				catch (Exception e)
				{
					instance.LogStateException("Outer loop:", e);
					throw;
				}
				finally
				{
					if (flag)
					{
						lock (ADNotificationListener.InstanceLockRoot)
						{
							if (instance.state != notificationListenerState)
							{
								instance.TransitionToState(notificationListenerState, "ReissueNotificationRequestsRestoring");
							}
						}
					}
				}
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00014BAC File Offset: 0x00012DAC
		internal static void RegisterChangeNotification(ADNotificationRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			ADNotificationListener instance = ADNotificationListener.GetInstance();
			ExTraceGlobals.ADNotificationsTracer.TraceDebug<int, string, string>((long)instance.GetHashCode(), "Registering request {0} {1} {2}", request.GetHashCode(), request.ObjectClass, request.RootId.ToDNString());
			NotificationListenerState newState = NotificationListenerState.Idle;
			bool flag = false;
			List<SearchRequest> notificationRequest = null;
			bool flag2 = false;
			bool flag3 = false;
			try
			{
				lock (ADNotificationListener.InstanceLockRoot)
				{
					instance.WaitForStates(new NotificationListenerState[]
					{
						NotificationListenerState.Idle,
						NotificationListenerState.Listening
					});
					flag2 = (instance.state == NotificationListenerState.Idle);
					ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)instance.GetHashCode(), "There are {0} current requests, {1} deleted object request and the state is {2}.  We {3}need to issue requests against the AD.", new object[]
					{
						instance.clientNotificationRequests.Count,
						instance.deletedObjectsNotificationRequests.Count,
						instance.state.ToString(),
						flag2 ? string.Empty : "do not "
					});
					if (flag2)
					{
						newState = NotificationListenerState.Idle;
						flag = true;
						instance.TransitionToState(NotificationListenerState.Connecting, "RegisterChangeNotification");
						notificationRequest = instance.GetAllNotificationsToIssue();
					}
					ExTraceGlobals.FaultInjectionTracer.TraceTest(4240846141U);
				}
				if (flag2)
				{
					while (!ADNotificationListener.IsStopping)
					{
						lock (ADNotificationListener.InstanceLockRoot)
						{
							if (instance.TryIssueNotificationRequests(notificationRequest))
							{
								flag3 = true;
								newState = NotificationListenerState.Listening;
								break;
							}
						}
						Thread.Sleep(1000);
					}
				}
				else
				{
					flag3 = true;
				}
			}
			finally
			{
				lock (ADNotificationListener.InstanceLockRoot)
				{
					if (flag)
					{
						instance.TransitionToState(newState, "RegisterChangeNotification");
					}
					if (flag3)
					{
						instance.clientNotificationRequests.Add(request);
					}
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00014E28 File Offset: 0x00013028
		internal static void RegisterChangeNotificationForDeletedObjects(ADNotificationRequest request)
		{
			if (!Globals.IsDatacenter)
			{
				return;
			}
			request.isDeletedContainer = true;
			ADNotificationListener instance = ADNotificationListener.GetInstance();
			instance.LoadPolicyConfigurationIfRequired(false);
			ExTraceGlobals.ADNotificationsTracer.TraceDebug<int, string, string>((long)instance.GetHashCode(), "Registering deleted notification request {0} {1} {2}", request.GetHashCode(), request.ObjectClass, request.RootId.ToDNString());
			NotificationListenerState newState = instance.state;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			try
			{
				lock (ADNotificationListener.InstanceLockRoot)
				{
					instance.WaitForStates(new NotificationListenerState[]
					{
						NotificationListenerState.Idle,
						NotificationListenerState.Listening
					});
					ADNotificationListener.DeletedNotificationTuple deletedNotificationTuple = instance.uniqueDeletedObjectsNotificationRequestTuples.FirstOrDefault((ADNotificationListener.DeletedNotificationTuple x) => x.BaseDN.Equals(request.RootId.DistinguishedName, StringComparison.OrdinalIgnoreCase));
					if (deletedNotificationTuple != null)
					{
						flag2 = false;
						deletedNotificationTuple.NumberOfListeners++;
						flag3 = true;
					}
					else
					{
						flag2 = (NotificationListenerState.Listening == instance.state);
						instance.uniqueDeletedObjectsNotificationRequestTuples.Add(new ADNotificationListener.DeletedNotificationTuple
						{
							BaseDN = request.RootId.DistinguishedName,
							NumberOfListeners = 1
						});
						if (!flag2)
						{
							flag3 = true;
						}
					}
					ExTraceGlobals.ADNotificationsTracer.TraceDebug((long)instance.GetHashCode(), "There are {0} current requests, {1} deleted object request and the state is {2}.  We {3}need to issue deleted notification requests against the AD.", new object[]
					{
						instance.clientNotificationRequests.Count,
						instance.deletedObjectsNotificationRequests.Count,
						instance.state.ToString(),
						flag2 ? string.Empty : "do not "
					});
					if (instance.uniqueDeletedObjectsNotificationRequestTuples.Count + 4 > instance.maximumNumberOfListenersPerConnection)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_MaximumNumberOrNotificationsForDeletedObjects, request.RootId.DistinguishedName, new object[]
						{
							request.RootId.DistinguishedName,
							instance.maximumNumberOfListenersPerConnection
						});
						flag2 = false;
					}
					if (flag2)
					{
						newState = NotificationListenerState.Listening;
						flag = true;
						instance.TransitionToState(NotificationListenerState.ConnectingForDeletedNofications, "IssueDeletedNotificationRequests");
						List<SearchRequest> notificationRequest = instance.ConstructRequestForDeletedNotification(request);
						ExTraceGlobals.FaultInjectionTracer.TraceTest(4240846141U);
						if (instance.TryIssueNotificationRequests(notificationRequest))
						{
							flag3 = true;
						}
						instance.TransitionToState(newState, "RegisterDeletedNotificationRequestsCompleted");
						flag = false;
					}
				}
				if (!flag3 && flag2)
				{
					flag = false;
					ADNotificationListener.ReissueNotificationRequests(true, true);
					flag3 = true;
				}
			}
			finally
			{
				lock (ADNotificationListener.InstanceLockRoot)
				{
					if (flag)
					{
						instance.TransitionToState(newState, "RegisterDeletedNotificationRequestsRestored");
					}
					if (flag3)
					{
						instance.deletedObjectsNotificationRequests.Add(request);
					}
					else
					{
						instance.uniqueDeletedObjectsNotificationRequestTuples.FirstOrDefault((ADNotificationListener.DeletedNotificationTuple x) => x.BaseDN.Equals(request.RootId.DistinguishedName, StringComparison.OrdinalIgnoreCase) && 1 == x.NumberOfListeners);
					}
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001515C File Offset: 0x0001335C
		internal static void UnRegisterChangeNotification(ADNotificationRequest request, bool block)
		{
			ADNotificationListener.UnRegisterChangeNotification(request, false, block);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00015166 File Offset: 0x00013366
		internal static void UnRegisterChangeNotificationForDeletedObjects(ADNotificationRequest request, bool block)
		{
			if (!Globals.IsDatacenter)
			{
				return;
			}
			ADNotificationListener.UnRegisterChangeNotification(request, true, block);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00015178 File Offset: 0x00013378
		private static ADNotificationListener GetInstance()
		{
			if (ADNotificationListener.staticInstance == null)
			{
				ADNotificationListener value = new ADNotificationListener();
				Interlocked.CompareExchange<ADNotificationListener>(ref ADNotificationListener.staticInstance, value, null);
			}
			return ADNotificationListener.staticInstance;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000151A4 File Offset: 0x000133A4
		private string NotificationRequestToString()
		{
			return string.Empty;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000151AC File Offset: 0x000133AC
		[Conditional("DEBUG")]
		internal static void DbgLogInvalidNotificationIfNeeded(ADObjectId rootId)
		{
			string distinguishedName = rootId.DistinguishedName;
			if (distinguishedName.Contains(ADObject.ConfigurationUnits) || distinguishedName.Contains("Microsoft Exchange Hosted Organizations"))
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_InvalidNotificationRequest, null, new object[]
				{
					rootId.DistinguishedName,
					Environment.StackTrace
				});
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00015200 File Offset: 0x00013400
		internal static void TraceParsedADNotificationEntry(string[] objectClasses, ADObjectId id, bool isDeleted, ADObjectId lastKnownParent)
		{
			ExTraceGlobals.ADNotificationsTracer.TraceDebug(0L, "Parsed notification result: dn={0} guid={1} isDeleted={2} lastknownparent={3}", new object[]
			{
				id.DistinguishedName,
				id.ObjectGuid.ToString(),
				isDeleted.ToString(),
				(lastKnownParent == null) ? "<null>" : lastKnownParent.ToDNString()
			});
		}

		// Token: 0x04000110 RID: 272
		private const uint ThreadAbortExceptionLid = 4240846141U;

		// Token: 0x04000111 RID: 273
		private const uint DirectoryExceptionLid = 4190514493U;

		// Token: 0x04000112 RID: 274
		private const string ReissueNotificationRequestsDisconnecting = "ReissueNotificationRequestsDisconnecting";

		// Token: 0x04000113 RID: 275
		private const string ReissueNotificationRequestsConnecting = "ReissueNotificationRequestsConnecting";

		// Token: 0x04000114 RID: 276
		private const string ReissueNotificationRequestsIdle = "ReissueNotificationRequestsIdle";

		// Token: 0x04000115 RID: 277
		private const string ReissueNotificationRequestsListening = "ReissueNotificationRequestsListening";

		// Token: 0x04000116 RID: 278
		private const string ReissueNotificationRequestsRestoring = "ReissueNotificationRequestsRestoring";

		// Token: 0x04000117 RID: 279
		private const string ReissueNotificationRequestsException = "ReissueNotificationRequestsException";

		// Token: 0x04000118 RID: 280
		private const string RegisterChangeNotificationMethodName = "RegisterChangeNotification";

		// Token: 0x04000119 RID: 281
		private const string UnRegisterChangeNotificationMethodName = "UnRegisterChangeNotification";

		// Token: 0x0400011A RID: 282
		private const string RegisterDeletedNotificationRequests = "IssueDeletedNotificationRequests";

		// Token: 0x0400011B RID: 283
		private const string RegisterDeletedNotificationRequestsCompleted = "RegisterDeletedNotificationRequestsCompleted";

		// Token: 0x0400011C RID: 284
		private const string RegisterDeletedNotificationRequestsRestored = "RegisterDeletedNotificationRequestsRestored";

		// Token: 0x0400011D RID: 285
		private const string NotificationObjectClassFilter = "(objectClass=*)";

		// Token: 0x0400011E RID: 286
		private const int DefaultMaximumNumberOfListenersPerConnection = 5;

		// Token: 0x0400011F RID: 287
		private static int NotificationThrottlingTimeMinutes = RegistrySettings.ExchangeServerCurrentVersion.NotificationThrottlingTimeMinutes;

		// Token: 0x04000120 RID: 288
		private static Timer issueSkippedNotificationAfterThrottling = new Timer(new TimerCallback(ADNotificationListener.ProcessQueuedNotifications), null, 0, ADNotificationListener.NotificationThrottlingTimeMinutes * 60 * 1000);

		// Token: 0x04000121 RID: 289
		private static List<string> exceptionProcessList = new List<string>
		{
			"edgetransport.exe",
			"MSExchangeDelivery.exe",
			"MSExchangeFrontendTransport.exe",
			"MSExchangeSubmission.exe"
		};

		// Token: 0x04000122 RID: 290
		private static readonly string[] NotificationProperties = new string[]
		{
			"distinguishedName",
			"objectGuid",
			"objectClass",
			"lastKnownParent",
			"isDeleted"
		};

		// Token: 0x04000123 RID: 291
		private static readonly object InstanceLockRoot = new object();

		// Token: 0x04000124 RID: 292
		private readonly object RequestsLock = new object();

		// Token: 0x04000125 RID: 293
		private static ADNotificationListener staticInstance;

		// Token: 0x04000126 RID: 294
		private static ExDateTime nextWatsonReport = ExDateTime.Now;

		// Token: 0x04000127 RID: 295
		private static volatile bool stop;

		// Token: 0x04000128 RID: 296
		private ADTopologyConfigurationSession session;

		// Token: 0x04000129 RID: 297
		private ADNotificationListener.RequestInstance currentRequests;

		// Token: 0x0400012A RID: 298
		private List<string> exceptionDNsList;

		// Token: 0x0400012B RID: 299
		private List<ADNotificationRequest> clientNotificationRequests;

		// Token: 0x0400012C RID: 300
		private List<ADNotificationRequest> deletedObjectsNotificationRequests;

		// Token: 0x0400012D RID: 301
		private List<ADNotificationListener.DeletedNotificationTuple> uniqueDeletedObjectsNotificationRequestTuples;

		// Token: 0x0400012E RID: 302
		private NotificationListenerState state;

		// Token: 0x0400012F RID: 303
		private ADQueryPolicy queryPolicy;

		// Token: 0x04000130 RID: 304
		private int maximumNumberOfListenersPerConnection = 5;

		// Token: 0x04000131 RID: 305
		private Breadcrumbs<ADNotificationListener.TransitionContext> stateLog = new Breadcrumbs<ADNotificationListener.TransitionContext>(64);

		// Token: 0x02000043 RID: 67
		internal class DeletedNotificationTuple
		{
			// Token: 0x04000134 RID: 308
			public string BaseDN;

			// Token: 0x04000135 RID: 309
			public int NumberOfListeners;
		}

		// Token: 0x02000044 RID: 68
		private sealed class RequestTuple
		{
			// Token: 0x04000136 RID: 310
			public IAsyncResult Handler;

			// Token: 0x04000137 RID: 311
			public string BaseDN;

			// Token: 0x04000138 RID: 312
			public bool IsDeletedNotification;
		}

		// Token: 0x02000045 RID: 69
		private sealed class RequestInstance
		{
			// Token: 0x04000139 RID: 313
			public List<ADNotificationListener.RequestTuple> NotifyHandles;

			// Token: 0x0400013A RID: 314
			public PooledLdapConnection Connection;
		}

		// Token: 0x02000046 RID: 70
		private sealed class TransitionContext
		{
			// Token: 0x060003B2 RID: 946 RVA: 0x0001533C File Offset: 0x0001353C
			public TransitionContext(NotificationListenerState newState, string method)
			{
				this.newState = newState;
				this.method = method;
				this.transitionTime = DateTime.UtcNow.ToString("o");
				this.threadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x00015380 File Offset: 0x00013580
			public override string ToString()
			{
				return string.Format("{0} at {1} by thread {2} -> {3}", new object[]
				{
					this.method,
					this.transitionTime,
					this.threadId,
					this.newState
				});
			}

			// Token: 0x0400013B RID: 315
			private readonly string method;

			// Token: 0x0400013C RID: 316
			private readonly NotificationListenerState newState;

			// Token: 0x0400013D RID: 317
			private readonly string transitionTime;

			// Token: 0x0400013E RID: 318
			private readonly int threadId;
		}
	}
}
