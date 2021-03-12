using System;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000112 RID: 274
	public class Assign : Activity
	{
		// Token: 0x06001FBB RID: 8123 RVA: 0x0005F93D File Offset: 0x0005DB3D
		public Assign()
		{
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0005F945 File Offset: 0x0005DB45
		protected Assign(Assign activity) : base(activity)
		{
			this.Variable = activity.Variable;
			this.Value = activity.Value;
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0005F966 File Offset: 0x0005DB66
		public override Activity Clone()
		{
			return new Assign(this);
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x0005F96E File Offset: 0x0005DB6E
		// (set) Token: 0x06001FBF RID: 8127 RVA: 0x0005F976 File Offset: 0x0005DB76
		[DDIMandatoryValue]
		[DDIVariableNameExist]
		public string Variable { get; set; }

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0005F97F File Offset: 0x0005DB7F
		// (set) Token: 0x06001FC1 RID: 8129 RVA: 0x0005F987 File Offset: 0x0005DB87
		[DDIMandatoryValue]
		[DDIValidLambdaExpression]
		public object Value { get; set; }

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0005F990 File Offset: 0x0005DB90
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			RunResult result = new RunResult();
			string text = this.Value as string;
			if (DDIHelper.IsLambdaExpression(text))
			{
				dataTable.Rows[0][this.Variable] = ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(text), typeof(object), DDIHelper.GetLambdaExpressionDataRow(dataTable), input);
			}
			else
			{
				dataTable.Rows[0][this.Variable] = this.Value;
			}
			return result;
		}
	}
}
