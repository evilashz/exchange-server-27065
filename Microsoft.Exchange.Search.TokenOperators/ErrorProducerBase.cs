using System;
using System.Text;
using Microsoft.Ceres.DocParsing.Runtime.Client;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Abstraction.Exceptions;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200000B RID: 11
	internal abstract class ErrorProducerBase : ExchangeProducerBase<ErrorOperator>
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000422F File Offset: 0x0000242F
		public ErrorProducerBase(ErrorOperator errorOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(errorOperator, inputType, context, ExTraceGlobals.ErrorOperatorTracer)
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000423F File Offset: 0x0000243F
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00004247 File Offset: 0x00002447
		internal IUpdateableRecord Holder { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004250 File Offset: 0x00002450
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00004258 File Offset: 0x00002458
		internal int ExceptionIndex { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004261 File Offset: 0x00002461
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004269 File Offset: 0x00002469
		internal int ExceptionCorrelationIdIndex { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004272 File Offset: 0x00002472
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000427A File Offset: 0x0000247A
		internal int CompositeItemIdIndex { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004283 File Offset: 0x00002483
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000428B File Offset: 0x0000248B
		internal int FolderIdIndex { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004294 File Offset: 0x00002494
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000429C File Offset: 0x0000249C
		internal int TenantIdIndex { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000042A5 File Offset: 0x000024A5
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000042AD File Offset: 0x000024AD
		internal int MailboxGuidIndex { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000042B6 File Offset: 0x000024B6
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000042BE File Offset: 0x000024BE
		internal int DocumentIdIndex { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000042C7 File Offset: 0x000024C7
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000042CF File Offset: 0x000024CF
		internal int IndexIdIndex { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000042D8 File Offset: 0x000024D8
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000042E0 File Offset: 0x000024E0
		internal int ErrorCodeIndex { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000042E9 File Offset: 0x000024E9
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000042F1 File Offset: 0x000024F1
		internal int ErrorMessageIndex { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000042FA File Offset: 0x000024FA
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00004302 File Offset: 0x00002502
		internal int AttemptCountIndex { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000430B File Offset: 0x0000250B
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004313 File Offset: 0x00002513
		internal int LastAttemptTimeIndex { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000431C File Offset: 0x0000251C
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00004324 File Offset: 0x00002524
		internal int IndexSystemNameIndex { get; private set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x00004330 File Offset: 0x00002530
		public override void InternalProcessRecord(IRecord record)
		{
			this.Holder.UpdateFrom(record, record.Descriptor.Count);
			Exception exceptionValue = ((IExceptionField)record[this.ExceptionIndex]).ExceptionValue;
			Guid primitiveGuidValue = ((IGuidField)record[this.ExceptionCorrelationIdIndex]).PrimitiveGuidValue;
			StringBuilder stringBuilder = new StringBuilder();
			if (exceptionValue != null)
			{
				int attemptCount;
				int num;
				string text;
				long documentId;
				try
				{
					RecordInformation associatedRecordInformation = this.GetAssociatedRecordInformation(primitiveGuidValue);
					IIdentity compositeItemId = associatedRecordInformation.CompositeItemId;
					if (exceptionValue is EndlessParsingException)
					{
						this.HandleEndlessParsingException(compositeItemId, primitiveGuidValue, exceptionValue.ToString());
					}
					if (exceptionValue is EndlessEvaluationFaultInjectionException)
					{
						this.HandleEndlessParsingException(compositeItemId, primitiveGuidValue, exceptionValue.ToString());
					}
					attemptCount = associatedRecordInformation.AttemptCount;
					num = associatedRecordInformation.ErrorCode;
					text = compositeItemId.ToString();
					documentId = this.GetDocumentId(compositeItemId);
					((IUpdateableInt64Field)this.Holder[this.IndexIdIndex]).Int64Value = documentId;
					((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value = documentId;
					((IUpdateableStringField)this.Holder[this.MailboxGuidIndex]).StringValue = this.GetMailboxId(compositeItemId);
					((IUpdateableStringField)this.Holder[this.CompositeItemIdIndex]).StringValue = text;
					((IUpdateableGuidField)this.Holder[this.TenantIdIndex]).PrimitiveGuidValue = associatedRecordInformation.TenantId;
					((IUpdateableInt32Field)this.Holder[this.AttemptCountIndex]).Int32Value = attemptCount;
					((IUpdateableStringField)this.Holder[this.IndexSystemNameIndex]).StringValue = associatedRecordInformation.IndexSystemName;
					((IUpdateableStringField)this.Holder[this.FolderIdIndex]).StringValue = this.GetFolderId(compositeItemId);
				}
				catch (ArgumentOutOfRangeException)
				{
					string message = string.Format("No associated record information for the given CorrelationId: {0}", primitiveGuidValue);
					base.Tracer.TraceError((long)base.TracingContext, message);
					throw new Exception(message, exceptionValue);
				}
				finally
				{
					this.CleanupExchangeResources(primitiveGuidValue, exceptionValue);
				}
				if (exceptionValue is EvaluationException)
				{
					EvaluationException ex = (EvaluationException)exceptionValue;
					num = ex.ErrorId;
					if (!string.IsNullOrEmpty(ex.HelpLink))
					{
						stringBuilder.AppendLine(ex.HelpLink);
					}
					stringBuilder.AppendLine(string.Format("{0} : ", ex.OriginatingOperator));
				}
				else
				{
					if (num == 0)
					{
						num = this.MapErrorCodeFromException(exceptionValue);
					}
					stringBuilder.AppendLine(exceptionValue.ToString());
				}
				((IUpdateableStringField)this.Holder[this.ErrorMessageIndex]).StringValue = stringBuilder.ToString();
				DateTime dateTime = Util.NormalizeDateTime(DateTime.UtcNow);
				((IUpdateableDateTimeField)this.Holder[this.LastAttemptTimeIndex]).DateTimeValue = dateTime;
				base.Tracer.TraceDebug<long, string, DateTime>((long)base.TracingContext, "Set LastAttemptTime, DocumentId: {0}, MdbCompositeItemId: {1}, DateTime: {2}", documentId, text, dateTime);
				if (EvaluationErrorsHelper.ShouldMakePermanent(attemptCount, num))
				{
					int int32Value = EvaluationErrorsHelper.MakePermanentError(num);
					((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = int32Value;
					EvaluationErrors errorCode = EvaluationErrorsHelper.GetErrorCode(num);
					base.Diagnostics.LogFailedItem(dateTime, text, primitiveGuidValue, false, attemptCount, errorCode.ToString(), stringBuilder.ToString());
				}
				else
				{
					((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = EvaluationErrorsHelper.MakeRetriableError(num);
				}
				base.Tracer.TraceDebug<long, string, string>((long)base.TracingContext, "Failed Item entry for DocumentId: {0}, MdbCompositeItemId: {1}, ErrorMessage: {2}", documentId, text, ((IUpdateableStringField)this.Holder[this.ErrorMessageIndex]).StringValue);
				IndexableRecordValidation.Instance.ValidateIndexableRecord(this.Holder, base.FlowIdentifier, base.InputProperties.RecordSetType.RecordType, false);
				base.SetNextRecord(this.Holder);
				return;
			}
			throw new InvalidOperationException("Exception value was empty. The Error Operator asserts that this field be set when used on an error edge.");
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004728 File Offset: 0x00002928
		protected override void Initialize()
		{
			base.Initialize();
			this.recordImpl = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.Holder = this.recordImpl.CreateUpdatableRecord();
			base.OutputProperties = new RecordSetProperties(base.OutputType, this.Holder, this.recordImpl);
			this.ExceptionIndex = base.InputType.RecordType.Position(base.OperatorInstance.Exception);
			this.ExceptionCorrelationIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.ExceptionCorrelationId);
			this.CompositeItemIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.CompositeItemId);
			this.TenantIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.TenantId);
			this.MailboxGuidIndex = base.InputType.RecordType.Position(base.OperatorInstance.MailboxGuid);
			this.DocumentIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.DocumentId);
			this.IndexIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.IndexId);
			this.ErrorCodeIndex = base.InputType.RecordType.Position(base.OperatorInstance.ErrorCode);
			this.ErrorMessageIndex = base.InputType.RecordType.Position(base.OperatorInstance.ErrorMessage);
			this.AttemptCountIndex = base.InputType.RecordType.Position(base.OperatorInstance.AttemptCount);
			this.LastAttemptTimeIndex = base.InputType.RecordType.Position(base.OperatorInstance.LastAttemptTime);
			this.IndexSystemNameIndex = base.InputType.RecordType.Position(base.OperatorInstance.IndexSystemName);
			this.FolderIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.FolderId);
		}

		// Token: 0x060000A9 RID: 169
		protected abstract long GetDocumentId(IIdentity identity);

		// Token: 0x060000AA RID: 170
		protected abstract string GetMailboxId(IIdentity identity);

		// Token: 0x060000AB RID: 171
		protected abstract string GetFolderId(IIdentity identity);

		// Token: 0x060000AC RID: 172
		protected abstract int MapErrorCodeFromException(Exception e);

		// Token: 0x060000AD RID: 173
		protected abstract void CleanupExchangeResources(Guid correlationId, Exception exception);

		// Token: 0x060000AE RID: 174
		protected abstract void HandleEndlessParsingException(IIdentity identity, Guid correlationId, string errorMessage);

		// Token: 0x060000AF RID: 175 RVA: 0x0000492C File Offset: 0x00002B2C
		private RecordInformation GetAssociatedRecordInformation(Guid correlationId)
		{
			return ItemDepot.Instance.GetAssociatedRecordInformation(base.FlowIdentifier, correlationId);
		}

		// Token: 0x0400003D RID: 61
		private IRecordImplDescriptor recordImpl;
	}
}
