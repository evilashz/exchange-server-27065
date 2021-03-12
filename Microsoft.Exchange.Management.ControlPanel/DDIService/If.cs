using System;
using System.ComponentModel;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200013E RID: 318
	public class If : BranchActivity
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x000640C6 File Offset: 0x000622C6
		public If()
		{
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000640CE File Offset: 0x000622CE
		protected If(If activity) : base(activity)
		{
			this.Condition = activity.Condition;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000640E3 File Offset: 0x000622E3
		public override Activity Clone()
		{
			return new If(this);
		}

		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000640EB File Offset: 0x000622EB
		// (set) Token: 0x06002117 RID: 8471 RVA: 0x000640F3 File Offset: 0x000622F3
		[DefaultValue(null)]
		[DDIValidLambdaExpression]
		public virtual string Condition { get; set; }

		// Token: 0x06002118 RID: 8472 RVA: 0x000640FC File Offset: 0x000622FC
		protected override bool CalculateCondition(DataRow input, DataTable dataTable)
		{
			return (bool)ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(this.Condition), typeof(bool), DDIHelper.GetLambdaExpressionDataRow(dataTable), input);
		}
	}
}
