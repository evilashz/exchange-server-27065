using System;
using Microsoft.Ceres.CoreServices.Services.Container;
using Microsoft.Ceres.CoreServices.Services.DependencyInjection;
using Microsoft.Ceres.CoreServices.Services.Node;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000024 RID: 36
	[DynamicComponent]
	public class TokenEvaluatorBinderSource : AbstractContainerManaged
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00006281 File Offset: 0x00004481
		static TokenEvaluatorBinderSource()
		{
			ExWatson.Register();
			ProcessorAffinityHelper.SetProcessorAffinityForCts();
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000628D File Offset: 0x0000448D
		[Exposed]
		public IEvaluatorBinder TokenEvaluatorBinder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x0400007F RID: 127
		private readonly TokenEvaluatorBinder binder = new TokenEvaluatorBinder();
	}
}
