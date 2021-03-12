using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A35 RID: 2613
	internal class RestrictedExpression : ValidationRuleExpression
	{
		// Token: 0x06007819 RID: 30745 RVA: 0x0018B9B7 File Offset: 0x00189BB7
		public RestrictedExpression(string queryFilter, ObjectSchema schema, List<Type> applicableObjects) : base(queryFilter, schema, applicableObjects)
		{
		}
	}
}
