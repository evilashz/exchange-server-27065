using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200002F RID: 47
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("BaseForm", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class BaseForm : EcpContentPage
	{
		// Token: 0x06001900 RID: 6400 RVA: 0x0004E720 File Offset: 0x0004C920
		public BaseForm()
		{
			this.footerPanel = new ButtonsPanel();
			this.contentPanel = new Panel();
			this.inPagePanel = new Panel();
			this.ShowHeader = true;
			this.captionPanel = new Panel();
			this.captionPanel.CssClass = "prpgCap";
			this.CaptionLabel = new EllipsisLabel();
			this.CaptionLabel.ID = "caption";
			WebControl textContainer = ((EllipsisLabel)this.CaptionLabel).TextContainer;
			textContainer.CssClass += " capEllipsisDivLabel";
			this.captionPanel.Controls.Add(this.CaptionLabel);
			this.HelpControl = new HelpControl();
			this.HelpControl.ID = "helpCtrl";
			this.HelpControl.CssClass = "prpgHlp";
			this.contentPanel.ID = "contentPanel";
			this.contentPanel.CssClass = "cttPane";
			this.inPagePanel.ID = "inPagePanel";
			this.inPagePanel.CssClass = "baseFrm prpg";
			this.inPagePanel.Controls.Add(this.captionPanel);
			this.inPagePanel.Controls.Add(this.HelpControl);
			this.inPagePanel.Controls.Add(this.contentPanel);
			this.inPagePanel.Controls.Add(this.footerPanel);
			this.ReservedSpaceForFVA = true;
			this.SetInitialFocus = true;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0004E8A0 File Offset: 0x0004CAA0
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("CommitButton", this.CommitButtonClientID, true);
			descriptor.AddElementProperty("CancelButton", this.CancelButtonClientID, true);
			descriptor.AddElementProperty("ContentPanel", this.ContentPanel.ClientID, true);
			descriptor.AddProperty("HideFieldValidationAssistant", this.HideFieldValidationAssistant, true);
			descriptor.AddProperty("FvaEnabled", this.ReservedSpaceForFVA && !this.HideFieldValidationAssistant, true);
			descriptor.AddProperty("FvaResource", this.FVAResource);
			descriptor.AddProperty("PassingDataOnClient", this.PassingDataOnClient, true);
			descriptor.AddProperty("HideDefaultCancelAction", this.HideDefaultCancelAction, false);
			descriptor.AddProperty("AlwaysCheckDataLoss", this.AlwaysCheckDataLoss, false);
			descriptor.AddProperty("SetInitialFocus", this.SetInitialFocus, false);
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0004E97A File Offset: 0x0004CB7A
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x0004E982 File Offset: 0x0004CB82
		private HelpControl HelpControl { get; set; }

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0004E98B File Offset: 0x0004CB8B
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x0004E993 File Offset: 0x0004CB93
		[Bindable(true)]
		[DefaultValue(true)]
		[Category("Appearance")]
		public bool ShowHeader { get; set; }

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0004E99C File Offset: 0x0004CB9C
		// (set) Token: 0x06001907 RID: 6407 RVA: 0x0004E9AE File Offset: 0x0004CBAE
		[Localizable(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue("")]
		public string Caption
		{
			get
			{
				return ((EllipsisLabel)this.CaptionLabel).Text;
			}
			set
			{
				((EllipsisLabel)this.CaptionLabel).Text = value;
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x0004E9C1 File Offset: 0x0004CBC1
		// (set) Token: 0x06001909 RID: 6409 RVA: 0x0004E9C9 File Offset: 0x0004CBC9
		public WebControl CaptionLabel { get; private set; }

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x0004E9D2 File Offset: 0x0004CBD2
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x0004E9DF File Offset: 0x0004CBDF
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		public string HelpId
		{
			get
			{
				return this.HelpControl.HelpId;
			}
			set
			{
				this.HelpControl.HelpId = value;
			}
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0004E9ED File Offset: 0x0004CBED
		// (set) Token: 0x0600190D RID: 6413 RVA: 0x0004E9F5 File Offset: 0x0004CBF5
		[DefaultValue("")]
		[Bindable(true)]
		[Category("Behavior")]
		public string AdditionalContentPanelStyle { get; set; }

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x0004E9FE File Offset: 0x0004CBFE
		// (set) Token: 0x0600190F RID: 6415 RVA: 0x0004EA06 File Offset: 0x0004CC06
		public string FVAResource { get; set; }

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0004EA0F File Offset: 0x0004CC0F
		// (set) Token: 0x06001911 RID: 6417 RVA: 0x0004EA17 File Offset: 0x0004CC17
		[DefaultValue(true)]
		public bool ReservedSpaceForFVA { get; set; }

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0004EA20 File Offset: 0x0004CC20
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x0004EA28 File Offset: 0x0004CC28
		public bool HideFieldValidationAssistant { get; set; }

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0004EA31 File Offset: 0x0004CC31
		// (set) Token: 0x06001915 RID: 6421 RVA: 0x0004EA39 File Offset: 0x0004CC39
		public bool HideDefaultCancelAction { get; set; }

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0004EA42 File Offset: 0x0004CC42
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x0004EA4A File Offset: 0x0004CC4A
		public bool AlwaysCheckDataLoss { get; set; }

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x0004EA53 File Offset: 0x0004CC53
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x0004EA5B File Offset: 0x0004CC5B
		[DefaultValue(true)]
		public bool SetInitialFocus { get; set; }

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0004EA64 File Offset: 0x0004CC64
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x0004EA6C File Offset: 0x0004CC6C
		public string FieldValidationAssistantCanvas { get; set; }

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x0004EA75 File Offset: 0x0004CC75
		protected Panel ContentPanel
		{
			get
			{
				return this.contentPanel;
			}
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0004EA7D File Offset: 0x0004CC7D
		protected Panel CaptionPanel
		{
			get
			{
				return this.captionPanel;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0004EA85 File Offset: 0x0004CC85
		protected ContentPlaceHolder ContentPlaceHolder
		{
			get
			{
				return this.iMasterPage.ContentPlaceHolder;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0004EA92 File Offset: 0x0004CC92
		protected Panel InPagePanel
		{
			get
			{
				return this.inPagePanel;
			}
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0004EA9A File Offset: 0x0004CC9A
		internal virtual bool PassingDataOnClient
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0004EA9D File Offset: 0x0004CC9D
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x0004EAAA File Offset: 0x0004CCAA
		[Localizable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		[Category("Appearance")]
		public string CommitButtonText
		{
			get
			{
				return this.footerPanel.CommitButtonText;
			}
			set
			{
				this.footerPanel.CommitButtonText = value;
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0004EAB8 File Offset: 0x0004CCB8
		public HtmlButton CommitButton
		{
			get
			{
				return this.footerPanel.CommitButton;
			}
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0004EAC5 File Offset: 0x0004CCC5
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		[Category("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		public string CancelButtonText
		{
			get
			{
				return this.footerPanel.CancelButtonText;
			}
			set
			{
				this.footerPanel.CancelButtonText = value;
			}
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x0004EAE0 File Offset: 0x0004CCE0
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x0004EAED File Offset: 0x0004CCED
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		[Bindable(true)]
		public string CancelButtonTextTagName
		{
			get
			{
				return this.footerPanel.CancelButtonTextTagName;
			}
			set
			{
				this.footerPanel.CancelButtonTextTagName = value;
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x0004EAFB File Offset: 0x0004CCFB
		public HtmlButton CancelButton
		{
			get
			{
				return this.footerPanel.CancelButton;
			}
		}

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x0004EB08 File Offset: 0x0004CD08
		public HtmlButton BackButton
		{
			get
			{
				this.EnsureChildControls();
				return this.FooterPanel.BackButton;
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x0004EB1B File Offset: 0x0004CD1B
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x0004EB28 File Offset: 0x0004CD28
		[Bindable(true)]
		[DefaultValue("")]
		[Category("Appearance")]
		[Localizable(true)]
		public string BackButtonText
		{
			get
			{
				return this.FooterPanel.BackButtonText;
			}
			set
			{
				this.FooterPanel.BackButtonText = value;
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x0004EB36 File Offset: 0x0004CD36
		public HtmlButton NextButton
		{
			get
			{
				this.EnsureChildControls();
				return this.FooterPanel.NextButton;
			}
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0004EB49 File Offset: 0x0004CD49
		// (set) Token: 0x0600192E RID: 6446 RVA: 0x0004EB56 File Offset: 0x0004CD56
		[Bindable(true)]
		[Localizable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		public string NextButtonText
		{
			get
			{
				return this.FooterPanel.NextButtonText;
			}
			set
			{
				this.FooterPanel.NextButtonText = value;
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0004EB64 File Offset: 0x0004CD64
		protected IBaseFormContentControl ContentControl
		{
			get
			{
				this.EnsureChildControls();
				if (this.contentControl == null)
				{
					foreach (object obj in this.ContentPanel.Controls)
					{
						Control control = (Control)obj;
						IBaseFormContentControl baseFormContentControl = control as IBaseFormContentControl;
						if (baseFormContentControl != null)
						{
							this.contentControl = baseFormContentControl;
							break;
						}
					}
				}
				return this.contentControl;
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0004EBE4 File Offset: 0x0004CDE4
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.ContentControl != null && this.ContentControl.SaveWebServiceMethod != null)
			{
				this.footerPanel.SaveWebServiceMethods.Add(this.ContentControl.SaveWebServiceMethod);
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0004EC1C File Offset: 0x0004CE1C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!string.IsNullOrEmpty(this.AdditionalContentPanelStyle))
			{
				Panel panel = this.contentPanel;
				panel.CssClass = panel.CssClass + " " + this.AdditionalContentPanelStyle;
			}
			if (this.ShowHeader)
			{
				Panel panel2 = this.inPagePanel;
				panel2.CssClass += " sHdr";
				this.HelpControl.ShowHelp = base.ShowHelp;
				string caption = this.Caption;
				if (string.IsNullOrEmpty(caption))
				{
					this.CaptionLabel.Attributes.Add("data-value", "{DefaultCaptionText, Mode=OneWay}");
				}
				else if (caption.IsBindingExpression())
				{
					this.CaptionLabel.Attributes.Add("data-value", caption);
					this.Caption = string.Empty;
				}
				this.captionPanel.Attributes.Add("data-control", "ContainerControl");
				this.captionPanel.Attributes.Add("data-cssbinder-uppercase", "{ShowCapitalCaption, Mode=OneWay}");
			}
			else
			{
				Panel panel3 = this.inPagePanel;
				panel3.CssClass += " noHdr";
				this.captionPanel.Visible = false;
				this.CaptionLabel.Visible = false;
				this.HelpControl.Visible = false;
			}
			this.footerPanel.CloseWindowOnCancel = false;
			this.SetFormDefaultButton();
			if (!string.IsNullOrEmpty(this.FVAResource))
			{
				base.ScriptManager.EnableScriptLocalization = true;
				((ToolkitScriptManager)base.ScriptManager).CombineScript(this.FVAResource);
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0004EDA0 File Offset: 0x0004CFA0
		protected override void Render(HtmlTextWriter writer)
		{
			SectionCollection sectionCollection = (this.ContentControl != null) ? this.ContentControl.Sections : null;
			if (sectionCollection != null && sectionCollection.Count > 1)
			{
				Bookmark bookmark = new Bookmark();
				for (int i = 0; i < sectionCollection.Count; i++)
				{
					this.SetInitialFocus = false;
					Section section = sectionCollection[i];
					if (section.Visible)
					{
						bookmark.AddEntry(section.ID, section.Title, section.WorkflowName, section.ClientVisibilityBinding);
						if (i != 0)
						{
							section.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
						}
					}
				}
				this.inPagePanel.Controls.AddAt(2, bookmark);
				Panel panel = this.inPagePanel;
				panel.CssClass += " sBmk";
			}
			else
			{
				Panel panel2 = this.inPagePanel;
				panel2.CssClass += " nobmk";
			}
			if (!this.ReadOnly && this.ContentControl != null && this.ContentControl.ReadOnly)
			{
				this.ReadOnly = true;
			}
			base.Render(writer);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0004EEB1 File Offset: 0x0004D0B1
		protected virtual void SetFormDefaultButton()
		{
			this.SetPanelDefaultButton(this.inPagePanel, this.ReadOnly ? this.footerPanel.CancelButtonUniqueID : this.footerPanel.CommitButtonUniqueID);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0004EEE0 File Offset: 0x0004D0E0
		private void SetPanelDefaultButton(Panel panel, string buttonUniqueId)
		{
			if (panel.NamingContainer == this.Page)
			{
				panel.DefaultButton = buttonUniqueId;
				return;
			}
			string uniqueID = panel.NamingContainer.UniqueID;
			int num = uniqueID.Length + 1;
			panel.DefaultButton = buttonUniqueId.Substring(num, buttonUniqueId.Length - num);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0004EF30 File Offset: 0x0004D130
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode)
			{
				MasterPage master = base.Master;
				while (master != null && this.iMasterPage == null)
				{
					this.iMasterPage = (master as IMasterPage);
					master = master.Master;
				}
			}
			if (base.Form != null)
			{
				Control contentPlaceHolder = this.ContentPlaceHolder;
				if (contentPlaceHolder != null)
				{
					this.InjectDefaultLayoutControls(contentPlaceHolder);
					this.InitExtenderParameters();
				}
			}
			if (!string.IsNullOrEmpty(this.SetRoles) && !LoginUtil.IsInRoles(this.Context.User, this.SetRoles.Split(new char[]
			{
				','
			})))
			{
				this.ReadOnly = true;
			}
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0004EFD0 File Offset: 0x0004D1D0
		private void InjectDefaultLayoutControls(Control rootControl)
		{
			Control[] array = new Control[rootControl.Controls.Count];
			rootControl.Controls.CopyTo(array, 0);
			rootControl.Controls.Clear();
			foreach (Control child in array)
			{
				this.ContentPanel.Controls.Add(child);
			}
			rootControl.Controls.Add(this.inPagePanel);
			this.contentControl = null;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0004F044 File Offset: 0x0004D244
		private void InitExtenderParameters()
		{
			if (!this.HideFieldValidationAssistant)
			{
				FieldValidationAssistantExtender fieldValidationAssistantExtender = new FieldValidationAssistantExtender();
				fieldValidationAssistantExtender.HelpId = this.HelpId;
				fieldValidationAssistantExtender.LocStringsResource = this.FVAResource;
				fieldValidationAssistantExtender.TargetControlID = this.ContentPanel.UniqueID;
				fieldValidationAssistantExtender.Canvas = (this.FieldValidationAssistantCanvas ?? this.ContentPanel.ClientID);
				fieldValidationAssistantExtender.IndentCssClass = "baseFrmFvaIndent";
				this.inPagePanel.Controls.Add(fieldValidationAssistantExtender);
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0004F0BF File Offset: 0x0004D2BF
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x0004F0CF File Offset: 0x0004D2CF
		public bool ReadOnly
		{
			get
			{
				return this.footerPanel.State == ButtonsPanelState.ReadOnly;
			}
			set
			{
				this.footerPanel.State = (value ? ButtonsPanelState.ReadOnly : ButtonsPanelState.SaveCancel);
			}
		}

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x0004F0E3 File Offset: 0x0004D2E3
		public string CommitButtonClientID
		{
			get
			{
				this.EnsureChildControls();
				return this.footerPanel.CommitButtonClientID;
			}
		}

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x0004F0F6 File Offset: 0x0004D2F6
		public string CancelButtonClientID
		{
			get
			{
				this.EnsureChildControls();
				return this.footerPanel.CancelButtonClientID;
			}
		}

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x0004F109 File Offset: 0x0004D309
		// (set) Token: 0x0600193D RID: 6461 RVA: 0x0004F111 File Offset: 0x0004D311
		public string SetRoles { get; set; }

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0004F11A File Offset: 0x0004D31A
		protected ButtonsPanel FooterPanel
		{
			get
			{
				return this.footerPanel;
			}
		}

		// Token: 0x04001A92 RID: 6802
		private Panel inPagePanel;

		// Token: 0x04001A93 RID: 6803
		private Panel contentPanel;

		// Token: 0x04001A94 RID: 6804
		private Panel captionPanel;

		// Token: 0x04001A95 RID: 6805
		private ButtonsPanel footerPanel;

		// Token: 0x04001A96 RID: 6806
		private IMasterPage iMasterPage;

		// Token: 0x04001A97 RID: 6807
		private IBaseFormContentControl contentControl;
	}
}
