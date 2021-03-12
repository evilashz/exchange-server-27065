using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.UM.PersonalAutoAttendant;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000191 RID: 401
	internal class NonBlockingCallAnsweringData : DisposableBase
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x000330D8 File Offset: 0x000312D8
		internal NonBlockingCallAnsweringData(UMRecipient recipient, string callId, PhoneNumber callerId, string diversion, bool evaluatePAA)
		{
			this.callId = callId;
			this.callerId = callerId;
			this.diversion = diversion;
			this.shouldEvaluatePAA = evaluatePAA;
			this.paaEvaluationTimer = new Stopwatch();
			this.elapsedTime = new Stopwatch();
			this.reader = new NonBlockingReader(new NonBlockingReader.Operation(this.PopulateUserData), this, GlobCfg.CallAnswerMailboxDataDownloadTimeout, new NonBlockingReader.TimeoutCallback(this.TimedOutPopulatingUserData));
			this.subscriber = (recipient as UMSubscriber);
			PIIMessage data = PIIMessage.Create(PIIType._User, recipient.DisplayName);
			if (this.subscriber != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, data, "NonblockingUmSubscriberData(#{0})::ctor() user = _user WaitTimeout = {1} Threshold = {2}", new object[]
				{
					this.GetHashCode(),
					GlobCfg.CallAnswerMailboxDataDownloadTimeout,
					GlobCfg.CallAnswerMailboxDataDownloadTimeoutThreshold
				});
				this.subscriber.AddReference();
				this.reader.StartAsyncOperation();
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, data, "NonblockingUmSubscriberData(#{0})::ctor() user _User is not UMEnabled on a compatible mailbox", new object[]
			{
				this.GetHashCode()
			});
			this.greetingFile = null;
			this.outOfOffice = false;
			this.quotaExceeded = false;
			this.transcriptionEnabledInMailboxConfig = TranscriptionEnabledSetting.Disabled;
			this.reader.ForceCompletion();
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0003320F File Offset: 0x0003140F
		public TranscriptionEnabledSetting TranscriptionEnabledInMailboxConfig
		{
			get
			{
				if (this.reader.WaitForCompletion())
				{
					return this.transcriptionEnabledInMailboxConfig;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "CallAnsweringData::Timeout checking whether transcription is enabled in mailbox config.", new object[0]);
				return TranscriptionEnabledSetting.Unknown;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0003323C File Offset: 0x0003143C
		internal bool TimedOut
		{
			get
			{
				return this.reader.TimeOutExpired;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00033249 File Offset: 0x00031449
		internal bool IsQuotaExceeded
		{
			get
			{
				if (this.reader.WaitForCompletion())
				{
					return this.quotaExceeded;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "CallAnsweringData::Timeout checking the quota.", new object[0]);
				return false;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00033276 File Offset: 0x00031476
		internal ITempWavFile GreetingFile
		{
			get
			{
				if (this.reader.WaitForCompletion())
				{
					return this.greetingFile;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "CallAnsweringData::Timeout getting the greeting file.", new object[0]);
				return null;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x000332A3 File Offset: 0x000314A3
		internal bool IsOOF
		{
			get
			{
				if (this.reader.WaitForCompletion())
				{
					return this.outOfOffice;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "CallAnsweringData::Timeout checking the OOF.", new object[0]);
				return false;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x000332D0 File Offset: 0x000314D0
		internal PersonalAutoAttendant PersonalAutoAttendant
		{
			get
			{
				if (this.reader.WaitForCompletion())
				{
					return this.personalAutoAttendant;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "CallAnsweringData::PersonalAutoAttendant timed out", new object[0]);
				return null;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000332FD File Offset: 0x000314FD
		internal TimeSpan PAAEvaluationTime
		{
			get
			{
				return this.paaEvaluationTimer.Elapsed;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0003330A File Offset: 0x0003150A
		internal bool SubscriberHasPAAConfigured
		{
			get
			{
				return this.subscriberHasAtleastOnePAAConfigured;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00033312 File Offset: 0x00031512
		internal TimeSpan ElapsedTime
		{
			get
			{
				return this.elapsedTime.Elapsed;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0003331F File Offset: 0x0003151F
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00033327 File Offset: 0x00031527
		internal uint AdCount { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00033330 File Offset: 0x00031530
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x00033338 File Offset: 0x00031538
		internal TimeSpan AdLatency { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00033341 File Offset: 0x00031541
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x00033349 File Offset: 0x00031549
		internal uint RpcCount { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00033352 File Offset: 0x00031552
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0003335A File Offset: 0x0003155A
		internal TimeSpan RpcLatency { get; set; }

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00033363 File Offset: 0x00031563
		internal bool WaitForCompletion()
		{
			return this.reader.WaitForCompletion();
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00033370 File Offset: 0x00031570
		internal void TimedOutPopulatingUserData(object state)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_TimedOutRetrievingMailboxData, null, new object[]
			{
				this.subscriber.MailAddress,
				this.callId,
				this.subscriber.ExchangePrincipal.MailboxInfo.Location.ServerFqdn,
				this.subscriber.ExchangePrincipal.MailboxInfo.MailboxDatabase.ToString(),
				CommonUtil.ToEventLogString(new StackTrace(true))
			});
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000333F8 File Offset: 0x000315F8
		internal void PopulateUserData(object state)
		{
			LatencyDetectionContext latencyDetectionContext = null;
			this.elapsedTime.Start();
			try
			{
				using (this.subscriber.CreateConnectionGuard())
				{
					using (new CallId(this.callId))
					{
						latencyDetectionContext = PAAUtils.GetCallAnsweringDataFactory.CreateContext(CommonConstants.ApplicationVersion, CallId.Id ?? string.Empty, new IPerformanceDataProvider[]
						{
							RpcDataProvider.Instance,
							PerformanceContext.Current
						});
						CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "NonblockingUmSubscriberData({0})::PopulateUserData() IsPAAEnabled = {1} IsVirtualNumberEnabled = {2} shouldEvaluatePAA = {3}", new object[]
						{
							this.GetHashCode(),
							this.subscriber.IsPAAEnabled,
							this.subscriber.IsVirtualNumberEnabled,
							this.shouldEvaluatePAA
						});
						if ((this.subscriber.IsPAAEnabled || this.subscriber.IsVirtualNumberEnabled) && this.shouldEvaluatePAA)
						{
							this.EvaluatePAA(this.subscriber);
						}
						if (this.elapsedTime.Elapsed > GlobCfg.CallAnswerMailboxDataDownloadTimeoutThreshold)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "NonblockingUmSubscriberData({0})::PopulateUserData() Elapsed = {1} Threshold = {2}. CA data will not be fetched from mailbox", new object[]
							{
								this.GetHashCode(),
								this.elapsedTime.Elapsed,
								GlobCfg.CallAnswerMailboxDataDownloadTimeoutThreshold
							});
						}
						else
						{
							UMSubscriberCallAnsweringData umsubscriberCallAnsweringData = null;
							using (IUMUserMailboxStorage umuserMailboxAccessor = InterServerMailboxAccessor.GetUMUserMailboxAccessor(this.subscriber.ADUser, true))
							{
								umsubscriberCallAnsweringData = umuserMailboxAccessor.GetUMSubscriberCallAnsweringData(this.subscriber, GlobCfg.CallAnswerMailboxDataDownloadTimeoutThreshold.Subtract(this.elapsedTime.Elapsed));
							}
							this.outOfOffice = umsubscriberCallAnsweringData.IsOOF;
							this.greetingFile = umsubscriberCallAnsweringData.Greeting;
							this.transcriptionEnabledInMailboxConfig = umsubscriberCallAnsweringData.IsTranscriptionEnabledInMailboxConfig;
							this.quotaExceeded = umsubscriberCallAnsweringData.IsMailboxQuotaExceeded;
							if (umsubscriberCallAnsweringData.TaskTimedOut)
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "NonblockingUmSubscriberData({0})::PopulateUserData() Elapsed = {1} Threshold = {2}. Quota check is not done", new object[]
								{
									this.GetHashCode(),
									this.elapsedTime.Elapsed,
									GlobCfg.CallAnswerMailboxDataDownloadTimeoutThreshold
								});
							}
						}
					}
				}
			}
			finally
			{
				TaskPerformanceData[] array = latencyDetectionContext.StopAndFinalizeCollection();
				TaskPerformanceData taskPerformanceData = array[0];
				PerformanceData end = taskPerformanceData.End;
				if (end != PerformanceData.Zero)
				{
					PerformanceData difference = taskPerformanceData.Difference;
					this.RpcCount = difference.Count;
					this.RpcLatency = difference.Latency;
				}
				TaskPerformanceData taskPerformanceData2 = array[1];
				PerformanceData end2 = taskPerformanceData2.End;
				if (end2 != PerformanceData.Zero)
				{
					PerformanceData difference2 = taskPerformanceData2.Difference;
					this.AdCount = difference2.Count;
					this.AdLatency = difference2.Latency;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "NonblockingUmSubscriberData({0})::PopulateUserData()) RPCRequests = {1} RPCLatency = {2} ADRequests = {3}, ADLatency = {4}", new object[]
				{
					this.GetHashCode(),
					this.RpcCount,
					this.RpcLatency.TotalMilliseconds,
					this.AdCount,
					this.AdLatency.TotalMilliseconds
				});
				this.elapsedTime.Stop();
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "NonblockingUmSubscriberData({0})::PopulateUserData()) Evaluation Elapsed Time = {1}", new object[]
				{
					this.GetHashCode(),
					this.elapsedTime.Elapsed
				});
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000337F0 File Offset: 0x000319F0
		private void EvaluatePAA(UMSubscriber subscriber)
		{
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, subscriber.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, data, "NonblockingUmSubscriberData({0})::PopulateUserData() user = _UserDisplayName", new object[]
			{
				this.GetHashCode()
			});
			bool flag = false;
			this.paaEvaluationTimer.Start();
			try
			{
				Util.IncrementCounter(AvailabilityCounters.PercentageCARDownloadFailures_Base, 1L);
				using (IPAAEvaluator ipaaevaluator = EvaluateUserPAA.CreateSynchronous(subscriber, this.callerId, this.diversion))
				{
					flag = ipaaevaluator.GetEffectivePAA(out this.personalAutoAttendant);
					this.subscriberHasAtleastOnePAAConfigured = ipaaevaluator.SubscriberHasPAAConfigured;
				}
			}
			catch (CorruptedPAAStoreException)
			{
				Util.IncrementCounter(AvailabilityCounters.PercentageCARDownloadFailures, 1L);
			}
			catch (LocalizedException e)
			{
				XsoUtil.LogMailboxConnectionFailureException(e, subscriber.ExchangePrincipal);
				if (!XsoUtil.IsMailboxConnectionFailureException(e))
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FailedToRetrieveMailboxData, null, new object[]
					{
						CallId.Id,
						subscriber.MailAddress,
						CommonUtil.ToEventLogString(Utils.ConcatenateMessagesOnException(e))
					});
				}
				Util.IncrementCounter(AvailabilityCounters.PercentageCARDownloadFailures, 1L);
			}
			catch (XmlException)
			{
				Util.IncrementCounter(AvailabilityCounters.PercentageCARDownloadFailures, 1L);
			}
			this.paaEvaluationTimer.Stop();
			if (flag)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "NonblockingUmSubscriberData({0})::PopulateUserData() Got PAA ID={1} Enabled={2} Version={3} Valid={4}", new object[]
				{
					this.GetHashCode(),
					this.personalAutoAttendant.Identity.ToString(),
					this.personalAutoAttendant.Enabled,
					this.personalAutoAttendant.Version.ToString(),
					this.personalAutoAttendant.Valid
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "NonblockingUmSubscriberData({0})::PopulateUserData() PAA Evaluation Time = {1}", new object[]
				{
					this.GetHashCode(),
					this.paaEvaluationTimer.Elapsed
				});
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "NonblockingUmSubscriberData({0})::PopulateUserData() Did not get a valid PAA", new object[]
			{
				this.GetHashCode()
			});
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00033A34 File Offset: 0x00031C34
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.reader != null)
				{
					this.reader.Dispose();
					this.reader = null;
				}
				if (this.subscriber != null)
				{
					this.subscriber.ReleaseReference();
				}
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00033A66 File Offset: 0x00031C66
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NonBlockingCallAnsweringData>(this);
		}

		// Token: 0x040009E9 RID: 2537
		private UMSubscriber subscriber;

		// Token: 0x040009EA RID: 2538
		private ITempWavFile greetingFile;

		// Token: 0x040009EB RID: 2539
		private bool quotaExceeded;

		// Token: 0x040009EC RID: 2540
		private bool outOfOffice;

		// Token: 0x040009ED RID: 2541
		private TranscriptionEnabledSetting transcriptionEnabledInMailboxConfig;

		// Token: 0x040009EE RID: 2542
		private NonBlockingReader reader;

		// Token: 0x040009EF RID: 2543
		private string callId;

		// Token: 0x040009F0 RID: 2544
		private PersonalAutoAttendant personalAutoAttendant;

		// Token: 0x040009F1 RID: 2545
		private PhoneNumber callerId;

		// Token: 0x040009F2 RID: 2546
		private string diversion;

		// Token: 0x040009F3 RID: 2547
		private bool subscriberHasAtleastOnePAAConfigured;

		// Token: 0x040009F4 RID: 2548
		private bool shouldEvaluatePAA;

		// Token: 0x040009F5 RID: 2549
		private Stopwatch paaEvaluationTimer;

		// Token: 0x040009F6 RID: 2550
		private Stopwatch elapsedTime;
	}
}
