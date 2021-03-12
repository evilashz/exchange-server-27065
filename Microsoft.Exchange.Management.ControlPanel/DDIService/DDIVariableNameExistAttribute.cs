using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000183 RID: 387
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIVariableNameExistAttribute : DDIValidateAttribute
	{
		// Token: 0x06002254 RID: 8788 RVA: 0x00067C8F File Offset: 0x00065E8F
		public DDIVariableNameExistAttribute() : base("DDIVariableNameExistAttribute")
		{
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00067CD0 File Offset: 0x00065ED0
		public override List<string> Validate(object target, Service profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIVariableNameExistAttribute can only apply to String property");
			}
			string columnName = target as string;
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(columnName))
			{
				if (profile.Variables.All((Variable columnProfile) => !columnProfile.Name.Equals(columnName)))
				{
					if (profile.ExtendedColumns.All((DataColumn dataColumn) => dataColumn.ColumnName != columnName))
					{
						list.Add(string.Format("{0} is not a valid variable name", target));
					}
				}
			}
			return list;
		}
	}
}
