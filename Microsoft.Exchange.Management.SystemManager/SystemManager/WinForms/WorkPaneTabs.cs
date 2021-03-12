using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200021E RID: 542
	public class WorkPaneTabs : TabControl
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x000684F4 File Offset: 0x000666F4
		public AbstractResultPane SelectedResultPane
		{
			get
			{
				AbstractResultPane result = null;
				WorkPanePage workPanePage = base.SelectedTab as WorkPanePage;
				if (workPanePage != null)
				{
					result = workPanePage.ResultPane;
				}
				return result;
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0006851C File Offset: 0x0006671C
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			WorkPanePage workPanePage = e.Control as WorkPanePage;
			if (workPanePage.ResultPane != null)
			{
				if (workPanePage.ResultPane.Icon != null)
				{
					workPanePage.ImageIndex = this.AddNewImageIcon(workPanePage.ResultPane.Icon);
				}
				workPanePage.ResultPane.IconChanged += this.ResultPane_IconChanged;
			}
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00068580 File Offset: 0x00066780
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			WorkPanePage workPanePage = e.Control as WorkPanePage;
			if (workPanePage.ResultPane != null)
			{
				if (workPanePage.ResultPane.Icon != null)
				{
					this.ImageIcons.Icons.Remove(workPanePage.ResultPane.GetHashCode().ToString());
				}
				workPanePage.ResultPane.IconChanged -= this.ResultPane_IconChanged;
			}
			base.OnControlRemoved(e);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000685F0 File Offset: 0x000667F0
		private void ResultPane_IconChanged(object sender, EventArgs e)
		{
			AbstractResultPane abstractResultPane = sender as AbstractResultPane;
			if (abstractResultPane != null)
			{
				if (abstractResultPane.Icon == null)
				{
					this.GetWorkPanePage(abstractResultPane).ImageIndex = -1;
					return;
				}
				this.GetWorkPanePage(abstractResultPane).ImageIndex = this.AddNewImageIcon(abstractResultPane.Icon);
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00068635 File Offset: 0x00066835
		private WorkPanePage GetWorkPanePage(AbstractResultPane resultPane)
		{
			return resultPane.Parent as WorkPanePage;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00068644 File Offset: 0x00066844
		private int AddNewImageIcon(Icon icon)
		{
			int num = this.ImageIcons.Icons.IndexOf(icon);
			if (num == -1)
			{
				this.ImageIcons.Icons.Add(Guid.NewGuid().ToString(), icon);
				num = this.ImageIcons.Icons.IndexOf(icon);
			}
			return num;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x0006869E File Offset: 0x0006689E
		private IconLibrary ImageIcons
		{
			get
			{
				if (this.icons == null)
				{
					this.icons = new IconLibrary();
					base.ImageList = this.icons.SmallImageList;
				}
				return this.icons;
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x000686CC File Offset: 0x000668CC
		protected override void OnHandleDestroyed(EventArgs e)
		{
			try
			{
				base.OnHandleDestroyed(e);
			}
			catch (KeyNotFoundException)
			{
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x000686F8 File Offset: 0x000668F8
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x00068700 File Offset: 0x00066900
		public override bool RightToLeftLayout
		{
			get
			{
				return LayoutHelper.IsRightToLeft(this);
			}
			set
			{
			}
		}

		// Token: 0x04000933 RID: 2355
		private IconLibrary icons;
	}
}
