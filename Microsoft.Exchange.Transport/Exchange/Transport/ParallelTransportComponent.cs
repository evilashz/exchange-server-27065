using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200006D RID: 109
	internal sealed class ParallelTransportComponent : CompositeTransportComponent
	{
		// Token: 0x06000355 RID: 853 RVA: 0x0000F18F File Offset: 0x0000D38F
		public ParallelTransportComponent(string description) : base(description)
		{
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F198 File Offset: 0x0000D398
		public override void Load()
		{
			List<ParallelTransportComponent.TransportComponentStartContext> list = new List<ParallelTransportComponent.TransportComponentStartContext>();
			this.componentsLoading = base.TransportComponents.Count;
			this.finishedLoading = new AutoResetEvent(false);
			foreach (ITransportComponent component in base.TransportComponents)
			{
				ParallelTransportComponent.TransportComponentStartContext transportComponentStartContext = new ParallelTransportComponent.TransportComponentStartContext(component);
				list.Add(transportComponentStartContext);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnLoadChild), transportComponentStartContext);
			}
			try
			{
				this.finishedLoading.WaitOne();
			}
			catch (ThreadInterruptedException inner)
			{
				throw new TransportComponentLoadFailedException("Failed loading component", inner);
			}
			foreach (ParallelTransportComponent.TransportComponentStartContext transportComponentStartContext2 in list)
			{
				if (transportComponentStartContext2.FailureException != null)
				{
					for (int i = list.Count - 1; i >= 0; i--)
					{
						if (list[i].FailureException == null)
						{
							ITransportComponent component2 = list[i].Component;
							CompositeTransportComponent.UnRegisterForDiagnostics(component2);
							component2.Unload();
						}
					}
					string message = Strings.TransportComponentLoadFailedWithName(transportComponentStartContext2.Component.GetType().Name);
					throw new TransportComponentLoadFailedException(message, transportComponentStartContext2.FailureException);
				}
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F320 File Offset: 0x0000D520
		private void OnLoadChild(object state)
		{
			ParallelTransportComponent.TransportComponentStartContext childContext = (ParallelTransportComponent.TransportComponentStartContext)state;
			try
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Loading component {0}.", childContext.Component.GetType().Name);
				childContext.SetStartTime();
				base.BeginTiming(CompositeTransportComponent.Operation.Load, childContext.Component);
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					childContext.Component.Load();
				}, 1);
				if (!adoperationResult.Succeeded)
				{
					throw new TransportComponentLoadFailedException(Strings.ReadingADConfigFailed, adoperationResult.Exception);
				}
				base.EndTiming(CompositeTransportComponent.Operation.Load, childContext.Component);
				childContext.SetEndTime();
				CompositeTransportComponent.RegisterForDiagnostics(childContext.Component);
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Loaded component {0}.", childContext.Component.GetType().Name);
			}
			catch (TransportComponentLoadFailedException ex)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<string, string>(0L, "Failed loading component {0}. {1}", childContext.Component.GetType().Name, ex.Message);
				childContext.Failed(ex);
			}
			if (Interlocked.Decrement(ref this.componentsLoading) == 0)
			{
				this.finishedLoading.Set();
			}
		}

		// Token: 0x040001D8 RID: 472
		private int componentsLoading;

		// Token: 0x040001D9 RID: 473
		private AutoResetEvent finishedLoading;

		// Token: 0x0200006E RID: 110
		private class TransportComponentStartContext
		{
			// Token: 0x06000358 RID: 856 RVA: 0x0000F474 File Offset: 0x0000D674
			public TransportComponentStartContext(ITransportComponent component)
			{
				this.component = component;
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0000F48E File Offset: 0x0000D68E
			public Exception FailureException
			{
				get
				{
					return this.failureException;
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x0600035A RID: 858 RVA: 0x0000F496 File Offset: 0x0000D696
			public TimeSpan TimeElapsed
			{
				get
				{
					return this.stepStopwatch.Elapsed;
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x0600035B RID: 859 RVA: 0x0000F4A3 File Offset: 0x0000D6A3
			public ITransportComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x0600035C RID: 860 RVA: 0x0000F4AB File Offset: 0x0000D6AB
			public void Failed(Exception failureException)
			{
				this.failureException = failureException;
			}

			// Token: 0x0600035D RID: 861 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
			public void SetStartTime()
			{
				this.stepStopwatch.Start();
			}

			// Token: 0x0600035E RID: 862 RVA: 0x0000F4C1 File Offset: 0x0000D6C1
			public void SetEndTime()
			{
				this.stepStopwatch.Stop();
			}

			// Token: 0x040001DA RID: 474
			private ITransportComponent component;

			// Token: 0x040001DB RID: 475
			private Exception failureException;

			// Token: 0x040001DC RID: 476
			private Stopwatch stepStopwatch = new Stopwatch();
		}
	}
}
