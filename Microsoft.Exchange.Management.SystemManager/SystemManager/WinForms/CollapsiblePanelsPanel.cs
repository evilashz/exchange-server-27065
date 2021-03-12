using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200018E RID: 398
	[DefaultProperty("CollapsiblePanels")]
	public class CollapsiblePanelsPanel : FlowPanel
	{
		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003D708 File Offset: 0x0003B908
		public CollapsiblePanelsPanel()
		{
			this.collapsiblePanels = (TypedControlCollection<CollapsiblePanel>)this.Controls;
			base.SetStyle(Theme.UserPaintStyle, true);
			this.contextMenu = new ContextMenu();
			this.expandAll = new MenuItem();
			this.collapseAll = new MenuItem();
			base.SuspendLayout();
			this.contextMenu.MenuItems.AddRange(new MenuItem[]
			{
				this.expandAll,
				this.collapseAll
			});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Popup += this.contextMenu_Popup;
			this.expandAll.Name = "expandAll";
			this.expandAll.Text = Strings.ExpandAll;
			this.expandAll.Click += delegate(object param0, EventArgs param1)
			{
				this.ExpandAll();
			};
			this.collapseAll.Name = "collapseAll";
			this.collapseAll.Text = Strings.CollapseAll;
			this.collapseAll.Click += delegate(object param0, EventArgs param1)
			{
				this.CollapseAll();
			};
			this.ContextMenu = this.contextMenu;
			base.Name = "CollapsiblePanelsPanel";
			this.ForeColor = SystemColors.WindowText;
			this.BackColor = SystemColors.Window;
			base.ResumeLayout(false);
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x0003D86A File Offset: 0x0003BA6A
		// (set) Token: 0x06000FC5 RID: 4037 RVA: 0x0003D872 File Offset: 0x0003BA72
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003D87C File Offset: 0x0003BA7C
		private void contextMenu_Popup(object sender, EventArgs e)
		{
			bool enabled;
			bool enabled2;
			this.GetPanelsState(out enabled, out enabled2);
			this.expandAll.Enabled = enabled;
			this.collapseAll.Enabled = enabled2;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003D8AC File Offset: 0x0003BAAC
		protected virtual void GetPanelsState(out bool enableExpandAll, out bool enableCollapseAll)
		{
			enableExpandAll = false;
			enableCollapseAll = false;
			for (int i = 0; i < this.CollapsiblePanels.Count; i++)
			{
				if (this.CollapsiblePanels[i].Visible)
				{
					if (this.CollapsiblePanels[i].IsMinimized)
					{
						enableExpandAll = true;
					}
					else
					{
						enableCollapseAll = true;
					}
					if (enableCollapseAll && enableExpandAll)
					{
						return;
					}
				}
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003D90C File Offset: 0x0003BB0C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.contextMenu.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003D923 File Offset: 0x0003BB23
		public void ExpandAll()
		{
			this.SetIsMinimizeInAll(false);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0003D92C File Offset: 0x0003BB2C
		public void CollapseAll()
		{
			this.SetIsMinimizeInAll(true);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003D938 File Offset: 0x0003BB38
		protected virtual void SetIsMinimizeInAll(bool collapse)
		{
			Control activeControl = this.GetChildAtPointIfHandleCreated(new Point(base.Padding.Left, base.Padding.Top));
			CollapsiblePanel.Animate = false;
			base.SuspendLayout();
			using (new ControlWaitCursor(this))
			{
				try
				{
					foreach (object obj in this.CollapsiblePanels)
					{
						CollapsiblePanel collapsiblePanel = (CollapsiblePanel)obj;
						if (collapsiblePanel.ContainsFocus)
						{
							activeControl = collapsiblePanel;
						}
						collapsiblePanel.IsMinimized = collapse;
					}
				}
				finally
				{
					CollapsiblePanel.Animate = true;
					base.ResumeLayout();
				}
			}
			base.ScrollControlIntoView(activeControl);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0003DA1C File Offset: 0x0003BC1C
		private Control GetChildAtPointIfHandleCreated(Point pt)
		{
			if (!base.IsHandleCreated)
			{
				return null;
			}
			return base.GetChildAtPoint(pt);
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0003DA2F File Offset: 0x0003BC2F
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x0003DA37 File Offset: 0x0003BC37
		[DefaultValue(typeof(Color), "WindowText")]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0003DA40 File Offset: 0x0003BC40
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0003DA48 File Offset: 0x0003BC48
		[DefaultValue(typeof(Color), "Window")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003DA51 File Offset: 0x0003BC51
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TypedControlCollection<CollapsiblePanel>(this);
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0003DA59 File Offset: 0x0003BC59
		[Category("Behavior")]
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		public TypedControlCollection<CollapsiblePanel> CollapsiblePanels
		{
			get
			{
				return this.collapsiblePanels;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0003DA61 File Offset: 0x0003BC61
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003DA6C File Offset: 0x0003BC6C
		protected override void OnLayout(LayoutEventArgs levent)
		{
			bool flag = levent.AffectedProperty == CollapsiblePanel.AlignLayout;
			if (!flag)
			{
				base.OnLayout(levent);
			}
			bool flag2 = levent.AffectedProperty == "Parent";
			if (flag || flag2)
			{
				this.AlignStatusLabel();
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003DAB4 File Offset: 0x0003BCB4
		internal void AlignStatusLabel()
		{
			int num = 0;
			for (int i = 0; i < this.CollapsiblePanels.Count; i++)
			{
				num = Math.Max(num, this.CollapsiblePanels[i].GetStatusWidth());
			}
			for (int j = 0; j < this.CollapsiblePanels.Count; j++)
			{
				this.CollapsiblePanels[j].SetStatusWidth(num);
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0003DB19 File Offset: 0x0003BD19
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x0003DB24 File Offset: 0x0003BD24
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				if (base.MaximumSize != value)
				{
					base.MaximumSize = value;
					if (base.Height < this.MaximumSize.Height && base.VScroll)
					{
						base.Height = this.MaximumSize.Height;
					}
				}
			}
		}

		// Token: 0x0400062D RID: 1581
		private ContextMenu contextMenu;

		// Token: 0x0400062E RID: 1582
		private MenuItem expandAll;

		// Token: 0x0400062F RID: 1583
		private MenuItem collapseAll;

		// Token: 0x04000630 RID: 1584
		private TypedControlCollection<CollapsiblePanel> collapsiblePanels;
	}
}
