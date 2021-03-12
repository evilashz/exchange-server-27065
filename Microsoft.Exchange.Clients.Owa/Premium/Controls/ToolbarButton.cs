using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200042A RID: 1066
	public class ToolbarButton
	{
		// Token: 0x060025FC RID: 9724 RVA: 0x000DC356 File Offset: 0x000DA556
		public ToolbarButton(string command, ToolbarButtonFlags flags, Strings.IDs textId, ThemeFileId image, Strings.IDs tooltipId)
		{
			this.command = command;
			this.flags = flags;
			this.textId = textId;
			this.tooltipId = tooltipId;
			this.image = image;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000DC383 File Offset: 0x000DA583
		public ToolbarButton(string command, ToolbarButtonFlags flags, Strings.IDs textId, ThemeFileId image) : this(command, flags, textId, image, textId)
		{
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000DC391 File Offset: 0x000DA591
		public ToolbarButton(string command, ToolbarButtonFlags flags, ThemeFileId image) : this(command, flags, -1018465893, image)
		{
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000DC3A1 File Offset: 0x000DA5A1
		public ToolbarButton(string command, ToolbarButtonFlags flags) : this(command, flags, -1018465893, ThemeFileId.None)
		{
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000DC3B1 File Offset: 0x000DA5B1
		public ToolbarButton(string command, Strings.IDs textId) : this(command, ToolbarButtonFlags.Text, textId, ThemeFileId.None)
		{
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x000DC3BD File Offset: 0x000DA5BD
		public ToolbarButtonFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x000DC3C5 File Offset: 0x000DA5C5
		public Strings.IDs TooltipId
		{
			get
			{
				return this.tooltipId;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x000DC3CD File Offset: 0x000DA5CD
		public Strings.IDs TextId
		{
			get
			{
				return this.textId;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x000DC3D5 File Offset: 0x000DA5D5
		public ThemeFileId Image
		{
			get
			{
				return this.image;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x000DC3DD File Offset: 0x000DA5DD
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000DC3E5 File Offset: 0x000DA5E5
		public ToolbarButton[] ToggleWithButtons
		{
			get
			{
				return this.toggleWithButtons;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x000DC3ED File Offset: 0x000DA5ED
		public ToolbarButton[] SwapWithButtons
		{
			get
			{
				return this.swapWithButtons;
			}
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000DC3F5 File Offset: 0x000DA5F5
		public void SetSwapButtons(params ToolbarButton[] swapWithButtons)
		{
			if (swapWithButtons == null)
			{
				throw new ArgumentNullException("swapWithButtons");
			}
			this.swapWithButtons = swapWithButtons;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000DC40C File Offset: 0x000DA60C
		public void SetToggleButtons(params ToolbarButton[] toggleWithButtons)
		{
			if (toggleWithButtons == null)
			{
				throw new ArgumentNullException("toggleWithButtons");
			}
			this.toggleWithButtons = toggleWithButtons;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000DC423 File Offset: 0x000DA623
		public virtual void RenderControl(TextWriter writer)
		{
		}

		// Token: 0x04001A46 RID: 6726
		private Strings.IDs textId;

		// Token: 0x04001A47 RID: 6727
		private Strings.IDs tooltipId;

		// Token: 0x04001A48 RID: 6728
		private ThemeFileId image;

		// Token: 0x04001A49 RID: 6729
		private string command;

		// Token: 0x04001A4A RID: 6730
		private ToolbarButtonFlags flags;

		// Token: 0x04001A4B RID: 6731
		private ToolbarButton[] toggleWithButtons;

		// Token: 0x04001A4C RID: 6732
		private ToolbarButton[] swapWithButtons;
	}
}
