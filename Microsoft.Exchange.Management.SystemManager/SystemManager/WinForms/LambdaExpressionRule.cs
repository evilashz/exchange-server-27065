using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000100 RID: 256
	internal abstract class LambdaExpressionRule : IDDIValidator
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00020D80 File Offset: 0x0001EF80
		public List<string> Validate(object target, PageConfigurableProfile profile)
		{
			string text = target as string;
			if (string.IsNullOrEmpty(text))
			{
				return new List<string>();
			}
			return this.OnValidate(text, profile);
		}

		// Token: 0x0600095C RID: 2396
		protected abstract List<string> OnValidate(string lambdaExpression, PageConfigurableProfile profile);
	}
}
