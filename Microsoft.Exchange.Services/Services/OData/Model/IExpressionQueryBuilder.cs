using System;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E8E RID: 3726
	internal interface IExpressionQueryBuilder
	{
		// Token: 0x0600611B RID: 24859
		MemberExpression GetQueryPropertyExpression();

		// Token: 0x0600611C RID: 24860
		ConstantExpression GetQueryConstant(object value);
	}
}
