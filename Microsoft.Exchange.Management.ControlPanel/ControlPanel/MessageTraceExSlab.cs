using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200021D RID: 541
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("MessageTraceExSlab", "Microsoft.Exchange.Management.ControlPanel.Client.MessageTrace.js")]
	public class MessageTraceExSlab : SlabControl, IScriptControl
	{
		// Token: 0x06002756 RID: 10070 RVA: 0x0007B6BA File Offset: 0x000798BA
		public MessageTraceExSlab()
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x0007B6C8 File Offset: 0x000798C8
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

		// Token: 0x06002758 RID: 10072 RVA: 0x0007B6EC File Offset: 0x000798EC
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

		// Token: 0x06002759 RID: 10073 RVA: 0x0007B739 File Offset: 0x00079939
		public virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0007B748 File Offset: 0x00079948
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<MessageTraceExSlab>(this);
			}
			if (base.FieldValidationAssistantExtender != null)
			{
				this.searchParamsFvaCanvas = (HtmlGenericControl)this.dockpanel.FindControl("searchParamsFvaCanvas");
				base.FieldValidationAssistantExtender.TargetControlID = this.searchParamsFvaCanvas.UniqueID;
			}
			ExTimeZone exTimeZone = ExTimeZone.UtcTimeZone;
			if (RbacPrincipal.Current.UserTimeZone != null)
			{
				exTimeZone = RbacPrincipal.Current.UserTimeZone;
			}
			else if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName("UTC", out exTimeZone))
			{
				exTimeZone = ExTimeZone.UtcTimeZone;
			}
			foreach (ExTimeZone exTimeZone2 in ExTimeZoneEnumerator.Instance)
			{
				string text = RtlUtil.ConvertToDecodedBidiString(exTimeZone2.LocalizableDisplayName.ToString(CultureInfo.CurrentCulture), RtlUtil.IsRtl);
				this.ddTimeZone.Items.Add(new ListItem(text, exTimeZone2.Id)
				{
					Selected = (exTimeZone.Id == exTimeZone2.Id)
				});
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0007B878 File Offset: 0x00079A78
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

		// Token: 0x0600275C RID: 10076 RVA: 0x0007B8D8 File Offset: 0x00079AD8
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

		// Token: 0x0600275D RID: 10077 RVA: 0x0007B968 File Offset: 0x00079B68
		private void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("SearchButton", this.searchButton.ClientID, true);
			descriptor.AddElementProperty("ClearButton", this.clearButton.ClientID, true);
			descriptor.AddComponentProperty("FromEditor", this.senderMatches.ClientID, true);
			descriptor.AddComponentProperty("ToEditor", this.recipientMatches.ClientID, true);
			descriptor.AddElementProperty("MessageIDTextBox", this.messageIDTextBox.ClientID, true);
			descriptor.AddElementProperty("ClientIPTextBox", this.originalClientIPTextbox.ClientID, true);
			descriptor.AddElementProperty("ReportTitleTextBox", this.reportTitleTextbox.ClientID, true);
			descriptor.AddElementProperty("NotifyAddressTextBox", this.emailTextbox.ClientID, true);
			descriptor.AddElementProperty("DdMessageTimeFrame", this.ddMessageTimeframe.ClientID, true);
			descriptor.AddElementProperty("DdDeliveryStatus", this.ddDeliveryStatus.ClientID, true);
			descriptor.AddElementProperty("DdDeliveryStatusEx", this.ddDeliveryStatusEx.ClientID, true);
			descriptor.AddElementProperty("DirectionDropdown", this.ddDirection.ClientID, true);
			descriptor.AddElementProperty("TimeZoneDropdown", this.ddTimeZone.ClientID, true);
			descriptor.AddElementProperty("ReportOptionsPanel", "panelOnDemandOptions", true);
			descriptor.AddElementProperty("DateRangePanel", "dateRangePanel", true);
			descriptor.AddElementProperty("IncludeReportDetails", this.chkReportDetails.ClientID, true);
			descriptor.AddComponentProperty("StartDate", this.startDate.ClientID, true);
			descriptor.AddComponentProperty("EndDate", this.endDate.ClientID, true);
			descriptor.AddComponentProperty("DockPanel", this.dockpanel.ClientID, true);
			WebServiceReference webServiceReference = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=MessageTraceEx");
			descriptor.AddProperty("ServiceUrl", EcpUrl.ProcessUrl(webServiceReference.ServiceUrl));
		}

		// Token: 0x04001FE8 RID: 8168
		protected DockPanel2 dockpanel;

		// Token: 0x04001FE9 RID: 8169
		protected RecipientPickerControl senderMatches;

		// Token: 0x04001FEA RID: 8170
		protected RecipientPickerControl recipientMatches;

		// Token: 0x04001FEB RID: 8171
		protected DropDownList ddMessageTimeframe;

		// Token: 0x04001FEC RID: 8172
		protected DropDownList ddDeliveryStatus;

		// Token: 0x04001FED RID: 8173
		protected DropDownList ddDeliveryStatusEx;

		// Token: 0x04001FEE RID: 8174
		protected DropDownList ddTimeZone;

		// Token: 0x04001FEF RID: 8175
		protected DropDownList ddDirection;

		// Token: 0x04001FF0 RID: 8176
		protected TextBox messageIDTextBox;

		// Token: 0x04001FF1 RID: 8177
		protected TextBox originalClientIPTextbox;

		// Token: 0x04001FF2 RID: 8178
		protected TextBox reportTitleTextbox;

		// Token: 0x04001FF3 RID: 8179
		protected TextBox emailTextbox;

		// Token: 0x04001FF4 RID: 8180
		protected HtmlButton searchButton;

		// Token: 0x04001FF5 RID: 8181
		protected HtmlButton clearButton;

		// Token: 0x04001FF6 RID: 8182
		protected HtmlGenericControl searchParamsFvaCanvas;

		// Token: 0x04001FF7 RID: 8183
		protected DateTimePicker startDate;

		// Token: 0x04001FF8 RID: 8184
		protected DateTimePicker endDate;

		// Token: 0x04001FF9 RID: 8185
		protected CheckBox chkReportDetails;
	}
}
