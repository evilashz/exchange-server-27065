using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A36 RID: 2614
	internal class OverridingAllowExpression : ValidationRuleExpression
	{
		// Token: 0x0600781A RID: 30746 RVA: 0x0018B9C2 File Offset: 0x00189BC2
		public OverridingAllowExpression(string queryFilter, ObjectSchema schema, List<Type> applicableObjects) : base(queryFilter, schema, applicableObjects)
		{
		}
	}
}
