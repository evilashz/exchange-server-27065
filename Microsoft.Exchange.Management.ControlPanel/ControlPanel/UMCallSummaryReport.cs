using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004B4 RID: 1204
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("UMCallSummaryReport", "Microsoft.Exchange.Management.ControlPanel.Client.UnifiedMessaging.js")]
	public class UMCallSummaryReport : SlabControl, IScriptControl
	{
		// Token: 0x06003B7E RID: 15230 RVA: 0x000B3974 File Offset: 0x000B1B74
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Command command = this.listView.Commands.FindCommandByName("Refresh");
			command.Visible = false;
			ScriptManager current = ScriptManager.GetCurrent(this.Page);
			current.RegisterScriptControl<UMCallSummaryReport>(this);
			if (base.FieldValidationAssistantExtender != null)
			{
				base.FieldValidationAssistantExtender.Canvas = this.callSummaryReportFVACanvas.ClientID;
				base.FieldValidationAssistantExtender.TargetControlID = this.callSummaryReportFVACanvas.UniqueID;
			}
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000B39EC File Offset: 0x000B1BEC
		protected override void Render(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.Render(writer);
			writer.RenderEndTag();
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000B3A24 File Offset: 0x000B1C24
		protected void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			foreach (object obj in base.Attributes.Keys)
			{
				string text = (string)obj;
				writer.AddAttribute(text, base.Attributes[text]);
			}
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000B3AB4 File Offset: 0x000B1CB4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.FillGroupByDropDown();
			this.FillDialPlanAndGWDropDown();
			this.SetupFilterBindings();
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000B3ACF File Offset: 0x000B1CCF
		public virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x000B3ADC File Offset: 0x000B1CDC
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			ClientScriptResourceAttribute clientScriptResourceAttribute = (ClientScriptResourceAttribute)TypeDescriptor.GetAttributes(this)[typeof(ClientScriptResourceAttribute)];
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor(clientScriptResourceAttribute.ComponentType, this.ClientID);
			this.BuildScriptDescriptor(scriptControlDescriptor);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x000B3B2C File Offset: 0x000B1D2C
		private void FillGroupByDropDown()
		{
			this.ddlGroupBy.Items.Add(new ListItem(Strings.GroupByDay, GroupBy.Day.ToString()));
			this.ddlGroupBy.Items.Add(new ListItem(Strings.GroupByMonth, GroupBy.Month.ToString()));
			this.ddlGroupBy.Items.Add(new ListItem(Strings.GroupByAll, GroupBy.Total.ToString()));
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000B3BB8 File Offset: 0x000B1DB8
		private void FillDialPlanAndGWDropDown()
		{
			this.ddlDialPlan.Items.Add(new ListItem(Strings.AllDialplans, string.Empty));
			this.ddlIPGateway.Items.Add(new ListItem(Strings.AllGateways, string.Empty));
			SortOptions sortOptions = new SortOptions();
			sortOptions.PropertyName = "DisplayName";
			sortOptions.Direction = SortDirection.Ascending;
			WebServiceReference webServiceReference = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=UMIPGatewayService");
			PowerShellResults<JsonDictionary<object>> list = webServiceReference.GetList(null, sortOptions);
			WebServiceReference webServiceReference2 = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=UMDialPlanService");
			PowerShellResults<JsonDictionary<object>> list2 = webServiceReference2.GetList(null, sortOptions);
			if (!list.Succeeded || !list2.Succeeded)
			{
				return;
			}
			Dictionary<string, Identity> dictionary = new Dictionary<string, Identity>();
			if (list.Output != null && list.Output.Length > 0)
			{
				JsonDictionary<object>[] output = list.Output;
				for (int i = 0; i < output.Length; i++)
				{
					Dictionary<string, object> dictionary2 = output[i];
					string text = (string)dictionary2["DisplayName"];
					Identity identity = (Identity)dictionary2["Identity"];
					dictionary.Add(text, identity);
					this.ddlIPGateway.Items.Add(new ListItem(text, identity.RawIdentity));
				}
			}
			if (dictionary.Count > 0)
			{
				GWDropDownItems gwdropDownItems = new GWDropDownItems();
				gwdropDownItems.GatewayInfo = dictionary.Values.ToArray<Identity>();
				this.dialPlanToGatewaysMapping.Add(string.Empty, gwdropDownItems);
			}
			if (list2.Output != null && list2.Output.Length > 0)
			{
				JsonDictionary<object>[] output2 = list2.Output;
				for (int j = 0; j < output2.Length; j++)
				{
					Dictionary<string, object> dictionary3 = output2[j];
					this.ddlDialPlan.Items.Add(new ListItem((string)dictionary3["DisplayName"], ((Identity)dictionary3["Identity"]).RawIdentity));
					IEnumerable<object> enumerable = (IEnumerable<object>)dictionary3["UMIPGateway"];
					if (enumerable != null && enumerable.Any<object>())
					{
						List<Identity> list3 = new List<Identity>();
						foreach (object obj in enumerable)
						{
							Identity identity2 = (Identity)obj;
							Identity item;
							if (dictionary.TryGetValue(identity2.DisplayName, out item))
							{
								list3.Add(item);
							}
						}
						GWDropDownItems gwdropDownItems2 = new GWDropDownItems();
						gwdropDownItems2.GatewayInfo = list3;
						this.dialPlanToGatewaysMapping.Add(((Identity)dictionary3["Identity"]).RawIdentity, gwdropDownItems2);
					}
				}
			}
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000B3E80 File Offset: 0x000B2080
		private void SetupFilterBindings()
		{
			BindingCollection filterParameters = this.listViewDataSource.FilterParameters;
			ClientControlBinding clientControlBinding = new ClientControlBinding(this.ddlDialPlan, "value");
			clientControlBinding.Name = "UMDialPlan";
			ClientControlBinding clientControlBinding2 = new ClientControlBinding(this.ddlGroupBy, "value");
			clientControlBinding2.Name = "GroupBy";
			ClientControlBinding clientControlBinding3 = new ClientControlBinding(this.ddlIPGateway, "value");
			clientControlBinding3.Name = "UMIPGateway";
			filterParameters.Add(clientControlBinding);
			filterParameters.Add(clientControlBinding2);
			filterParameters.Add(clientControlBinding3);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000B3F04 File Offset: 0x000B2104
		private void BuildScriptDescriptor(ScriptControlDescriptor descriptor)
		{
			descriptor.AddElementProperty("GroupByDropDown", this.ddlGroupBy.ClientID, true);
			descriptor.AddElementProperty("DialPlanDropDown", this.ddlDialPlan.ClientID, true);
			descriptor.AddElementProperty("IPGatewayDropDown", this.ddlIPGateway.ClientID, true);
			descriptor.AddComponentProperty("ListView", this.listView.ClientID, true);
			descriptor.AddComponentProperty("ListViewDataSource", this.listViewDataSource.ClientID, true);
			descriptor.AddProperty("DialPlanToGatewaysMapping", this.dialPlanToGatewaysMapping);
		}

		// Token: 0x0400276E RID: 10094
		private Dictionary<string, GWDropDownItems> dialPlanToGatewaysMapping = new Dictionary<string, GWDropDownItems>();

		// Token: 0x0400276F RID: 10095
		protected DropDownList ddlGroupBy;

		// Token: 0x04002770 RID: 10096
		protected DropDownList ddlDialPlan;

		// Token: 0x04002771 RID: 10097
		protected DropDownList ddlIPGateway;

		// Token: 0x04002772 RID: 10098
		protected WebServiceListSource listViewDataSource;

		// Token: 0x04002773 RID: 10099
		protected Microsoft.Exchange.Management.ControlPanel.WebControls.ListView listView;

		// Token: 0x04002774 RID: 10100
		protected HtmlGenericControl callSummaryReportFVACanvas;
	}
}
