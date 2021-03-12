using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000064 RID: 100
	internal abstract class ContainerComponent : StartStopComponent, IDiagnosable
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00005500 File Offset: 0x00003700
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = this.InternalGetDiagnosticInfo(parameters);
			lock (this.diagnosableComponents)
			{
				foreach (IDiagnosable diagnosable in this.diagnosableComponents)
				{
					xelement.Add(diagnosable.GetDiagnosticInfo(parameters));
				}
			}
			return xelement;
		}

		// Token: 0x06000237 RID: 567
		protected abstract void CreateChildren();

		// Token: 0x06000238 RID: 568
		protected abstract void DisposeChildren();

		// Token: 0x06000239 RID: 569
		protected abstract void PrepareToStartChildrenAsync();

		// Token: 0x0600023A RID: 570
		protected abstract void StartChildrenAsync();

		// Token: 0x0600023B RID: 571
		protected abstract void StopChildrenAsync();

		// Token: 0x0600023C RID: 572 RVA: 0x0000558C File Offset: 0x0000378C
		protected void AddComponent(IDiagnosable component)
		{
			lock (this.diagnosableComponents)
			{
				this.diagnosableComponents.Add(component);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000055D4 File Offset: 0x000037D4
		protected void RemoveComponent(IDiagnosable component)
		{
			lock (this.diagnosableComponents)
			{
				this.diagnosableComponents.Remove(component);
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000561C File Offset: 0x0000381C
		protected sealed override void AtPrepareToStart(AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<ContainerComponent>("{0} is preparing to start.", this);
			if (Interlocked.CompareExchange<AsyncResult>(ref this.pendingAsyncResult, asyncResult, null) != null)
			{
				throw new InvalidOperationException("There is another pending async result not completed yet.");
			}
			try
			{
				this.CreateChildren();
				this.PrepareToStartChildrenAsync();
			}
			catch (ComponentException exception)
			{
				this.CompletePrepareToStart(exception);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000567C File Offset: 0x0000387C
		protected sealed override void AtStart(AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<ContainerComponent>("{0} is starting.", this);
			if (Interlocked.CompareExchange<AsyncResult>(ref this.pendingAsyncResult, asyncResult, null) != null)
			{
				throw new InvalidOperationException("There is another pending async result not completed yet.");
			}
			try
			{
				this.StartChildrenAsync();
			}
			catch (ComponentException exception)
			{
				this.CompleteStart(exception);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000056D8 File Offset: 0x000038D8
		protected sealed override void AtStop(AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<ContainerComponent>("{0} is stopping.", this);
			if (Interlocked.CompareExchange<AsyncResult>(ref this.pendingAsyncResult, asyncResult, null) != null)
			{
				throw new InvalidOperationException("There is another pending async result not completed yet.");
			}
			try
			{
				this.StopChildrenAsync();
			}
			catch (ComponentException exception)
			{
				this.CompleteStop(exception);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00005734 File Offset: 0x00003934
		protected override void AtDoneStopping(AsyncResult asyncResult)
		{
			this.DisposeChildren();
			base.AtDoneStopping(asyncResult);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00005743 File Offset: 0x00003943
		protected sealed override void AtStopInFailed(AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<ContainerComponent>("{0} is stopping from failed state.", this);
			if (Interlocked.CompareExchange<AsyncResult>(ref this.pendingAsyncResult, asyncResult, null) != null)
			{
				throw new InvalidOperationException("There is another pending async result not completed yet.");
			}
			this.StopChildrenAsync();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00005776 File Offset: 0x00003976
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.DisposeChildren();
				base.DiagnosticsSession.Assert(this.diagnosableComponents.Count == 0, "There must be no diagnosable components after children are disposed", new object[0]);
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000057AC File Offset: 0x000039AC
		protected void CompletePrepareToStart(ComponentException exception)
		{
			base.DiagnosticsSession.TraceDebug<ContainerComponent>("{0} is completing prepare to start.", this);
			AsyncResult asyncResult = Interlocked.Exchange<AsyncResult>(ref this.pendingAsyncResult, null);
			base.DiagnosticsSession.Assert(asyncResult != null, "The pendingAsyncResult must not be null", new object[0]);
			if (exception != null)
			{
				asyncResult.SetAsCompleted(exception);
				return;
			}
			base.BeginDispatchDonePreparingToStartSignal(asyncResult, new AsyncCallback(base.EndDispatchDonePreparingToStartSignal), null);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00005814 File Offset: 0x00003A14
		protected void CompleteStart(ComponentException exception)
		{
			base.DiagnosticsSession.TraceDebug<ContainerComponent>("{0} is completing start.", this);
			AsyncResult asyncResult = Interlocked.Exchange<AsyncResult>(ref this.pendingAsyncResult, null);
			base.DiagnosticsSession.Assert(asyncResult != null, "The pendingAsyncResult must not be null", new object[0]);
			if (exception != null)
			{
				asyncResult.SetAsCompleted(exception);
				return;
			}
			base.BeginDispatchDoneStartingSignal(asyncResult, new AsyncCallback(base.EndDispatchDoneStartingSignal), null);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000587C File Offset: 0x00003A7C
		protected void CompleteStop(ComponentException exception)
		{
			base.DiagnosticsSession.TraceDebug<ContainerComponent>("{0} is completing stop.", this);
			AsyncResult asyncResult = Interlocked.Exchange<AsyncResult>(ref this.pendingAsyncResult, null);
			base.DiagnosticsSession.Assert(asyncResult != null, "The pendingAsyncResult must not be null", new object[0]);
			if (exception != null)
			{
				asyncResult.SetAsCompleted(exception);
				return;
			}
			base.BeginDispatchDoneStoppingSignal(asyncResult, new AsyncCallback(base.EndDispatchDoneStoppingSignal), null);
		}

		// Token: 0x040000DD RID: 221
		private readonly IList<IDiagnosable> diagnosableComponents = new List<IDiagnosable>();

		// Token: 0x040000DE RID: 222
		private AsyncResult pendingAsyncResult;
	}
}
