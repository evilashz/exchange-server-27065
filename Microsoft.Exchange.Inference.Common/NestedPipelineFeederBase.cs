using System;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000038 RID: 56
	internal abstract class NestedPipelineFeederBase : BaseComponent, IStartStopPipelineComponent, IPipelineComponent, IDocumentProcessor, INotifyFailed, IStartStop, IDisposable
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00003832 File Offset: 0x00001A32
		internal NestedPipelineFeederBase(IPipelineComponentConfig config, IPipelineContext context, IPipeline pipeline)
		{
			Util.ThrowOnNullArgument(pipeline, "pipeline");
			this.Config = config;
			this.Context = context;
			this.NestedPipeline = pipeline;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000385A File Offset: 0x00001A5A
		internal IPipelineComponentConfig ComponentConfig
		{
			get
			{
				return this.Config;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003862 File Offset: 0x00001A62
		internal IPipelineContext PipelineContext
		{
			get
			{
				return this.Context;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000386A File Offset: 0x00001A6A
		public IAsyncResult BeginPrepareToStart(AsyncCallback callback, object context)
		{
			return this.CompleteAsyncResult(callback, context);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003874 File Offset: 0x00001A74
		public IAsyncResult BeginStart(AsyncCallback callback, object context)
		{
			return this.CompleteAsyncResult(callback, context);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000387E File Offset: 0x00001A7E
		public IAsyncResult BeginStop(AsyncCallback callback, object context)
		{
			return this.CompleteAsyncResult(callback, context);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003888 File Offset: 0x00001A88
		public void EndPrepareToStart(IAsyncResult asyncResult)
		{
			((AsyncResult)asyncResult).End();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003895 File Offset: 0x00001A95
		public void EndStart(IAsyncResult asyncResult)
		{
			((AsyncResult)asyncResult).End();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000038A2 File Offset: 0x00001AA2
		public void EndStop(IAsyncResult asyncResult)
		{
			((AsyncResult)asyncResult).End();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000038AF File Offset: 0x00001AAF
		public virtual void Dispose()
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000038B4 File Offset: 0x00001AB4
		private AsyncResult CompleteAsyncResult(AsyncCallback callback, object context)
		{
			AsyncResult asyncResult = new AsyncResult(callback, context);
			asyncResult.SetAsCompleted();
			return asyncResult;
		}

		// Token: 0x040000E2 RID: 226
		protected readonly IPipeline NestedPipeline;

		// Token: 0x040000E3 RID: 227
		private readonly IPipelineComponentConfig Config;

		// Token: 0x040000E4 RID: 228
		private readonly IPipelineContext Context;
	}
}
