using System;
using System.Data;
using System.Linq;
using System.Windows.Markup;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000153 RID: 339
	[ContentProperty("Body")]
	[DDIIsValidPipelineInnerActivity]
	public class Pipeline : Sequence
	{
		// Token: 0x0600218D RID: 8589 RVA: 0x00064FF1 File Offset: 0x000631F1
		public Pipeline()
		{
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00065005 File Offset: 0x00063205
		protected Pipeline(Pipeline activity) : base(activity)
		{
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0006501A File Offset: 0x0006321A
		public override Activity Clone()
		{
			return new Pipeline(this);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x00065024 File Offset: 0x00063224
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			RunResult runResult = new RunResult();
			IPSCommandWrapper ipscommandWrapper = base.PowershellFactory.CreatePSCommand();
			Activity activity = null;
			foreach (Activity activity2 in base.Body)
			{
				((CmdletActivity)activity2).BuildCommand(input, dataTable, store, codeBehind);
				ipscommandWrapper.AddCommand(((CmdletActivity)activity2).Command.Commands[0]);
				activity = activity2;
			}
			if (activity != null)
			{
				base.CurrentExecutingActivity = activity;
				((CmdletActivity)activity).Command = ipscommandWrapper;
				RunResult runResult2 = activity.Run(input, dataTable, store, codeBehind, updateTableDelegate);
				runResult.DataObjectes.AddRange(runResult2.DataObjectes);
				this.statusReport = this.statusReport.Concat(activity.GetStatusReport(input, dataTable, store)).ToArray<PowerShellResults>();
				base.CurrentExecutingActivity = null;
				if (runResult2.ErrorOccur && base.ErrorBehavior == ErrorBehavior.Stop && activity.ErrorBehavior == ErrorBehavior.Stop)
				{
					runResult.ErrorOccur = true;
					return runResult;
				}
			}
			return runResult;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00065134 File Offset: 0x00063334
		public override PowerShellResults[] GetStatusReport(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			return this.statusReport;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0006513C File Offset: 0x0006333C
		public override bool IsRunnable(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			if (!base.IsRunnable(input, dataTable, store))
			{
				return false;
			}
			for (int i = 0; i < base.Body.Count; i++)
			{
				Activity activity = base.Body[i];
				if (i < base.Body.Count - 1 && !(activity is OutputObjectCmdlet) && !(activity is GetListCmdlet))
				{
					return false;
				}
				if (!string.IsNullOrEmpty(((CmdletActivity)activity).PreAction) || !string.IsNullOrEmpty(((CmdletActivity)activity).PostAction))
				{
					return false;
				}
				if (!activity.IsRunnable(input, dataTable, store))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x000651CF File Offset: 0x000633CF
		internal override int ProgressPercent
		{
			get
			{
				if (base.CurrentExecutingActivity != null)
				{
					return base.CurrentExecutingActivity.ProgressPercent;
				}
				return base.ProgressPercent;
			}
		}

		// Token: 0x04001D36 RID: 7478
		private PowerShellResults[] statusReport = new PowerShellResults[0];
	}
}
