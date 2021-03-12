using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200017D RID: 381
	internal class LambdaSeparatorRule : LambdaExpressionRule
	{
		// Token: 0x06002246 RID: 8774 RVA: 0x00067848 File Offset: 0x00065A48
		protected override List<string> OnValidate(string lambdaExpression, Service profile)
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
