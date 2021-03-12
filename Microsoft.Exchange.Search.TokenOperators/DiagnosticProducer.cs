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
	// Token: 0x02000009 RID: 9
	internal class DiagnosticProducer : ExchangeProducerBase<DiagnosticOperator>
	{
		// Token: 0x0600006C RID: 108 RVA: 0x0000393C File Offset: 0x00001B3C
		public DiagnosticProducer(DiagnosticOperator diagnosticOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(diagnosticOperator, inputType, context, ExTraceGlobals.DiagnosticOperatorTracer)
		{
			this.languageSelectionEnabled = base.Snapshot.Search.LanguageDetection.EnableLanguageSelection;
			this.regionDefaultLanguage = base.Snapshot.Search.LanguageDetection.RegionDefaultLanguage;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003993 File Offset: 0x00001B93
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000399B File Offset: 0x00001B9B
		internal IUpdateableRecord Holder { get; private set; }

		// Token: 0x0600006F RID: 111 RVA: 0x000039A4 File Offset: 0x00001BA4
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DiagnosticProducer>(this);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000039AC File Offset: 0x00001BAC
		public override void InternalProcessRecord(IRecord record)
		{
			this.UpdateFlowPerfCounters();
			this.Holder.UpdateFrom(record, record.Descriptor.Count);
			if (this.languageSelectionEnabled && base.Operator.Label == "PriorWordBreakerDiagnosticOperator")
			{
				string stringValue = ((IStringField)record[this.languageIndex]).StringValue;
				int num = (((IInt32Field)record[this.messageCodePageIndex]).NullableInt32Value != null) ? ((IInt32Field)record[this.messageCodePageIndex]).NullableInt32Value.Value : -1;
				int num2 = (((IInt32Field)record[this.messageLocaleIDIndex]).NullableInt32Value != null) ? ((IInt32Field)record[this.messageLocaleIDIndex]).NullableInt32Value.Value : -1;
				int num3 = (((IInt32Field)record[this.internetCPIDIndex]).NullableInt32Value != null) ? ((IInt32Field)record[this.internetCPIDIndex]).NullableInt32Value.Value : -1;
				IUpdateableStringField updateableField = base.GetUpdateableField<IUpdateableStringField>(this.Holder, this.languageIndex);
				updateableField.StringValue = LanguagePropertyMapping.GetDesiredLanguage(stringValue, num, num2, num3, this.regionDefaultLanguage);
				base.Tracer.TraceDebug((long)base.TracingContext, "SelectedLanguage: {0}; DetectedLanguage: {1}; MessageCodepage: {2}; MessageLocaleID: {3}; InternetCPID: {4}; RegionDefaultLanguage: {5}", new object[]
				{
					updateableField.StringValue,
					stringValue,
					num,
					num2,
					num3,
					this.regionDefaultLanguage
				});
			}
			if (base.Operator.Label == "PostLanguageIdentifierDiagnosticOperator")
			{
				base.Tracer.TraceDebug<string>((long)base.TracingContext, "PostLanguageIdentifierDiagnosticOperator-DetectedLanguage: {0}", ((IStringField)record[this.languageIndex]).StringValue);
			}
			base.SetNextRecord(this.Holder);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003BB4 File Offset: 0x00001DB4
		protected override void Initialize()
		{
			base.Initialize();
			this.recordImpl = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.Holder = this.recordImpl.CreateUpdatableRecord();
			base.OutputProperties = new RecordSetProperties(base.OutputType, this.Holder, this.recordImpl);
			if (base.Operator.Label == "PriorWordBreakerDiagnosticOperator")
			{
				this.languageIndex = base.InputType.RecordType.Position(base.OperatorInstance.Language);
				this.messageCodePageIndex = base.OutputType.RecordType.Position(base.OperatorInstance.MessageCodePage);
				this.messageLocaleIDIndex = base.OutputType.RecordType.Position(base.OperatorInstance.MessageLocaleID);
				this.internetCPIDIndex = base.OutputType.RecordType.Position(base.OperatorInstance.InternetCPID);
			}
			if (base.Operator.Label == "PostLanguageIdentifierDiagnosticOperator")
			{
				this.languageIndex = base.InputType.RecordType.Position(base.OperatorInstance.Language);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003CE4 File Offset: 0x00001EE4
		private void UpdateFlowPerfCounters()
		{
			if (base.Operator.Label == "PostLanguageIdentifierDiagnosticOperator")
			{
				base.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalTimeSpentLanguageDetection, base.Diagnostics.LastEntry.Elapsed);
				return;
			}
			if (base.Operator.Label == "PostTextualDiagnosticOperator")
			{
				base.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalTimeSpentConvertingToTextual, base.Diagnostics.LastEntry.Elapsed);
			}
		}

		// Token: 0x04000028 RID: 40
		private const string PostLanguageIdentifierDiagnosticOperator = "PostLanguageIdentifierDiagnosticOperator";

		// Token: 0x04000029 RID: 41
		private const string PostTextualDiagnosticOperator = "PostTextualDiagnosticOperator";

		// Token: 0x0400002A RID: 42
		private const string PriorWordBreakerDiagnosticOperator = "PriorWordBreakerDiagnosticOperator";

		// Token: 0x0400002B RID: 43
		private readonly bool languageSelectionEnabled;

		// Token: 0x0400002C RID: 44
		private readonly string regionDefaultLanguage;

		// Token: 0x0400002D RID: 45
		private IRecordImplDescriptor recordImpl;

		// Token: 0x0400002E RID: 46
		private int languageIndex;

		// Token: 0x0400002F RID: 47
		private int messageCodePageIndex;

		// Token: 0x04000030 RID: 48
		private int messageLocaleIDIndex;

		// Token: 0x04000031 RID: 49
		private int internetCPIDIndex;
	}
}
