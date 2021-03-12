using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000101 RID: 257
	internal class LambdaSeparatorRule : LambdaExpressionRule
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x00020DB4 File Offset: 0x0001EFB4
		protected override List<string> OnValidate(string lambdaExpression, PageConfigurableProfile profile)
		{
			List<string> list = new List<string>();
			string[] array = lambdaExpression.Split(new string[]
			{
				"=>"
			}, StringSplitOptions.None);
			if (array.Length == 1)
			{
				list.Add(string.Format("{0} separator is required for lambda expression {1}", "=>", lambdaExpression));
			}
			else if (array.Length > 2)
			{
				list.Add(string.Format("Multiply {0} separators are found in lambda expression {1}", "=>", lambdaExpression));
			}
			return list;
		}
	}
}
