using System;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200000A RID: 10
	internal abstract class ErrorBypassProducerBase : ExchangeProducerBase<ErrorBypassOperator>
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003D50 File Offset: 0x00001F50
		public ErrorBypassProducerBase(ErrorBypassOperator errorBypassOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(errorBypassOperator, inputType, context, ExTraceGlobals.ErrorBypassOperatorTracer)
		{
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003D60 File Offset: 0x00001F60
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003D68 File Offset: 0x00001F68
		internal IUpdateableRecord Holder { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003D71 File Offset: 0x00001F71
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003D79 File Offset: 0x00001F79
		internal int DocumentIdIndex { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003D82 File Offset: 0x00001F82
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003D8A File Offset: 0x00001F8A
		internal int ErrorCodeIndex { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003D93 File Offset: 0x00001F93
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003D9B File Offset: 0x00001F9B
		internal int AttemptCountIndex { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003DA4 File Offset: 0x00001FA4
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003DAC File Offset: 0x00001FAC
		internal int CompositeItemIdIndex { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003DB5 File Offset: 0x00001FB5
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003DBD File Offset: 0x00001FBD
		internal int CorrelationIdIndex { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003DC6 File Offset: 0x00001FC6
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003DCE File Offset: 0x00001FCE
		internal int ErrorMessageIndex { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003DD7 File Offset: 0x00001FD7
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003DDF File Offset: 0x00001FDF
		internal int LastAttemptTimeIndex { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003DE8 File Offset: 0x00001FE8
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003DF0 File Offset: 0x00001FF0
		internal int FolderIdIndex { get; private set; }

		// Token: 0x06000086 RID: 134 RVA: 0x00003DFC File Offset: 0x00001FFC
		public override void InternalProcessRecord(IRecord record)
		{
			this.Holder.UpdateFrom(record, record.Descriptor.Count);
			int int32Value = ((IInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value;
			if (int32Value == 202)
			{
				base.Tracer.TraceDebug((long)base.TracingContext, "Intentionally throwing Exception due to test error code.");
				throw new InvalidOperationException("Test Intentional Failure.");
			}
			if (int32Value == 204)
			{
				base.Tracer.TraceDebug((long)base.TracingContext, "Intentionally throwing Transient Exception due to test error code.");
				throw new EvaluationException("Test Intentional Failure.")
				{
					IsTransient = true
				};
			}
			if (int32Value == 205)
			{
				base.Tracer.TraceDebug((long)base.TracingContext, "Intentionally throwing Transient Item Truncated Exception due to test error code.");
				throw new EvaluationException("The item could not be indexed successfully because the item failed in the indexing subsystem. Content group 8XSBcM10.95 was submitted in generation GID[94840]. Item truncated. Field=body, Occurrences=93952, Chars=3145760 RecoverableIndexException. [DocumentName=]")
				{
					IsTransient = true
				};
			}
			DateTime dateTime = Util.NormalizeDateTime(DateTime.UtcNow);
			((IUpdateableDateTimeField)this.Holder[this.LastAttemptTimeIndex]).DateTimeValue = dateTime;
			string stringValue = ((IStringField)this.Holder[this.CompositeItemIdIndex]).StringValue;
			((IUpdateableStringField)this.Holder[this.FolderIdIndex]).StringValue = this.GetFolderId(stringValue);
			base.Tracer.TraceDebug<long, DateTime>((long)base.TracingContext, "Set LastAttemptTime, DocumentId: {0}, DateTime: {1}", ((IInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, dateTime);
			int attemptCount = ((IInt32Field)this.Holder[this.AttemptCountIndex]).NullableInt32Value ?? 0;
			if (EvaluationErrorsHelper.ShouldMakePermanent(attemptCount, int32Value))
			{
				int int32Value2 = EvaluationErrorsHelper.MakePermanentError(int32Value);
				((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = int32Value2;
				EvaluationErrors errorCode = EvaluationErrorsHelper.GetErrorCode(int32Value);
				if (errorCode == EvaluationErrors.PoisonDocument)
				{
					base.IncrementPerfcounter(OperatorPerformanceCounter.TotalPoisonDocumentsProcessed);
				}
				base.Diagnostics.LogFailedItem(dateTime, ((IStringField)this.Holder[this.CompositeItemIdIndex]).StringValue, ((IGuidField)this.Holder[this.CorrelationIdIndex]).PrimitiveGuidValue, false, attemptCount, errorCode.ToString(), ((IStringField)this.Holder[this.ErrorMessageIndex]).StringValue);
			}
			else
			{
				((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = EvaluationErrorsHelper.MakeRetriableError(int32Value);
			}
			base.Tracer.TraceDebug<long, int>((long)base.TracingContext, "Setting error values for DocumentId: {0}, ErrorCode: {1}", ((IInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, ((IInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value);
			base.SetNextRecord(this.Holder);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000040C8 File Offset: 0x000022C8
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ErrorBypassProducerBase>(this);
		}

		// Token: 0x06000088 RID: 136
		protected abstract string GetFolderId(string identity);

		// Token: 0x06000089 RID: 137 RVA: 0x000040D0 File Offset: 0x000022D0
		protected override void Initialize()
		{
			base.Initialize();
			this.recordImpl = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.Holder = this.recordImpl.CreateUpdatableRecord();
			base.OutputProperties = new RecordSetProperties(base.OutputType, this.Holder, this.recordImpl);
			this.DocumentIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.DocumentId);
			this.ErrorCodeIndex = base.InputType.RecordType.Position(base.OperatorInstance.ErrorCode);
			this.AttemptCountIndex = base.InputType.RecordType.Position(base.OperatorInstance.AttemptCount);
			this.LastAttemptTimeIndex = base.InputType.RecordType.Position(base.OperatorInstance.LastAttemptTime);
			this.CompositeItemIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.CompositeItemId);
			this.ErrorMessageIndex = base.InputType.RecordType.Position(base.OperatorInstance.ErrorMessage);
			this.CorrelationIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.CorrelationId);
			this.FolderIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.FolderId);
		}

		// Token: 0x04000033 RID: 51
		private IRecordImplDescriptor recordImpl;
	}
}
