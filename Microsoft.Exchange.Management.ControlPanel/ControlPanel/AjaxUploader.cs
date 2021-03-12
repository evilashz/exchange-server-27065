using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200056B RID: 1387
	[ToolboxData("<{0}:AjaxUploader runat=server></{0}:AjaxUploader>")]
	[ClientScriptResource("AjaxUploader", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class AjaxUploader : ScriptControlBase
	{
		// Token: 0x06004082 RID: 16514 RVA: 0x000C4D6C File Offset: 0x000C2F6C
		public AjaxUploader() : base(HtmlTextWriterTag.Div)
		{
			this.HasDefaultValue = true;
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
			htmlGenericControl.Attributes["class"] = "AjaxUploaderNameDiv";
			this.fileNameLabel = new EncodingLabel();
			this.fileNameLabel.ID = "fileNameLbl";
			htmlGenericControl.Controls.Add(this.fileNameLabel);
			this.progressLabel = new EncodingLabel();
			this.progressLabel.ID = "progressLbl";
			this.progressLabel.Text = Strings.Uploading;
			htmlGenericControl.Controls.Add(this.progressLabel);
			this.Controls.Add(htmlGenericControl);
			this.separator = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
			this.separator.ID = "separator";
			this.separator.Attributes["class"] = "AjaxUploaderSeparator";
			this.Controls.Add(this.separator);
			htmlGenericControl = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
			htmlGenericControl.Attributes["class"] = "AjaxUploaderNameDiv";
			this.cancelButton = new HyperLink();
			this.cancelButton.ID = "cancelBtn";
			this.cancelButton.NavigateUrl = "#";
			this.cancelButton.Text = Strings.CancelUpload;
			htmlGenericControl.Controls.Add(this.cancelButton);
			this.deleteButton = new HyperLink();
			this.deleteButton.ID = "deleteBtn";
			this.deleteButton.NavigateUrl = "#";
			CommandSprite commandSprite = new CommandSprite();
			commandSprite.ImageId = CommandSprite.SpriteId.ToolBarDeleteSmall;
			commandSprite.AlternateText = Strings.DeleteCommandText;
			this.deleteButton.Controls.Add(commandSprite);
			htmlGenericControl.Controls.Add(this.deleteButton);
			EncodingLabel child = Util.CreateHiddenForSRLabel(string.Empty, this.cancelButton.ID);
			EncodingLabel child2 = Util.CreateHiddenForSRLabel(string.Empty, this.deleteButton.ID);
			htmlGenericControl.Controls.Add(child);
			htmlGenericControl.Controls.Add(child2);
			this.Controls.Add(htmlGenericControl);
			this.Controls.Add(new LiteralControl("<br />"));
			htmlGenericControl = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
			htmlGenericControl.Attributes["class"] = "AjaxUploaderButtonDiv";
			this.editFileButton = new IconButton();
			this.editFileButton.CssClass = "ajaxUploaderEditButton";
			this.editFileButton.ID = "editFileBtn";
			if (string.IsNullOrEmpty(this.editFileButton.Text))
			{
				this.editFileButton.Text = Strings.DefaultEditButtonText;
			}
			htmlGenericControl.Controls.Add(this.editFileButton);
			this.uploaderBase = new UploaderBase();
			this.uploaderBase.ID = "uploader";
			htmlGenericControl.Controls.Add(this.uploaderBase);
			this.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000C5094 File Offset: 0x000C3294
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.deleteButton.Attributes["SetRoles"] = this.ChangeFileNameRoles;
			base.Attributes["SetRoles"] = this.ChangeFileNameRoles;
			string text = string.Empty;
			if (this.ChangeFileNameRoles != null)
			{
				text += this.ChangeFileNameRoles;
			}
			if (this.UploadRoles != null)
			{
				if (text != string.Empty)
				{
					text += "+";
				}
				text += this.UploadRoles;
			}
			if (text != string.Empty)
			{
				this.editFileButton.Attributes["SetRoles"] = text;
			}
			foreach (Binding item in this.Parameters)
			{
				this.uploaderBase.Parameters.Add(item);
			}
		}

		// Token: 0x170024FF RID: 9471
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x000C5190 File Offset: 0x000C3390
		// (set) Token: 0x06004085 RID: 16517 RVA: 0x000C519D File Offset: 0x000C339D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public string EditButtonText
		{
			get
			{
				return this.editFileButton.Text;
			}
			set
			{
				this.editFileButton.Text = value;
			}
		}

		// Token: 0x17002500 RID: 9472
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000C51AB File Offset: 0x000C33AB
		// (set) Token: 0x06004087 RID: 16519 RVA: 0x000C51B3 File Offset: 0x000C33B3
		public string DefaultText { get; set; }

		// Token: 0x17002501 RID: 9473
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x000C51BC File Offset: 0x000C33BC
		// (set) Token: 0x06004089 RID: 16521 RVA: 0x000C51C4 File Offset: 0x000C33C4
		public string UploadRoles { get; set; }

		// Token: 0x17002502 RID: 9474
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x000C51CD File Offset: 0x000C33CD
		// (set) Token: 0x0600408B RID: 16523 RVA: 0x000C51D5 File Offset: 0x000C33D5
		public string ChangeFileNameRoles { get; set; }

		// Token: 0x17002503 RID: 9475
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x000C51DE File Offset: 0x000C33DE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public BindingCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17002504 RID: 9476
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x000C51E6 File Offset: 0x000C33E6
		// (set) Token: 0x0600408E RID: 16526 RVA: 0x000C51F3 File Offset: 0x000C33F3
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] ParameterNames
		{
			get
			{
				return this.uploaderBase.ParameterNames;
			}
			set
			{
				this.uploaderBase.ParameterNames = value;
			}
		}

		// Token: 0x17002505 RID: 9477
		// (get) Token: 0x0600408F RID: 16527 RVA: 0x000C5201 File Offset: 0x000C3401
		// (set) Token: 0x06004090 RID: 16528 RVA: 0x000C520E File Offset: 0x000C340E
		public UploadHandlers UploadHandlerClass
		{
			get
			{
				return this.uploaderBase.UploadHandlerClass;
			}
			set
			{
				this.uploaderBase.UploadHandlerClass = value;
			}
		}

		// Token: 0x17002506 RID: 9478
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x000C521C File Offset: 0x000C341C
		// (set) Token: 0x06004092 RID: 16530 RVA: 0x000C5229 File Offset: 0x000C3429
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] Extensions
		{
			get
			{
				return this.uploaderBase.Extensions;
			}
			set
			{
				this.uploaderBase.Extensions = value;
			}
		}

		// Token: 0x17002507 RID: 9479
		// (get) Token: 0x06004093 RID: 16531 RVA: 0x000C5237 File Offset: 0x000C3437
		// (set) Token: 0x06004094 RID: 16532 RVA: 0x000C523F File Offset: 0x000C343F
		[DefaultValue(true)]
		public bool HasDefaultValue { get; set; }

		// Token: 0x17002508 RID: 9480
		// (get) Token: 0x06004095 RID: 16533 RVA: 0x000C5248 File Offset: 0x000C3448
		// (set) Token: 0x06004096 RID: 16534 RVA: 0x000C5250 File Offset: 0x000C3450
		public bool InitStateAsEditClicked { get; set; }

		// Token: 0x06004097 RID: 16535 RVA: 0x000C525C File Offset: 0x000C345C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("DefaultText", this.DefaultText);
			descriptor.AddProperty("HasDefaultValue", this.HasDefaultValue);
			descriptor.AddProperty("InitStateAsEditClicked", this.InitStateAsEditClicked);
			descriptor.AddElementProperty("FileNameLbl", this.fileNameLabel.ClientID);
			descriptor.AddElementProperty("ProgressLbl", this.progressLabel.ClientID);
			descriptor.AddElementProperty("SeparatorDiv", this.separator.ClientID);
			descriptor.AddElementProperty("CancelBtn", this.cancelButton.ClientID);
			descriptor.AddElementProperty("DeleteBtn", this.deleteButton.ClientID);
			descriptor.AddElementProperty("EditFileBtn", this.editFileButton.ClientID);
			descriptor.AddComponentProperty("UploaderImplementation", this.uploaderBase.ClientID);
		}

		// Token: 0x04002AE5 RID: 10981
		private EncodingLabel fileNameLabel;

		// Token: 0x04002AE6 RID: 10982
		private EncodingLabel progressLabel;

		// Token: 0x04002AE7 RID: 10983
		private HtmlGenericControl separator;

		// Token: 0x04002AE8 RID: 10984
		private HyperLink cancelButton;

		// Token: 0x04002AE9 RID: 10985
		protected HyperLink deleteButton;

		// Token: 0x04002AEA RID: 10986
		protected IconButton editFileButton;

		// Token: 0x04002AEB RID: 10987
		private UploaderBase uploaderBase;

		// Token: 0x04002AEC RID: 10988
		private BindingCollection parameters = new BindingCollection();
	}
}
