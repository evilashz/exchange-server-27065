using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000590 RID: 1424
	public class IdentityParameterBinding : QueryStringBinding
	{
		// Token: 0x060041A9 RID: 16809 RVA: 0x000C7DBC File Offset: 0x000C5FBC
		protected override object GetInternalValue()
		{
			object result = null;
			if (!string.IsNullOrEmpty(base.QueryStringValue))
			{
				result = Identity.ParseIdentity(base.QueryStringValue);
			}
			return result;
		}
	}
}
