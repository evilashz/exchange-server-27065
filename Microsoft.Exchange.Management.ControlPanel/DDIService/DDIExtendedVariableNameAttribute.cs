using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000184 RID: 388
	public class DDIExtendedVariableNameAttribute : DDIValidateAttribute
	{
		// Token: 0x06002256 RID: 8790 RVA: 0x00067D6A File Offset: 0x00065F6A
		public DDIExtendedVariableNameAttribute() : base("VariableNameIsExtended")
		{
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00067DB0 File Offset: 0x00065FB0
		public override List<string> Validate(object target, Service profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIExtendedVariableNameAttribute can only apply to String property");
			}
			string columnName = target as string;
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(columnName))
			{
				if (profile.Variables.Any((Variable columnProfile) => columnProfile.Name.Equals(columnName)))
				{
					list.Add(string.Format("{0} is extended column which cannot be explicitly declare in XAML file", target));
				}
				else if (profile.ExtendedColumns.All((DataColumn dataColumn) => dataColumn.ColumnName != columnName))
				{
					list.Add(string.Format("{0} doesn't exist in extended columns list {1}", target, string.Join(",", (from dataColumn in profile.ExtendedColumns
					select dataColumn.ColumnName).ToArray<string>())));
				}
			}
			return list;
		}
	}
}
