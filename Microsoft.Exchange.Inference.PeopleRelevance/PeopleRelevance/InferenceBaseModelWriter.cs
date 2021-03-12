using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000007 RID: 7
	internal abstract class InferenceBaseModelWriter<TInferenceModelDataBinder, TModelItem> : BaseComponent where TInferenceModelDataBinder : IInferenceModelDataBinder<TModelItem> where TModelItem : InferenceBaseModelItem
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002729 File Offset: 0x00000929
		protected InferenceBaseModelWriter(IPipelineComponentConfig config, IPipelineContext context)
		{
			Util.ThrowOnNullArgument(context, "context");
			this.Config = config;
			this.Context = context;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000274C File Offset: 0x0000094C
		protected override void InternalProcessDocument(DocumentContext data)
		{
			this.DiagnosticsSession.TraceDebug<IIdentity>("Processing document - {0}", data.Document.Identity);
			TInferenceModelDataBinder modelDataBinder = this.GetModelDataBinder(data.AsyncResult.AsyncState);
			this.DiagnosticsSession.TraceDebug<string>("Got the model data binder", modelDataBinder.GetType().ToString());
			TModelItem modelItem = this.GetModelItem(modelDataBinder);
			this.DiagnosticsSession.TraceDebug("Got the inference model item", new object[0]);
			this.PrepareModelItem(data.Document, modelItem);
			modelItem.LastModifiedTime = ExDateTime.Now.UniversalTime;
			ExAssert.RetailAssert(modelItem.Version != null, "Required Inference model item Version is not set");
			this.WriteModelItem(modelDataBinder, modelItem);
		}

		// Token: 0x06000049 RID: 73
		protected abstract void PrepareModelItem(IDocument document, TModelItem modelItem);

		// Token: 0x0600004A RID: 74
		protected abstract TInferenceModelDataBinder GetModelDataBinder(object context);

		// Token: 0x0600004B RID: 75
		protected abstract void WriteModelItem(TInferenceModelDataBinder dataBinder, TModelItem modelItem);

		// Token: 0x0600004C RID: 76
		protected abstract TModelItem GetModelItem(TInferenceModelDataBinder dataBinder);

		// Token: 0x0400001D RID: 29
		protected readonly IPipelineComponentConfig Config;

		// Token: 0x0400001E RID: 30
		protected readonly IPipelineContext Context;
	}
}
