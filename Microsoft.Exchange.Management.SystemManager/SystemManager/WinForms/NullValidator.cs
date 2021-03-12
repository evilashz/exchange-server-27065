using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000144 RID: 324
	public sealed class NullValidator : IValidator
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x0002FAD7 File Offset: 0x0002DCD7
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}
	}
}
