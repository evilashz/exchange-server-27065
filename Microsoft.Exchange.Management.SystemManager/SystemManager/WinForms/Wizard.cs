using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000216 RID: 534
	[DefaultProperty("WizardPages")]
	[DefaultEvent("CurrentPageChanged")]
	public class Wizard : ExchangeUserControl
	{
		// Token: 0x06001807 RID: 6151 RVA: 0x0006540C File Offset: 0x0006360C
		public Wizard()
		{
			this.wizardPages = (TypedControlCollection<WizardPage>)base.Controls;
			this.wizardPages.ControlMoved += new ControlMovedEventHandler(this.wizardPages_ControlMoved);
			this.sharedContextFlags = new DataContextFlags();
			this.help = new Command();
			this.help.Name = "help";
			this.help.Text = Strings.Help;
			this.help.Execute += delegate(object param0, EventArgs param1)
			{
				this.OnHelpRequested(new HelpEventArgs(Point.Empty));
			};
			this.reset = new Command();
			this.reset.Name = "reset";
			this.reset.Text = Strings.Reset;
			this.reset.Execute += delegate(object param0, EventArgs param1)
			{
				if (this.reset.Enabled)
				{
					(this.CurrentPage as ISupportResetWizardPage).Reset();
				}
			};
			this.back = new Command();
			this.back.Name = "back";
			this.back.Text = Strings.Back;
			this.back.Execute += delegate(object param0, EventArgs param1)
			{
				if (this.back.Enabled && this.CurrentPage.NotifyGoBack())
				{
					this.CurrentPageIndex--;
				}
			};
			this.next = new Command();
			this.next.Name = "next";
			this.next.Text = Strings.Next;
			this.next.Execute += delegate(object param0, EventArgs param1)
			{
				if (this.next.Enabled && this.CurrentPage.NotifyGoForward())
				{
					this.CurrentPageIndex++;
				}
			};
			this.finish = new Command();
			this.finish.Name = "finish";
			this.finish.Text = Strings.Finish;
			this.finish.Execute += delegate(object param0, EventArgs param1)
			{
				if (this.CanFinish())
				{
					this.CloseParentForm(DialogResult.OK);
				}
			};
			this.cancel = new Command();
			this.cancel.Name = "cancel";
			this.cancel.Text = Strings.Cancel;
			this.cancel.Execute += delegate(object param0, EventArgs param1)
			{
				if (this.CanCancel())
				{
					this.CloseParentForm(DialogResult.Cancel);
				}
			};
			base.Name = "Wizard";
			this.TabStop = false;
			this.UpdateWizardButtons();
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00065648 File Offset: 0x00063848
		private void CloseParentForm(DialogResult dialogResult)
		{
			Form parentForm = base.ParentForm;
			if (parentForm != null)
			{
				parentForm.DialogResult = dialogResult;
				parentForm.Close();
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0006566C File Offset: 0x0006386C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Command Help
		{
			get
			{
				return this.help;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x00065674 File Offset: 0x00063874
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command Reset
		{
			get
			{
				return this.reset;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0006567C File Offset: 0x0006387C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command Back
		{
			get
			{
				return this.back;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x00065684 File Offset: 0x00063884
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0006568C File Offset: 0x0006388C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command Finish
		{
			get
			{
				return this.finish;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00065694 File Offset: 0x00063894
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command Cancel
		{
			get
			{
				return this.cancel;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0006569C File Offset: 0x0006389C
		protected override Size DefaultSize
		{
			get
			{
				return WizardPage.defaultSize;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x000656A3 File Offset: 0x000638A3
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x000656AB File Offset: 0x000638AB
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x000656B4 File Offset: 0x000638B4
		[RefreshProperties(RefreshProperties.All)]
		[Description("Wizard Pages. Use the collection editor to add or remove pages and set the CurrentPageIndex to design each page.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Wizard")]
		public TypedControlCollection<WizardPage> WizardPages
		{
			get
			{
				return this.wizardPages;
			}
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x000656BC File Offset: 0x000638BC
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TypedControlCollection<WizardPage>(this);
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x000656C4 File Offset: 0x000638C4
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x000656CC File Offset: 0x000638CC
		[DefaultValue(null)]
		public DataContext Context
		{
			get
			{
				return this.dataContext;
			}
			set
			{
				if (value != this.Context)
				{
					DataContext context = this.Context;
					this.dataContext = value;
					foreach (object obj in this.WizardPages)
					{
						WizardPage wizardPage = (WizardPage)obj;
						if (wizardPage.Context == context)
						{
							wizardPage.Context = value;
						}
					}
				}
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00065748 File Offset: 0x00063948
		private void wizardPages_ControlMoved(object sender, ControlMovedEventArgs e)
		{
			this.OnWizardPageMoved(e);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00065751 File Offset: 0x00063951
		protected virtual void OnWizardPageMoved(ControlMovedEventArgs e)
		{
			if (this.WizardPageMoved != null)
			{
				this.WizardPageMoved.Invoke(this, e);
			}
		}

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06001818 RID: 6168 RVA: 0x00065768 File Offset: 0x00063968
		// (remove) Token: 0x06001819 RID: 6169 RVA: 0x000657A0 File Offset: 0x000639A0
		[Category("Wizard")]
		public event ControlMovedEventHandler WizardPageMoved;

		// Token: 0x0600181A RID: 6170 RVA: 0x000657D8 File Offset: 0x000639D8
		protected override void OnControlAdded(ControlEventArgs e)
		{
			Extensions.EnsureDoubleBuffer(e.Control);
			WizardPage wizardPage = (WizardPage)e.Control;
			e.Control.Visible = false;
			e.Control.Dock = DockStyle.Fill;
			if (wizardPage.ParentPage != null && (!base.Controls.Contains(wizardPage.ParentPage) || !wizardPage.ParentPage.ChildPages.Contains(wizardPage)))
			{
				throw new NotSupportedException();
			}
			wizardPage.ChildPages.ListChanged += this.wizardPage_ChildPagesListChanged;
			if (wizardPage.ParentPage != null)
			{
				int num = base.Controls.IndexOf(wizardPage.ParentPage) + 1;
				for (int i = 0; i < wizardPage.ParentPage.ChildPages.IndexOf(wizardPage); i++)
				{
					WizardPage wizardPage2 = wizardPage.ParentPage.ChildPages[i];
					num += wizardPage2.GetChildCount() + 1;
				}
				base.Controls.SetChildIndex(wizardPage, num);
			}
			if (wizardPage.Context == null)
			{
				wizardPage.Context = this.Context;
			}
			this.sharedContextFlags.Pages.Add(wizardPage);
			this.OnWizardPageAdded(e);
			if (1 == this.WizardPages.Count)
			{
				this.CurrentPageIndex = 0;
			}
			foreach (WizardPage value in wizardPage.ChildPages)
			{
				base.Controls.Add(value);
			}
			base.OnControlAdded(e);
			this.UpdateWizardButtons();
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00065960 File Offset: 0x00063B60
		protected virtual void OnWizardPageAdded(ControlEventArgs e)
		{
			if (this.WizardPageAdded != null)
			{
				this.WizardPageAdded(this, e);
			}
		}

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x0600181C RID: 6172 RVA: 0x00065978 File Offset: 0x00063B78
		// (remove) Token: 0x0600181D RID: 6173 RVA: 0x000659B0 File Offset: 0x00063BB0
		[Category("Wizard")]
		public event ControlEventHandler WizardPageAdded;

		// Token: 0x0600181E RID: 6174 RVA: 0x000659E8 File Offset: 0x00063BE8
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			WizardPage wizardPage = (WizardPage)e.Control;
			wizardPage.ChildPages.ListChanged -= this.wizardPage_ChildPagesListChanged;
			if (wizardPage.ParentPage != null && base.Controls.Contains(wizardPage.ParentPage) && wizardPage.ParentPage.ChildPages.Contains(wizardPage))
			{
				throw new NotSupportedException();
			}
			foreach (WizardPage value in wizardPage.ChildPages)
			{
				base.Controls.Remove(value);
			}
			if (this.CurrentPage == wizardPage)
			{
				this.CurrentPage = ((this.WizardPages.Count > 0) ? this.WizardPages[0] : null);
			}
			else
			{
				this.UpdateWizardButtons();
			}
			this.sharedContextFlags.Pages.Remove(wizardPage);
			this.OnWizardPageRemoved(e);
			base.OnControlRemoved(e);
			this.UpdateWizardButtons();
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00065AF0 File Offset: 0x00063CF0
		protected virtual void OnWizardPageRemoved(ControlEventArgs e)
		{
			if (this.WizardPageRemoved != null)
			{
				this.WizardPageRemoved(this, e);
			}
		}

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06001820 RID: 6176 RVA: 0x00065B08 File Offset: 0x00063D08
		// (remove) Token: 0x06001821 RID: 6177 RVA: 0x00065B40 File Offset: 0x00063D40
		[Category("Wizard")]
		public event ControlEventHandler WizardPageRemoved;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06001822 RID: 6178 RVA: 0x00065B78 File Offset: 0x00063D78
		// (remove) Token: 0x06001823 RID: 6179 RVA: 0x00065BB0 File Offset: 0x00063DB0
		[Category("Wizard")]
		public event EventHandler CurrentPageChanged;

		// Token: 0x06001824 RID: 6180 RVA: 0x00065BE5 File Offset: 0x00063DE5
		protected virtual void OnCurrentPageChanged()
		{
			if (this.CurrentPageChanged != null)
			{
				this.CurrentPageChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x00065C00 File Offset: 0x00063E00
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x00065C08 File Offset: 0x00063E08
		[Category("Wizard")]
		[Browsable(false)]
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public WizardPage CurrentPage
		{
			get
			{
				return this.currentPage;
			}
			set
			{
				if (value != this.CurrentPage)
				{
					base.SuspendLayout();
					try
					{
						if (this.CurrentPage != null)
						{
							if (!this.CurrentPage.OnKillActive())
							{
								return;
							}
							this.CurrentPage.CanGoBackChanged -= this.UpdateButtons;
							this.CurrentPage.CanGoForwardChanged -= this.UpdateButtons;
							this.CurrentPage.CanFinishChanged -= this.UpdateButtons;
							this.CurrentPage.CanCancelChanged -= this.UpdateButtons;
							this.CurrentPage.NextButtonTextChanged -= this.NextButtonChanged;
							this.CurrentPage.TextChanged -= this.CurrentPage_TextChanged;
							this.CurrentPage.Visible = false;
						}
						this.currentPage = value;
						this.UpdateWizardButtons();
						if (this.CurrentPage != null)
						{
							this.CurrentPage.CanGoBackChanged += this.UpdateButtons;
							this.CurrentPage.CanGoForwardChanged += this.UpdateButtons;
							this.CurrentPage.CanFinishChanged += this.UpdateButtons;
							this.CurrentPage.CanCancelChanged += this.UpdateButtons;
							this.CurrentPage.NextButtonTextChanged += this.NextButtonChanged;
							this.CurrentPage.TextChanged += this.CurrentPage_TextChanged;
							this.CurrentPage.Visible = true;
							this.CurrentPage.OnSetActive();
							this.Text = this.CurrentPage.Text;
							this.TabStop = this.CurrentPage.TabStop;
							this.next.Text = this.CurrentPage.NextButtonText;
							this.CurrentPage.Select();
						}
						else
						{
							this.Text = "";
							this.TabStop = false;
							this.next.Text = Strings.Next;
						}
						this.OnCurrentPageChanged();
					}
					finally
					{
						base.ResumeLayout(true);
					}
				}
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00065E30 File Offset: 0x00064030
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x00065E43 File Offset: 0x00064043
		[Category("Wizard")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Index of the current Wizard Page. Use the collection editor in WizardPages to add or remove pages and set the CurrentPageIndex to design each page.")]
		public int CurrentPageIndex
		{
			get
			{
				return this.WizardPages.IndexOf(this.CurrentPage);
			}
			set
			{
				this.CurrentPage = this.WizardPages[value];
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00065E57 File Offset: 0x00064057
		private void CurrentPage_TextChanged(object sender, EventArgs e)
		{
			this.Text = this.CurrentPage.Text;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00065E6A File Offset: 0x0006406A
		private void NextButtonChanged(object sender, EventArgs e)
		{
			this.next.Text = this.CurrentPage.NextButtonText;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00065E87 File Offset: 0x00064087
		private void UpdateButtons(object sender, EventArgs e)
		{
			this.UpdateWizardButtons();
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00065E90 File Offset: 0x00064090
		protected void UpdateWizardButtons()
		{
			this.OnUpdatingButtons(EventArgs.Empty);
			try
			{
				if (this.WizardPages.Count != 0)
				{
					bool flag = this.WizardPages.Count > 1;
					bool flag2 = this.CurrentPage == this.WizardPages[0];
					bool flag3 = this.CurrentPage == this.WizardPages[this.WizardPages.Count - 1];
					this.back.Enabled = (!flag2 && flag && this.CurrentPage.CanGoBack);
					this.next.Enabled = (!flag3 && flag && this.CurrentPage.CanGoForward);
					this.finish.Enabled = this.CurrentPage.CanFinish;
					this.cancel.Enabled = this.CurrentPage.CanCancel;
					this.back.Visible = flag;
					this.next.Visible = (!flag3 && flag);
					this.finish.Visible = flag3;
					this.reset.Visible = (this.CurrentPage is ISupportResetWizardPage);
				}
				else
				{
					this.back.Visible = false;
					this.next.Visible = false;
					this.finish.Visible = false;
					this.back.Enabled = false;
					this.next.Enabled = false;
					this.finish.Enabled = false;
					this.cancel.Enabled = true;
				}
			}
			finally
			{
				this.OnButtonsUpdated(EventArgs.Empty);
			}
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0006602C File Offset: 0x0006422C
		protected virtual void OnUpdatingButtons(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Wizard.EventUpdatingButtons];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600182E RID: 6190 RVA: 0x0006605A File Offset: 0x0006425A
		// (remove) Token: 0x0600182F RID: 6191 RVA: 0x0006606D File Offset: 0x0006426D
		public event EventHandler UpdatingButtons
		{
			add
			{
				base.Events.AddHandler(Wizard.EventUpdatingButtons, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.EventUpdatingButtons, value);
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00066080 File Offset: 0x00064280
		protected virtual void OnButtonsUpdated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Wizard.EventButtonsUpdated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x06001831 RID: 6193 RVA: 0x000660AE File Offset: 0x000642AE
		// (remove) Token: 0x06001832 RID: 6194 RVA: 0x000660C1 File Offset: 0x000642C1
		public event EventHandler ButtonsUpdated
		{
			add
			{
				base.Events.AddHandler(Wizard.EventButtonsUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.EventButtonsUpdated, value);
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000660D4 File Offset: 0x000642D4
		public bool CanCancel()
		{
			bool result = true;
			if (this.CurrentPage != null)
			{
				result = this.CurrentPage.NotifyCancel();
			}
			return result;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000660F8 File Offset: 0x000642F8
		public bool CanFinish()
		{
			bool result = false;
			if (this.CurrentPage != null && (this.CurrentPage.OnKillActive() || this.CurrentPage.NotifyFinish()))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0006612C File Offset: 0x0006432C
		private void wizardPage_ChildPagesListChanged(object sender, ListChangedEventArgs e)
		{
			WizardPageCollection wizardPageCollection = (WizardPageCollection)sender;
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				base.Controls.Add(wizardPageCollection[e.NewIndex]);
				return;
			}
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				int num = base.Controls.IndexOf(wizardPageCollection.ParentPage) + 1;
				for (int i = 0; i < e.NewIndex; i++)
				{
					num += wizardPageCollection[i].GetChildCount() + 1;
				}
				base.Controls.RemoveAt(num);
				return;
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				int j = base.Controls.Count;
				while (j > 0)
				{
					j--;
					WizardPage wizardPage = (WizardPage)base.Controls[j];
					if (wizardPage.ParentPage == wizardPageCollection.ParentPage)
					{
						base.Controls.Remove(wizardPage);
					}
				}
				foreach (WizardPage value in wizardPageCollection)
				{
					base.Controls.Add(value);
				}
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00066244 File Offset: 0x00064444
		internal bool IsDirty
		{
			get
			{
				bool result = false;
				foreach (object obj in this.WizardPages)
				{
					WizardPage wizardPage = (WizardPage)obj;
					if (wizardPage.IsHandleCreated && wizardPage.IsDirty)
					{
						result = true;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x000662B0 File Offset: 0x000644B0
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x000662B8 File Offset: 0x000644B8
		[DefaultValue(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsDataChanged
		{
			get
			{
				return this.isDataChanged;
			}
			internal set
			{
				this.isDataChanged = value;
			}
		}

		// Token: 0x040008FF RID: 2303
		private const string WizardCategory = "Wizard";

		// Token: 0x04000900 RID: 2304
		private TypedControlCollection<WizardPage> wizardPages;

		// Token: 0x04000901 RID: 2305
		private DataContextFlags sharedContextFlags;

		// Token: 0x04000902 RID: 2306
		private Command help;

		// Token: 0x04000903 RID: 2307
		private Command reset;

		// Token: 0x04000904 RID: 2308
		private Command back;

		// Token: 0x04000905 RID: 2309
		private Command next;

		// Token: 0x04000906 RID: 2310
		private Command finish;

		// Token: 0x04000907 RID: 2311
		private Command cancel;

		// Token: 0x04000908 RID: 2312
		private DataContext dataContext;

		// Token: 0x0400090D RID: 2317
		private WizardPage currentPage;

		// Token: 0x0400090E RID: 2318
		private static readonly object EventUpdatingButtons = new object();

		// Token: 0x0400090F RID: 2319
		private static readonly object EventButtonsUpdated = new object();

		// Token: 0x04000910 RID: 2320
		private bool isDataChanged;
	}
}
