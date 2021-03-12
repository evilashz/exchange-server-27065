using System;
using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000645 RID: 1605
	public class RegexExpressionBuilder : ExpressionBuilder
	{
		// Token: 0x17002710 RID: 10000
		// (get) Token: 0x06004639 RID: 17977 RVA: 0x000D450D File Offset: 0x000D270D
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x000D4510 File Offset: 0x000D2710
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return CommonRegex.GetRegexExpressionById(entry.Expression.Trim()).ToString();
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x000D4527 File Offset: 0x000D2727
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(CommonRegex)), entry.Expression.Trim()), "ToString", new CodeExpression[0]);
		}
	}
}
