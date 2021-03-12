using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000289 RID: 649
	internal class MobileSpeechRecoDispatcher : IUMAsyncComponent
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x00055685 File Offset: 0x00053885
		private MobileSpeechRecoDispatcher()
		{
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000556B1 File Offset: 0x000538B1
		public static MobileSpeechRecoDispatcher TestHookCreateInstance()
		{
			return new MobileSpeechRecoDispatcher();
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x000556B8 File Offset: 0x000538B8
		public static MobileSpeechRecoDispatcher Instance
		{
			get
			{
				if (MobileSpeechRecoDispatcher.instance == null)
				{
					lock (MobileSpeechRecoDispatcher.staticLock)
					{
						if (MobileSpeechRecoDispatcher.instance == null)
						{
							MobileSpeechRecoTracer.TraceDebug(null, Guid.Empty, "Creating singleton instance of MobileSpeechRecoDispatcher", new object[0]);
							MobileSpeechRecoDispatcher.instance = new MobileSpeechRecoDispatcher();
						}
					}
				}
				return MobileSpeechRecoDispatcher.instance;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00055724 File Offset: 0x00053924
		public bool IsStopping
		{
			get
			{
				bool result;
				lock (this.lockObj)
				{
					result = this.syncIsStopping;
				}
				return result;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00055768 File Offset: 0x00053968
		public int NumPendingRequests
		{
			get
			{
				int count;
				lock (this.lockObj)
				{
					count = this.syncSpeechRecoRequestTable.Count;
				}
				return count;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x000557B0 File Offset: 0x000539B0
		public AutoResetEvent StoppedEvent
		{
			get
			{
				return this.stoppedEvent;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x000557B8 File Offset: 0x000539B8
		public bool IsInitialized
		{
			get
			{
				bool result;
				lock (this.lockObj)
				{
					result = this.syncIsInitialized;
				}
				return result;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x000557FC File Offset: 0x000539FC
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00055809 File Offset: 0x00053A09
		public void CleanupAfterStopped()
		{
			if (this.stoppedEvent != null)
			{
				this.stoppedEvent.Close();
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00055820 File Offset: 0x00053A20
		public void StartNow(StartupStage stage)
		{
			if (stage == StartupStage.WPActivation)
			{
				lock (this.lockObj)
				{
					if (this.syncIsInitialized)
					{
						throw new InvalidOperationException();
					}
					this.syncMaxConcurrentRequests = GlobCfg.MaxMobileSpeechRecoRequestsPerCore * Environment.ProcessorCount;
					MobileSpeechRecoTracer.TraceDebug(this, Guid.Empty, "Maximum number of concurrent requests='{0}'", new object[]
					{
						this.syncMaxConcurrentRequests
					});
					this.syncIsInitialized = true;
					MobileSpeechRecoTracer.TraceDebug(this, Guid.Empty, "{0} starting in stage {1}", new object[]
					{
						this.Name,
						stage
					});
				}
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000558D8 File Offset: 0x00053AD8
		public void StopAsync()
		{
			lock (this.lockObj)
			{
				this.syncIsStopping = true;
				MobileSpeechRecoTracer.TraceDebug(this, Guid.Empty, "{0} stopping", new object[]
				{
					this.Name
				});
				if (this.NumPendingRequests <= 0)
				{
					this.stoppedEvent.Set();
					MobileSpeechRecoTracer.TraceDebug(this, Guid.Empty, "{0} stopped", new object[]
					{
						this.Name
					});
				}
			}
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00055970 File Offset: 0x00053B70
		public void AddRecoRequestAsync(Guid requestId, IMobileSpeechRecoRequestBehavior requestBehavior, IPlatformBuilder platformBuilder, MobileRecoAsyncCompletedDelegate callback)
		{
			ValidateArgument.NotNull(requestBehavior, "requestBehavior");
			ValidateArgument.NotNull(platformBuilder, "platformBuilder");
			ValidateArgument.NotNull(callback, "callback");
			MobileSpeechRecoTracer.TraceDebug(this, requestId, "Entering MobileSpeechRecoDispatcher.AddRecoRequestAsync", new object[0]);
			MobileSpeechRecoRequest mobileSpeechRecoRequest = null;
			Exception ex = null;
			try
			{
				mobileSpeechRecoRequest = new MobileSpeechRecoRequest(requestId, requestBehavior, platformBuilder);
				lock (this.lockObj)
				{
					try
					{
						this.ValidateNewRequest(requestId);
						this.syncSpeechRecoRequestTable.Add(requestId, mobileSpeechRecoRequest);
					}
					catch (MobileRecoRequestCannotBeHandledException ex2)
					{
						ex = ex2;
					}
					catch (MobileRecoInvalidRequestException ex3)
					{
						ex = ex3;
					}
				}
			}
			finally
			{
				if (ex != null)
				{
					mobileSpeechRecoRequest.Dispose();
					mobileSpeechRecoRequest = null;
				}
			}
			if (mobileSpeechRecoRequest != null)
			{
				MobileSpeechRecoTracer.TraceDebug(this, requestId, "Set up completed callback and start PrepareRecoRequestAsync", new object[0]);
				mobileSpeechRecoRequest.Completed += this.OnRequestCompleted;
				mobileSpeechRecoRequest.PrepareRecoRequestAsync(callback);
				return;
			}
			MobileRecoAsyncCompletedArgs mobileRecoAsyncCompletedArgs = new MobileRecoAsyncCompletedArgs(string.Empty, -1, ex);
			MobileSpeechRecoTracer.TraceError(this, requestId, "Request rejected Error:'{0}'", new object[]
			{
				mobileRecoAsyncCompletedArgs.Error
			});
			callback(mobileRecoAsyncCompletedArgs);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00055AAC File Offset: 0x00053CAC
		public void RecognizeAsync(Guid requestId, byte[] audioBytes, MobileRecoAsyncCompletedDelegate callback)
		{
			ValidateArgument.NotNull(audioBytes, "audioBytes");
			ValidateArgument.NotNull(callback, "callback");
			MobileSpeechRecoTracer.TraceDebug(this, requestId, "Entering MobileSpeechRecoDispatcher.RecognizeAsync", new object[0]);
			MobileSpeechRecoRequest mobileSpeechRecoRequest = null;
			lock (this.lockObj)
			{
				this.syncSpeechRecoRequestTable.TryGetValue(requestId, out mobileSpeechRecoRequest);
			}
			if (mobileSpeechRecoRequest != null)
			{
				MobileSpeechRecoTracer.TraceDebug(this, requestId, "Start RecognizeAsync", new object[0]);
				mobileSpeechRecoRequest.RecognizeAsync(audioBytes, callback);
				return;
			}
			MobileRecoAsyncCompletedArgs mobileRecoAsyncCompletedArgs = new MobileRecoAsyncCompletedArgs(string.Empty, -1, new InvalidRecoRequestIdException(requestId));
			MobileSpeechRecoTracer.TraceError(this, requestId, "Request rejected Error:'{0}'", new object[]
			{
				mobileRecoAsyncCompletedArgs.Error
			});
			callback(mobileRecoAsyncCompletedArgs);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00055B78 File Offset: 0x00053D78
		private void OnRequestCompleted(object sender, EventArgs e)
		{
			MobileSpeechRecoRequest mobileSpeechRecoRequest = sender as MobileSpeechRecoRequest;
			Guid id = mobileSpeechRecoRequest.Id;
			MobileSpeechRecoTracer.TraceDebug(this, id, "Entering MobileSpeechRecoDispatcher.OnRequestCompleted", new object[0]);
			lock (this.lockObj)
			{
				bool condition = this.syncSpeechRecoRequestTable.Remove(id);
				ExAssert.RetailAssert(condition, "Tried to remove request '{0}' but it was not found in the table", new object[]
				{
					id
				});
				if (this.syncIsStopping && this.NumPendingRequests <= 0)
				{
					this.stoppedEvent.Set();
					MobileSpeechRecoTracer.TraceDebug(this, id, "{0} stopped after last request was completed", new object[]
					{
						this.Name
					});
				}
			}
			MobileSpeechRecoTracer.TraceDebug(this, id, "Disposing request", new object[0]);
			mobileSpeechRecoRequest.Dispose();
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00055C58 File Offset: 0x00053E58
		private void ValidateNewRequest(Guid requestId)
		{
			lock (this.lockObj)
			{
				if (!this.syncIsInitialized)
				{
					throw new MobileRecoDispatcherNotInitializedException();
				}
				if (this.syncIsStopping)
				{
					throw new MobileRecoDispatcherStoppingException();
				}
				if (this.syncSpeechRecoRequestTable.Count >= this.syncMaxConcurrentRequests)
				{
					throw new MaxMobileRecoRequestsReachedException(this.syncSpeechRecoRequestTable.Count, this.syncMaxConcurrentRequests);
				}
				if (requestId == Guid.Empty)
				{
					throw new EmptyRecoRequestIdException(requestId);
				}
				if (this.syncSpeechRecoRequestTable.ContainsKey(requestId))
				{
					throw new DuplicateRecoRequestIdException(requestId);
				}
			}
		}

		// Token: 0x04000C54 RID: 3156
		private static object staticLock = new object();

		// Token: 0x04000C55 RID: 3157
		private static MobileSpeechRecoDispatcher instance;

		// Token: 0x04000C56 RID: 3158
		private AutoResetEvent stoppedEvent = new AutoResetEvent(false);

		// Token: 0x04000C57 RID: 3159
		private object lockObj = new object();

		// Token: 0x04000C58 RID: 3160
		private bool syncIsInitialized;

		// Token: 0x04000C59 RID: 3161
		private bool syncIsStopping;

		// Token: 0x04000C5A RID: 3162
		private Dictionary<Guid, MobileSpeechRecoRequest> syncSpeechRecoRequestTable = new Dictionary<Guid, MobileSpeechRecoRequest>(64);

		// Token: 0x04000C5B RID: 3163
		private int syncMaxConcurrentRequests;
	}
}
