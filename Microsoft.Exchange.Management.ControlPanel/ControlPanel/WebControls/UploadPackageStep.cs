using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020001E9 RID: 489
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.Extension.js")]
	public class UploadPackageStep : WizardStep
	{
		// Token: 0x060025F6 RID: 9718 RVA: 0x000748DC File Offset: 0x00072ADC
		public UploadPackageStep()
		{
			base.ClientClassName = "UploadPackageStep";
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
			htmlGenericControl.Attributes["class"] = "AjaxUploaderButtonDiv";
			this.Controls.Add(new LiteralControl("<div class = \"ExtensionFileselect\">"));
			this.fileLocationLabel = new EncodingLabel();
			this.fileLocationLabel.ID = "fileLocationLbl";
			this.fileLocationLabel.Text = OwaOptionStrings.ExtensionPackageLocation;
			this.Controls.Add(this.fileLocationLabel);
			this.Controls.Add(new LiteralControl("<br /><br />"));
			this.uploaderBase = new UploaderBase();
			this.uploaderBase.ID = "uploader";
			htmlGenericControl.Controls.Add(this.uploaderBase);
			this.Controls.Add(htmlGenericControl);
			this.Controls.Add(new LiteralControl("</div>"));
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000749EC File Offset: 0x00072BEC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			foreach (Binding item in this.Parameters)
			{
				this.uploaderBase.Parameters.Add(item);
			}
		}

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x00074A4C File Offset: 0x00072C4C
		// (set) Token: 0x060025F9 RID: 9721 RVA: 0x00074A54 File Offset: 0x00072C54
		public string ProgressDescription { get; set; }

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x00074A5D File Offset: 0x00072C5D
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BindingCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00074A65 File Offset: 0x00072C65
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x00074A72 File Offset: 0x00072C72
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

		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x00074A80 File Offset: 0x00072C80
		// (set) Token: 0x060025FE RID: 9726 RVA: 0x00074A8D File Offset: 0x00072C8D
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

		// Token: 0x060025FF RID: 9727 RVA: 0x00074A9C File Offset: 0x00072C9C
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.AddComponentProperty("Properties", base.FindPropertiesParent().ClientID);
			scriptDescriptor.AddElementProperty("FileLocationLbl", this.fileLocationLabel.ClientID);
			scriptDescriptor.AddComponentProperty("UploaderImplementation", this.uploaderBase.ClientID);
			scriptDescriptor.AddProperty("ProgressDescription", this.ProgressDescription ?? OwaOptionStrings.PleaseWait);
			return scriptDescriptor;
		}

		// Token: 0x04001F30 RID: 7984
		private UploaderBase uploaderBase;

		// Token: 0x04001F31 RID: 7985
		private EncodingLabel fileLocationLabel;

		// Token: 0x04001F32 RID: 7986
		private BindingCollection parameters = new BindingCollection();
	}
}
