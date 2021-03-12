using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000670 RID: 1648
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("ToolBar", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class ToolBar : ScriptControlBase
	{
		// Token: 0x06004767 RID: 18279 RVA: 0x000D8971 File Offset: 0x000D6B71
		public ToolBar()
		{
			this.items = new List<ToolBarItem>();
		}

		// Token: 0x1700276C RID: 10092
		// (get) Token: 0x06004768 RID: 18280 RVA: 0x000D898F File Offset: 0x000D6B8F
		[MergableProperty(false)]
		[RefreshProperties(RefreshProperties.All)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public CommandCollection Commands
		{
			get
			{
				if (this.commands == null)
				{
					this.commands = new CommandCollection();
				}
				return this.commands;
			}
		}

		// Token: 0x1700276D RID: 10093
		// (get) Token: 0x06004769 RID: 18281 RVA: 0x000D89AA File Offset: 0x000D6BAA
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700276E RID: 10094
		// (get) Token: 0x0600476A RID: 18282 RVA: 0x000D89AE File Offset: 0x000D6BAE
		// (set) Token: 0x0600476B RID: 18283 RVA: 0x000D89B6 File Offset: 0x000D6BB6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700276F RID: 10095
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x000D89BD File Offset: 0x000D6BBD
		// (set) Token: 0x0600476D RID: 18285 RVA: 0x000D89C5 File Offset: 0x000D6BC5
		[DefaultValue(false)]
		public bool RightAlign { get; set; }

		// Token: 0x17002770 RID: 10096
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x000D89CE File Offset: 0x000D6BCE
		// (set) Token: 0x0600476F RID: 18287 RVA: 0x000D89D6 File Offset: 0x000D6BD6
		public string OwnerControlID { get; set; }

		// Token: 0x17002771 RID: 10097
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x000D89DF File Offset: 0x000D6BDF
		// (set) Token: 0x06004771 RID: 18289 RVA: 0x000D89E7 File Offset: 0x000D6BE7
		public string CssForDropDownContextMenu { get; set; }

		// Token: 0x06004772 RID: 18290 RVA: 0x000D89F0 File Offset: 0x000D6BF0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			if (string.IsNullOrEmpty(cssClass))
			{
				this.CssClass = "ToolBar";
			}
			else
			{
				this.CssClass += " ToolBar";
			}
			if (this.RightAlign)
			{
				this.CssClass += " ToolBarRightAlign";
			}
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x000D8A5C File Offset: 0x000D6C5C
		internal void ApplyRolesFilter()
		{
			if (!this.rolesFilterApplied)
			{
				IPrincipal user = this.Context.User;
				for (int i = this.Commands.Count - 1; i >= 0; i--)
				{
					Command command = this.Commands[i];
					if (command is DropDownCommand)
					{
						((DropDownCommand)command).ApplyRolesFilter(user);
					}
					if (!command.IsAccessibleToUser(user))
					{
						this.Commands.RemoveAt(i);
					}
				}
				this.Commands.MakeReadOnly();
				this.rolesFilterApplied = true;
			}
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x000D8AE0 File Offset: 0x000D6CE0
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.ApplyRolesFilter();
			if (!string.IsNullOrEmpty(this.OwnerControlID))
			{
				Control control = this.Parent;
				while (control != null)
				{
					Control control2 = control.FindControl(this.OwnerControlID);
					if (control2 != null)
					{
						this.OwnerControlID = control2.ClientID;
						control = null;
					}
					else
					{
						control = control.Parent;
					}
				}
			}
			base.Attributes.Add("role", "toolbar");
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x000D8B70 File Offset: 0x000D6D70
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			int num = int.MaxValue;
			IEnumerable<Command> enumerable = from cmd in this.Commands
			where cmd.AsMoreOption
			select cmd;
			foreach (Command command in enumerable)
			{
				this.additionalItems.Add(new ContextMenuItem(command));
			}
			Command[] array;
			if (this.RightAlign)
			{
				array = (from cmd in this.Commands.Reverse<Command>()
				where !cmd.AsMoreOption
				select cmd).ToArray<Command>();
			}
			else
			{
				array = (from cmd in this.Commands
				where !cmd.AsMoreOption
				select cmd).ToArray<Command>();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i] is SeparatorCommand))
				{
					bool flag = array[i].SelectionMode == SelectionMode.SelectionIndependent;
					bool flag2 = array[i].Visible && (flag || !array[i].HideOnDisable);
					if (flag2 && i < num)
					{
						num = i;
					}
					if (i > 0)
					{
						ToolBarSeparator toolBarSeparator = new ToolBarSeparator();
						if (!flag2 || num >= i)
						{
							toolBarSeparator.Style.Add(HtmlTextWriterStyle.Display, "none");
						}
						this.items.Add(toolBarSeparator);
						this.Controls.Add(toolBarSeparator);
					}
					ToolBarItem toolBarItem;
					if (array[i] is DropDownCommand)
					{
						toolBarItem = new ToolBarSplitButton((DropDownCommand)array[i])
						{
							HideArrow = ((DropDownCommand)array[i]).HideArrow
						};
					}
					else if (array[i] is InlineSearchBarCommand)
					{
						toolBarItem = new ToolBarMoveButton((InlineSearchBarCommand)array[i]);
					}
					else
					{
						toolBarItem = new ToolBarButton(array[i]);
						ToolBarItem toolBarItem2 = toolBarItem;
						toolBarItem2.CssClass += (flag ? " EnabledToolBarItem" : " DisabledToolBarItem");
					}
					if (!flag2)
					{
						toolBarItem.Style.Add(HtmlTextWriterStyle.Display, "none");
					}
					this.items.Add(toolBarItem);
					this.Controls.Add(toolBarItem);
				}
			}
			base.RenderChildren(writer);
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x000D8DD0 File Offset: 0x000D6FD0
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddScriptProperty("Items", this.BuildClientItems(from item in this.items
			select item.ToJavaScript()));
			descriptor.AddComponentProperty("OwnerControl", this.OwnerControlID, true);
			descriptor.AddProperty("CssForDropDownContextMenu", this.CssForDropDownContextMenu, true);
			descriptor.AddScriptProperty("AdditionalItems", this.BuildClientItems(from item in this.additionalItems
			select item.ToJavaScript()));
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x000D8E7C File Offset: 0x000D707C
		private string BuildClientItems(IEnumerable<string> items)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			stringBuilder.Append(string.Join(",", items.ToArray<string>()));
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04003007 RID: 12295
		private CommandCollection commands;

		// Token: 0x04003008 RID: 12296
		private bool rolesFilterApplied;

		// Token: 0x04003009 RID: 12297
		private List<ToolBarItem> items;

		// Token: 0x0400300A RID: 12298
		private List<MenuItem> additionalItems = new List<MenuItem>();
	}
}
