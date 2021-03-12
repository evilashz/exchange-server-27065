using System;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000201 RID: 513
	public class ResultsCommandSetting : CommandUtil
	{
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00062A19 File Offset: 0x00060C19
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x00062A26 File Offset: 0x00060C26
		public ResultsCommandProfile Profile
		{
			get
			{
				return base.Profile as ResultsCommandProfile;
			}
			protected internal set
			{
				base.Profile = value;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x00062A2F File Offset: 0x00060C2F
		public ResultPane ResultPane
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.ResultPane;
				}
				return null;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00062A46 File Offset: 0x00060C46
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00062A4E File Offset: 0x00060C4E
		public CommandOperation Operation
		{
			get
			{
				return this.operation;
			}
			set
			{
				if (this.Operation != value)
				{
					this.operation = value;
					this.UpdateUseSingleRowRefresh();
				}
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00062A66 File Offset: 0x00060C66
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00062A6E File Offset: 0x00060C6E
		public bool IsSelectionCommand
		{
			get
			{
				return this.isSelectionCommand;
			}
			set
			{
				if (this.IsSelectionCommand != value)
				{
					this.isSelectionCommand = value;
					this.UpdateUseSingleRowRefresh();
					this.UpdateDefaultUpdatingUtil();
				}
			}
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00062A8C File Offset: 0x00060C8C
		private void UpdateUseSingleRowRefresh()
		{
			this.UseSingleRowRefresh = (this.IsSelectionCommand && this.Operation != CommandOperation.Create);
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00062AAB File Offset: 0x00060CAB
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x00062AB3 File Offset: 0x00060CB3
		public bool RequiresSingleSelection { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00062ABC File Offset: 0x00060CBC
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x00062AC4 File Offset: 0x00060CC4
		public bool RequiresMultiSelection { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x00062ACD File Offset: 0x00060CCD
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x00062AD5 File Offset: 0x00060CD5
		public bool IsPropertiesCommand { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00062ADE File Offset: 0x00060CDE
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x00062AE6 File Offset: 0x00060CE6
		public bool IsRemoveCommand { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x00062AEF File Offset: 0x00060CEF
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x00062AF7 File Offset: 0x00060CF7
		public bool UseSingleRowRefresh
		{
			get
			{
				return this.useSingleRowRefresh;
			}
			set
			{
				if (this.UseSingleRowRefresh != value)
				{
					this.useSingleRowRefresh = value;
					if (this.UseSingleRowRefresh)
					{
						this.UseFullRefresh = false;
					}
				}
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x00062B18 File Offset: 0x00060D18
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x00062B20 File Offset: 0x00060D20
		public bool UseFullRefresh
		{
			get
			{
				return this.useFullRefresh;
			}
			set
			{
				if (this.UseFullRefresh != value)
				{
					this.useFullRefresh = value;
					if (this.UseFullRefresh)
					{
						this.UseSingleRowRefresh = false;
					}
				}
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00062B41 File Offset: 0x00060D41
		protected override void OnProfileUpdated()
		{
			base.OnProfileUpdated();
			this.UpdateDefaultUpdatingUtil();
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00062B50 File Offset: 0x00060D50
		private void UpdateDefaultUpdatingUtil()
		{
			if (this.Profile != null)
			{
				if (this.IsSelectionCommand && this.Profile.UpdatingUtil == null)
				{
					this.Profile.UpdatingUtil = this.defaultSelectionCommandUpdatingUtil;
					return;
				}
				if (!this.IsSelectionCommand && this.Profile.UpdatingUtil == this.defaultSelectionCommandUpdatingUtil)
				{
					this.Profile.UpdatingUtil = null;
				}
			}
		}

		// Token: 0x040008C3 RID: 2243
		private CommandOperation operation;

		// Token: 0x040008C4 RID: 2244
		private bool isSelectionCommand;

		// Token: 0x040008C5 RID: 2245
		private bool useSingleRowRefresh;

		// Token: 0x040008C6 RID: 2246
		private bool useFullRefresh;

		// Token: 0x040008C7 RID: 2247
		private ResultsCommandUpdatingUtil defaultSelectionCommandUpdatingUtil = new SelectionCommandUpdatingUtil();
	}
}
