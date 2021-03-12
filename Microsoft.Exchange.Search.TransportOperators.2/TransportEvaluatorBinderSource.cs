using System;
using Microsoft.Ceres.CoreServices.Services.Container;
using Microsoft.Ceres.CoreServices.Services.DependencyInjection;
using Microsoft.Ceres.CoreServices.Services.Node;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000008 RID: 8
	[DynamicComponent]
	public class TransportEvaluatorBinderSource : AbstractContainerManaged
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000030DD File Offset: 0x000012DD
		static TransportEvaluatorBinderSource()
		{
			ExWatson.Register();
			ProcessorAffinityHelper.SetProcessorAffinityForCts();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000030E9 File Offset: 0x000012E9
		[Exposed]
		public IEvaluatorBinder TransportEvaluatorBinder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x04000026 RID: 38
		private readonly TransportEvaluatorBinder binder = new TransportEvaluatorBinder();
	}
}
