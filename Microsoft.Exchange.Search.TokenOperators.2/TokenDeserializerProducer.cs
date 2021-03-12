using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.NlpBase.AnnotationStore;
using Microsoft.Ceres.NlpBase.AnnotationStore.Annotations;
using Microsoft.Ceres.NlpBase.RichFields;
using Microsoft.Ceres.NlpBase.RichTypes;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000022 RID: 34
	internal class TokenDeserializerProducer : ExchangeProducerBase<TokenDeserializerOperator>
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00005BFE File Offset: 0x00003DFE
		public TokenDeserializerProducer(TokenDeserializerOperator tokenDeserializerOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(tokenDeserializerOperator, inputType, context, ExTraceGlobals.AnnotationTokenTracer)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005C10 File Offset: 0x00003E10
		public override void InternalProcessRecord(IRecord record)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			base.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalTimeSpentWorkBreaking, base.Diagnostics.LastEntry.Elapsed);
			this.holder.UpdateFrom(record, record.Descriptor.Count);
			IUpdateableTextualField updateableTextualField = (IUpdateableTextualField)this.holder[this.outputFieldIndex];
			updateableTextualField.ResetFieldAndAnnotations();
			IBlobField blobField = (IBlobField)record[this.annotationTokenIndex];
			byte[] blobValue = blobField.BlobValue;
			ITextualField textualField = (ITextualField)this.holder[this.bodyIndex];
			long num = 0L;
			bool flag = false;
			if (blobValue != null && blobValue.Length > 0)
			{
				base.Tracer.TraceDebug((long)base.TracingContext, "Deserialize a TokenInfo object from the annotation blob.");
				TokenInfo tokenInfo = null;
				using (MemoryStream memoryStream = new MemoryStream(blobValue))
				{
					try
					{
						tokenInfo = TokenInfo.Create(memoryStream);
					}
					catch (Exception arg)
					{
						base.Tracer.TraceError<Exception>((long)base.TracingContext, "Failed to create TokenInfo object from the annotation blob. Exception = {0}.", arg);
						((IUpdateableInt32Field)this.holder[this.errorCodeIndex]).Int32Value = 8;
						((IUpdateableDateTimeField)this.holder[this.lastAttemptTimeIndex]).DateTimeValue = Util.NormalizeDateTime(DateTime.UtcNow);
					}
				}
				if (tokenInfo == null)
				{
					goto IL_2F0;
				}
				base.Tracer.TraceDebug((long)base.TracingContext, "Creating the textual field from the text of TokenInfo (and 'body' property - optional).");
				if (textualField.AnnotationStore != null)
				{
					num = (long)((tokenInfo.Text != null) ? tokenInfo.Text.Length : 0);
					updateableTextualField.TextualValue = TextualFunctions.ToTextual(tokenInfo.Text + textualField.ContentReader.ReadToEnd());
				}
				else
				{
					updateableTextualField.TextualValue = TextualFunctions.ToTextual(tokenInfo.Text);
				}
				flag = true;
				using (IAnnotationStoreReadWriter annotationStoreReadWriter = updateableTextualField.AnnotationStore.OpenReadWriter(base.OperatorInstance.Label))
				{
					foreach (AnnotationInfo annotationInfo in tokenInfo.Annotations)
					{
						base.Tracer.TraceFunction<long, long>((long)base.TracingContext, "Adding annotation [{0}, {1}) into annotation store of the textual field.", annotationInfo.Start, annotationInfo.End);
						IUpdatableAnnotation updatableAnnotation = annotationStoreReadWriter.Create(annotationInfo.Name, new Range(annotationInfo.Start, annotationInfo.End), 0);
						foreach (KeyValuePair<string, string> keyValuePair in annotationInfo.Attributes)
						{
							base.Tracer.TraceFunction<string, string>((long)base.TracingContext, "Adding attribute '{0}={1}' into above annotation.", keyValuePair.Key, keyValuePair.Value);
							updatableAnnotation.SetAttribute(keyValuePair.Key, keyValuePair.Value);
						}
						annotationStoreReadWriter.Add(updatableAnnotation);
					}
					annotationStoreReadWriter.Checkpoint(Range.MaxRange);
					goto IL_2F0;
				}
			}
			base.Tracer.TraceDebug((long)base.TracingContext, "Skip deserialization for empty annotation blob.");
			IL_2F0:
			if (textualField.AnnotationStore != null)
			{
				if (!flag)
				{
					updateableTextualField.TextualValue = TextualFunctions.ToTextual(textualField.ContentReader.ReadToEnd());
				}
				using (IAnnotationStoreReadWriter annotationStoreReadWriter2 = updateableTextualField.AnnotationStore.OpenReadWriter(base.OperatorInstance.Label))
				{
					using (IAnnotationStoreReader annotationStoreReader = textualField.AnnotationStore.OpenReader())
					{
						foreach (IAnnotation annotation in annotationStoreReader.QueryByType(0, 0))
						{
							IUpdatableAnnotation updatableAnnotation2 = annotation.ForUpdate();
							updatableAnnotation2.Range = new Range(annotation.Range.Start + num, annotation.Range.End + num);
							annotationStoreReadWriter2.Add(updatableAnnotation2);
						}
						annotationStoreReadWriter2.Checkpoint(Range.MaxRange);
					}
				}
			}
			updateableTextualField.Seal();
			stopwatch.Stop();
			base.IncrementPerfcounterBy(OperatorPerformanceCounter.IndexablePropertiesSize, updateableTextualField.SizeEstimate);
			base.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalTimeSpentTokenDeserializer, (long)stopwatch.Elapsed.TotalMilliseconds);
			base.SetNextRecord(this.holder);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000060FC File Offset: 0x000042FC
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<TokenDeserializerProducer>(this);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006104 File Offset: 0x00004304
		protected override void Initialize()
		{
			base.Initialize();
			IRecordImplDescriptor recordImplDescriptor = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.holder = recordImplDescriptor.CreateUpdatableRecord();
			base.OutputProperties = new RecordSetProperties(base.OutputType, this.holder, recordImplDescriptor);
			this.bodyIndex = base.InputType.RecordType.Position(base.OperatorInstance.Body);
			this.annotationTokenIndex = base.InputType.RecordType.Position(base.OperatorInstance.AnnotationToken);
			this.errorCodeIndex = base.OutputType.RecordType.Position(base.OperatorInstance.ErrorCode);
			this.outputFieldIndex = base.OutputType.RecordType.Position(base.OperatorInstance.OutputField);
			this.lastAttemptTimeIndex = base.OutputType.RecordType.Position(base.OperatorInstance.LastAttemptTime);
		}

		// Token: 0x04000079 RID: 121
		private IUpdateableRecord holder;

		// Token: 0x0400007A RID: 122
		private int annotationTokenIndex;

		// Token: 0x0400007B RID: 123
		private int errorCodeIndex;

		// Token: 0x0400007C RID: 124
		private int outputFieldIndex;

		// Token: 0x0400007D RID: 125
		private int bodyIndex;

		// Token: 0x0400007E RID: 126
		private int lastAttemptTimeIndex;
	}
}
