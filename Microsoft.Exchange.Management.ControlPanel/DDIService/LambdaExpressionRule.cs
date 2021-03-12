using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200017C RID: 380
	internal abstract class LambdaExpressionRule : IDDIValidator
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x00067814 File Offset: 0x00065A14
		public List<string> Validate(object target, Service profile)
		{
			string text = target as string;
			if (string.IsNullOrEmpty(text))
			{
				return new List<string>();
			}
			return this.OnValidate(text, profile);
		}

		// Token: 0x06002244 RID: 8772
		protected abstract List<string> OnValidate(string lambdaExpression, Service profile);
	}
}
