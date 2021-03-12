using System;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Monad;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001A3 RID: 419
	internal class MonadAsyncResult : IAsyncResult
	{
		// Token: 0x06000F1E RID: 3870 RVA: 0x0002BA80 File Offset: 0x00029C80
		internal MonadAsyncResult(MonadCommand runningCommand, AsyncCallback callback, object stateObject, IAsyncResult psAsyncResult, PSDataCollection<PSObject> output)
		{
			ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "new MonadAsyncResult({0})", runningCommand.CommandText);
			this.runningCommand = runningCommand;
			this.asyncState = stateObject;
			this.callback = callback;
			this.completionEvent = new ManualResetEvent(false);
			runningCommand.ActivePipeline.InvocationStateChanged += this.PipelineStateChanged;
			this.SetIsCompleted(runningCommand.ActivePipeline.InvocationStateInfo.State);
			this.psAsyncResult = psAsyncResult;
			this.output = output;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0002BB0D File Offset: 0x00029D0D
		public MonadCommand RunningCommand
		{
			get
			{
				return this.runningCommand;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0002BB15 File Offset: 0x00029D15
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0002BB1D File Offset: 0x00029D1D
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.completionEvent;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0002BB25 File Offset: 0x00029D25
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0002BB28 File Offset: 0x00029D28
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0002BB30 File Offset: 0x00029D30
		public IAsyncResult PowerShellIAsyncResult
		{
			get
			{
				return this.psAsyncResult;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0002BB38 File Offset: 0x00029D38
		public PSDataCollection<PSObject> Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0002BB40 File Offset: 0x00029D40
		private void SetIsCompleted(PSInvocationState psi)
		{
			ExTraceGlobals.IntegrationTracer.Information<PSInvocationState>((long)this.GetHashCode(), "MonadAsyncResult.SetIsCompleted({0})", psi);
			if (this.isCompleted)
			{
				return;
			}
			this.isCompleted = (psi == PSInvocationState.Completed || psi == PSInvocationState.Failed || psi == PSInvocationState.Stopped);
			if (this.isCompleted)
			{
				this.completionEvent.Set();
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0002BB98 File Offset: 0x00029D98
		internal void PipelineStateChanged(object sender, PSInvocationStateChangedEventArgs e)
		{
			ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "-->MonadAsyncResult.PipelineStateChanged()");
			this.SetIsCompleted(e.InvocationStateInfo.State);
			if (this.IsCompleted && this.callback != null)
			{
				ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "\tInvoking callback.");
				this.callback(this);
			}
			ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "<--MonadAsyncResult.PipelineStateChanged()");
		}

		// Token: 0x04000326 RID: 806
		private MonadCommand runningCommand;

		// Token: 0x04000327 RID: 807
		private object asyncState;

		// Token: 0x04000328 RID: 808
		private AsyncCallback callback;

		// Token: 0x04000329 RID: 809
		private bool isCompleted;

		// Token: 0x0400032A RID: 810
		private ManualResetEvent completionEvent;

		// Token: 0x0400032B RID: 811
		private IAsyncResult psAsyncResult;

		// Token: 0x0400032C RID: 812
		private PSDataCollection<PSObject> output;
	}
}
