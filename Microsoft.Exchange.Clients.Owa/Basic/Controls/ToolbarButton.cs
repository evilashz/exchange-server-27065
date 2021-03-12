using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000089 RID: 137
	public class ToolbarButton
	{
		// Token: 0x060003CE RID: 974 RVA: 0x00021E4A File Offset: 0x0002004A
		public ToolbarButton(string command, ToolbarButtonFlags flags, Strings.IDs textId, ThemeFileId image)
		{
			this.command = command;
			this.flags = flags;
			this.textId = textId;
			this.image = image;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00021E6F File Offset: 0x0002006F
		public ToolbarButton(ToolbarButtonFlags flags, Strings.IDs textId, ThemeFileId image)
		{
			this.flags = flags;
			this.textId = textId;
			this.image = image;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00021E8C File Offset: 0x0002008C
		public ToolbarButton(string command, ToolbarButtonFlags flags, Strings.IDs textId, ThemeFileId image, string toolTip)
		{
			this.command = command;
			this.flags = flags;
			this.textId = textId;
			this.image = image;
			this.toolTip = toolTip;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00021EB9 File Offset: 0x000200B9
		public string ToolTip
		{
			get
			{
				return this.toolTip;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00021EC1 File Offset: 0x000200C1
		public ToolbarButtonFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00021EC9 File Offset: 0x000200C9
		public Strings.IDs TextId
		{
			get
			{
				return this.textId;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00021ED1 File Offset: 0x000200D1
		public ThemeFileId Image
		{
			get
			{
				return this.image;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00021ED9 File Offset: 0x000200D9
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x040002FA RID: 762
		private Strings.IDs textId;

		// Token: 0x040002FB RID: 763
		private ThemeFileId image;

		// Token: 0x040002FC RID: 764
		private string command;

		// Token: 0x040002FD RID: 765
		private ToolbarButtonFlags flags;

		// Token: 0x040002FE RID: 766
		private string toolTip;
	}
}
