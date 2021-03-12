using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Microsoft.Exchange.Management.SystemManager.WinForms.Design
{
	// Token: 0x020001AE RID: 430
	public class AutoSizePanelDesigner : ScrollableControlDesigner
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0004380E File Offset: 0x00041A0E
		public AutoSizePanel AutoSizePanel
		{
			get
			{
				return this.Control as AutoSizePanel;
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0004381B File Offset: 0x00041A1B
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.Control.SizeChanged += this.Control_SizeChanged;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0004383B File Offset: 0x00041A3B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.Control != null)
			{
				this.Control.SizeChanged -= this.Control_SizeChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00043866 File Offset: 0x00041A66
		private void Control_SizeChanged(object sender, EventArgs e)
		{
			if (this.Control.Parent != null)
			{
				this.Control.Parent.Invalidate(true);
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00043888 File Offset: 0x00041A88
		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			Rectangle clientRectangle = this.Control.ClientRectangle;
			clientRectangle.Width--;
			clientRectangle.Height--;
			Color backColor = this.Control.BackColor;
			Color color = ((double)backColor.GetBrightness() < 0.5) ? ControlPaint.Light(backColor) : ControlPaint.Dark(backColor);
			using (Pen pen = new Pen(color))
			{
				pen.DashStyle = DashStyle.Dash;
				pe.Graphics.DrawRectangle(pen, clientRectangle);
			}
			base.OnPaintAdornments(pe);
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0004392C File Offset: 0x00041B2C
		public override SelectionRules SelectionRules
		{
			get
			{
				return AutoSizePanelDesigner.FilterContainerSelectionRules(base.SelectionRules, this.Control, this.AutoSizePanel.AutoSize);
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0004394C File Offset: 0x00041B4C
		public static SelectionRules FilterContainerSelectionRules(SelectionRules baseRules, Control control, bool autoSize)
		{
			if (autoSize)
			{
				bool flag = false;
				bool flag2 = true;
				bool flag3 = true;
				for (int i = 0; i < control.Controls.Count; i++)
				{
					Control control2 = control.Controls[i];
					if (control2.Visible)
					{
						flag = true;
						switch (control2.Dock)
						{
						case DockStyle.None:
							flag2 = false;
							flag3 = false;
							break;
						case DockStyle.Top:
						case DockStyle.Bottom:
							flag3 = false;
							break;
						case DockStyle.Left:
						case DockStyle.Right:
							flag2 = false;
							break;
						}
					}
				}
				if (flag && !flag2 && AutoSizePanel.CanAutoSizeWidth(control))
				{
					baseRules &= ~(SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
				}
				if (flag && !flag3 && AutoSizePanel.CanAutoSizeHeight(control))
				{
					baseRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
				}
			}
			return baseRules;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000439F3 File Offset: 0x00041BF3
		public static SelectionRules FilterSelectionRules(SelectionRules baseRules, Control control, bool autoSize)
		{
			if (autoSize)
			{
				if (AutoSizePanel.CanAutoSizeWidth(control))
				{
					baseRules &= ~(SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
				}
				if (AutoSizePanel.CanAutoSizeHeight(control))
				{
					baseRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
				}
			}
			return baseRules;
		}
	}
}
