using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000020 RID: 32
	internal static class LayoutHelper
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00008506 File Offset: 0x00006706
		public static bool CultureInfoIsRightToLeft
		{
			get
			{
				return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008517 File Offset: 0x00006717
		public static void Convert(Control control)
		{
			if (LayoutHelper.CultureInfoIsRightToLeft)
			{
				if (control.RightToLeft != RightToLeft.Yes)
				{
					control.RightToLeft = RightToLeft.Yes;
				}
			}
			else if (control.RightToLeft == RightToLeft.Yes)
			{
				control.RightToLeft = RightToLeft.No;
			}
			LayoutHelper.ConvertLayout(control);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008548 File Offset: 0x00006748
		public static DialogResult ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			if (LayoutHelper.CultureInfoIsRightToLeft)
			{
				return MessageBox.Show(text, caption, buttons, icon, defaultButton, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
			}
			return MessageBox.Show(text, caption, buttons, icon, defaultButton);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008570 File Offset: 0x00006770
		private static void ConvertLayout(Control control)
		{
			Form form = control as Form;
			ListView listView = control as ListView;
			ProgressBar progressBar = control as ProgressBar;
			TabControl tabControl = control as TabControl;
			TrackBar trackBar = control as TrackBar;
			TreeView treeView = control as TreeView;
			bool cultureInfoIsRightToLeft = LayoutHelper.CultureInfoIsRightToLeft;
			if (form != null && form.RightToLeftLayout != cultureInfoIsRightToLeft)
			{
				form.RightToLeftLayout = cultureInfoIsRightToLeft;
				foreach (object obj in control.Controls)
				{
					Control control2 = (Control)obj;
					foreach (object obj2 in control2.Controls)
					{
						Control control3 = (Control)obj2;
						LayoutHelper.Mirror(control3);
					}
				}
				return;
			}
			if (listView != null && listView.RightToLeftLayout != cultureInfoIsRightToLeft)
			{
				listView.RightToLeftLayout = cultureInfoIsRightToLeft;
			}
			if (progressBar != null && progressBar.RightToLeftLayout != cultureInfoIsRightToLeft)
			{
				progressBar.RightToLeftLayout = cultureInfoIsRightToLeft;
			}
			if (tabControl != null && tabControl.RightToLeftLayout != cultureInfoIsRightToLeft)
			{
				tabControl.RightToLeftLayout = cultureInfoIsRightToLeft;
			}
			if (trackBar != null && trackBar.RightToLeftLayout != cultureInfoIsRightToLeft)
			{
				trackBar.RightToLeftLayout = cultureInfoIsRightToLeft;
			}
			if (treeView != null && treeView.RightToLeftLayout != cultureInfoIsRightToLeft)
			{
				treeView.RightToLeftLayout = cultureInfoIsRightToLeft;
			}
			foreach (object obj3 in control.Controls)
			{
				Control control4 = (Control)obj3;
				LayoutHelper.Mirror(control4);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000872C File Offset: 0x0000692C
		private static AnchorStyles Mirror(AnchorStyles anchor)
		{
			bool flag = (anchor & AnchorStyles.Right) == AnchorStyles.Right;
			bool flag2 = (anchor & AnchorStyles.Left) == AnchorStyles.Left;
			if (flag)
			{
				anchor |= AnchorStyles.Left;
			}
			else
			{
				anchor &= ~AnchorStyles.Left;
			}
			if (flag2)
			{
				anchor |= AnchorStyles.Right;
			}
			else
			{
				anchor &= ~AnchorStyles.Right;
			}
			return anchor;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008768 File Offset: 0x00006968
		private static void Mirror(Control control)
		{
			switch (control.Dock)
			{
			case DockStyle.None:
				control.Anchor = LayoutHelper.Mirror(control.Anchor);
				control.Location = LayoutHelper.MirrorLocation(control);
				break;
			case DockStyle.Left:
				control.Dock = DockStyle.Right;
				break;
			case DockStyle.Right:
				control.Dock = DockStyle.Left;
				break;
			}
			foreach (object obj in control.Controls)
			{
				Control control2 = (Control)obj;
				LayoutHelper.Mirror(control2);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008814 File Offset: 0x00006A14
		private static Point MirrorLocation(Control control)
		{
			return new Point(control.Parent.ClientSize.Width - control.Left, control.Top);
		}
	}
}
