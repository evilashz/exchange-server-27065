using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005E9 RID: 1513
	[ClientScriptResource("GroupedCheckBoxList", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class GroupedCheckBoxList : ScriptControlBase
	{
		// Token: 0x17002645 RID: 9797
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x000CCED2 File Offset: 0x000CB0D2
		// (set) Token: 0x060043CF RID: 17359 RVA: 0x000CCEDA File Offset: 0x000CB0DA
		public List<GroupHeader> Groups { get; set; }

		// Token: 0x17002646 RID: 9798
		// (get) Token: 0x060043D0 RID: 17360 RVA: 0x000CCEE3 File Offset: 0x000CB0E3
		// (set) Token: 0x060043D1 RID: 17361 RVA: 0x000CCEEB File Offset: 0x000CB0EB
		private GroupHeader DefaultGroup { get; set; }

		// Token: 0x17002647 RID: 9799
		// (get) Token: 0x060043D2 RID: 17362 RVA: 0x000CCEF4 File Offset: 0x000CB0F4
		// (set) Token: 0x060043D3 RID: 17363 RVA: 0x000CCEFC File Offset: 0x000CB0FC
		[DefaultValue("Ungrouped")]
		public string DefaultGroupText { get; set; }

		// Token: 0x17002648 RID: 9800
		// (get) Token: 0x060043D4 RID: 17364 RVA: 0x000CCF05 File Offset: 0x000CB105
		// (set) Token: 0x060043D5 RID: 17365 RVA: 0x000CCF0D File Offset: 0x000CB10D
		[DefaultValue("GetList")]
		public string WebServiceMethodName { get; set; }

		// Token: 0x17002649 RID: 9801
		// (get) Token: 0x060043D6 RID: 17366 RVA: 0x000CCF16 File Offset: 0x000CB116
		// (set) Token: 0x060043D7 RID: 17367 RVA: 0x000CCF1E File Offset: 0x000CB11E
		[DefaultValue(null)]
		[UrlProperty("*.svc")]
		public WebServiceReference GroupWebService { get; set; }

		// Token: 0x1700264A RID: 9802
		// (get) Token: 0x060043D8 RID: 17368 RVA: 0x000CCF27 File Offset: 0x000CB127
		// (set) Token: 0x060043D9 RID: 17369 RVA: 0x000CCF2F File Offset: 0x000CB12F
		[DefaultValue(false)]
		public bool ReadOnly { get; set; }

		// Token: 0x1700264B RID: 9803
		// (get) Token: 0x060043DA RID: 17370 RVA: 0x000CCF38 File Offset: 0x000CB138
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BindingCollection FilterParameters
		{
			get
			{
				return this.filterParameters;
			}
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x000CCF54 File Offset: 0x000CB154
		protected override void OnLoad(EventArgs e)
		{
			this.DefaultGroup = new GroupHeader
			{
				ID = "Ungrouped",
				Text = (this.DefaultGroupText ?? Strings.DefaultGroupText)
			};
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x000CCF93 File Offset: 0x000CB193
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x000CCFAC File Offset: 0x000CB1AC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.InvokeDataItemWebService();
			Dictionary<GroupHeader, List<GroupedCheckBoxListItem>> dictionary = this.GroupDataItems();
			this.Groups.Add(this.DefaultGroup);
			this.CssClass += " GroupedCheckBoxControl";
			if (this.ReadOnly)
			{
				this.CssClass += " ReadOnlyGroupedCheckBoxControl";
			}
			this.CreateGroupControls(dictionary, this);
			if (dictionary.Count == 0)
			{
				Label label = new Label();
				label.Text = Strings.NoGroupedItems;
				this.Controls.Add(label);
			}
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x000CD044 File Offset: 0x000CB244
		internal static Panel CreateSimplePanel(string className, string divContent)
		{
			return new Panel
			{
				CssClass = className,
				Controls = 
				{
					new LiteralControl(divContent)
				}
			};
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x000CD070 File Offset: 0x000CB270
		private void InvokeDataItemWebService()
		{
			MethodInfo method = this.GroupWebService.ServiceType.GetMethod(this.WebServiceMethodName ?? "GetList");
			try
			{
				object filter = this.GetFilter();
				MethodBase methodBase = method;
				object serviceInstance = this.GroupWebService.ServiceInstance;
				object[] array = new object[2];
				array[0] = filter;
				PowerShellResults powerShellResults = (PowerShellResults)methodBase.Invoke(serviceInstance, array);
				if (powerShellResults.Succeeded)
				{
					IEnumerable source = (IEnumerable)powerShellResults;
					List<GroupedCheckBoxListItem> list = new List<GroupedCheckBoxListItem>();
					foreach (GroupedCheckBoxListItem item in source.Cast<GroupedCheckBoxListItem>())
					{
						list.Add(item);
					}
					this.results = new PowerShellResults<GroupedCheckBoxListItem>
					{
						Output = list.ToArray()
					};
				}
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x000CD160 File Offset: 0x000CB360
		private object GetFilter()
		{
			object obj = null;
			if (this.FilterParameters != null && this.FilterParameters.Count > 0)
			{
				Type @interface = this.GroupWebService.ServiceType.GetInterface(typeof(IGetListService<, >).FullName);
				Type type = @interface.GetGenericArguments()[0];
				obj = Activator.CreateInstance(type);
				foreach (Binding binding in this.FilterParameters)
				{
					ISupportServerSideEvaluate supportServerSideEvaluate = binding as ISupportServerSideEvaluate;
					type.GetProperty(binding.Name).SetValue(obj, supportServerSideEvaluate.Value, null);
				}
			}
			return obj;
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x000CD21C File Offset: 0x000CB41C
		private Dictionary<GroupHeader, List<GroupedCheckBoxListItem>> GroupDataItems()
		{
			Dictionary<GroupHeader, List<GroupedCheckBoxListItem>> dictionary = new Dictionary<GroupHeader, List<GroupedCheckBoxListItem>>();
			if (this.results.Failed || this.results.Output == null)
			{
				return dictionary;
			}
			List<GroupedCheckBoxListItem> list = new List<GroupedCheckBoxListItem>();
			foreach (GroupedCheckBoxListItem groupedCheckBoxListItem in this.results.Output)
			{
				bool flag = false;
				foreach (GroupHeader groupHeader in this.Groups)
				{
					if (groupedCheckBoxListItem.Group == groupHeader.ID)
					{
						this.AddItemToGroup(groupedCheckBoxListItem, groupHeader, dictionary);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(groupedCheckBoxListItem);
				}
			}
			if (list.Count > 0)
			{
				dictionary[this.DefaultGroup] = new List<GroupedCheckBoxListItem>(list);
			}
			return dictionary;
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x000CD304 File Offset: 0x000CB504
		private void AddItemToGroup(GroupedCheckBoxListItem item, GroupHeader group, Dictionary<GroupHeader, List<GroupedCheckBoxListItem>> groupings)
		{
			if (!groupings.ContainsKey(group))
			{
				groupings[group] = new List<GroupedCheckBoxListItem>();
			}
			groupings[group].Add(item);
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x000CD328 File Offset: 0x000CB528
		private void CreateGroupControls(Dictionary<GroupHeader, List<GroupedCheckBoxListItem>> groups, Control parent)
		{
			foreach (GroupHeader groupHeader in this.Groups)
			{
				if (groups.ContainsKey(groupHeader))
				{
					Panel panel = new Panel();
					Panel panel2 = panel;
					panel2.CssClass += " GroupedCheckBoxGroup";
					parent.Controls.Add(panel);
					this.CreateGroupHeaderControl(groupHeader, panel);
					this.CreateGroupItemControls(groups[groupHeader], panel);
				}
			}
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x000CD3BC File Offset: 0x000CB5BC
		private void CreateGroupHeaderControl(GroupHeader header, Panel groupPanel)
		{
			Panel child = GroupedCheckBoxList.CreateSimplePanel("GroupedCheckBoxGroupCaption", header.Text);
			groupPanel.Controls.Add(child);
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x000CD3E8 File Offset: 0x000CB5E8
		private void CreateGroupItemControls(IEnumerable<GroupedCheckBoxListItem> items, Control parent)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("ul");
			htmlGenericControl.Attributes.Add("role", "group");
			parent.Controls.Add(htmlGenericControl);
			foreach (GroupedCheckBoxListItem groupedCheckBoxListItem in items)
			{
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("li");
				htmlGenericControl.Controls.Add(htmlGenericControl2);
				if (this.ReadOnly)
				{
					htmlGenericControl2.Attributes.Add("value", groupedCheckBoxListItem.Identity.ToJsonString(null));
				}
				else
				{
					this.CreateCheckboxControl(groupedCheckBoxListItem, htmlGenericControl2);
				}
				this.CreateItemControl(groupedCheckBoxListItem, htmlGenericControl2);
				this.CreateFooterControl(htmlGenericControl2);
			}
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x000CD4AC File Offset: 0x000CB6AC
		private void CreateCheckboxControl(GroupedCheckBoxListItem item, HtmlGenericControl parent)
		{
			CheckBox checkBox = new CheckBox();
			CheckBox checkBox2 = checkBox;
			checkBox2.CssClass += " GroupedCheckBox";
			checkBox.InputAttributes.Add("value", item.Identity.ToJsonString(null));
			checkBox.ID = item.Identity.RawIdentity;
			parent.Controls.Add(checkBox);
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x000CD510 File Offset: 0x000CB710
		private void CreateItemControl(GroupedCheckBoxListItem item, HtmlGenericControl parent)
		{
			Panel panel = new Panel();
			Panel panel2 = panel;
			panel2.CssClass += " GroupedCheckBoxItem";
			panel.ID = item.Identity.RawIdentity + "_label";
			panel.Attributes.Add("aria-hidden", "false");
			parent.Controls.Add(panel);
			Panel child = GroupedCheckBoxList.CreateSimplePanel("GroupedCheckBoxItemCaption", item.Name);
			panel.Controls.Add(child);
			Panel child2 = GroupedCheckBoxList.CreateSimplePanel("GroupedCheckBoxItemDescription", item.Description);
			panel.Controls.Add(child2);
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x000CD5B0 File Offset: 0x000CB7B0
		private void CreateFooterControl(HtmlGenericControl listItem)
		{
			Panel panel = new Panel();
			Panel panel2 = panel;
			panel2.CssClass += " GroupedCheckBoxFooter";
			listItem.Controls.Add(panel);
		}

		// Token: 0x1700264C RID: 9804
		// (get) Token: 0x060043EA RID: 17386 RVA: 0x000CD5E5 File Offset: 0x000CB7E5
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x000CD5E9 File Offset: 0x000CB7E9
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("ReadOnly", this.ReadOnly);
		}

		// Token: 0x04002DC6 RID: 11718
		private PowerShellResults<GroupedCheckBoxListItem> results;

		// Token: 0x04002DC7 RID: 11719
		private BindingCollection filterParameters = new BindingCollection();
	}
}
