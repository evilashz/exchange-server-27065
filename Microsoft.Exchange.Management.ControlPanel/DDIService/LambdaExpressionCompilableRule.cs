using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Management.SystemManager;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200017F RID: 383
	internal class LambdaExpressionCompilableRule : LambdaExpressionRule
	{
		// Token: 0x0600224B RID: 8779 RVA: 0x00067A5C File Offset: 0x00065C5C
		protected override List<string> OnValidate(string lambdaExpression, Service profile)
		{
			List<string> list = new List<string>();
			try
			{
				lambdaExpression = lambdaExpression.Replace("@1", "@0");
				ColumnExpression expression = ExpressionCalculator.BuildColumnExpression(lambdaExpression);
				using (DataTable dataTable = new DataTable())
				{
					DataRow dataRow = dataTable.NewRow();
					ExpressionCalculator.CompileLambdaExpression(expression, typeof(object), profile.PredefinedTypes.ToArray(), dataRow, null);
				}
			}
			catch (Exception ex)
			{
				list.Add(string.Format("Fail to compile lambda expression {0} with error {1}. \r\n If the error is about no applicable method, please also double check you do type cast explicitly in your lambdaexpression, like SearchText=>DDIHelper.AsteriskAround(String(@1[SearchText])) ", lambdaExpression, ex.ToString()));
			}
			return list;
		}
	}
}
