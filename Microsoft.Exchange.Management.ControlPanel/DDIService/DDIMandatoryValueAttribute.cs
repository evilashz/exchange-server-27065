using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000171 RID: 369
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIMandatoryValueAttribute : DDIValidateAttribute
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x00066E76 File Offset: 0x00065076
		public DDIMandatoryValueAttribute() : base("DDIMandatoryValueAttribute")
		{
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00066E84 File Offset: 0x00065084
		public override List<string> Validate(object target, Service profile)
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
