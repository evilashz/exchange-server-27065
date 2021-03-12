using System;
using System.ComponentModel;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200016B RID: 363
	public class ResultsTaskCommandAction : TaskCommandAction
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x000390E3 File Offset: 0x000372E3
		public ResultsTaskCommandAction()
		{
			this.PipelineInputProperty = string.Empty;
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x000390F6 File Offset: 0x000372F6
		public ResultsCommandProfile Profile
		{
			get
			{
				return base.Profile as ResultsCommandProfile;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00039103 File Offset: 0x00037303
		public ResultsCommandSetting Setting
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.Setting;
				}
				return null;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0003911A File Offset: 0x0003731A
		public DataListViewResultPane DataListViewResultPane
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.ResultPane as DataListViewResultPane;
				}
				return null;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00039136 File Offset: 0x00037336
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x0003913E File Offset: 0x0003733E
		[DefaultValue(false)]
		public bool UseCustomInputRequestedHandler { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00039147 File Offset: 0x00037347
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x0003914F File Offset: 0x0003734F
		public string PipelineInputProperty { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00039158 File Offset: 0x00037358
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00039160 File Offset: 0x00037360
		public Type PipelineInputType { get; set; }

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003916C File Offset: 0x0003736C
		protected override void OnExecuting(out bool cancelled)
		{
			base.OnExecuting(ref cancelled);
			if (this.DataListViewResultPane != null && this.Setting.IsSelectionCommand && this.Setting.Operation == CommandOperation.Delete)
			{
				DataListView listControl = this.DataListViewResultPane.ListControl;
				if (listControl.SelectedIndices.Count > 0 && listControl.SelectedIndices.Count != listControl.Items.Count)
				{
					int num = (listControl.FocusedItem != null) ? listControl.FocusedItem.Index : -1;
					if (num < 0)
					{
						num = listControl.SelectedIndices[listControl.SelectedIndices.Count - 1];
					}
					int num2 = num;
					if (listControl.SelectedIndices.Contains(num))
					{
						num2 = num + 1;
						while (num2 < listControl.Items.Count && listControl.SelectedIndices.Contains(num2))
						{
							num2++;
						}
					}
					if (num2 >= listControl.Items.Count)
					{
						num2 = num - 1;
						while (num2 >= 0 && listControl.SelectedIndices.Contains(num2))
						{
							num2--;
						}
					}
					if (num2 >= 0 && num2 < listControl.Items.Count)
					{
						this.rowIdToSelectAfterDelete = listControl.GetRowIdentity(listControl.GetRowFromItem(listControl.Items[num2]));
					}
				}
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000392A8 File Offset: 0x000374A8
		protected sealed override void OnInputRequested(WorkUnitCollectionEventArgs e)
		{
			e.WorkUnits.AddRange(this.OnRequestInputs());
			if (this.DataListViewResultPane != null && this.Setting.IsSelectionCommand)
			{
				if (this.Setting.UseSingleRowRefresh && this.DataListViewResultPane.HasSelection)
				{
					base.RefreshOnFinish = this.DataListViewResultPane.GetSelectionRefreshObjects();
					base.MultiRefreshOnFinish = null;
					return;
				}
				if (this.Setting.UseFullRefresh)
				{
					base.RefreshOnFinish = this.DataListViewResultPane.RefreshableDataSource;
					base.MultiRefreshOnFinish = null;
				}
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00039334 File Offset: 0x00037534
		protected virtual WorkUnit[] OnRequestInputs()
		{
			if (this.DataListViewResultPane != null && !this.UseCustomInputRequestedHandler)
			{
				string targetPropertyName = string.IsNullOrEmpty(this.PipelineInputProperty) ? this.DataListViewResultPane.ListControl.IdentityProperty : this.PipelineInputProperty;
				return this.DataListViewResultPane.ListControl.GetSelectedWorkUnits(targetPropertyName, this.PipelineInputType);
			}
			return new WorkUnit[0];
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00039398 File Offset: 0x00037598
		protected sealed override void OnCompleted(WorkUnitCollectionEventArgs e)
		{
			if (this.DataListViewResultPane != null && this.Setting.IsSelectionCommand && this.Setting.Operation == CommandOperation.Delete)
			{
				ISupportFastRefresh supportFastRefresh = this.DataListViewResultPane.RefreshableDataSource as ISupportFastRefresh;
				if (supportFastRefresh != null && base.RefreshOnFinish == null && base.MultiRefreshOnFinish == null)
				{
					foreach (WorkUnit workUnit in e.WorkUnits)
					{
						if (workUnit.Status == WorkUnitStatus.Completed)
						{
							supportFastRefresh.Remove(workUnit.Target);
						}
					}
				}
				if (this.rowIdToSelectAfterDelete != null && !e.WorkUnits.HasFailures)
				{
					this.DataListViewResultPane.ListControl.SelectItemBySpecifiedIdentity(this.rowIdToSelectAfterDelete, false);
				}
				this.rowIdToSelectAfterDelete = null;
			}
			this.OnCompleted(e.WorkUnits.ToArray());
			base.OnCompleted(e);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00039490 File Offset: 0x00037690
		protected virtual void OnCompleted(WorkUnit[] workUnits)
		{
		}

		// Token: 0x040005F6 RID: 1526
		private object rowIdToSelectAfterDelete;
	}
}
