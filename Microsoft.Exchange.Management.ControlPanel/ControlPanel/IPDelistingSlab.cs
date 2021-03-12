using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000043 RID: 67
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("IPDelistingSlab", "Microsoft.Exchange.Management.ControlPanel.Client.Antispam.js")]
	public class IPDelistingSlab : SlabControl, IScriptControl
	{
		// Token: 0x06001998 RID: 6552 RVA: 0x000520D3 File Offset: 0x000502D3
		public IPDelistingSlab()
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000520E1 File Offset: 0x000502E1
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000520EC File Offset: 0x000502EC
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

		// Token: 0x0600199B RID: 6555 RVA: 0x00052139 File Offset: 0x00050339
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00052148 File Offset: 0x00050348
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

		// Token: 0x0600199D RID: 6557 RVA: 0x000521A8 File Offset: 0x000503A8
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<IPDelistingSlab>(this);
			}
			if (base.FieldValidationAssistantExtender != null)
			{
				base.FieldValidationAssistantExtender.Canvas = this.searchParamsFvaCanvas.ClientID;
				base.FieldValidationAssistantExtender.TargetControlID = this.searchParamsFvaCanvas.UniqueID;
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0005220C File Offset: 0x0005040C
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

		// Token: 0x0600199F RID: 6559 RVA: 0x0005229C File Offset: 0x0005049C
		protected void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("IPListTextArea", this.txtIPList.ClientID, true);
			descriptor.AddElementProperty("SearchButton", this.searchButton.ClientID, true);
			descriptor.AddElementProperty("ClearButton", this.clearButton.ClientID, true);
			descriptor.AddComponentProperty("IPDelistingCollectionEditor", this.ipDelistingCollectionEditor.ClientID, true);
			descriptor.AddComponentProperty("CollectionEditorRefreshMethod", this.ipDelistingDataSource.RefreshWebServiceMethod.ClientID, true);
			WebServiceReference webServiceReference = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=IPDelisting");
			descriptor.AddProperty("ServiceUrl", EcpUrl.ProcessUrl(webServiceReference.ServiceUrl));
		}

		// Token: 0x04001AC6 RID: 6854
		protected TextArea txtIPList;

		// Token: 0x04001AC7 RID: 6855
		protected HtmlButton searchButton;

		// Token: 0x04001AC8 RID: 6856
		protected HtmlButton clearButton;

		// Token: 0x04001AC9 RID: 6857
		protected HtmlGenericControl searchParamsFvaCanvas;

		// Token: 0x04001ACA RID: 6858
		protected EcpCollectionEditor ipDelistingCollectionEditor;

		// Token: 0x04001ACB RID: 6859
		protected WebServiceListSource ipDelistingDataSource;
	}
}
