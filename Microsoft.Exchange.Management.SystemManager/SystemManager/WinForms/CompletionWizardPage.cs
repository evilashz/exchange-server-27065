using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001BD RID: 445
	public class CompletionWizardPage : TridentsWizardPage
	{
		// Token: 0x06001244 RID: 4676 RVA: 0x00049B7B File Offset: 0x00047D7B
		public CompletionWizardPage()
		{
			this.InitializeComponent();
			this.Text = Strings.WizardCompletionTitleText;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00049B9C File Offset: 0x00047D9C
		private void InitializeComponent()
		{
			((ISupportInitialize)base.BindingSource).BeginInit();
			base.SuspendLayout();
			base.InputValidationProvider.SetEnabled(base.BindingSource, true);
			base.WorkUnitsPanel.TaskState = 2;
			this.CanCancel = false;
			base.Name = "CompletionWizardPage";
			((ISupportInitialize)base.BindingSource).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00049C02 File Offset: 0x00047E02
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x00049C0A File Offset: 0x00047E0A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00049C13 File Offset: 0x00047E13
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x00049C1B File Offset: 0x00047E1B
		[DefaultValue(false)]
		public new bool CanCancel
		{
			get
			{
				return base.CanCancel;
			}
			set
			{
				base.CanCancel = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00049C24 File Offset: 0x00047E24
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x00049C2C File Offset: 0x00047E2C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00049C38 File Offset: 0x00047E38
		protected override void OnSetActive(EventArgs e)
		{
			base.OnSetActive(e);
			if (base.DataHandler != null)
			{
				base.WorkUnits = base.DataHandler.WorkUnits;
				base.ElapsedTimeText = base.WorkUnits.ElapsedTimeText;
				bool flag = true;
				if (base.Wizard != null && base.Wizard.CurrentPageIndex - 1 >= 0)
				{
					ProgressTridentsWizardPage progressTridentsWizardPage = base.Wizard.WizardPages[base.Wizard.CurrentPageIndex - 1] as ProgressTridentsWizardPage;
					if (progressTridentsWizardPage != null)
					{
						flag = progressTridentsWizardPage.CanGoBack;
					}
				}
				base.CanGoBack = (base.DataHandler.WorkUnits.HasFailures && flag);
				if (base.WorkUnits.Cancelled)
				{
					base.Status = Strings.TheWizardWasCancelled;
					base.ShortDescription = Strings.WizardCancelledNotAllActionsCompleted + " " + Strings.FinishWizardDescription;
					return;
				}
				base.Status = base.DataHandler.CompletionStatus;
				string text = base.DataHandler.CompletionDescription.Trim();
				if (string.IsNullOrEmpty(text))
				{
					if (base.Wizard != null && base.Wizard.ParentForm is WizardForm)
					{
						if (base.DataHandler.IsSucceeded)
						{
							text = (base.WorkUnits.AllCompleted ? Strings.WizardCompletionSucceededDescription : Strings.WizardCompletionPartialSucceededDescription);
						}
						else
						{
							text = Strings.WizardCompletionFailedDescription;
						}
					}
					text = text + " " + Strings.FinishWizardDescription;
				}
				base.ShortDescription = text;
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00049DB8 File Offset: 0x00047FB8
		protected override void OnGoBack(CancelEventArgs e)
		{
			base.OnGoBack(e);
			if (!e.Cancel)
			{
				base.WorkUnits = null;
			}
		}
	}
}
