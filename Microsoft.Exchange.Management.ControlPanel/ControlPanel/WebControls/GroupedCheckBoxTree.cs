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
	// Token: 0x020005EA RID: 1514
	[ClientScriptResource("GroupedCheckBoxTree", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class GroupedCheckBoxTree : ScriptControlBase
	{
		// Token: 0x1700264D RID: 9805
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x000CD608 File Offset: 0x000CB808
		// (set) Token: 0x060043ED RID: 17389 RVA: 0x000CD610 File Offset: 0x000CB810
		public List<GroupHeader> Groups { get; set; }

		// Token: 0x1700264E RID: 9806
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x000CD619 File Offset: 0x000CB819
		// (set) Token: 0x060043EF RID: 17391 RVA: 0x000CD621 File Offset: 0x000CB821
		private GroupHeader DefaultGroup { get; set; }

		// Token: 0x1700264F RID: 9807
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x000CD62A File Offset: 0x000CB82A
		// (set) Token: 0x060043F1 RID: 17393 RVA: 0x000CD632 File Offset: 0x000CB832
		[DefaultValue("Ungrouped")]
		public string DefaultGroupText { get; set; }

		// Token: 0x17002650 RID: 9808
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x000CD63B File Offset: 0x000CB83B
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x000CD643 File Offset: 0x000CB843
		[DefaultValue("GetList")]
		public string WebServiceMethodName { get; set; }

		// Token: 0x17002651 RID: 9809
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x000CD64C File Offset: 0x000CB84C
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x000CD654 File Offset: 0x000CB854
		[DefaultValue(null)]
		[UrlProperty("*.svc")]
		public WebServiceReference GroupWebService { get; set; }

		// Token: 0x17002652 RID: 9810
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x000CD65D File Offset: 0x000CB85D
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BindingCollection FilterParameters
		{
			get
			{
				return this.filterParameters;
			}
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000CD678 File Offset: 0x000CB878
		protected override void OnLoad(EventArgs e)
		{
			this.DefaultGroup = new GroupHeader
			{
				ID = "Ungrouped",
				Text = (this.DefaultGroupText ?? Strings.DefaultGroupText)
			};
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x000CD6B7 File Offset: 0x000CB8B7
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x000CD6D0 File Offset: 0x000CB8D0
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.InvokeDataItemWebService();
			Dictionary<GroupHeader, Dictionary<GroupedCheckBoxTreeItem, List<GroupedCheckBoxTreeItem>>> dictionary = this.BuildTree();
			this.Groups.Add(this.DefaultGroup);
			this.CssClass += " GroupedCheckBoxControl";
			this.CreateGroupControls(dictionary, this);
			if (dictionary.Count == 0)
			{
				Label label = new Label();
				label.Text = Strings.NoGroupedItems;
				this.Controls.Add(label);
			}
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x000CD74C File Offset: 0x000CB94C
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
					List<GroupedCheckBoxTreeItem> list = new List<GroupedCheckBoxTreeItem>();
					foreach (GroupedCheckBoxTreeItem item in source.Cast<GroupedCheckBoxTreeItem>())
					{
						list.Add(item);
					}
					this.results = new PowerShellResults<GroupedCheckBoxTreeItem>
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

		// Token: 0x060043FC RID: 17404 RVA: 0x000CD83C File Offset: 0x000CBA3C
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

		// Token: 0x060043FD RID: 17405 RVA: 0x000CD8F8 File Offset: 0x000CBAF8
		private Dictionary<GroupHeader, Dictionary<GroupedCheckBoxTreeItem, List<GroupedCheckBoxTreeItem>>> BuildTree()
		{
			Dictionary<string, GroupedCheckBoxTreeItem> hash = this.BuildDictionary(this.results.Output);
			Dictionary<GroupHeader, Dictionary<GroupedCheckBoxTreeItem, List<GroupedCheckBoxTreeItem>>> dictionary = new Dictionary<GroupHeader, Dictionary<GroupedCheckBoxTreeItem, List<GroupedCheckBoxTreeItem>>>();
			foreach (GroupedCheckBoxTreeItem groupedCheckBoxTreeItem in this.results.Output)
			{
				if (string.IsNullOrEmpty(groupedCheckBoxTreeItem.Parent))
				{
					GroupHeader headerFor = this.GetHeaderFor(groupedCheckBoxTreeItem);
					if (!dictionary.ContainsKey(headerFor))
					{
						dictionary[headerFor] = new Dictionary<GroupedCheckBoxTreeItem, List<GroupedCheckBoxTreeItem>>();
					}
					if (!dictionary[headerFor].ContainsKey(groupedCheckBoxTreeItem))
					{
						dictionary[headerFor].Add(groupedCheckBoxTreeItem, new List<GroupedCheckBoxTreeItem>());
					}
				}
			}
			foreach (GroupedCheckBoxTreeItem groupedCheckBoxTreeItem2 in this.results.Output)
			{
				if (!string.IsNullOrEmpty(groupedCheckBoxTreeItem2.Parent))
				{
					GroupedCheckBoxTreeItem rootFor = this.GetRootFor(groupedCheckBoxTreeItem2, hash);
					dictionary[this.GetHeaderFor(groupedCheckBoxTreeItem2)][rootFor].Add(groupedCheckBoxTreeItem2);
				}
			}
			return dictionary;
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x000CD9EB File Offset: 0x000CBBEB
		private GroupedCheckBoxTreeItem GetRootFor(GroupedCheckBoxTreeItem item, Dictionary<string, GroupedCheckBoxTreeItem> hash)
		{
			if (item.Parent == null)
			{
				return item;
			}
			return hash[item.Group];
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x000CDA04 File Offset: 0x000CBC04
		private Dictionary<string, GroupedCheckBoxTreeItem> BuildDictionary(GroupedCheckBoxTreeItem[] items)
		{
			Dictionary<string, GroupedCheckBoxTreeItem> dictionary = new Dictionary<string, GroupedCheckBoxTreeItem>();
			foreach (GroupedCheckBoxTreeItem groupedCheckBoxTreeItem in items)
			{
				if (groupedCheckBoxTreeItem.Parent == null)
				{
					dictionary[groupedCheckBoxTreeItem.Group] = groupedCheckBoxTreeItem;
				}
			}
			return dictionary;
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x000CDA64 File Offset: 0x000CBC64
		private GroupHeader GetHeaderFor(GroupedCheckBoxTreeItem item)
		{
			IEnumerable<GroupHeader> source = from x in this.Groups
			where x.ID == item.Group
			select x;
			if (source.Count<GroupHeader>() <= 0)
			{
				return this.DefaultGroup;
			}
			return source.First<GroupHeader>();
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x000CDAAC File Offset: 0x000CBCAC
		private void CreateGroupControls(Dictionary<GroupHeader, Dictionary<GroupedCheckBoxTreeItem, List<GroupedCheckBoxTreeItem>>> tree, Control parent)
		{
			foreach (GroupHeader groupHeader in this.Groups)
			{
				if (tree.ContainsKey(groupHeader))
				{
					Panel panel = new Panel();
					Panel panel2 = panel;
					panel2.CssClass += " GroupedCheckBoxGroup";
					this.Controls.Add(panel);
					this.CreateGroupHeaderControl(groupHeader, panel);
					Dictionary<GroupedCheckBoxTreeItem, List<GroupedCheckBoxTreeItem>> dictionary = tree[groupHeader];
					foreach (GroupedCheckBoxTreeItem groupedCheckBoxTreeItem in dictionary.Keys)
					{
						this.CreateGroupItemControls(groupedCheckBoxTreeItem, dictionary[groupedCheckBoxTreeItem], panel);
					}
				}
			}
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x000CDB8C File Offset: 0x000CBD8C
		private void CreateGroupHeaderControl(GroupHeader header, Panel groupPanel)
		{
			Panel child = GroupedCheckBoxList.CreateSimplePanel("GroupedCheckBoxGroupCaption", header.Text);
			groupPanel.Controls.Add(child);
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x000CDBB8 File Offset: 0x000CBDB8
		private void CreateGroupItemControls(GroupedCheckBoxTreeItem root, IEnumerable<GroupedCheckBoxTreeItem> children, Control parent)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("ul");
			htmlGenericControl.Attributes.Add("role", "group");
			parent.Controls.Add(htmlGenericControl);
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("li");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			CheckBox checkBox = this.CreateCheckboxControl(root, htmlGenericControl2, true);
			this.CreateItemControl(root, htmlGenericControl2);
			this.CreateFooterControl(htmlGenericControl2);
			if (children.Count<GroupedCheckBoxTreeItem>() > 0)
			{
				this.CreateGroupSubList(children, checkBox.ID, htmlGenericControl2);
			}
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000CDC38 File Offset: 0x000CBE38
		private void CreateGroupSubList(IEnumerable<GroupedCheckBoxTreeItem> items, string root, Control parent)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("ul");
			htmlGenericControl.ID = root + "_sublist";
			htmlGenericControl.Attributes.Add("class", " GroupedCheckBoxSubList");
			parent.Controls.Add(htmlGenericControl);
			foreach (GroupedCheckBoxTreeItem item in items)
			{
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("li");
				htmlGenericControl.Controls.Add(htmlGenericControl2);
				this.CreateCheckboxControl(item, htmlGenericControl2, false);
				this.CreateItemControl(item, htmlGenericControl2);
				this.CreateFooterControl(htmlGenericControl2);
			}
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000CDCE8 File Offset: 0x000CBEE8
		private CheckBox CreateCheckboxControl(GroupedCheckBoxTreeItem item, HtmlGenericControl parent, bool root)
		{
			CheckBox checkBox = new CheckBox();
			string value = " GroupedCheckBox " + (root ? " GroupedCheckBoxParent" : string.Empty);
			checkBox.InputAttributes.Add("class", value);
			checkBox.InputAttributes.Add("value", item.Identity.ToJsonString(null));
			checkBox.ID = item.Identity.RawIdentity;
			parent.Controls.Add(checkBox);
			return checkBox;
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000CDD60 File Offset: 0x000CBF60
		private void CreateItemControl(GroupedCheckBoxTreeItem item, HtmlGenericControl parent)
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

		// Token: 0x06004407 RID: 17415 RVA: 0x000CDE00 File Offset: 0x000CC000
		private void CreateFooterControl(HtmlGenericControl listItem)
		{
			Panel panel = new Panel();
			Panel panel2 = panel;
			panel2.CssClass += " GroupedCheckBoxFooter";
			listItem.Controls.Add(panel);
		}

		// Token: 0x17002653 RID: 9811
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x000CDE35 File Offset: 0x000CC035
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000CDE39 File Offset: 0x000CC039
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x04002DCE RID: 11726
		private PowerShellResults<GroupedCheckBoxTreeItem> results;

		// Token: 0x04002DCF RID: 11727
		private BindingCollection filterParameters = new BindingCollection();
	}
}
