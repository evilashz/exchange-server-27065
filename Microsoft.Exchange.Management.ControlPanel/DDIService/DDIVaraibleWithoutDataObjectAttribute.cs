using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000182 RID: 386
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIVaraibleWithoutDataObjectAttribute : DDIValidateAttribute
	{
		// Token: 0x06002252 RID: 8786 RVA: 0x00067BE0 File Offset: 0x00065DE0
		public DDIVaraibleWithoutDataObjectAttribute() : base("DDIVaraibleWithoutDataObjectAttribute ")
		{
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00067C1C File Offset: 0x00065E1C
		public override List<string> Validate(object target, Service profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIVaraibleWithoutDataObjectAttribute  can only apply to String property");
			}
			string columnName = target as string;
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(columnName) && profile.Variables.Any((Variable columnProfile) => columnProfile.Name.Equals(columnName) && !string.IsNullOrWhiteSpace(columnProfile.DataObjectName)))
			{
				list.Add(string.Format("OutputVariable must refer to Variable which don't specify value for the property DataObjectName.", target));
			}
			return list;
		}
	}
}
