using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200021B RID: 539
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("MessageTraceSlab", "Microsoft.Exchange.Management.ControlPanel.Client.MessageTrace.js")]
	public class MessageTraceSlab : SlabControl, IScriptControl
	{
		// Token: 0x06002747 RID: 10055 RVA: 0x0007AF4F File Offset: 0x0007914F
		public override Control FindControl(string id)
		{
			Control result;
			if ((result = base.FindControl(id)) == null)
			{
				if (this.dockpanel == null)
				{
					return null;
				}
				result = this.dockpanel.FindControl(id);
			}
			return result;
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0007AF74 File Offset: 0x00079174
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

		// Token: 0x06002749 RID: 10057 RVA: 0x0007AFC1 File Offset: 0x000791C1
		public virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x0007AFD0 File Offset: 0x000791D0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<MessageTraceSlab>(this);
			}
			if (base.FieldValidationAssistantExtender != null)
			{
				this.searchParamsFvaCanvas = (HtmlGenericControl)this.dockpanel.FindControl("searchParamsFvaCanvas");
				base.FieldValidationAssistantExtender.TargetControlID = this.searchParamsFvaCanvas.UniqueID;
			}
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x0007B038 File Offset: 0x00079238
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.FieldValidationAssistantExtender != null)
			{
				base.FieldValidationAssistantExtender.Canvas = this.searchParamsFvaCanvas.ClientID;
			}
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.Render(writer);
			writer.RenderEndTag();
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x0007B098 File Offset: 0x00079298
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

		// Token: 0x0600274D RID: 10061 RVA: 0x0007B128 File Offset: 0x00079328
		private void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("SearchButton", this.searchButton.ClientID, true);
			descriptor.AddElementProperty("ClearButton", this.clearButton.ClientID, true);
			descriptor.AddComponentProperty("FromEditor", this.senderMatches.ClientID, true);
			descriptor.AddComponentProperty("ToEditor", this.recipientMatches.ClientID, true);
			descriptor.AddElementProperty("MessageIDTextBox", this.messageIDTextBox.ClientID, true);
			descriptor.AddComponentProperty("ListView", this.listViewSearchResults.ClientID, true);
			descriptor.AddComponentProperty("ListViewDataSource", this.messageTraceDataSource.ClientID, true);
			descriptor.AddComponentProperty("ListViewRefreshMethod", this.messageTraceDataSource.RefreshWebServiceMethod.ClientID, true);
			descriptor.AddElementProperty("DdMessageTimeFrame", this.ddMessageTimeframe.ClientID, true);
			descriptor.AddElementProperty("DdDeliveryStatus", this.ddDeliveryStatus.ClientID, true);
			descriptor.AddElementProperty("CustomLink", this.customLink.ClientID, true);
			ExDateTime dateTimeValue = ExDateTime.UtcNow.ToUserExDateTime();
			dateTimeValue = dateTimeValue.AddMinutes((double)((60 - dateTimeValue.Minute) % 30));
			descriptor.AddProperty("StartDate", dateTimeValue.AddHours(-48.0).ToUserDateTimeGeneralFormatString(), true);
			descriptor.AddProperty("EndDate", dateTimeValue.ToUserDateTimeGeneralFormatString(), true);
			ExTimeZone exTimeZone;
			if (RbacPrincipal.Current.UserTimeZone != null)
			{
				exTimeZone = RbacPrincipal.Current.UserTimeZone;
			}
			else if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName("UTC", out exTimeZone))
			{
				exTimeZone = ExTimeZone.UtcTimeZone;
			}
			descriptor.AddProperty("TimeZone", exTimeZone.Id, true);
			descriptor.AddUrlProperty("PickerFormUrl", "~/Pickers/CustomDateRangePicker.aspx", this);
		}

		// Token: 0x04001FDC RID: 8156
		protected DockPanel2 dockpanel;

		// Token: 0x04001FDD RID: 8157
		protected WebServiceListSource messageTraceDataSource;

		// Token: 0x04001FDE RID: 8158
		protected RecipientPickerControl senderMatches;

		// Token: 0x04001FDF RID: 8159
		protected RecipientPickerControl recipientMatches;

		// Token: 0x04001FE0 RID: 8160
		protected DropDownList ddMessageTimeframe;

		// Token: 0x04001FE1 RID: 8161
		protected DropDownList ddDeliveryStatus;

		// Token: 0x04001FE2 RID: 8162
		protected TextBox messageIDTextBox;

		// Token: 0x04001FE3 RID: 8163
		protected HtmlButton searchButton;

		// Token: 0x04001FE4 RID: 8164
		protected HtmlButton clearButton;

		// Token: 0x04001FE5 RID: 8165
		protected HyperLink customLink;

		// Token: 0x04001FE6 RID: 8166
		protected Microsoft.Exchange.Management.ControlPanel.WebControls.ListView listViewSearchResults;

		// Token: 0x04001FE7 RID: 8167
		protected HtmlGenericControl searchParamsFvaCanvas;
	}
}
