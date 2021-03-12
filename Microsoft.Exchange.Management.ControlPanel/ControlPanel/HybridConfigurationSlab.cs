using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000211 RID: 529
	[ClientScriptResource("HybridConfigurationSlab", "Microsoft.Exchange.Management.ControlPanel.Client.OrgSettings.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class HybridConfigurationSlab : SlabControl, IScriptControl
	{
		// Token: 0x060026F2 RID: 9970 RVA: 0x00079C5B File Offset: 0x00077E5B
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<HybridConfigurationSlab>(this);
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x00079C80 File Offset: 0x00077E80
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.Render(writer);
			writer.RenderEndTag();
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x00079CD4 File Offset: 0x00077ED4
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

		// Token: 0x060026F5 RID: 9973 RVA: 0x00079D21 File Offset: 0x00077F21
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(base.GetType());
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x00079D30 File Offset: 0x00077F30
		private void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("EnableButton", this.btnEnable.ClientID, true);
			descriptor.AddElementProperty("EditButton", this.btnEdit.ClientID, true);
			descriptor.AddElementProperty("IsGallatinCheckbox", this.chkIsHostedOnGallatin.ClientID, true);
			descriptor.AddProperty("LinkToCrossPremiseWorldWide", CrossPremiseUtil.OnPremiseLinkToOffice365WorldWide);
			descriptor.AddProperty("LinkToCrossPremiseGallatin", CrossPremiseUtil.OnPremiseLinkToOffice365Gallatin);
		}

		// Token: 0x04001FB5 RID: 8117
		protected HtmlInputButton btnEnable;

		// Token: 0x04001FB6 RID: 8118
		protected HtmlInputButton btnEdit;

		// Token: 0x04001FB7 RID: 8119
		protected CheckBox chkIsHostedOnGallatin;
	}
}
