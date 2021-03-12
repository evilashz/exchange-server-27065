using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Markup;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000152 RID: 338
	[ContentProperty("Body")]
	public class Sequence : Activity
	{
		// Token: 0x0600217F RID: 8575 RVA: 0x00064D14 File Offset: 0x00062F14
		public Sequence()
		{
			this.Body = new Collection<Activity>();
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x00064D3C File Offset: 0x00062F3C
		protected Sequence(Sequence activity) : base(activity)
		{
			this.Body = new Collection<Activity>((from c in activity.Body
			select c.Clone()).ToList<Activity>());
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00064D94 File Offset: 0x00062F94
		public override Activity Clone()
		{
			return new Sequence(this);
		}

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x00064D9C File Offset: 0x00062F9C
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x00064DA4 File Offset: 0x00062FA4
		[DDIMandatoryValue]
		public Collection<Activity> Body { get; internal set; }

		// Token: 0x06002184 RID: 8580 RVA: 0x00064DB0 File Offset: 0x00062FB0
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			RunResult runResult = new RunResult();
			this.activitiesExecutedCount = 0;
			foreach (Activity activity in this.Body)
			{
				if (activity.IsRunnable(input, dataTable, store))
				{
					this.CurrentExecutingActivity = activity;
					RunResult runResult2 = activity.RunCore(input, dataTable, store, codeBehind, updateTableDelegate);
					this.statusReport = this.statusReport.Concat(activity.GetStatusReport(input, dataTable, store)).ToArray<PowerShellResults>();
					runResult.DataObjectes.AddRange(runResult2.DataObjectes);
					if (runResult2.ErrorOccur && base.ErrorBehavior == ErrorBehavior.Stop && activity.ErrorBehavior == ErrorBehavior.Stop)
					{
						runResult.ErrorOccur = true;
						break;
					}
				}
				this.activitiesExecutedCount++;
			}
			this.CurrentExecutingActivity = null;
			return runResult;
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x00064E90 File Offset: 0x00063090
		public override PowerShellResults[] GetStatusReport(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			return this.statusReport;
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x00064E98 File Offset: 0x00063098
		public override List<DataColumn> GetExtendedColumns()
		{
			List<DataColumn> list = new List<DataColumn>();
			foreach (Activity activity in this.Body)
			{
				list.AddRange(activity.GetExtendedColumns());
			}
			return list;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x00064F0C File Offset: 0x0006310C
		internal override IEnumerable<Activity> Find(Func<Activity, bool> predicate)
		{
			return this.Body.SelectMany((Activity c) => c.Find(predicate));
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x00064F40 File Offset: 0x00063140
		internal override bool? FindAndCheckPermission(Func<Activity, bool> predicate, DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			bool? newVal = null;
			bool? flag = null;
			foreach (Activity activity in this.Body)
			{
				newVal = activity.FindAndCheckPermission(predicate, input, dataTable, store, updatingVariable);
				flag = flag.Or(newVal);
				if (flag.IsTrue())
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x00064FBC File Offset: 0x000631BC
		internal override int ProgressPercent
		{
			get
			{
				return ProgressCalculatorBase.CalculatePercentageHelper(base.ProgressPercent, this.activitiesExecutedCount, this.Body.Count, this.CurrentExecutingActivity);
			}
		}

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x00064FE0 File Offset: 0x000631E0
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x00064FE8 File Offset: 0x000631E8
		protected Activity CurrentExecutingActivity { get; set; }

		// Token: 0x04001D31 RID: 7473
		private PowerShellResults[] statusReport = new PowerShellResults[0];

		// Token: 0x04001D32 RID: 7474
		private int activitiesExecutedCount;
	}
}
