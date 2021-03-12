using System;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003C6 RID: 966
	public class MultipartToolbar : Toolbar
	{
		// Token: 0x06002402 RID: 9218 RVA: 0x000CFEE9 File Offset: 0x000CE0E9
		public MultipartToolbar(params MultipartToolbar.ToolbarInfo[] toolbars)
		{
			this.toolbars = toolbars;
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x000CFEF8 File Offset: 0x000CE0F8
		public override bool HasBigButton
		{
			get
			{
				foreach (MultipartToolbar.ToolbarInfo toolbarInfo in this.toolbars)
				{
					if (toolbarInfo.Toolbar.HasBigButton)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000CFF34 File Offset: 0x000CE134
		public override void Render(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			foreach (MultipartToolbar.ToolbarInfo toolbarInfo in this.toolbars)
			{
				writer.Write("<div id=\"");
				writer.Write(toolbarInfo.ContainerId);
				writer.Write("\"");
				if (toolbarInfo.Toolbar.IsRightAligned)
				{
					writer.Write(" fRtAlgn=\"1\"");
				}
				writer.Write(">");
				toolbarInfo.Toolbar.Render(writer);
				writer.Write("</div>");
			}
			writer.Write("<div class=\"tfFill ");
			writer.Write(this.HasBigButton ? "tfBigHeight" : "tfHeight");
			writer.Write("\"></div>");
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000CFFF4 File Offset: 0x000CE1F4
		protected override void RenderButtons()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040018F8 RID: 6392
		private MultipartToolbar.ToolbarInfo[] toolbars;

		// Token: 0x020003C7 RID: 967
		public class ToolbarInfo
		{
			// Token: 0x06002406 RID: 9222 RVA: 0x000CFFFB File Offset: 0x000CE1FB
			public ToolbarInfo(Toolbar toolbar, string containerId)
			{
				this.Toolbar = toolbar;
				this.ContainerId = containerId;
			}

			// Token: 0x1700099B RID: 2459
			// (get) Token: 0x06002407 RID: 9223 RVA: 0x000D0011 File Offset: 0x000CE211
			// (set) Token: 0x06002408 RID: 9224 RVA: 0x000D0019 File Offset: 0x000CE219
			public Toolbar Toolbar { get; set; }

			// Token: 0x1700099C RID: 2460
			// (get) Token: 0x06002409 RID: 9225 RVA: 0x000D0022 File Offset: 0x000CE222
			// (set) Token: 0x0600240A RID: 9226 RVA: 0x000D002A File Offset: 0x000CE22A
			public string ContainerId { get; set; }
		}
	}
}
