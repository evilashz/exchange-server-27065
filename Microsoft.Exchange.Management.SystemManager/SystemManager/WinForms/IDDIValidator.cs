using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000F3 RID: 243
	public interface IDDIValidator
	{
		// Token: 0x06000934 RID: 2356
		List<string> Validate(object target, PageConfigurableProfile profile);
	}
}
