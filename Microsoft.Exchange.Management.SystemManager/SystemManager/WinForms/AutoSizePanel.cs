using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SystemManager.WinForms.Design;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000189 RID: 393
	[Designer(typeof(AutoSizePanelDesigner))]
	[DefaultEvent("Layout")]
	[DefaultProperty("AutoSize")]
	public class AutoSizePanel : ExchangeUserControl
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x0003B3A6 File Offset: 0x000395A6
		public AutoSizePanel()
		{
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SuspendLayout();
			this.AutoSize = true;
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.ResumeLayout(false);
			base.Name = "AutoSizePanel";
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003B3E3 File Offset: 0x000395E3
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x0003B3EB File Offset: 0x000395EB
		[DefaultValue(true)]
		public override bool AutoSize
		{
			get
			{
				return this.autoSize;
			}
			set
			{
				if (value != this.autoSize)
				{
					this.autoSize = value;
					this.OnAutoSizeChanged(EventArgs.Empty);
					base.PerformLayout(this, "AutoSize");
				}
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003B414 File Offset: 0x00039614
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x0003B41C File Offset: 0x0003961C
		[DefaultValue(AutoSizeMode.GrowAndShrink)]
		public new AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.AutoSizeMode;
			}
			set
			{
				base.AutoSizeMode = value;
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003B425 File Offset: 0x00039625
		protected override void OnDockChanged(EventArgs e)
		{
			base.OnDockChanged(e);
			if (this.Dock == DockStyle.None)
			{
				base.PerformLayout(this, "Dock");
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003B442 File Offset: 0x00039642
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (this.AutoSize)
			{
				AutoSizePanel.ApplyPreferredSize(this);
			}
			base.OnLayout(levent);
			if (this.AutoSize)
			{
				AutoSizePanel.ApplyPreferredSize(this);
			}
			if (this.AutoScroll)
			{
				base.AdjustFormScrollbars(this.AutoScroll);
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003B47C File Offset: 0x0003967C
		public static bool CanAutoSizeWidth(Control control)
		{
			if (control.Parent == null)
			{
				return true;
			}
			switch (control.Dock)
			{
			case DockStyle.Top:
			case DockStyle.Bottom:
			case DockStyle.Fill:
				return false;
			}
			return AnchorStyles.Right != (control.Anchor & AnchorStyles.Right);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0003B4C8 File Offset: 0x000396C8
		public static bool CanAutoSizeHeight(Control control)
		{
			if (control.Parent == null)
			{
				return true;
			}
			switch (control.Dock)
			{
			case DockStyle.Left:
			case DockStyle.Right:
			case DockStyle.Fill:
				return false;
			default:
				return AnchorStyles.Bottom != (control.Anchor & AnchorStyles.Bottom);
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003B50C File Offset: 0x0003970C
		public static Size GetFitToContentsSize(Control control)
		{
			if (control.HasChildren)
			{
				int num = int.MinValue;
				int num2 = int.MinValue;
				int num3 = 0;
				int num4 = 0;
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
						{
							flag2 = false;
							flag3 = false;
							Size size = control2.Size;
							num = Math.Max(num, control2.Left + size.Width);
							num2 = Math.Max(num2, control2.Top + size.Height);
							break;
						}
						case DockStyle.Top:
						case DockStyle.Bottom:
							flag3 = false;
							num4 += control2.Height;
							break;
						case DockStyle.Left:
						case DockStyle.Right:
							flag2 = false;
							num3 += control2.Width;
							break;
						case DockStyle.Fill:
							num3 += control2.Width;
							num4 += control2.Height;
							break;
						}
					}
				}
				ScrollableControl scrollableControl = control as ScrollableControl;
				if (scrollableControl != null)
				{
					ScrollableControl.DockPaddingEdges dockPadding = scrollableControl.DockPadding;
					num += dockPadding.Right;
					num2 += dockPadding.Bottom;
					num3 += dockPadding.Left + dockPadding.Right;
					num4 += dockPadding.Top + dockPadding.Bottom;
				}
				num = Math.Max(num, num3);
				num2 = Math.Max(num2, num4);
				if (!flag || !AutoSizePanel.CanAutoSizeWidth(control) || flag2)
				{
					num = control.Size.Width;
				}
				if (!flag || !AutoSizePanel.CanAutoSizeHeight(control) || flag3)
				{
					num2 = control.Size.Height;
				}
				return AutoSizePanel.GetAutoSizeModeSize(control, new Size(num, num2));
			}
			return control.Size;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003B6C8 File Offset: 0x000398C8
		public static void ApplyPreferredSize(Control control)
		{
			if (AutoSizePanel.CanAutoSizeWidth(control) || AutoSizePanel.CanAutoSizeHeight(control))
			{
				Size fitToContentsSize = AutoSizePanel.GetFitToContentsSize(control);
				if (control.Size != fitToContentsSize)
				{
					control.Size = AutoSizePanel.GetAutoSizeModeSize(control, fitToContentsSize);
				}
			}
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003B708 File Offset: 0x00039908
		private static Size GetAutoSizeModeSize(Control control, Size suggestedSize)
		{
			int num = suggestedSize.Width;
			int num2 = suggestedSize.Height;
			UserControl userControl = control as UserControl;
			if (userControl != null && userControl.AutoSizeMode == AutoSizeMode.GrowOnly)
			{
				num = Math.Max(num, control.Size.Width);
				num2 = Math.Max(num2, control.Size.Height);
			}
			return new Size(num, num2);
		}

		// Token: 0x0400060B RID: 1547
		private bool autoSize = true;
	}
}
