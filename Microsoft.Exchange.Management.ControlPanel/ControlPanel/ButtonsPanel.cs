using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200056F RID: 1391
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ToolboxData("<{0}:ButtonsPanel runat=\"server\" />")]
	public class ButtonsPanel : Panel, INamingContainer
	{
		// Token: 0x060040A0 RID: 16544 RVA: 0x000C5638 File Offset: 0x000C3838
		public ButtonsPanel()
		{
			this.btnCommit = new HtmlButton();
			this.btnCancel = new HtmlButton();
			this.btnBack = new HtmlButton();
			this.btnNext = new HtmlButton();
			this.btnCommit.ID = "btnCommit";
			this.btnCommit.InnerText = Strings.CommitButtonText;
			this.btnCommit.Attributes.Add("data-control", "Button");
			this.btnCommit.Attributes.Add("data-Command", "{SaveCommand, Mode=OneWay}");
			this.btnCancel.ID = "btnCancel";
			this.btnCancel.InnerText = Strings.CancelButtonText;
			this.btnCancel.Attributes.Add("data-control", "Button");
			this.btnCancel.Attributes.Add("data-Command", "{CancelCommand, Mode=OneWay}");
			this.btnBack.ID = "btnBack";
			this.btnBack.InnerText = ClientStrings.Back;
			this.btnBack.Attributes.Add("data-control", "Button");
			this.btnBack.Attributes.Add("data-Command", "{BackCommand, Mode=OneWay}");
			this.btnBack.Visible = false;
			this.btnNext.ID = "btnNext";
			this.btnNext.InnerText = ClientStrings.Next;
			this.btnNext.Attributes.Add("data-control", "Button");
			this.btnNext.Attributes.Add("data-Command", "{NextCommand, Mode=OneWay}");
			this.btnNext.Visible = false;
			this.CssClass = "btnPane";
			this.ID = "ButtonsPanel";
			Util.RequireUpdateProgressPopUp(this);
			this.SaveWebServiceMethods = new List<WebServiceMethod>();
		}

		// Token: 0x1700250A RID: 9482
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x000C5810 File Offset: 0x000C3A10
		public HtmlButton CommitButton
		{
			get
			{
				return this.btnCommit;
			}
		}

		// Token: 0x1700250B RID: 9483
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x000C5818 File Offset: 0x000C3A18
		// (set) Token: 0x060040A3 RID: 16547 RVA: 0x000C5820 File Offset: 0x000C3A20
		public ButtonsPanelState State
		{
			get
			{
				return this.state;
			}
			set
			{
				if (this.state != value)
				{
					this.state = value;
					switch (value)
					{
					case ButtonsPanelState.SaveCancel:
						this.btnCommit.Visible = true;
						this.btnCancel.Visible = true;
						this.CancelButtonText = Strings.CancelButtonText;
						return;
					case ButtonsPanelState.ReadOnly:
						this.btnCommit.Visible = false;
						this.btnCancel.Visible = true;
						this.CancelButtonText = ClientStrings.Close;
						return;
					case ButtonsPanelState.Save:
						this.btnCommit.Visible = true;
						this.btnCancel.Visible = false;
						return;
					case ButtonsPanelState.Wizard:
						this.btnBack.Visible = true;
						this.btnNext.Visible = true;
						this.btnCancel.Visible = true;
						this.btnCommit.Visible = true;
						this.CancelButtonText = Strings.CancelButtonText;
						this.CommitButtonText = Strings.FinishButtonText;
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x1700250C RID: 9484
		// (get) Token: 0x060040A4 RID: 16548 RVA: 0x000C590F File Offset: 0x000C3B0F
		// (set) Token: 0x060040A5 RID: 16549 RVA: 0x000C591C File Offset: 0x000C3B1C
		[Category("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		public string CommitButtonText
		{
			get
			{
				return this.btnCommit.InnerText;
			}
			set
			{
				this.btnCommit.InnerText = value;
			}
		}

		// Token: 0x1700250D RID: 9485
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x000C592A File Offset: 0x000C3B2A
		// (set) Token: 0x060040A7 RID: 16551 RVA: 0x000C5937 File Offset: 0x000C3B37
		[Category("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		public string CancelButtonText
		{
			get
			{
				return this.btnCancel.InnerText;
			}
			set
			{
				this.btnCancel.InnerText = value;
			}
		}

		// Token: 0x1700250E RID: 9486
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x000C5945 File Offset: 0x000C3B45
		// (set) Token: 0x060040A9 RID: 16553 RVA: 0x000C595C File Offset: 0x000C3B5C
		[Bindable(true)]
		[DefaultValue("")]
		[Category("Appearance")]
		[Localizable(true)]
		public string CancelButtonTextTagName
		{
			get
			{
				return this.btnCancel.Attributes["data-texttagname"];
			}
			set
			{
				this.btnCancel.Attributes.Add("data-texttagname", value);
			}
		}

		// Token: 0x1700250F RID: 9487
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x000C5974 File Offset: 0x000C3B74
		// (set) Token: 0x060040AB RID: 16555 RVA: 0x000C597C File Offset: 0x000C3B7C
		[Category("Behavior")]
		[Bindable(true)]
		[DefaultValue(false)]
		public bool CloseWindowOnCancel
		{
			get
			{
				return this.closeWindowOnCancel;
			}
			set
			{
				if (this.closeWindowOnCancel != value)
				{
					this.closeWindowOnCancel = value;
					if (this.closeWindowOnCancel)
					{
						this.btnCancel.Attributes["onclick"] = "window.close();";
					}
				}
			}
		}

		// Token: 0x17002510 RID: 9488
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x000C59B0 File Offset: 0x000C3BB0
		[Category("Behavior")]
		[DefaultValue("")]
		public string CommitButtonUniqueID
		{
			get
			{
				this.EnsureChildControls();
				return this.btnCommit.UniqueID;
			}
		}

		// Token: 0x17002511 RID: 9489
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x000C59C3 File Offset: 0x000C3BC3
		[Category("Behavior")]
		[DefaultValue("")]
		public string CommitButtonClientID
		{
			get
			{
				this.EnsureChildControls();
				return this.btnCommit.ClientID;
			}
		}

		// Token: 0x17002512 RID: 9490
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x000C59D6 File Offset: 0x000C3BD6
		public HtmlButton CancelButton
		{
			get
			{
				return this.btnCancel;
			}
		}

		// Token: 0x17002513 RID: 9491
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x000C59DE File Offset: 0x000C3BDE
		[DefaultValue("")]
		[Category("Behavior")]
		public string CancelButtonUniqueID
		{
			get
			{
				this.EnsureChildControls();
				return this.btnCancel.UniqueID;
			}
		}

		// Token: 0x17002514 RID: 9492
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x000C59F1 File Offset: 0x000C3BF1
		[Category("Behavior")]
		[DefaultValue("")]
		public string CancelButtonClientID
		{
			get
			{
				this.EnsureChildControls();
				return this.btnCancel.ClientID;
			}
		}

		// Token: 0x17002515 RID: 9493
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x000C5A04 File Offset: 0x000C3C04
		[DefaultValue("")]
		[Category("Behavior")]
		public string BackButtonClientID
		{
			get
			{
				this.EnsureChildControls();
				return this.btnBack.ClientID;
			}
		}

		// Token: 0x17002516 RID: 9494
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000C5A17 File Offset: 0x000C3C17
		public HtmlButton BackButton
		{
			get
			{
				return this.btnBack;
			}
		}

		// Token: 0x17002517 RID: 9495
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x000C5A1F File Offset: 0x000C3C1F
		// (set) Token: 0x060040B4 RID: 16564 RVA: 0x000C5A2C File Offset: 0x000C3C2C
		[Bindable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Appearance")]
		public string BackButtonText
		{
			get
			{
				return this.btnBack.InnerText;
			}
			set
			{
				this.btnBack.InnerText = value;
			}
		}

		// Token: 0x17002518 RID: 9496
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x000C5A3A File Offset: 0x000C3C3A
		public HtmlButton NextButton
		{
			get
			{
				return this.btnNext;
			}
		}

		// Token: 0x17002519 RID: 9497
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x000C5A42 File Offset: 0x000C3C42
		// (set) Token: 0x060040B7 RID: 16567 RVA: 0x000C5A4F File Offset: 0x000C3C4F
		[Category("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		public string NextButtonText
		{
			get
			{
				return this.btnNext.InnerText;
			}
			set
			{
				this.btnNext.InnerText = value;
			}
		}

		// Token: 0x1700251A RID: 9498
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x000C5A5D File Offset: 0x000C3C5D
		[DefaultValue("")]
		[Category("Behavior")]
		public string NextButtonUniqueID
		{
			get
			{
				this.EnsureChildControls();
				return this.btnNext.UniqueID;
			}
		}

		// Token: 0x1700251B RID: 9499
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x000C5A70 File Offset: 0x000C3C70
		[DefaultValue("")]
		[Category("Behavior")]
		public string NextButtonClientID
		{
			get
			{
				this.EnsureChildControls();
				return this.btnNext.ClientID;
			}
		}

		// Token: 0x1700251C RID: 9500
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x000C5A83 File Offset: 0x000C3C83
		// (set) Token: 0x060040BB RID: 16571 RVA: 0x000C5A8B File Offset: 0x000C3C8B
		public WebServiceMethod LoadWebServiceMethod { get; set; }

		// Token: 0x1700251D RID: 9501
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x000C5A94 File Offset: 0x000C3C94
		// (set) Token: 0x060040BD RID: 16573 RVA: 0x000C5A9C File Offset: 0x000C3C9C
		public List<WebServiceMethod> SaveWebServiceMethods { get; private set; }

		// Token: 0x060040BE RID: 16574 RVA: 0x000C5AA8 File Offset: 0x000C3CA8
		protected override void OnPreRender(EventArgs e)
		{
			int count = this.SaveWebServiceMethods.Count;
			if (this.State != ButtonsPanelState.ReadOnly && count > 0)
			{
				this.invokeSaveWebService = new InvokeWebService();
				this.invokeSaveWebService.ID = "webServiceBehaviorForCommit";
				this.invokeSaveWebService.TargetControlID = this.btnCommit.ID;
				this.invokeSaveWebService.EnableConfirmation = true;
				this.invokeSaveWebService.EnableProgressPopup = true;
				this.invokeSaveWebService.IsSaveMethod = true;
				if (this.State == ButtonsPanelState.SaveCancel || this.State == ButtonsPanelState.Wizard)
				{
					this.invokeSaveWebService.CloseAfterSuccess = true;
				}
				if (this.State == ButtonsPanelState.SaveCancel)
				{
					if (count > 1)
					{
						throw new InvalidOperationException("Have more than one SaveWebServiceMethod while ButtonPanel state is SaveCancel.");
					}
					this.invokeSaveWebService.AssociateElementID = this.btnCancel.ClientID;
				}
				else if (this.State == ButtonsPanelState.Wizard)
				{
					this.invokeSaveWebService.AssociateElementID = this.btnBack.ClientID + "," + this.btnCancel.ClientID;
				}
				this.Controls.Add(this.invokeSaveWebService);
			}
			if (this.State == ButtonsPanelState.SaveCancel && this.LoadWebServiceMethod != null)
			{
				InvokeWebService invokeWebService = new InvokeWebService();
				invokeWebService.ID = "webServiceBehaviorForReload";
				invokeWebService.TargetControlID = this.btnCancel.ID;
				invokeWebService.WebServiceMethods.Add(this.LoadWebServiceMethod);
				this.Controls.Add(invokeWebService);
				InvokeWebService invokeWebService2 = new InvokeWebService();
				invokeWebService2.ID = "webServiceBehaviorForDisableSave";
				invokeWebService2.TargetControlID = this.btnCommit.ID;
				invokeWebService2.Trigger = string.Empty;
				invokeWebService2.WebServiceMethods.Add(this.LoadWebServiceMethod);
				this.Controls.Add(invokeWebService2);
			}
			base.OnPreRender(e);
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x000C5C60 File Offset: 0x000C3E60
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.State == ButtonsPanelState.Save || this.State == ButtonsPanelState.SaveCancel)
			{
				this.btnCommit.Attributes.Add("data-visible", "{IsReadOnly, Mode=OneWay, ConvertTo=ValueConverter.Not}");
				this.btnCancel.Attributes.Add("data-text", "{IsReadOnly, Mode=OneWay, ConvertTo=ValueConverter.IIF, ConverterParameter=json:[ImportedStrings.Close,'" + HttpUtility.JavaScriptStringEncode(this.CancelButtonText) + "']}");
			}
			if (this.invokeSaveWebService != null)
			{
				this.invokeSaveWebService.WebServiceMethods.Clear();
				this.invokeSaveWebService.WebServiceMethods.AddRange(this.SaveWebServiceMethods);
			}
			base.Render(writer);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x000C5CFC File Offset: 0x000C3EFC
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			switch (this.State)
			{
			case ButtonsPanelState.SaveCancel:
				this.Controls.Add(this.btnCommit);
				this.Controls.Add(this.btnCancel);
				return;
			case ButtonsPanelState.ReadOnly:
				this.Controls.Add(this.btnCancel);
				return;
			case ButtonsPanelState.Save:
				this.Controls.Add(this.btnCommit);
				return;
			case ButtonsPanelState.Wizard:
				this.Controls.Add(this.btnBack);
				this.Controls.Add(this.btnNext);
				this.Controls.Add(this.btnCommit);
				this.Controls.Add(this.btnCancel);
				return;
			default:
				return;
			}
		}

		// Token: 0x04002AFE RID: 11006
		private const string CloseWindowScript = "window.close();";

		// Token: 0x04002AFF RID: 11007
		private HtmlButton btnCommit;

		// Token: 0x04002B00 RID: 11008
		private HtmlButton btnCancel;

		// Token: 0x04002B01 RID: 11009
		private HtmlButton btnBack;

		// Token: 0x04002B02 RID: 11010
		private HtmlButton btnNext;

		// Token: 0x04002B03 RID: 11011
		private bool closeWindowOnCancel;

		// Token: 0x04002B04 RID: 11012
		private InvokeWebService invokeSaveWebService;

		// Token: 0x04002B05 RID: 11013
		private ButtonsPanelState state;
	}
}
