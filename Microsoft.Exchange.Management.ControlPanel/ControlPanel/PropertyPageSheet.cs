using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000263 RID: 611
	[ClientScriptResource("PropertyPageSheet", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class PropertyPageSheet : DataContextProvider, IBaseFormContentControl
	{
		// Token: 0x06002917 RID: 10519 RVA: 0x00081491 File Offset: 0x0007F691
		public PropertyPageSheet()
		{
			this.Sections = new SectionCollection(this);
			base.ViewModel = "PropertyPageViewModel";
			this.UseSetObject = true;
			this.PreLoadData = true;
			this.HasSaveMethod = true;
			this.ReadOnDemand = false;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000814CC File Offset: 0x0007F6CC
		protected void ResolveServiceUrl()
		{
			string name = "sn";
			string text = this.Context.Request.QueryString[name];
			if (!string.IsNullOrWhiteSpace(text))
			{
				DDIHelper.CheckSchemaName(text);
				base.ServiceUrl = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=" + text);
			}
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x0008151F File Offset: 0x0007F71F
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(this.CssClass))
			{
				this.CssClass = "propPane";
			}
			else
			{
				this.CssClass += " propPane";
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00081558 File Offset: 0x0007F758
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.UseWarningPanel)
			{
				this.warningPanel = this.CreateWarningPanel("PropertyPaneWarningPanel");
				this.Controls.Add(this.warningPanel);
			}
			if (this.Content != null && this.Sections.Count > 0)
			{
				throw new NotSupportedException("PropertyPage control cannot have both sections and content");
			}
			if (this.Content != null)
			{
				PropertiesContentPanel propertiesContentPanel = new PropertiesContentPanel();
				propertiesContentPanel.ID = "contentContainer";
				this.Controls.Add(propertiesContentPanel);
				this.Content.InstantiateIn(propertiesContentPanel);
			}
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000815E7 File Offset: 0x0007F7E7
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000815F8 File Offset: 0x0007F7F8
		protected override void OnPreRender(EventArgs e)
		{
			this.ResolveServiceUrl();
			base["DataTransferMode"] = this.DataTransferMode;
			if (this.PreLoadData && base.ServiceUrl != null && this.DataTransferMode != DataTransferMode.Collaboration)
			{
				PowerShellResults powerShellResults;
				if (this.ReadOnDemand)
				{
					string workflowName = this.Sections[0].WorkflowName;
					powerShellResults = base.ServiceUrl.GetObjectOnDemand(this.ObjectIdentity, workflowName);
					powerShellResults.UseAsRbacScopeInCurrentHttpContext();
					this.InitialLoadedWorkflow = workflowName;
				}
				else if (this.UseSetObject)
				{
					powerShellResults = base.ServiceUrl.GetObject(this.ObjectIdentity);
					powerShellResults.UseAsRbacScopeInCurrentHttpContext();
				}
				else
				{
					powerShellResults = base.ServiceUrl.GetObjectForNew(this.ObjectIdentity);
				}
				base["PreLoadResults"] = powerShellResults;
			}
			foreach (Section section in this.Sections)
			{
				if (!string.IsNullOrEmpty(section.SetRoles) && !LoginUtil.IsInRoles(this.Context.User, section.SetRoles.Split(new char[]
				{
					','
				})))
				{
					section.Visible = false;
				}
			}
			base.OnPreRender(e);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x00081740 File Offset: 0x0007F940
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("UseWarningPanel", this.UseWarningPanel, true);
			descriptor.AddProperty("SuppressWarning", this.SuppressWarning, true);
			descriptor.AddProperty("HideErrors", this.HideErrors, true);
			descriptor.AddProperty("ReadOnDemand", this.ReadOnDemand, true);
			descriptor.AddProperty("InitialLoadedWorkflow", this.InitialLoadedWorkflow, true);
			if (this.warningPanel != null)
			{
				descriptor.AddElementProperty("WarningPanel", this.warningPanel.ClientID);
			}
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000817CC File Offset: 0x0007F9CC
		// (set) Token: 0x0600291F RID: 10527 RVA: 0x000817D4 File Offset: 0x0007F9D4
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateInstance(TemplateInstance.Single)]
		[Browsable(false)]
		[Description("Property Pane Content")]
		[TemplateContainer(typeof(PropertiesContentPanel))]
		public virtual ITemplate Content { get; set; }

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000817DD File Offset: 0x0007F9DD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x000817E1 File Offset: 0x0007F9E1
		// (set) Token: 0x06002922 RID: 10530 RVA: 0x000817E9 File Offset: 0x0007F9E9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SectionCollection Sections { get; private set; }

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x000817F2 File Offset: 0x0007F9F2
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x000817FA File Offset: 0x0007F9FA
		public string IdQueryStringField { get; set; }

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x00081804 File Offset: 0x0007FA04
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x0008186B File Offset: 0x0007FA6B
		public Identity ObjectIdentity
		{
			get
			{
				if (this.objectIdentity == null)
				{
					string name = string.IsNullOrEmpty(this.IdQueryStringField) ? "id" : this.IdQueryStringField;
					string text = this.Context.Request.QueryString[name];
					if (!string.IsNullOrEmpty(text))
					{
						this.objectIdentity = Identity.ParseIdentity(text);
					}
				}
				return this.objectIdentity;
			}
			protected set
			{
				this.objectIdentity = value;
			}
		}

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x00081874 File Offset: 0x0007FA74
		// (set) Token: 0x06002928 RID: 10536 RVA: 0x0008187C File Offset: 0x0007FA7C
		public bool PreLoadData { get; set; }

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x00081885 File Offset: 0x0007FA85
		// (set) Token: 0x0600292A RID: 10538 RVA: 0x0008188D File Offset: 0x0007FA8D
		public bool ReadOnDemand { get; set; }

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x00081896 File Offset: 0x0007FA96
		// (set) Token: 0x0600292C RID: 10540 RVA: 0x0008189E File Offset: 0x0007FA9E
		private string InitialLoadedWorkflow { get; set; }

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x000818A8 File Offset: 0x0007FAA8
		private DataTransferMode DataTransferMode
		{
			get
			{
				DataTransferMode result = DataTransferMode.Default;
				string value = this.Context.Request["dtm"];
				if (!string.IsNullOrEmpty(value) && !Enum.TryParse<DataTransferMode>(value, out result))
				{
					throw new BadQueryParameterException("dtm");
				}
				return result;
			}
		}

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x000818EC File Offset: 0x0007FAEC
		// (set) Token: 0x0600292F RID: 10543 RVA: 0x0008191C File Offset: 0x0007FB1C
		public bool UseSetObject
		{
			get
			{
				return ((bool?)base["UseSetObject"]) ?? true;
			}
			set
			{
				base["UseSetObject"] = value;
			}
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x0008192F File Offset: 0x0007FB2F
		// (set) Token: 0x06002931 RID: 10545 RVA: 0x00081941 File Offset: 0x0007FB41
		public string SaveConfirmationText
		{
			get
			{
				return (string)base["SaveConfirmationText"];
			}
			set
			{
				base["SaveConfirmationText"] = value;
			}
		}

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x0008194F File Offset: 0x0007FB4F
		// (set) Token: 0x06002933 RID: 10547 RVA: 0x00081957 File Offset: 0x0007FB57
		public bool SuppressWarning { get; set; }

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x06002934 RID: 10548 RVA: 0x00081960 File Offset: 0x0007FB60
		// (set) Token: 0x06002935 RID: 10549 RVA: 0x00081968 File Offset: 0x0007FB68
		public bool UseWarningPanel { get; set; }

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x00081971 File Offset: 0x0007FB71
		// (set) Token: 0x06002937 RID: 10551 RVA: 0x00081979 File Offset: 0x0007FB79
		public bool HideErrors { get; set; }

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x00081982 File Offset: 0x0007FB82
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x0008198A File Offset: 0x0007FB8A
		public bool HasSaveMethod { get; set; }

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x00081993 File Offset: 0x0007FB93
		WebServiceMethod IBaseFormContentControl.RefreshWebServiceMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x00081996 File Offset: 0x0007FB96
		WebServiceMethod IBaseFormContentControl.SaveWebServiceMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x00081999 File Offset: 0x0007FB99
		bool IBaseFormContentControl.ReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040020B2 RID: 8370
		private Panel warningPanel;

		// Token: 0x040020B3 RID: 8371
		private Identity objectIdentity;
	}
}
