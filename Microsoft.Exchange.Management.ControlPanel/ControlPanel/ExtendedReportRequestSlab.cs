using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003F6 RID: 1014
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("ExtendedReportRequestSlab", "Microsoft.Exchange.Management.ControlPanel.Client.ExtendedReportRequest.js")]
	public class ExtendedReportRequestSlab : SlabControl, IScriptControl
	{
		// Token: 0x0600337F RID: 13183 RVA: 0x000A0C8E File Offset: 0x0009EE8E
		public ExtendedReportRequestSlab()
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000A0C9C File Offset: 0x0009EE9C
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

		// Token: 0x06003381 RID: 13185 RVA: 0x000A0CC0 File Offset: 0x0009EEC0
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

		// Token: 0x06003382 RID: 13186 RVA: 0x000A0D0D File Offset: 0x0009EF0D
		public virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000A0D1C File Offset: 0x0009EF1C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<ExtendedReportRequestSlab>(this);
			}
			if (base.FieldValidationAssistantExtender != null)
			{
				this.searchParamsFvaCanvas = (HtmlGenericControl)this.dockpanel.FindControl("searchParamsFvaCanvas");
				base.FieldValidationAssistantExtender.TargetControlID = this.searchParamsFvaCanvas.UniqueID;
			}
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000A0D84 File Offset: 0x0009EF84
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

		// Token: 0x06003385 RID: 13189 RVA: 0x000A0DE4 File Offset: 0x0009EFE4
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

		// Token: 0x06003386 RID: 13190 RVA: 0x000A0E74 File Offset: 0x0009F074
		private void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("SubmitButton", this.submitButton.ClientID, true);
			descriptor.AddElementProperty("CloseButton", this.closeButton.ClientID, true);
			descriptor.AddComponentProperty("FromEditor", this.senderMatches.ClientID, true);
			descriptor.AddComponentProperty("ToEditor", this.recipientMatches.ClientID, true);
			descriptor.AddElementProperty("MessageIDTextBox", this.messageIDTextBox.ClientID, true);
			descriptor.AddElementProperty("ClientIPTextBox", this.originalClientIPTextbox.ClientID, true);
			descriptor.AddElementProperty("ReportTitleTextBox", this.reportTitleTextbox.ClientID, true);
			string value = ExDateTime.UtcNow.ToUserExDateTime().ToString();
			descriptor.AddProperty("ServerDate", value, true);
			descriptor.AddElementProperty("NotifyAddressTextBox", this.emailTextbox.ClientID, true);
			descriptor.AddElementProperty("DdDeliveryStatusEx", this.ddDeliveryStatusEx.ClientID, true);
			descriptor.AddElementProperty("ReportOptionsPanel", "panelOnDemandOptions", true);
			descriptor.AddElementProperty("DateRangePanel", "dateRangePanel", true);
			descriptor.AddComponentProperty("StartDate", this.startDate.ClientID, true);
			descriptor.AddComponentProperty("EndDate", this.endDate.ClientID, true);
			descriptor.AddComponentProperty("DockPanel", this.dockpanel.ClientID, true);
			WebServiceReference webServiceReference = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=ExtendedReportRequest");
			descriptor.AddProperty("ServiceUrl", EcpUrl.ProcessUrl(webServiceReference.ServiceUrl));
		}

		// Token: 0x04002504 RID: 9476
		protected DockPanel2 dockpanel;

		// Token: 0x04002505 RID: 9477
		protected RecipientPickerControl senderMatches;

		// Token: 0x04002506 RID: 9478
		protected RecipientPickerControl recipientMatches;

		// Token: 0x04002507 RID: 9479
		protected DropDownList ddDeliveryStatusEx;

		// Token: 0x04002508 RID: 9480
		protected TextBox messageIDTextBox;

		// Token: 0x04002509 RID: 9481
		protected TextBox originalClientIPTextbox;

		// Token: 0x0400250A RID: 9482
		protected TextBox reportTitleTextbox;

		// Token: 0x0400250B RID: 9483
		protected TextBox emailTextbox;

		// Token: 0x0400250C RID: 9484
		protected HtmlButton submitButton;

		// Token: 0x0400250D RID: 9485
		protected HtmlButton closeButton;

		// Token: 0x0400250E RID: 9486
		protected HtmlGenericControl searchParamsFvaCanvas;

		// Token: 0x0400250F RID: 9487
		protected DateTimePicker startDate;

		// Token: 0x04002510 RID: 9488
		protected DateTimePicker endDate;
	}
}
