using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Inference;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000003 RID: 3
	internal abstract class BaseComponent : IPipelineComponent, IDocumentProcessor, INotifyFailed
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002152 File Offset: 0x00000352
		protected BaseComponent() : this(null)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000215C File Offset: 0x0000035C
		protected BaseComponent(IPipelineComponentConfig config)
		{
			InferenceCommonUtility.ConfigTryParseHelper<bool>(config, new InferenceCommonUtility.TryParseFunction<bool>(bool.TryParse), "AsyncComponent", out this.isAsynchronous, this.DiagnosticsSession, false);
			this.DiagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession("BaseComponent", ExTraceGlobals.SynchronousComponentBaseTracer, (long)this.GetHashCode());
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000E RID: 14 RVA: 0x000021B0 File Offset: 0x000003B0
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x000021E8 File Offset: 0x000003E8
		public event EventHandler<FailedEventArgs> Failed;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16
		public abstract string Description { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17
		public abstract string Name { get; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002220 File Offset: 0x00000420
		public void ProcessDocument(IDocument document, object context)
		{
			IAsyncResult asyncResult = this.InternalBeginProcess(document, null, context, false);
			this.EndProcess(asyncResult);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000223F File Offset: 0x0000043F
		public virtual IAsyncResult BeginProcess(IDocument document, AsyncCallback callback, object context)
		{
			return this.InternalBeginProcess(document, callback, context, this.isAsynchronous);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002250 File Offset: 0x00000450
		public virtual void EndProcess(IAsyncResult asyncResult)
		{
			this.DiagnosticsSession.TraceDebug("Called end process", new object[0]);
			((AsyncResult)asyncResult).End();
		}

		// Token: 0x06000015 RID: 21
		protected abstract void InternalProcessDocument(DocumentContext data);

		// Token: 0x06000016 RID: 22 RVA: 0x00002274 File Offset: 0x00000474
		protected void OnFailed(FailedEventArgs e)
		{
			EventHandler<FailedEventArgs> failed = this.Failed;
			if (failed != null)
			{
				failed(this, e);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002294 File Offset: 0x00000494
		private IAsyncResult InternalBeginProcess(IDocument document, AsyncCallback callback, object context, bool isAsynchronous)
		{
			Util.ThrowOnNullArgument(document, "document");
			AsyncResult asyncResult = new AsyncResult(callback, context);
			this.DiagnosticsSession.TraceDebug<IIdentity>("Called begin process - {0} ", document.Identity);
			DocumentContext documentContext = new DocumentContext(document, asyncResult);
			if (isAsynchronous)
			{
				ThreadPool.QueueUserWorkItem(CallbackWrapper.WaitCallback(new WaitCallback(this.ProcessDocument)), documentContext);
			}
			else
			{
				this.ProcessDocument(documentContext);
			}
			return asyncResult;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022F8 File Offset: 0x000004F8
		private void ProcessDocument(object context)
		{
			DocumentContext documentContext = context as DocumentContext;
			Util.ThrowOnNullArgument(documentContext, "context");
			IDocument document = documentContext.Document;
			Util.ThrowOnNullArgument(document, "document");
			AsyncResult asyncResult = documentContext.AsyncResult;
			this.DiagnosticsSession.TraceDebug<IIdentity>("Called begin process - {0} ", document.Identity);
			ComponentException ex = null;
			try
			{
				this.InternalProcessDocument(documentContext);
			}
			catch (OperationFailedException ex2)
			{
				ex = ex2;
			}
			catch (ComponentFailedException ex3)
			{
				this.OnFailed(new FailedEventArgs(ex3));
				ex = ex3;
			}
			catch (Exception ex4)
			{
				if (ex4 is OutOfMemoryException || ex4 is StackOverflowException || ex4 is ThreadAbortException)
				{
					throw;
				}
				if (!(ex4 is QuotaExceededException))
				{
					this.DiagnosticsSession.SendInformationalWatsonReport(ex4, null);
				}
				ex = new PoisonComponentException(ex4);
			}
			if (ex != null)
			{
				this.DiagnosticsSession.TraceError<Type, string>("Received an exception of type = {0} and message = {1} ", ex.GetType(), ex.Message);
				asyncResult.SetAsCompleted(ex);
				return;
			}
			asyncResult.SetAsCompleted();
		}

		// Token: 0x04000006 RID: 6
		protected readonly IDiagnosticsSession DiagnosticsSession;

		// Token: 0x04000007 RID: 7
		private readonly bool isAsynchronous;
	}
}
