using System;
using System.Web.UI.Design;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000668 RID: 1640
	internal class StringsExpressionEditor : ExpressionEditor
	{
		// Token: 0x0600473C RID: 18236 RVA: 0x000D8148 File Offset: 0x000D6348
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			return new StringsExpressionEditorSheet(expression, serviceProvider).Evaluate();
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x000D8157 File Offset: 0x000D6357
		public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			return new StringsExpressionEditorSheet(expression, serviceProvider);
		}
	}
}
