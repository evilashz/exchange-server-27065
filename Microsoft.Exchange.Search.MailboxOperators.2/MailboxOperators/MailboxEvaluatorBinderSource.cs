using System;
using Microsoft.Ceres.CoreServices.Services.Container;
using Microsoft.Ceres.CoreServices.Services.DependencyInjection;
using Microsoft.Ceres.CoreServices.Services.Node;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000015 RID: 21
	[DynamicComponent]
	public class MailboxEvaluatorBinderSource : AbstractContainerManaged
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00006835 File Offset: 0x00004A35
		static MailboxEvaluatorBinderSource()
		{
			ExWatson.Register();
			OperatorDiagnosticsFactory.EnableGetExchangeDiagnosticsInfo();
			ProcessorAffinityHelper.SetProcessorAffinityForCts();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00006846 File Offset: 0x00004A46
		[Exposed]
		public IEvaluatorBinder MailboxEvaluatorBinder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x040000E2 RID: 226
		private readonly MailboxEvaluatorBinder binder = new MailboxEvaluatorBinder();
	}
}
