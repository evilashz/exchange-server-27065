using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000FA RID: 250
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIMandatoryValueAttribute : DDIValidateAttribute
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x00020773 File Offset: 0x0001E973
		public DDIMandatoryValueAttribute() : base("DDIMandatoryValueAttribute")
		{
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00020780 File Offset: 0x0001E980
		public override List<string> Validate(object target, PageConfigurableProfile profile)
		{
			List<string> list = new List<string>();
			if (WinformsHelper.IsEmptyValue(target))
			{
				list.Add("cannot be null or empty");
			}
			return list;
		}
	}
}
