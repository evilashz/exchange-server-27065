using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200029C RID: 668
	[ClientScriptResource("EditAccountProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Users.js")]
	public sealed class EditAccountProperties : Properties
	{
		// Token: 0x17001D68 RID: 7528
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x00087636 File Offset: 0x00085836
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x0008763E File Offset: 0x0008583E
		public WebServiceMethod CancelPhotoWebServiceMethod { get; private set; }

		// Token: 0x17001D69 RID: 7529
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x00087647 File Offset: 0x00085847
		// (set) Token: 0x06002B5D RID: 11101 RVA: 0x0008764F File Offset: 0x0008584F
		public WebServiceMethod SavePhotoWebServiceMethod { get; private set; }

		// Token: 0x17001D6A RID: 7530
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x00087658 File Offset: 0x00085858
		// (set) Token: 0x06002B5F RID: 11103 RVA: 0x00087660 File Offset: 0x00085860
		public WebServiceMethod RemovePhotoWebServiceMethod { get; private set; }

		// Token: 0x17001D6B RID: 7531
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x00087669 File Offset: 0x00085869
		// (set) Token: 0x06002B61 RID: 11105 RVA: 0x00087671 File Offset: 0x00085871
		public WebServiceReference UserPhotoServiceUrl { get; set; }

		// Token: 0x17001D6C RID: 7532
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x0008767A File Offset: 0x0008587A
		// (set) Token: 0x06002B63 RID: 11107 RVA: 0x00087682 File Offset: 0x00085882
		public string PhotoSectionId { get; set; }

		// Token: 0x06002B64 RID: 11108 RVA: 0x0008768C File Offset: 0x0008588C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CancelPhotoWebServiceMethod = new WebServiceMethod();
			this.CancelPhotoWebServiceMethod.ID = "CancelUserPhoto";
			this.CancelPhotoWebServiceMethod.ServiceUrl = this.UserPhotoServiceUrl;
			this.CancelPhotoWebServiceMethod.Method = "CancelPhoto";
			this.CancelPhotoWebServiceMethod.ParameterNames = WebServiceParameterNames.Identity;
			this.Controls.Add(this.CancelPhotoWebServiceMethod);
			this.SavePhotoWebServiceMethod = new WebServiceMethod();
			this.SavePhotoWebServiceMethod.ID = "SaveUserPhoto";
			this.SavePhotoWebServiceMethod.ServiceUrl = this.UserPhotoServiceUrl;
			this.SavePhotoWebServiceMethod.Method = "SavePhoto";
			this.SavePhotoWebServiceMethod.ParameterNames = WebServiceParameterNames.Identity;
			this.Controls.Add(this.SavePhotoWebServiceMethod);
			this.RemovePhotoWebServiceMethod = new WebServiceMethod();
			this.RemovePhotoWebServiceMethod.ID = "RemoveUserPhoto";
			this.RemovePhotoWebServiceMethod.ServiceUrl = this.UserPhotoServiceUrl;
			this.RemovePhotoWebServiceMethod.Method = "RemovePhoto";
			this.RemovePhotoWebServiceMethod.ParameterNames = WebServiceParameterNames.Identity;
			this.Controls.Add(this.RemovePhotoWebServiceMethod);
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000877AA File Offset: 0x000859AA
		protected override void OnInit(EventArgs e)
		{
			this.ApplySimplifiedPhotoExperienceLayout();
			base.OnInit(e);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000877BC File Offset: 0x000859BC
		protected override void OnPreRender(EventArgs e)
		{
			this.ApplySimplifiedPhotoExperienceLayout();
			base.OnPreRender(e);
			foreach (ImageUploader imageUploader in this.GetVisibleImageUploaders(base.Sections[this.PhotoSectionId]))
			{
				if (this.SimplifiedPhotoExperience())
				{
					imageUploader.InitStateAsEditClicked = true;
				}
				imageUploader.CancelWebServiceMethod = this.CancelPhotoWebServiceMethod;
				imageUploader.SaveWebServiceMethod = this.SavePhotoWebServiceMethod;
				imageUploader.RemoveWebServiceMethod = this.RemovePhotoWebServiceMethod;
			}
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x00087854 File Offset: 0x00085A54
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "EditAccountProperties";
			return scriptDescriptor;
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x00087B18 File Offset: 0x00085D18
		private IEnumerable<ImageUploader> GetVisibleImageUploaders(Control parent)
		{
			if (parent.Visible)
			{
				ImageUploader pl = parent as ImageUploader;
				if (pl != null)
				{
					yield return pl;
				}
				foreach (object obj in parent.Controls)
				{
					Control subControl = (Control)obj;
					foreach (ImageUploader c in this.GetVisibleImageUploaders(subControl))
					{
						yield return c;
					}
				}
			}
			yield break;
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x00087B3C File Offset: 0x00085D3C
		private void ApplySimplifiedPhotoExperienceLayout()
		{
			if (!this.SimplifiedPhotoExperience())
			{
				return;
			}
			this.RemoveSectionsExceptPhoto();
			base.CaptionTextField = "ChangePhotoCaption";
			((BaseForm)this.Page).HideFieldValidationAssistant = true;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x00087B6C File Offset: 0x00085D6C
		private void RemoveSectionsExceptPhoto()
		{
			if (base.Sections == null || base.Sections.Count == 0)
			{
				return;
			}
			for (int i = base.Sections.Count - 1; i >= 0; i--)
			{
				if (!this.PhotoSectionId.Equals(base.Sections[i].ID, StringComparison.OrdinalIgnoreCase))
				{
					base.Sections.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x00087BD2 File Offset: 0x00085DD2
		private bool SimplifiedPhotoExperience()
		{
			return this.Page.Request.QueryString.AllKeys.Contains("chgPhoto", StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x04002197 RID: 8599
		private const string SimplifiedPhotoExperienceCaption = "ChangePhotoCaption";
	}
}
