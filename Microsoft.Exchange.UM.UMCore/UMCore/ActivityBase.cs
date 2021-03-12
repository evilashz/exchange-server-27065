using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000007 RID: 7
	internal abstract class ActivityBase : DisposableBase
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002CA2 File Offset: 0x00000EA2
		internal ActivityBase(ActivityManager manager, ActivityConfig config)
		{
			this.manager = manager;
			this.config = config;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002CB8 File Offset: 0x00000EB8
		private ActivityBase()
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002CC0 File Offset: 0x00000EC0
		internal string UniqueId
		{
			get
			{
				return this.config.UniqueId;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002CCD File Offset: 0x00000ECD
		internal string ActivityId
		{
			get
			{
				return this.config.ActivityId;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002CDA File Offset: 0x00000EDA
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002CE2 File Offset: 0x00000EE2
		protected internal int FailureCount
		{
			get
			{
				return this.failureCount;
			}
			protected set
			{
				this.failureCount = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002CEB File Offset: 0x00000EEB
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002CF3 File Offset: 0x00000EF3
		protected internal ActivityManager Manager
		{
			get
			{
				return this.manager;
			}
			protected set
			{
				this.manager = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002CFC File Offset: 0x00000EFC
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002D04 File Offset: 0x00000F04
		protected ActivityConfig Config
		{
			get
			{
				return this.config;
			}
			set
			{
				this.config = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D0D File Offset: 0x00000F0D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002D15 File Offset: 0x00000F15
		protected int NumFailures
		{
			get
			{
				return this.failureCount;
			}
			set
			{
				this.failureCount = value;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D20 File Offset: 0x00000F20
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type: {0}, ID: {1}", new object[]
			{
				base.GetType().ToString(),
				this.UniqueId
			});
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D5C File Offset: 0x00000F5C
		internal virtual void Start(BaseUMCallSession vo, string refInfo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Starting {0} with refInfo {1}.", new object[]
			{
				this,
				refInfo
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FsmActivityStart, null, new object[]
			{
				this.ToString(),
				vo.CallId
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DB4 File Offset: 0x00000FB4
		internal virtual void OnSpeech(object sender, UMSpeechEventArgs args)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DBC File Offset: 0x00000FBC
		internal virtual void OnInput(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			PIIMessage data = PIIMessage.Create(PIIType._PII, Encoding.ASCII.GetString(callSessionEventArgs.DtmfDigits));
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "ActivityBase OnDTMFInput. Received _PII after {0} seconds.", new object[]
			{
				callSessionEventArgs.PlayTime
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E07 File Offset: 0x00001007
		internal virtual void OnUserHangup(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityBase OnUserHangup.", new object[0]);
			vo.DisconnectCall();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E25 File Offset: 0x00001025
		internal virtual void OnCancelled(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityBase OnCancelled.", new object[0]);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E40 File Offset: 0x00001040
		internal virtual void OnComplete(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityBase OnComplete Elapsed Seconds: {0}.", new object[]
			{
				callSessionEventArgs.PlayTime
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E73 File Offset: 0x00001073
		internal virtual void OnTimeout(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ActivityBase OnTimeout called.", new object[0]);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E8B File Offset: 0x0000108B
		internal virtual void OnStateInfoSent(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "State info send complete.", new object[0]);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002EA3 File Offset: 0x000010A3
		internal virtual void OnOutBoundCallRequestCompleted(BaseUMCallSession vo, OutboundCallDetailsEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "OnOutBoundCallRequestCompleted", new object[0]);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EBC File Offset: 0x000010BC
		internal virtual void OnError(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "OnError(), Error={0}.", new object[]
			{
				callSessionEventArgs.Error
			});
			if (callSessionEventArgs.Error != null)
			{
				string exceptionCategory = this.GetExceptionCategory(callSessionEventArgs);
				TransitionBase transition = this.GetTransition(exceptionCategory);
				if (transition == null)
				{
					return;
				}
				callSessionEventArgs.Error = null;
				transition.Execute(this.manager, vo);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F1C File Offset: 0x0000111C
		internal virtual void OnTransferComplete(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "OnTransferComplete(), Error={0}.", new object[]
			{
				callSessionEventArgs.Error
			});
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F4C File Offset: 0x0000114C
		internal virtual void OnMessageReceived(BaseUMCallSession vo, InfoMessage.MessageReceivedEventArgs e)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Received message on trunk {0}:{1}.", new object[]
			{
				vo.SessionGuid,
				e.Message
			});
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F88 File Offset: 0x00001188
		internal virtual void OnMessageSent(BaseUMCallSession vo, EventArgs e)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Message sent on trunk {0}.", new object[]
			{
				vo.SessionGuid
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002FBC File Offset: 0x000011BC
		internal virtual void OnHeavyBlockingOperation(BaseUMCallSession vo, HeavyBlockingOperationEventArgs hboea)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "OnHeavyBlockingOperation(), completion type {0}.", new object[]
			{
				hboea.CompletionType
			});
			if (hboea.CompletionType == HeavyBlockingOperationCompletionType.Timeout)
			{
				return;
			}
			HeavyBlockingOperation heavyBlockingOperation = (HeavyBlockingOperation)hboea.Operation;
			heavyBlockingOperation.CompleteHeavyBlockingOperation();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000300B File Offset: 0x0000120B
		internal virtual void OnDispose(BaseUMCallSession vo, EventArgs e)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "OnDispose.", new object[0]);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003023 File Offset: 0x00001223
		internal virtual void OnHold(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Received a Hold event from the SIP peer.", new object[0]);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000303B File Offset: 0x0000123B
		internal virtual void OnResume(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Received a Hold event from the SIP peer.", new object[0]);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003053 File Offset: 0x00001253
		internal virtual TransitionBase GetTransition(string input)
		{
			return this.config.GetTransition(input, this.manager);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003067 File Offset: 0x00001267
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003069 File Offset: 0x00001269
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ActivityBase>(this);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003071 File Offset: 0x00001271
		protected virtual string GetExceptionCategory(UMCallSessionEventArgs callSessionEventArgs)
		{
			if (callSessionEventArgs.Error is QuotaExceededException)
			{
				return "quotaExceeded";
			}
			if (callSessionEventArgs.Error != null)
			{
				return "xsoError";
			}
			return null;
		}

		// Token: 0x0400000F RID: 15
		private ActivityManager manager;

		// Token: 0x04000010 RID: 16
		private ActivityConfig config;

		// Token: 0x04000011 RID: 17
		private int failureCount;
	}
}
