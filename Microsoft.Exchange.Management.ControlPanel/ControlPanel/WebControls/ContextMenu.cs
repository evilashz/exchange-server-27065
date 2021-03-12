using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000577 RID: 1399
	public class ContextMenu : WebControl
	{
		// Token: 0x0600411F RID: 16671 RVA: 0x000C6630 File Offset: 0x000C4830
		public ContextMenu(CommandCollection commands)
		{
			foreach (Command command in commands)
			{
				if (command.Visible)
				{
					MenuItem item;
					if (command is SeparatorCommand)
					{
						item = new MenuSeparator();
					}
					else
					{
						item = new ContextMenuItem(command);
					}
					this.Items.Add(item);
				}
			}
		}

		// Token: 0x17002542 RID: 9538
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x000C66A4 File Offset: 0x000C48A4
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002543 RID: 9539
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x000C66A8 File Offset: 0x000C48A8
		public IList<MenuItem> Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new List<MenuItem>();
				}
				return this.items;
			}
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x000C66C4 File Offset: 0x000C48C4
		protected override void CreateChildControls()
		{
			base.EnsureID();
			foreach (MenuItem child in this.Items)
			{
				this.Controls.Add(child);
			}
			base.Attributes.Add("role", "menu");
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x000C673C File Offset: 0x000C493C
		public string ToJavaScript()
		{
			StringBuilder stringBuilder = new StringBuilder("new ContextMenu(");
			stringBuilder.Append(string.Format("$get('{0}'),[", this.ClientID));
			stringBuilder.Append(string.Join(",", (from o in this.Items
			select o.ToJavaScript()).ToArray<string>()));
			stringBuilder.Append("])");
			return stringBuilder.ToString();
		}

		// Token: 0x04002B30 RID: 11056
		private IList<MenuItem> items;
	}
}
