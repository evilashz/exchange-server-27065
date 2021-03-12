using System;
using System.Threading;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000011 RID: 17
	internal abstract class MailboxProducerBase<TOperator> : ExchangeProducerBase<TOperator> where TOperator : OperatorBase
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00005E8A File Offset: 0x0000408A
		protected MailboxProducerBase(TOperator operatorInstance, IRecordSetTypeDescriptor inputType, IEvaluationContext context, Trace tracer) : base(operatorInstance, inputType, context, tracer)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005E97 File Offset: 0x00004097
		protected override void SetInstance(Guid instanceGuid, string instanceName)
		{
			if (instanceGuid != base.InstanceGuid || instanceName != base.InstanceName)
			{
				base.SetInstance(instanceGuid, instanceName);
				this.mailboxPerfCounterInstance = null;
				this.SetFlowPerformanceCounters();
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005ECA File Offset: 0x000040CA
		protected void SetFlowPerformanceCounters()
		{
			if (Interlocked.CompareExchange<MailboxFlowPerformanceCounters>(ref this.mailboxPerfCounterInstance, new MailboxFlowPerformanceCounters(base.InstanceName), null) == null)
			{
				base.SetFlowPerformanceCounters(this.mailboxPerfCounterInstance);
			}
		}

		// Token: 0x040000D3 RID: 211
		private MailboxFlowPerformanceCounters mailboxPerfCounterInstance;
	}
}
