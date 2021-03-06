using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Xml.Linq;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.SubscriptionSubmission;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Manager.Throttling;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SubscriptionDispatcher : IDispatcher
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0001122C File Offset: 0x0000F42C
		public SubscriptionDispatcher()
		{
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)336UL, this.diag, "Starting Dispatcher", new object[0]);
			StatefulHubPicker.Instance.Start();
			SubscriptionDispatcher.SubscriptionRemoting.Initialize();
			if (SubscriptionDispatcher.SubscriptionRemoting.IsTestReady)
			{
				SubscriptionDispatcher.SubscriptionRemoting.OnWindowBegin();
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public XElement GetDiagnosticInfo(SyncDiagnosticMode mode)
		{
			XElement xelement = new XElement("Dispatcher");
			xelement.Add(new XElement("lastDispatchRpcAttemptTime", (this.lastDispatchRpcAttempt != null) ? this.lastDispatchRpcAttempt.Value.ToString("o") : string.Empty));
			return xelement;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00011310 File Offset: 0x0000F510
		public void Shutdown()
		{
			lock (this.syncObject)
			{
				if (this.shuttingDown)
				{
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)337UL, this.diag, "ShutdownDispatcher: Already was in shutdown. Doing nothing.", new object[0]);
					return;
				}
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)338UL, this.diag, "ShutdownDispatcher: Shutting down Dispatcher.", new object[0]);
				this.shuttingDown = true;
			}
			lock (this.pendingRpcThreads)
			{
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)339UL, this.diag, "ShutdownDispatcher: Canceling all pendingRpcThreads.", new object[0]);
				foreach (SafeThreadHandle threadHandle in this.pendingRpcThreads)
				{
					NativeMethods.RpcCancelThreadEx(threadHandle, 15);
				}
				this.pendingRpcThreads.Clear();
			}
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)340UL, this.diag, "ShutdownDispatcher: All current pendingRpcThreads cancelled.", new object[0]);
			lock (this.syncObject)
			{
				if (this.shuttingDown)
				{
					StatefulHubPicker.Instance.Shutdown();
				}
			}
			if (SubscriptionDispatcher.SubscriptionRemoting.IsTestReady)
			{
				SubscriptionDispatcher.SubscriptionRemoting.OnWindowEnd();
			}
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)341UL, this.diag, "Dispatcher is stopped.", new object[0]);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000114E4 File Offset: 0x0000F6E4
		public DispatchResult DispatchSubscription(DispatchEntry dispatchEntry, ISubscriptionInformation subscriptionInformation)
		{
			SyncUtilities.ThrowIfArgumentNull("dispatchEntry", dispatchEntry);
			SyncUtilities.ThrowIfArgumentNull("subscriptionInformation", subscriptionInformation);
			SyncLogSession syncLogSession = ContentAggregationConfig.SyncLog.OpenSession(dispatchEntry.MiniSubscriptionInformation.MailboxGuid, dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid);
			ExDateTime dispatchAttemptTime = dispatchEntry.DispatchAttemptTime;
			DispatchResult dispatchResult = this.ProcessSubscription(syncLogSession, (SubscriptionInformation)subscriptionInformation, dispatchAttemptTime, dispatchEntry.WorkType);
			syncLogSession.LogDebugging((TSLID)1300UL, this.diag, "DispatchSubscription: Attempt compelete. databaseGuid:{0}, workType:{1}, dispatchResult:{2}.", new object[]
			{
				dispatchEntry.MiniSubscriptionInformation.DatabaseGuid,
				dispatchEntry.WorkType,
				dispatchResult
			});
			return dispatchResult;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00011594 File Offset: 0x0000F794
		private DispatchResult ProcessSubscription(SyncLogSession syncLogSession, SubscriptionInformation subscriptionInformation, ExDateTime currentDispatchAttemptTime, WorkType workType)
		{
			WorkTypeDefinition workTypeDefinition = WorkTypeManager.Instance.GetWorkTypeDefinition(workType);
			ExDateTime? lastSuccessfulDispatchTime = subscriptionInformation.LastSuccessfulDispatchTime;
			ExDateTime? firstOutstandingDispatchTime = subscriptionInformation.FirstOutstandingDispatchTime;
			string hubServerDispatched = subscriptionInformation.HubServerDispatched;
			if (!string.IsNullOrEmpty(subscriptionInformation.HubServerDispatched))
			{
				syncLogSession.LogVerbose((TSLID)220UL, this.diag, "ProcessSubscription: The last dispatch attempt '{0}' hasn't reported a completion yet. The first dispatch attempt was at '{1}'", new object[]
				{
					subscriptionInformation.LastSuccessfulDispatchTime,
					subscriptionInformation.FirstOutstandingDispatchTime
				});
				subscriptionInformation.MarkSyncTimeOut();
			}
			if (!StatefulHubPicker.Instance.IsSubscriptionTypeEnabled(subscriptionInformation.SubscriptionType))
			{
				syncLogSession.LogVerbose((TSLID)356UL, this.diag, "ProcessSubscription: No hub servers with subscription type: {0} enabled.", new object[]
				{
					subscriptionInformation.SubscriptionType
				});
				subscriptionInformation.MarkLastSuccessfulDispatch(new ExDateTime?(currentDispatchAttemptTime));
				subscriptionInformation.TrySave(syncLogSession);
				return DispatchResult.PermanentFailure;
			}
			ContentAggregationHubServer contentAggregationHubServer;
			SubscriptionSubmissionResult? subscriptionSubmissionResult;
			if (!StatefulHubPicker.Instance.TryGetServerForDispatch(out contentAggregationHubServer, out subscriptionSubmissionResult))
			{
				syncLogSession.LogVerbose((TSLID)357UL, this.diag, "ProcessSubscription: No hub servers found to dispatch.", new object[0]);
				subscriptionInformation.TrySave(syncLogSession);
				if (subscriptionSubmissionResult == null)
				{
					return DispatchResult.NoHubsToDispatchTo;
				}
				return this.GetDispatchResultFromLastSubmissionResult(syncLogSession, subscriptionSubmissionResult.Value);
			}
			else
			{
				subscriptionInformation.MarkOutstandingDispatch(currentDispatchAttemptTime, contentAggregationHubServer.MachineName);
				if (!subscriptionInformation.TrySave(null))
				{
					syncLogSession.LogVerbose((TSLID)358UL, this.diag, "ProcessSubscription: Subscription information could not be updated so we're moving on to next subscription.", new object[0]);
					return DispatchResult.PermanentFailure;
				}
				byte[] subscriptionSubmitBytes = this.GetSubscriptionSubmitBytes(subscriptionInformation, workTypeDefinition.IsSyncNow, contentAggregationHubServer.MachineName);
				bool flag = false;
				DispatchResult result;
				try
				{
					Stopwatch stopwatch = Stopwatch.StartNew();
					SubscriptionSubmissionResult subscriptionSubmissionResult2;
					if (SubscriptionDispatcher.SubscriptionRemoting.IsTestReady)
					{
						subscriptionSubmissionResult2 = SubscriptionDispatcher.SubscriptionRemoting.DispatchSubscription(contentAggregationHubServer.MachineName, subscriptionSubmitBytes);
					}
					else
					{
						subscriptionSubmissionResult2 = this.DispatchSubscriptionToHubServer(contentAggregationHubServer, subscriptionSubmitBytes);
					}
					stopwatch.Stop();
					TimeSpan elapsed = stopwatch.Elapsed;
					StatefulHubPicker.Instance.ProcessDispatchResult(contentAggregationHubServer, subscriptionInformation.SubscriptionType, subscriptionSubmissionResult2, currentDispatchAttemptTime);
					DispatchResult dispatchResult = this.DetermineDispatchResultAndUpdatePerfCounters(syncLogSession, subscriptionSubmissionResult2, subscriptionInformation.SubscriptionType, elapsed, out flag);
					if (flag && workTypeDefinition.WorkType == WorkType.AggregationIncremental && lastSuccessfulDispatchTime != null)
					{
						TimeSpan actualSyncInterval = currentDispatchAttemptTime - lastSuccessfulDispatchTime.Value;
						ManagerPerfCounterHandler.Instance.AddSubscriptionSyncInterval(workTypeDefinition.TimeTillSyncDue, actualSyncInterval);
					}
					result = dispatchResult;
				}
				finally
				{
					if (!flag)
					{
						subscriptionInformation.MarkFailedDispatch(lastSuccessfulDispatchTime, firstOutstandingDispatchTime, hubServerDispatched);
						subscriptionInformation.TrySave(syncLogSession);
					}
					syncLogSession.LogDebugging((TSLID)362UL, this.diag, "ProcessSubscription: Subscription processed.", new object[0]);
				}
				return result;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0001181C File Offset: 0x0000FA1C
		private DispatchResult DetermineDispatchResultAndUpdatePerfCounters(SyncLogSession syncLogSession, SubscriptionSubmissionResult dispatchReturnCode, AggregationSubscriptionType subscriptionType, TimeSpan dispatchDuration, out bool dispatchSuccess)
		{
			this.UpdatePerfCountersForCurrentDispatch(dispatchReturnCode, subscriptionType, dispatchDuration);
			dispatchSuccess = (dispatchReturnCode == SubscriptionSubmissionResult.Success || dispatchReturnCode == SubscriptionSubmissionResult.SubscriptionAlreadyOnHub);
			if (dispatchSuccess)
			{
				syncLogSession.LogVerbose((TSLID)359UL, this.diag, "ProcessSubscription: Submission returned code {0}.", new object[]
				{
					dispatchReturnCode
				});
				return DispatchResult.Success;
			}
			syncLogSession.LogError((TSLID)360UL, this.diag, "ProcessSubscription: Dispatch failed with code {0}.", new object[]
			{
				dispatchReturnCode
			});
			return this.GetDispatchResultFromLastSubmissionResult(syncLogSession, dispatchReturnCode);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000118AC File Offset: 0x0000FAAC
		private void UpdatePerfCountersForCurrentDispatch(SubscriptionSubmissionResult dispatchReturnCode, AggregationSubscriptionType subscriptionType, TimeSpan dispatchDuration)
		{
			if (dispatchReturnCode != SubscriptionSubmissionResult.Success && dispatchReturnCode != SubscriptionSubmissionResult.SubscriptionAlreadyOnHub)
			{
				ManagerPerfCounterHandler.Instance.IncrementTemporarySubmissionFailures(subscriptionType);
				return;
			}
			long valueInMilliSeconds = Convert.ToInt64(dispatchDuration.TotalMilliseconds);
			ManagerPerfCounterHandler.Instance.IncrementAverageDispatchTimeBy(subscriptionType, valueInMilliSeconds);
			ManagerPerfCounterHandler.Instance.IncrementAverageDispatchTimeBase(subscriptionType);
			ManagerPerfCounterHandler.Instance.SetTimeToCompleteLastDispatch(subscriptionType, valueInMilliSeconds);
			if (dispatchReturnCode == SubscriptionSubmissionResult.SubscriptionAlreadyOnHub)
			{
				ManagerPerfCounterHandler.Instance.IncrementDuplicateSubmissions(subscriptionType);
				return;
			}
			ManagerPerfCounterHandler.Instance.IncrementSuccessfulSubmissions(subscriptionType);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00011918 File Offset: 0x0000FB18
		private DispatchResult GetDispatchResultFromLastSubmissionResult(SyncLogSession syncLogSession, SubscriptionSubmissionResult lastSubmissionResult)
		{
			syncLogSession.LogDebugging((TSLID)400UL, "ProcessSubscription: Last Subscription Submission result to the Hub is {0}", new object[]
			{
				lastSubmissionResult
			});
			switch (lastSubmissionResult)
			{
			case SubscriptionSubmissionResult.SchedulerQueueFull:
				return DispatchResult.WorkerSlotsFull;
			case SubscriptionSubmissionResult.MaxConcurrentMailboxSubmissions:
				return DispatchResult.MaxConcurrentMailboxSubmissions;
			case SubscriptionSubmissionResult.RpcServerTooBusy:
			case SubscriptionSubmissionResult.RetryableRpcError:
				return DispatchResult.UnableToContactWorker;
			case SubscriptionSubmissionResult.DatabaseRpcLatencyUnhealthy:
				return DispatchResult.DatabaseRpcLatencyUnhealthy;
			case SubscriptionSubmissionResult.DatabaseHealthUnknown:
				return DispatchResult.DatabaseHealthUnknown;
			case SubscriptionSubmissionResult.DatabaseOverloaded:
				return DispatchResult.DatabaseOverloaded;
			case SubscriptionSubmissionResult.ServerNotAvailable:
				return DispatchResult.ServerNotAvailable;
			case SubscriptionSubmissionResult.EdgeTransportStopped:
				return DispatchResult.EdgeTransportStopped;
			case SubscriptionSubmissionResult.SubscriptionTypeDisabled:
				return DispatchResult.SubscriptionTypeDisabled;
			case SubscriptionSubmissionResult.TransportSyncDisabled:
				return DispatchResult.TransportSyncDisabled;
			case SubscriptionSubmissionResult.MailboxServerCpuUnhealthy:
				return DispatchResult.MailboxServerCpuOverloaded;
			case SubscriptionSubmissionResult.MailboxServerCpuUnknown:
				return DispatchResult.MailboxServerCpuUnknown;
			case SubscriptionSubmissionResult.MailboxServerHAUnhealthy:
				return DispatchResult.MailboxServerHAUnhealthy;
			default:
				return DispatchResult.TransientFailure;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000119E4 File Offset: 0x0000FBE4
		private byte[] GetSubscriptionSubmitBytes(SubscriptionInformation subscriptionInformation, bool isSyncNow, string serverName)
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection[2684420127U] = subscriptionInformation.UserLegacyDn;
			mdbefPropertyCollection[2684354562U] = (short)subscriptionInformation.SubscriptionType;
			mdbefPropertyCollection[2684551240U] = subscriptionInformation.SubscriptionGuid;
			mdbefPropertyCollection[2684485890U] = subscriptionInformation.SubscriptionMessageId.GetBytes();
			mdbefPropertyCollection[2684616715U] = subscriptionInformation.RecoverySyncEnabled;
			mdbefPropertyCollection[2684682312U] = subscriptionInformation.DatabaseGuid;
			mdbefPropertyCollection[2684747848U] = subscriptionInformation.MailboxGuid;
			mdbefPropertyCollection[2684878920U] = subscriptionInformation.TenantGuid;
			mdbefPropertyCollection[2684813343U] = serverName;
			mdbefPropertyCollection[2684944386U] = (short)subscriptionInformation.AggregationType;
			mdbefPropertyCollection[2685009931U] = (subscriptionInformation.SyncPhase == SyncPhase.Initial);
			mdbefPropertyCollection[2685337602U] = (short)subscriptionInformation.SyncPhase;
			mdbefPropertyCollection[2685075714U] = subscriptionInformation.SerializedSubscription.GetBytes();
			mdbefPropertyCollection[2685141003U] = isSyncNow;
			mdbefPropertyCollection[2685272136U] = SubscriptionDispatcher.CurrentServerGuid;
			if (subscriptionInformation.SyncWatermark != null)
			{
				mdbefPropertyCollection[2685206559U] = subscriptionInformation.SyncWatermark;
			}
			return mdbefPropertyCollection.GetBytes();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00011B54 File Offset: 0x0000FD54
		private SubscriptionSubmissionResult DispatchSubscriptionToHubServer(ContentAggregationHubServer server, byte[] subscriptionBytes)
		{
			SubscriptionSubmissionResult result = SubscriptionSubmissionResult.Success;
			SafeThreadHandle currentThreadHandle = SafeThreadHandle.GetCurrentThreadHandle();
			try
			{
				lock (this.pendingRpcThreads)
				{
					if (currentThreadHandle != null)
					{
						this.pendingRpcThreads.Add(currentThreadHandle);
					}
				}
				result = this.RpcSubscriptionSubmit(server, subscriptionBytes);
			}
			catch (RpcException ex)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)363UL, this.diag, "Submission to server {0} failed with RpcException {1}.", new object[]
				{
					server.MachineName,
					ex.Message
				});
				if (ex.ErrorCode == SubscriptionSubmissionRpcClient.RpcServerTooBusy)
				{
					result = SubscriptionSubmissionResult.RpcServerTooBusy;
				}
				else if (ex.ErrorCode == SubscriptionSubmissionRpcClient.RpcCallFailedDidNotExecute)
				{
					result = SubscriptionSubmissionResult.RetryableRpcError;
				}
				else
				{
					result = SubscriptionSubmissionResult.ServerNotAvailable;
				}
				return result;
			}
			finally
			{
				lock (this.pendingRpcThreads)
				{
					if (currentThreadHandle != null)
					{
						this.pendingRpcThreads.Remove(currentThreadHandle);
						currentThreadHandle.Close();
					}
				}
			}
			return result;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00011C80 File Offset: 0x0000FE80
		private SubscriptionSubmissionResult RpcSubscriptionSubmit(ContentAggregationHubServer server, byte[] subscriptionBytes)
		{
			SubscriptionSubmissionResult result = SubscriptionSubmissionResult.Success;
			this.lastDispatchRpcAttempt = new ExDateTime?(ExDateTime.UtcNow);
			byte[] array = server.RpcClient.SubscriptionSubmit(0, subscriptionBytes);
			MdbefPropertyCollection mdbefPropertyCollection = MdbefPropertyCollection.Create(array, 0, array.Length);
			object obj;
			if (mdbefPropertyCollection.TryGetValue(2835349507U, out obj) && obj is int)
			{
				result = (SubscriptionSubmissionResult)((int)obj);
			}
			return result;
		}

		// Token: 0x04000174 RID: 372
		private const int DefaultCapacityDispatcherThreads = 16;

		// Token: 0x04000175 RID: 373
		private const int RpcCancellationTimeoutSeconds = 15;

		// Token: 0x04000176 RID: 374
		private static readonly Guid CurrentServerGuid = Guid.NewGuid();

		// Token: 0x04000177 RID: 375
		private readonly Microsoft.Exchange.Diagnostics.Trace diag = ExTraceGlobals.SubscriptionSubmitTracer;

		// Token: 0x04000178 RID: 376
		private readonly object syncObject = new object();

		// Token: 0x04000179 RID: 377
		private bool shuttingDown;

		// Token: 0x0400017A RID: 378
		private List<SafeThreadHandle> pendingRpcThreads = new List<SafeThreadHandle>(16);

		// Token: 0x0400017B RID: 379
		private ExDateTime? lastDispatchRpcAttempt = null;

		// Token: 0x02000033 RID: 51
		internal static class SubscriptionRemoting
		{
			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600029A RID: 666 RVA: 0x00011CE3 File Offset: 0x0000FEE3
			internal static bool IsTestReady
			{
				get
				{
					return SubscriptionDispatcher.SubscriptionRemoting.testReady;
				}
			}

			// Token: 0x0600029B RID: 667 RVA: 0x00011CEC File Offset: 0x0000FEEC
			internal static void Initialize()
			{
				try
				{
					if (!SubscriptionDispatcher.SubscriptionRemoting.TryLoadAssembly() || !SubscriptionDispatcher.SubscriptionRemoting.TryGetType() || !SubscriptionDispatcher.SubscriptionRemoting.TryGetMethod())
					{
						ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)364UL, ExTraceGlobals.SubscriptionSubmitTracer, 0L, "Fail load Assembly, Type and Method", new object[0]);
					}
					else
					{
						SubscriptionDispatcher.SubscriptionRemoting.remotingObject = Activator.GetObject(SubscriptionDispatcher.SubscriptionRemoting.testType, SubscriptionDispatcher.SubscriptionRemoting.tcpConnection + SubscriptionDispatcher.SubscriptionRemoting.testType.Name);
						SubscriptionDispatcher.SubscriptionRemoting.testReady = true;
					}
				}
				catch (RemotingException ex)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)365UL, ExTraceGlobals.SubscriptionSubmitTracer, "Remoting Error {0}", new object[]
					{
						ex
					});
				}
				catch (ConfigurationErrorsException ex2)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)366UL, ExTraceGlobals.SubscriptionSubmitTracer, "Configuration Error {0}", new object[]
					{
						ex2
					});
				}
				catch (FileNotFoundException ex3)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)367UL, ExTraceGlobals.SubscriptionSubmitTracer, "Specified file not found {0}", new object[]
					{
						ex3
					});
				}
				catch (BadImageFormatException ex4)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)369UL, ExTraceGlobals.SubscriptionSubmitTracer, "Bad image format {0}", new object[]
					{
						ex4
					});
				}
				catch (ArgumentException)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)370UL, ExTraceGlobals.SubscriptionSubmitTracer, "Invalid type name", new object[0]);
				}
				catch (AmbiguousMatchException ex5)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)371UL, ExTraceGlobals.SubscriptionSubmitTracer, "Ambiguous match {0}", new object[]
					{
						ex5
					});
				}
			}

			// Token: 0x0600029C RID: 668 RVA: 0x00011EDC File Offset: 0x000100DC
			internal static void OnWindowBegin()
			{
				Exception ex = null;
				try
				{
					SubscriptionDispatcher.SubscriptionRemoting.testOnWindowBeginMethod.Invoke(SubscriptionDispatcher.SubscriptionRemoting.remotingObject, null);
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)372UL, ExTraceGlobals.SubscriptionSubmitTracer, 0L, "OnWindowBegin test method called", new object[0]);
				}
				catch (TargetException ex2)
				{
					ex = ex2;
				}
				catch (TargetInvocationException ex3)
				{
					ex = ex3;
				}
				catch (TargetParameterCountException ex4)
				{
					ex = ex4;
				}
				catch (MethodAccessException ex5)
				{
					ex = ex5;
				}
				catch (InvalidOperationException ex6)
				{
					ex = ex6;
				}
				if (ex != null)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)373UL, ExTraceGlobals.SubscriptionSubmitTracer, "Test OnWindowBegin remoting failed with error {0}", new object[]
					{
						ex
					});
				}
			}

			// Token: 0x0600029D RID: 669 RVA: 0x00011FB4 File Offset: 0x000101B4
			internal static void OnWindowEnd()
			{
				Exception ex = null;
				try
				{
					SubscriptionDispatcher.SubscriptionRemoting.testOnWindowEndMethod.Invoke(SubscriptionDispatcher.SubscriptionRemoting.remotingObject, null);
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)374UL, ExTraceGlobals.SubscriptionSubmitTracer, 0L, "OnWindowEnd test method called", new object[0]);
				}
				catch (TargetException ex2)
				{
					ex = ex2;
				}
				catch (TargetInvocationException ex3)
				{
					ex = ex3;
				}
				catch (TargetParameterCountException ex4)
				{
					ex = ex4;
				}
				catch (MethodAccessException ex5)
				{
					ex = ex5;
				}
				catch (InvalidOperationException ex6)
				{
					ex = ex6;
				}
				if (ex != null)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)375UL, ExTraceGlobals.SubscriptionSubmitTracer, "Test OnWindowEnd remoting failed with error {0}", new object[]
					{
						ex
					});
				}
			}

			// Token: 0x0600029E RID: 670 RVA: 0x0001208C File Offset: 0x0001028C
			internal static SubscriptionSubmissionResult DispatchSubscription(string hubServerName, byte[] subscriptionBytes)
			{
				if (subscriptionBytes == null)
				{
					throw new ArgumentNullException("subscriptionBytes");
				}
				SubscriptionSubmissionResult subscriptionSubmissionResult = SubscriptionSubmissionResult.Success;
				object[] parameters = new object[]
				{
					hubServerName,
					subscriptionBytes
				};
				Exception ex = null;
				byte[] array = null;
				try
				{
					array = (byte[])SubscriptionDispatcher.SubscriptionRemoting.testDispatchMethod.Invoke(SubscriptionDispatcher.SubscriptionRemoting.remotingObject, parameters);
				}
				catch (TargetException ex2)
				{
					ex = ex2;
				}
				catch (TargetInvocationException ex3)
				{
					ex = ex3;
				}
				catch (TargetParameterCountException ex4)
				{
					ex = ex4;
				}
				catch (MethodAccessException ex5)
				{
					ex = ex5;
				}
				catch (InvalidOperationException ex6)
				{
					ex = ex6;
				}
				if (ex != null)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)376UL, ExTraceGlobals.SubscriptionSubmitTracer, "Test remoting failed with {0}.", new object[]
					{
						ex
					});
					subscriptionSubmissionResult = SubscriptionSubmissionResult.UnknownRetryableError;
				}
				if (array != null)
				{
					MdbefPropertyCollection mdbefPropertyCollection = MdbefPropertyCollection.Create(array, 0, array.Length);
					object obj;
					if (mdbefPropertyCollection.TryGetValue(2835349507U, out obj) && obj is int)
					{
						subscriptionSubmissionResult = (SubscriptionSubmissionResult)((int)obj);
					}
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)377UL, ExTraceGlobals.SubscriptionSubmitTracer, "Length of result is {0}, dispatch return code is {1}", new object[]
					{
						array.Length,
						subscriptionSubmissionResult
					});
				}
				else
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)378UL, ExTraceGlobals.SubscriptionSubmitTracer, "No test delegate registered in test case", new object[0]);
				}
				return subscriptionSubmissionResult;
			}

			// Token: 0x0600029F RID: 671 RVA: 0x00012204 File Offset: 0x00010404
			private static bool TryLoadAssembly()
			{
				string text = string.Empty;
				try
				{
					text = ConfigurationManager.AppSettings["DispatcherTestModule"];
				}
				catch (ConfigurationErrorsException ex)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)379UL, ExTraceGlobals.SubscriptionSubmitTracer, "TryLoadAssembly : Error during trying to read app.config. {0}", new object[]
					{
						ex
					});
				}
				if (string.IsNullOrEmpty(text))
				{
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)380UL, ExTraceGlobals.SubscriptionSubmitTracer, 0L, "No DispatcherTestModule specified in config file", new object[0]);
					return false;
				}
				SubscriptionDispatcher.SubscriptionRemoting.testAssembly = Assembly.LoadFrom(text);
				return true;
			}

			// Token: 0x060002A0 RID: 672 RVA: 0x000122A4 File Offset: 0x000104A4
			private static bool TryGetType()
			{
				string text = string.Empty;
				try
				{
					text = ConfigurationManager.AppSettings["RemotingTestType"];
				}
				catch (ConfigurationErrorsException ex)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)381UL, ExTraceGlobals.SubscriptionSubmitTracer, "TryGetType : Error during trying to read app.config. {0}", new object[]
					{
						ex
					});
				}
				if (string.IsNullOrEmpty(text))
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)382UL, ExTraceGlobals.SubscriptionSubmitTracer, "No RemotingTestType specified in config file", new object[0]);
					return false;
				}
				SubscriptionDispatcher.SubscriptionRemoting.testType = SubscriptionDispatcher.SubscriptionRemoting.testAssembly.GetType(text);
				if (SubscriptionDispatcher.SubscriptionRemoting.testType == null)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)383UL, ExTraceGlobals.SubscriptionSubmitTracer, "Can not find specified type {0} from assembly {1}", new object[]
					{
						text,
						SubscriptionDispatcher.SubscriptionRemoting.testAssembly.FullName
					});
					return false;
				}
				return true;
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x00012390 File Offset: 0x00010590
			private static bool TryGetMethod()
			{
				string text = string.Empty;
				try
				{
					text = ConfigurationManager.AppSettings["RemotingTestDispatchMethod"];
				}
				catch (ConfigurationErrorsException ex)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)384UL, ExTraceGlobals.SubscriptionSubmitTracer, "TryGetMethod : Error during trying to read app.config. {0}", new object[]
					{
						ex
					});
				}
				if (string.IsNullOrEmpty(text))
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)385UL, ExTraceGlobals.SubscriptionSubmitTracer, "No RemotingTestDispatchMethod specified", new object[0]);
					return false;
				}
				SubscriptionDispatcher.SubscriptionRemoting.testDispatchMethod = SubscriptionDispatcher.SubscriptionRemoting.testType.GetMethod(text);
				if (SubscriptionDispatcher.SubscriptionRemoting.testDispatchMethod == null)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)386UL, ExTraceGlobals.SubscriptionSubmitTracer, "Can't find specified Method {0} in Type {1}", new object[]
					{
						text,
						SubscriptionDispatcher.SubscriptionRemoting.testType.FullName
					});
					return false;
				}
				try
				{
					text = ConfigurationManager.AppSettings["RemotingTestWindowBeginMethod"];
				}
				catch (ConfigurationErrorsException ex2)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)387UL, ExTraceGlobals.SubscriptionSubmitTracer, "TryGetMethod : Error during trying to read app.config. {0}", new object[]
					{
						ex2
					});
				}
				if (string.IsNullOrEmpty(text))
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)388UL, ExTraceGlobals.SubscriptionSubmitTracer, "No RemotingTestWindowBeginMethod specified", new object[0]);
					return false;
				}
				SubscriptionDispatcher.SubscriptionRemoting.testOnWindowBeginMethod = SubscriptionDispatcher.SubscriptionRemoting.testType.GetMethod(text);
				if (SubscriptionDispatcher.SubscriptionRemoting.testOnWindowBeginMethod == null)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)389UL, ExTraceGlobals.SubscriptionSubmitTracer, "Can't find specified Method {0} in Type {1}", new object[]
					{
						text,
						SubscriptionDispatcher.SubscriptionRemoting.testType.FullName
					});
					return false;
				}
				try
				{
					text = ConfigurationManager.AppSettings["RemotingTestWindowEndMethod"];
				}
				catch (ConfigurationErrorsException ex3)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)390UL, ExTraceGlobals.SubscriptionSubmitTracer, "TryGetMethod : Error during trying to read app.config. {0}", new object[]
					{
						ex3
					});
				}
				if (string.IsNullOrEmpty(text))
				{
					ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)391UL, ExTraceGlobals.SubscriptionSubmitTracer, "No RemotingTestWindowEndMethod specified", new object[0]);
					return false;
				}
				SubscriptionDispatcher.SubscriptionRemoting.testOnWindowEndMethod = SubscriptionDispatcher.SubscriptionRemoting.testType.GetMethod(text);
				if (SubscriptionDispatcher.SubscriptionRemoting.testOnWindowEndMethod == null)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)392UL, ExTraceGlobals.SubscriptionSubmitTracer, "Can't find specified Method {0} in Type {1}", new object[]
					{
						text,
						SubscriptionDispatcher.SubscriptionRemoting.testType.FullName
					});
					return false;
				}
				return true;
			}

			// Token: 0x0400017C RID: 380
			private static readonly string tcpConnection = "tcp://" + Environment.MachineName + ":780/";

			// Token: 0x0400017D RID: 381
			private static Assembly testAssembly;

			// Token: 0x0400017E RID: 382
			private static Type testType;

			// Token: 0x0400017F RID: 383
			private static MethodInfo testDispatchMethod;

			// Token: 0x04000180 RID: 384
			private static MethodInfo testOnWindowBeginMethod;

			// Token: 0x04000181 RID: 385
			private static MethodInfo testOnWindowEndMethod;

			// Token: 0x04000182 RID: 386
			private static object remotingObject;

			// Token: 0x04000183 RID: 387
			private static bool testReady;
		}
	}
}
