using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Management.Automation;
using System.Web.UI.WebControls;
using System.Windows.Markup;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200018C RID: 396
	[ContentProperty("Activities")]
	[DDIWorkflow]
	public class Workflow
	{
		// Token: 0x0600229A RID: 8858 RVA: 0x00068753 File Offset: 0x00066953
		public Workflow()
		{
			this.ProgressCalculator = typeof(SingleOperationProgressCalculator);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x00068780 File Offset: 0x00066980
		protected Workflow(Workflow workflow)
		{
			this.Name = workflow.Name;
			this.AsyncMode = workflow.AsyncMode;
			this.AsyncRunning = workflow.AsyncRunning;
			this.ProgressCalculator = workflow.ProgressCalculator;
			this.Output = workflow.Output;
			this.Activities = new Collection<Activity>((from c in workflow.Activities
			select c.Clone()).ToList<Activity>());
			this.OutputOnError = workflow.OutputOnError;
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0006881E File Offset: 0x00066A1E
		public virtual Workflow Clone()
		{
			return new Workflow(this);
		}

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x00068826 File Offset: 0x00066A26
		// (set) Token: 0x0600229E RID: 8862 RVA: 0x0006882E File Offset: 0x00066A2E
		[DDIMandatoryValue]
		public string Name { get; set; }

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x00068837 File Offset: 0x00066A37
		// (set) Token: 0x060022A0 RID: 8864 RVA: 0x0006883F File Offset: 0x00066A3F
		[DefaultValue(false)]
		public bool AsyncRunning { get; set; }

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x00068848 File Offset: 0x00066A48
		// (set) Token: 0x060022A2 RID: 8866 RVA: 0x00068850 File Offset: 0x00066A50
		[DefaultValue(AsyncMode.SynchronousOnly)]
		public AsyncMode AsyncMode { get; set; }

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x00068859 File Offset: 0x00066A59
		// (set) Token: 0x060022A4 RID: 8868 RVA: 0x00068861 File Offset: 0x00066A61
		[DefaultValue(typeof(SingleOperationProgressCalculator))]
		public Type ProgressCalculator { get; set; }

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x0006886A File Offset: 0x00066A6A
		// (set) Token: 0x060022A6 RID: 8870 RVA: 0x00068872 File Offset: 0x00066A72
		[DDICharSeparatorItems(AttributeType = typeof(DDIVariableNameExistAttribute), Separators = " ,")]
		[DefaultValue(null)]
		public virtual string Output { get; set; }

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x0006887B File Offset: 0x00066A7B
		// (set) Token: 0x060022A8 RID: 8872 RVA: 0x00068883 File Offset: 0x00066A83
		[TypeConverter(typeof(StringArrayConverter))]
		[DefaultValue(null)]
		[DDICollectionDecorator(AttributeType = typeof(DDIVariableNameExistAttribute))]
		public string[] OutputOnError
		{
			get
			{
				return this.outputOnError;
			}
			set
			{
				this.outputOnError = value;
			}
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0006888C File Offset: 0x00066A8C
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x00068894 File Offset: 0x00066A94
		public Collection<Activity> Activities
		{
			get
			{
				return this.activities;
			}
			internal set
			{
				this.activities = value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060022AB RID: 8875 RVA: 0x000688A0 File Offset: 0x00066AA0
		// (remove) Token: 0x060022AC RID: 8876 RVA: 0x000688D8 File Offset: 0x00066AD8
		public event EventHandler<ProgressReportEventArgs> ProgressChanged;

		// Token: 0x060022AD RID: 8877 RVA: 0x00068910 File Offset: 0x00066B10
		public IList<string> GetOutputVariables()
		{
			if (this.Output == null)
			{
				return null;
			}
			IList<string> list = new List<string>();
			if (!string.IsNullOrEmpty(this.Output))
			{
				string[] array = this.Output.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					if (!string.IsNullOrEmpty(text2))
					{
						list.Add(text2);
					}
				}
			}
			return list;
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x00068989 File Offset: 0x00066B89
		protected virtual void Initialize(DataRow input, DataTable dataTable)
		{
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x00068998 File Offset: 0x00066B98
		public PowerShellResults Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			DDIHelper.Trace("Executing workflow: {0} {1}", new object[]
			{
				base.GetType().Name,
				this.Name
			});
			this.Initialize(input, dataTable);
			List<PowerShellResults> list = new List<PowerShellResults>();
			this.activitiesExecutedCount = 0;
			bool forGetListProgress = DDIHelper.ForGetListProgress;
			foreach (Activity activity in this.Activities)
			{
				if (activity.IsRunnable(input, dataTable, store))
				{
					this.currentExecutingActivity = activity;
					if (!forGetListProgress && this.AsyncRunning)
					{
						foreach (Activity activity2 in activity.Find((Activity x) => x is CmdletActivity))
						{
							CmdletActivity cmdletActivity = (CmdletActivity)activity2;
							cmdletActivity.PSProgressChanged += this.Activity_ProgressChanged;
						}
					}
					RunResult runResult = activity.RunCore(input, dataTable, store, codeBehind, updateTableDelegate);
					list.AddRange(activity.GetStatusReport(input, dataTable, store));
					if (runResult.ErrorOccur && activity.ErrorBehavior == ErrorBehavior.Stop)
					{
						break;
					}
				}
				this.activitiesExecutedCount++;
			}
			PowerShellResults powerShellResults = forGetListProgress ? new PowerShellResults<JsonDictionary<object>>() : new PowerShellResults();
			foreach (PowerShellResults powerShellResults2 in list)
			{
				powerShellResults.MergeErrors(powerShellResults2);
				if (forGetListProgress)
				{
					((PowerShellResults<JsonDictionary<object>>)powerShellResults).MergeProgressData<PSObject>(powerShellResults2 as PowerShellResults<PSObject>);
				}
			}
			return powerShellResults;
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00068B68 File Offset: 0x00066D68
		public int ProgressPercent
		{
			get
			{
				return ProgressCalculatorBase.CalculatePercentageHelper(100, this.activitiesExecutedCount, this.Activities.Count, this.currentExecutingActivity);
			}
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x00068B88 File Offset: 0x00066D88
		private void Activity_ProgressChanged(object sender, ProgressReportEventArgs e)
		{
			if (this.ProgressChanged != null)
			{
				e.Percent = this.ProgressPercent;
				this.ProgressChanged(this, e);
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x00068BAB File Offset: 0x00066DAB
		internal virtual void LoadMetaData(DataRow input, DataTable dataTable, DataObjectStore store, IList<string> outputVariables, out Dictionary<string, ValidatorInfo[]> validators, out IList<string> readOnlyProperties, out IList<string> notAccessProperties, Service service)
		{
			validators = new Dictionary<string, ValidatorInfo[]>();
			readOnlyProperties = new List<string>();
			notAccessProperties = new List<string>();
		}

		// Token: 0x17001AAF RID: 6831
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x00068BC5 File Offset: 0x00066DC5
		// (set) Token: 0x060022B4 RID: 8884 RVA: 0x00068BCD File Offset: 0x00066DCD
		public ProgressCalculatorBase ProgressCalculatorInstance { get; set; }

		// Token: 0x04001D8E RID: 7566
		private Collection<Activity> activities = new Collection<Activity>();

		// Token: 0x04001D8F RID: 7567
		private Activity currentExecutingActivity;

		// Token: 0x04001D90 RID: 7568
		private int activitiesExecutedCount;

		// Token: 0x04001D91 RID: 7569
		private string[] outputOnError;

		// Token: 0x0200018D RID: 397
		// (Invoke) Token: 0x060022B8 RID: 8888
		public delegate void UpdateTableDelegate(string dataObject, bool fillAllColumns = false);
	}
}
