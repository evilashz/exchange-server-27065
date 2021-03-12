using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Markup;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000116 RID: 278
	[ContentProperty("Then")]
	public abstract class BranchActivity : Activity
	{
		// Token: 0x06001FD4 RID: 8148 RVA: 0x0005FCB3 File Offset: 0x0005DEB3
		public BranchActivity()
		{
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0005FCBB File Offset: 0x0005DEBB
		protected BranchActivity(BranchActivity activity) : base(activity)
		{
			if (activity.Then != null)
			{
				this.Then = activity.Then.Clone();
			}
			if (activity.Else != null)
			{
				this.Else = activity.Else.Clone();
			}
		}

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x0005FCF6 File Offset: 0x0005DEF6
		// (set) Token: 0x06001FD7 RID: 8151 RVA: 0x0005FCFE File Offset: 0x0005DEFE
		public Activity Then { get; set; }

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x0005FD07 File Offset: 0x0005DF07
		// (set) Token: 0x06001FD9 RID: 8153 RVA: 0x0005FD0F File Offset: 0x0005DF0F
		public Activity Else { get; set; }

		// Token: 0x06001FDA RID: 8154 RVA: 0x0005FD18 File Offset: 0x0005DF18
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			RunResult result = new RunResult();
			if (this.CheckCondition(input, dataTable) && this.Then.IsRunnable(input, dataTable, store))
			{
				this.currentExecutingActivity = this.Then;
				result = this.Then.RunCore(input, dataTable, store, codeBehind, updateTableDelegate);
			}
			else if (this.Else != null && this.Else.IsRunnable(input, dataTable, store))
			{
				this.currentExecutingActivity = this.Else;
				result = this.Else.RunCore(input, dataTable, store, codeBehind, updateTableDelegate);
			}
			this.currentExecutingActivity = null;
			return result;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0005FDA5 File Offset: 0x0005DFA5
		public override bool IsRunnable(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			this.cachedCondtion = null;
			return base.IsRunnable(input, dataTable, store);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0005FDBC File Offset: 0x0005DFBC
		public override PowerShellResults[] GetStatusReport(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			if (this.CheckCondition(input, dataTable))
			{
				return this.Then.GetStatusReport(input, dataTable, store);
			}
			if (this.Else != null)
			{
				return this.Else.GetStatusReport(input, dataTable, store);
			}
			return new PowerShellResults[0];
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0005FDF4 File Offset: 0x0005DFF4
		public override List<DataColumn> GetExtendedColumns()
		{
			List<DataColumn> list = new List<DataColumn>();
			if (this.Then != null)
			{
				list.AddRange(this.Then.GetExtendedColumns());
			}
			if (this.Else != null)
			{
				list.AddRange(this.Else.GetExtendedColumns());
			}
			return list;
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0005FE3C File Offset: 0x0005E03C
		internal override IEnumerable<Activity> Find(Func<Activity, bool> predicate)
		{
			List<Activity> list = new List<Activity>();
			if (this.cachedCondtion == null)
			{
				if (this.Then != null)
				{
					list.AddRange(this.Then.Find(predicate));
				}
				if (this.Else != null)
				{
					list.AddRange(this.Else.Find(predicate));
				}
			}
			else if (this.cachedCondtion.IsTrue())
			{
				list.AddRange(this.Then.Find(predicate));
			}
			else if (this.Else != null)
			{
				list.AddRange(this.Else.Find(predicate));
			}
			return list;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0005FED4 File Offset: 0x0005E0D4
		internal override bool? FindAndCheckPermission(Func<Activity, bool> predicate, DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			bool? newVal = null;
			bool? oldVal = null;
			bool? flag = null;
			if (this.Then != null)
			{
				newVal = this.Then.FindAndCheckPermission(predicate, input, dataTable, store, updatingVariable);
			}
			if (this.Else != null)
			{
				oldVal = this.Else.FindAndCheckPermission(predicate, input, dataTable, store, updatingVariable);
			}
			return oldVal.Or(newVal);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0005FF38 File Offset: 0x0005E138
		protected bool CheckCondition(DataRow input, DataTable dataTable)
		{
			if (this.cachedCondtion == null)
			{
				this.cachedCondtion = new bool?(this.CalculateCondition(input, dataTable));
			}
			return this.cachedCondtion.Value;
		}

		// Token: 0x06001FE1 RID: 8161
		protected abstract bool CalculateCondition(DataRow input, DataTable dataTable);

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0005FF65 File Offset: 0x0005E165
		internal override int ProgressPercent
		{
			get
			{
				if (this.currentExecutingActivity == null)
				{
					return base.ProgressPercent;
				}
				return this.currentExecutingActivity.ProgressPercent;
			}
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0005FF81 File Offset: 0x0005E181
		internal override void SetResultSize(int resultSize)
		{
			this.Then.SetResultSize(resultSize);
			if (this.Else != null)
			{
				this.Else.SetResultSize(resultSize);
			}
		}

		// Token: 0x04001C96 RID: 7318
		private bool? cachedCondtion;

		// Token: 0x04001C97 RID: 7319
		private Activity currentExecutingActivity;
	}
}
