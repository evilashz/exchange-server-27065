using System;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.NlpBase.AnnotationStore;
using Microsoft.Ceres.NlpBase.AnnotationStore.Annotations;
using Microsoft.Ceres.NlpBase.RichTypes;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200002B RID: 43
	internal class TokenSerializerProducer : ExchangeProducerBase<TokenSerializerOperator>
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000068CE File Offset: 0x00004ACE
		public TokenSerializerProducer(TokenSerializerOperator tokenSerializerOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(tokenSerializerOperator, inputType, context, ExTraceGlobals.AnnotationTokenTracer)
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000068E0 File Offset: 0x00004AE0
		public override void InternalProcessRecord(IRecord record)
		{
			this.holder.UpdateFrom(record, record.Descriptor.Count);
			if (!((IBooleanField)this.holder[this.shouldBypassTokenSerializerIndex]).BooleanValue)
			{
				ITextualField textualField = (ITextualField)this.holder[this.inputFieldIndex];
				base.Tracer.TraceDebug((long)base.TracingContext, "Creating a TokenInfo object from the textual field.");
				TokenInfo tokenInfo = new TokenInfo(textualField.ContentReader.ReadToEnd());
				using (IAnnotationStoreReader annotationStoreReader = textualField.AnnotationStore.OpenReader())
				{
					foreach (IAnnotation annotation in annotationStoreReader.QueryByType(0, 0))
					{
						AnnotationInfo item = new AnnotationInfo(annotation);
						base.Tracer.TraceFunction<IAnnotation>((long)base.TracingContext, "{0}", annotation);
						tokenInfo.Annotations.Add(item);
					}
				}
				IUpdateableBlobField updateableBlobField = (IUpdateableBlobField)this.holder[this.annotationTokenIndex];
				base.Tracer.TraceDebug((long)base.TracingContext, "Serialize the TokenInfo object to the annotation blob.");
				using (LohFriendlyStream lohFriendlyStream = new LohFriendlyStream(tokenInfo.Text.Length))
				{
					tokenInfo.SerializeTo(lohFriendlyStream);
					updateableBlobField.BlobValue = lohFriendlyStream.ToArray();
				}
			}
			base.SetNextRecord(this.holder);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006A74 File Offset: 0x00004C74
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<TokenSerializerProducer>(this);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006A7C File Offset: 0x00004C7C
		protected override void Initialize()
		{
			base.Initialize();
			IRecordImplDescriptor recordImplDescriptor = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.holder = recordImplDescriptor.CreateUpdatableRecord();
			base.OutputProperties = new RecordSetProperties(base.OutputType, this.holder, recordImplDescriptor);
			this.inputFieldIndex = base.InputType.RecordType.Position(base.OperatorInstance.InputField);
			this.shouldBypassTokenSerializerIndex = base.InputType.RecordType.Position(base.OperatorInstance.ShouldBypassTokenSerializer);
			this.annotationTokenIndex = base.OutputType.RecordType.Position(base.OperatorInstance.AnnotationToken);
		}

		// Token: 0x04000089 RID: 137
		private IUpdateableRecord holder;

		// Token: 0x0400008A RID: 138
		private int inputFieldIndex;

		// Token: 0x0400008B RID: 139
		private int annotationTokenIndex;

		// Token: 0x0400008C RID: 140
		private int shouldBypassTokenSerializerIndex;
	}
}
