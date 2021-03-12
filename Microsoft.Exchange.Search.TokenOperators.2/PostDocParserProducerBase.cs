using System;
using System.Text;
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
	// Token: 0x0200001C RID: 28
	internal abstract class PostDocParserProducerBase : ExchangeProducerBase<PostDocParserOperator>, IDisposeTrackable, IDisposable
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00005701 File Offset: 0x00003901
		public PostDocParserProducerBase(PostDocParserOperator postDocParserOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(postDocParserOperator, inputType, context, ExTraceGlobals.PostDocParserOperatorTracer)
		{
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005711 File Offset: 0x00003911
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00005719 File Offset: 0x00003919
		internal IUpdateableRecord Holder { get; private set; }

		// Token: 0x060000FD RID: 253 RVA: 0x00005724 File Offset: 0x00003924
		public override void InternalProcessRecord(IRecord record)
		{
			this.errorCodeField.NullableInt32Value = null;
			this.errorMessageField.StringValue = null;
			this.Holder.UpdateFrom(record, record.Descriptor.Count);
			string stringValue = this.contactTitleField.StringValue;
			this.titleField.StringValue = stringValue;
			base.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalTimeSpentDocParser, base.Diagnostics.LastEntry.Elapsed);
			int? nullableInt32Value = this.errorCodeField.NullableInt32Value;
			string text = this.errorMessageField.StringValue;
			StringBuilder stringBuilder = new StringBuilder(text);
			bool flag = false;
			foreach (Exception ex in this.documentParserErrorsField.FieldEnumerable)
			{
				EvaluationException ex2 = ex as EvaluationException;
				if (ex2 != null)
				{
					stringBuilder.AppendFormat(" {0} {1}", ex2.ErrorId, ex2.Message);
					flag = true;
				}
			}
			if (flag)
			{
				if (nullableInt32Value == null || nullableInt32Value == 0)
				{
					nullableInt32Value = new int?(EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.DocumentParserFailure));
				}
				text = stringBuilder.ToString();
				DateTime dateTimeValue = Util.NormalizeDateTime(DateTime.UtcNow);
				ManagedProperties.SetAsPartiallyProcessed(this.Holder);
				this.lastAttemptedDateTimeField.DateTimeValue = dateTimeValue;
				this.errorCodeField.NullableInt32Value = nullableInt32Value;
				this.errorMessageField.StringValue = text;
			}
			this.CleanupExchangeResources();
			base.SetNextRecord(this.Holder);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000058B8 File Offset: 0x00003AB8
		protected override void Initialize()
		{
			base.Initialize();
			this.recordImpl = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.Holder = this.recordImpl.CreateUpdatableRecord();
			base.OutputProperties = new RecordSetProperties(base.OutputType, this.Holder, this.recordImpl);
			this.documentParserErrorsField = (IUpdateableListField<Exception>)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.DocumentParserErrors)];
			this.contactTitleField = (IUpdateableStringField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.ContactTitle)];
			this.errorMessageField = (IUpdateableStringField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.ErrorMessage)];
			this.errorCodeField = (IUpdateableInt32Field)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.ErrorCode)];
			this.lastAttemptedDateTimeField = (IUpdateableDateTimeField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.LastAttemptTime)];
			this.titleField = (IUpdateableStringField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.Title)];
		}

		// Token: 0x060000FF RID: 255
		protected abstract void CleanupExchangeResources();

		// Token: 0x0400006A RID: 106
		private IRecordImplDescriptor recordImpl;

		// Token: 0x0400006B RID: 107
		private IUpdateableStringField errorMessageField;

		// Token: 0x0400006C RID: 108
		private IUpdateableInt32Field errorCodeField;

		// Token: 0x0400006D RID: 109
		private IUpdateableDateTimeField lastAttemptedDateTimeField;

		// Token: 0x0400006E RID: 110
		private IUpdateableStringField titleField;

		// Token: 0x0400006F RID: 111
		private IUpdateableStringField contactTitleField;

		// Token: 0x04000070 RID: 112
		private IUpdateableListField<Exception> documentParserErrorsField;
	}
}
