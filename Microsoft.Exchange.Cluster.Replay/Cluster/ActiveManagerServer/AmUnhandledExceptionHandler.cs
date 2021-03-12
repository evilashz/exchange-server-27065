using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000099 RID: 153
	internal class AmUnhandledExceptionHandler
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001EECB File Offset: 0x0001D0CB
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AmSystemManagerTracer;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001EED4 File Offset: 0x0001D0D4
		internal void Add(IUnhandledExceptionHandler handler)
		{
			lock (this.m_locker)
			{
				this.m_handlers.Add(handler);
				if (!this.m_isHandlerSet)
				{
					AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;
					AmUnhandledExceptionHandler.Tracer.TraceDebug((long)this.GetHashCode(), "AmUnhandledExceptionHandler: Unhandled exception handler is set.");
					this.m_isHandlerSet = true;
				}
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001EF58 File Offset: 0x0001D158
		internal void Remove(IUnhandledExceptionHandler handler)
		{
			lock (this.m_locker)
			{
				if (handler != null)
				{
					if (this.m_handlers.Count > 0)
					{
						this.m_handlers.Remove(handler);
					}
				}
				else
				{
					this.m_handlers.Clear();
				}
				if (this.m_handlers.Count == 0 && this.m_isHandlerSet)
				{
					AppDomain.CurrentDomain.UnhandledException -= this.OnUnhandledException;
					AmUnhandledExceptionHandler.Tracer.TraceDebug((long)this.GetHashCode(), "AmUnhandledExceptionHandler: Unhandled exception handler is removed.");
					this.m_isHandlerSet = false;
				}
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001F008 File Offset: 0x0001D208
		internal void Cleanup()
		{
			this.Remove(null);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001F014 File Offset: 0x0001D214
		private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			lock (this.m_locker)
			{
				string text = (e != null && e.ExceptionObject != null) ? e.ExceptionObject.ToString() : "<unknown>";
				ReplayCrimsonEvents.OperationGeneratedUnhandledException.Log<string>(text);
				AmUnhandledExceptionHandler.Tracer.TraceError<string>((long)this.GetHashCode(), "Best effort critical cleanup before watson dump is triggered (error: {0})", text);
				foreach (IUnhandledExceptionHandler unhandledExceptionHandler in this.m_handlers)
				{
					if (unhandledExceptionHandler != null)
					{
						unhandledExceptionHandler.OnUnhandledException();
					}
				}
			}
		}

		// Token: 0x0400028E RID: 654
		private List<IUnhandledExceptionHandler> m_handlers = new List<IUnhandledExceptionHandler>();

		// Token: 0x0400028F RID: 655
		private object m_locker = new object();

		// Token: 0x04000290 RID: 656
		private bool m_isHandlerSet;
	}
}
