using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000F2 RID: 242
	internal class EvaluateUserPAA : DisposableBase, IPAAEvaluator, IDisposeTrackable, IDisposable
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x0001EEAC File Offset: 0x0001D0AC
		private EvaluateUserPAA(UMSubscriber callee, PhoneNumber callerid, string diversion, bool asynchronous)
		{
			if (callee == null)
			{
				throw new ArgumentNullException("callee");
			}
			if (diversion == null)
			{
				throw new ArgumentNullException("diversion");
			}
			PIIMessage[] data = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._Callee, callee.DisplayName),
				PIIMessage.Create(PIIType._Caller, callerid),
				PIIMessage.Create(PIIType._PII, diversion)
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "EvaluateUserPAA(callee = _Callee callerid=\"_Caller\" diversion=\"_PII\"", new object[0]);
			this.callerId = callerid;
			this.diversion = diversion;
			this.asynchronous = asynchronous;
			this.callee = callee;
			this.callee.AddReference();
			if (this.asynchronous)
			{
				this.reader = new NonBlockingReader(new NonBlockingReader.Operation(this.EvaluateCallback), callee, PAAConstants.PAAEvaluationTimeout, null);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001EF6C File Offset: 0x0001D16C
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0001EF74 File Offset: 0x0001D174
		public IDataLoader UserDataLoader
		{
			get
			{
				return this.dataLoader;
			}
			set
			{
				this.dataLoader = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001EF7D File Offset: 0x0001D17D
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0001EF85 File Offset: 0x0001D185
		public IPAAStore PAAStorage
		{
			get
			{
				return this.paaStore;
			}
			set
			{
				this.paaStore = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0001EF8E File Offset: 0x0001D18E
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x0001EF96 File Offset: 0x0001D196
		public string CallId
		{
			get
			{
				return this.callId;
			}
			set
			{
				this.callId = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0001EF9F File Offset: 0x0001D19F
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0001EFA7 File Offset: 0x0001D1A7
		public Breadcrumbs Crumbs
		{
			get
			{
				return this.crumbs;
			}
			set
			{
				this.crumbs = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		public bool EvaluationTimedOut
		{
			get
			{
				return this.evaluationTimedOut;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001EFB8 File Offset: 0x0001D1B8
		public bool SubscriberHasPAAConfigured
		{
			get
			{
				return this.haveAtleastOnePAA;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001EFC0 File Offset: 0x0001D1C0
		public bool GetEffectivePAA(out PersonalAutoAttendant personalAutoAttendant)
		{
			base.CheckDisposed();
			personalAutoAttendant = null;
			this.haveAtleastOnePAA = false;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA::GetEffectivePAA()", new object[0]);
			if (this.asynchronous)
			{
				this.reader.StartAsyncOperation();
				if (!this.reader.WaitForCompletion())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA::GetEffectivePAA() Timed out", new object[0]);
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_TimedOutEvaluatingPAA, null, new object[]
					{
						this.callee.MailAddress,
						this.callId,
						this.callee.ExchangePrincipal.MailboxInfo.Location.ServerFqdn,
						this.callee.ExchangePrincipal.MailboxInfo.MailboxDatabase.ToString()
					});
					this.evaluationTimedOut = true;
					personalAutoAttendant = null;
					return false;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA::GetEffectivePAA() Evaluation finished in allotted time", new object[0]);
			}
			else
			{
				this.EvaluateCallbackWorker(this.callee);
			}
			if (this.paa != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA::GetEffectivePAA() found PAA: GUID={0} Description=\"{1}\"", new object[]
				{
					this.paa.Identity,
					this.paa.Name
				});
				personalAutoAttendant = this.paa;
				return true;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA::GetEffectivePAA() did not find any PAA", new object[0]);
			return false;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001F12D File Offset: 0x0001D32D
		internal static IPAAEvaluator Create(UMSubscriber callee, PhoneNumber callerid, string diversion)
		{
			return new EvaluateUserPAA(callee, callerid, diversion, true);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001F138 File Offset: 0x0001D338
		internal static IPAAEvaluator CreateSynchronous(UMSubscriber callee, PhoneNumber callerid, string diversion)
		{
			return new EvaluateUserPAA(callee, callerid, diversion, false);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001F143 File Offset: 0x0001D343
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EvaluateUserPAA>(this);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001F14B File Offset: 0x0001D34B
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.reader != null)
				{
					this.reader.Dispose();
					this.reader = null;
				}
				if (this.callee != null)
				{
					this.callee.ReleaseReference();
				}
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001F180 File Offset: 0x0001D380
		private void EvaluateCallback(object state)
		{
			using (new CallId(this.callId))
			{
				this.EvaluateCallbackWorker(state);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001F1BC File Offset: 0x0001D3BC
		private void EvaluateCallbackWorker(object state)
		{
			UMSubscriber umsubscriber = (UMSubscriber)state;
			PIIMessage data = PIIMessage.Create(PIIType._Callee, umsubscriber.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "EvaluateUserPAA:EvaluateCallback() subscriber = _Callee", new object[0]);
			bool flag = false;
			bool flag2 = false;
			try
			{
				if (this.paaStore == null)
				{
					this.paaStore = PAAStore.Create(umsubscriber);
					flag = true;
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() using Custom Plugin Storage of type: {0}", new object[]
					{
						this.paaStore.GetType()
					});
				}
				PAAStoreStatus paastoreStatus;
				IList<PersonalAutoAttendant> autoAttendants = this.paaStore.GetAutoAttendants(PAAValidationMode.Actions, out paastoreStatus);
				if (autoAttendants.Count == 0)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() no autoattendants in store. Returning.", new object[0]);
					return;
				}
				this.haveAtleastOnePAA = true;
				if (this.dataLoader == null)
				{
					this.dataLoader = new UserDataLoader(umsubscriber, this.callerId, this.diversion);
					flag2 = true;
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() using Custom User dataloader of type: {0}", new object[]
					{
						this.dataLoader.GetType()
					});
				}
				try
				{
					for (int i = 0; i < autoAttendants.Count; i++)
					{
						PersonalAutoAttendant personalAutoAttendant = autoAttendants[i];
						CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() Evaluating PAA {0} Enabled={1} Version={2} Compatible={3} Valid={4}", new object[]
						{
							personalAutoAttendant.Identity,
							personalAutoAttendant.Enabled,
							personalAutoAttendant.Version,
							personalAutoAttendant.IsCompatible,
							personalAutoAttendant.Valid
						});
						if (!personalAutoAttendant.IsCompatible)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() PAA {0} Version = {1} Is not compatible. Aborting PAA evaluation", new object[]
							{
								personalAutoAttendant.Identity,
								personalAutoAttendant.Version
							});
							break;
						}
						if (!personalAutoAttendant.Enabled)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() PAA {0} is disabled. No rule matching will be performed", new object[]
							{
								personalAutoAttendant.Identity
							});
						}
						else
						{
							PAARulesEvaluator paarulesEvaluator = PAARulesEvaluator.Create(personalAutoAttendant);
							LatencyDetectionContext latencyDetectionContext = PAAUtils.PAAEvaluationFactory.CreateContext(CommonConstants.ApplicationVersion, Microsoft.Exchange.UM.UMCommon.CallId.Id ?? string.Empty, new IPerformanceDataProvider[]
							{
								RpcDataProvider.Instance,
								PerformanceContext.Current
							});
							bool flag3 = paarulesEvaluator.Evaluate(this.dataLoader);
							TaskPerformanceData[] array = latencyDetectionContext.StopAndFinalizeCollection();
							TaskPerformanceData taskPerformanceData = array[0];
							PerformanceData end = taskPerformanceData.End;
							if (end != PerformanceData.Zero)
							{
								PerformanceData difference = taskPerformanceData.Difference;
								CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() Evaluating PAA {0} RPCRequests = {1}, RPCLatency = {2}", new object[]
								{
									personalAutoAttendant.Identity,
									difference.Count,
									difference.Milliseconds
								});
							}
							TaskPerformanceData taskPerformanceData2 = array[1];
							PerformanceData end2 = taskPerformanceData2.End;
							if (end2 != PerformanceData.Zero)
							{
								PerformanceData difference2 = taskPerformanceData2.Difference;
								CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() Evaluating PAA {0} ADRequests = {1}, ADLatency = {2}", new object[]
								{
									personalAutoAttendant.Identity,
									difference2.Count,
									difference2.Milliseconds
								});
							}
							if (flag3)
							{
								this.paa = personalAutoAttendant;
								break;
							}
							CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() PAA {0} Failed Evaluation", new object[]
							{
								personalAutoAttendant.Identity
							});
						}
					}
				}
				finally
				{
					if (flag2 && this.dataLoader != null)
					{
						this.dataLoader.Dispose();
					}
				}
			}
			catch (ObjectDisposedException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() Exception on Querying/Evaluating PAA", new object[0]);
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "Exception : {0}", new object[]
				{
					ex
				});
				throw;
			}
			catch (LocalizedException ex2)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() Exception on Querying/Evaluating PAA", new object[0]);
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "Exception : {0}", new object[]
				{
					ex2
				});
				throw;
			}
			catch (XmlException ex3)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() Exception on Querying/Evaluating PAA", new object[0]);
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "Exception : {0}", new object[]
				{
					ex3
				});
				throw;
			}
			finally
			{
				if (flag && this.paaStore != null)
				{
					this.paaStore.Dispose();
				}
			}
			if (this.paa != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() found PAA: GUID={0} Description=\"{1}\"", new object[]
				{
					this.paa.Identity,
					this.paa.Name
				});
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "EvaluateUserPAA:EvaluateCallback() No PAA Found", new object[0]);
		}

		// Token: 0x04000470 RID: 1136
		private readonly bool asynchronous;

		// Token: 0x04000471 RID: 1137
		private NonBlockingReader reader;

		// Token: 0x04000472 RID: 1138
		private PersonalAutoAttendant paa;

		// Token: 0x04000473 RID: 1139
		private PhoneNumber callerId;

		// Token: 0x04000474 RID: 1140
		private string diversion;

		// Token: 0x04000475 RID: 1141
		private IDataLoader dataLoader;

		// Token: 0x04000476 RID: 1142
		private IPAAStore paaStore;

		// Token: 0x04000477 RID: 1143
		private bool evaluationTimedOut;

		// Token: 0x04000478 RID: 1144
		private bool haveAtleastOnePAA;

		// Token: 0x04000479 RID: 1145
		private string callId;

		// Token: 0x0400047A RID: 1146
		private Breadcrumbs crumbs;

		// Token: 0x0400047B RID: 1147
		private UMSubscriber callee;
	}
}
