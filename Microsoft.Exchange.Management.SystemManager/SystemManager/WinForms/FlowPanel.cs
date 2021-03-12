using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200018D RID: 397
	public class FlowPanel : AutoSizePanel
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003D1E4 File Offset: 0x0003B3E4
		public FlowPanel()
		{
			base.SuspendLayout();
			this.AutoScroll = true;
			this.TabStop = false;
			base.ResumeLayout(false);
			base.Name = "FlowPanel";
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003D212 File Offset: 0x0003B412
		protected override void CreateHandle()
		{
			this.resumeLayout = true;
			this.Cursor = Cursors.WaitCursor;
			base.SuspendLayout();
			base.CreateHandle();
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003D232 File Offset: 0x0003B432
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (this.resumeLayout)
			{
				this.resumeLayout = false;
				this.Cursor = null;
				base.ResumeLayout(false);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0003D257 File Offset: 0x0003B457
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 0);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0003D264 File Offset: 0x0003B464
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x0003D26C File Offset: 0x0003B46C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(true)]
		[Browsable(false)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0003D275 File Offset: 0x0003B475
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x0003D27D File Offset: 0x0003B47D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		[Browsable(false)]
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

		// Token: 0x06000FBE RID: 4030 RVA: 0x0003D286 File Offset: 0x0003B486
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (base.Visible && base.IsHandleCreated)
			{
				this.AdjustLocationOfChildControls(levent);
				base.OnLayout(levent);
			}
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003D2A8 File Offset: 0x0003B4A8
		private void AdjustChildControlsToHideHorizontalScrollBar()
		{
			int num = base.Size.Width - base.Padding.Horizontal;
			if (base.Controls[0].Width != num)
			{
				num -= SystemInformation.VerticalScrollBarWidth;
			}
			int num2 = this.UpdateChildControlsWithProposedWidth(num);
			num = base.Size.Width - base.Padding.Horizontal;
			if (num2 > Math.Max(this.MaximumSize.Height, base.ClientSize.Height))
			{
				num -= SystemInformation.VerticalScrollBarWidth;
			}
			int num3 = Math.Max(0, base.VerticalScroll.Value);
			int val = Math.Max(0, num2 - base.ClientSize.Height);
			num3 = Math.Min(num3, val);
			base.AutoScrollPosition = new Point(-base.AutoScrollPosition.X, num3);
			this.UpdateChildControlsWithProposedWidth(num);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003D3A0 File Offset: 0x0003B5A0
		private int UpdateChildControlsWithProposedWidth(int proposedControlWidth)
		{
			int num = 0;
			int x = LayoutHelper.IsRightToLeft(this) ? base.Padding.Right : base.Padding.Left;
			this.TabStop = false;
			Control control = null;
			Control control2 = null;
			for (int i = 0; i < base.Controls.Count; i++)
			{
				control2 = base.Controls[i];
				control2.TabIndex = i;
				if (control2.Visible)
				{
					int y = (control == null) ? control2.Top : (control.Top + control.Height + control.Margin.Bottom + control2.Margin.Top);
					control2.SetBounds(x, y, proposedControlWidth, control2.Height);
					control = control2;
					num += control2.Height + control2.Margin.Bottom;
					this.TabStop |= control2.TabStop;
				}
				else
				{
					control2.Width = proposedControlWidth;
				}
			}
			return num - control2.Margin.Bottom;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003D4B3 File Offset: 0x0003B6B3
		protected virtual void AdjustLocationOfChildControls(LayoutEventArgs levent)
		{
			if (base.Controls.Count == 0)
			{
				return;
			}
			this.AdjustChildControlsToHideHorizontalScrollBar();
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003D4CC File Offset: 0x0003B6CC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			ScrollEventType scrollEventType = ScrollEventType.EndScroll;
			Keys keys = keyData & ~(Keys.Shift | Keys.Control);
			if (base.VScroll)
			{
				int num = 0;
				switch (keys)
				{
				case Keys.Prior:
					num = -base.VerticalScroll.LargeChange;
					scrollEventType = ScrollEventType.LargeIncrement;
					break;
				case Keys.Next:
					num = base.VerticalScroll.LargeChange;
					scrollEventType = ScrollEventType.LargeDecrement;
					break;
				case Keys.End:
					num = base.VerticalScroll.Maximum - base.VerticalScroll.Value;
					scrollEventType = ScrollEventType.Last;
					break;
				case Keys.Home:
					num = -base.VerticalScroll.Value;
					scrollEventType = ScrollEventType.First;
					break;
				case Keys.Up:
					num = -base.VerticalScroll.SmallChange;
					scrollEventType = ScrollEventType.SmallIncrement;
					break;
				case Keys.Down:
					num = base.VerticalScroll.SmallChange;
					scrollEventType = ScrollEventType.SmallDecrement;
					break;
				}
				if (num != 0)
				{
					int num2 = Math.Max(base.VerticalScroll.Minimum, Math.Min(base.VerticalScroll.Value + num, base.VerticalScroll.Maximum));
					if (num2 != base.VerticalScroll.Value)
					{
						int oldValue = -base.AutoScrollPosition.Y;
						base.AutoScrollPosition = new Point(-base.AutoScrollPosition.X, num2);
						if (scrollEventType != ScrollEventType.EndScroll)
						{
							ScrollEventArgs se = new ScrollEventArgs(scrollEventType, oldValue, num2, ScrollOrientation.VerticalScroll);
							this.OnScroll(se);
						}
						return true;
					}
				}
			}
			if (base.HScroll)
			{
				int num3 = 0;
				switch (keys)
				{
				case Keys.Left:
					num3 = -base.HorizontalScroll.SmallChange;
					scrollEventType = ScrollEventType.SmallDecrement;
					break;
				case Keys.Right:
					num3 = base.HorizontalScroll.SmallChange;
					scrollEventType = ScrollEventType.SmallIncrement;
					break;
				}
				if (num3 != 0)
				{
					int num4 = Math.Max(base.HorizontalScroll.Minimum, Math.Min(base.HorizontalScroll.Value + num3, base.HorizontalScroll.Maximum));
					if (num4 != base.HorizontalScroll.Value)
					{
						int oldValue2 = -base.AutoScrollPosition.X;
						base.AutoScrollPosition = new Point(num4, -base.AutoScrollPosition.Y);
						if (scrollEventType != ScrollEventType.EndScroll)
						{
							ScrollEventArgs se2 = new ScrollEventArgs(scrollEventType, oldValue2, num4, ScrollOrientation.HorizontalScroll);
							this.OnScroll(se2);
						}
						return true;
					}
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x0400062C RID: 1580
		private bool resumeLayout;
	}
}
