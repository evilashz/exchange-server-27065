using System;
using System.ComponentModel;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000196 RID: 406
	public class GetListWorkflow : Workflow
	{
		// Token: 0x060022EA RID: 8938 RVA: 0x000695C4 File Offset: 0x000677C4
		public GetListWorkflow()
		{
			base.Name = "GetList";
			this.ResultSize = (DDIHelper.IsGetListAsync ? DDIHelper.GetListAsyncModeResultSize.ToString() : DDIHelper.GetListDefaultResultSize.ToString());
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x00069612 File Offset: 0x00067812
		protected GetListWorkflow(GetListWorkflow workflow) : base(workflow)
		{
			this.ResultSize = workflow.ResultSize;
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0006962E File Offset: 0x0006782E
		public override Workflow Clone()
		{
			return new GetListWorkflow(this);
		}

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x00069636 File Offset: 0x00067836
		// (set) Token: 0x060022EE RID: 8942 RVA: 0x0006963E File Offset: 0x0006783E
		[DefaultValue("=>IIF(!DDIHelper.IsGetListAsync, DDIHelper.GetListDefaultResultSize, DDIHelper.GetListAsyncModeResultSize)")]
		[DDIValidLambdaExpression]
		public string ResultSize { get; set; }

		// Token: 0x060022EF RID: 8943 RVA: 0x00069648 File Offset: 0x00067848
		public int GetResultSizeInt32(DataRow input, DataTable dataTable)
		{
			if (this.resultSizeInt32 == -1 && !string.IsNullOrEmpty(this.ResultSize))
			{
				int num = -1;
				if (DDIHelper.IsLambdaExpression(this.ResultSize))
				{
					num = (int)ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(this.ResultSize), typeof(int), DDIHelper.GetLambdaExpressionDataRow(dataTable), input);
				}
				else
				{
					int.TryParse(this.ResultSize, out num);
				}
				this.resultSizeInt32 = num;
			}
			return this.resultSizeInt32;
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000696C0 File Offset: 0x000678C0
		protected override void Initialize(DataRow input, DataTable dataTable)
		{
			int resultSize = this.GetResultSizeInt32(input, dataTable);
			foreach (Activity activity in base.Activities)
			{
				activity.SetResultSize(resultSize);
			}
		}

		// Token: 0x04001DA6 RID: 7590
		private int resultSizeInt32 = -1;
	}
}
