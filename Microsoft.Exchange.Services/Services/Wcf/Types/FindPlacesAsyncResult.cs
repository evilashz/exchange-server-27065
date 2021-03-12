using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A50 RID: 2640
	internal class FindPlacesAsyncResult : ServiceAsyncResult<Persona[]>
	{
		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06004ACE RID: 19150 RVA: 0x00104D0D File Offset: 0x00102F0D
		// (set) Token: 0x06004ACF RID: 19151 RVA: 0x00104D15 File Offset: 0x00102F15
		public BingRequestAsyncState LocationAsyncState { get; private set; }

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06004AD0 RID: 19152 RVA: 0x00104D1E File Offset: 0x00102F1E
		// (set) Token: 0x06004AD1 RID: 19153 RVA: 0x00104D26 File Offset: 0x00102F26
		public BingRequestAsyncState PhonebookAsyncState { get; private set; }

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06004AD2 RID: 19154 RVA: 0x00104D2F File Offset: 0x00102F2F
		// (set) Token: 0x06004AD3 RID: 19155 RVA: 0x00104D37 File Offset: 0x00102F37
		public FaultException Fault { get; set; }

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00104D40 File Offset: 0x00102F40
		public FindPlacesAsyncResult(AsyncCallback asyncCallback, object asyncState, int maxResults, CallContext callContext)
		{
			base.AsyncCallback = asyncCallback;
			base.AsyncState = asyncState;
			this.maxResults = maxResults;
			this.callContext = callContext;
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x00104D80 File Offset: 0x00102F80
		public void InitializeLocationRequest()
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult>((long)this.GetHashCode(), "{0}: Initializing a Location request.", this);
			this.locationRequestCompleted = false;
			this.LocationAsyncState = new BingRequestAsyncState(FindPlacesMetadata.LocationStatusCode, FindPlacesMetadata.LocationResultsCount, FindPlacesMetadata.LocationLatency, FindPlacesMetadata.LocationErrorCode, FindPlacesMetadata.LocationErrorMessage, FindPlacesMetadata.LocationFailed, new Action(this.OnLocationCompleted));
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x00104DCC File Offset: 0x00102FCC
		public void InitializePhonebookRequest()
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult>((long)this.GetHashCode(), "{0}: Initializing a Phonebook request.", this);
			this.phonebookRequestCompleted = false;
			this.PhonebookAsyncState = new BingRequestAsyncState(FindPlacesMetadata.PhonebookStatusCode, FindPlacesMetadata.PhonebookResultsCount, FindPlacesMetadata.PhonebookLatency, FindPlacesMetadata.PhonebookErrorCode, FindPlacesMetadata.PhonebookErrorMessage, FindPlacesMetadata.PhonebookFailed, new Action(this.OnPhonebookCompleted));
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x00104E17 File Offset: 0x00103017
		public void StartTimeoutDetection()
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult>((long)this.GetHashCode(), "{0}: Starting timeout detection.", this);
			this.timeoutDetector = new GuardedTimer(new TimerCallback(this.TimeoutTriggered), null, FindPlacesAsyncResult.RequestTimeout, TimeSpan.Zero);
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x00104E52 File Offset: 0x00103052
		public void EndTimeoutDetection()
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult>((long)this.GetHashCode(), "{0}: Ending timeout detection.", this);
			if (this.timeoutDetector != null)
			{
				this.timeoutDetector.Dispose(true);
				this.timeoutDetector = null;
			}
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00104E86 File Offset: 0x00103086
		public void CompleteSuccessfully(Persona[] results)
		{
			base.Data = results;
			base.Complete(null);
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00104E96 File Offset: 0x00103096
		public void CompleteWithFault(FaultException fault)
		{
			this.Fault = fault;
			base.Complete(fault);
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x00104EA8 File Offset: 0x001030A8
		private void OnPhonebookCompleted()
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult>((long)this.GetHashCode(), "{0}: Phonebook request completed.", this);
			lock (this.locker)
			{
				this.phonebookRequestCompleted = true;
				this.VerifyIfRequestIsComplete();
			}
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x00104F08 File Offset: 0x00103108
		private void OnLocationCompleted()
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult>((long)this.GetHashCode(), "{0}: Location request completed.", this);
			lock (this.locker)
			{
				this.locationRequestCompleted = true;
				this.VerifyIfRequestIsComplete();
			}
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x00104F68 File Offset: 0x00103168
		private void TimeoutTriggered(object stateNotUsed)
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult>((long)this.GetHashCode(), "{0}: Timeout reached.", this);
			lock (this.locker)
			{
				this.requestAborted = true;
				if (this.locationRequestCompleted && this.phonebookRequestCompleted)
				{
					ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult>((long)this.GetHashCode(), "{0}: Timeout reached after requests have already completed.", this);
				}
				else
				{
					LocationServicesRequestTimedOutException ex = new LocationServicesRequestTimedOutException();
					if (!this.locationRequestCompleted)
					{
						this.callContext.ProtocolLog.Set(FindPlacesMetadata.LocationFailed, "True");
						this.callContext.ProtocolLog.Set(FindPlacesMetadata.LocationErrorCode, "601");
						this.callContext.ProtocolLog.Set(FindPlacesMetadata.LocationErrorMessage, ex.Message);
					}
					if (!this.phonebookRequestCompleted)
					{
						this.callContext.ProtocolLog.Set(FindPlacesMetadata.PhonebookFailed, "True");
						this.callContext.ProtocolLog.Set(FindPlacesMetadata.PhonebookErrorCode, "601");
						this.callContext.ProtocolLog.Set(FindPlacesMetadata.PhonebookErrorMessage, ex.Message);
					}
					ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult>((long)this.GetHashCode(), "{0}: Aborting http requests.", this);
					if (this.LocationAsyncState != null)
					{
						this.LocationAsyncState.Abort();
					}
					if (this.PhonebookAsyncState != null)
					{
						this.PhonebookAsyncState.Abort();
					}
					this.CompleteWithFault(FaultExceptionUtilities.CreateFault(ex, FaultParty.Receiver, ExchangeVersion.Current));
				}
			}
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x00105110 File Offset: 0x00103310
		private void VerifyIfRequestIsComplete()
		{
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug((long)this.GetHashCode(), "{0}: Verifying if the request has completed. Aborted: {1}, Location: {2}, Phonebook: {3}", new object[]
			{
				this,
				this.requestAborted,
				this.locationRequestCompleted,
				this.phonebookRequestCompleted
			});
			if (!this.requestAborted && this.locationRequestCompleted && this.phonebookRequestCompleted)
			{
				this.timeoutDetector.Pause();
				FaultException requestFault = this.GetRequestFault();
				if (requestFault != null)
				{
					ExTraceGlobals.FindPlacesCallTracer.TraceError<FindPlacesAsyncResult, FaultException>((long)this.GetHashCode(), "{0}: Request failed with {1}", this, requestFault);
					this.CompleteWithFault(requestFault);
					return;
				}
				List<Persona> list = new List<Persona>();
				if (this.PhonebookAsyncState != null)
				{
					ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult, int>((long)this.GetHashCode(), "{0}: Total of {1} results returned by the Phonebook services.", this, this.PhonebookAsyncState.ResultsList.Count);
					list.AddRange(this.PhonebookAsyncState.ResultsList);
				}
				if (this.LocationAsyncState != null)
				{
					ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlacesAsyncResult, int>((long)this.GetHashCode(), "{0}: Total of {1} results returned by the Location services.", this, this.LocationAsyncState.ResultsList.Count);
					list.AddRange(this.LocationAsyncState.ResultsList);
				}
				this.CompleteSuccessfully(list.GetRange(0, Math.Min(this.maxResults, list.Count)).ToArray());
			}
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x00105270 File Offset: 0x00103470
		private FaultException GetRequestFault()
		{
			int num = 0;
			List<Exception> list = new List<Exception>();
			if (this.PhonebookAsyncState != null)
			{
				num++;
				if (this.PhonebookAsyncState.Exception != null)
				{
					list.Add(this.PhonebookAsyncState.Exception);
				}
			}
			if (this.LocationAsyncState != null)
			{
				num++;
				if (this.LocationAsyncState.Exception != null)
				{
					list.Add(this.LocationAsyncState.Exception);
				}
			}
			if (list.Count == num)
			{
				Exception innerException = new Exception(string.Join("; ", from v in list
				select v.ToString()));
				return FaultExceptionUtilities.CreateFault(new LocationServicesRequestFailedException(innerException), FaultParty.Receiver, ExchangeVersion.Current);
			}
			return null;
		}

		// Token: 0x04002A99 RID: 10905
		private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(2.0);

		// Token: 0x04002A9A RID: 10906
		private readonly int maxResults;

		// Token: 0x04002A9B RID: 10907
		private GuardedTimer timeoutDetector;

		// Token: 0x04002A9C RID: 10908
		private object locker = new object();

		// Token: 0x04002A9D RID: 10909
		private bool locationRequestCompleted = true;

		// Token: 0x04002A9E RID: 10910
		private bool phonebookRequestCompleted = true;

		// Token: 0x04002A9F RID: 10911
		private bool requestAborted;

		// Token: 0x04002AA0 RID: 10912
		private CallContext callContext;
	}
}
