using System;
using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000667 RID: 1639
	[ExpressionPrefix("Strings")]
	[ExpressionEditor(typeof(StringsExpressionEditor))]
	internal class StringsExpressionBuilder : ExpressionBuilder
	{
		// Token: 0x17002761 RID: 10081
		// (get) Token: 0x06004737 RID: 18231 RVA: 0x000D80D9 File Offset: 0x000D62D9
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x000D80DC File Offset: 0x000D62DC
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			StringsExpressionEditorSheet stringsExpressionEditorSheet = (StringsExpressionEditorSheet)parsedData;
			return stringsExpressionEditorSheet.Evaluate();
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x000D80F8 File Offset: 0x000D62F8
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			StringsExpressionEditorSheet stringsExpressionEditorSheet = (StringsExpressionEditorSheet)parsedData;
			return new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(stringsExpressionEditorSheet.Group.StringsType), stringsExpressionEditorSheet.StringID), "ToString", new CodeExpression[0]);
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x000D8137 File Offset: 0x000D6337
		public override object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
		{
			return new StringsExpressionEditorSheet(expression, null);
		}
	}
}
