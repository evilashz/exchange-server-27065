using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200017B RID: 379
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIValidLambdaExpressionAttribute : DDIValidateAttribute
	{
		// Token: 0x06002240 RID: 8768 RVA: 0x00067752 File Offset: 0x00065952
		public DDIValidLambdaExpressionAttribute() : base("DDIValidLambdaExpressionAttribute")
		{
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00067760 File Offset: 0x00065960
		public override List<string> Validate(object target, Service profile)
		{
			List<string> list = new List<string>();
			string value = target as string;
			if (DDIHelper.IsLambdaExpression(value))
			{
				foreach (LambdaExpressionRule lambdaExpressionRule in DDIValidLambdaExpressionAttribute.rules)
				{
					list.AddRange(lambdaExpressionRule.Validate(target, profile));
					if (list.Count > 0)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x04001D6A RID: 7530
		private static List<LambdaExpressionRule> rules = new List<LambdaExpressionRule>
		{
			new LambdaSeparatorRule(),
			new DependentColumnExistRule(),
			new LambdaExpressionCompilableRule()
		};
	}
}
