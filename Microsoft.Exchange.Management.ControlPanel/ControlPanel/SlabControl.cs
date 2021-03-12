using System;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000029 RID: 41
	public class SlabControl : UserControl, IThemable
	{
		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0004E002 File Offset: 0x0004C202
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x0004E00A File Offset: 0x0004C20A
		public string HelpId
		{
			get
			{
				return this.helpId;
			}
			set
			{
				this.helpId = value;
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0004E013 File Offset: 0x0004C213
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x0004E01B File Offset: 0x0004C21B
		public string Title { get; set; }

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0004E024 File Offset: 0x0004C224
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x0004E02C File Offset: 0x0004C22C
		public string Caption { get; set; }

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0004E035 File Offset: 0x0004C235
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x0004E03D File Offset: 0x0004C23D
		public string Roles { get; set; }

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0004E046 File Offset: 0x0004C246
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x0004E04E File Offset: 0x0004C24E
		public bool ShowCloseButton { get; set; }

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0004E057 File Offset: 0x0004C257
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x0004E05F File Offset: 0x0004C25F
		public string IncludeCssFiles { get; set; }

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0004E068 File Offset: 0x0004C268
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x0004E070 File Offset: 0x0004C270
		public Unit Height { get; set; }

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0004E079 File Offset: 0x0004C279
		// (set) Token: 0x060018CB RID: 6347 RVA: 0x0004E081 File Offset: 0x0004C281
		public bool AlwaysDockSaveButton { get; set; }

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0004E08A File Offset: 0x0004C28A
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x0004E092 File Offset: 0x0004C292
		public bool HideSlabBorder { get; set; }

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0004E09B File Offset: 0x0004C29B
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x0004E0A3 File Offset: 0x0004C2A3
		public string FVAResource { get; set; }

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0004E0AC File Offset: 0x0004C2AC
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x0004E0B4 File Offset: 0x0004C2B4
		public bool IsPrimarySlab { get; set; }

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0004E0BD File Offset: 0x0004C2BD
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x0004E0C5 File Offset: 0x0004C2C5
		public bool UsePropertyPageStyle { get; set; }

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0004E0CE File Offset: 0x0004C2CE
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x0004E0D6 File Offset: 0x0004C2D6
		public FeatureSet FeatureSet { get; set; }

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0004E0DF File Offset: 0x0004C2DF
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x0004E0E7 File Offset: 0x0004C2E7
		public bool LayoutOnly { get; set; }

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0004E0F0 File Offset: 0x0004C2F0
		internal FieldValidationAssistantExtender FieldValidationAssistantExtender
		{
			get
			{
				return this.fieldValidationAssistantExtender;
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0004E0F8 File Offset: 0x0004C2F8
		internal IBaseFormContentControl PropertiesControl
		{
			get
			{
				if (this.propertyControl == null)
				{
					foreach (object obj in this.Controls)
					{
						Control control = (Control)obj;
						IBaseFormContentControl baseFormContentControl = control as IBaseFormContentControl;
						if (baseFormContentControl != null)
						{
							this.propertyControl = baseFormContentControl;
							break;
						}
					}
				}
				return this.propertyControl;
			}
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0004E16C File Offset: 0x0004C36C
		internal bool AccessibleToUser(IPrincipal user)
		{
			return string.IsNullOrEmpty(this.Roles) || LoginUtil.IsInRoles(user, this.Roles.Split(new char[]
			{
				','
			}));
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0004E1A8 File Offset: 0x0004C3A8
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Context.ThrowIfViewOptionsWithBEParam(this.FeatureSet);
			base.EnsureID();
			if (!string.IsNullOrEmpty(this.FVAResource))
			{
				this.fieldValidationAssistantExtender = new FieldValidationAssistantExtender();
				this.fieldValidationAssistantExtender.HelpId = this.HelpId;
				this.fieldValidationAssistantExtender.IndentCssClass = "baseFrmFvaIndent";
				this.fieldValidationAssistantExtender.LocStringsResource = this.FVAResource;
				if (this.PropertiesControl != null)
				{
					this.fieldValidationAssistantExtender.TargetControlID = this.Parent.UniqueID;
					this.fieldValidationAssistantExtender.Canvas = this.Parent.ClientID;
				}
				this.Controls.Add(this.fieldValidationAssistantExtender);
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0004E268 File Offset: 0x0004C468
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!string.IsNullOrEmpty(this.FVAResource))
			{
				ScriptManager current = ScriptManager.GetCurrent(this.Page);
				current.EnableScriptLocalization = true;
				((ToolkitScriptManager)current).CombineScript(this.FVAResource);
				if (this.fieldValidationAssistantExtender.TargetControlID == null)
				{
					throw new InvalidOperationException("You enabled FVA in the slab but the TargetControlID is not set. See example in DeliverReport.ascx.cs. To turn FVA off on this slab, remove the FVAResource attribute.");
				}
			}
		}

		// Token: 0x04001A79 RID: 6777
		private FieldValidationAssistantExtender fieldValidationAssistantExtender;

		// Token: 0x04001A7A RID: 6778
		private IBaseFormContentControl propertyControl;

		// Token: 0x04001A7B RID: 6779
		private string helpId = string.Empty;
	}
}
