using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000BD RID: 189
	[RequiredScript(typeof(Properties))]
	[ClientScriptResource("CalendarPublishing", "Microsoft.Exchange.Management.ControlPanel.Client.Calendar.js")]
	public class CalendarPublishing : SlabControl, IScriptControl
	{
		// Token: 0x06001CAD RID: 7341 RVA: 0x00058E9C File Offset: 0x0005709C
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			string displayName = CalendarSharingsSlab.GetDisplayName(this.Context.Request, "fldID");
			base.Title = string.Format("{0} - {1}", base.Title, displayName);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00058EE0 File Offset: 0x000570E0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<CalendarPublishing>(this);
			}
			if (base.FieldValidationAssistantExtender != null)
			{
				base.FieldValidationAssistantExtender.Canvas = this.calendarPublishingPropertiesWrapper.ClientID;
				base.FieldValidationAssistantExtender.TargetControlID = this.calendarPublishingPropertiesWrapper.UniqueID;
			}
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00058F41 File Offset: 0x00057141
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			base.Render(writer);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00058F7C File Offset: 0x0005717C
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

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00058FC9 File Offset: 0x000571C9
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00058FD8 File Offset: 0x000571D8
		private void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("SubscriptionUrlLabel", this.txtSubscriptionUrl_label.ClientID, true);
			descriptor.AddElementProperty("SubscriptionUrl", this.txtSubscriptionUrl.ClientID, true);
			descriptor.AddElementProperty("ViewUrlLabel", this.txtViewUrl_label.ClientID, true);
			descriptor.AddElementProperty("ViewUrl", this.txtViewUrl.ClientID, true);
		}

		// Token: 0x04001BA9 RID: 7081
		protected PropertiesWrapper calendarPublishingPropertiesWrapper;

		// Token: 0x04001BAA RID: 7082
		protected Label txtSubscriptionUrl_label;

		// Token: 0x04001BAB RID: 7083
		protected TextBox txtSubscriptionUrl;

		// Token: 0x04001BAC RID: 7084
		protected Label txtViewUrl_label;

		// Token: 0x04001BAD RID: 7085
		protected TextBox txtViewUrl;
	}
}
