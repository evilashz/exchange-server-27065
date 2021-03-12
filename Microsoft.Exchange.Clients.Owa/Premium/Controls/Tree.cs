using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000382 RID: 898
	internal abstract class Tree
	{
		// Token: 0x060021FB RID: 8699 RVA: 0x000C2443 File Offset: 0x000C0643
		protected Tree(UserContext userContext, TreeNode rootNode)
		{
			this.userContext = userContext;
			this.rootNode = rootNode;
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x000C2464 File Offset: 0x000C0664
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000C246C File Offset: 0x000C066C
		// (set) Token: 0x060021FE RID: 8702 RVA: 0x000C2474 File Offset: 0x000C0674
		internal string CustomAttributes
		{
			get
			{
				return this.customAttributes;
			}
			set
			{
				this.customAttributes = value;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x000C247D File Offset: 0x000C067D
		// (set) Token: 0x06002200 RID: 8704 RVA: 0x000C2485 File Offset: 0x000C0685
		internal string ErrDiv
		{
			get
			{
				return this.errDiv;
			}
			set
			{
				this.errDiv = value;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000C248E File Offset: 0x000C068E
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x000C2496 File Offset: 0x000C0696
		internal string ErrHideId
		{
			get
			{
				return this.errHideId;
			}
			set
			{
				this.errHideId = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x000C249F File Offset: 0x000C069F
		internal TreeNode RootNode
		{
			get
			{
				return this.rootNode;
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000C24A8 File Offset: 0x000C06A8
		internal void Render(TextWriter writer)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "Tree.Render()");
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.RenderOuterDivStart(writer);
			this.RootNode.Render(writer, 0);
			this.RenderOuterDivEnd(writer);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000C24F4 File Offset: 0x000C06F4
		protected virtual void RenderAdditionalProperties(TextWriter writer)
		{
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x000C24F6 File Offset: 0x000C06F6
		internal virtual string Id
		{
			get
			{
				return "divTr";
			}
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000C2500 File Offset: 0x000C0700
		private void RenderOuterDivStart(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<div fTR=1 id=\"");
			writer.Write(this.Id);
			writer.Write("\"");
			if (!string.IsNullOrEmpty(this.ErrDiv))
			{
				writer.Write(" _errDiv=\"");
				writer.Write(this.ErrDiv);
				writer.Write("\"");
			}
			if (!string.IsNullOrEmpty(this.ErrHideId))
			{
				writer.Write(" _errHd=\"");
				writer.Write(this.ErrHideId);
				writer.Write("\"");
			}
			if (!string.IsNullOrEmpty(this.CustomAttributes))
			{
				writer.Write(" ");
				writer.Write(this.CustomAttributes);
			}
			this.RenderAdditionalProperties(writer);
			writer.Write(">");
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000C25D1 File Offset: 0x000C07D1
		private void RenderOuterDivEnd(TextWriter writer)
		{
			writer.Write("</div>");
		}

		// Token: 0x040017F4 RID: 6132
		private const string TreeDivId = "divTr";

		// Token: 0x040017F5 RID: 6133
		private readonly UserContext userContext;

		// Token: 0x040017F6 RID: 6134
		private readonly TreeNode rootNode;

		// Token: 0x040017F7 RID: 6135
		private string customAttributes = string.Empty;

		// Token: 0x040017F8 RID: 6136
		private string errDiv;

		// Token: 0x040017F9 RID: 6137
		private string errHideId;
	}
}
