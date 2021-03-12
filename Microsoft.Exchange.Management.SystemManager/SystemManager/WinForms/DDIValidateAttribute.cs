using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000F4 RID: 244
	public abstract class DDIValidateAttribute : DDIAttribute, IDDIValidator
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x000203EC File Offset: 0x0001E5EC
		public DDIValidateAttribute(string description) : base(description)
		{
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000203F5 File Offset: 0x0001E5F5
		public virtual List<string> Validate(object target, PageConfigurableProfile profile)
		{
			return new List<string>();
		}
	}
}
