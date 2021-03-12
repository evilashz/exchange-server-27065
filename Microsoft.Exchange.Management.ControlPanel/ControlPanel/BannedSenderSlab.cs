using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000045 RID: 69
	[ClientScriptResource("BannedSenderSlab", "Microsoft.Exchange.Management.ControlPanel.Client.Antispam.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class BannedSenderSlab : SlabControl, IScriptControl
	{
		// Token: 0x060019A1 RID: 6561 RVA: 0x00052408 File Offset: 0x00050608
		public BannedSenderSlab()
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00052416 File Offset: 0x00050616
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00052420 File Offset: 0x00050620
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

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005246D File Offset: 0x0005066D
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0005247C File Offset: 0x0005067C
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

		// Token: 0x060019A6 RID: 6566 RVA: 0x000524DC File Offset: 0x000506DC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<BannedSenderSlab>(this);
			}
			if (base.FieldValidationAssistantExtender != null)
			{
				base.FieldValidationAssistantExtender.Canvas = this.searchParamsFvaCanvas.ClientID;
				base.FieldValidationAssistantExtender.TargetControlID = this.searchParamsFvaCanvas.UniqueID;
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00052540 File Offset: 0x00050740
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

		// Token: 0x060019A8 RID: 6568 RVA: 0x000525D0 File Offset: 0x000507D0
		protected void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("SenderDomainTextArea", this.txtSenderDomainList.ClientID, true);
			descriptor.AddElementProperty("SearchButton", this.searchButton.ClientID, true);
			descriptor.AddElementProperty("ClearButton", this.clearButton.ClientID, true);
			descriptor.AddComponentProperty("BannedSenderCollectionEditor", this.bannedSenderCollectionEditor.ClientID, true);
			descriptor.AddComponentProperty("CollectionEditorRefreshMethod", this.bannedSenderDataSource.RefreshWebServiceMethod.ClientID, true);
			WebServiceReference webServiceReference = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=BannedSender");
			descriptor.AddProperty("ServiceUrl", EcpUrl.ProcessUrl(webServiceReference.ServiceUrl));
		}

		// Token: 0x04001ACC RID: 6860
		protected TextArea txtSenderDomainList;

		// Token: 0x04001ACD RID: 6861
		protected HtmlButton searchButton;

		// Token: 0x04001ACE RID: 6862
		protected HtmlButton clearButton;

		// Token: 0x04001ACF RID: 6863
		protected HtmlGenericControl searchParamsFvaCanvas;

		// Token: 0x04001AD0 RID: 6864
		protected EcpCollectionEditor bannedSenderCollectionEditor;

		// Token: 0x04001AD1 RID: 6865
		protected WebServiceListSource bannedSenderDataSource;
	}
}
