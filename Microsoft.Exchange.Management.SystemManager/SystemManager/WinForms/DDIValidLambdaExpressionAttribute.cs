using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000FF RID: 255
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIValidLambdaExpressionAttribute : DDIValidateAttribute
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x00020CB6 File Offset: 0x0001EEB6
		public DDIValidLambdaExpressionAttribute() : base("DDIValidLambdaExpressionAttribute")
		{
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00020CC4 File Offset: 0x0001EEC4
		public override List<string> Validate(object target, PageConfigurableProfile profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIValidLambdaExpressionAttribute can only apply to String property");
			}
			List<string> list = new List<string>();
			foreach (LambdaExpressionRule lambdaExpressionRule in DDIValidLambdaExpressionAttribute.rules)
			{
				list.AddRange(lambdaExpressionRule.Validate(target, profile));
				if (list.Count > 0)
				{
					break;
				}
			}
			return list;
		}

		// Token: 0x04000410 RID: 1040
		private static List<LambdaExpressionRule> rules = new List<LambdaExpressionRule>
		{
			new LambdaSeparatorRule(),
			new DependentColumnExistRule(),
			new LambdaExpressionCompilableRule()
		};
	}
}
