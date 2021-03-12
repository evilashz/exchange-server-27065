using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.LinkedIn;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.LinkedIn
{
	// Token: 0x020001E5 RID: 485
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LinkedInSyncStorageProvider : ISyncStorageProvider, ISyncStorageProviderItemRetriever
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x000315B4 File Offset: 0x0002F7B4
		public AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return AggregationSubscriptionType.LinkedIn;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000315B8 File Offset: 0x0002F7B8
		public SyncStorageProviderState Bind(ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery)
		{
			IPeopleConnectApplicationConfig peopleConnectApplicationConfig = this.ReadConfiguration();
			IConnectSubscription connectSubscription = (IConnectSubscription)subscription;
			string accessTokenInClearText = connectSubscription.AccessTokenInClearText;
			if (string.IsNullOrEmpty(accessTokenInClearText))
			{
				throw SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, new Exception("Access token is null or empty"));
			}
			string accessTokenSecretInClearText = connectSubscription.AccessTokenSecretInClearText;
			if (string.IsNullOrEmpty(accessTokenSecretInClearText))
			{
				throw SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, new Exception("Access secret is null or empty"));
			}
			LinkedInAppConfig linkedInAppConfig = new LinkedInAppConfig(peopleConnectApplicationConfig.AppId, peopleConnectApplicationConfig.AppSecretClearText, peopleConnectApplicationConfig.ProfileEndpoint, peopleConnectApplicationConfig.ConnectionsEndpoint, peopleConnectApplicationConfig.RemoveAppEndpoint, peopleConnectApplicationConfig.WebRequestTimeout, peopleConnectApplicationConfig.WebProxyUri);
			return new LinkedInSyncStorageProviderState(subscription, syncLogSession, linkedInAppConfig, new LinkedInWebClient(linkedInAppConfig, LinkedInSyncStorageProvider.Tracer));
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00031659 File Offset: 0x0002F859
		public void Unbind(SyncStorageProviderState state)
		{
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003165C File Offset: 0x0002F85C
		public IAsyncResult BeginAuthenticate(SyncStorageProviderState state, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			SyncUtilities.ThrowIfArgumentNull("state", state);
			LinkedInSyncStorageProviderState linkedInSyncStorageProviderState = (LinkedInSyncStorageProviderState)state;
			linkedInSyncStorageProviderState.CurrentWatermark = LinkedInSyncStorageProvider.GetWatermarkFromDateTime(DateTime.UtcNow);
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult = new AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>(this, linkedInSyncStorageProviderState, callback, callbackState, syncPoisonContext);
			asyncResult.SetCompletedSynchronously();
			asyncResult.ProcessCompleted();
			return asyncResult;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000316A4 File Offset: 0x0002F8A4
		public AsyncOperationResult<SyncProviderResultData> EndAuthenticate(IAsyncResult asyncResult)
		{
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult2 = (AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00031744 File Offset: 0x0002F944
		public IAsyncResult BeginCheckForChanges(SyncStorageProviderState state, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			SyncUtilities.ThrowIfArgumentNull("state", state);
			LinkedInSyncStorageProviderState syncState = (LinkedInSyncStorageProviderState)state;
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult = new AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>(this, syncState, callback, callbackState, syncPoisonContext);
			LinkedInSyncStorageProvider.LinkedInRequestParameters parameterInfo = LinkedInSyncStorageProvider.GetLinkedInRequestParameters(syncState, null);
			syncState.TriggerRequestEvent();
			LinkedInSyncStorageProvider.HandleExceptionsInBeginCall(asyncResult, delegate
			{
				asyncResult.PendingAsyncResult = syncState.LinkedInClient.BeginGetLinkedInConnections(parameterInfo.Url, parameterInfo.AuthorizationHeader, syncState.Config.WebRequestTimeout, syncState.Config.WebProxy, asyncResult.GetAsyncCallbackWithPoisonContextAndUnhandledExceptionRedirect(new AsyncCallback(LinkedInSyncStorageProvider.OnCheckForChangesCompleted)), asyncResult);
			});
			return asyncResult;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000317C0 File Offset: 0x0002F9C0
		private static void HandleExceptionsInBeginCall(AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult, Action action)
		{
			try
			{
				action();
			}
			catch (WebException e)
			{
				asyncResult.ProcessCompleted(LinkedInSyncStorageProvider.NewNonPromotableException(e));
			}
			catch (Exception ex)
			{
				asyncResult.State.SyncLogSession.ReportWatson(ex);
				asyncResult.ProcessCompleted(LinkedInSyncStorageProvider.NewNonPromotableException(ex));
			}
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00031820 File Offset: 0x0002FA20
		public AsyncOperationResult<SyncProviderResultData> EndCheckForChanges(IAsyncResult asyncResult)
		{
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult2 = (AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003198C File Offset: 0x0002FB8C
		public IAsyncResult BeginEnumerateChanges(SyncStorageProviderState syncState, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			LinkedInSyncStorageProvider.<>c__DisplayClass5 CS$<>8__locals1 = new LinkedInSyncStorageProvider.<>c__DisplayClass5();
			CS$<>8__locals1.linkedInProviderState = (LinkedInSyncStorageProviderState)syncState;
			IConnectSubscription connectSubscription = (IConnectSubscription)syncState.Subscription;
			CS$<>8__locals1.asyncResult = new AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>(this, CS$<>8__locals1.linkedInProviderState, callback, callbackState, syncPoisonContext);
			switch (connectSubscription.SyncPhase)
			{
			case SyncPhase.Initial:
			{
				int num = 0;
				using (IEnumerator<string> cloudItemEnumerator = CS$<>8__locals1.linkedInProviderState.StateStorage.GetCloudItemEnumerator())
				{
					while (cloudItemEnumerator.MoveNext())
					{
						if (num == 0)
						{
							string cloudId = cloudItemEnumerator.Current;
							string text;
							string currentWatermark;
							if (CS$<>8__locals1.linkedInProviderState.StateStorage.TryFindItem(cloudId, out text, out currentWatermark))
							{
								CS$<>8__locals1.linkedInProviderState.CurrentWatermark = currentWatermark;
							}
						}
						num++;
					}
				}
				string text2 = num.ToString(CultureInfo.InvariantCulture);
				LinkedInSyncStorageProvider.LinkedInRequestParameters requestInformationForInitial = LinkedInSyncStorageProvider.GetLinkedInRequestParameters(CS$<>8__locals1.linkedInProviderState, text2);
				CS$<>8__locals1.linkedInProviderState.SyncLogSession.LogDebugging((TSLID)361UL, LinkedInSyncStorageProvider.Tracer, "BeginEnumerateChanges: Calling {0} to get connections, start offset: {1}.", new object[]
				{
					requestInformationForInitial.Url,
					text2
				});
				LinkedInSyncStorageProvider.HandleExceptionsInBeginCall(CS$<>8__locals1.asyncResult, delegate
				{
					CS$<>8__locals1.asyncResult.PendingAsyncResult = CS$<>8__locals1.linkedInProviderState.LinkedInClient.BeginGetLinkedInConnections(requestInformationForInitial.Url, requestInformationForInitial.AuthorizationHeader, CS$<>8__locals1.linkedInProviderState.Config.WebRequestTimeout, CS$<>8__locals1.linkedInProviderState.Config.WebProxy, CS$<>8__locals1.asyncResult.GetAsyncCallbackWithPoisonContextAndUnhandledExceptionRedirect(new AsyncCallback(LinkedInSyncStorageProvider.OnEnumerateChangesCompleted)), CS$<>8__locals1.asyncResult);
				});
				break;
			}
			case SyncPhase.Incremental:
				if (CS$<>8__locals1.linkedInProviderState.CloudData != null)
				{
					syncState.Changes = LinkedInSyncStorageProvider.BuildSyncChangeEntries(CS$<>8__locals1.linkedInProviderState, false);
					CS$<>8__locals1.asyncResult.SetCompletedSynchronously();
					CS$<>8__locals1.asyncResult.ProcessCompleted(new SyncProviderResultData(syncState.Changes, false, false, syncState.Changes.Count, CS$<>8__locals1.linkedInProviderState.MoreItemsAvailable));
				}
				else
				{
					LinkedInSyncStorageProvider.LinkedInRequestParameters requestInformationForIncremental = LinkedInSyncStorageProvider.GetLinkedInRequestParameters(CS$<>8__locals1.linkedInProviderState, null);
					CS$<>8__locals1.linkedInProviderState.SyncLogSession.LogDebugging((TSLID)1490UL, LinkedInSyncStorageProvider.Tracer, "BeginEnumerateChanges: Calling {0} to get connections.", new object[]
					{
						requestInformationForIncremental.Url
					});
					LinkedInSyncStorageProvider.HandleExceptionsInBeginCall(CS$<>8__locals1.asyncResult, delegate
					{
						CS$<>8__locals1.asyncResult.PendingAsyncResult = CS$<>8__locals1.linkedInProviderState.LinkedInClient.BeginGetLinkedInConnections(requestInformationForIncremental.Url, requestInformationForIncremental.AuthorizationHeader, CS$<>8__locals1.linkedInProviderState.Config.WebRequestTimeout, CS$<>8__locals1.linkedInProviderState.Config.WebProxy, CS$<>8__locals1.asyncResult.GetAsyncCallbackWithPoisonContextAndUnhandledExceptionRedirect(new AsyncCallback(LinkedInSyncStorageProvider.OnEnumerateChangesForIncrementalSyncCompleted)), CS$<>8__locals1.asyncResult);
					});
				}
				break;
			}
			return CS$<>8__locals1.asyncResult;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00031BE0 File Offset: 0x0002FDE0
		public AsyncOperationResult<SyncProviderResultData> EndEnumerateChanges(IAsyncResult asyncResult)
		{
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult2 = (AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00031BFC File Offset: 0x0002FDFC
		public IAsyncResult BeginGetItem(object itemRetrieverState, SyncChangeEntry item, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			LinkedInSyncStorageProviderState linkedInSyncStorageProviderState = (LinkedInSyncStorageProviderState)itemRetrieverState;
			AsyncResult<LinkedInSyncStorageProviderState, SyncChangeEntry> asyncResult = new AsyncResult<LinkedInSyncStorageProviderState, SyncChangeEntry>(this, linkedInSyncStorageProviderState, callback, callbackState, syncPoisonContext);
			linkedInSyncStorageProviderState.ItemBeingRetrieved = item;
			asyncResult.ProcessCompleted(linkedInSyncStorageProviderState.ItemBeingRetrieved);
			return asyncResult;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00031C34 File Offset: 0x0002FE34
		public AsyncOperationResult<SyncChangeEntry> EndGetItem(IAsyncResult asyncResult)
		{
			AsyncResult<LinkedInSyncStorageProviderState, SyncChangeEntry> asyncResult2 = (AsyncResult<LinkedInSyncStorageProviderState, SyncChangeEntry>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00031C50 File Offset: 0x0002FE50
		public IAsyncResult BeginAcknowledgeChanges(SyncStorageProviderState state, IList<SyncChangeEntry> changeList, bool hasPermanentSyncErrors, bool hasTransientSyncErrors, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			SyncUtilities.ThrowIfArgumentNull("state", state);
			LinkedInSyncStorageProviderState linkedInSyncStorageProviderState = (LinkedInSyncStorageProviderState)state;
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult = new AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>(this, linkedInSyncStorageProviderState, callback, callbackState, syncPoisonContext);
			linkedInSyncStorageProviderState.HasPermanentSyncErrors = hasPermanentSyncErrors;
			linkedInSyncStorageProviderState.HasTransientSyncErrors = hasTransientSyncErrors;
			if (linkedInSyncStorageProviderState.HasTransientSyncErrors)
			{
				linkedInSyncStorageProviderState.SyncLogSession.LogVerbose((TSLID)293UL, LinkedInSyncStorageProvider.Tracer, "Sync had transient errors, setting MoreItemsAvailable = true.", new object[0]);
				linkedInSyncStorageProviderState.MoreItemsAvailable = true;
			}
			if (!linkedInSyncStorageProviderState.MoreItemsAvailable && !linkedInSyncStorageProviderState.HasTransientSyncErrors)
			{
				((StringWatermark)linkedInSyncStorageProviderState.BaseWatermark).Save(linkedInSyncStorageProviderState.CurrentWatermark);
				linkedInSyncStorageProviderState.SyncLogSession.LogInformation((TSLID)1487UL, LinkedInSyncStorageProvider.Tracer, "Watermark has been set to: {0}.", new object[]
				{
					linkedInSyncStorageProviderState.CurrentWatermark
				});
			}
			else
			{
				linkedInSyncStorageProviderState.SyncLogSession.LogInformation((TSLID)462UL, LinkedInSyncStorageProvider.Tracer, "BeginAcknowledgeChanges: Skipping Watermark update, MoreItemsAvailable:{0}, HasTransientSyncErrors:{1}.", new object[]
				{
					linkedInSyncStorageProviderState.MoreItemsAvailable,
					linkedInSyncStorageProviderState.HasTransientSyncErrors
				});
			}
			asyncResult.SetCompletedSynchronously();
			asyncResult.ProcessCompleted(SyncProviderResultData.CreateAcknowledgeChangesResult(linkedInSyncStorageProviderState.Changes, linkedInSyncStorageProviderState.HasPermanentSyncErrors, linkedInSyncStorageProviderState.HasTransientSyncErrors, (linkedInSyncStorageProviderState.Changes == null) ? 0 : linkedInSyncStorageProviderState.Changes.Count, linkedInSyncStorageProviderState.MoreItemsAvailable));
			return asyncResult;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00031DA0 File Offset: 0x0002FFA0
		private static string GetWatermarkFromDateTime(DateTime syncStartTime)
		{
			return Convert.ToInt64((syncStartTime - LinkedInSyncStorageProvider.EpochUtc).TotalMilliseconds).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00031DD4 File Offset: 0x0002FFD4
		public AsyncOperationResult<SyncProviderResultData> EndAcknowledgeChanges(IAsyncResult asyncResult)
		{
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult2 = (AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>)asyncResult;
			return asyncResult2.WaitForCompletion();
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00031DEE File Offset: 0x0002FFEE
		public IAsyncResult BeginApplyChanges(SyncStorageProviderState state, IList<SyncChangeEntry> changeList, ISyncStorageProviderItemRetriever itemRetriever, object itemRetrieverState, AsyncCallback callback, object callbackState, object syncPoisonContext)
		{
			throw new NotSupportedException("Two-way sync is not supported by the LinkedIn provider");
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00031DFA File Offset: 0x0002FFFA
		public AsyncOperationResult<SyncProviderResultData> EndApplyChanges(IAsyncResult asyncResult)
		{
			throw new NotSupportedException("Two-way sync is not supported by the LinkedIn provider");
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00031E06 File Offset: 0x00030006
		public void CancelGetItem(IAsyncResult asyncResult)
		{
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00031E08 File Offset: 0x00030008
		public void Cancel(IAsyncResult asyncResult)
		{
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult2 = (AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>)asyncResult;
			ILinkedInWebClient linkedInClient = asyncResult2.State.LinkedInClient;
			IAsyncResult pendingAsyncResult = asyncResult2.PendingAsyncResult;
			if (linkedInClient != null && pendingAsyncResult != null)
			{
				linkedInClient.Abort(pendingAsyncResult);
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00031E3C File Offset: 0x0003003C
		private static LinkedInSyncStorageProvider.LinkedInRequestParameters GetLinkedInRequestParameters(LinkedInSyncStorageProviderState syncState, string startOffset)
		{
			IConnectSubscription connectSubscription = (IConnectSubscription)syncState.Subscription;
			string accessTokenInClearText = connectSubscription.AccessTokenInClearText;
			string accessTokenSecretInClearText = connectSubscription.AccessTokenSecretInClearText;
			LinkedInSyncStorageProvider.LinkedInRequestParameters linkedInRequestParameters = new LinkedInSyncStorageProvider.LinkedInRequestParameters();
			switch (connectSubscription.SyncPhase)
			{
			case SyncPhase.Initial:
			{
				NameValueCollection queryParameters = new NameValueCollection
				{
					{
						"start",
						startOffset
					},
					{
						"count",
						syncState.MaxDownloadItemsPerConnection.ToString(CultureInfo.InvariantCulture)
					}
				};
				linkedInRequestParameters.Url = LinkedInSyncStorageProvider.BuildConnectionsEndpointUrl(syncState.Config.ConnectionsEndpoint, queryParameters);
				linkedInRequestParameters.AuthorizationHeader = LinkedInSyncStorageProvider.GetAuthorizationHeader(linkedInRequestParameters.Url, queryParameters, accessTokenInClearText, accessTokenSecretInClearText, syncState.Config.AppId, syncState.Config.AppSecret);
				break;
			}
			case SyncPhase.Incremental:
			{
				string syncWatermark = syncState.SyncWatermark;
				syncState.SyncLogSession.LogInformation((TSLID)519UL, LinkedInSyncStorageProvider.Tracer, "GetLinkedInRequestParameters: Watermark retrieved is {0}.", new object[]
				{
					syncWatermark
				});
				NameValueCollection queryParameters2 = new NameValueCollection
				{
					{
						"modified-since",
						syncWatermark
					}
				};
				linkedInRequestParameters.Url = LinkedInSyncStorageProvider.BuildConnectionsEndpointUrl(syncState.Config.ConnectionsEndpoint, queryParameters2);
				linkedInRequestParameters.AuthorizationHeader = LinkedInSyncStorageProvider.GetAuthorizationHeader(linkedInRequestParameters.Url, queryParameters2, accessTokenInClearText, accessTokenSecretInClearText, syncState.Config.AppId, syncState.Config.AppSecret);
				break;
			}
			default:
			{
				string message = string.Format("Sync Phase: {0}, not supported for the LinkedIn Provider", connectSubscription.SyncPhase);
				throw new InvalidOperationException(message);
			}
			}
			return linkedInRequestParameters;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00031FBC File Offset: 0x000301BC
		private static string BuildConnectionsEndpointUrl(string connectionsEndpoint, NameValueCollection queryParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}:({1})?", connectionsEndpoint, LinkedInSyncStorageProvider.UserFields);
			if (queryParameters != null && queryParameters.Count > 0)
			{
				for (int i = 0; i < queryParameters.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append("&");
					}
					stringBuilder.AppendFormat("{0}={1}", queryParameters.GetKey(i), queryParameters.Get(i));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00032030 File Offset: 0x00030230
		private static IList<SyncChangeEntry> BuildSyncChangeEntries(LinkedInSyncStorageProviderState syncState, bool enforceLimit)
		{
			LinkedInConnections cloudData = syncState.CloudData;
			IList<SyncChangeEntry> list = new List<SyncChangeEntry>();
			if (cloudData != null && cloudData.People != null)
			{
				int num = cloudData.People.Count;
				if (num > syncState.MaxDownloadItemsPerConnection)
				{
					AggregationComponent.EventLogger.LogEvent(TransportSyncWorkerEventLogConstants.Tuple_LinkedInProviderRequestTooManyItems, null, new object[]
					{
						num,
						syncState.MaxDownloadItemsPerConnection
					});
					syncState.SyncLogSession.LogError((TSLID)217UL, LinkedInSyncStorageProvider.Tracer, "BuildSyncChangeEntries: LinkedIn Provider downloaded more items during incremental sync than the maximum expected. Total items downloaded was {0}, but MaxDownloadItemsPerConnection value is {1}.", new object[]
					{
						num,
						syncState.MaxDownloadItemsPerConnection
					});
					if (enforceLimit)
					{
						num = syncState.MaxDownloadItemsPerConnection;
					}
				}
				for (int i = 0; i < num; i++)
				{
					LinkedInPerson linkedInPerson = cloudData.People[i];
					if (string.IsNullOrEmpty(linkedInPerson.Id) || linkedInPerson.Id.Equals("private", StringComparison.OrdinalIgnoreCase))
					{
						syncState.SyncLogSession.LogError((TSLID)294UL, LinkedInSyncStorageProvider.Tracer, "BuildSyncChangeEntries: contact id is either BLANK or PRIVATE.  First name: '{0}'; Last name: '{1}'.  Skipping.", new object[]
						{
							linkedInPerson.FirstName,
							linkedInPerson.LastName
						});
					}
					else
					{
						string text;
						string text2;
						list.Add(new SyncChangeEntry(syncState.StateStorage.TryFindItem(linkedInPerson.Id, out text, out text2) ? ChangeType.Change : ChangeType.Add, SchemaType.Contact, linkedInPerson.Id, new LinkedInContact(linkedInPerson, new ExDateTime(ExTimeZone.UtcTimeZone, syncState.Subscription.CreationTime)))
						{
							CloudVersion = syncState.CurrentWatermark
						});
						syncState.TriggerContactDownloadEvent();
					}
				}
			}
			return list;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000321E0 File Offset: 0x000303E0
		private static LinkedInSyncStorageProvider.GetOperationResult ProcessEndGetOperation(IAsyncResult ar)
		{
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult = (AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>)ar.AsyncState;
			LinkedInSyncStorageProviderState state = asyncResult.State;
			LinkedInSyncStorageProvider.GetOperationResult getOperationResult = new LinkedInSyncStorageProvider.GetOperationResult();
			try
			{
				getOperationResult.Connections = state.LinkedInClient.EndGetLinkedInConnections(ar);
			}
			catch (TimeoutException ex)
			{
				state.SyncLogSession.LogError((TSLID)1495UL, LinkedInSyncStorageProvider.Tracer, "EndGetLinkedInConnections: Received a Timeout exception: {0}, generating a transient connection error operation exception", new object[]
				{
					ex
				});
				getOperationResult.Exception = LinkedInSyncStorageProvider.NewNonPromotableException(ex);
			}
			catch (WebException ex2)
			{
				state.SyncLogSession.LogError((TSLID)1496UL, LinkedInSyncStorageProvider.Tracer, "EndGetLinkedInConnections: Exception is {0}", new object[]
				{
					ex2
				});
				if (HttpWebRequestExceptionHandler.IsUnAuthorizedException(ex2))
				{
					getOperationResult.Exception = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, ex2);
				}
				else if (HttpWebRequestExceptionHandler.IsConnectionException(ex2, null))
				{
					getOperationResult.Exception = SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, ex2);
				}
				else
				{
					getOperationResult.Exception = LinkedInSyncStorageProvider.NewNonPromotableException(ex2);
				}
			}
			catch (Exception ex3)
			{
				state.SyncLogSession.LogError((TSLID)1497UL, LinkedInSyncStorageProvider.Tracer, "EndGetLinkedInConnections: Exception is {0}", new object[]
				{
					ex3
				});
				getOperationResult.Exception = LinkedInSyncStorageProvider.NewNonPromotableException(ex3);
			}
			return getOperationResult;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0003233C File Offset: 0x0003053C
		private static SyncTransientException NewNonPromotableException(Exception e)
		{
			return SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ProviderException, new LinkedInNonPromotableTransientException(e), true);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0003234C File Offset: 0x0003054C
		private static void OnEnumerateChangesCompleted(IAsyncResult ar)
		{
			LinkedInSyncStorageProvider.GetOperationResult getOperationResult = LinkedInSyncStorageProvider.ProcessEndGetOperation(ar);
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult = (AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>)ar.AsyncState;
			LinkedInSyncStorageProviderState state = asyncResult.State;
			if (getOperationResult.Exception != null)
			{
				asyncResult.ProcessCompleted(getOperationResult.Exception);
				return;
			}
			state.CloudData = getOperationResult.Connections;
			state.MoreItemsAvailable = false;
			if (getOperationResult.Connections != null && getOperationResult.Connections.People != null && getOperationResult.Connections.People.Count >= state.MaxDownloadItemsPerConnection)
			{
				state.SyncLogSession.LogInformation((TSLID)1503UL, LinkedInSyncStorageProvider.Tracer, "OnEnumerateChangesCompleted: Reached MaxDownloadItemsPerConnection {0}. Need to sync again from LinkedIn.", new object[]
				{
					getOperationResult.Connections.People.Count
				});
				state.MoreItemsAvailable = true;
			}
			state.Changes = LinkedInSyncStorageProvider.BuildSyncChangeEntries(state, true);
			state.SyncLogSession.LogInformation((TSLID)1498UL, LinkedInSyncStorageProvider.Tracer, "OnEnumerateChangesCompleted: {0} need to be synced to the mailbox.", new object[]
			{
				state.Changes.Count
			});
			asyncResult.ProcessCompleted(new SyncProviderResultData(state.Changes, false, false, state.Changes.Count, state.MoreItemsAvailable));
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00032480 File Offset: 0x00030680
		private static void OnEnumerateChangesForIncrementalSyncCompleted(IAsyncResult ar)
		{
			LinkedInSyncStorageProvider.GetOperationResult getOperationResult = LinkedInSyncStorageProvider.ProcessEndGetOperation(ar);
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult = (AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>)ar.AsyncState;
			LinkedInSyncStorageProviderState state = asyncResult.State;
			if (getOperationResult.Exception != null)
			{
				asyncResult.ProcessCompleted(getOperationResult.Exception);
				return;
			}
			state.CloudData = getOperationResult.Connections;
			state.MoreItemsAvailable = false;
			state.Changes = LinkedInSyncStorageProvider.BuildSyncChangeEntries(state, false);
			state.SyncLogSession.LogInformation((TSLID)1500UL, LinkedInSyncStorageProvider.Tracer, "OnEnumerateChangesCompleted: {0} need to be synced to the mailbox.", new object[]
			{
				state.Changes.Count
			});
			asyncResult.ProcessCompleted(new SyncProviderResultData(state.Changes, false, false, state.Changes.Count, state.MoreItemsAvailable));
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0003253C File Offset: 0x0003073C
		private static void OnCheckForChangesCompleted(IAsyncResult ar)
		{
			LinkedInSyncStorageProvider.GetOperationResult getOperationResult = LinkedInSyncStorageProvider.ProcessEndGetOperation(ar);
			AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData> asyncResult = (AsyncResult<LinkedInSyncStorageProviderState, SyncProviderResultData>)ar.AsyncState;
			LinkedInSyncStorageProviderState state = asyncResult.State;
			if (getOperationResult.Exception != null)
			{
				asyncResult.ProcessCompleted(getOperationResult.Exception);
				return;
			}
			state.CloudData = getOperationResult.Connections;
			int num = 0;
			if (getOperationResult.Connections != null && getOperationResult.Connections.People != null && getOperationResult.Connections.People.Count > 0)
			{
				num = getOperationResult.Connections.People.Count;
				state.HasNoChangesOnCloud = false;
			}
			else
			{
				state.HasNoChangesOnCloud = true;
			}
			state.SyncLogSession.LogInformation((TSLID)1499UL, LinkedInSyncStorageProvider.Tracer, "OnCheckForChangesCompleted: {0} need to be synced to the mailbox.", new object[]
			{
				num
			});
			state.Changes = new List<SyncChangeEntry>();
			asyncResult.ProcessCompleted(new SyncProviderResultData(state.Changes, false, false, state.HasNoChangesOnCloud));
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00032627 File Offset: 0x00030827
		private static string GetAuthorizationHeader(string url, NameValueCollection queryParameters, string accessToken, string accessSecret, string appId, string appSecret)
		{
			return new LinkedInOAuth(LinkedInSyncStorageProvider.Tracer).GetAuthorizationHeader(url, "GET", queryParameters, accessToken, accessSecret, appId, appSecret);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00032648 File Offset: 0x00030848
		private IPeopleConnectApplicationConfig ReadConfiguration()
		{
			IPeopleConnectApplicationConfig result;
			try
			{
				result = CachedPeopleConnectApplicationConfig.Instance.ReadLinkedIn();
			}
			catch (ExchangeConfigurationException innerException)
			{
				throw SyncPermanentException.CreateOperationLevelException(DetailedAggregationStatus.ConfigurationError, innerException);
			}
			return result;
		}

		// Token: 0x040008B0 RID: 2224
		private const string QueryParameterModifiedSince = "modified-since";

		// Token: 0x040008B1 RID: 2225
		private const string QueryParameterStart = "start";

		// Token: 0x040008B2 RID: 2226
		private const string QueryParameterMaxNumberOfItemsRequested = "count";

		// Token: 0x040008B3 RID: 2227
		private const string PrivateId = "private";

		// Token: 0x040008B4 RID: 2228
		private static readonly DateTime EpochUtc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x040008B5 RID: 2229
		private static readonly Trace Tracer = ExTraceGlobals.LinkedInProviderTracer;

		// Token: 0x040008B6 RID: 2230
		private static readonly string UserFields = string.Join(",", new string[]
		{
			"date-of-birth",
			"educations",
			"email-address",
			"first-name",
			"headline",
			"id",
			"im-accounts",
			"last-name",
			"main-address",
			"phone-numbers",
			"picture-urls::(original)",
			"public-profile-url",
			"three-current-positions"
		});

		// Token: 0x020001E6 RID: 486
		private class LinkedInRequestParameters
		{
			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x0600100E RID: 4110 RVA: 0x00032739 File Offset: 0x00030939
			// (set) Token: 0x0600100F RID: 4111 RVA: 0x00032741 File Offset: 0x00030941
			public string Url { get; set; }

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x06001010 RID: 4112 RVA: 0x0003274A File Offset: 0x0003094A
			// (set) Token: 0x06001011 RID: 4113 RVA: 0x00032752 File Offset: 0x00030952
			public string AuthorizationHeader { get; set; }
		}

		// Token: 0x020001E7 RID: 487
		private class GetOperationResult
		{
			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x06001013 RID: 4115 RVA: 0x00032763 File Offset: 0x00030963
			// (set) Token: 0x06001014 RID: 4116 RVA: 0x0003276B File Offset: 0x0003096B
			public LinkedInConnections Connections { get; set; }

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x06001015 RID: 4117 RVA: 0x00032774 File Offset: 0x00030974
			// (set) Token: 0x06001016 RID: 4118 RVA: 0x0003277C File Offset: 0x0003097C
			public Exception Exception { get; set; }
		}
	}
}
