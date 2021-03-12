using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000167 RID: 359
	public interface IDDIHasArgumentValidator
	{
		// Token: 0x06002202 RID: 8706
		List<string> ValidateWithArg(object target, Service profile, Dictionary<string, string> arguments);
	}
}
