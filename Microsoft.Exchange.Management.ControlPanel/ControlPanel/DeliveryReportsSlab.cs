using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003BA RID: 954
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("DeliveryReportsSlab", "Microsoft.Exchange.Management.ControlPanel.Client.DeliveryReports.js")]
	public class DeliveryReportsSlab : SlabControl, IScriptControl
	{
		// Token: 0x060031E7 RID: 12775 RVA: 0x0009A144 File Offset: 0x00098344
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

		// Token: 0x060031E8 RID: 12776 RVA: 0x0009A191 File Offset: 0x00098391
		public virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x0009A19E File Offset: 0x0009839E
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.SetupFilterBindings();
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x0009A1B0 File Offset: 0x000983B0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<DeliveryReportsSlab>(this);
			}
			if (base.FieldValidationAssistantExtender != null)
			{
				base.FieldValidationAssistantExtender.Canvas = this.searchParamsFvaCanvas.ClientID;
				base.FieldValidationAssistantExtender.TargetControlID = this.searchParamsFvaCanvas.UniqueID;
			}
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x0009A211 File Offset: 0x00098411
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

		// Token: 0x060031EC RID: 12780 RVA: 0x0009A248 File Offset: 0x00098448
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

		// Token: 0x060031ED RID: 12781 RVA: 0x0009A2D8 File Offset: 0x000984D8
		private void SetupFilterBindings()
		{
			BindingCollection filterParameters = this.messageTrackingsearchDataSource.FilterParameters;
			if (this.pickerMailboxToSearch != null)
			{
				filterParameters.Add(new ComponentBinding(this.pickerMailboxToSearch, "value")
				{
					Name = "Identity"
				});
			}
			ClientControlBinding clientControlBinding = new ComponentBinding(this.fromAddress, "value");
			clientControlBinding.Name = "Sender";
			ClientControlBinding clientControlBinding2 = new ComponentBinding(this.toAddress, "value");
			clientControlBinding2.Name = "Recipients";
			ClientControlBinding clientControlBinding3 = new ClientControlBinding(this.subjectTextBox, "value");
			clientControlBinding3.Name = "Subject";
			filterParameters.Add(clientControlBinding);
			filterParameters.Add(clientControlBinding2);
			filterParameters.Add(clientControlBinding3);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x0009A38C File Offset: 0x0009858C
		private void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("SearchButton", this.searchButton.ClientID, true);
			descriptor.AddElementProperty("ClearButton", this.clearButton.ClientID, true);
			descriptor.AddComponentProperty("FromEditor", this.fromAddress.ClientID, true);
			descriptor.AddComponentProperty("ToEditor", this.toAddress.ClientID, true);
			descriptor.AddElementProperty("SubjectTextBox", this.subjectTextBox.ClientID, true);
			descriptor.AddComponentProperty("ListView", this.listViewSearchResults.ClientID, true);
			descriptor.AddComponentProperty("ListViewDataSource", this.messageTrackingsearchDataSource.ClientID, true);
			descriptor.AddComponentProperty("ListViewRefreshMethod", this.messageTrackingsearchDataSource.RefreshWebServiceMethod.ClientID, true);
			descriptor.AddElementProperty("ToAddressRadioButton", this.rbToAddress.ClientID, true);
			descriptor.AddElementProperty("FromAddressRadioButton", this.rbFromAddress.ClientID, true);
			descriptor.AddProperty("FvaResource", base.FVAResource);
			if (this.pickerMailboxToSearch != null)
			{
				descriptor.AddComponentProperty("MailboxPicker", this.pickerMailboxToSearch.ClientID, true);
			}
		}

		// Token: 0x0400242C RID: 9260
		protected WebServiceListSource messageTrackingsearchDataSource;

		// Token: 0x0400242D RID: 9261
		protected RecipientPickerControl fromAddress;

		// Token: 0x0400242E RID: 9262
		protected RecipientPickerControl toAddress;

		// Token: 0x0400242F RID: 9263
		protected TextBox subjectTextBox;

		// Token: 0x04002430 RID: 9264
		protected HtmlButton searchButton;

		// Token: 0x04002431 RID: 9265
		protected HtmlButton clearButton;

		// Token: 0x04002432 RID: 9266
		protected LoginView loginView;

		// Token: 0x04002433 RID: 9267
		protected RadioButton rbToAddress;

		// Token: 0x04002434 RID: 9268
		protected RadioButton rbFromAddress;

		// Token: 0x04002435 RID: 9269
		protected Microsoft.Exchange.Management.ControlPanel.WebControls.ListView listViewSearchResults;

		// Token: 0x04002436 RID: 9270
		protected EcpSingleSelect pickerMailboxToSearch;

		// Token: 0x04002437 RID: 9271
		protected HtmlGenericControl searchParamsFvaCanvas;
	}
}
