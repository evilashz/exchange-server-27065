using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000103 RID: 259
	internal class LambdaExpressionCompilableRule : LambdaExpressionRule
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x00020F48 File Offset: 0x0001F148
		protected override List<string> OnValidate(string lambdaExpression, PageConfigurableProfile profile)
		{
			List<string> list = new List<string>();
			try
			{
				ColumnExpression expression = ExpressionCalculator.BuildColumnExpression(lambdaExpression);
				DataTable dataTable = new DataTable();
				DataRow dataRow = dataTable.NewRow();
				ExpressionCalculator.CompileLambdaExpression(expression, typeof(object), dataRow, null);
			}
			catch (Exception ex)
			{
				list.Add(string.Format("Fail to compile lambda expression {0} with error {1}", lambdaExpression, ex.ToString()));
			}
			return list;
		}
	}
}
