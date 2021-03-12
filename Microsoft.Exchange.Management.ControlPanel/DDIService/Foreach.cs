using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Markup;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200012F RID: 303
	[ContentProperty("Activity")]
	public class Foreach : Activity
	{
		// Token: 0x060020BB RID: 8379 RVA: 0x0006330C File Offset: 0x0006150C
		public Foreach()
		{
			base.ErrorBehavior = ErrorBehavior.SilentlyContinue;
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00063328 File Offset: 0x00061528
		protected Foreach(Foreach activity) : base(activity)
		{
			this.Collection = activity.Collection;
			this.FailedCollection = activity.FailedCollection;
			this.Item = activity.Item;
			this.Activity = activity.Activity.Clone();
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0006337D File Offset: 0x0006157D
		public override Activity Clone()
		{
			return new Foreach(this);
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x00063385 File Offset: 0x00061585
		// (set) Token: 0x060020BF RID: 8383 RVA: 0x0006338D File Offset: 0x0006158D
		[DDIMandatoryValue]
		[DDIVariableNameExist]
		public string Collection { get; set; }

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x00063396 File Offset: 0x00061596
		// (set) Token: 0x060020C1 RID: 8385 RVA: 0x0006339E File Offset: 0x0006159E
		[DDIVariableNameExist]
		public string FailedCollection { get; set; }

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x000633A7 File Offset: 0x000615A7
		// (set) Token: 0x060020C3 RID: 8387 RVA: 0x000633AF File Offset: 0x000615AF
		[DDIExtendedVariableName]
		[DDIMandatoryValue]
		public string Item { get; set; }

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x000633B8 File Offset: 0x000615B8
		// (set) Token: 0x060020C5 RID: 8389 RVA: 0x000633C0 File Offset: 0x000615C0
		[DDIMandatoryValue]
		public Activity Activity { get; set; }

		// Token: 0x060020C6 RID: 8390 RVA: 0x000633CC File Offset: 0x000615CC
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			DDIHelper.CheckDataTableForSingleObject(dataTable);
			DataRow dataRow = dataTable.Rows[0];
			RunResult runResult = new RunResult();
			IList<object> list = new List<object>();
			IEnumerable<object> enumerable = DDIHelper.GetVariableValue(store.ModifiedColumns, this.Collection, input, dataTable, store.IsGetListWorkflow) as IEnumerable<object>;
			this.totalItems = enumerable.Count<object>();
			this.executedItemCount = 0;
			foreach (object obj in enumerable)
			{
				dataRow[this.Item] = obj;
				if (this.Activity.IsRunnable(input, dataTable, store))
				{
					RunResult runResult2 = this.Activity.RunCore(input, dataTable, store, codeBehind, updateTableDelegate);
					runResult.DataObjectes.AddRange(runResult2.DataObjectes);
					this.statusReport = this.statusReport.Concat(this.Activity.GetStatusReport(input, dataTable, store)).ToArray<PowerShellResults>();
					if (runResult2.ErrorOccur)
					{
						list.Add(obj);
						if (base.ErrorBehavior == ErrorBehavior.Stop && this.Activity.ErrorBehavior == ErrorBehavior.Stop)
						{
							runResult.ErrorOccur = true;
							break;
						}
					}
				}
				this.executedItemCount++;
			}
			if (!string.IsNullOrEmpty(this.FailedCollection))
			{
				dataRow[this.FailedCollection] = list;
			}
			return runResult;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0006352C File Offset: 0x0006172C
		public override List<DataColumn> GetExtendedColumns()
		{
			List<DataColumn> list = new List<DataColumn>();
			list.Add(new DataColumn(this.Item, typeof(object)));
			list.AddRange(this.Activity.GetExtendedColumns());
			return list;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x0006356C File Offset: 0x0006176C
		public override bool IsRunnable(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			if (!base.IsRunnable(input, dataTable, store))
			{
				return false;
			}
			DDIHelper.CheckDataTableForSingleObject(dataTable);
			object variableValue = DDIHelper.GetVariableValue(store.ModifiedColumns, this.Collection, input, dataTable, store.IsGetListWorkflow);
			return variableValue is IEnumerable<object> && (variableValue as IEnumerable<object>).Count<object>() > 0;
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000635BE File Offset: 0x000617BE
		public override PowerShellResults[] GetStatusReport(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			return this.statusReport;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000635C6 File Offset: 0x000617C6
		internal override IEnumerable<Activity> Find(Func<Activity, bool> predicate)
		{
			return this.Activity.Find(predicate);
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000635D4 File Offset: 0x000617D4
		internal override bool? FindAndCheckPermission(Func<Activity, bool> predicate, DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			Variable variable = updatingVariable;
			if (this.Collection.Equals(updatingVariable.Name, StringComparison.OrdinalIgnoreCase))
			{
				variable = updatingVariable.ShallowClone();
				variable.Name = this.Item;
				variable.MappingProperty = this.Item;
			}
			return this.Activity.FindAndCheckPermission(predicate, input, dataTable, store, variable);
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x0006362A File Offset: 0x0006182A
		internal override int ProgressPercent
		{
			get
			{
				return ProgressCalculatorBase.CalculatePercentageHelper(base.ProgressPercent, this.executedItemCount, this.totalItems, this.Activity);
			}
		}

		// Token: 0x04001CF8 RID: 7416
		private PowerShellResults[] statusReport = new PowerShellResults[0];

		// Token: 0x04001CF9 RID: 7417
		private int executedItemCount;

		// Token: 0x04001CFA RID: 7418
		private int totalItems;
	}
}
