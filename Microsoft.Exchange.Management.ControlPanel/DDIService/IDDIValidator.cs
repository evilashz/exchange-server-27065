using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000165 RID: 357
	public interface IDDIValidator
	{
		// Token: 0x060021FF RID: 8703
		List<string> Validate(object target, Service profile);
	}
}
