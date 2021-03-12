using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200076F RID: 1903
	public abstract class WcfClientBase<T> : ClientBase<T>, IDisposeTrackable, IDisposable where T : class
	{
		// Token: 0x0600259F RID: 9631 RVA: 0x0004F194 File Offset: 0x0004D394
		public WcfClientBase()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x0004F1A8 File Offset: 0x0004D3A8
		public WcfClientBase(string endpointConfigurationName) : base(endpointConfigurationName)
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0004F1BD File Offset: 0x0004D3BD
		public WcfClientBase(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x0004F1D3 File Offset: 0x0004D3D3
		public bool IsDisposed
		{
			get
			{
				return this.disposed;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0004F1DB File Offset: 0x0004D3DB
		public DisposeTracker DisposeTracker
		{
			get
			{
				return this.disposeTracker;
			}
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x0004F1E3 File Offset: 0x0004D3E3
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x0004F1F8 File Offset: 0x0004D3F8
		public void ForceLeakReport()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.AddExtraDataWithStackTrace("Force leak was called");
			}
			this.disposeTracker = null;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0004F219 File Offset: 0x0004D419
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0004F228 File Offset: 0x0004D428
		public DisposeTracker GetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0004F230 File Offset: 0x0004D430
		protected void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x0004F24B File Offset: 0x0004D44B
		protected void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				this.InternalDispose(disposing);
				this.disposed = true;
			}
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x0004F280 File Offset: 0x0004D480
		protected virtual void InternalDispose(bool disposing)
		{
			WcfUtils.DisposeWcfClientGracefully(this, true);
			try
			{
				typeof(ClientBase<T>).GetInterfaceMap(typeof(IDisposable)).TargetMethods[0].Invoke(this, null);
			}
			catch (Exception arg)
			{
				ExTraceGlobals.CommonTracer.TraceInformation<Exception>(0, (long)this.GetHashCode(), "WcfClientBase.Dispose: base.Dispose() failed with: {0}", arg);
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0004F2EC File Offset: 0x0004D4EC
		protected virtual DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WcfClientBase<T>>(this);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0004F2F4 File Offset: 0x0004D4F4
		protected virtual void CallService(Action serviceCall, string context)
		{
			try
			{
				serviceCall();
			}
			catch (TimeoutException ex)
			{
				throw new TimeoutErrorTransientException(context, WcfUtils.FullExceptionMessage(ex), ex);
			}
			catch (EndpointNotFoundException ex2)
			{
				throw new EndpointNotFoundTransientException(context, WcfUtils.FullExceptionMessage(ex2), ex2);
			}
			catch (CommunicationException ex3)
			{
				if (ex3 is FaultException)
				{
					FaultException ex4 = (FaultException)ex3;
					if (ex4.Code != null && ex4.Code.SubCode != null && ex4.Code.IsSenderFault && ex4.Code.SubCode.Name == "DeserializationFailed")
					{
						throw new CommunicationErrorPermanentException(context, WcfUtils.FullExceptionMessage(ex3), ex3);
					}
				}
				throw new CommunicationErrorTransientException(context, WcfUtils.FullExceptionMessage(ex3), ex3);
			}
			catch (QuotaExceededException ex5)
			{
				throw new QuotaExceededPermanentException(context, WcfUtils.FullExceptionMessage(ex5), ex5);
			}
			catch (InvalidOperationException ex6)
			{
				throw new InvalidOperationTransientException(context, WcfUtils.FullExceptionMessage(ex6), ex6);
			}
			catch (InvalidDataException ex7)
			{
				throw new InvalidDataTransientException(context, WcfUtils.FullExceptionMessage(ex7), ex7);
			}
		}

		// Token: 0x040022E7 RID: 8935
		private bool disposed;

		// Token: 0x040022E8 RID: 8936
		private DisposeTracker disposeTracker;
	}
}
