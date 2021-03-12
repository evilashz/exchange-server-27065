using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000431 RID: 1073
	internal sealed class SimpleTreeNode : TreeNode
	{
		// Token: 0x060026D3 RID: 9939 RVA: 0x000DE099 File Offset: 0x000DC299
		internal SimpleTreeNode(UserContext userContext, string id) : base(userContext)
		{
			this.id = id;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x000DE0B4 File Offset: 0x000DC2B4
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000DE0BC File Offset: 0x000DC2BC
		public void SetIconSrc(string iconSrc)
		{
			this.iconHTML = string.Format(CultureInfo.InvariantCulture, "<img src=\"{0}\" {{0}}>", new object[]
			{
				Utilities.HtmlEncode(iconSrc)
			});
			this.themeFileId = ThemeFileId.None;
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x000DE0F6 File Offset: 0x000DC2F6
		public void SetIcon(ThemeFileId themeFileId)
		{
			this.themeFileId = themeFileId;
			this.iconHTML = null;
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x000DE106 File Offset: 0x000DC306
		// (set) Token: 0x060026D8 RID: 9944 RVA: 0x000DE10E File Offset: 0x000DC30E
		internal string NodeAdditionalProperties { get; set; }

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x000DE117 File Offset: 0x000DC317
		// (set) Token: 0x060026DA RID: 9946 RVA: 0x000DE11F File Offset: 0x000DC31F
		internal string ClientNodeType { get; set; }

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x000DE128 File Offset: 0x000DC328
		internal override bool Selectable
		{
			get
			{
				return this.selectable;
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000DE130 File Offset: 0x000DC330
		public void SetSelectable(bool value)
		{
			this.selectable = value;
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x000DE139 File Offset: 0x000DC339
		protected override bool HasIcon
		{
			get
			{
				return !string.IsNullOrEmpty(this.iconHTML) || this.themeFileId != ThemeFileId.None;
			}
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000DE158 File Offset: 0x000DC358
		protected override void RenderAdditionalProperties(TextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.ClientNodeType))
			{
				writer.Write(" _t=\"");
				Utilities.HtmlEncode(this.ClientNodeType, writer);
				writer.Write("\"");
			}
			if (!string.IsNullOrEmpty(this.NodeAdditionalProperties))
			{
				writer.Write(" ");
				writer.Write(this.NodeAdditionalProperties);
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000DE1B8 File Offset: 0x000DC3B8
		public void SetContent(string content)
		{
			this.SetContent(content, false);
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000DE1C2 File Offset: 0x000DC3C2
		public void SetContent(string content, bool htmlEncoded)
		{
			if (htmlEncoded)
			{
				this.contentHTML = content;
				return;
			}
			this.contentHTML = Utilities.HtmlEncode(content, true);
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000DE1DC File Offset: 0x000DC3DC
		protected override void RenderContent(TextWriter writer)
		{
			writer.Write(this.contentHTML);
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000DE1EA File Offset: 0x000DC3EA
		protected override void RenderIcon(TextWriter writer, params string[] extraAttributes)
		{
			if (this.iconHTML != null)
			{
				writer.Write(this.iconHTML, extraAttributes);
				return;
			}
			if (this.themeFileId != ThemeFileId.None)
			{
				base.UserContext.RenderThemeImage(writer, this.themeFileId, null, extraAttributes);
			}
		}

		// Token: 0x04001B2C RID: 6956
		private string id;

		// Token: 0x04001B2D RID: 6957
		private string iconHTML;

		// Token: 0x04001B2E RID: 6958
		private ThemeFileId themeFileId;

		// Token: 0x04001B2F RID: 6959
		private bool selectable;

		// Token: 0x04001B30 RID: 6960
		private string contentHTML = string.Empty;
	}
}
