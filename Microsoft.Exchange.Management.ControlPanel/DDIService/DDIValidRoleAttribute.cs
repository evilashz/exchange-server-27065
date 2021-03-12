using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000180 RID: 384
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIValidRoleAttribute : DDIValidateAttribute
	{
		// Token: 0x0600224D RID: 8781 RVA: 0x00067B04 File Offset: 0x00065D04
		static DDIValidRoleAttribute()
		{
			RbacModule.RegisterQueryProcessors();
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00067B15 File Offset: 0x00065D15
		public DDIValidRoleAttribute() : base("DDIValidRoleAttribute")
		{
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x00067B22 File Offset: 0x00065D22
		public override List<string> Validate(object target, Service profile)
		{
			return DDIValidRoleAttribute.rule.Validate(target, profile);
		}

		// Token: 0x04001D6B RID: 7531
		private static ValidRoleRule rule = new ValidRoleRule();
	}
}
