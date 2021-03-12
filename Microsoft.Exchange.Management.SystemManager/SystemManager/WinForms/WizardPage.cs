using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001BA RID: 442
	[DefaultEvent("Validating")]
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	public class WizardPage : ExchangePage
	{
		// Token: 0x060011FB RID: 4603 RVA: 0x000487C8 File Offset: 0x000469C8
		public WizardPage()
		{
			base.Name = "WizardPage";
			this.childPages = new WizardPageCollection(this);
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x00048807 File Offset: 0x00046A07
		protected override Size DefaultSize
		{
			get
			{
				return WizardPage.defaultSize;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0004880E File Offset: 0x00046A0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public Wizard Wizard
		{
			get
			{
				if (base.Parent == null)
				{
					return null;
				}
				return (Wizard)base.Parent;
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00048828 File Offset: 0x00046A28
		protected override void OnSetActive(EventArgs e)
		{
			if (base.CheckReadOnlyAndDisablePage() && this.Wizard.WizardPages.Count == 2 && this.Wizard.WizardPages[0] == this)
			{
				this.CanGoForward = false;
				base.CanCancel = true;
			}
			base.OnSetActive(e);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00048879 File Offset: 0x00046A79
		protected override void OnKillActive(CancelEventArgs e)
		{
			base.InputValidationProvider.WriteBindings();
			base.OnKillActive(e);
		}

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06001200 RID: 4608 RVA: 0x00048890 File Offset: 0x00046A90
		// (remove) Token: 0x06001201 RID: 4609 RVA: 0x000488C8 File Offset: 0x00046AC8
		public event CancelEventHandler GoBack;

		// Token: 0x06001202 RID: 4610 RVA: 0x000488FD File Offset: 0x00046AFD
		protected virtual void OnGoBack(CancelEventArgs e)
		{
			if (this.GoBack != null)
			{
				this.GoBack(this, e);
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00048914 File Offset: 0x00046B14
		public bool NotifyGoBack()
		{
			if (this.backupIgnorePageValidationOnGoBack == null)
			{
				this.backupIgnorePageValidationOnGoBack = new bool?(base.IgnorePageValidation);
			}
			base.IgnorePageValidation = true;
			CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
			this.OnGoBack(cancelEventArgs);
			return !cancelEventArgs.Cancel;
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0004895D File Offset: 0x00046B5D
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x00048965 File Offset: 0x00046B65
		[DefaultValue(true)]
		public bool CanGoBack
		{
			get
			{
				return this.canGoBack;
			}
			set
			{
				if (this.CanGoBack != value)
				{
					this.canGoBack = value;
					this.OnCanGoBackChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00048982 File Offset: 0x00046B82
		protected virtual void OnCanGoBackChanged(EventArgs e)
		{
			if (this.CanGoBackChanged != null)
			{
				this.CanGoBackChanged(this, e);
			}
		}

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06001207 RID: 4615 RVA: 0x0004899C File Offset: 0x00046B9C
		// (remove) Token: 0x06001208 RID: 4616 RVA: 0x000489D4 File Offset: 0x00046BD4
		public event EventHandler CanGoBackChanged;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06001209 RID: 4617 RVA: 0x00048A0C File Offset: 0x00046C0C
		// (remove) Token: 0x0600120A RID: 4618 RVA: 0x00048A44 File Offset: 0x00046C44
		public event CancelEventHandler GoForward;

		// Token: 0x0600120B RID: 4619 RVA: 0x00048A79 File Offset: 0x00046C79
		protected virtual void OnGoForward(CancelEventArgs e)
		{
			if (this.GoForward != null)
			{
				this.GoForward(this, e);
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00048A90 File Offset: 0x00046C90
		public bool NotifyGoForward()
		{
			if (this.backupIgnorePageValidationOnGoBack != null)
			{
				base.IgnorePageValidation = this.backupIgnorePageValidationOnGoBack.Value;
			}
			CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
			this.OnGoForward(cancelEventArgs);
			return !cancelEventArgs.Cancel;
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00048AD2 File Offset: 0x00046CD2
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00048ADA File Offset: 0x00046CDA
		[DefaultValue(true)]
		public bool CanGoForward
		{
			get
			{
				return this.canGoForward;
			}
			set
			{
				if (this.CanGoForward != value)
				{
					this.canGoForward = value;
					this.OnCanGoForwardChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00048AF7 File Offset: 0x00046CF7
		protected virtual void OnCanGoForwardChanged(EventArgs e)
		{
			if (this.CanGoForwardChanged != null)
			{
				this.CanGoForwardChanged(this, e);
			}
		}

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06001210 RID: 4624 RVA: 0x00048B10 File Offset: 0x00046D10
		// (remove) Token: 0x06001211 RID: 4625 RVA: 0x00048B48 File Offset: 0x00046D48
		public event EventHandler CanGoForwardChanged;

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06001212 RID: 4626 RVA: 0x00048B80 File Offset: 0x00046D80
		// (remove) Token: 0x06001213 RID: 4627 RVA: 0x00048BB8 File Offset: 0x00046DB8
		public event CancelEventHandler Finish;

		// Token: 0x06001214 RID: 4628 RVA: 0x00048BED File Offset: 0x00046DED
		protected virtual void OnFinish(CancelEventArgs e)
		{
			if (this.Finish != null)
			{
				this.Finish(this, e);
			}
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00048C04 File Offset: 0x00046E04
		public bool NotifyFinish()
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
			this.OnFinish(cancelEventArgs);
			return !cancelEventArgs.Cancel;
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00048C28 File Offset: 0x00046E28
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x00048C30 File Offset: 0x00046E30
		[DefaultValue(true)]
		public bool CanFinish
		{
			get
			{
				return this.canFinish;
			}
			set
			{
				if (this.CanFinish != value)
				{
					this.canFinish = value;
					this.OnCanFinishChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00048C4D File Offset: 0x00046E4D
		protected virtual void OnCanFinishChanged(EventArgs e)
		{
			if (this.CanFinishChanged != null)
			{
				this.CanFinishChanged(this, e);
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06001219 RID: 4633 RVA: 0x00048C64 File Offset: 0x00046E64
		// (remove) Token: 0x0600121A RID: 4634 RVA: 0x00048C9C File Offset: 0x00046E9C
		public event EventHandler CanFinishChanged;

		// Token: 0x0600121B RID: 4635 RVA: 0x00048CD1 File Offset: 0x00046ED1
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.OnNextButtonTextChanged(EventArgs.Empty);
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00048CE5 File Offset: 0x00046EE5
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x00048CFB File Offset: 0x00046EFB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public LocalizedString NextButtonText
		{
			get
			{
				if (!base.Enabled)
				{
					return Strings.Next;
				}
				return this.nextButtonText;
			}
			set
			{
				value = ((value == LocalizedString.Empty) ? Strings.Next : value);
				if (value != this.NextButtonText)
				{
					this.nextButtonText = value;
					this.OnNextButtonTextChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00048D34 File Offset: 0x00046F34
		private void ResetNextButtonText()
		{
			this.NextButtonText = Strings.Next;
		}

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x0600121F RID: 4639 RVA: 0x00048D44 File Offset: 0x00046F44
		// (remove) Token: 0x06001220 RID: 4640 RVA: 0x00048D7C File Offset: 0x00046F7C
		public event EventHandler NextButtonTextChanged;

		// Token: 0x06001221 RID: 4641 RVA: 0x00048DB1 File Offset: 0x00046FB1
		protected void OnNextButtonTextChanged(EventArgs e)
		{
			if (this.NextButtonTextChanged != null)
			{
				this.NextButtonTextChanged(this, e);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00048DC8 File Offset: 0x00046FC8
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x00048DD0 File Offset: 0x00046FD0
		[DefaultValue(null)]
		public WizardPage ParentPage
		{
			get
			{
				return this.parentPage;
			}
			internal set
			{
				if (value != this.ParentPage)
				{
					this.parentPage = value;
				}
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00048DE2 File Offset: 0x00046FE2
		public WizardPageCollection ChildPages
		{
			get
			{
				return this.childPages;
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00048DEC File Offset: 0x00046FEC
		internal int GetChildCount()
		{
			int num = this.ChildPages.Count;
			foreach (WizardPage wizardPage in this.ChildPages)
			{
				num += wizardPage.GetChildCount();
			}
			return num;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00048E48 File Offset: 0x00047048
		protected override void OnCancel(CancelEventArgs e)
		{
			if (this.Wizard != null && this.Wizard.WizardPages.Count >= 2 && this.Wizard.IsDirty)
			{
				DialogResult dialogResult = base.ShowMessage(Strings.CancelWizardConfirmationMessage, MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.No)
				{
					e.Cancel = true;
				}
			}
			base.OnCancel(e);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00048EA1 File Offset: 0x000470A1
		protected override void OnReadDataFailed(EventArgs e)
		{
			base.BeginInvoke(new EventHandler(this.CloseWizard));
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00048EB8 File Offset: 0x000470B8
		private void CloseWizard(object sender, EventArgs e)
		{
			Form parentForm = base.ParentForm;
			if (parentForm != null)
			{
				parentForm.DialogResult = DialogResult.Cancel;
				parentForm.Close();
			}
		}

		// Token: 0x040006DD RID: 1757
		private bool? backupIgnorePageValidationOnGoBack;

		// Token: 0x040006DE RID: 1758
		internal static Size defaultSize = new Size(454, 398);

		// Token: 0x040006E0 RID: 1760
		private bool canGoBack = true;

		// Token: 0x040006E3 RID: 1763
		private bool canGoForward = true;

		// Token: 0x040006E6 RID: 1766
		private bool canFinish = true;

		// Token: 0x040006E8 RID: 1768
		private LocalizedString nextButtonText = Strings.Next;

		// Token: 0x040006EA RID: 1770
		private WizardPage parentPage;

		// Token: 0x040006EB RID: 1771
		private WizardPageCollection childPages;
	}
}
