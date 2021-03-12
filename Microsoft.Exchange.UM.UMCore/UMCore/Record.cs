using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001C0 RID: 448
	internal class Record : CommonActivity
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x0003911B File Offset: 0x0003731B
		internal Record(ActivityManager manager, ActivityConfig config) : base(manager, config)
		{
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0003913C File Offset: 0x0003733C
		internal override void StartActivity(BaseUMCallSession vo, string refInfo)
		{
			ActivityConfig config = base.Config;
			UMSubscriber umsubscriber = vo.CurrentCallContext.CalleeInfo as UMSubscriber;
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FsmActivityStart, null, new object[]
			{
				this.ToString(),
				vo.CallId
			});
			if (umsubscriber != null)
			{
				this.recordingIdleTimeout = TimeSpan.FromSeconds((double)umsubscriber.DialPlan.RecordingIdleTimeout);
			}
			else if (vo.CurrentCallContext.CallerInfo != null)
			{
				this.recordingIdleTimeout = TimeSpan.FromSeconds((double)vo.CurrentCallContext.CallerInfo.DialPlan.RecordingIdleTimeout);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Recording idle timeout set to {0}.", new object[]
			{
				this.recordingIdleTimeout.TotalSeconds
			});
			this.RecordStart(vo);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00039208 File Offset: 0x00037408
		internal override void OnInput(BaseUMCallSession callSession, UMCallSessionEventArgs callSessionEventArgs)
		{
			base.OnInput(callSession, callSessionEventArgs);
			ITempWavFile recording = base.Manager.RecordContext.Recording;
			this.UpdateRecordContext(callSessionEventArgs);
			string @string = Encoding.ASCII.GetString(callSessionEventArgs.DtmfDigits);
			TransitionBase transition = this.GetTransition(@string);
			if (string.Compare(@string, "faxtone", StringComparison.OrdinalIgnoreCase) == 0 && transition != null)
			{
				transition.Execute(base.Manager, callSession);
				return;
			}
			if (base.Manager.RecordContext.TotalSeconds <= 0)
			{
				this.HandleTotalSilence(callSession);
				return;
			}
			base.Manager.RecordContext.TotalFailures = 0;
			base.Manager.WriteVariable("recordingFailureCount", base.Manager.RecordContext.TotalFailures);
			if (transition == null)
			{
				transition = this.GetTransition("anyKey");
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Record Activity making dtmf transition: {0}.", new object[]
			{
				transition
			});
			transition.Execute(base.Manager, callSession);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000392F8 File Offset: 0x000374F8
		internal override void OnComplete(BaseUMCallSession callSession, UMCallSessionEventArgs callSessionEventArgs)
		{
			base.OnComplete(callSession, callSessionEventArgs);
			base.Manager.RecordContext.Append = false;
			if (callSessionEventArgs.RecordTime.TotalSeconds <= 1.0)
			{
				this.HandleTotalSilence(callSession);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "User spoke and then was silent.  Total recording time was: {0}.", new object[]
			{
				callSessionEventArgs.RecordTime
			});
			this.UpdateRecordContext(callSessionEventArgs);
			base.Manager.RecordContext.TotalFailures = 1;
			base.Manager.WriteVariable("recordingFailureCount", base.Manager.RecordContext.TotalFailures);
			TransitionBase transition = this.GetTransition("silence");
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Record Activity making silence transition: {0}.", new object[]
			{
				transition
			});
			transition.Execute(base.Manager, callSession);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000393D8 File Offset: 0x000375D8
		internal override void OnTimeout(BaseUMCallSession callSession, UMCallSessionEventArgs callSessionEventArgs)
		{
			base.OnTimeout(callSession, callSessionEventArgs);
			this.UpdateRecordContext(callSessionEventArgs);
			base.Manager.RecordContext.TotalFailures = 0;
			base.Manager.WriteVariable("recordingTimedOut", true);
			TransitionBase transition = this.GetTransition("timeout");
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Record Activity making timeout transition: {0}.", new object[]
			{
				transition
			});
			transition.Execute(base.Manager, callSession);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00039450 File Offset: 0x00037650
		internal override void OnUserHangup(BaseUMCallSession callSession, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Record Activity OnUserHangup.", new object[0]);
			this.UpdateRecordContext(callSessionEventArgs);
			base.Manager.RecordContext.TotalFailures = 0;
			TransitionBase transition = this.GetTransition("userHangup");
			if (transition != null)
			{
				transition.Execute(base.Manager, callSession);
				return;
			}
			base.OnUserHangup(callSession, callSessionEventArgs);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000394B0 File Offset: 0x000376B0
		private void RecordStart(BaseUMCallSession vo)
		{
			if (string.Compare(base.Manager.RecordContext.Id, base.UniqueId, true, CultureInfo.InvariantCulture) != 0)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Resetting recording context because current id={0} is not equal to saved id={1}.", new object[]
				{
					base.UniqueId,
					base.Manager.RecordContext.Id
				});
				base.Manager.RecordContext.Reset();
				base.Manager.RecordContext.Id = base.UniqueId;
			}
			base.Manager.WriteVariable("recordingTimedOut", false);
			base.Manager.WriteVariable("recordingFailureCount", 0);
			if (!base.Manager.RecordContext.Append)
			{
				base.Manager.RecordContext.Recording = null;
				base.Manager.RecordContext.TotalSeconds = 0;
			}
			if (base.Manager.RecordContext.Recording == null)
			{
				base.Manager.RecordContext.Recording = TempFileFactory.CreateTempWavFile(true);
			}
			base.Manager.WriteVariable("recording", base.Manager.RecordContext.Recording);
			int num = Math.Max(0, this.MaxSecondsFromType(vo) - base.Manager.RecordContext.TotalSeconds);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Recording user input.  IdleTimeout={0}, MaxTimeout={1}.", new object[]
			{
				this.recordingIdleTimeout.TotalSeconds,
				num
			});
			vo.RecordFile(base.Manager.RecordContext.Recording.FilePath, (int)this.recordingIdleTimeout.TotalSeconds, num, ((RecordConfig)base.Config).DtmfStopTones, base.Manager.RecordContext.Append, GlobCfg.DefaultPromptHelper.Build(base.Manager, vo.CurrentCallContext.Culture, GlobCfg.DefaultPrompts.ComfortNoise));
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000396A0 File Offset: 0x000378A0
		private int MaxSecondsFromType(BaseUMCallSession callSession)
		{
			RecordConfig recordConfig = (RecordConfig)base.Config;
			int num = 0;
			string type;
			if ((type = recordConfig.Type) != null)
			{
				if (!(type == "greeting"))
				{
					if (!(type == "message"))
					{
						if (type == "name")
						{
							num = 8;
						}
					}
					else
					{
						num = 600;
						if (callSession.CurrentCallContext.DialPlan != null)
						{
							num = callSession.CurrentCallContext.DialPlan.MaxRecordingDuration * 60;
						}
					}
				}
				else
				{
					num = 600;
					if (callSession.CurrentCallContext.CallerInfo != null)
					{
						num = callSession.CurrentCallContext.CallerInfo.UMMailboxPolicy.MaxGreetingDuration * 60;
					}
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "RecordActivity::MaxSecondsFromType returning sec={0} for type={1}.", new object[]
			{
				num,
				recordConfig.Type
			});
			return num;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00039778 File Offset: 0x00037978
		private void UpdateRecordContext(UMCallSessionEventArgs callSessionEventArgs)
		{
			base.Manager.RecordContext.Append = false;
			base.Manager.RecordContext.TotalSeconds = (int)callSessionEventArgs.TotalRecordTime.TotalSeconds;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000397B8 File Offset: 0x000379B8
		private void HandleTotalSilence(BaseUMCallSession callSession)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "User was completely silent for this recording.  Failure count is: {0}.", new object[]
			{
				base.Manager.RecordContext.TotalFailures + 1
			});
			base.Manager.RecordContext.TotalFailures++;
			if (base.Manager.RecordContext.TotalSeconds <= 0)
			{
				base.Manager.RecordContext.Recording = null;
				base.Manager.WriteVariable("recording", null);
				base.Manager.RecordContext.Append = false;
			}
			base.Manager.WriteVariable("recordingFailureCount", base.Manager.RecordContext.TotalFailures);
			int num = 3;
			if (callSession.CurrentCallContext.CallerInfo != null && callSession.CurrentCallContext.CallerInfo.DialPlan != null)
			{
				num = callSession.CurrentCallContext.CallerInfo.DialPlan.InputFailuresBeforeDisconnect;
			}
			if (base.Manager.RecordContext.TotalFailures < num)
			{
				TransitionBase transition = this.GetTransition("silence");
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Record Activity making silence transition: {0}.", new object[]
				{
					transition
				});
				transition.Execute(base.Manager, callSession);
				return;
			}
			TransitionBase transition2 = this.GetTransition("recordFailure");
			if (transition2 != null)
			{
				transition2.Execute(base.Manager, callSession);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Record Activity dropping call because failure count is: {0}.", new object[]
			{
				base.Manager.RecordContext.TotalFailures
			});
			base.Manager.DropCall(callSession, DropCallReason.UserError);
		}

		// Token: 0x04000A68 RID: 2664
		private TimeSpan recordingIdleTimeout = TimeSpan.FromSeconds(3.0);
	}
}
