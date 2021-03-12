using System;
using System.ComponentModel;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200013F RID: 319
	public class IfInRole : BranchActivity
	{
		// Token: 0x06002119 RID: 8473 RVA: 0x00064124 File Offset: 0x00062324
		public IfInRole()
		{
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0006412C File Offset: 0x0006232C
		protected IfInRole(IfInRole activity) : base(activity)
		{
			this.Role = activity.Role;
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x00064141 File Offset: 0x00062341
		public override Activity Clone()
		{
			return new IfInRole(this);
		}

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x00064149 File Offset: 0x00062349
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x00064151 File Offset: 0x00062351
		[DefaultValue(null)]
		[DDIValidRole]
		public string Role { get; set; }

		// Token: 0x0600211E RID: 8478 RVA: 0x0006415C File Offset: 0x0006235C
		protected override bool CalculateCondition(DataRow input, DataTable dataTable)
		{
			string[] array = this.Role.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (base.RbacChecker.IsInRole(array[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000641A4 File Offset: 0x000623A4
		internal override bool HasPermission(DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			if (base.CheckCondition(input, dataTable))
			{
				return base.Then == null || base.Then.HasPermission(input, dataTable, store, updatingVariable);
			}
			return base.Else == null || base.Else.HasPermission(input, dataTable, store, updatingVariable);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000641F0 File Offset: 0x000623F0
		internal override bool? FindAndCheckPermission(Func<Activity, bool> predicate, DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			bool? flag = null;
			bool? flag2 = null;
			bool? result = null;
			if (base.Then != null)
			{
				flag = base.Then.FindAndCheckPermission(predicate, input, dataTable, store, updatingVariable);
			}
			if (base.Else != null)
			{
				flag2 = base.Else.FindAndCheckPermission(predicate, input, dataTable, store, updatingVariable);
			}
			if (base.CheckCondition(input, dataTable))
			{
				result = flag;
				if (result == null && flag2 != null)
				{
					result = new bool?(false);
				}
			}
			else
			{
				result = flag2;
				if (result == null && flag != null)
				{
					result = new bool?(false);
				}
			}
			return result;
		}
	}
}
