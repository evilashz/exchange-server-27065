using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000321 RID: 801
	internal abstract class DropDownCombo
	{
		// Token: 0x06001E60 RID: 7776 RVA: 0x000AF834 File Offset: 0x000ADA34
		protected DropDownCombo(string id, bool showOnValueMouseDown)
		{
			this.id = id;
			this.showOnValueMouseDown = showOnValueMouseDown;
			this.sessionContext = OwaContext.Current.SessionContext;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000AF861 File Offset: 0x000ADA61
		protected DropDownCombo(string id) : this(id, true)
		{
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x000AF86B File Offset: 0x000ADA6B
		// (set) Token: 0x06001E63 RID: 7779 RVA: 0x000AF873 File Offset: 0x000ADA73
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x000AF87C File Offset: 0x000ADA7C
		// (set) Token: 0x06001E65 RID: 7781 RVA: 0x000AF884 File Offset: 0x000ADA84
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x000AF88D File Offset: 0x000ADA8D
		// (set) Token: 0x06001E67 RID: 7783 RVA: 0x000AF895 File Offset: 0x000ADA95
		public string ClassName
		{
			get
			{
				return this.className;
			}
			set
			{
				this.className = value;
			}
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x000AF89E File Offset: 0x000ADA9E
		public void Render(TextWriter writer)
		{
			this.Render(writer, !this.enabled);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x000AF8B0 File Offset: 0x000ADAB0
		public void Render(TextWriter writer, bool disabled)
		{
			this.enabled = !disabled;
			writer.Write("<div id=\"");
			writer.Write(this.id);
			writer.Write("\"");
			writer.Write(" class=\"");
			if (this.ClassName != null)
			{
				writer.Write("cmb ");
				writer.Write(this.className);
			}
			else
			{
				writer.Write("cmb");
			}
			writer.Write("\" ");
			this.RenderExpandoData(writer);
			writer.Write(">");
			this.sessionContext.RenderThemeImage(writer, ThemeFileId.DownButton2, null, new object[]
			{
				"id=\"divCmbDd\"",
				this.enabled ? "tabIndex=\"0\"" : string.Empty
			});
			writer.Write("<div class=\"cmbVis\"><div class=\"cmbCont\">");
			writer.Write("<img class=\"cmbVASp\">");
			writer.Write("<span id=\"spanCmbSel\">");
			this.RenderSelectedValue(writer);
			writer.Write("</span>");
			writer.Write("</div></div>");
			this.RenderDropControl(writer);
			writer.Write("</div>");
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000AF9C3 File Offset: 0x000ADBC3
		protected virtual void RenderExpandoData(TextWriter writer)
		{
			writer.Write(" fEnbld=");
			writer.Write(this.enabled ? "1" : "0");
			if (this.showOnValueMouseDown)
			{
				writer.Write(" fd=1 style=\"cursor:default;\"");
			}
		}

		// Token: 0x06001E6B RID: 7787
		protected abstract void RenderDropControl(TextWriter writer);

		// Token: 0x06001E6C RID: 7788
		protected abstract void RenderSelectedValue(TextWriter writer);

		// Token: 0x04001670 RID: 5744
		private string id;

		// Token: 0x04001671 RID: 5745
		private bool enabled = true;

		// Token: 0x04001672 RID: 5746
		private string className;

		// Token: 0x04001673 RID: 5747
		private bool showOnValueMouseDown;

		// Token: 0x04001674 RID: 5748
		protected ISessionContext sessionContext;
	}
}
