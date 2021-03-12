using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200000B RID: 11
	internal sealed class CustomTabControl : TabControl
	{
		// Token: 0x06000047 RID: 71 RVA: 0x0000287C File Offset: 0x00000A7C
		internal CustomTabControl()
		{
			this.AllowDrop = true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002898 File Offset: 0x00000A98
		private int GetTabAt(Point location)
		{
			int result = -1;
			for (int i = 0; i < base.TabCount; i++)
			{
				if (base.GetTabRect(i).Contains(location))
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028D0 File Offset: 0x00000AD0
		protected override void OnDragOver(DragEventArgs e)
		{
			base.OnDragOver(e);
			if (!e.Data.GetDataPresent(typeof(CustomTabPage)))
			{
				e.Effect = DragDropEffects.None;
				return;
			}
			e.Effect = DragDropEffects.Move;
			TabPage tabPage = e.Data.GetData(typeof(CustomTabPage)) as CustomTabPage;
			Point point = base.PointToClient(new Point(e.X, e.Y));
			if (this.hoverTabRectangle.Contains(point))
			{
				return;
			}
			int num = base.TabPages.IndexOf(tabPage);
			int tabAt = this.GetTabAt(point);
			if (num != tabAt && tabAt != -1)
			{
				TabPage value = base.TabPages[tabAt];
				base.TabPages[num] = value;
				base.TabPages[tabAt] = tabPage;
				this.Refresh();
			}
			num = this.GetTabAt(point);
			if (num >= 0)
			{
				this.hoverTabRectangle = base.GetTabRect(num);
				return;
			}
			this.hoverTabRectangle = default(Rectangle);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029C1 File Offset: 0x00000BC1
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			if (!drgevent.Data.GetDataPresent(typeof(CustomTabPage)))
			{
				base.OnDragEnter(drgevent);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029E4 File Offset: 0x00000BE4
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				DetailsTemplateTypeService detailsTemplateTypeService = (DetailsTemplateTypeService)this.GetService(typeof(DetailsTemplateTypeService));
				if (detailsTemplateTypeService != null && detailsTemplateTypeService.TemplateType.Equals("Mailbox Agent"))
				{
					return;
				}
				int tabAt = this.GetTabAt(e.Location);
				if (tabAt == -1)
				{
					return;
				}
				TabPage tabPage = base.TabPages[tabAt];
				if (tabPage != null)
				{
					this.hoverTabRectangle = base.GetTabRect(tabAt);
					base.DoDragDrop(tabPage, DragDropEffects.Move);
					if (base.TabPages.IndexOf(tabPage) != tabAt)
					{
						IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
						if (componentChangeService != null)
						{
							componentChangeService.OnComponentChanged(tabPage, null, null, null);
						}
					}
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002A9E File Offset: 0x00000C9E
		[ReadOnly(true)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AA6 File Offset: 0x00000CA6
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			y = (x = 0);
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x04000009 RID: 9
		private Rectangle hoverTabRectangle = default(Rectangle);
	}
}
