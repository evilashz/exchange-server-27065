using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005F5 RID: 1525
	[ClientScriptResource("ImageUploader", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:ImageUploader runat=server></{0}:ImageUploader>")]
	public class ImageUploader : AjaxUploader
	{
		// Token: 0x06004461 RID: 17505 RVA: 0x000CEB04 File Offset: 0x000CCD04
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.editFileButton != null)
			{
				this.editFileButton.Attributes["NoRoleState"] = "Hide";
			}
			if (this.deleteButton != null)
			{
				this.deleteButton.Attributes["NoRoleState"] = "Hide";
			}
		}

		// Token: 0x1700266B RID: 9835
		// (get) Token: 0x06004462 RID: 17506 RVA: 0x000CEB5C File Offset: 0x000CCD5C
		// (set) Token: 0x06004463 RID: 17507 RVA: 0x000CEB64 File Offset: 0x000CCD64
		public WebServiceMethod CancelWebServiceMethod { get; set; }

		// Token: 0x1700266C RID: 9836
		// (get) Token: 0x06004464 RID: 17508 RVA: 0x000CEB6D File Offset: 0x000CCD6D
		// (set) Token: 0x06004465 RID: 17509 RVA: 0x000CEB75 File Offset: 0x000CCD75
		public WebServiceMethod SaveWebServiceMethod { get; set; }

		// Token: 0x1700266D RID: 9837
		// (get) Token: 0x06004466 RID: 17510 RVA: 0x000CEB7E File Offset: 0x000CCD7E
		// (set) Token: 0x06004467 RID: 17511 RVA: 0x000CEB86 File Offset: 0x000CCD86
		public WebServiceMethod RemoveWebServiceMethod { get; set; }

		// Token: 0x1700266E RID: 9838
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x000CEB8F File Offset: 0x000CCD8F
		// (set) Token: 0x06004469 RID: 17513 RVA: 0x000CEB97 File Offset: 0x000CCD97
		public string ImageElementId { get; set; }

		// Token: 0x1700266F RID: 9839
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x000CEBA0 File Offset: 0x000CCDA0
		// (set) Token: 0x0600446B RID: 17515 RVA: 0x000CEBA8 File Offset: 0x000CCDA8
		public string RemovingPreviewPhotoText { get; set; }

		// Token: 0x0600446C RID: 17516 RVA: 0x000CEBB4 File Offset: 0x000CCDB4
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			Identity dataContract = Identity.FromExecutingUserId();
			Image image = (Image)this.Parent.FindControl(this.ImageElementId);
			descriptor.AddElementProperty("Image", image.ClientID);
			descriptor.AddComponentProperty("CancelWebServiceMethod", this.CancelWebServiceMethod.ClientID);
			descriptor.AddComponentProperty("SaveWebServiceMethod", this.SaveWebServiceMethod.ClientID);
			descriptor.AddComponentProperty("RemoveWebServiceMethod", this.RemoveWebServiceMethod.ClientID);
			descriptor.AddScriptProperty("ObjectIdentity", dataContract.ToJsonString(null));
			descriptor.AddProperty("RemovingPreviewPhotoText", this.RemovingPreviewPhotoText);
		}
	}
}
