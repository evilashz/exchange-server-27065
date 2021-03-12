using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000F9 RID: 249
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIDataColumnExistAttribute : DDIValidateAttribute
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x000206D3 File Offset: 0x0001E8D3
		public DDIDataColumnExistAttribute() : base("DDIDataColumnExistAttribute")
		{
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00020700 File Offset: 0x0001E900
		public override List<string> Validate(object target, PageConfigurableProfile profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIDataColumnExistAttribute can only apply to String property");
			}
			string columnName = target as string;
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(columnName) && profile.ColumnProfiles.All((ColumnProfile columnProfile) => !columnProfile.Name.Equals(columnName)))
			{
				list.Add(string.Format("{0} is not a valid column name", target));
			}
			return list;
		}
	}
}
