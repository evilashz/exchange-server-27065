using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000061 RID: 97
	internal abstract class StartStopComponent : StatefulComponent, IStartStop, IDisposable, INotifyFailed
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00004A0F File Offset: 0x00002C0F
		internal StartStopComponent() : base(2U)
		{
			base.DiagnosticsSession.ComponentName = "StartStopComponent";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.CoreComponentTracer;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001F8 RID: 504 RVA: 0x00004A38 File Offset: 0x00002C38
		// (remove) Token: 0x060001F9 RID: 505 RVA: 0x00004A70 File Offset: 0x00002C70
		public event EventHandler<FailedEventArgs> Failed;

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00004AA5 File Offset: 0x00002CA5
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00004AAD File Offset: 0x00002CAD
		internal ComponentFailedException LastFailedReason { get; private set; }

		// Token: 0x060001FC RID: 508 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public IAsyncResult BeginPrepareToStart(AsyncCallback callback, object context)
		{
			base.CheckDisposed();
			AsyncResult asyncResult = new AsyncResult(callback, context);
			this.BeginDispatchPrepareToStartSignal(asyncResult, this.EndDispatchSignalCallback(asyncResult), null);
			return asyncResult;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public void EndPrepareToStart(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			((AsyncResult)asyncResult).End();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public IAsyncResult BeginStart(AsyncCallback callback, object context)
		{
			base.CheckDisposed();
			AsyncResult asyncResult = new AsyncResult(callback, context);
			this.BeginDispatchStartSignal(asyncResult, this.EndDispatchSignalCallback(asyncResult), null);
			return asyncResult;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00004B24 File Offset: 0x00002D24
		public void EndStart(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			((AsyncResult)asyncResult).End();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00004B38 File Offset: 0x00002D38
		public IAsyncResult BeginStop(AsyncCallback callback, object context)
		{
			base.CheckDisposed();
			AsyncResult asyncResult = new AsyncResult(callback, context);
			this.BeginDispatchStopSignal(asyncResult, this.EndDispatchSignalCallback(asyncResult), null);
			return asyncResult;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00004B64 File Offset: 0x00002D64
		public void EndStop(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			((AsyncResult)asyncResult).End();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00004B77 File Offset: 0x00002D77
		protected override bool AtNoTransitionDefined(uint signal)
		{
			if (base.CurrentState == 9U || base.CurrentState == 8U)
			{
				this.LastFailedReason.RethrowNewInstance();
			}
			return base.AtNoTransitionDefined(signal);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00004B9E File Offset: 0x00002D9E
		protected virtual void AtPrepareToStart(AsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			this.BeginDispatchDonePreparingToStartSignal(asyncResult, this.EndDispatchSignalCallback(asyncResult), null);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00004BBB File Offset: 0x00002DBB
		protected virtual void AtDonePreparingToStart(AsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			asyncResult.SetAsCompleted();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00004BCE File Offset: 0x00002DCE
		protected virtual void AtStart(AsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			this.BeginDispatchDoneStartingSignal(asyncResult, this.EndDispatchSignalCallback(asyncResult), null);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00004BEB File Offset: 0x00002DEB
		protected virtual void AtDoneStarting(AsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			asyncResult.SetAsCompleted();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00004BFE File Offset: 0x00002DFE
		protected virtual void AtStop(AsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			this.BeginDispatchDoneStoppingSignal(asyncResult, this.EndDispatchSignalCallback(asyncResult), null);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00004C1B File Offset: 0x00002E1B
		protected virtual void AtDoneStopping(AsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			asyncResult.SetAsCompleted();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00004C2E File Offset: 0x00002E2E
		protected virtual void AtFail(ComponentFailedException reason)
		{
			Util.ThrowOnNullArgument(reason, "reason");
			this.BeginDispatchDoneFailingSignal(reason, new AsyncCallback(this.EndDispatchDoneFailingSignal), null);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00004C50 File Offset: 0x00002E50
		protected virtual void AtDoneFailing(ComponentFailedException reason)
		{
			Util.ThrowOnNullArgument(reason, "reason");
			this.OnFailed(new FailedEventArgs(reason));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00004C69 File Offset: 0x00002E69
		protected virtual void AtStopInFailed(AsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			this.BeginDispatchDoneStoppingSignal(asyncResult, this.EndDispatchSignalCallback(asyncResult), null);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00004CA4 File Offset: 0x00002EA4
		protected bool TryRunUnderExceptionHandler<TReturnValue>(Func<TReturnValue> action, out TReturnValue returnValue, LocalizedString message)
		{
			TReturnValue tempReturnValue = default(TReturnValue);
			bool result = this.TryRunUnderExceptionHandler(delegate()
			{
				tempReturnValue = action();
			}, message);
			returnValue = tempReturnValue;
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00004CEC File Offset: 0x00002EEC
		protected bool TryRunUnderExceptionHandler(Action action, LocalizedString message)
		{
			ComponentFailedException ex;
			return this.TryRunUnderExceptionHandler(action, message, out ex);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00004D40 File Offset: 0x00002F40
		protected bool TryRunUnderExceptionHandler(Action action, LocalizedString message, out ComponentFailedException result)
		{
			result = null;
			try
			{
				action();
				return true;
			}
			catch (ComponentFailedPermanentException innerException)
			{
				result = new ComponentFailedPermanentException(message, innerException);
			}
			catch (ComponentFailedTransientException innerException2)
			{
				result = new ComponentFailedTransientException(message, innerException2);
			}
			catch (OperationFailedException innerException3)
			{
				result = new ComponentFailedTransientException(message, innerException3);
			}
			base.DiagnosticsSession.TraceError<ComponentFailedException>("Component failing: {0}", result);
			this.BeginDispatchFailSignal(result, delegate(IAsyncResult failResult)
			{
				try
				{
					this.EndDispatchFailSignal(failResult);
				}
				catch (ComponentFailedException arg)
				{
					base.DiagnosticsSession.TraceError<ComponentFailedException>("Got error from signal processing: {0}", arg);
				}
			}, null);
			return false;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00004DD4 File Offset: 0x00002FD4
		protected override XElement InternalGetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = base.InternalGetDiagnosticInfo(parameters);
			if (base.CurrentState == 8U)
			{
				xelement.Add(new XElement("Reason", this.LastFailedReason));
			}
			return xelement;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00004E0E File Offset: 0x0000300E
		private void AtFailInternal(ComponentFailedException reason)
		{
			Util.ThrowOnNullArgument(reason, "reason");
			base.DiagnosticsSession.TraceError<StartStopComponent, ComponentFailedException>("Component failed: {0}. Exception: {1}", this, reason);
			this.LastFailedReason = reason;
			this.AtFail(reason);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00004E3C File Offset: 0x0000303C
		private void OnFailed(FailedEventArgs e)
		{
			EventHandler<FailedEventArgs> failed = this.Failed;
			if (failed != null)
			{
				failed(this, e);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00004EAC File Offset: 0x000030AC
		private AsyncCallback EndDispatchSignalCallback(AsyncResult result)
		{
			return delegate(IAsyncResult ar)
			{
				try
				{
					if (!this.EndDispatchSignal(ar))
					{
						result.SetAsCompleted();
					}
				}
				catch (ComponentException asCompleted)
				{
					result.SetAsCompleted(asCompleted);
				}
			};
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00004EDC File Offset: 0x000030DC
		private static void RegisterComponent(ComponentInfo componentInfo)
		{
			componentInfo.RegisterSignal(StartStopComponent.Signal.PrepareToStart, SignalPriority.Medium);
			componentInfo.RegisterSignal(StartStopComponent.Signal.DonePreparingToStart, SignalPriority.Medium);
			componentInfo.RegisterSignal(StartStopComponent.Signal.Start, SignalPriority.Medium);
			componentInfo.RegisterSignal(StartStopComponent.Signal.DoneStarting, SignalPriority.Medium);
			componentInfo.RegisterSignal(StartStopComponent.Signal.Stop, SignalPriority.Medium);
			componentInfo.RegisterSignal(StartStopComponent.Signal.DoneStopping, SignalPriority.Medium);
			componentInfo.RegisterSignal(StartStopComponent.Signal.Fail, SignalPriority.High);
			componentInfo.RegisterSignal(StartStopComponent.Signal.DoneFailing, SignalPriority.Medium);
			componentInfo.RegisterState(StartStopComponent.State.Stopped);
			componentInfo.RegisterState(StartStopComponent.State.Stopping);
			componentInfo.RegisterState(StartStopComponent.State.PreparedToStart);
			componentInfo.RegisterState(StartStopComponent.State.PreparingToStart);
			componentInfo.RegisterState(StartStopComponent.State.Started);
			componentInfo.RegisterState(StartStopComponent.State.Starting);
			componentInfo.RegisterState(StartStopComponent.State.Failed);
			componentInfo.RegisterState(StartStopComponent.State.Failing);
			componentInfo.RegisterTransition(2U, 5U, 2U, null, null);
			componentInfo.RegisterTransition(2U, 3U, 7U, null, new ActionMethod(StartStopComponent.Transition_AtStart));
			componentInfo.RegisterTransition(2U, 1U, 5U, null, new ActionMethod(StartStopComponent.Transition_AtPrepareToStart));
			componentInfo.RegisterTransition(7U, 4U, 6U, null, new ActionMethod(StartStopComponent.Transition_AtDoneStarting));
			componentInfo.RegisterTransition(7U, 5U, 3U, null, new ActionMethod(StartStopComponent.Transition_AtStop));
			componentInfo.RegisterTransition(6U, 3U, 6U, null, null);
			componentInfo.RegisterTransition(6U, 5U, 3U, null, new ActionMethod(StartStopComponent.Transition_AtStop));
			componentInfo.RegisterTransition(5U, 2U, 4U, null, new ActionMethod(StartStopComponent.Transition_AtDonePreparingToStart));
			componentInfo.RegisterTransition(5U, 5U, 3U, null, new ActionMethod(StartStopComponent.Transition_AtStop));
			componentInfo.RegisterTransition(4U, 1U, 4U, null, null);
			componentInfo.RegisterTransition(4U, 3U, 7U, null, new ActionMethod(StartStopComponent.Transition_AtStart));
			componentInfo.RegisterTransition(4U, 5U, 3U, null, new ActionMethod(StartStopComponent.Transition_AtStop));
			componentInfo.RegisterTransition(3U, 6U, 2U, null, new ActionMethod(StartStopComponent.Transition_AtDoneStopping));
			componentInfo.RegisterTransition(uint.MaxValue, 7U, 9U, null, new ActionMethod(StartStopComponent.Transition_AtFailInternal));
			componentInfo.RegisterTransition(2U, 7U, 2U, null, null);
			componentInfo.RegisterTransition(8U, 7U, 8U, null, null);
			componentInfo.RegisterTransition(9U, 7U, 9U, null, null);
			componentInfo.RegisterTransition(3U, 7U, 3U, null, null);
			componentInfo.RegisterTransition(9U, 8U, 8U, null, new ActionMethod(StartStopComponent.Transition_AtDoneFailing));
			componentInfo.RegisterTransition(8U, 5U, 3U, null, new ActionMethod(StartStopComponent.Transition_AtStopInFailed));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00005121 File Offset: 0x00003321
		internal static void Transition_AtStart(object component, params object[] args)
		{
			((StartStopComponent)component).AtStart((AsyncResult)args[0]);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00005136 File Offset: 0x00003336
		internal static void Transition_AtPrepareToStart(object component, params object[] args)
		{
			((StartStopComponent)component).AtPrepareToStart((AsyncResult)args[0]);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000514B File Offset: 0x0000334B
		internal static void Transition_AtDoneStarting(object component, params object[] args)
		{
			((StartStopComponent)component).AtDoneStarting((AsyncResult)args[0]);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00005160 File Offset: 0x00003360
		internal static void Transition_AtStop(object component, params object[] args)
		{
			((StartStopComponent)component).AtStop((AsyncResult)args[0]);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00005175 File Offset: 0x00003375
		internal static void Transition_AtDonePreparingToStart(object component, params object[] args)
		{
			((StartStopComponent)component).AtDonePreparingToStart((AsyncResult)args[0]);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000518A File Offset: 0x0000338A
		internal static void Transition_AtDoneStopping(object component, params object[] args)
		{
			((StartStopComponent)component).AtDoneStopping((AsyncResult)args[0]);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000519F File Offset: 0x0000339F
		internal static void Transition_AtFailInternal(object component, params object[] args)
		{
			((StartStopComponent)component).AtFailInternal((ComponentFailedException)args[0]);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000051B4 File Offset: 0x000033B4
		internal static void Transition_AtDoneFailing(object component, params object[] args)
		{
			((StartStopComponent)component).AtDoneFailing((ComponentFailedException)args[0]);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000051C9 File Offset: 0x000033C9
		internal static void Transition_AtStopInFailed(object component, params object[] args)
		{
			((StartStopComponent)component).AtStopInFailed((AsyncResult)args[0]);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000051E0 File Offset: 0x000033E0
		internal IAsyncResult BeginDispatchPrepareToStartSignal(AsyncResult asyncResult, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 1U, callback, context, TimeSpan.Zero, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00005208 File Offset: 0x00003408
		internal IAsyncResult BeginDispatchPrepareToStartSignal(AsyncResult asyncResult, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 1U, callback, context, delayInTimespan, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000523A File Offset: 0x0000343A
		internal void EndDispatchPrepareToStartSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00005244 File Offset: 0x00003444
		internal IAsyncResult BeginDispatchDonePreparingToStartSignal(AsyncResult asyncResult, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 2U, callback, context, TimeSpan.Zero, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000526C File Offset: 0x0000346C
		internal IAsyncResult BeginDispatchDonePreparingToStartSignal(AsyncResult asyncResult, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 2U, callback, context, delayInTimespan, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000529E File Offset: 0x0000349E
		internal void EndDispatchDonePreparingToStartSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000052A8 File Offset: 0x000034A8
		internal IAsyncResult BeginDispatchStartSignal(AsyncResult asyncResult, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 3U, callback, context, TimeSpan.Zero, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000052D0 File Offset: 0x000034D0
		internal IAsyncResult BeginDispatchStartSignal(AsyncResult asyncResult, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 3U, callback, context, delayInTimespan, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00005302 File Offset: 0x00003502
		internal void EndDispatchStartSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000530C File Offset: 0x0000350C
		internal IAsyncResult BeginDispatchDoneStartingSignal(AsyncResult asyncResult, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 4U, callback, context, TimeSpan.Zero, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00005334 File Offset: 0x00003534
		internal IAsyncResult BeginDispatchDoneStartingSignal(AsyncResult asyncResult, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 4U, callback, context, delayInTimespan, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00005366 File Offset: 0x00003566
		internal void EndDispatchDoneStartingSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00005370 File Offset: 0x00003570
		internal IAsyncResult BeginDispatchStopSignal(AsyncResult asyncResult, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 5U, callback, context, TimeSpan.Zero, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00005398 File Offset: 0x00003598
		internal IAsyncResult BeginDispatchStopSignal(AsyncResult asyncResult, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 5U, callback, context, delayInTimespan, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000053CA File Offset: 0x000035CA
		internal void EndDispatchStopSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000053D4 File Offset: 0x000035D4
		internal IAsyncResult BeginDispatchDoneStoppingSignal(AsyncResult asyncResult, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 6U, callback, context, TimeSpan.Zero, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000053FC File Offset: 0x000035FC
		internal IAsyncResult BeginDispatchDoneStoppingSignal(AsyncResult asyncResult, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 6U, callback, context, delayInTimespan, new object[]
			{
				asyncResult
			});
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000542E File Offset: 0x0000362E
		internal void EndDispatchDoneStoppingSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00005438 File Offset: 0x00003638
		internal IAsyncResult BeginDispatchFailSignal(ComponentFailedException reason, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 7U, callback, context, TimeSpan.Zero, new object[]
			{
				reason
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00005460 File Offset: 0x00003660
		internal IAsyncResult BeginDispatchFailSignal(ComponentFailedException reason, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 7U, callback, context, delayInTimespan, new object[]
			{
				reason
			});
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00005492 File Offset: 0x00003692
		internal void EndDispatchFailSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000549C File Offset: 0x0000369C
		internal IAsyncResult BeginDispatchDoneFailingSignal(ComponentFailedException reason, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 8U, callback, context, TimeSpan.Zero, new object[]
			{
				reason
			});
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000054C4 File Offset: 0x000036C4
		internal IAsyncResult BeginDispatchDoneFailingSignal(ComponentFailedException reason, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 8U, callback, context, delayInTimespan, new object[]
			{
				reason
			});
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000054F6 File Offset: 0x000036F6
		internal void EndDispatchDoneFailingSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x02000062 RID: 98
		internal new enum Signal : uint
		{
			// Token: 0x040000CA RID: 202
			PrepareToStart = 1U,
			// Token: 0x040000CB RID: 203
			DonePreparingToStart,
			// Token: 0x040000CC RID: 204
			Start,
			// Token: 0x040000CD RID: 205
			DoneStarting,
			// Token: 0x040000CE RID: 206
			Stop,
			// Token: 0x040000CF RID: 207
			DoneStopping,
			// Token: 0x040000D0 RID: 208
			Fail,
			// Token: 0x040000D1 RID: 209
			DoneFailing,
			// Token: 0x040000D2 RID: 210
			Max
		}

		// Token: 0x02000063 RID: 99
		internal new enum State : uint
		{
			// Token: 0x040000D4 RID: 212
			Stopped = 2U,
			// Token: 0x040000D5 RID: 213
			Stopping,
			// Token: 0x040000D6 RID: 214
			PreparedToStart,
			// Token: 0x040000D7 RID: 215
			PreparingToStart,
			// Token: 0x040000D8 RID: 216
			Started,
			// Token: 0x040000D9 RID: 217
			Starting,
			// Token: 0x040000DA RID: 218
			Failed,
			// Token: 0x040000DB RID: 219
			Failing,
			// Token: 0x040000DC RID: 220
			Max
		}
	}
}
