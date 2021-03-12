using System;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000004 RID: 4
	internal class TransportErrorProducer : ExchangeProducerBase<TransportErrorOperator>
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000021F1 File Offset: 0x000003F1
		public TransportErrorProducer(TransportErrorOperator errorOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(errorOperator, inputType, context, ExTraceGlobals.ErrorOperatorTracer)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002201 File Offset: 0x00000401
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002209 File Offset: 0x00000409
		internal IUpdateableRecord Holder { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002212 File Offset: 0x00000412
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000221A File Offset: 0x0000041A
		internal int ExceptionIndex { get; private set; }

		// Token: 0x0600000C RID: 12 RVA: 0x00002223 File Offset: 0x00000423
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<TransportErrorProducer>(this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222C File Offset: 0x0000042C
		public override void InternalProcessRecord(IRecord record)
		{
			this.Holder.UpdateFrom(record, record.Descriptor.Count);
			Exception exceptionValue = ((IExceptionField)record[this.ExceptionIndex]).ExceptionValue;
			ItemDepot.Instance.DisposeItems(base.FlowIdentifier);
			throw new EvaluationException("Failure exception re-throw", exceptionValue);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002284 File Offset: 0x00000484
		protected override void Initialize()
		{
			base.Initialize();
			this.recordImpl = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.Holder = this.recordImpl.CreateUpdatableRecord();
			this.ExceptionIndex = base.InputType.RecordType.Position(base.OperatorInstance.Exception);
		}

		// Token: 0x04000002 RID: 2
		private IRecordImplDescriptor recordImpl;
	}
}
