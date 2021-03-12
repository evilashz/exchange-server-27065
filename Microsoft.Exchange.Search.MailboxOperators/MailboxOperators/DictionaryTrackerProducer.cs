using System;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000005 RID: 5
	internal class DictionaryTrackerProducer : ExchangeProducerBase<DictionaryTrackerOperator>
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002320 File Offset: 0x00000520
		public DictionaryTrackerProducer(DictionaryTrackerOperator dictionaryTrackerOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(dictionaryTrackerOperator, inputType, context, ExTraceGlobals.DocumentTrackerOperatorTracer)
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002330 File Offset: 0x00000530
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002338 File Offset: 0x00000538
		public int MailboxGuidIndex { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002341 File Offset: 0x00000541
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002349 File Offset: 0x00000549
		public int DatabaseGuidIndex { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002352 File Offset: 0x00000552
		protected override bool StartOfFlow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002355 File Offset: 0x00000555
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DictionaryTrackerProducer>(this);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002360 File Offset: 0x00000560
		public override void InternalProcessRecord(IRecord record)
		{
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			base.Context.PushProperty("MailboxGuid", ((IGuidField)record[this.MailboxGuidIndex]).PrimitiveGuidValue);
			base.Context.PushProperty("DatabaseGuid", ((IGuidField)record[this.DatabaseGuidIndex]).PrimitiveGuidValue);
			base.SetNextRecord(record);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023D8 File Offset: 0x000005D8
		protected override void Initialize()
		{
			base.Initialize();
			this.MailboxGuidIndex = base.InputType.RecordType.Position(base.Operator.MailboxGuid);
			this.DatabaseGuidIndex = base.InputType.RecordType.Position(base.Operator.DatabaseGuid);
		}
	}
}
