using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000195 RID: 405
	public sealed class BulkCustomMaximumEventExpectedWorkflow : BulkEditWorkflow
	{
		// Token: 0x060022E4 RID: 8932 RVA: 0x00069480 File Offset: 0x00067680
		public BulkCustomMaximumEventExpectedWorkflow()
		{
			base.Name = "BulkCustomMaximumEventExpected";
			base.ProgressCalculator = typeof(MaximumCountProgressCalculator);
		}

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x000694A3 File Offset: 0x000676A3
		// (set) Token: 0x060022E6 RID: 8934 RVA: 0x000694AB File Offset: 0x000676AB
		[DDIValidLambdaExpression]
		public string MaxProgressBarEventsExpected { get; set; }

		// Token: 0x060022E7 RID: 8935 RVA: 0x000694B4 File Offset: 0x000676B4
		protected override void Initialize(DataRow input, DataTable dataTable)
		{
			base.Initialize(input, dataTable);
			if (string.IsNullOrEmpty(this.MaxProgressBarEventsExpected) || !DDIHelper.IsLambdaExpression(this.MaxProgressBarEventsExpected))
			{
				return;
			}
			MaximumCountProgressCalculator maximumCountProgressCalculator = base.ProgressCalculatorInstance as MaximumCountProgressCalculator;
			if (maximumCountProgressCalculator != null)
			{
				maximumCountProgressCalculator.ProgressRecord.MaxCount = (int)ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(this.MaxProgressBarEventsExpected), typeof(int), DDIHelper.GetLambdaExpressionDataRow(dataTable), input);
				return;
			}
			string message = string.Format("BulkCustomMaximumEventExpectedWorkflow should be used only with MaximumCountProgressCalculator. Current: {0}", (base.ProgressCalculatorInstance == null) ? "<null>" : base.ProgressCalculatorInstance.GetType().Name);
			throw new InvalidOperationException(message);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x00069560 File Offset: 0x00067760
		public override Workflow Clone()
		{
			BulkCustomMaximumEventExpectedWorkflow bulkCustomMaximumEventExpectedWorkflow = new BulkCustomMaximumEventExpectedWorkflow();
			bulkCustomMaximumEventExpectedWorkflow.Name = base.Name;
			bulkCustomMaximumEventExpectedWorkflow.Activities = new Collection<Activity>((from c in base.Activities
			select c.Clone()).ToList<Activity>());
			bulkCustomMaximumEventExpectedWorkflow.MaxProgressBarEventsExpected = this.MaxProgressBarEventsExpected;
			return bulkCustomMaximumEventExpectedWorkflow;
		}
	}
}
